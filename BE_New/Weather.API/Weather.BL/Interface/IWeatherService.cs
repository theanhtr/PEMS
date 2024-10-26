namespace Weather.BL
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string city);
    }
}
