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
        IUserService _userService;
        
        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var result = await _userService.GetUserInfo(Guid.Parse(userId));

            return StatusCode(StatusCodes.Status200OK, result);
        }

        protected async virtual Task BlockAllAction(int role)
        {
            // Lấy các claim từ JWT
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var user = await _userService.GetUserInfo(Guid.Parse(userId));

            if (user != null)
            {
                var roleId = user.RoleID;
                if (roleId > role)
                {
                    throw new ForbiddenException(StatusErrorCode.Forbidden, "Bạn không có quyền truy cập trang này.", null);
                }
            }
            else
            {
                throw new ForbiddenException(StatusErrorCode.Forbidden, "Bạn không có quyền truy cập trang này.", null);
            }
        }

        protected override async Task ValidateBeforeActionBase(BaseAction action)
        {
            await BlockAllAction(1);
        }
    }
}
