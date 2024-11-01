# Service lấy thông tin dự báo thời tiết
import aiohttp
from helper import get_api_url

class WeatherService:
    __api_url_key = 'WEATHER_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    # Trả về thời tiết theo địa chỉ
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

    # Trả về thời tiết theo quận, huyện, xã
    async def fetch_weather_temperature_address_range(self, address, startDate, endDate):
        url = f"{self.__api_url}/Weather/temperature-address-range"
        
        params = {
            'address': address,
            'startDate': startDate,
            'endDate': endDate
        }

        try:
            async with aiohttp.ClientSession() as session:
                async with session.get(url, params=params, ssl=False) as response:
                    response.raise_for_status()  # Kiểm tra trạng thái HTTP
                    return await response.json()  # Trả về JSON bất đồng bộ
        except aiohttp.ClientError as e:
            print(f"Error fetching weather data: {e}")
            return None

    # Trả về thời tiết theo quận, huyện, xã
    async def fetch_weather_temperature(self, province, district, ward, street, day=7):
        url = f"{self.__api_url}/Weather/temperature"
        
        params = {
            'province': province,
            'district': district,
            'ward': ward,
            'street': street,
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

    # Trả về thời tiết theo quận, huyện, xã
    async def fetch_weather_temperature_range(self, province, district, ward, street, startDate, endDate):
        url = f"{self.__api_url}/Weather/temperature-range"
        
        params = {
            'province': province,
            'district': district,
            'ward': ward,
            'street': street,
            'startDate': startDate,
            'endDate': endDate
        }

        # bỏ giá trị None do aiohttp không chấp nhận
        params = {key: (value if value is not None else '') for key, value in params.items()}

        try:
            async with aiohttp.ClientSession(timeout=aiohttp.ClientTimeout(total=60)) as session:
                async with session.get(url, params=params, ssl=False) as response:
                    response.raise_for_status()  # Kiểm tra trạng thái HTTP
                    return await response.json()  # Trả về JSON bất đồng bộ
        except aiohttp.ClientError as e:
            print(f"Error fetching weather data: {e}")
            return None