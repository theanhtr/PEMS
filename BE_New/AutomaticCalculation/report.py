# Service tương tác dữ liệu báo cáo
import requests
from helper import get_api_url

class ReportService:
    __api_url_key = 'REPORT_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    def fetch_report_temperature_address(self, address, day = 7):
        url = f"{self.__api_url}/Report/temperature-address"
        
        params = {
            'address' : {address},
            'day' : {day}
        }

        try:
            response = requests.get(url, params=params, verify=False)
            response.raise_for_status()
            return response.json()
        except requests.RequestException as e:
            print(f"Error fetching Report data: {e}")
            return None