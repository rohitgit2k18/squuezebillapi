using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
    public class PlanDetailResponseModel
    {
        public PlanDetailResponseModel()
        {
            Response = new PlanDetailModel();
        }
        public PlanDetailModel Response{ get; set; }
    }

    public class PlanDetail {
        public string Logo { get; set; }
        public string RetailerName { get; set; }
        public string Per_Kwh { get; set; }
        public decimal? Fees { get; set; }
        public string GreenEnergy { get; set; }
        public int? Duration { get; set; }
        public string Offers { get; set; }
    }

    public class PlanDetailModel {
        public PlanDetailModel()
        {
            Details = new PlanDetail();
        }
        public PlanDetail Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
