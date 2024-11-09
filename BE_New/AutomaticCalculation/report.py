# Service tương tác dữ liệu báo cáo
import aiohttp
from helper import get_api_url

class ReportService:
    __api_url_key = 'REPORT_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    # Trả về dữ liệu báo cáo của 1 dự báo
    async def fetch_report(self, provinceId, districtId, wardId, startDate, endDate, cropId):
        url = f"{self.__api_url}/Report/filter"
        
        datas = {
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
            async with aiohttp.ClientSession() as session:
                async with session.post(url, json=datas, ssl=False) as response:
                    response.raise_for_status()
                    return await response.json()
        except aiohttp.ClientError as e:
            print(f"Error fetching Predict data: {e}")
            return None