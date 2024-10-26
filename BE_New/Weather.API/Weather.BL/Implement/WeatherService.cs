namespace Weather.BL
{
    public class WeatherService : IWeatherService
    {
        
        #region Constructor
        #endregion

        #region Methods
        public async Task<string> GetWeatherAsync(string city)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://wttr.in/{city}?format=%C+%t");
            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
