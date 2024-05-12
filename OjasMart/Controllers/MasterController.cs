using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;
using System.IO;
using System.Collections;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data.SqlClient;

namespace OjasMart.Controllers
{
    public class MasterController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: Master
        public ActionResult CreateItemGroup()
        {
            objp.Action = "2";
            objp.dt = objL.InsertUpdateItemGroupMaster(objp, "Proc_InsertUpdateItemGroup");
            return View(objp);
        }
        //public ActionResult CreateItemHead()
        //{
        //    ViewBag.UOMList = PropertyClass.BindDDL(objL.BindUOMDropDown());
        //    ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
        //    objp.Action = "1";
        //    objp.dt = objL.GetItemHeadDetails(objp, "Proc_GetItemHeadDetails");
        //    return View(objp);
        //}

        public ActionResult CreateItemHead(string AccountType)
        {
            string CompanyCode = null;
            if (Convert.ToString(Session["Role"]) == "2")
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
            }
            else if (Convert.ToString(Session["Role"]) == "1")
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
            }
            else
            {
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
            }

            if (Convert.ToString(Session["Role"]) == "3")
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
            }

            ViewBag.UOMList = PropertyClass.BindDDL(objL.BindUOMDropDown());
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown(objp.CompanyCode));
            DataTable dta = objL.binditemnew(CompanyCode);
            objp.ActiveStatus = ConvertTableToList(dta);
            ViewBag.itemlist = PropertyClass.BindDDL(objL.binditemnew(CompanyCode));
            objp.Action = "1";

            objp.AccountType = AccountType;

            objp.dt = objL.GetItemHeadDetails(objp, "Proc_GetItemHeadDetails");
            return View(objp);
        }
        public static string ConvertTableToList(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Hashtable[] pr = new Hashtable[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Hashtable ch = new Hashtable();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string columnName = Convert.ToString(dt.Columns[j]);
                        string columnValue = Convert.ToString(dt.Rows[i][columnName]);
                        ch.Add(columnName, columnValue);
                    }
                    pr[i] = ch;
                }
                return new JavaScriptSerializer().Serialize(pr);
            }
            return "False";
        }
        public JsonResult InsertItemHead(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.MfgDate = Convert.ToDateTime(p.mDate);
                p.ExpiryDate = Convert.ToDateTime(p.eDate);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);
                //p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "2" || Convert.ToString(Session["Role"]) == "3")
                {
                    p.BranchCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.BranchCode = Convert.ToString(Session["CompanyCode"]);
                }
                p.Role = Convert.ToString(Session["Role"]);
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("ItemCode");
                dt1.Columns.Add("Qty");
                if (p.BundleList != null)
                {
                    foreach (var item in p.BundleList)
                    {
                        dt1.Rows.Add(item.ItemCode, item.InitialQty);
                    }
                }
                DataTable dt = objL.InsertItemHead(p, "proc_InsertItemHead", dt1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemHeadDetailsForEdit(string ItemCode1)
        {
            try
            {
                objp.Action = "2";
                objp.ItemCode = ItemCode1.Trim();
                DataTable dt = objL.GetItemHeadDetails(objp, "Proc_GetItemHeadDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.ItemCode = dt.Rows[0]["ItemCode"].ToString();
                    objp.ItemName = dt.Rows[0]["ItemName"].ToString();
                    objp.BatchNo = dt.Rows[0]["BatchNo"].ToString();
                    objp.HSNCode = dt.Rows[0]["HSNCode"].ToString();
                    objp.MRP = dt.Rows[0]["MRP"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["MRP"].ToString()) : 0;
                    objp.GSTPer = dt.Rows[0]["GSTPer"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["GSTPer"].ToString()) : 0;
                    objp.CGSTPer = dt.Rows[0]["CGSTPer"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["CGSTPer"].ToString()) : 0;
                    objp.SGSTPer = dt.Rows[0]["SGSTPer"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["SGSTPer"].ToString()) : 0;
                    if (dt.Rows[0]["Mfg"].ToString() != "01-Jan-1900")
                    {
                        objp.mDate = dt.Rows[0]["Mfg"].ToString();
                    }
                    if (dt.Rows[0]["Exp"].ToString() != "01-Jan-1900")
                    {
                        objp.eDate = dt.Rows[0]["Exp"].ToString();
                    }
                    objp.ItemBarCode = dt.Rows[0]["ItemBarCode"].ToString();
                    objp.PurchaseRate_Bulk = dt.Rows[0]["PurchaseRate_Bulk"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["PurchaseRate_Bulk"].ToString()) : 0;
                    objp.PurchaseRate_Loose = dt.Rows[0]["PurchaseRate_Loose"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["PurchaseRate_Loose"].ToString()) : 0;
                    objp.SaleRate_Bulk = dt.Rows[0]["SaleRate_Bulk"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["SaleRate_Bulk"].ToString()) : 0;
                    objp.SaleRate_Loose = dt.Rows[0]["SaleRate_Loose"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["SaleRate_Loose"].ToString()) : 0;
                    objp.StorePrice = dt.Rows[0]["StorePrice"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["StorePrice"].ToString()) : 0;
                    objp.OnlinePrice = dt.Rows[0]["OnlinePrice"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["OnlinePrice"].ToString()) : 0;
                    objp.BulkUOM = dt.Rows[0]["BulkUOM"].ToString();
                    objp.LooseUOM = dt.Rows[0]["LooseUOM"].ToString();
                    objp.BulkUOMQty = dt.Rows[0]["BulkUOMQty"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["BulkUOMQty"].ToString()) : 0;
                    objp.GroupCode = dt.Rows[0]["ItemGroup"].ToString();

                    objp.Purchase_taxIncludeExclude = dt.Rows[0]["Purchase_TaxIncludeExclude"].ToString();
                    objp.Sale_taxIncludeExclude = dt.Rows[0]["Sale_TaxIncludeExclude"].ToString();

                    objp.InitialStockQty = dt.Rows[0]["InitialStockQty"].ToString();
                    objp.LowStockAlert = dt.Rows[0]["LowStockAlert"].ToString();

                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemGroupDetailsForEdit(string ItemCode1)
        {
            try
            {
                objp.Action = "4";
                objp.GroupCode = ItemCode1.Trim();
                DataTable dt = objL.InsertUpdateItemGroupMaster(objp, "Proc_InsertUpdateItemGroup");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.GroupCode = dt.Rows[0]["SrNo"].ToString();
                    objp.GroupName = dt.Rows[0]["ItemGroupName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult CreateAccountGroup()
        //{
        //    ViewBag.GroupList = PropertyClass.BindDDL(objL.BindAccountGroupDLL());
        //    return View(objp);
        //}
        public ActionResult CreateAccountGroup()
        {
            try
            {
                ViewBag.GroupList = PropertyClass.BindDDL(objL.BindAccountGroupDLL());
                objp.FinancialYear = objL.GetCurrentFinancialYear();
                objp.dt = objL.BindCloseUnderGroups();
                objp.Action = "1";
                objp.dt1 = objL.AccountGroupMaster(objp, "proc_AccountGroupMaster");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertAccountGroup(PropertyClass p)
        {
            string msg = "";
            try
            {
                //p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                p.EntryBy = Convert.ToString(Session["UserName"]);
                //p.Action = "2";
                DataTable dt = objL.AccountGroupMaster(p, "proc_AccountGroupMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccGroupDetailsForEdit(string GroupCode)
        {
            try
            {
                objp.Action = "3";
                objp.GroupCode = GroupCode.Trim();
                DataTable dt = objL.AccountGroupMaster(objp, "proc_AccountGroupMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.GroupCode = dt.Rows[0]["group_code"].ToString();
                    objp.GroupName = dt.Rows[0]["group_name"].ToString();
                    objp.parent_groupcode = dt.Rows[0]["PrntCode"].ToString();
                    objp.close_to = dt.Rows[0]["close_to"].ToString();
                    //objp.FinancialYear = dt.Rows[0]["FinancialYear"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateAccountHead()
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                ViewBag.GroupList = PropertyClass.BindDDL(objL.BindAccountGroupDLLAll());
                ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
                objp.FinancialYear = objL.GetCurrentFinancialYear();
                objp.Action = "1";
                objp.dt1 = objL.AccountHeadMaster(objp, "proc_Accounthead");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertAccountHead(PropertyClass p)
        {
            string msg = "";
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                p.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.AccountHeadMaster(p, "proc_Accounthead");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccHeadDetailsForEdit(string GroupCode)
        {
            try
            {
                objp.Action = "3";
                objp.AccountCode = GroupCode.Trim();
                DataTable dt = objL.AccountHeadMaster(objp, "proc_Accounthead");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.AccountCode = dt.Rows[0]["account_code"].ToString();
                    objp.GroupCode = dt.Rows[0]["group_code"].ToString();
                    objp.AccountName = dt.Rows[0]["account_name"].ToString();
                    objp.PanNo = dt.Rows[0]["pan"].ToString();
                    objp.CoupenAmount = dt.Rows[0]["opening_amt"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["opening_amt"].ToString()) : 0;
                    objp.Address = dt.Rows[0]["address"].ToString();
                    objp.CityName = dt.Rows[0]["city"].ToString();
                    objp.ContactNo = dt.Rows[0]["mobile"].ToString();
                    objp.EmailAddress = dt.Rows[0]["email"].ToString();
                    objp.GstNo = dt.Rows[0]["GSTNo"].ToString();
                    objp.accountno = dt.Rows[0]["AccountNo"].ToString();
                    objp.ifsccode = dt.Rows[0]["IFSCCode"].ToString();
                    objp.branchname = dt.Rows[0]["Branch"].ToString();
                    objp.StCode = dt.Rows[0]["StateCode"].ToString();
                    objp.BanKAccName = dt.Rows[0]["BankName"].ToString();
                    objp.mDate = dt.Rows[0]["EffectDate"].ToString();
                    objp.OfferType = dt.Rows[0]["opening_type"].ToString();
                    //objp.FinancialYear = dt.Rows[0]["FinancialYear"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertItemGroup(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);
                DataTable dt = objL.InsertUpdateItemGroupMaster(p, "Proc_InsertUpdateItemGroup");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //vandana pandey

       
        #region Size Manager 

        public ActionResult Size()
        {
            try
            {

                objp.Action = "3";
                objp.dtSize = objL.mst_SaveUpdate_Size(objp);

            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public JsonResult Insert_size(PropertyClass objT)
        {
            try
            {
                objT.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.mst_SaveUpdate_Size(objT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objT.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    objT.msg = Convert.ToString(dt.Rows[0]["msg"]);
                }
                else
                {
                    objT.Id = 0;
                    objT.msg = "Something went wrong!!";
                }

            }
            catch (Exception ex)
            {
                objT.Id = 0;
                objT.msg = "Something went wrong!!";
            }
            return Json(objT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetsizeDetails(int Id)
        {
            try
            {

                DataTable dt = new DataTable();
                objp.Action = "3";
                objp.Id = Id;
                dt = objL.mst_SaveUpdate_Size(objp);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    objp.Size_Name = dt.Rows[0]["Size_Name"].ToString();
                    objp.Size_Value = dt.Rows[0]["Size_Value"].ToString();
                }

            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public ActionResult Color()
        {
            try
            {
                objp.Action = "3";
                objp.dtColor = objL.mst_SaveUpdate_Colour(objp);
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult Insert_Color(PropertyClass objT)
        {
            try
            {
                objT.EntryBy = Convert.ToString(Session["UserName"]);

                DataTable dt = objL.mst_SaveUpdate_Colour(objT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    objT.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    objT.msg = Convert.ToString(dt.Rows[0]["msg"]);
                }
                else
                {
                    objT.Id = 0;
                    objT.msg = "Something went wrong!!";
                }

            }
            catch (Exception ex)
            {
                objT.Id = 0;
                objT.msg = "Something went wrong!!";
            }
            return Json(objT, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetcolorDetails(int Id)
        {
            try
            {
                int cid = 0;

                DataTable dt = new DataTable();
                objp.Action = "3";
                objp.Id = Id;
                dt = objL.mst_SaveUpdate_Colour(objp);
                if (dt != null && dt.Rows.Count > 0)
                {

                    objp.Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    objp.ColorName = dt.Rows[0]["ColorName"].ToString();
                    objp.ColorValue = dt.Rows[0]["ColorValue"].ToString();

                }

            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddUOM()
        {
            try
            {
                objp.Action = "2";
                objp.dt = objL.InsertUpdateUOMpMaster(objp, "Proc_InsertUpdateUOM");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertUOMDetails(PropertyClass p)
        {
            string msg = "";
            try
            {
                DataTable dt = objL.InsertUpdateUOMpMaster(p, "Proc_InsertUpdateUOM");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUOMDetailsForEdit(string ItemCode1)
        {
            try
            {
                objp.Action = "4";
                objp.ItemCode = ItemCode1.Trim();
                DataTable dt = objL.InsertUpdateUOMpMaster(objp, "Proc_InsertUpdateUOM");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.ItemCode = dt.Rows[0]["UnitCode"].ToString();
                    objp.UOM = dt.Rows[0]["UnitName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DailyDeals(DaliyDeal p)
        {
            try
            {

                p.Action = "2";
                objp.dt = objL.InsertUpdateDailyDealspMaster(p, "Proc_InsertUpdateDailyDeals");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertDailyDealsDetails(DaliyDeal p)
        {
            string msg = "";
            try
            {
                p.Entryby = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.InsertUpdateDailyDealspMaster(p, "Proc_InsertUpdateDailyDeals");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDailyDealsDetailsForEdit(DaliyDeal p)
        {
            try
            {
                p.Action = "4";     
                DataTable dt = objL.InsertUpdateDailyDealspMaster(p, "Proc_InsertUpdateDailyDeals");
                if (dt != null && dt.Rows.Count > 0)
                {
                    p.DealName = dt.Rows[0]["DealName"].ToString();
                    p.ToTime = dt.Rows[0]["ToTime"].ToString();
                    p.FromTime = dt.Rows[0]["FromTime"].ToString();
                    p.Date = dt.Rows[0]["Date"].ToString();
                    p.DisPrice = dt.Rows[0]["DisPrice"].ToString();
                    p.dId = dt.Rows[0]["did"].ToString();

                 }
            }
            catch (Exception ex)
            {

            }
            return Json(p, JsonRequestBehavior.AllowGet);
        }


        public ActionResult WalletRecharge()
        {
            if (Session["Role"].ToString() == "4")
            {
                DataTable dt = objL.GetCustomerMobile(Convert.ToString(Session["userName"]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.ContactNo = dt.Rows[0]["MobileNo"].ToString();
                    objp.SSName = dt.Rows[0]["Name"].ToString();
                    objp.CustomerId = dt.Rows[0]["CustomerId"].ToString();
                }
            }
            return View(objp);
        }
        public JsonResult DeleteItemHeadDetails(string ItemCode1)
        {
            try
            {
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.ItemCode = ItemCode1.Trim();
                DataTable dt = objL.DeleteItemHeadDetails(objp, "Proc_DeleteItemHead");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItemGroupDetails(string ItemCode1)
        {
            try
            {
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.ItemCode = ItemCode1.Trim();
                DataTable dt = objL.DeleteItemHeadDetails(objp, "Proc_DeleteItemGroup");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUOMDetails(string ItemCode1)
        {
            try
            {
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.ItemCode = ItemCode1.Trim();
                DataTable dt = objL.DeleteItemHeadDetails(objp, "Proc_DeleteUOM");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateOffer()
        {
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
            ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
            objp.Action = "1";
            objp.dt = objL.GetOfferDetails(objp, "Proc_GetOfferDetails");
            return View(objp);
        }
        public JsonResult InsertOfferDetails(PropertyClass p)
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == "1")
                {
                    p.BranchCode = Convert.ToString(Session["CompanyCode"]);
                }
                else
                {
                    p.BranchCode = Convert.ToString(Session["UserName"]);
                }

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var fileName = Path.GetFileName(mainPic.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string myfile = fname + fileExt;
                    var path = Path.Combine(Server.MapPath("~/BannerImages/"), myfile);
                    mainPic.SaveAs(path);
                    p.PurchaseFile = myfile;
                }


                p.EntryBy = Convert.ToString(Session["StaffCode"]);
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);

                DataTable dt = objL.InsertOfferMaster(p, "proc_InsertOfferMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteOfferDetails(string ItemCode1)
        {
            try
            {
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.RespoCode = ItemCode1.Trim();
                objp.ValidStartDate = Convert.ToDateTime("01/01/1900");
                objp.ValidEndDate = Convert.ToDateTime("01/01/1900");
                objp.Action = "2";
                DataTable dt = objL.InsertOfferMaster(objp, "proc_InsertOfferMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOfferDetailsForEdit(string OfferId)
        {
            try
            {
                objp.Action = "2";
                objp.RespoCode = OfferId.Trim();
                DataTable dt = objL.GetOfferDetails(objp, "Proc_GetOfferDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.OfferTitle = dt.Rows[0]["OfferTitle"].ToString();
                    objp.OfferType = dt.Rows[0]["OfferType"].ToString();

                    objp.ItemCode = dt.Rows[0]["ItemCode"].ToString();
                    objp.OnPurchaseAmount = dt.Rows[0]["PurchaseAmount"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["PurchaseAmount"].ToString()) : 0;
                    objp.CashBackAmount = dt.Rows[0]["CashBackAmount"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["CashBackAmount"].ToString()) : 0;
                    objp.Points = dt.Rows[0]["Points"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["Points"].ToString()) : 0;
                    objp.AmountPerPoint = dt.Rows[0]["AmountPerPoint"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["AmountPerPoint"].ToString()) : 0;
                    if (dt.Rows[0]["validForm"].ToString() != "01-Jan-1900")
                    {
                        objp.mDate = dt.Rows[0]["validForm"].ToString();
                    }
                    if (dt.Rows[0]["validEnd"].ToString() != "01-Jan-1900")
                    {
                        objp.eDate = dt.Rows[0]["validEnd"].ToString();
                    }
                    objp.RespoCode = dt.Rows[0]["OfferCode"].ToString();
                    objp.Status = dt.Rows[0]["isFirstPurchase"].ToString();

                    objp.DiscPer = dt.Rows[0]["DiscountValue"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["DiscountValue"].ToString()) : 0;
                    objp.AccountType = dt.Rows[0]["OfferFor"].ToString();
                    objp.CardType = dt.Rows[0]["DiscountType"].ToString();
                    objp.PurchaseFile = dt.Rows[0]["ImgFile"].ToString();
                    objp.ItemBarCode = dt.Rows[0]["PromoCode"].ToString();

                    decimal ApplyMRPFrom = 0, ApplyMRPTo = 0;
                    decimal.TryParse(Convert.ToString(dt.Rows[0]["ApplyMRPFrom"].ToString()), out ApplyMRPFrom);
                    decimal.TryParse(Convert.ToString(dt.Rows[0]["ApplyMRPTo"].ToString()), out ApplyMRPTo);

                    objp.ApplyMRPFrom = ApplyMRPFrom;
                    objp.ApplyMRPTo = ApplyMRPTo;

                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BannerMaster()
        {
            try
            {
                ViewBag.CategoryList = PropertyClass.BindDDL(objL.BindAllCategory());
                objp.Action = "2";
                objp.dt = objL.BannerMaster(objp, "proc_BannerMaster");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertBannerDetails(PropertyClass p)
        {
            try
            {

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var fileName = Path.GetFileName(mainPic.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string myfile = fname + fileExt;
                    var path = Path.Combine(Server.MapPath("~/BannerImages/"), myfile);
                    mainPic.SaveAs(path);
                    p.PurchaseFile = myfile;
                }

                p.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt1 = objL.BannerMaster(p, "proc_BannerMaster");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.strId = dt1.Rows[0]["id"].ToString();
                    objp.msg = dt1.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.strId = "0";
                }


            }
            catch (Exception ex)
            {
                objp.strId = "2";
                objp.msg = ex.Message;
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BannerMaster(string imageData, string OfferTitle, string MainCategoryCode, int DiscPer, string CardType) // Changed parameter name to imageData
        {
            PropertyClass pl = new PropertyClass();

            try
            {
                // Connection string from web.config
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultNewCon"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL query to call proc_BannerMaster stored procedure with Action = 1
                    string query = "proc_BannerMaster";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        command.CommandType = CommandType.StoredProcedure;
                        // Parameterized query to prevent SQL injection
                        command.Parameters.AddWithValue("@ImageUrl", imageData); // Using @ImageUrl as parameter value
                        command.Parameters.AddWithValue("@Action", "1");
                        command.Parameters.AddWithValue("@BannerType", "TopHeader"); // Assuming "BannerType" is the name of the form field
                        command.Parameters.AddWithValue("@BannerTitle", OfferTitle); // Assuming "BannerTitle" is the name of the form field
                        command.Parameters.AddWithValue("@DiscountType", CardType); // Assuming "DiscountType" is the name of the form field
                        command.Parameters.AddWithValue("@DiscountValue", DiscPer); // Assuming "DiscountValue" is the name of the form field
                        command.Parameters.AddWithValue("@CategoryId", MainCategoryCode); // Assuming "CategoryId" is the name of the form field
                        command.Parameters.AddWithValue("@EntryBy", Request.Form["EntryBy"]); // Assuming "EntryBy" is the name of the form field
                        command.Parameters.AddWithValue("@RefId", Request.Form["RefId"]); // Assuming "RefId" is the name of the form field
                        // Open the connection
                        connection.Open();

                        // Execute the query
                        command.ExecuteNonQuery();
                    }
                }

                // Return success message
                return Content("Image URL inserted successfully!");
            }
            catch (Exception ex)
            {
                // Log error or handle it as per your requirement
                return Content("Error inserting image URL: " + ex.Message);
            }
        }

        public JsonResult GetBannerDetailsForEdit(string ItemCode1)
        {
            try
            {
                objp.Action = "3";
                objp.RespoCode = ItemCode1.Trim();
                DataTable dt = objL.BannerMaster(objp, "proc_BannerMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.RespoCode = dt.Rows[0]["SrNo"].ToString();
                    objp.OfferType = dt.Rows[0]["BannerType"].ToString();
                    objp.OfferTitle = dt.Rows[0]["Bannertitle"].ToString();
                    objp.CardType = dt.Rows[0]["DiscountType"].ToString();
                    objp.todayPoAmt = dt.Rows[0]["DiscountValue"].ToString();
                    objp.MainCategoryCode = dt.Rows[0]["CategoryId"].ToString();
                    objp.PurchaseFile = dt.Rows[0]["BannerImage"].ToString();
                    objp.Url = dt.Rows[0]["BannerImg"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogMaster()
        {
            try
            {
                Blogmaster obj = new Blogmaster();
                obj.Action = 2;
                objp.dt = objL.BlogMaster(obj, "[proc_BlogMaster]");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertBlogDetails(Blogmaster p)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var fileName = Path.GetFileName(mainPic.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string myfile = fname + fileExt;
                    var path = Path.Combine(Server.MapPath("~/BlogImage/"), myfile);
                    mainPic.SaveAs(path);
                    p.PurchaseFile = myfile;
                }

                p.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt1 = objL.BlogMaster(p, "[proc_BlogMaster]");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.strId = dt1.Rows[0]["id"].ToString();
                    objp.msg = dt1.Rows[0]["msg"].ToString();
                }
                else
                {
                    objp.strId = "0";
                }


            }
            catch (Exception ex)
            {
                objp.strId = "2";
                objp.msg = ex.Message;
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBlogDetailsForEdit(string ItemCode1)
        {
            try
            {
                Blogmaster obj = new Blogmaster();
                obj.Action = 3;
                obj.Id = Convert.ToInt32(ItemCode1);
                DataTable dt = objL.BlogMaster(obj, "[proc_BlogMaster]");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    objp.Title = dt.Rows[0]["Title"].ToString();
                    objp.Description = dt.Rows[0]["Description"].ToString();
                    objp.Image = dt.Rows[0]["BlogImg"].ToString();
                    objp.EntryBy = dt.Rows[0]["Entryby"].ToString();
                    objp.EntryDate = dt.Rows[0]["EntryDate"].ToString();
                    objp.BlogImages = dt.Rows[0]["BlogImg"].ToString();
                    objp.Url = dt.Rows[0]["BlogImg"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManufacturerMaster()
        {
            try
            {
                objp.Action = "2";
                objp.dt = objL.ManufacturerMaster(objp, "proc_ManufacturerMaster");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult insertmanufacturer(PropertyClass obj)
        {

            try
            {
                //---- Upload Main Pic
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var fileName = Path.GetFileName(mainPic.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string targetFolder = Server.MapPath("~/BrandImages");
                    string targetPath = Path.Combine(targetFolder, fname + fileExt);

                    mainPic.SaveAs(targetPath);
                    string myfile = fname + fileExt;
                    obj.PurchaseFile = myfile;
                }
                else if (!string.IsNullOrEmpty(obj.ChqNo))
                {
                    obj.PurchaseFile = obj.ChqNo;
                }
                obj.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt2 = objL.ManufacturerMaster(obj, "proc_ManufacturerMaster");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    obj.msg = dt2.Rows[0]["msg"].ToString();
                    obj.strId = dt2.Rows[0]["id"].ToString();
                }
                else
                {
                    obj.msg = "Server not Responding";
                    obj.strId = "0";
                }

            }
            catch (Exception ex)
            {
                obj.msg = ex.Message;
                obj.strId = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailsForManufacturerEdit(string SrNo)
        {
            try
            {
                objp.Action = "3";
                objp.RespoCode = SrNo.Trim();
                DataTable dt = objL.ManufacturerMaster(objp, "proc_ManufacturerMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.RespoCode = dt.Rows[0]["SrNo"].ToString();
                    objp.GroupName = dt.Rows[0]["ManufacturerName"].ToString();
                    objp.PurchaseFile = dt.Rows[0]["ImgFile"].ToString();
                    objp.BatchNo = dt.Rows[0]["ManufacturerImage"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOfferNew()
        {
            ViewBag.ItemHeadList = PropertyClass.BindDDLwithbarcode(objL.BindItemheadDropDownOffercreatenewoffer(Session["UserName"].ToString()));
            ViewBag.BrandList = PropertyClass.BindDDL(objL.BindManufacturerDropDown());
            ViewBag.MainCategoryList = PropertyClass.BindDDL(objL.BindProductMainCategoryDropDown());
            objp.Action = "1";
            objp.dt = objL.GetOfferDetails(objp, "Proc_GetOfferDetails");
            return View(objp);
        }

        public ActionResult AddPinCode()
        {
            try
            {
                objp.Action = "2";
                objp.dt = objL.InsertUpdatePincode(objp, "proc_InsertUpdatePinCode");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public JsonResult InsertUpdatePincode(PropertyClass model)
        {
            try
            {
                DataTable dt = objL.InsertUpdatePincode(model, "proc_InsertUpdatePinCode");
                if (dt != null && dt.Rows.Count > 0)
                {
                    model.strId = dt.Rows[0]["id"].ToString();
                    model.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    model.strId = "0";
                }
            }
            catch (Exception ex)
            {
                model.strId = "0";
                model.msg = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPinCodeForEdit(string PinCode)
        {
            string msg = "";
            PropertyClass model = new PropertyClass();
            try
            {
                model.PinCode = PinCode;
                model.Action = "3";
                DataTable dt = objL.InsertUpdatePincode(model, "proc_InsertUpdatePinCode");
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["PinCode"].ToString() != "")
                    {
                        model.RespoCode = dt.Rows[0]["SrNo"].ToString();
                        model.PinCode = dt.Rows[0]["PinCode"].ToString();
                        model.Area = dt.Rows[0]["Area"].ToString();
                        model.DeliveryDays = dt.Rows[0]["Delivery_Days"].ToString();
                        model.DelCharges = dt.Rows[0]["DeliveryCharges"].ToString();
                        model.MinDelCharges = dt.Rows[0]["MinDelAmount"].ToString();
                    }
                    else
                    {
                        model.RespoCode = "0";
                    }

                }
                else
                {
                    model.RespoCode = "0";
                }
            }
            catch (Exception ex)
            {
                model.RespoCode = "0";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //Reason Master

        //public ActionResult AddReasonMaster() 
        //{
        //    try
        //    {
        //        objp.Action = "2";
        //        objp.dt = objL.InsertUpdateUOMpMaster(objp, "Proc_InsertUpdateUOM");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View(objp);
        //}

        //public JsonResult InsertDetails(PropertyClass p)
        //{
        //    string msg = "";
        //    try
        //    {
        //        DataTable dt = objL.InsertUpdateUOMpMaster(p, "Proc_InsertUpdateUOM");
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            msg = dt.Rows[0]["msg"].ToString();
        //        }
        //        else
        //        {
        //            msg = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "0";
        //    }
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

        #region Divanshu Shakya

        #region Combo Offer

        public ActionResult combooffer()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            try
            {

                ViewBag.ItemHeadList = PropertyClass.BindDDLwithbarcode(objL.BindItemheadDropDown1creatoffer(Session["UserName"].ToString()));

                objp.dt = objL.mst_SaveUpdateComboOffer(3, null, null, 0,0, null, 0);



            }
            catch (Exception ex) { }


            return View(objp);

        }

        public JsonResult InsertComboOffer(int Action, string ItemCode, string FreeItemCode, int FreeQuantity,int ItemQuantity, 
             int Id)
        {
            string strId = "";
            try
            {

                DataTable dt = objL.mst_SaveUpdateComboOffer(Action, ItemCode, FreeItemCode, FreeQuantity, ItemQuantity, Convert.ToString(Session["UserName"]), Id);

                if (dt != null && dt.Rows.Count > 0)
                {
                    strId = dt.Rows[0]["id"].ToString();
                }
                else
                {
                    strId = "0";
                }
            }
            catch (Exception ex)
            {
                strId = "0";
            }
            return Json(strId, JsonRequestBehavior.AllowGet);

        }


        public JsonResult DeleteComboOffer(int Id)

        {
            string strId = "";
            try
            {

                DataTable dt = objL.mst_SaveUpdateComboOffer(2, "", "", 0, 0, Convert.ToString(Session["UserName"]), Id);

                if (dt != null && dt.Rows.Count > 0)
                {
                    strId = dt.Rows[0]["id"].ToString();
                }
                else
                {
                    strId = "0";
                }
            }
            catch (Exception ex)
            {
                strId = "0";
            }
            return Json(strId, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #endregion


        #region prahlad singh
        public ActionResult ManageDeliveryCharges()
        {
            try
            {
                objp.Action = "2";
                objp.dt = objL.InsertUpdateDeliveryCharges(objp, "Proc_deliverychargesMaster");
            }
            catch (Exception ex) { }
            return View(objp);
        }
        public JsonResult InsertUpdateCharge(PropertyClass p)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = objL.InsertUpdateDeliveryCharges(p, "Proc_deliverychargesMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    p.strId = "1";
                    p.msg = dt.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            { }
            return Json(p, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeliveryChargesForEdit(PropertyClass p)
        {
            DataTable dt = new DataTable();
            try
            {
                p.Action = "3";
                dt = objL.InsertUpdateDeliveryCharges(p, "Proc_deliverychargesMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    p.deliverycharges = Convert.ToDecimal(dt.Rows[0]["DeliveryCharge"]);
                    p.Id = Convert.ToInt32(dt.Rows[0]["id"]);
                    p.MinDelCharges = Convert.ToString(dt.Rows[0]["mrpfrom"]);
                    p.DeliveryTo = Convert.ToString(dt.Rows[0]["mrpto"]);

                }
            }
            catch (Exception ex)
            { }
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTOPincode(string Id)
        {
            try
            {
                objp.strId = Id;
                objp.Id = Convert.ToInt32(Id);
                objp.Action = "5";
                objp.dt = objL.InsertUpdateDeliveryCharges(objp, "Proc_deliverychargesMaster");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult Addpincodewithdeliverycharge(string PincodeList, string id)
        {
            if (id != "" && id != null)
            {
                try
                {
                    DataTable dt = new DataTable();
                    DataTable dtpincode = new DataTable();
                    dtpincode.Columns.Add("pincodeid");
                    dtpincode.Columns.Add("delchargeid");
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    List<PincodeList1> result = js.Deserialize<List<PincodeList1>>(PincodeList);
                    foreach (var mid in result)
                    {
                        dtpincode.Rows.Add(mid.PincodeList, id);
                    }
                    objp.Action = "1";
                    objp.strId = id;
                    dt = objL.InsertUpdateDeliveryChargeswithPincode(objp, dtpincode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objp.strId = dt.Rows[0]["id"].ToString();
                        objp.msg = dt.Rows[0]["msg"].ToString();
                    }
                }
                catch (Exception ex)
                { }
            }
            else
            {
                objp.strId = "2";
                objp.msg = "Server not responding";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        #region Point offer
        public ActionResult PointOffer()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            try
            {

                ViewBag.ItemHeadList = PropertyClass.BindDDLwithbarcode(objL.BindItemheadDropDown1pointoffer(Session["UserName"].ToString()));


                objp.Action = "2";
                objp.dt = objL.InsertUpdatePointsOffer(objp);



            }
            catch (Exception ex) { }


            return View(objp);
        }

        #endregion
        public JsonResult InsertPointOffer(string ItemCode, string Points, string FromDate, string Todate, string Action, string Id)
        {
            objp.ItemCode = ItemCode;
            objp.Points = Convert.ToDecimal(Points);
            objp.startDate = FromDate;
            objp.EndDate = Todate;
            objp.Action = Action;
            objp.Id = Convert.ToInt32(Id);
            objp.EntryBy = Session["UserName"].ToString();
            DataTable dt = objL.InsertUpdatePointsOffer(objp);
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.strId = dt.Rows[0]["id"].ToString();
                objp.msg = dt.Rows[0]["msg"].ToString();
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region Mohd Nadeem 08/08/2022

        public ActionResult Redeempointmaster()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            try
            {



                objp.Action = "2";
                objp.dt = objL.InsertUpdateRedeem(objp);



            }
            catch (Exception ex) { }


            return View(objp);
        }



        public JsonResult InsertRedeemPointmaster(string Points, string Action, string NoOfTimesRedeem)
        {

            objp.Points = Convert.ToDecimal(Points);

            objp.Action = Action;
            objp.NoOfTimesRedeem = NoOfTimesRedeem;
            objp.EntryBy = Session["UserName"].ToString();
            DataTable dt = objL.InsertUpdateRedeem(objp);
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.strId = dt.Rows[0]["id"].ToString();
                objp.msg = dt.Rows[0]["msg"].ToString();
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }


        public JsonResult InsertRedeemdeletemaster(string RespoCode, string Action)
        {



            objp.Action = Action;
            objp.RespoCode = RespoCode;
            objp.EntryBy = Session["UserName"].ToString();
            DataTable dt = objL.DeleteReportRedeem(objp);
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.strId = dt.Rows[0]["id"].ToString();
                objp.msg = dt.Rows[0]["msg"].ToString();
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }


        #endregion



        #region Anchal Chaurasiya  : 23/08/2022

        #region Add Delivery Agent 

        public ActionResult AddNewSalesPerson()
        {
            SalesPerson obj = new SalesPerson();
            try
            {
                if (Request.QueryString["fCode"] != null)
                {
                    obj.Action = "3";
                    obj.SalesPersonId = Convert.ToString(Request.QueryString["fCode"]);
                    DataTable dt = objL.insertSalesPerson(obj);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        obj.SalesPersonId = dt.Rows[0]["SpCode"].ToString();
                        obj.Name = dt.Rows[0]["Name"].ToString();
                        obj.ContactNo = dt.Rows[0]["ContactNo"].ToString();
                        obj.Address = dt.Rows[0]["Address"].ToString();
                        obj.Pincode = dt.Rows[0]["Pincode"].ToString();
                        obj.AadharNo = dt.Rows[0]["AadharNo"].ToString();
                        obj.Emailid = dt.Rows[0]["EmailId"].ToString();
                    }
                    else
                    {
                        obj.SalesPersonId = "";
                    }
                }
            }
            catch (Exception ex)
            {
                obj.SalesPersonId = "";
            }
            return View(obj);
        }

        public JsonResult InserSalesPerson(SalesPerson model)
        {
            CommonRes res = new CommonRes();
            try
            {
                if (Session["UserName"] != null)
                {
                    DataTable dt = objL.insertSalesPerson(model);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        res.Id = dt.Rows[0]["Id"].ToString();
                        res.msg = dt.Rows[0]["msg"].ToString();
                    }
                    else
                    {
                        res.Id = "0";
                        res.msg = "something went wrong , please try again later !!";
                    }
                }
                else
                {
                    res.Id = "0";
                    res.msg = "something went wrong , please try again later !!";
                }
            }
            catch (Exception ex)
            {
                res.Id = "0";
                res.msg = ex.Message;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllSalesPerson()
        {
            SalesPerson obj = new SalesPerson();
            try
            {
                obj.Action = "2";
                
                obj.dt = objL.insertSalesPerson(obj);
            }
            catch (Exception ex)
            { }
            return View(obj);
        }


        #endregion



        #endregion

    }
    public class PincodeList1
    {
        public string PincodeList { get; set; }
    }

}