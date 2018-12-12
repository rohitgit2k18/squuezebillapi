using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class PricePlan
    {
        public int Planid { get; set; }
        public int? RetailerId { get; set; }
        public decimal? CommercialPrice { get; set; }
        public decimal? ResidentialPrice { get; set; }
        public int? Duration { get; set; }
        public string Offer { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
