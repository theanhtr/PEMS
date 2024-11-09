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

        /// <summary>
        /// Dự đoán nhiệt độ tối đa của 1 địa chỉ trong số ngày tương lai
        /// </summary>
        /// <param name="day"></param>
        /// <param name="street"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        /// <returns></returns>
        [HttpGet("temperature")]
        public async Task<IActionResult> TemperatureMax([FromQuery] string? street, [FromQuery] string? ward, [FromQuery] string? district, [FromQuery] string? province, [FromQuery] int day = 7)
        {
            // số ngày tối đa là 16
            if (day < 1 && day > 16)
            {
                day = 16;
            }

            var result = await _weatherService.TemperatureMax(day, street, ward, district, province);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy nhiệt độ tối đa của 1 địa chỉ trong 1 khoảng thời gian
        /// </summary>
        /// <returns></returns>
        [HttpGet("temperature-range")]
        public async Task<IActionResult> TemperatureMax([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, [FromQuery] string? street, [FromQuery] string? ward, [FromQuery] string? district, [FromQuery] string? province)
        {
            var result = await _weatherService.TemperatureMax(startDate, endDate, street, ward, district, province);

            return StatusCode(StatusCodes.Status200OK, result);
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
        [HttpGet("temperature-address")]
        public async Task<IActionResult> TemperatureMax([FromQuery] string? address, [FromQuery] int day = 7)
        {
            // số ngày tối đa là 16
            if (day < 1 && day > 16)
            {
                day = 16;
            }

            var result = await _weatherService.TemperatureMax(day, address);

            return StatusCode(StatusCodes.Status200OK, result);
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
        [HttpGet("temperature-address-range")]
        public async Task<IActionResult> TemperatureMax([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, [FromQuery] string? address)
        {
            var result = await _weatherService.TemperatureMax(startDate, endDate, address);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
