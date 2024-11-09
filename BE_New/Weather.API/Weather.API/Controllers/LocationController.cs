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
    public class LocationController : ControllerBase
    {
        ILocationService _locationService;
        
        public LocationController(ILocationService weatherService)
        {
            _locationService = weatherService;
        }

        [HttpGet()]
        public async Task<IActionResult> GeocodingAddress([FromQuery] string address)
        {
            var result = await _locationService.GeocodingAddress(address);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> GeocodingAddress([FromQuery] string? street, [FromQuery] string? ward, [FromQuery] string? district, [FromQuery] string? province)
        {
            var result = await _locationService.GeocodingAddress(street, ward, district, province);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
