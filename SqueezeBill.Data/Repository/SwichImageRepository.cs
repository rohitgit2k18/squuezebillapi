using EntityDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using SqueezeBill.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SqueezeBill.Data.Repository
{
   public class SwichImageRepository: ISwichImage
    {
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
       
        public SwitchingListResponseModel GetSwitchImageList()
        {
            SwitchingListResponseModel objSwitching = new SwitchingListResponseModel();
            List <SwitchingListDetailsModel> objSwitchingList = new List<SwitchingListDetailsModel>();
            try
            {
                var GetswitchingList = _ObjDBContext.HowSwitching.Where(x => x.IsDelete == false).ToList();
                foreach (var item in GetswitchingList)
                {
                    var Image = _ObjDBContext.HowSwitchingImage.Where(x => x.HowSwitchingId == item.Id).FirstOrDefault();
                    if (Image != null)
                    {
                        SwitchingListDetailsModel objSwitch = new SwitchingListDetailsModel();
                        objSwitch.Description = item.Description;
                        objSwitch.Id = item.Id;
                        objSwitch.ShortDescription = item.ShortDescription;
                        objSwitch.ImageURL = Image.ImagePaht;
                        objSwitchingList.Add(objSwitch);
                    }
                }
                objSwitching.Response.Details = objSwitchingList;
                objSwitching.Response.Message = "Switch List";
                objSwitching.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                objSwitching.Response.Message = Convert.ToString(ex);
                objSwitching.Response.StatusCode = 401;
            }
            return objSwitching;
        }       
    }
}
