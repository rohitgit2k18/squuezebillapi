﻿using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
namespace SqueezeBill.Data.Repository
{
   public interface ISwichImage
    {
        SwitchingListResponseModel GetSwitchImageList();
    }
}
