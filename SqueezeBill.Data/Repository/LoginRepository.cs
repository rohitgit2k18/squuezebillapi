using EntityDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.DtoServiceModel.DtoServiceRequetModel;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using SqueezeBill.Data.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SqueezeBill.Data.Repository
{
    public class LoginRepository : ILoginRepository
    {                           
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        private ILogger<RegistrationRepository> _log;
        private string Key = "CNUnwR05TRtiO6kCDbTtvddLmBD6E9rR";

        public ForgetPasswordResponseModel ForgetPassword(ForgetPasswordRequestModel model)
        {
            ForgetPasswordResponseModel FPRM = new ForgetPasswordResponseModel();
            try
            {
                Users UserValidEmailId = _ObjDBContext.Users.FirstOrDefault(u => u.Email.ToLower() == model.EmailId.ToLower() && u.IsActive == true && u.IsDeleted == false);
                Users UserValidMobileNum = _ObjDBContext.Users.FirstOrDefault(u => u.MobilePhoneNumber.ToLower() == model.MobileNum.ToLower() && u.IsActive == true && u.IsDeleted == false);

                Random _random = new Random();
                string otp = _random.Next(0, 999999).ToString("D6");

                if (UserValidEmailId != null)
                {
                    var UpdateOTPEmail = _ObjDBContext.Users.Where(s => s.Email == model.EmailId).FirstOrDefault();
                    if (UpdateOTPEmail != null)
                    {
                        UpdateOTPEmail.Otp = otp;
                        _ObjDBContext.Entry(UpdateOTPEmail).State = EntityState.Modified;
                        _ObjDBContext.SaveChanges();
                    }

                    SendEmailRequestModel SERM = new SendEmailRequestModel();
                    SERM.CustomerName = UserValidEmailId.FirstName;
                    SERM.FileAttachmentURL = "";
                    SERM.MessageBody = "Hi " + UserValidEmailId.FirstName + " Please Use " + otp + " as OTP for Change password!";
                    SERM.Subject = "Squeeze Bill Forget Password";
                    SERM.ToEmailId = UserValidEmailId.Email;
                    EmailService ES = new EmailService();
                    string EmailService = ES.SendEmail(SERM);
                    FPRM.Response.Message = "Please check your Email Inbox for change password !";
                    FPRM.Response.StatusCode = 200;
                }
                else if (UserValidEmailId == null)
                {
                    FPRM.Response.Message = model.EmailId + " Invalid";
                    FPRM.Response.StatusCode = 201;
                }
                else if (UserValidMobileNum != null)
                {
                    var UpdateOTPMobile = _ObjDBContext.Users.Where(s => s.MobilePhoneNumber == model.MobileNum).FirstOrDefault();
                    if (UpdateOTPMobile != null)
                    {
                        UpdateOTPMobile.Otp = otp;
                        _ObjDBContext.Entry(UpdateOTPMobile).State = EntityState.Modified;
                        _ObjDBContext.SaveChanges();
                    }

                    SendSMSRequestModel SSRM = new SendSMSRequestModel();
                    SSRM.CustomerName = UserValidEmailId.FirstName;
                    SSRM.MessageBody = "Hi " + UserValidEmailId.FirstName + " Please Use " + otp + " as OTP for Change password!";
                    SSRM.ToMobileNum = UserValidEmailId.MobilePhoneCountryCode + " " + UserValidEmailId.MobilePhoneNumber;
                    SMSService SS = new SMSService();
                    string SMSService = SS.SendSMS(SSRM);
                    FPRM.Response.Message = "Please check your Mobile Message for change password !";
                    FPRM.Response.StatusCode = 200;
                }
                else if (UserValidMobileNum == null)
                {
                    FPRM.Response.Message = model.MobileNum + " Invalid";
                    FPRM.Response.StatusCode = 201;
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation("UnExpected");
                FPRM.Response.Message = Convert.ToString(ex);
                FPRM.Response.StatusCode = 401;
            }
            return FPRM;
        }

        public ResetPasswordResponseModel ResetPassword(ResetPasswordRequestModel RPRM)
        {
            ResetPasswordResponseModel rprm = new ResetPasswordResponseModel();
            try
            {
                if (RPRM.NewPassword != RPRM.ConfirmPassword)
                {
                    rprm.Response.Message = "New Password not matched with confirm password!";
                    rprm.Response.StatusCode = 400;
                }
                else if (RPRM.NewPassword == RPRM.ConfirmPassword)
                {
                    var GetUserInfo = _ObjDBContext.Users.Where(s => s.Otp == RPRM.OTP).FirstOrDefault();
                    if (GetUserInfo != null)
                    {
                        GetUserInfo.SecurityStamp = HashHelper.GetPasswordSalt();
                        GetUserInfo.PasswordHash = HashHelper.GetPasswordHash(GetUserInfo.SecurityStamp, RPRM.NewPassword);
                        _ObjDBContext.Entry(GetUserInfo).State = EntityState.Modified;
                        _ObjDBContext.SaveChanges();
                        rprm.Response.Message = "Password is changed Successfully !";
                        rprm.Response.StatusCode = 200;
                    }
                    else
                    {
                        rprm.Response.Message = "Wrong OTP";
                        rprm.Response.StatusCode = 401;
                    }
                }
            }
            catch (Exception ex)
            {
                rprm.Response.Message = Convert.ToString(ex);
                rprm.Response.StatusCode = 400;
            }
            return rprm;
        }

        public UserLoginResponseModel ValidateLogin(UserLoginReqeuestModel model)
        {
            UserLoginResponseModel ObjResponse = new UserLoginResponseModel();
            try
            {
                Users user = _ObjDBContext.Users.FirstOrDefault(u => u.Email.ToLower() == model.Email.ToLower() && u.IsActive == true && u.IsDeleted == false);
                if (user != null)
                {
                    var passwordHash = HashHelper.GetPasswordHash(user.SecurityStamp, model.Password);
                    if (user.PasswordHash != passwordHash)
                    {
                        ObjResponse.ResponseMessage = "Password is Wrong !";
                        ObjResponse.StatusCode = 201;
                    }
                    else
                    {
                        //Below for generate JWT Token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(Key);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                    new Claim(ClaimTypes.Name, user.Email.ToString())
                            }),
                            Expires = DateTime.Now.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        ObjResponse.Token = tokenHandler.WriteToken(token);
                        ObjResponse.ExpiryDate = tokenDescriptor.Expires;
                        ObjResponse.ResponseMessage = "Ok";
                        ObjResponse.StatusCode = 200;
                    }
                }
                else
                {
                    ObjResponse.ResponseMessage = "Email Id is Wrong !";
                    ObjResponse.StatusCode = 201;
                }
            }
            catch (Exception ex)
            {
                ObjResponse.ResponseMessage = Convert.ToString(ex);
                ObjResponse.StatusCode = 401;
            }
            return ObjResponse;
        }

    }
}
