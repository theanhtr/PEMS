import schedule
import time
from weather import WeatherService
from predict import PredictService

def calculate_predict():
    weather_service = WeatherService()
    predict_service = PredictService()

    # Lấy dữ liệu từ API
    weather_data = weather_service.fetch_weather_temperature_address(address='Quận Cầu Giấy, Thành Phố Hà Nội')  # Lấy dữ liệu thời tiết
    predict_data = predict_service.fetch_predict_in_season()  # lấy dữ liệu dự báo trong mùa vụ

    # Kiểm tra dữ liệu
    if not weather_data or not predict_data:
        print("Failed to fetch data for calculation.")
        return

    print("weather data:", weather_data['Daily']['Temperature2mMax'])
    print("predict_data:", predict_data['Data'])

# Lập lịch chạy hàm `calculate_predict` mỗi 10 phút
schedule.every(5).seconds.do(calculate_predict)

# Chạy vòng lặp để thực thi theo lịch
while True:
    schedule.run_pending()
    time.sleep(1)
