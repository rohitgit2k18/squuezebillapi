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
    public class AreayaServiceRepository: IAreaService
    {
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        private ILogger<AreaServicesModel> _AreayaListcontext;
        
        //public List<AreaServicesModel> GetAreaServiceImageList()
        //{

        //    var Service = _ObjDBContext.ServiceArea.Where(x => x.IsDelete == false).ToList();

        //    List<AreaServicesModel> List = new List<AreaServicesModel>();
        //    foreach (var item in Service)
        //    {
        //        AreaServicesModel single = new AreaServicesModel();
        //        single.ShortDescription = item.ShortDescription;
        //        single.Id = item.Id;
        //        single.Description = item.Description;

        //        var Image = _ObjDBContext.ServiceAreaImage.Where(x => x.ServiceAreaId == item.Id).ToList();
        //        single.GetImagebyId = Image;
        //        List.Add(single);
        //    }
        //    return List;
        //}


        public GetAreaServiceResponseModel GetAreaServiceImageList()
        {
            GetAreaServiceResponseModel ObjAreaService = new GetAreaServiceResponseModel();
            List<AreaServiceListDetailsModel> objAreaServiceList = new List<AreaServiceListDetailsModel>();
            try
            {
                var GetswitchingList = _ObjDBContext.ServiceArea.Where(x => x.IsDelete == false).ToList();
                if (GetswitchingList.Count>0)
                {
                    foreach (var item in GetswitchingList)
                    {
                        var Image = _ObjDBContext.ServiceAreaImage.Where(x => x.ServiceAreaId == item.Id).FirstOrDefault();
                        if (Image != null)
                        {
                            AreaServiceListDetailsModel objAreaservice = new AreaServiceListDetailsModel();
                            objAreaservice.Description = item.Description;
                            objAreaservice.Id = item.Id;
                            objAreaservice.ShortDescription = item.ShortDescription;
                            objAreaservice.ImageURL = Image.ImagePaht;
                            objAreaServiceList.Add(objAreaservice);
                        }
                    }
                    ObjAreaService.Response.Details = objAreaServiceList;
                    ObjAreaService.Response.Message = "Area Service List";
                    ObjAreaService.Response.StatusCode = 200;
                }
                else
                {     
                    ObjAreaService.Response.Message = "No Records Found !";
                    ObjAreaService.Response.StatusCode = 401;
                }

            }
            catch (Exception ex)
            {
                ObjAreaService.Response.Message = Convert.ToString(ex);
                ObjAreaService.Response.StatusCode = 401;
            }
            return ObjAreaService;
        }
    }

}


