using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class CompareRatesListResponseModel
    {
        public CompareRatesListResponseModel()
        {
            Response = new CompareRatesDetails();
        }
        public CompareRatesDetails Response { get; set; }
    }

    public class CompanyList
    {
        public Nullable<decimal> FutureAnnualSavings { get; set; }
        public List<SupplierCompanyList> SupplierList { get; set; }
        public List<RetailerCompanyList> RetailerList { get; set; }
    }

    public class SupplierCompanyList
    {
        public string CompanyName { get; set; }
        public Nullable<decimal> Rate { get; set; }
    }

    public class RetailerCompanyList
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

    public class CompareRatesDetails {

        public CompareRatesDetails()
        {
            CompareListDetails = new CompanyList();
        }
        public CompanyList CompareListDetails { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }

    
}
