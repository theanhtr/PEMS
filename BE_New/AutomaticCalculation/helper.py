# lấy config theo môi trường
import os
from dotenv import load_dotenv

def get_api_url(key):
  # Xác định môi trường
  environment = os.getenv('ENV', 'development')

  # Tải tệp .env từ thư mục config
  load_dotenv(f'config/.env.{environment}')

  # API thời tiết
  return os.getenv(key)

# lấy thời gian lặp lại của worker
def get_time_worker():
  # Xác định môi trường
  return os.getenv('TIME_WORKER', 1) # mặc định 1 phút