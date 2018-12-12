using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDatabase.Models;
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
    public class NewsImageListController : ControllerBase
    {
        private GetImageLisRepository _ObjGetimagelist;
        private SwichImageRepository _ObjGetSwichimagelist;
        private AreayaServiceRepository _ObjGetAreaList;
        private EnergyGlossaryRepositiry _ObjGetGlossaryList;
        private ILogger<NewsImageListController> _ImageLog;

        public NewsImageListController(ILogger<NewsImageListController> log)
        {
            _ImageLog = log;
            _ObjGetimagelist = new GetImageLisRepository();
            _ObjGetSwichimagelist = new SwichImageRepository();
            _ObjGetAreaList = new AreayaServiceRepository();
            _ObjGetGlossaryList = new EnergyGlossaryRepositiry();
        }


        [HttpGet("GetListOfImageList")]
        [AllowAnonymous]
        public NewsListResponseModel GetListOfImageList()
        {
            return _ObjGetimagelist.GetNewsImageList();
        }

        [HttpGet("GetSwitchImageList")]
        [AllowAnonymous]
        public SwitchingListResponseModel GetSwitchImageList()
        {
            return _ObjGetSwichimagelist.GetSwitchImageList();
        }
        [HttpGet("GetAreaList")]
        [AllowAnonymous]
        public GetAreaServiceResponseModel GetAreaList()
        {
            return _ObjGetAreaList.GetAreaServiceImageList();
        }

        [HttpGet("GetGlossaryList")]
        [AllowAnonymous]
        public GlossaryListModelResponse GetGlossaryList()
        {
            return _ObjGetGlossaryList.GetEnergyGlossaryList();
        }
    }
}