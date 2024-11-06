using Microsoft.Extensions.Configuration;
using Weather.Model;
using Base.Model;
using Newtonsoft.Json;

namespace Weather.BL
{
    public class WeatherService : IWeatherService
    {
        private IConfiguration? _config;
        private string? weatherApi = "";
        private string? weatherHistoryApi = "";
        private string? timezone = "";
        private ILocationService? _locationService;
        private string formatDateWeatherApi;

        #region Constructor
        public WeatherService(IConfiguration config, ILocationService? locationService)
        {
            _config = config;
            weatherApi = _config?.GetSection("ThirdPartyApi:WeatherApi")?.Value;
            weatherHistoryApi = _config?.GetSection("ThirdPartyApi:WeatherHistoryApi")?.Value;
            timezone = _config?.GetSection("AppSettings:Timezone")?.Value;
            _locationService = locationService;
            formatDateWeatherApi = "yyyy-MM-dd";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy thông tin tọa độ từ địa chỉ
        /// </summary>
        /// <returns></returns>
        public async Task<GeocodeAddress?> GetGeocodeAddressAsync(string? street, string? ward, string? district, string? province)
        {
            var geocodeAddress = await _locationService?.GeocodingAddress(street, ward, district, province);

            // nếu bị null thì tìm lại 1 lần
            if (geocodeAddress == null)
            {
                return await _locationService?.GeocodingAddress(street, ward, district, province);
            }

            return geocodeAddress;
        }

        /// <summary>
        /// Lấy thông tin tọa độ từ địa chỉ
        /// </summary>
        /// <returns></returns>
        public async Task<GeocodeAddress?> GetGeocodeAddressAsync(string? address)
        {
            var geocodeAddress = await _locationService?.GeocodingAddress(address);

            // nếu bị null thì tìm lại 1 lần
            if (geocodeAddress == null)
            {
                return await _locationService?.GeocodingAddress(address);
            }

            return geocodeAddress;
        }

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        public async Task<WeatherResponse?> TemperatureMax(int day, string? street, string? ward, string? district, string? province)
        {
            var geocodeAddress = await GetGeocodeAddressAsync(street, ward, district, province);

            if (geocodeAddress == null)
            {
                throw new NotFoundException(StatusErrorCode.NotFoundData, "Không tìm thấy địa chỉ");
            }

            var urlWeather = $"{weatherApi}/forecast?latitude={geocodeAddress.Latitude}&longitude={geocodeAddress.Longitude}&daily=temperature_2m_max,temperature_2m_mean&timezone={timezone}&forecast_days={day}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlWeather);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<WeatherResponse>(result);
                }
            }

            return null;
        }

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        public async Task<WeatherResponse?> TemperatureMax(DateOnly startDate, DateOnly endDate, string? street, string? ward, string? district, string? province) 
        {
            var geocodeAddress = await GetGeocodeAddressAsync(street, ward, district, province);

            if (geocodeAddress == null)
            {
                throw new NotFoundException(StatusErrorCode.NotFoundData, "Không tìm thấy địa chỉ");
            }

            var urlWeather = "";

            if (startDate < DateOnly.FromDateTime(DateTime.Now.AddMonths(-3)))
            {
                urlWeather = $"{weatherHistoryApi}/archive?latitude={geocodeAddress.Latitude}&longitude={geocodeAddress.Longitude}&daily=temperature_2m_max,temperature_2m_mean&timezone={timezone}&start_date={startDate.ToString(formatDateWeatherApi)}&end_date={endDate.ToString(formatDateWeatherApi)}";
            }
            else
            {
                // ngày kết thúc dự đoán trong tương lai thì không được quá 14 ngày từ hôm nay
                if (endDate > DateOnly.FromDateTime(DateTime.Now.AddDays(14)))
                {
                    endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14));
                }

                urlWeather = $"{weatherApi}/forecast?latitude={geocodeAddress.Latitude}&longitude={geocodeAddress.Longitude}&daily=temperature_2m_max,temperature_2m_mean&timezone={timezone}&start_date={startDate.ToString(formatDateWeatherApi)}&end_date={endDate.ToString(formatDateWeatherApi)}";
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlWeather);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<WeatherResponse>(result);
                }
            }

            return null;
        }

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        public async Task<WeatherResponse?> TemperatureMax(int day, string? address)
        {
            var geocodeAddress = await GetGeocodeAddressAsync(address);

            if (geocodeAddress == null)
            {
                throw new NotFoundException(StatusErrorCode.NotFoundData, "Không tìm thấy địa chỉ");
            }

            var urlWeather = $"{weatherApi}/forecast?latitude={geocodeAddress.Latitude}&longitude={geocodeAddress.Longitude}&daily=temperature_2m_max,temperature_2m_mean&timezone={timezone}&forecast_days={day}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlWeather);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<WeatherResponse>(result);
                }
            }

            return null;
        }

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        public async Task<WeatherResponse?> TemperatureMax(DateOnly startDate, DateOnly endDate, string? address)
        {
            var geocodeAddress = await GetGeocodeAddressAsync(address);

            if (geocodeAddress == null)
            {
                throw new NotFoundException(StatusErrorCode.NotFoundData, "Không tìm thấy địa chỉ");
            }

            var urlWeather = "";

            if (startDate < DateOnly.FromDateTime(DateTime.Now.AddMonths(-3)))
            {
                urlWeather = $"{weatherHistoryApi}/archive?latitude={geocodeAddress.Latitude}&longitude={geocodeAddress.Longitude}&daily=temperature_2m_max,temperature_2m_mean&timezone={timezone}&start_date={startDate.ToString(formatDateWeatherApi)}&end_date={endDate.ToString(formatDateWeatherApi)}";
            } 
            else
            {
                // ngày kết thúc dự đoán trong tương lai thì không được quá 14 ngày từ hôm nay
                if (endDate > DateOnly.FromDateTime(DateTime.Now.AddDays(14)))
                {
                    endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14));
                }

                urlWeather = $"{weatherApi}/forecast?latitude={geocodeAddress.Latitude}&longitude={geocodeAddress.Longitude}&daily=temperature_2m_max,temperature_2m_mean&timezone={timezone}&start_date={startDate.ToString(formatDateWeatherApi)}&end_date={endDate.ToString(formatDateWeatherApi)}";
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlWeather);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<WeatherResponse>(result);
                }
            }

            return null;
        }
        #endregion
    }
}
