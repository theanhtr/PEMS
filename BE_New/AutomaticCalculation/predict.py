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
            'ProvinceId': None,
            'DistrictId': None,
            'WardId': None,
            'StartDate': None,
            'EndDate': None,
            'CropStageId': None,
            'PestStageId': None,
            'CropId': None,
            'PestId': None,
            'SeasonEnd': False,
            'PageNumber': 1,
            'PageSize': 10000,
        }

        try:
            async with aiohttp.ClientSession() as session:
                async with session.post(url, json=data, ssl=False) as response:
                    response.raise_for_status()
                    return await response.json()
        except aiohttp.ClientError as e:
            print(f"Error fetching Predict data: {e}")
            return None
        
    

    # lấy những dự báo đang trong mùa vụ để dự đoán
    async def fetch_crop_stage(self, pest_id):
        url = f"{self.__api_url}/Predict/pest-stage"
        
        params = {
            'PestId': pest_id
        }

        try:
            async with aiohttp.ClientSession() as session:
                async with session.get(url, params=params, ssl=False) as response:
                    response.raise_for_status()
                    return await response.json()
        except aiohttp.ClientError as e:
            print(f"Error fetching Predict data: {e}")
            return None