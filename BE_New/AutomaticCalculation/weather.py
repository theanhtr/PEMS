# Service lấy thông tin dự báo thời tiết
import requests
from helper import get_api_url

class WeatherService:
    __api_url_key = 'WEATHER_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    def fetch_weather_temperature_address(self, address, day = 7):
        url = f"{self.__api_url}/Weather/temperature-address"
        
        params = {
            'address' : {address},
            'day' : {day}
        }

        try:
            response = requests.get(url, params=params, verify=False)
            response.raise_for_status()
            return response.json()
        except requests.RequestException as e:
            print(f"Error fetching weather data: {e}")
            return None