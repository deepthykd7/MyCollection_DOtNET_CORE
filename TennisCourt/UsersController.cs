using BLL;
using DTO;
using DTO.Request;
using DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TennisCourtApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRegistrationBLL _userRegistrationBLL;

                public UsersController(UserRegistrationBLL userRegistrationBLL)
        {
            _userRegistrationBLL = userRegistrationBLL ?? throw new ArgumentNullException(nameof(userRegistrationBLL));
        }

        [HttpPost("UserRegistration")]
        public ActionResult<BaseResponse<UserRegistrationResponse>> UserRegistration([FromBody] UserRegistrationRequest request)
        {
            if (ModelState.IsValid)
            {
                                return _userRegistrationBLL.RegisterUser(request);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [Authorize]
        [HttpPost("UserLogin")]
        public ActionResult<BaseResponse<LoginResponse>>UserLogin([FromBody]Login_request userLogin)
        {
            if(ModelState.IsValid)
            {
                return _userRegistrationBLL.LoginUser(userLogin);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}
