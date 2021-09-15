using IdentityServer4.Test.Api.Models;
using IdentityServer4.Test.Api.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityServer4.Test.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private UserManager<User> _userManager;
        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
        {
            var res = new ApiResult();
            var result = await _userManager.FindByNameAsync(registerRequest.UserName);
            if(result != null)
            {
                res.ResultCode = ResultCode.BadRequest;
                res.Data = null;
                res.DevMessage = "Tên tài khoản đã tồn tại";
                res.ClientMessage = "Tên tài khoản đã tồn tại";
                return BadRequest(res);
            }
            else
            {
                var user = new User() { Email = registerRequest.Email, UserName = registerRequest.UserName };
                var createUser = await _userManager.CreateAsync(user, registerRequest.Password);
                if(createUser.Succeeded)
                {
                    res.ResultCode = ResultCode.SuccessCreated;
                    res.Data = 1;
                    res.DevMessage = "Tạo mới tài khoản thành công";
                    res.ClientMessage = "Tạo mới tài khoản thành công";
                    return StatusCode(StatusCodes.Status201Created, res);
                } 
                else
                {
                    res.ResultCode = ResultCode.ServerError;
                    res.Data = null;
                    res.IdentityError = createUser.Errors;
                    return StatusCode(StatusCodes.Status500InternalServerError, res);
                }    
            }    
        }
    }
}
