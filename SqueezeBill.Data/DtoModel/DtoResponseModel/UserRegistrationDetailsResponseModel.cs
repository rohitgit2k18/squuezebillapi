using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class UserRegistrationDetailsResponseModel
    {
        public UserRegistrationDetailsResponseModel()
        {
            Response = new UserRegistrationDetails();
        }
        public UserRegistrationDetails Response { get; set; }
    }

    public class UserRegistrationDetailsModel {
        public string FirstName { get; set; }      
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? CountryCode { get; set; }
        public string MobileNum { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string InfoAbout { get; set; }
        public string ProfilePic { get; set; }
        public bool? PriceAlert { get; set; }
    }

    public class UserRegistrationDetails {

        public UserRegistrationDetails()
        {
            Details = new UserRegistrationDetailsModel();
        }
        public UserRegistrationDetailsModel Details { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

}
