using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.IRepository
{
   public interface IProductRepository
    {
        ZipCodeResponseModel GetZipCodeList();
        CompareRatesListResponseModel CompareRatesList(CompareRatesListRequestModel CompareRatesModel);
        PlanDetailResponseModel PlanDetails(int PlanId, bool IsResidential);
        CompareRatesRetailerListResponseModel CompareRatesRetailerList(Search CompareRatesModel, string SupplierCompanyName, int TermsFrom, int TermsTo);
        CompareRatesSupplierListResponseModel CompareRatesSupplierList(Search CompareRatesModel);
        FiltertedRetailerListResponseModel FilterRetailerList(Search CompareRatesModel, string SupplierCompanyName);
    }
}
