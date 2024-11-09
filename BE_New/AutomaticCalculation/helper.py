# Code lấy dữ liệu thời tiết
import os
from dotenv import load_dotenv

def get_api_url(key):
  # Xác định môi trường
  environment = os.getenv('ENV', 'development')

  # Tải tệp .env từ thư mục config
  load_dotenv(f'config/.env.{environment}')

  # API thời tiết
  return os.getenv(key)