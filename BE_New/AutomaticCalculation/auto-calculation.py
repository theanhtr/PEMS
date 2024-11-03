import asyncio
from weather import WeatherService
from predict import PredictService
from report import ReportService
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
        end_date = (predict_start_date + datetime.timedelta(days=30)).strftime('%Y-%m-%d')

        # Lấy ra các trạng thái của sâu bệnh
        pest_stages = await predict_service.fetch_crop_stage(predict.get('PestId'))

        # lấy thông tin thời tiết của địa điểm dự báo
        weather = await weather_service.fetch_weather_temperature_range(predict.get('ProvinceName'), predict.get('DistrictName'), predict.get('WardName'), predict.get('Address'), predict_start_date.strftime('%Y-%m-%d'), end_date)

        print ('weather mean: ')
        print(weather.get('Daily', {}).get('Temperature2mMean'))
        print ('weather max: ')
        print(weather.get('Daily', {}).get('Temperature2mMax'))
        # tính toán trạng thái của sâu bệnh
        stages_by_day = await gdd(predict, pest_stages, weather.get('Daily', {}).get('Temperature2mMax'))

        print (stages_by_day)
        
        if not weather:
            print ("Không có thông tin thời tiết: " + 
                   predict.get('ProvinceName') + " " +
                   predict.get('DistrictName') + " " + 
                   predict.get('WardName') + " " + 
                   predict.get('Address') + " " + 
                   predict_start_date.strftime('%Y-%m-%d') + " " + 
                   end_date)
            return
        
        # Lấy ra các trạng thái của cây trồng
        crop_stages = await predict_service.fetch_crop_stage(predict.get('CropId'))
        
        # Lấy ra các trạng thái cảnh báo
        level_warnings = await predict_service.fetch_crop_stage(predict.get('CropId'))
        
        # # lấy report để biết trạng thái cây trồng để đưa ra cảnh báo phù hợp
        # report = report_service.fetch_report(predict['ProvinceId'], predict['DistrictId'], Predict['WardId'], predict['StreetId'], predict['currentStartDate'], predict['currentEndDate'], predict['CropId'])

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

    # Parse ngày bắt đầu từ `predict`
    start_date = datetime.datetime.strptime(predict.get('CurrentStartDate'), '%Y-%m-%dT%H:%M:%S%z').date()
    
    # Sắp xếp các giai đoạn theo tổng tích ôn yêu cầu (K) để đảm bảo đúng thứ tự phát triển
    pest_stages_sorted = sorted(pest_stages, key=lambda x: x['K'])
    
    # Lấy các tên giai đoạn, nhiệt độ phát dục (T0), và tổng tích ôn yêu cầu (K) cho từng giai đoạn
    pest_stage_names = [stage['PestStageName'] for stage in pest_stages_sorted]
    base_temps = numpy.array([stage['T0'] for stage in pest_stages_sorted])
    degree_days_required = numpy.array([stage['K'] for stage in pest_stages_sorted])

    # Tính toán GDD hàng ngày
    cumulative_gdd = 0
    stages_by_day = []
    current_stage_index = 0
    
    # Duyệt qua từng ngày với dữ liệu nhiệt độ
    for i, temp in enumerate(weather):
        # Tính GDD cho ngày hiện tại dựa trên nhiệt độ ngày và nhiệt độ phát dục của giai đoạn hiện tại
        daily_gdd = max(temp - base_temps[current_stage_index], 0)
        cumulative_gdd += daily_gdd
        
        # Kiểm tra nếu GDD tích lũy đã đạt ngưỡng của giai đoạn hiện tại
        if cumulative_gdd >= degree_days_required[current_stage_index]:
            # Chuyển sang giai đoạn tiếp theo nếu chưa phải giai đoạn cuối
            if current_stage_index < len(pest_stages_sorted) - 1:
                current_stage_index += 1

        # Ghi lại ngày, giai đoạn và GDD tích lũy
        date = start_date + datetime.timedelta(days=i)
        stages_by_day.append({"date": date.strftime('%Y-%m-%d'), "stage": pest_stage_names[current_stage_index], "temp": temp, "gdd": cumulative_gdd})
    
    return stages_by_day

# Lập lịch chạy hàm `predict_auto_calculate` mỗi 1 phút
async def schedule_job():
    while True:
        await predict_auto_calculate()
        await asyncio.sleep(1 * 60)  # Đợi 1 phút trước khi chạy lại

# Chạy chương trình async
if __name__ == "__main__":
    asyncio.run(schedule_job())
