﻿using EntityDatabase.Models;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.IRepository
{
    public interface IWhySwitch
    {
        WhySwitchListResponseModel ListWhySwitching();
        FAQsListResponseModel ListFAQ();
        ContactUsResponseModel Contact();
    }
}
