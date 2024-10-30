import asyncio
from weather import WeatherService
from predict import PredictService

async def calculate_predict():
    weather_service = WeatherService()
    predict_service = PredictService()

    # Chạy cả hai hàm async đồng thời
    weather_data, predict_data = await asyncio.gather(
        weather_service.fetch_weather_temperature_address(address='Quận Cầu Giấy, Thành Phố Hà Nội'),
        predict_service.fetch_predict_in_season()
    )

    # Xử lý dữ liệu sau khi nhận được
    print("weather data:", weather_data.get('Daily', {}).get('Temperature2mMax'))
    print("predict_data:", predict_data.get('Data'))

    # Kiểm tra dữ liệu
    if not weather_data or not predict_data:
        print("Failed to fetch data for calculation.")
        return

# Lập lịch chạy hàm `calculate_predict` mỗi 1 phút
async def schedule_job():
    while True:
        await calculate_predict()
        await asyncio.sleep(1 * 60)  # Đợi 1 phút trước khi chạy lại

# Chạy chương trình async
if __name__ == "__main__":
    asyncio.run(schedule_job())
