using EntityDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SqueezeBill.Data.DtoModel.DtoRequestModel;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using SqueezeBill.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SqueezeBill.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private SqueezeBillContext _ObjDBContext = new SqueezeBillContext();
        private ILogger<ProductRepository> _log;

        public CompareRatesListResponseModel CompareRatesList(CompareRatesListRequestModel CompareRatesModel)
        {
            CompareRatesListResponseModel objCompareRatesListResponseModel = new CompareRatesListResponseModel();
            try
            {
                var connection = @"Server=180.151.232.92;Database=SqueezeBill; user id=sandbox;password=VnvSan@#18Sky#";
                SqlConnection con = new SqlConnection(connection.ToString());
                SqlCommand cmd = new SqlCommand("CompareRates", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Zipcode", CompareRatesModel.RequestSearch.ZipCode);
                cmd.Parameters.AddWithValue("@IsElectricity", CompareRatesModel.RequestSearch.IsElectricity);
                cmd.Parameters.AddWithValue("@IsGas", CompareRatesModel.RequestSearch.IsGas);
                cmd.Parameters.AddWithValue("@IsResidential", CompareRatesModel.RequestSearch.IsResidential);
                cmd.Parameters.AddWithValue("@IsCommecial", CompareRatesModel.RequestSearch.IsCommercial);
                cmd.Parameters.AddWithValue("@Terms", CompareRatesModel.RequestFilterSearch.Terms);
                cmd.Parameters.AddWithValue("@SupplierName", CompareRatesModel.RequestFilterSearch.SupplierName);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                List<SupplierCompanyList> SCList = new List<SupplierCompanyList>();
                List<RetailerCompanyList> RCList = new List<RetailerCompanyList>();
                if (ds.Tables.Count > 1)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SupplierCompanyList cobj = new SupplierCompanyList();
                        cobj.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                        cobj.Rate = Convert.ToDecimal(ds.Tables[0].Rows[i]["Rate"]);
                        SCList.Add(cobj);
                    }

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        RetailerCompanyList Rc_obj = new RetailerCompanyList();
                        if (ds.Tables[1].Rows[i]["Rate"] != null && ds.Tables[1].Rows[i]["Planid"] != null && ds.Tables[1].Rows[i]["Duration"] != null && ds.Tables[1].Rows[i]["Offer"] != null)
                        {
                            Rc_obj.Planid = Convert.ToInt32(ds.Tables[1].Rows[i]["Planid"]);
                            Rc_obj.Offer = Convert.ToString(ds.Tables[1].Rows[i]["Offer"]);
                            Rc_obj.Duration = Convert.ToInt32(ds.Tables[1].Rows[i]["Duration"]);
                            Rc_obj.Rate = Convert.ToDecimal(ds.Tables[1].Rows[i]["Rate"]);
                            Rc_obj.ImagePath = ds.Tables[1].Rows[i]["ImagePath"].ToString();
                            Rc_obj.RetailerName = ds.Tables[1].Rows[i]["RetailerName"].ToString();
                            Rc_obj.RetailerId = Convert.ToInt32(ds.Tables[1].Rows[i]["RetailerId"]);
                            Rc_obj.IsResidential = Convert.ToBoolean(ds.Tables[1].Rows[i]["IsResidential"]);
                            RCList.Add(Rc_obj);
                        }
                    }
                }
                objCompareRatesListResponseModel.Response.CompareListDetails.RetailerList = RCList;
                objCompareRatesListResponseModel.Response.CompareListDetails.SupplierList = SCList;

                decimal maxID = 0, min = 0;
                if (RCList.Count > 0)
                {
                    maxID = ds.Tables[1].AsEnumerable().Max(r => r.Field<decimal>("Rate"));
                    min = ds.Tables[1].AsEnumerable().Min(r => r.Field<decimal>("Rate"));
                }
                objCompareRatesListResponseModel.Response.CompareListDetails.FutureAnnualSavings = maxID - min;
                objCompareRatesListResponseModel.Response.Message = "Compare Product List";
                objCompareRatesListResponseModel.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                objCompareRatesListResponseModel.Response.Message = Convert.ToString(ex);
                objCompareRatesListResponseModel.Response.StatusCode = 401;
            }
            return objCompareRatesListResponseModel;
        }

        public ZipCodeResponseModel GetZipCodeList()
        {
            ZipCodeResponseModel ObjZipCodeResponseModel = new ZipCodeResponseModel();

            List<ZipCodeModelDetailModel> objZipCodeList = new List<ZipCodeModelDetailModel>();
            try
            {
                var GetUserInfo = _ObjDBContext.ZipCode.ToList();
                foreach (var item in GetUserInfo)
                {
                    if (item.Code != null)
                    {
                        ZipCodeModelDetailModel objZipCode = new ZipCodeModelDetailModel();
                        objZipCode.ZipCode = item.Code;
                        objZipCodeList.Add(objZipCode);
                    }
                }
                ObjZipCodeResponseModel.Response.Details = objZipCodeList;
                ObjZipCodeResponseModel.Response.Message = "Zipcode List";
                ObjZipCodeResponseModel.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                ObjZipCodeResponseModel.Response.Message = Convert.ToString(ex);
                ObjZipCodeResponseModel.Response.StatusCode = 401;
            }
            return ObjZipCodeResponseModel;
        }

        public PlanDetailResponseModel PlanDetails(int PlanId, bool IsResidential)
        {
            PlanDetailResponseModel obj_PlanDetailResponseModel = new PlanDetailResponseModel();
            PlanDetail UserDetails = new PlanDetail();
            List<PlanDetail> List_UserDetails = new List<PlanDetail>();
            try
            {
                //var s= _ObjDBContext.PricePlan.FromSql<PricePlan>("select * from PricePlan");
                //List_UserDetails = _ObjDBContext.Set<PlanDetail>().FromSql("PlanDetailsById @PlanId = {0},@IsResidential = {1}", PlanId, IsResidential).ToList();
                if (IsResidential == true)
                {
                    var QueryIsResidential = (from p in _ObjDBContext.PricePlan
                                              join r in _ObjDBContext.Retailer on p.RetailerId equals r.RetailerId
                                              join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                              where p.Planid == PlanId
                                              orderby p.ResidentialPrice
                                              select new
                                              {
                                                  i.ImagePath,
                                                  r.RetailerName,
                                                  r.KhwM,
                                                  p.ResidentialPrice,
                                                  p.Duration,
                                                  p.Offer,
                                              }).FirstOrDefault();
                    UserDetails.Logo = QueryIsResidential.ImagePath;
                    UserDetails.RetailerName = QueryIsResidential.RetailerName;
                    UserDetails.Per_Kwh = QueryIsResidential.KhwM;
                    UserDetails.Fees = QueryIsResidential.ResidentialPrice;
                    UserDetails.GreenEnergy = "0 %";
                    UserDetails.Duration = QueryIsResidential.Duration;
                    UserDetails.Offers = QueryIsResidential.Offer;
                }
                else
                {
                    var QueryIsNotResidential = (from p in _ObjDBContext.PricePlan
                                                 join r in _ObjDBContext.Retailer on p.RetailerId equals r.RetailerId
                                                 join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                 where p.Planid == PlanId
                                                 orderby p.CommercialPrice
                                                 select new
                                                 {
                                                     i.ImagePath,
                                                     r.RetailerName,
                                                     r.KhwM,
                                                     p.CommercialPrice,
                                                     p.Duration,
                                                     p.Offer,
                                                 }).FirstOrDefault();
                    UserDetails.Logo = QueryIsNotResidential.ImagePath;
                    UserDetails.RetailerName = QueryIsNotResidential.RetailerName;
                    UserDetails.Per_Kwh = QueryIsNotResidential.KhwM;
                    UserDetails.Fees = QueryIsNotResidential.CommercialPrice;
                    UserDetails.GreenEnergy = "0 %";
                    UserDetails.Duration = QueryIsNotResidential.Duration;
                    UserDetails.Offers = QueryIsNotResidential.Offer;

                }
                obj_PlanDetailResponseModel.Response.Details = UserDetails;
                obj_PlanDetailResponseModel.Response.Message = "Plan Details";
                obj_PlanDetailResponseModel.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                obj_PlanDetailResponseModel.Response.Message = Convert.ToString(ex);
                obj_PlanDetailResponseModel.Response.StatusCode = 401;
            }
            return obj_PlanDetailResponseModel;
        }

        public CompareRatesRetailerListResponseModel CompareRatesRetailerList(Search CompareRatesModel, string SupplierCompanyName, int TermsFrom, int TermsTo)
        {
            CompareRatesRetailerListResponseModel objCompareRatesRetailerListResponseModel = new CompareRatesRetailerListResponseModel();
            List<CRRetailerCompanyList> ObjRetailerList = new List<CRRetailerCompanyList>();
            try
            {
                var ZipCodeId = _ObjDBContext.ZipCode.Where(s => s.Code == CompareRatesModel.ZipCode).FirstOrDefault();
                var GetSupplierCompanyId = _ObjDBContext.Company.Where(s => s.ZipCode == ZipCodeId.ZipCodeid && s.CompanyName==SupplierCompanyName).FirstOrDefault();
                if (ZipCodeId != null && GetSupplierCompanyId!=null)
                {
                    if (CompareRatesModel.RetailerName_CompareRatesRetailerOnly==null)
                    {
                        if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsElectricity == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsCommecial == true && s.IsElectricity == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                        else if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsGas == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsCommecial == true && s.IsGas == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                        else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsElectricity == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsResidential == true && s.IsElectricity == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                        else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsGas == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsResidential == true && s.IsGas == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }

                    }
                    else
                    {
                        if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsElectricity == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsCommecial == true && s.IsElectricity == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo && s.RetailerName==CompareRatesModel.RetailerName_CompareRatesRetailerOnly);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                        else if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsGas == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsCommecial == true && s.IsGas == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo && s.RetailerName == CompareRatesModel.RetailerName_CompareRatesRetailerOnly);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                        else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsElectricity == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsResidential == true && s.IsElectricity == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo && s.RetailerName == CompareRatesModel.RetailerName_CompareRatesRetailerOnly);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                        else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsGas == true)
                        {
                            var GetRetialerList = (from r in _ObjDBContext.Retailer
                                                   join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                                   join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                                   //orderby od.OrderID
                                                   select new
                                                   {
                                                       r.ComapnyId,
                                                       r.RetailerId,
                                                       r.RetailerName,
                                                       i.ImagePath,
                                                       p.ResidentialPrice,
                                                       p.CommercialPrice,
                                                       p.Duration,
                                                       p.Offer,
                                                       p.Planid,
                                                       r.IsActive,
                                                       r.IsDeleted,
                                                       r.IsCommecial,
                                                       r.IsResidential,
                                                       r.IsGas,
                                                       r.IsElectricity,
                                                   }).ToList().Where(s => s.IsActive == true && s.IsDeleted == false && s.IsResidential == true && s.IsGas == true && s.ComapnyId == GetSupplierCompanyId.CompanyId && s.Duration >= TermsFrom && s.Duration <= TermsTo && s.RetailerName == CompareRatesModel.RetailerName_CompareRatesRetailerOnly);

                            foreach (var item in GetRetialerList)
                            {
                                if (item.Planid != null && item.Offer != null && item.Duration != null && item.ImagePath != null && item.RetailerName != null && item.RetailerId != null && item.CommercialPrice != null)
                                {
                                    CRRetailerCompanyList ObjRetailer = new CRRetailerCompanyList();
                                    ObjRetailer.Planid = Convert.ToInt32(item.Planid);
                                    ObjRetailer.Offer = Convert.ToString(item.Offer);
                                    ObjRetailer.Duration = Convert.ToInt32(item.Duration);
                                    ObjRetailer.ImagePath = Convert.ToString(item.ImagePath);
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailer.RetailerId = Convert.ToInt32(item.RetailerId);

                                    if (CompareRatesModel.IsCommercial == true)
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.CommercialPrice);
                                        ObjRetailer.IsResidential = false;
                                    }
                                    else
                                    {
                                        ObjRetailer.Rate = Convert.ToDecimal(item.ResidentialPrice);
                                        ObjRetailer.IsResidential = true;
                                    }
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                    }
                    objCompareRatesRetailerListResponseModel.Response.RetailerCompareListDetails = ObjRetailerList;
                    objCompareRatesRetailerListResponseModel.Response.Message = "Retailer List";
                    objCompareRatesRetailerListResponseModel.Response.StatusCode = 200;
                }
                else
                {
                    objCompareRatesRetailerListResponseModel.Response.RetailerCompareListDetails = ObjRetailerList;
                    objCompareRatesRetailerListResponseModel.Response.Message = "No Data Found in Retailer List";
                    objCompareRatesRetailerListResponseModel.Response.StatusCode = 401;
                }
                    
            }
            catch (Exception ex)
            {
                objCompareRatesRetailerListResponseModel.Response.Message = Convert.ToString(ex);
                objCompareRatesRetailerListResponseModel.Response.StatusCode = 401;
            }
            return objCompareRatesRetailerListResponseModel;
        }

        public CompareRatesSupplierListResponseModel CompareRatesSupplierList(Search CompareRatesModel)
        {
            CompareRatesSupplierListResponseModel objCompareRatesSupplierListResponseModel = new CompareRatesSupplierListResponseModel();
            List<CRSupplierCompanyList> ObjListCRSupplier = new List<CRSupplierCompanyList>();
            try
            {
                var ZipCodeId = _ObjDBContext.ZipCode.Where(s => s.Code == CompareRatesModel.ZipCode).FirstOrDefault();
                if (ZipCodeId != null)
                {

                    if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsElectricity == true)
                    {
                        var GetSupplierList = _ObjDBContext.Company.Where(s => s.IsCommecial == true && s.IsElectricity == true && s.ZipCode == ZipCodeId.ZipCodeid).ToList();
                        foreach (var item in GetSupplierList)
                        {
                            CRSupplierCompanyList ObjSupplier = new CRSupplierCompanyList();
                            ObjSupplier.CompanyName = item.CompanyName;
                            ObjSupplier.Rate = item.CommecialRate;
                            ObjListCRSupplier.Add(ObjSupplier);
                        }
                    }
                    else if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsGas == true)
                    {
                        var GetSupplierList = _ObjDBContext.Company.Where(s => s.IsCommecial == true && s.IsGas == true && s.ZipCode == ZipCodeId.ZipCodeid).ToList();
                        foreach (var item in GetSupplierList)
                        {
                            CRSupplierCompanyList ObjSupplier = new CRSupplierCompanyList();
                            ObjSupplier.CompanyName = item.CompanyName;
                            ObjSupplier.Rate = item.CommecialRate;
                            ObjListCRSupplier.Add(ObjSupplier);
                        }
                    }
                    else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsElectricity == true)
                    {
                        var GetSupplierList = _ObjDBContext.Company.Where(s => s.IsResidential == true && s.IsElectricity == true && s.ZipCode == ZipCodeId.ZipCodeid).ToList();
                        foreach (var item in GetSupplierList)
                        {
                            CRSupplierCompanyList ObjSupplier = new CRSupplierCompanyList();
                            ObjSupplier.CompanyName = item.CompanyName;
                            ObjSupplier.Rate = item.ResidentialRate;
                            ObjListCRSupplier.Add(ObjSupplier);
                        }
                    }
                    else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsGas == true)
                    {
                        var GetSupplierList = _ObjDBContext.Company.Where(s => s.IsResidential == true && s.IsGas == true && s.ZipCode == ZipCodeId.ZipCodeid).ToList();
                        foreach (var item in GetSupplierList)
                        {
                            CRSupplierCompanyList ObjSupplier = new CRSupplierCompanyList();
                            ObjSupplier.CompanyName = item.CompanyName;
                            ObjSupplier.Rate = item.ResidentialRate;
                            ObjListCRSupplier.Add(ObjSupplier);
                        }
                    }
                    if (ObjListCRSupplier.Count > 0)
                    {
                        objCompareRatesSupplierListResponseModel.Response.SupplierCompareListDetails = ObjListCRSupplier;
                        objCompareRatesSupplierListResponseModel.Response.Message = "Supplier List";
                        objCompareRatesSupplierListResponseModel.Response.StatusCode = 200;
                    }
                    else
                    {
                        objCompareRatesSupplierListResponseModel.Response.SupplierCompareListDetails = ObjListCRSupplier;
                        objCompareRatesSupplierListResponseModel.Response.Message = "Not Found Supplier List";
                        objCompareRatesSupplierListResponseModel.Response.StatusCode = 401;
                    }
                }
                else
                {
                    objCompareRatesSupplierListResponseModel.Response.SupplierCompareListDetails = ObjListCRSupplier;
                    objCompareRatesSupplierListResponseModel.Response.Message = "Zip Code Incorrect";
                    objCompareRatesSupplierListResponseModel.Response.StatusCode = 401;
                }
            }
            catch (Exception ex)
            {
                objCompareRatesSupplierListResponseModel.Response.Message = Convert.ToString(ex);
                objCompareRatesSupplierListResponseModel.Response.StatusCode = 401;
            }
            return objCompareRatesSupplierListResponseModel; ;
        }

        public FiltertedRetailerListResponseModel FilterRetailerList(Search CompareRatesModel, string SupplierCompanyName)
        {
            FiltertedRetailerListResponseModel objCompareRatesRetailerListResponseModel = new FiltertedRetailerListResponseModel();
            List<FiltertedRetailerCompanyList> ObjRetailerList = new List<FiltertedRetailerCompanyList>();
            try
            {
                var ZipCodeId = _ObjDBContext.ZipCode.Where(s => s.Code == CompareRatesModel.ZipCode).FirstOrDefault();
                var GetSupplierCompanyId = _ObjDBContext.Company.Where(s => s.ZipCode == ZipCodeId.ZipCodeid && s.CompanyName == SupplierCompanyName).FirstOrDefault();
                if (ZipCodeId != null && GetSupplierCompanyId != null)
                {
                    if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsElectricity == true)
                    {
                        var GetRetialerList = (from r in _ObjDBContext.Retailer
                                               join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                               join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                               //orderby od.OrderID
                                               select new
                                               {
                                                   r.ComapnyId,
                                                   r.RetailerName,
                                                   r.IsCommecial,
                                                   r.IsResidential,
                                                   r.IsGas,
                                                   r.IsElectricity,
                                               }).ToList().Where(s => s.IsCommecial == true && s.IsElectricity == true && s.ComapnyId == GetSupplierCompanyId.CompanyId);

                        foreach (var item in GetRetialerList)
                        {
                            if (item.RetailerName != null)
                            {
                                var RetailerExist = ObjRetailerList.Where(s => s.RetailerName == item.RetailerName).FirstOrDefault();
                                if (RetailerExist==null)
                                {
                                    FiltertedRetailerCompanyList ObjRetailer = new FiltertedRetailerCompanyList();
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailerList.Add(ObjRetailer);
                                }                           
                            }
                        }
                    }
                    else if (CompareRatesModel.IsCommercial == true && CompareRatesModel.IsGas == true)
                    {
                        var GetRetialerList = (from r in _ObjDBContext.Retailer
                                               join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                               join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                               //orderby od.OrderID
                                               select new
                                               {
                                                   r.ComapnyId,
                                                   r.RetailerName,
                                                   r.IsCommecial,
                                                   r.IsResidential,
                                                   r.IsGas,
                                                   r.IsElectricity,
                                               }).ToList().Where(s => s.IsCommecial == true && s.IsGas == true && s.ComapnyId == GetSupplierCompanyId.CompanyId);

                        foreach (var item in GetRetialerList)
                        {
                            if (item.RetailerName != null)
                            {
                                var RetailerExist = ObjRetailerList.Where(s => s.RetailerName == item.RetailerName).FirstOrDefault();
                                if (RetailerExist == null)
                                {
                                    FiltertedRetailerCompanyList ObjRetailer = new FiltertedRetailerCompanyList();
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                    }
                    else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsElectricity == true)
                    {
                        var GetRetialerList = (from r in _ObjDBContext.Retailer
                                               join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                               join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                               //orderby od.OrderID
                                               select new
                                               {
                                                   r.ComapnyId,
                                                   r.RetailerName,
                                                   r.IsCommecial,
                                                   r.IsResidential,
                                                   r.IsGas,
                                                   r.IsElectricity,
                                               }).ToList().Where(s => s.IsResidential == true && s.IsElectricity == true && s.ComapnyId == GetSupplierCompanyId.CompanyId);

                        foreach (var item in GetRetialerList)
                        {
                            if (item.RetailerName != null)
                            {
                                var RetailerExist = ObjRetailerList.Where(s => s.RetailerName == item.RetailerName).FirstOrDefault();
                                if (RetailerExist == null)
                                {
                                    FiltertedRetailerCompanyList ObjRetailer = new FiltertedRetailerCompanyList();
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                    }
                    else if (CompareRatesModel.IsResidential == true && CompareRatesModel.IsGas == true)
                    {
                        var GetRetialerList = (from r in _ObjDBContext.Retailer
                                               join i in _ObjDBContext.Image on r.LogoId equals i.ImageId
                                               join p in _ObjDBContext.PricePlan on r.RetailerId equals p.RetailerId
                                               //orderby od.OrderID
                                               select new
                                               {
                                                   r.ComapnyId,
                                                   r.RetailerName,
                                                   r.IsCommecial,
                                                   r.IsResidential,
                                                   r.IsGas,
                                                   r.IsElectricity,
                                               }).ToList().Where(s => s.IsResidential == true && s.IsGas == true && s.ComapnyId == GetSupplierCompanyId.CompanyId);

                        foreach (var item in GetRetialerList)
                        {
                            if (item.RetailerName != null)
                            {
                                var RetailerExist = ObjRetailerList.Where(s => s.RetailerName == item.RetailerName).FirstOrDefault();
                                if (RetailerExist == null)
                                {
                                    FiltertedRetailerCompanyList ObjRetailer = new FiltertedRetailerCompanyList();
                                    ObjRetailer.RetailerName = Convert.ToString(item.RetailerName);
                                    ObjRetailerList.Add(ObjRetailer);
                                }
                            }
                        }
                    }
                    objCompareRatesRetailerListResponseModel.Response.FilteredRetailerObj = ObjRetailerList;
                    objCompareRatesRetailerListResponseModel.Response.Message = "Retailer List";
                    objCompareRatesRetailerListResponseModel.Response.StatusCode = 200;
                }
                else
                {
                    objCompareRatesRetailerListResponseModel.Response.FilteredRetailerObj = ObjRetailerList;
                    objCompareRatesRetailerListResponseModel.Response.Message = "No Data Found in Retailer List";
                    objCompareRatesRetailerListResponseModel.Response.StatusCode = 401;
                }

            }
            catch (Exception ex)
            {
                objCompareRatesRetailerListResponseModel.Response.Message = Convert.ToString(ex);
                objCompareRatesRetailerListResponseModel.Response.StatusCode = 401;
            }
            return objCompareRatesRetailerListResponseModel;
        }
    }
}

