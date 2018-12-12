using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string EmailId { get; set; }
        public int? LogoId { get; set; }
        public string MobileNumber { get; set; }
        public int? CountryId { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public int? ZipCode { get; set; }
        public string Address { get; set; }
        public bool? IsElectricity { get; set; }
        public bool? IsGas { get; set; }
        public bool IsResidential { get; set; }
        public bool? IsCommecial { get; set; }
        public string UnitCharget { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string KhwM { get; set; }
        public string CKwh { get; set; }
        public string CcfM { get; set; }
        public string CCcf { get; set; }
        public decimal? CommecialRate { get; set; }
        public decimal? ResidentialRate { get; set; }
        public int? Duration { get; set; }
    }
}
