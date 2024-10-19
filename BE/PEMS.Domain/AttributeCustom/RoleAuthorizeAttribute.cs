using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace PEMS.Domain
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public int Role { get; }

        public RoleAuthorizeAttribute(int role)
        {
            Role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var headers = context.HttpContext.Request.Headers;

            // Lấy thông tin từ JWT
            var dataToken = context.HttpContext.User;

            if (dataToken.Identity.IsAuthenticated)
            {
                // Lấy các claim từ JWT
                var userId = dataToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                var userRepository = context.HttpContext.RequestServices.GetService<IUserRepository>();
                var user = userRepository.GetByUserId(Guid.Parse(userId));

                if (user != null)
                {
                    var roleId = user.RoleID;
                    if (roleId > Role)
                    {
                        // Trả về 403 Forbidden nếu người dùng không có quyền truy cập
                        context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
                    }
                }
                else
                {
                    // Trả về 403 Forbidden nếu người dùng không có quyền truy cập
                    context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
                }
            }
            else
            {
                // Trả về 401 Unauthorized nếu người dùng không được xác thực
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            }
        }
    }
}
