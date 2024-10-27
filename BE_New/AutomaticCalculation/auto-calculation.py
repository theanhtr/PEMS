import schedule
import time
from weather import WeatherService

def calculate_predict():
    weather_service = WeatherService()

    # Lấy dữ liệu từ API
    weather_data = weather_service.fetch_weather_temperature_address(address='Quận Cầu Giấy, Thành Phố Hà Nội')  # Lấy dữ liệu thời tiết

    # Kiểm tra dữ liệu
    if not weather_data:
        print("Failed to fetch data for calculation.")
        return

    print("weather data:", weather_data['Daily']['Temperature2mMax'])

# Lập lịch chạy hàm `calculate_predict` mỗi 10 phút
schedule.every(5).seconds.do(calculate_predict)

# Chạy vòng lặp để thực thi theo lịch
while True:
    schedule.run_pending()
    time.sleep(1)
