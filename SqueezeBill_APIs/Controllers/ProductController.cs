using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using SqueezeBill.Data.IRepository;
using SqueezeBill.Data.Repository;

namespace SqueezeBill_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _ObjIProduct;
        readonly ILogger<ProductController> _log;
        public ProductController(ILogger<ProductController> log)
        {
            _log = log;
            _ObjIProduct = new ProductRepository();
        }

        [AllowAnonymous]
        [HttpGet("ZipCodeList")]
        public ZipCodeResponseModel ZipCodeList()
        {
            return _ObjIProduct.GetZipCodeList();
        }

        [AllowAnonymous]
        [HttpPost("CompareRates")]
        public CompareRatesListResponseModel CompareRates(CompareRatesListRequestModel CompareRatesModel)
        {
            return _ObjIProduct.CompareRatesList(CompareRatesModel);
        }


        [AllowAnonymous]
        [HttpGet("PlanDetails/{PlanId}/{IsResidential}")]
        public PlanDetailResponseModel PlanDetails(int PlanId, bool IsResidential)
        {
            return _ObjIProduct.PlanDetails(PlanId, IsResidential);
        }

        [AllowAnonymous]
        [HttpPost("CompareRatesRetailerList/{SupplierCompanyName}/{TermsFrom}/{TermsTo}")]
        public CompareRatesRetailerListResponseModel CompareRatesRetailerList(Search CompareRatesModel, string SupplierCompanyName, int TermsFrom, int TermsTo)
        {
            return _ObjIProduct.CompareRatesRetailerList(CompareRatesModel, SupplierCompanyName, TermsFrom, TermsTo);
        }


        [AllowAnonymous]
        [HttpPost("CompareRatesSupplierList")]
        public CompareRatesSupplierListResponseModel CompareRatesSupplierList(Search CompareRatesModel)
        {
            return _ObjIProduct.CompareRatesSupplierList(CompareRatesModel);
        }


        [AllowAnonymous]
        [HttpPost("FilterRetailerList/{SupplierCompanyName}")]
        public FiltertedRetailerListResponseModel CompareRatesRetailerList(Search CompareRatesModel, string SupplierCompanyName)
        {
            return _ObjIProduct.FilterRetailerList(CompareRatesModel, SupplierCompanyName);
        }

    }
}