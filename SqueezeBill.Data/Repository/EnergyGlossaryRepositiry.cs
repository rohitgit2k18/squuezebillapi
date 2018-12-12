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
  public  class EnergyGlossaryRepositiry:IEnergyGlossary
    {
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        private ILogger<EnergyGlossaryRepositiry> _ImageListcontext;

        public GlossaryListModelResponse GetEnergyGlossaryList()
        {
            GlossaryListModelResponse ObjGlossaryResponse = new GlossaryListModelResponse();
            List<GlossaryListDetailsModel> ObjGlossaryList = new List<GlossaryListDetailsModel>();
            try
            {
                var GetswitchingList = _ObjDBContext.EnergyGlossary.Where(x => x.IsDelete == false).ToList();
                if (GetswitchingList.Count > 0)
                {
                    foreach (var item in GetswitchingList)
                    {
                        var Image = _ObjDBContext.EnergyGlossaryImage.Where(x => x.EnergyGlossaryId == item.Id).FirstOrDefault();
                        if (Image != null)
                        {
                            GlossaryListDetailsModel ObjGlossary = new GlossaryListDetailsModel();
                            ObjGlossary.Description = item.Description;
                            ObjGlossary.Id = item.Id;
                            ObjGlossary.ShortDescription = item.ShortDescription;
                            ObjGlossary.ImageURL = Image.ImagePaht;
                            ObjGlossaryList.Add(ObjGlossary);
                        }
                    }
                    ObjGlossaryResponse.Response.Details = ObjGlossaryList;
                    ObjGlossaryResponse.Response.Message = "Glossary List";
                    ObjGlossaryResponse.Response.StatusCode = 200;
                }
                else
                {
                    ObjGlossaryResponse.Response.Message = "No Records Found !";
                    ObjGlossaryResponse.Response.StatusCode = 401;
                }

            }
            catch (Exception ex)
            {
                ObjGlossaryResponse.Response.Message = Convert.ToString(ex);
                ObjGlossaryResponse.Response.StatusCode = 401;
            }
            return ObjGlossaryResponse;
        }

        //public List<GlossaryListModel> GetEnergyGlossaryList()
        //{
        //    var Energ = _ObjDBContext.EnergyGlossary.Where(x => x.IsDelete == false).ToList();

        //    List<GlossaryListModel> List = new List<GlossaryListModel>();
        //    foreach (var item in Energ)
        //    {
        //        GlossaryListModel single = new GlossaryListModel();
        //        single.ShortDescription = item.ShortDescription;
        //        single.Id = item.Id;
        //        single.Description = item.Description;

        //        var Image = _ObjDBContext.EnergyGlossaryImage.Where(x => x.EnergyGlossaryId == item.Id).ToList();
        //        single.GetImagebyId = Image;
        //        List.Add(single);
        //    }
        //    return List;

        //}
    }
}
