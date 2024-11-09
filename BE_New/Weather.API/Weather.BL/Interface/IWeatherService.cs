using Weather.Model;

namespace Weather.BL
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IWeatherService
    {
        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        Task<WeatherResponse?> TemperatureMax(int day, string? street, string? ward, string? district, string? province);

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        Task<WeatherResponse?> TemperatureMax(DateOnly startDate, DateOnly endDate, string? street, string? ward, string? district, string? province);

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        Task<WeatherResponse?> TemperatureMax(int day, string? address);

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        Task<WeatherResponse?> TemperatureMax(DateOnly startDate, DateOnly endDate, string? address);
    }
}
