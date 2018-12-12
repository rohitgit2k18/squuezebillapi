using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public string MobilePhoneNumber { get; set; }
        public int? MobilePhoneCountryCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool? TermandCondition1 { get; set; }
        public bool? TermandCondition2 { get; set; }
        public bool? TermandCondition3 { get; set; }
        public bool? IsMedEdAccountat { get; set; }
        public string MedEdAccountatthisAddress { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ImageId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? UserType { get; set; }
        public string InfoAbout { get; set; }
        public string PerUnit { get; set; }
        public string Otp { get; set; }
        public bool? PriceAlert { get; set; }
    }
}
