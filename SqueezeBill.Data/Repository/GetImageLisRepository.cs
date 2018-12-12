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

    public class GetImageLisRepository : INewImage
    {
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        private ILogger<GetImageLisRepository> _ImageListcontext;       

        public NewsListResponseModel GetNewsImageList()
        {
            NewsListResponseModel ObjNLRM = new NewsListResponseModel();
            List<NewsListDetailsModel> objNewsList = new List<NewsListDetailsModel>();
            try
            {
                var NewsList = _ObjDBContext.News.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
                foreach (var item in NewsList)
                {
                    var Image = _ObjDBContext.NewsImage.Where(x => x.NewsId == item.NewsId).FirstOrDefault();
                    if (Image != null)
                    {
                        NewsListDetailsModel ObjNLDM = new NewsListDetailsModel();
                        ObjNLDM.CompleteDescription = item.CompleteDescription;
                        ObjNLDM.Id = item.NewsId;
                        ObjNLDM.ShortDescription = item.ShortDescription;
                        ObjNLDM.Image = Image.ImagePaht;
                        objNewsList.Add(ObjNLDM);
                    }
                }
                ObjNLRM.Response.Details = objNewsList;
                ObjNLRM.Response.Message = "News List";
                ObjNLRM.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                ObjNLRM.Response.Message = Convert.ToString(ex);
                ObjNLRM.Response.StatusCode = 401;
            }
            return ObjNLRM;
        }

       
    }
}
