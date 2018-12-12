using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.IRepository
{
    public interface INewImage
    {
        NewsListResponseModel GetNewsImageList();
    }
}
