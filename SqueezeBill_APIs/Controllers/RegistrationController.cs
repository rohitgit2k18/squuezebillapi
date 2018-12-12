using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using SqueezeBill.Data.IRepository;
using SqueezeBill.Data.Repository;

namespace SqueezeBill_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IRegistrationRepository _ObjIRegistration;
        readonly ILogger<RegistrationController> _log;
        public RegistrationController(ILogger<RegistrationController> log)
        {
            _log = log;
            _ObjIRegistration = new RegistrationRepository();
        }

        [AllowAnonymous]
        [HttpPost("UserRegister")]
        public UserRegistrationResponseModel UserRegister([FromBody]UserRegistrationReqestModel registerDto)
        {
            return _ObjIRegistration.CreateUserRegistration(registerDto);
        }


        [HttpPut("UpdateUserRegistration")]
        [Authorize]
        public UserRegistrationResponseModel UpdateUserRegistration([FromBody]UpdateUserRegistrationRequestModel UpdateregisterDto)
        {
            UserRegistrationResponseModel Response = new UserRegistrationResponseModel();
            bool IsAuth = User.Identity.IsAuthenticated;
            if (IsAuth != false)
            {
                string UserEmailId = User.Identity.Name;
                return _ObjIRegistration.UpdateUserRegistration(UpdateregisterDto, UserEmailId);
            }
            else
            {
                Response.Response.Message = "Invaid Token!";
                Response.Response.StatusCode = 401;
                return Response;
            }
        }

        [HttpGet("UserRegistrationDetail")]
        [Authorize]
        public UserRegistrationDetailsResponseModel UserRegistrationDetail()
        {
            UserRegistrationDetailsResponseModel Response = new UserRegistrationDetailsResponseModel();
            bool IsAuth = User.Identity.IsAuthenticated;
            if (IsAuth != false)
            {
                string UserEmailId = User.Identity.Name;
                return _ObjIRegistration.UserRegistrationDetail(UserEmailId);
            }
            else
            {
                Response.Response.Message = "Invaid Token!";
                Response.Response.StatusCode = 401;
                return Response;
            }

        }

        [HttpPut("UpdateUserProfilePic")]
        [Authorize]
        public UserRegistrationResponseModel UpdateUserProfilePic(UpdateUserProfilePicRequestModel ImageBase64)
        {
            UserRegistrationResponseModel Response = new UserRegistrationResponseModel();
            bool IsAuth = User.Identity.IsAuthenticated;
            if (IsAuth != false)
            {
                string UserEmailId = User.Identity.Name;
                return _ObjIRegistration.UpdateUserProfilePic(ImageBase64.Imagebase, UserEmailId);
            }
            else
            {
                Response.Response.Message = "Invaid Token!";
                Response.Response.StatusCode = 401;
                return Response;
            }
        }


    }
}