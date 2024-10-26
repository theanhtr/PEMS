using Microsoft.AspNetCore.Mvc;
using Weather.BL;

namespace Weather.API
{
    /// <summary>
    /// Controller của phòng ban
    /// </summary>
    /// CreatedBy: TTANH (12/07/2024)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        IWeatherService _weatherService;
        
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetWeatherInfo(string city)
        {
            var result = await _weatherService.GetWeatherAsync(city);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
