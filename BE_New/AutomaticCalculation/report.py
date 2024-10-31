# Service tương tác dữ liệu báo cáo
import requests
from helper import get_api_url

class ReportService:
    __api_url_key = 'REPORT_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    # Trả về dữ liệu báo cáo của 1 dự báo
    def fetch_report(self, provinceId, districtId, wardId, startDate, endDate, cropId):
        url = f"{self.__api_url}/Report/filter"
        
        params = {
            'ProvinceId': provinceId,
            'DistrictId': districtId,
            'WardId': wardId,
            'PageNumber': 1,
            'PageSize': 10000,
            'ReportStartDate': startDate,
            'ReportEndDate': endDate,
            'CropId': cropId
        }

        try:
            response = requests.post(url, params=params, verify=False)
            response.raise_for_status()
            return response.json()
        except requests.RequestException as e:
            print(f"Error fetching Report data: {e}")
            return None