using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class CompareRatesRetailerListResponseModel
    {
        public CompareRatesRetailerListResponseModel()
        {
            Response = new CompareRatesRetailerDetails();
        }
        public CompareRatesRetailerDetails Response { get; set; }
    }

    public class CRRetailerCompanyList
    {
        public int? Planid { get; set; }
        public string Offer { get; set; }
        public int? Duration { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public string ImagePath { get; set; }
        public string RetailerName { get; set; }
        public int RetailerId { get; set; }
        public bool IsResidential { get; set; }
    }

    public class CompareRatesRetailerDetails
    {
        public CompareRatesRetailerDetails()
        {
            RetailerCompareListDetails = new List<CRRetailerCompanyList>();
        }
        public List<CRRetailerCompanyList> RetailerCompareListDetails { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
