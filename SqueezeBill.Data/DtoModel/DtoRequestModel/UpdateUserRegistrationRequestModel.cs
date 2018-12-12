using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
    public class UpdateUserRegistrationRequestModel
    { 
        [Required(ErrorMessage = "Field can't be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string Password { get; set; }

        public string LastName { get; set; }

        public int? CountryCode { get; set; }
        public string MobileNum { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string InfoAbout { get; set; }

        public bool? PriceAlert { get; set; }
    }
}
