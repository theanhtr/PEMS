from flask import Flask, jsonify, request
from weather import WeatherService
from predict import PredictService
from report import ReportService
import asyncio
import datetime
import numpy
from helper import get_time_worker
import threading

app = Flask(__name__)
weather_service = WeatherService()
predict_service = PredictService()
report_service = ReportService()

# Endpoint để kích hoạt tính toán tự động
@app.route('/auto-calculate', methods=['POST'])
def run_predict():
    predict = request.get_json()
    print("Trigger tính toán: ", predict.get('PredictId'))
    asyncio.run(predict_calculate_one(predict))
    return jsonify({"message": "Đã chạy tính toán tự động."})

# Hàm chạy tự động tính toán dự đoán
async def predict_auto_calculate():
    print("Chạy tự động tính toán lúc: " + datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"))

    # Lấy các dự án đang trong mùa vụ để tính toán
    predict_data = await predict_service.fetch_predict_in_season()

    if not len(predict_data.get('Data', [])):
        print ("Không có dự đoán nào đang trong mùa vụ: " + datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
        return

    for predict in predict_data.get('Data', []):
        await predict_calculate_one(predict)

async def predict_calculate_one(predict):
    # Lấy giá trị currentStartDate từ predict

    #nếu currentStartDate chưa có múi giờ thì thêm vào
    predict['CurrentStartDate'] = predict.get('CurrentStartDate')
    if predict['CurrentStartDate'].find('+') == -1:
        predict['CurrentStartDate'] = predict['CurrentStartDate'] + '+07:00'
    
    #nếu currentEndDate chưa có múi giờ thì thêm vào
    predict['CurrentEndDate'] = predict.get('CurrentEndDate')
    if predict['CurrentEndDate'].find('+') == -1:
        predict['CurrentEndDate'] = predict['CurrentEndDate'] + '+07:00'

    predict_start_date = datetime.datetime.strptime(predict['CurrentStartDate'], '%Y-%m-%dT%H:%M:%S%z').date()
    end_date = datetime.datetime.strptime(predict['CurrentEndDate'], '%Y-%m-%dT%H:%M:%S%z').date()

    # Lấy ra các trạng thái của sâu bệnh
    pest_stages = await predict_service.fetch_pest_stage(predict.get('PestId'))

    # lấy thông tin thời tiết của địa điểm dự báo
    weather = await weather_service.fetch_weather_temperature_range(predict.get('ProvinceName'), predict.get('DistrictName'), predict.get('WardName'), predict.get('Address'), predict_start_date.strftime('%Y-%m-%d'), end_date.strftime('%Y-%m-%d')   )
    if not weather:
        print ("Không có thông tin thời tiết: ",
                predict.get('ProvinceName')," " +
                predict.get('DistrictName')," ",
                predict.get('WardName')," ",
                predict.get('Address')," ",
                predict_start_date.strftime('%Y-%m-%d')," ",
                end_date)
        return None
    
    # lấy thông tin báo cáo của địa điểm dự báo
    latest_report = await report_service.fetch_latest_report(predict['ProvinceId'], predict['DistrictId'], predict['WardId'], predict['PestId'])

    # nếu không có report thì không thể cập nhật
    if not latest_report:
        print ("Không có báo cáo tại: ",
                    predict.get('ProvinceName')," " +
                    predict.get('DistrictName')," ",
                    predict.get('WardName')," ",
                    predict.get('Address')," ",
                    predict.get('CurrentStartDate'))
        return None

    latest_report_date = datetime.datetime.strptime(latest_report.get('ModifiedDate'), '%Y-%m-%dT%H:%M:%S%z').date()
    if latest_report_date < (datetime.datetime.now() - datetime.timedelta(days=60)).date():
        print ("Báo cáo quá cũ (lớn hơn 60 ngày) tại: ",
                    predict.get('ProvinceName')," " +
                    predict.get('DistrictName')," ",
                    predict.get('WardName')," ",
                    predict.get('Address')," ",
                    predict.get('CurrentStartDate'))
        return None

    # tính toán trạng thái của sâu bệnh
    stages_by_day = await gdd(predict, pest_stages, weather, latest_report)    
    # Lấy ra các trạng thái của cây trồng
    crop_stages = await predict_service.fetch_crop_stage(predict.get('CropId'))
    # Lấy ra các trạng thái cảnh báo
    level_warnings = await predict_service.fetch_level_warning(predict.get('CropId'), predict.get('PestId'))

    stages_by_day = update_daily_forecast_predict(stages_by_day, level_warnings, crop_stages, predict)
    print('stages_by_day', stages_by_day)
    await predict_service.update_daily_forecast(predict.get('PredictId'), stages_by_day)

async def gdd(predict, pest_stages, weather, latest_report):
    # Tính  án GDD
    print ("dự báo bắt đầu ngày: ")
    print (datetime.datetime.strptime(latest_report.get('ModifiedDate'), '%Y-%m-%dT%H:%M:%S%z').date())
    print ("tại: ")
    print (predict.get('WardName') + " , " +
                   predict.get('DistrictName') + " , " + 
                   predict.get('ProvinceName'))

    #trích xuất các thông tin thời tiết từ weather
    times = weather.get('Daily', {}).get('Time') #thời gian tương ứng
    temps = weather.get('Daily', {}).get('Temperature2mMean') #nhiệt độ trung bình trong ngày
    rains = weather.get('Daily', {}).get('RainSum') #lượng mưa trong ngày
    humidities = weather.get('Daily', {}).get('RelativeHumidity2mMean') #độ ẩm trung bình trong ngày

    # Parse ngày bắt đầu từ `predict`
    start_date = datetime.datetime.strptime(latest_report.get('ModifiedDate'), '%Y-%m-%dT%H:%M:%S%z').date()
    
    # Sắp xếp các giai đoạn theo tổng tích ôn yêu cầu (K) để đảm bảo đúng thứ tự phát triển
    pest_stages_sorted = sorted(pest_stages, key=lambda x: x['K'])
    
    # Lấy các tên giai đoạn, nhiệt độ phát dục (T0), và tổng tích ôn yêu cầu (K) cho từng giai đoạn
    pest_stage_names = [stage['PestStageName'] for stage in pest_stages_sorted]
    pest_stage_ids = [stage['PestStageId'] for stage in pest_stages_sorted]
    base_temps = numpy.array([stage['T0'] for stage in pest_stages_sorted])
    degree_days_required = numpy.array([stage['K'] for stage in pest_stages_sorted])

    # Tính toán GDD hàng ngày
    stages_by_day = []
    cumulative_gdd = 0
    current_stage_index = 0

    for index, pest_stage_id in enumerate(pest_stage_ids):
        if pest_stage_id == latest_report.get('PestStageId'):
            current_stage_index = index
            break

    if current_stage_index > 0:
        cumulative_gdd = degree_days_required[current_stage_index - 1]

    # tính vị trí i của nhiệt độ với ngày bắt đầu là ngày cuối cùng của báo cáo trước đó
    start_index_weather = 0
    for i, time in enumerate(times):
        if time == start_date.strftime('%Y-%m-%d'):
            start_index_weather = i
            break
    print('start_index_weather', start_index_weather)
    
    # Duyệt qua từng ngày với dữ liệu nhiệt độ
    for i in range(start_index_weather, len(temps)):
        # Tính GDD cho ngày hiện tại dựa trên nhiệt độ ngày và nhiệt độ phát dục của giai đoạn hiện tại
        daily_gdd = max(temps[i] - base_temps[current_stage_index], 0)
        cumulative_gdd += daily_gdd
        
        # Lấy ngày hiện tại
        date = start_date + datetime.timedelta(days=(i-start_index_weather))
        
        # Ghi lại giai đoạn hiện tại trước khi kiểm tra điều kiện đổi giai đoạn
        stages_by_day.append({"stage": pest_stage_names[current_stage_index], "stage_id": pest_stage_ids[current_stage_index], "temp": temps[i], "rain": rains[i], "humidity": humidities[i], "date": date.strftime('%Y-%m-%d')})
        
        # Kiểm tra nếu GDD tích lũy đã đạt ngưỡng của giai đoạn hiện tại
        if cumulative_gdd >= degree_days_required[current_stage_index]:
            # Chuyển sang giai đoạn tiếp theo vào ngày tiếp theo
            current_stage_index += 1
            
            # Nếu đã vượt qua giai đoạn cuối thì reset về giai đoạn đầu tiên và GDD tích lũy về 0
            if current_stage_index >= len(pest_stages_sorted):
                current_stage_index = 0
                cumulative_gdd = 0
    
    return stages_by_day

def update_daily_forecast_predict(stages_by_day, level_warnings, crop_stages, predict):
    #  nếu có thì lấy id sâu hôm đó + id của trạng thái cây trong cảnh báo để tìm mức độ cảnh báo trong level_warnings
    
    predict_start_date = datetime.datetime.strptime(predict.get('CurrentStartDate'), '%Y-%m-%dT%H:%M:%S%z').date()

    for stage in stages_by_day:
        stage_date = datetime.datetime.strptime(stage.get('date'), '%Y-%m-%d').date()
        crop_age = calculate_crop_age(crop_stages, predict_start_date, stage_date)
        stage['crop_stage_name'] = crop_age.get('CropStageName')

        for level in level_warnings:
            if level.get('PestStageId') == stage.get('stage_id') and level.get('CropStageId') == crop_age.get('CropStageId'):
                stage['level_warning'] = level.get('LevelWarningName')
                break

    return stages_by_day

def calculate_crop_age(crop_stages, start_date, stage_date):
    # tính tuổi của cây bằng trường DevelopmentTime trong crop_stages với ngày bắt đầu của predict
    
    # Tính số ngày đã trôi qua
    days_passed = (stage_date - start_date).days

    total_days =  0
    for crop_stage in crop_stages:
        development_time = crop_stage.get('DevelopmentTime')
        total_days += development_time

        if days_passed < total_days:
            return crop_stage

# Lập lịch chạy hàm `predict_auto_calculate` mỗi 1 phút
async def schedule_job():
    time_worker = get_time_worker()
    print ("Chạy tự động tính toán sau mỗi: " + str(time_worker) + " phút. " + str(time_worker * 60)),
           
    while True:
        await predict_auto_calculate()
        await asyncio.sleep(time_worker * 60)  # Đợi 1 phút trước khi chạy lại

# Khởi chạy Flask trong một thread riêng
def start_flask():
    app.run(port=5000)

# Chạy chương trình async
if __name__ == "__main__":
    # Khởi động Flask trong thread khác
    # flask_thread = threading.Thread(target=start_flask)
    # flask_thread.start()
    app.run(port=5000)

    # Chạy cronjob trong main thread
    # asyncio.run(schedule_job())
