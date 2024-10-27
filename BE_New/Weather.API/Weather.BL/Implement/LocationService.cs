using Weather.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Base.Model;
using Newtonsoft.Json.Linq;


namespace Weather.BL
{
    public class LocationService : ILocationService
    {
        private IConfiguration? _config;
        private string? geocodingApi = "";
        private string? geocodingApiKey = "";

        #region Constructor
        public LocationService(IConfiguration config)
        {
            _config = config;
            geocodingApi = _config?.GetSection("ThirdPartyApi:GeocodingApi")?.Value;
            geocodingApiKey = _config?.GetSection("ThirdPartyApiKey:GeocodingApiKey")?.Value;

            if (string.IsNullOrEmpty(geocodingApi) || string.IsNullOrEmpty(geocodingApiKey))
            {
                throw new Exception("GeocodingApi or GeocodingApiKey is not found in appsettings.json");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy thông tin mã địa lý từ chuỗi địa chỉ
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<GeocodeAddress?> GeocodingAddress(string? address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                var url = $"{geocodingApi}/search?q={address},Vietnam&api_key={geocodingApiKey}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(); 
                    
                    var data = JsonConvert.DeserializeObject<List<GeocodeAddressResponse>>(result);

                    if (data.Count == 0)
                    {
                        return null;
                    }

                    return new GeocodeAddress
                    {
                        Latitude = data.FirstOrDefault().Lat,
                        Longitude = data.FirstOrDefault().Lon
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// Hàm lấy thông tin mã địa lý từ chi tiết địa chỉ
        /// </summary>
        /// <param name="ward">Xã</param>
        /// <param name="district">Huyện</param>
        /// <param name="province">Tỉnh</param>
        /// <returns></returns>
        public async Task<GeocodeAddress?> GeocodingAddress(string? street, string? ward, string? district, string? province) 
        {
            GeocodeAddress? geocodeAddress = null;

            using (var client = new HttpClient())
            {
                // nếu có tên đường thì gọi bằng tên đường, nếu không sẽ lấy tên phường
                var streetSend = String.IsNullOrEmpty(street) ? ward : street;

                var url = $"{geocodingApi}/search?street={streetSend}&county={district}&city={province}&country=Vietnam&api_key={geocodingApiKey}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var data = JsonConvert.DeserializeObject<List<GeocodeAddressResponse>>(result);

                    if (data.Count != 0)
                    {
                        geocodeAddress = new GeocodeAddress
                        {
                            Latitude = data.FirstOrDefault().Lat,
                            Longitude = data.FirstOrDefault().Lon
                        };
                    }
                }

                // nếu không tìm thấy bằng tên đường thì sẽ chuyển sử dụng tên phường
                if (geocodeAddress == null && !String.IsNullOrEmpty(street))
                {
                    url = $"{geocodingApi}/search?street={ward}&county={district}&city={province}&country=Vietnam&api_key={geocodingApiKey}";

                    response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        var data = JsonConvert.DeserializeObject<List<GeocodeAddressResponse>>(result);

                        if (data.Count != 0)
                        {
                            geocodeAddress = new GeocodeAddress
                            {
                                Latitude = data.FirstOrDefault().Lat,
                                Longitude = data.FirstOrDefault().Lon
                            };
                        }
                    }
                }

                // nếu mà không tìm thấy cả bằng tên phường thì sẽ tìm bằng tên quận
                if (geocodeAddress == null)
                {
                    geocodeAddress = await GeocodingAddress(ward + "," + district + "," + province);
                }
                
                // nếu mà không tìm thấy cả bằng tên quận thì sẽ tìm bằng tên quận
                if (geocodeAddress == null)
                {
                    geocodeAddress = await GeocodingAddress(district + "," + province);
                }
            }

            return geocodeAddress;
        }
        #endregion
    }
}
