using Weather.Model;

namespace Weather.BL
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface ILocationService
    {
        /// <summary>
        /// Hàm lấy thông tin mã địa lý từ chuỗi địa chỉ
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<GeocodeAddress> GeocodingAddress(string address);

        /// <summary>
        /// Hàm lấy thông tin mã địa lý từ chi tiết địa chỉ
        /// </summary>
        /// <param name="ward">Xã</param>
        /// <param name="district">Huyện</param>
        /// <param name="province">Tỉnh</param>
        /// <returns></returns>
        Task<GeocodeAddress> GeocodingAddress(string? street, string? ward, string? district, string? province);
    }
}
