# Service lấy thông tin dự báo thời tiết
import aiohttp
from helper import get_api_url

class WeatherService:
    __api_url_key = 'WEATHER_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    async def fetch_weather_temperature_address(self, address, day=7):
        url = f"{self.__api_url}/Weather/temperature-address"
        
        params = {
            'address': address,
            'day': day
        }

        try:
            async with aiohttp.ClientSession() as session:
                async with session.get(url, params=params, ssl=False) as response:
                    response.raise_for_status()  # Kiểm tra trạng thái HTTP
                    return await response.json()  # Trả về JSON bất đồng bộ
        except aiohttp.ClientError as e:
            print(f"Error fetching weather data: {e}")
            return None
