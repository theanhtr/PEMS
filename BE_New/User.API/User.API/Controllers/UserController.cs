using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.BL;
using User.Model;
using Base.Model;
using Base.Controller;

namespace User.API
{
    /// <summary>
    /// Controller của phòng ban
    /// </summary>
    /// CreatedBy: TTANH (12/07/2024)
    [Route("api/v1/[controller]")]
    [ApiController]
    [RoleAuthorize(3)]
    public class UserController : BaseController<Model.User>
    {
        IUserService _userInfoService;
        
        public UserController(IUserService userInfoService) : base(userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpGet("myinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var result = await _userInfoService.GetUserInfo(Guid.Parse(userId));

            return StatusCode(StatusCodes.Status200OK, result);
        }

        protected override void ValidateBeforeActionBase(BaseAction action)
        {
            BlockAllAction(1);
        }
    }
}
