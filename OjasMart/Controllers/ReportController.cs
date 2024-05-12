using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class ReportController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: Report  

        public ActionResult GSTReport(string StartDate, string EndDate, string AccCode)
        {
            try
            {
                ViewBag.SupplierList = PropertyClass.BindDDL(objL.BindSupplierDropDownNew());              
                
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.AccountCode = AccCode != "0" ? AccCode : null;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.Role = Convert.ToString(Session["Role"]);
                if (objp.Role == "1")
                {
                    objp.Action = "1";
                }
                else
                {
                    objp.Action = "2";
                }
                objp.UserName = Convert.ToString(Session["UserName"]);

                objp.dt = objL.GetPurchaseReport(objp, "Proc_GetGSTReport");
                decimal taxableAmt = 0, TotaltaxAmt = 0, NetTotal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        taxableAmt += dr["GrossPayable"].ToString() != "" ? Convert.ToDecimal(dr["GrossPayable"].ToString()) : 0;
                        TotaltaxAmt += dr["Totaltax"].ToString() != "" ? Convert.ToDecimal(dr["Totaltax"].ToString()) : 0;
                        NetTotal += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                    }

                    objp.GrossPayable = taxableAmt;
                    objp.Payablegst = TotaltaxAmt;
                    objp.NetTotal = NetTotal;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult PurchaseReport(string StartDate, string EndDate, string AccCode)
        {
            try
            {
                ViewBag.SupplierList = PropertyClass.BindDDL(objL.BindSupplierDropDownNew());
                
                
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.AccountCode = AccCode != "0" ? AccCode : null;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.Role = Convert.ToString(Session["Role"]);
                if (objp.Role == "1")
                {
                    objp.Action = "1";
                }
                else
                {
                    objp.Action = "2";
                }
                objp.UserName = Convert.ToString(Session["UserName"]);

                objp.dt = objL.GetPurchaseReport(objp, "Proc_GetPurchaseReport");
                decimal taxableAmt = 0, TotaltaxAmt = 0, NetTotal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        taxableAmt += dr["GrossPayable"].ToString() != "" ? Convert.ToDecimal(dr["GrossPayable"].ToString()) : 0;
                        TotaltaxAmt += dr["Totaltax"].ToString() != "" ? Convert.ToDecimal(dr["Totaltax"].ToString()) : 0;
                        NetTotal += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                    }

                    objp.GrossPayable = taxableAmt;
                    objp.Payablegst = TotaltaxAmt;
                    objp.NetTotal = NetTotal;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult SalesReport(string StartDate, string EndDate, string AccCode, string BillBy)
        {
            try
            {
                ViewBag.CustomerList = PropertyClass.BindDDL(objL.BindCustomerDropDownNew());
                ViewBag.SSList = PropertyClass.BindDDL(objL.BindStockiestDropDownNew());
                objp.Action = "1";
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.AccountCode = AccCode != "" ? AccCode : null;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.Role = Convert.ToString(Session["Role"]);
                if (Convert.ToString(Session["Role"]) == "1")
                {
                    if (!string.IsNullOrEmpty(BillBy))
                    {
                        objp.UserName = BillBy;
                    }
                    else
                    {
                        objp.UserName = null;
                    }
                }
                else
                {
                    objp.UserName = Convert.ToString(Session["UserName"]);
                }
                if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "2" && Session["Role"].ToString() != "3" && Session["Role"].ToString() != "4")
                {
                    objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                }

                objp.dt = objL.GetPurchaseReport(objp, "Proc_GetSalesReport");
                decimal taxableAmt = 0, TotaltaxAmt = 0, NetTotal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        taxableAmt += dr["GrossPayable"].ToString() != "" ? Convert.ToDecimal(dr["GrossPayable"].ToString()) : 0;
                        TotaltaxAmt += dr["Totaltax"].ToString() != "" ? Convert.ToDecimal(dr["Totaltax"].ToString()) : 0;
                        NetTotal += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                    }

                    objp.GrossPayable = taxableAmt;
                    objp.Payablegst = TotaltaxAmt;
                    objp.NetTotal = NetTotal;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult GetCustomerWalletRechargeDetails(string StartDate, string EndDate)
        {
            try
            {
                objp.Action = "1";
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.Role = Convert.ToString(Session["Role"]);
                objp.SSCode = Convert.ToString(Session["UserName"]);
                objp.CustomerId = Convert.ToString(Session["UserName"]);

                objp.dt = objL.GetWalletRechargeDetails(objp, "Proc_GetWalletRechargeDetails");
                decimal NetTotal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        NetTotal += dr["RechargeAmount"].ToString() != "" ? Convert.ToDecimal(dr["RechargeAmount"].ToString()) : 0;
                    }

                    objp.NetTotal = NetTotal;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult GetWalletRechargeHistory(string MobileNo, string StartDate, string EndDate, string CenterCode)
        {
            try
            {
                ViewBag.StoreList = PropertyClass.BindDDL(objL.BindStockiestDropDownNew());
                objp.Action = "2";
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.Role = Convert.ToString(Session["Role"]);
                objp.ContactNo = (!string.IsNullOrEmpty(MobileNo)) ? MobileNo : null;
                if (Session["Role"].ToString() != "1")
                {
                    objp.SSCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.SSCode = (!string.IsNullOrEmpty(CenterCode)) ? CenterCode : null;
                }
                if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "2" && Session["Role"].ToString() != "3" && Session["Role"].ToString() != "4")
                {
                    objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                }
                objp.dt = objL.GetWalletRechargeDetails(objp, "Proc_GetWalletRechargeDetails");
                decimal NetTotal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        NetTotal += dr["RechargeAmount"].ToString() != "" ? Convert.ToDecimal(dr["RechargeAmount"].ToString()) : 0;
                    }

                    objp.NetTotal = NetTotal;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult GetCashBackWalletReport(string MobNo)
        {
            try
            {



                if (Session["CompanyCode"] == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                objp.Action = "1";
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "4")
                {
                    objp.BranchCode = Convert.ToString(Session["UserName"]);
                }
                if (Session["Role"].ToString() == "4")
                {
                    objp.CustomerId = Convert.ToString(Session["UserName"]);
                }
                if (!string.IsNullOrEmpty(MobNo))
                {
                    objp.ContactNo = MobNo;
                }
                objp.dt = objL.GetCashbackWallet(objp, "Proc_getCashBackDetails");
                decimal tAmt = 0, pAmt = 0,avlBal=0,totAmt=0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                { 
                    foreach(DataRow dr in objp.dt.Rows)
                    {
                        tAmt += dr["CashBackAmount"].ToString() != "" ? Convert.ToDecimal(dr["CashBackAmount"].ToString()) : 0;
                        pAmt += dr["Points"].ToString() != "" ? Convert.ToDecimal(dr["Points"].ToString()) : 0;
                        avlBal += dr["AvlableBal"].ToString() != "" ? Convert.ToDecimal(dr["AvlableBal"].ToString()) : 0;
                        totAmt += dr["tot"].ToString() != "" ? Convert.ToDecimal(dr["tot"].ToString()) : 0;
                    }
                    objp.CashBackAmount = tAmt;
                    objp.GrossPayable = pAmt;
                    objp.DiscAmt = avlBal;
                    objp.NetTotal = totAmt;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult ContactUsDetails()
        {
            ClsContactUs objcls = new ClsContactUs();
            DataTable dt = new DataTable();
            objcls.Action = "2";
            dt=objL.ContactUs(objcls, "Proc_ContactUs");
            objcls.dtContactUs = dt;
            return View(objcls);
        }


        public ActionResult ChequeReport(string StartDate, string EndDate, string AccCode, string BillBy)
        {
            try
            {

                // ViewBag.SSList = PropertyClass.BindDDL(objL.BindStockiestDropDownNew());
                objp.Action = "1";
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.AccountCode = AccCode != "" && AccCode != "0" ? AccCode : null;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.Role = Convert.ToString(Session["Role"]);
                objp.Status = !string.IsNullOrEmpty(BillBy) && BillBy != "0" ? BillBy : null;
                if (Convert.ToString(Session["Role"]) == "1")
                {
                    if (!string.IsNullOrEmpty(BillBy))
                    {
                        objp.UserName = BillBy;
                    }
                    else
                    {
                        objp.UserName = null;
                    }
                }
                else
                {
                    objp.UserName = Convert.ToString(Session["UserName"]);
                }
                if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "2" && Session["Role"].ToString() != "3" && Session["Role"].ToString() != "4")
                {
                    objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                    objp.UserName = null;
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    objp.UserName = Convert.ToString(Session["UserName"]);
                }
                ViewBag.CustomerList = PropertyClass.BindDDL(objL.BindAllAcountsSelect(objp.CompanyCode));
                ViewBag.SSList = PropertyClass.BindDDL(objL.BindBranchList(objp.CompanyCode));
                objp.dt = objL.GetChequeReport(objp, "ChequeClearRejectReport");
                decimal taxableAmt = 0, TotaltaxAmt = 0, NetTotal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        taxableAmt += dr["Amount"].ToString() != "" ? Convert.ToDecimal(dr["Amount"].ToString()) : 0;

                    }

                    objp.GrossPayable = taxableAmt;

                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public ActionResult VoucherReport(string StartDate, string EndDate, string vtype)
        {
            try
            {
                string vType = null;
                if (Request.QueryString["vType"] != null)
                {
                    vType = Convert.ToString(Request.QueryString["vType"]);
                }
                else
                {
                    vType = vtype;
                }
                objp.Action = "1";
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.AccountCode = vType;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.UserName = Convert.ToString(Session["UserName"]);
                if (Convert.ToString(Session["Role"]) == "3")
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    objp.BranchCode = Convert.ToString(Session["UserName"]);
                }
                objp.dt = objL.getjournalvoucher(objp, "getjournalvoucher");
                decimal taxableAmt = 0, TotaltaxAmt = 0, NetTotal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        taxableAmt += dr["Amount"].ToString() != "" ? Convert.ToDecimal(dr["Amount"].ToString()) : 0;
                    }
                    objp.PaidAmount = taxableAmt;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult IssueMemberCardForfranchise()
        {
            DataTable dt = new DataTable();
            ClsMemberShip objcls = new ClsMemberShip();
            objcls.Action = "1";
            ViewBag.dtMemberShipType = PropertyClass.BindDDL(objL.MemberShipMaster(objcls, "Proc_MemberShipMaster"));
            return View();
        }
        [HttpPost]
        public ActionResult IssueMemberCardForfranchise(ClsMemberShip objcls)
        {

            if (objcls.ProfileImage != null)
            {
                string filenam = DateTime.Now.Ticks + "" + objcls.ProfileImage.FileName.ToString();
                string path = HttpContext.Server.MapPath("~/MemberPic/");
                path = path + filenam;
                objcls.ProfileImage.SaveAs(path);
                objcls.ProfilePicPath = "/MemberPic/" + filenam;
            }
            else
            {
                objcls.ProfilePicPath = null;
            }

            objcls.Action = "1";
            PropertyClass p = new PropertyClass();
            ViewBag.dtMemberShipType = PropertyClass.BindDDL(objL.MemberShipMaster(objcls, "Proc_MemberShipMaster"));
            p.SSName = objcls.CustomerName;
            p.ContactNo = objcls.MobileNo;
            p.EmailAddress = objcls.EmailAddress;
            p.Action = "1";
            p.Password = objcls.Password;
            p.memberShipId = objcls.MemberShip;
            p.PayMode = objcls.PaymentMode;
            p.ProfilePicPath = objcls.ProfilePicPath;
            p.CardValidFrom = objcls.CardValidFrom;
            p.CardValidTo = objcls.CardValidTo;
            p.CardBarCode = objcls.MemberBarCode;

            if (Session["CompanyCode"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            objp.Action = "1";
            objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
            if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "4")
            {
                objp.CustomerId = Convert.ToString(Session["UserName"]);
            }
            if (Session["Role"].ToString() == "4")
            {
                objp.CustomerId = Convert.ToString(Session["UserName"]);
            }
           
            if (Session["Role"].ToString()=="1")
            {
                objp.CustomerId = Convert.ToString(Session["CompanyCode"]);
            }
        
            p.CustomerId = objp.CustomerId;
            DataTable dt = objL.InsertCustomerAccountNewMember(p, "Proc_InserCustomerAccountWeb");
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.msg = dt.Rows[0]["msg"].ToString();
                TempData["msg"] = dt.Rows[0]["msg"].ToString();
          
                objp.strId = dt.Rows[0]["Id"].ToString();
                if (objp.strId == "1")
                {
                    Session["CompanyName"] = "Kalash Mart";
                    Session["UserName"] = dt.Rows[0]["UserId"].ToString();
                    Session["mCode"] = dt.Rows[0]["customerid"].ToString();
                    TempData["memberCardID"] = dt.Rows[0]["CardId"].ToString();
                    TempData["flag"] = "True";
                }
                if (!string.IsNullOrEmpty(p.ContactNo))
                {
                    //string msg1 = "Dear " + p.SSName + " Thankyou for joining us. Please find your Account Login Details Below. User Name: " + dt.Rows[0]["UserId"].ToString() + ". Password: " + dt.Rows[0]["Password"].ToString() + ". Thankyou Team OZAS Mart. http://ozas199.com .";

                    //objp.sendsms(p.ContactNo, msg1);
                }

                if (objp.strId == "2")
                {

                    TempData["msg"] = dt.Rows[0]["msg"].ToString();
                    TempData["flag"] = "False";
                }
            }
            else
            {
                objp.strId = "0";
                TempData["flag"] = "False";
            }
            return View();
        }

        public ActionResult PrintmemberCard(string MemberId)
        {
            DataTable dt = new DataTable();
            ClsMemberShip objcls = new ClsMemberShip();
            PropertyClass p = new PropertyClass();
            p.memberShipId = MemberId;

            if (Session["CompanyCode"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
            if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "4")
            {
                objp.BranchCode = Convert.ToString(Session["UserName"]);
            }
            if (Session["Role"].ToString() == "4")
            {
                objp.CustomerId = Convert.ToString(Session["UserName"]);
            }
            if (Session["Role"].ToString() == "1")
            {
                objp.CustomerId = Convert.ToString(Session["CompanyCode"]);
            }
            p.Action = "3";
            p.CustomerId = objp.CustomerId;
            if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "4")
            {
                objp.BranchCode = Convert.ToString(Session["UserName"]);
            }
            if (Session["Role"].ToString() == "4")
            {
                objp.CustomerId = Convert.ToString(Session["UserName"]);
            }
            if (Session["Role"].ToString() == "1")
            {
                objp.CustomerId = Convert.ToString(Session["CompanyCode"]);
            }
            dt = objL.InsertCustomerAccountNewMember(p, "Proc_InserCustomerAccountWeb");
            if (dt != null && dt.Rows.Count > 0)
            {
                objcls.CardId = dt.Rows[0]["membercardId"].ToString();
                objcls.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                objcls.CustomerName = dt.Rows[0]["Name"].ToString();

                objcls.CardType = dt.Rows[0]["MemberShipSubTitle"].ToString();
                objcls.CardValidFrom = dt.Rows[0]["CardValidFrom"].ToString();
                objcls.CardValidTo = dt.Rows[0]["cardValidTo"].ToString();
                objcls.MemberBarCode = dt.Rows[0]["CardBardCode"].ToString();
                objcls.ProfilePicPath = dt.Rows[0]["memberPic"].ToString();
            }
            return View(objcls);
        }

        public ActionResult MembercardDtls()

        {
            ClsMemberShip objcls = new ClsMemberShip();
            DataTable dt = new DataTable();
            PropertyClass p = new PropertyClass();
            p.Action = "3";
            
            objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
            if (Session["Role"].ToString() != "1" && Session["Role"].ToString() != "4")
            {
                objp.CustomerId = Convert.ToString(Session["UserName"]);
            }
            if (Session["Role"].ToString() == "4")
            {
                objp.CustomerId = Convert.ToString(Session["UserName"]);
            }
            if (Session["Role"].ToString() == "1")
            {
                objp.CustomerId =null;
            }
            p.CustomerId = objp.CustomerId;
            dt = objL.InsertCustomerAccountNewMember(p, "Proc_InserCustomerAccountWeb");
            objcls.dtmemberShipReport = dt;
            return View(objcls);
           
        }
    }
}