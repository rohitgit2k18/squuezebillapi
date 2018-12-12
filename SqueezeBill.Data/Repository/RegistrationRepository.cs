using EntityDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using SqueezeBill.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqueezeBill.Data.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        private ILogger<RegistrationRepository> _log;

        public UserRegistrationResponseModel CreateUserRegistration(UserRegistrationReqestModel registerDto)
        {
            UserRegistrationResponseModel Response = new UserRegistrationResponseModel();
            try
            {
                var user = _ObjDBContext.Users.FirstOrDefault(x => x.Email == registerDto.Email);
                if (user != null)
                {
                    Response.Response.StatusCode = 201;
                    Response.Response.Message = "This e-mail Id" + registerDto.Email + " is already in use.";
                }
                else
                {
                    user = new Users
                    {
                        FirstName = registerDto.FirstName,
                        LastName = registerDto.LastName,
                        Email = registerDto.Email,
                        MobilePhoneCountryCode = registerDto.PhoneCountryCode,
                        MobilePhoneNumber = registerDto.PhoneNumber,
                        CreatedDate = DateTime.UtcNow,
                        ImageId = 0,
                        Address1 = registerDto.Address1,
                        Address2 = registerDto.Address2,
                        ZipCode = registerDto.ZipCode,
                        State = registerDto.State,
                        City = registerDto.City,
                        TermandCondition1 = registerDto.TermandCondition1,
                        TermandCondition2 = registerDto.TermandCondition2,
                        TermandCondition3 = registerDto.TermandCondition3,
                        IsMedEdAccountat = registerDto.IsMedEdAccountat,
                        MedEdAccountatthisAddress = registerDto.MedEdAccountatthisAddress,
                        IsDeleted = false,
                        IsActive = true,
                        UserType = 4,
                        InfoAbout = registerDto.InfoAbout,
                    };
                    user.SecurityStamp = HashHelper.GetPasswordSalt();
                    user.PasswordHash = HashHelper.GetPasswordHash(user.SecurityStamp, registerDto.Password);
                    _ObjDBContext.Users.Add(user);
                    _ObjDBContext.SaveChanges();
                    Response.Response.Message = "This User is registered with Squeeze Bill as Customer.";
                    Response.Response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Response.Response.Message = Convert.ToString(ex);
                Response.Response.StatusCode = 401;
                _log.LogInformation(Response.Response.Message);
            }
            return Response;
        }

        public UserRegistrationResponseModel UpdateUserProfilePic(string ImageBase64, string UserEmailId)
        {
            UserRegistrationResponseModel _objUserRegistrationResponseModel = new UserRegistrationResponseModel();
            try
            {
                var res = _ObjDBContext.Users.Where(x => x.Email == UserEmailId).FirstOrDefault();
                if (res != null)
                {
                    Image objImage = new Image();
                    objImage.ImagePath = ImageBase64;
                    objImage.IsActive = true;
                    objImage.IsDeleted = false;
                    _ObjDBContext.Image.Add(objImage);
                    _ObjDBContext.SaveChanges();
                    //..................
                    res.ImageId = objImage.ImageId;
                    res.UpdatedDate = DateTime.Now;
                    _ObjDBContext.Entry(res).State = EntityState.Modified;
                    _ObjDBContext.SaveChanges();
                    _objUserRegistrationResponseModel.Response.Message = UserEmailId + " Profile Picture uploaded successfully !";
                    _objUserRegistrationResponseModel.Response.StatusCode = 200;
                }
                else
                {
                    _objUserRegistrationResponseModel.Response.Message = UserEmailId + " Not Found !";
                    _objUserRegistrationResponseModel.Response.StatusCode = 201;
                }
            }
            catch (Exception ex)
            {
                _objUserRegistrationResponseModel.Response.Message = Convert.ToString(ex);
                _objUserRegistrationResponseModel.Response.StatusCode = 401;
                _log.LogInformation(_objUserRegistrationResponseModel.Response.Message);
            }
           return _objUserRegistrationResponseModel;

        }

        public UserRegistrationResponseModel UpdateUserRegistration(UpdateUserRegistrationRequestModel UpdateregisterDto, string UserEmailId)
        {
            UserRegistrationResponseModel Response = new UserRegistrationResponseModel();
            try
            {
                if (UpdateregisterDto != null)
                {
                    var res = _ObjDBContext.Users.Where(x => x.Email == UserEmailId).FirstOrDefault();
                    if (res != null)
                    {
                        res.UpdatedDate = DateTime.Now;
                        res.MobilePhoneCountryCode = UpdateregisterDto.CountryCode;
                        res.MobilePhoneNumber = UpdateregisterDto.MobileNum;
                        res.Address1 = UpdateregisterDto.Address1;
                        res.Address2 = UpdateregisterDto.Address2;
                        res.ZipCode = UpdateregisterDto.ZipCode;
                        res.State = UpdateregisterDto.State;
                        res.City = UpdateregisterDto.City;
                        res.InfoAbout = UpdateregisterDto.InfoAbout;
                        res.FirstName = UpdateregisterDto.FirstName;
                        res.LastName = UpdateregisterDto.LastName;
                        res.SecurityStamp = HashHelper.GetPasswordSalt();
                        res.PasswordHash = HashHelper.GetPasswordHash(res.SecurityStamp, UpdateregisterDto.Password);
                        res.PriceAlert = UpdateregisterDto.PriceAlert;
                        _ObjDBContext.Entry(res).State = EntityState.Modified;
                        _ObjDBContext.SaveChanges();
                        Response.Response.Message = UserEmailId + " Registeration details has been updated.";
                        Response.Response.StatusCode = 200;
                    }
                    else
                    {
                        Response.Response.Message = UserEmailId + " Not Found !";
                        Response.Response.StatusCode = 201;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation(Convert.ToString(ex));
                Response.Response.Message = Convert.ToString(ex);
                Response.Response.StatusCode = 401;
            }
            return Response;
        }

        public UserRegistrationDetailsResponseModel UserRegistrationDetail(string UserEmailId)
        {
            UserRegistrationDetailsResponseModel Response = new UserRegistrationDetailsResponseModel();
            UserRegistrationDetailsModel URDM = new UserRegistrationDetailsModel();
            try
            {
                var GetUserInfo = _ObjDBContext.Users.Where(x => x.Email == UserEmailId).FirstOrDefault();
                if (GetUserInfo != null)
                {
                    var GetImage = _ObjDBContext.Image.Where(x => x.ImageId == GetUserInfo.ImageId).FirstOrDefault();
                    if (GetImage!=null)
                    {
                        URDM.ProfilePic = GetImage.ImagePath;
                    }
                    else
                    {
                        URDM.ProfilePic = "";
                    }
                    URDM.FirstName = GetUserInfo.FirstName;
                    URDM.LastName = GetUserInfo.LastName;
                    URDM.Email = GetUserInfo.Email;
                    URDM.CountryCode = GetUserInfo.MobilePhoneCountryCode;
                    URDM.MobileNum = GetUserInfo.MobilePhoneNumber;
                    URDM.Address1 = GetUserInfo.Address1;
                    URDM.Address2 = GetUserInfo.Address2;
                    URDM.ZipCode = GetUserInfo.ZipCode;
                    URDM.State = GetUserInfo.State;
                    URDM.City = GetUserInfo.City;
                    URDM.InfoAbout = GetUserInfo.InfoAbout;
                    URDM.PriceAlert = GetUserInfo.PriceAlert;                    
                    Response.Response.Message = "Successfully";
                    Response.Response.StatusCode = 200;
                    Response.Response.Details = URDM;
                }
                else
                {
                    Response.Response.Message = "Invaid Token!";
                    Response.Response.StatusCode = 401;
                }
            }
            catch (Exception ex)
            {
                Response.Response.Message = Convert.ToString(ex);
                Response.Response.StatusCode = 401;
            }
            return Response;
        }
    }
}
