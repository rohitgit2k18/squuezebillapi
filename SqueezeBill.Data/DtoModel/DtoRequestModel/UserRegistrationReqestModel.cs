using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
  public class UserRegistrationReqestModel
    {
        [Required(ErrorMessage = "Field can't be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public int? PhoneCountryCode { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public bool TermandCondition1 { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public bool TermandCondition2 { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public bool TermandCondition3 { get; set; }
        
        [Required(ErrorMessage = "Field can't be empty")]
        public string MedEdAccountatthisAddress { get; set; }
        
        public string LastName { get; set; }
        public int? CountryCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsMedEdAccountat { get; set; }   
        public string InfoAbout { get; set; }
    }
}
