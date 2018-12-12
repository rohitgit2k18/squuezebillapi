using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class FiltertedRetailerListResponseModel
    {
        public FiltertedRetailerListResponseModel()
        {
            Response = new FiltertedRetailerDetails();
        }
        public FiltertedRetailerDetails Response { get; set; }
    }

    public class FiltertedRetailerCompanyList
    {       
        public string RetailerName { get; set; }
        //public bool IsResidential { get; set; }
    }

    public class FiltertedRetailerDetails
    {
        public FiltertedRetailerDetails()
        {
            FilteredRetailerObj = new List<FiltertedRetailerCompanyList>();
        }
        public List<FiltertedRetailerCompanyList> FilteredRetailerObj { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
