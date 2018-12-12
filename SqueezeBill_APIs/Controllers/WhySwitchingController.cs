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
    public class WhySwitchingController : ControllerBase
    {    
        private IWhySwitch _Objlist;
        
        public WhySwitchingController()
        {
            _Objlist = new WhySwitch();
        }
        [HttpGet("WhySwitchList")]
        public WhySwitchListResponseModel WhySwitchList()
        {
            return _Objlist.ListWhySwitching();

        }
        [HttpGet("FAQ")]
        public FAQsListResponseModel FAQ()
        {
            return _Objlist.ListFAQ();

        }
        [HttpGet("Contact")]
        public ContactUsResponseModel Contact()
        {
            return _Objlist.Contact();

        }
    }
}