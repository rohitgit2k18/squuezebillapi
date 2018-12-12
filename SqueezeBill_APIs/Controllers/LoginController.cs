using EntityDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using SqueezeBill.Data.IRepository;
using SqueezeBill.Data.Repository;
using System.Linq;

namespace SqueezeBill_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginRepository _ObjILogin;
        readonly ILogger<LoginController> _log;
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        
        public LoginController(ILogger<LoginController> log)
        {
            _log = log;
            _ObjILogin = new LoginRepository();
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                              
        [AllowAnonymous]
        [HttpPost("UserLogin")]
        public UserLoginResponseModel UserLogin([FromBody] UserLoginReqeuestModel model)
        {
            return _ObjILogin.ValidateLogin(model);
        }

        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public ForgetPasswordResponseModel ForgetPassword([FromBody]ForgetPasswordRequestModel model)
        {
            return _ObjILogin.ForgetPassword(model);
        }

        [AllowAnonymous]
        [HttpPut("ResetPassword")]
        public ResetPasswordResponseModel ResetPassword([FromBody]ResetPasswordRequestModel RPRM)
        {
                return _ObjILogin.ResetPassword(RPRM);
        }
    }
}