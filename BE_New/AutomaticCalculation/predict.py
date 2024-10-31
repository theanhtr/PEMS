# Service tương tác dữ liệu dự báo
import aiohttp
from helper import get_api_url

class PredictService:
    __api_url_key = 'PREDICT_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    # lấy những dự báo đang trong mùa vụ để dự đoán
    async def fetch_predict_in_season(self):
        url = f"{self.__api_url}/Predict/filter"
        
        data = {
            'CropStageId': '',
            'DistrictId': '',
            'EndDate': None,
            'PageNumber': 1,
            'PageSize': 10000,
            'PestStageId': '',
            'ProvinceId': '',
            'SeasonEnd': False,
            'StartDate': None,
            'WardId': '',
            'PestId': '',
            'CropId': ''
        }

        try:
            async with aiohttp.ClientSession() as session:
                async with session.post(url, json=data, ssl=False) as response:
                    response.raise_for_status()
                    return await response.json()
        except aiohttp.ClientError as e:
            print(f"Error fetching Predict data: {e}")
            return None