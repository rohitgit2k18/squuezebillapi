using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.IRepository
{
   public interface IRegistrationRepository
    {
        UserRegistrationResponseModel CreateUserRegistration(UserRegistrationReqestModel UserRegister);
        UserRegistrationResponseModel UpdateUserRegistration(UpdateUserRegistrationRequestModel UpdateregisterDto, string UserEmailId);
        UserRegistrationDetailsResponseModel UserRegistrationDetail(string UserEmailId);
        UserRegistrationResponseModel UpdateUserProfilePic(string ImageBase64, string UserEmailId);
    }
}
