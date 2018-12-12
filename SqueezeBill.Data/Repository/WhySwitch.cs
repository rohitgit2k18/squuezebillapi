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
    public class WhySwitch : IWhySwitch
    {
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        private ILogger<WhySwitch> _ImageListcontext;

        public ContactUsResponseModel Contact()
        {
            ContactUsResponseModel ObjContactUsResponseModel = new ContactUsResponseModel();
            try
            {
                var ContactDetail = _ObjDBContext.ContactUs.FirstOrDefault();
                if (ContactDetail!=null)
                {
                    ContactUsDetail cd = new ContactUsDetail();
                    cd.Address = ContactDetail.Address;
                    cd.Content = ContactDetail.Content;
                    cd.Country = ContactDetail.Country;
                    cd.PhoneNumber = ContactDetail.PhoneNumber;
                    cd.State = ContactDetail.State;
                    cd.Zipcode = ContactDetail.Zipcode;
                    ObjContactUsResponseModel.Response.Details = cd;
                    ObjContactUsResponseModel.Response.Message = "Contact us Details";
                    ObjContactUsResponseModel.Response.StatusCode = 200;
                }
                else
                {
                    ObjContactUsResponseModel.Response.Message = "Contact Not Available";
                    ObjContactUsResponseModel.Response.StatusCode = 401;
                }
            }
            catch (Exception ex)
            {
                ObjContactUsResponseModel.Response.Message = "Contact us Details";
                ObjContactUsResponseModel.Response.StatusCode = 401;
            }
           return ObjContactUsResponseModel;
        }

        //public List<FAQ> ListFAQ()
        //{
        //    var Faq = _ObjDBContext.Faq.Where(x => x.IsDelete == false).ToList();
        //  List<FAQ> list = new List<FAQ>();
        //    foreach (var v in Faq)
        //    {
        //        FAQ oBJ = new FAQ();
        //        oBJ.Id = v.Id;
        //        oBJ.Question = v.Question;
        //        oBJ. Answer = v.Answer;
        //        list.Add(oBJ);
        //    }
        //    return list;
        //}

        //public List<CommonSwitchingData> ListWhySwitching()
        //{
        //    var WhySwitching = _ObjDBContext.WhySwitching.Where(x => x.IsDelete == false).ToList();
        //    List<CommonSwitchingData> list = new List<CommonSwitchingData>();
        //    foreach (var v in WhySwitching)
        //    {
        //        CommonSwitchingData Obj = new CommonSwitchingData();
        //        Obj.Description = v.Description;
        //        Obj.Id = v.Id;
        //        Obj.ShortDescription = v.ShortDescription;
        //        var Image = _ObjDBContext.WhySwitchingImage.Where(x => x.WhySwitchingId == v.Id).ToList();
        //        List<CommenImage> ImageList = new List<CommenImage>();
        //        foreach (var v1 in Image)
        //        {
        //            CommenImage obj = new CommenImage();
        //            obj.ImageId = v1.ImageId;
        //            obj.ImagePaht = v1.ImagePaht;
        //            ImageList.Add(obj);
        //        }
        //        Obj.ListImage = ImageList;
        //        list.Add(Obj);
        //    }

        //    return list;
        //}

        public WhySwitchListResponseModel ListWhySwitching()
        {
            WhySwitchListResponseModel ObjWhySwitchListResponseModel = new WhySwitchListResponseModel();
            List<WhySwitchListDetailsModel> objWhySwitchList = new List<WhySwitchListDetailsModel>();
            try
            {
                var whySwitchingsList = _ObjDBContext.WhySwitching.Where(s => s.IsDelete == false).ToList();
                foreach (var item in whySwitchingsList)
                {
                    var Image = _ObjDBContext.WhySwitchingImage.Where(x => x.WhySwitchingId == item.Id).FirstOrDefault();
                    if (Image != null)
                    {
                        WhySwitchListDetailsModel objWhySwitch = new WhySwitchListDetailsModel();
                        objWhySwitch.Description = item.Description;
                        objWhySwitch.Id = item.Id;
                        objWhySwitch.ShortDescription = item.ShortDescription;
                        objWhySwitch.ImagePaht = Image.ImagePaht;
                        objWhySwitchList.Add(objWhySwitch);
                    }
                }
                ObjWhySwitchListResponseModel.Response.Details = objWhySwitchList;
                ObjWhySwitchListResponseModel.Response.Message = "Why Switching List";
                ObjWhySwitchListResponseModel.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                ObjWhySwitchListResponseModel.Response.Message = Convert.ToString(ex);
                ObjWhySwitchListResponseModel.Response.StatusCode = 401;
            }
            return ObjWhySwitchListResponseModel;
        }

        public FAQsListResponseModel ListFAQ()
        {
            FAQsListResponseModel ObjFAQsListResponseModel = new FAQsListResponseModel();
            List<FAQsListDetailsModel> objFAQsList = new List<FAQsListDetailsModel>();
            try
            {
                var FAQList = _ObjDBContext.Faq.Where(x => x.IsDelete == false).ToList();
                foreach (var item in FAQList)
                {
                    if (item.Question!=null)
                    {
                        FAQsListDetailsModel objFAQs = new FAQsListDetailsModel();
                        objFAQs.Answer = item.Answer;
                        objFAQs.Id = item.Id;
                        objFAQs.Question = item.Question;
                        objFAQsList.Add(objFAQs);
                    }
                }
                ObjFAQsListResponseModel.Response.Details = objFAQsList;
                ObjFAQsListResponseModel.Response.Message = "Why Switching List";
                ObjFAQsListResponseModel.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                ObjFAQsListResponseModel.Response.Message = Convert.ToString(ex);
                ObjFAQsListResponseModel.Response.StatusCode = 401;
            }
            return ObjFAQsListResponseModel;
        }
    }
}
