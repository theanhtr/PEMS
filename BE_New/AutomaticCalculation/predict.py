# Service tương tác dữ liệu dự báo
import requests
from helper import get_api_url

class PredictService:
    __api_url_key = 'PREDICT_API_KEY'
    __api_url = ''

    def __init__(self):
        self.__api_url = get_api_url(self.__api_url_key)

    def fetch_predict_in_season(self):
        url = f"{self.__api_url}/Predict/filter"
        
        data = {
            'CropStateId': -1,
            'DistrictId': "",
            'EndDate': None,
            'PageNumber': 1,
            'PageSize': 10,
            'PestLevelId': -1,
            'ProvinceId': "",
            'SeasonEnd': False,
            'StartDate': None,
            'WardId': ""
        }

        try:
            response = requests.post(url, data=data, verify=False)
            response.raise_for_status()
            return response.json()
        except requests.RequestException as e:
            print(f"Error fetching Predict data: {e}")
            return None