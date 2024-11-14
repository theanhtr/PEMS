from weather import WeatherService
from predict import PredictService
from report import ReportService
import asyncio
import datetime
import numpy

# Hàm chạy tự động tính toán dự đoán
async def predict_auto_calculate():
    print("Chạy tự động tính toán lúc: " + datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
    weather_service = WeatherService()
    predict_service = PredictService()
    report_service = ReportService()

    # Lấy các dự án đang trong mùa vụ để tính toán
    predict_data = await predict_service.fetch_predict_in_season()

    if not len(predict_data.get('Data', [])):
        print ("Không có dự đoán nào đang trong mùa vụ: " + datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
        return

    for predict in predict_data.get('Data', []):
        # Lấy giá trị currentStartDate từ predict
        predict_start_date = datetime.datetime.strptime(predict.get('CurrentStartDate'), '%Y-%m-%dT%H:%M:%S%z').date()
        end_date = (predict_start_date + datetime.timedelta(days=58)).strftime('%Y-%m-%d')

        # Lấy ra các trạng thái của sâu bệnh
        pest_stages = await predict_service.fetch_pest_stage(predict.get('PestId'))

        # lấy thông tin thời tiết của địa điểm dự báo
        weather = await weather_service.fetch_weather_temperature_range(predict.get('ProvinceName'), predict.get('DistrictName'), predict.get('WardName'), predict.get('Address'), predict_start_date.strftime('%Y-%m-%d'), end_date)
        
        if not weather:
            print ("Không có thông tin thời tiết: ",
                   predict.get('ProvinceName')," " +
                   predict.get('DistrictName')," ",
                   predict.get('WardName')," ",
                   predict.get('Address')," ",
                   predict_start_date.strftime('%Y-%m-%d')," ",
                   end_date)
            return

        # tính toán trạng thái của sâu bệnh
        stages_by_day = await gdd(predict, pest_stages, weather)
        
        # Lấy ra các trạng thái của cây trồng
        crop_stages = await predict_service.fetch_crop_stage(predict.get('CropId'))
        
        # Lấy ra các trạng thái cảnh báo
        level_warnings = await predict_service.fetch_level_warning(predict.get('CropId'), predict.get('PestId'))

        # lấy report để biết trạng thái cây trồng để đưa ra cảnh báo phù hợp
        reports = await report_service.fetch_report(predict['ProvinceId'], predict['DistrictId'], predict['WardId'], predict_start_date.strftime('%Y-%m-%d'), end_date, predict['CropId'])

        stages_by_day = update_daily_forecast_predict(stages_by_day, level_warnings, reports.get('Data', []), predict)

        await predict_service.update_daily_forecast(predict.get('PredictId'), stages_by_day)

async def gdd(predict, pest_stages, weather):
    # Tính toán GDD
    print ("dự báo bắt đầu ngày: ")
    print (datetime.datetime.strptime(predict.get('CurrentStartDate'), '%Y-%m-%dT%H:%M:%S%z').date())
    print ("tại: ")
    print (predict.get('WardName') + " , " +
                   predict.get('DistrictName') + " , " + 
                   predict.get('ProvinceName'))
    print ("thông tin thời tiết: ")
    print (weather)

    #trích xuất các thông tin thời tiết từ weather
    temps = weather.get('Daily', {}).get('Temperature2mMean') #nhiệt độ trung bình trong ngày
    rains = weather.get('Daily', {}).get('RainSum') #lượng mưa trong ngày
    humidities = weather.get('Daily', {}).get('RelativeHumidity2mMean') #độ ẩm trung bình trong ngày

    # Parse ngày bắt đầu từ `predict`
    start_date = datetime.datetime.strptime(predict.get('CurrentStartDate'), '%Y-%m-%dT%H:%M:%S%z').date()
    
    # Sắp xếp các giai đoạn theo tổng tích ôn yêu cầu (K) để đảm bảo đúng thứ tự phát triển
    pest_stages_sorted = sorted(pest_stages, key=lambda x: x['K'])
    
    # Lấy các tên giai đoạn, nhiệt độ phát dục (T0), và tổng tích ôn yêu cầu (K) cho từng giai đoạn
    pest_stage_names = [stage['PestStageName'] for stage in pest_stages_sorted]
    pest_stage_ids = [stage['PestStageId'] for stage in pest_stages_sorted]
    base_temps = numpy.array([stage['T0'] for stage in pest_stages_sorted])
    degree_days_required = numpy.array([stage['K'] for stage in pest_stages_sorted])

    # Tính toán GDD hàng ngày
    cumulative_gdd = 0
    stages_by_day = []
    current_stage_index = 0
    
    # Duyệt qua từng ngày với dữ liệu nhiệt độ
    for i, temp in enumerate(temps):
        # Tính GDD cho ngày hiện tại dựa trên nhiệt độ ngày và nhiệt độ phát dục của giai đoạn hiện tại
        daily_gdd = max(temp - base_temps[current_stage_index], 0)
        cumulative_gdd += daily_gdd
        
        # Lấy ngày hiện tại
        date = start_date + datetime.timedelta(days=i)
        
        # Ghi lại giai đoạn hiện tại trước khi kiểm tra điều kiện đổi giai đoạn
        stages_by_day.append({"stage": pest_stage_names[current_stage_index], "stage_id": pest_stage_ids[current_stage_index], "temp": temp, "rain": rains[i], "humidity": humidities[i], "date": date.strftime('%Y-%m-%d')})
        
        # Kiểm tra nếu GDD tích lũy đã đạt ngưỡng của giai đoạn hiện tại
        if cumulative_gdd >= degree_days_required[current_stage_index]:
            # Chuyển sang giai đoạn tiếp theo vào ngày tiếp theo
            current_stage_index += 1
            
            # Nếu đã vượt qua giai đoạn cuối thì reset về giai đoạn đầu tiên và GDD tích lũy về 0
            if current_stage_index >= len(pest_stages_sorted):
                current_stage_index = 0
                cumulative_gdd = 0
    
    return stages_by_day

def update_daily_forecast_predict(stages_by_day, level_warnings, reports, predict):
    # nếu không có report thì không thể cập nhật mức độ cảnh báo
    if not reports:
        print ("Không có cảnh báo của chuyên gia: ",
                    predict.get('ProvinceName')," " +
                    predict.get('DistrictName')," ",
                    predict.get('WardName')," ",
                    predict.get('Address')," ",
                    predict.get('CurrentStartDate'))
        return stages_by_day

    # chạy qua từng ngày để cập nhật mức độ cảnh báo:
    # + xem hôm đó có báo cáo nào không (bằng cách so sánh date trong stage và reportDate ),
    #  nếu có thì lấy id sâu hôm đó + id của trạng thái cây trong cảnh báo để tìm mức độ cảnh báo trong level_warnings
    # + nếu không có thì bỏ qua
    for stage in stages_by_day:
        stage_date = datetime.datetime.strptime(stage.get('date'), '%Y-%m-%d').date()
        for report in reports:
            report_date = datetime.datetime.strptime(report.get('ReportDate'), '%Y-%m-%dT%H:%M:%S%z').date()
            if stage_date == report_date:
                for level in level_warnings:
                    if level.get('PestStageId') == stage.get('stage_id') and level.get('CropStageId') == report.get('CropStageId'):
                        stage['level_warning'] = level.get('LevelWarningName')
                        break

    return stages_by_day

# Lập lịch chạy hàm `predict_auto_calculate` mỗi 1 phút
async def schedule_job():
    while True:
        await predict_auto_calculate()
        await asyncio.sleep(1 * 60)  # Đợi 1 phút trước khi chạy lại

# Chạy chương trình async
if __name__ == "__main__":
    asyncio.run(schedule_job())
