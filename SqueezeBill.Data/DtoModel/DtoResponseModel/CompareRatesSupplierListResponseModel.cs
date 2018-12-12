using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class CompareRatesSupplierListResponseModel
    {
        public CompareRatesSupplierListResponseModel()
        {
            Response = new CompareRatesSupplierDetails();
        }
        public CompareRatesSupplierDetails Response { get; set; }
    }

    public class CRSupplierCompanyList
    {
        public string CompanyName { get; set; }
        public Nullable<decimal> Rate { get; set; }
    }

    public class CompareRatesSupplierDetails
    {
        public CompareRatesSupplierDetails()
        {
            SupplierCompareListDetails = new List<CRSupplierCompanyList>();
        }
        public List<CRSupplierCompanyList> SupplierCompareListDetails { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
