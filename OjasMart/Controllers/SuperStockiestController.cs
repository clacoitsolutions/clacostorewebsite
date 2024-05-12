using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;
using System.IO;

namespace OjasMart.Controllers
{
    public class SuperStockiestController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: SuperStockiest
        public ActionResult CreateSuperStockiest()
        {
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
            if (Request.QueryString["SSCode"] != null)
            {
                objp.Action = "2";
                objp.SSCode = Convert.ToString(Request.QueryString["SSCode"]);
                DataTable dt = objL.GetSuperStockiestDetails(objp, "Proc_GetSSList");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.SSCode = dt.Rows[0]["SSCode"].ToString();
                    objp.SSName = dt.Rows[0]["SSName"].ToString();
                    objp.ContactPerson = dt.Rows[0]["ContactPerson"].ToString();
                    objp.ContactNo = dt.Rows[0]["ContactNo"].ToString();
                    objp.EmailAddress = dt.Rows[0]["emailAddress"].ToString();
                    objp.StCode = dt.Rows[0]["State_id"].ToString();
                    objp.PinCode = dt.Rows[0]["PinCode"].ToString();
                    objp.Address = dt.Rows[0]["Address"].ToString();
                    objp.CityName = dt.Rows[0]["CityName"].ToString();
                    objp.GstNo = dt.Rows[0]["GstNo"].ToString();
                    objp.PanNo = dt.Rows[0]["PanNo"].ToString();
                }
            }
            return View(objp);
        }
        public JsonResult InsertUpdateSuperStockiest(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                DataTable dt = objL.InsertUpdateSuperStockiest(p, "Proc_InsertUpdateSuperStockiest");
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
        public ActionResult SuperStockiestList()
        {
            objp.Action = "1";
            objp.dt = objL.GetSuperStockiestDetails(objp, "Proc_GetSSList");
            return View(objp);
        }
        public ActionResult MyProfile()
        {
            
            if (Request.QueryString["MCode"] != null)
            {
                objp.Action = "1";
                objp.Mtype = Request.QueryString["Mtype"].ToString();
                objp.UserName = Convert.ToString(Request.QueryString["MCode"]);
                DataTable dt = objL.BindUSerProfile(objp, "Proc_BindMyProfile");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.SSCode = dt.Rows[0]["SSCode"].ToString();
                    objp.SSName = dt.Rows[0]["SSName"].ToString();
                    objp.ContactPerson = dt.Rows[0]["ContactPerson"].ToString();
                    objp.ContactNo = dt.Rows[0]["ContactNo"].ToString();

                    objp.EmailAddress = dt.Rows[0]["emailAddress"].ToString();
                    objp.PinCode = dt.Rows[0]["PinCode"].ToString();
                    objp.Address = dt.Rows[0]["Address"].ToString();
                    objp.CityName = dt.Rows[0]["CityName"].ToString();

                    objp.StCode = dt.Rows[0]["StateCode"].ToString();
                    objp.GstNo = dt.Rows[0]["GstNo"].ToString();
                    objp.PanNo = dt.Rows[0]["PanNo"].ToString();
                    objp.CompanyCode = dt.Rows[0]["CompanyCode"].ToString();                    
                    objp.StateName = dt.Rows[0]["State_name"].ToString();
                    objp.ActiveStatus= dt.Rows[0]["AccSt"].ToString();
                    if (Request.QueryString["Mtype"].ToString()=="SS")
                    {
                        objp.Mtype = "Super Stokist";
                    }
                    else
                    {
                        objp.Mtype = "OutLet";
                    }
                    objp.StockistName = dt.Rows[0]["SuperStockName"].ToString();

                }
                objp.Action = "2";
                objp.dt = objL.BindUSerProfile(objp, "Proc_BindMyProfile");
                decimal totalAmt = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {                       
                        totalAmt += dr["TotalAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0;                        
                    }                    
                    objp.NetTotal = totalAmt;                    
                }
                objp.Action = "3";
                objp.dt1 = objL.BindUSerProfile(objp, "Proc_BindMyProfile");

                objp.Action = "4";
                objp.dt2 = objL.BindUSerProfile(objp, "Proc_BindMyProfile");
                decimal totgst = 0, totalAmt1 = 0, grossAmt = 0;
                if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt2.Rows)
                    {
                        totgst += dr["Totaltax"].ToString() != "" ? Convert.ToDecimal(dr["Totaltax"].ToString()) : 0;
                        totalAmt1 += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                        grossAmt += dr["GrossPayable"].ToString() != "" ? Convert.ToDecimal(dr["GrossPayable"].ToString()) : 0;
                    }

                    objp.Payablegst = totgst;
                    objp.PaidAmount = totalAmt1;
                    objp.GrossPayable = grossAmt;
                }

            }
            else if(Session["Role"].ToString() !="1")
            {
                if(Session["Role"].ToString()=="2")
                {
                    objp.Mtype = "SS";
                }
                else
                {
                    objp.Mtype = "OT";
                }
                objp.Action = "1";                
                objp.UserName = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.BindUSerProfile(objp, "Proc_BindMyProfile");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.SSCode = dt.Rows[0]["SSCode"].ToString();
                    objp.SSName = dt.Rows[0]["SSName"].ToString();
                    objp.ContactPerson = dt.Rows[0]["ContactPerson"].ToString();
                    objp.ContactNo = dt.Rows[0]["ContactNo"].ToString();

                    objp.EmailAddress = dt.Rows[0]["emailAddress"].ToString();
                    objp.PinCode = dt.Rows[0]["PinCode"].ToString();
                    objp.Address = dt.Rows[0]["Address"].ToString();
                    objp.CityName = dt.Rows[0]["CityName"].ToString();

                    objp.StCode = dt.Rows[0]["StateCode"].ToString();
                    objp.GstNo = dt.Rows[0]["GstNo"].ToString();
                    objp.PanNo = dt.Rows[0]["PanNo"].ToString();
                    objp.CompanyCode = dt.Rows[0]["CompanyCode"].ToString();
                    objp.StateName = dt.Rows[0]["State_name"].ToString();
                    objp.ActiveStatus = dt.Rows[0]["AccSt"].ToString();
                    if (Session["Role"].ToString() == "2")
                    {
                        objp.Mtype = "Super Stokist";
                    }
                    else
                    {
                        objp.Mtype = "OutLet";
                    }
                    objp.StockistName = dt.Rows[0]["SuperStockName"].ToString();

                }
                objp.Action = "2";
                objp.dt = objL.BindUSerProfile(objp, "Proc_BindMyProfile");
                decimal totalAmt = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        totalAmt += dr["TotalAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0;
                    }
                    objp.NetTotal = totalAmt;
                }
                objp.Action = "3";
                objp.dt1 = objL.BindUSerProfile(objp, "Proc_BindMyProfile");

                objp.Action = "4";
                objp.dt2 = objL.BindUSerProfile(objp, "Proc_BindMyProfile");
                decimal totgst = 0, totalAmt1 = 0, grossAmt = 0;
                if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt2.Rows)
                    {
                        totgst += dr["Totaltax"].ToString() != "" ? Convert.ToDecimal(dr["Totaltax"].ToString()) : 0;
                        totalAmt1 += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                        grossAmt += dr["GrossPayable"].ToString() != "" ? Convert.ToDecimal(dr["GrossPayable"].ToString()) : 0;
                    }

                    objp.Payablegst = totgst;
                    objp.PaidAmount = totalAmt1;
                    objp.GrossPayable = grossAmt;
                }
            }
            return View(objp);
        }
        public JsonResult UpdateAccountStatus(PropertyClass p)
        {
            string msg = "";
            try
            {
                DataTable dt = objL.UpdateAccountStatus(p, "Proc_ActiveDeActiveAccount");
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

        public ActionResult CreateNewCompany()
        {
            try
            {
                ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
                if (Request.QueryString["SSCode"] != null)
                {
                    objp.Action = "2";
                    objp.SSCode = Convert.ToString(Request.QueryString["SSCode"]);
                    DataTable dt = objL.GetSuperStockiestDetails(objp, "Proc_GetSSList");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objp.SSCode = dt.Rows[0]["SSCode"].ToString();
                        objp.SSName = dt.Rows[0]["SSName"].ToString();
                        objp.ContactPerson = dt.Rows[0]["ContactPerson"].ToString();
                        objp.ContactNo = dt.Rows[0]["ContactNo"].ToString();
                        objp.EmailAddress = dt.Rows[0]["emailAddress"].ToString();
                        objp.StCode = dt.Rows[0]["State_id"].ToString();
                        objp.PinCode = dt.Rows[0]["PinCode"].ToString();
                        objp.Address = dt.Rows[0]["Address"].ToString();
                        objp.CityName = dt.Rows[0]["CityName"].ToString();
                        objp.GstNo = dt.Rows[0]["GstNo"].ToString();
                        objp.PanNo = dt.Rows[0]["PanNo"].ToString();

                        objp.CompanyLogo = dt.Rows[0]["CompanyLogo"].ToString();
                        objp.AadhaarNo = dt.Rows[0]["AadhaarNo"].ToString();
                        objp.UserName = dt.Rows[0]["UserName"].ToString();
                        objp.Bankname = dt.Rows[0]["BankName"].ToString();
                        objp.BanKAccName = dt.Rows[0]["BankAccountName"].ToString();
                        objp.branchname = dt.Rows[0]["BankBranch"].ToString();
                        objp.accountno = dt.Rows[0]["AccountNumber"].ToString();
                        objp.ifsccode = dt.Rows[0]["IFSCCode"].ToString();
                        objp.InvoicePrefix = dt.Rows[0]["InvoicePrefix"].ToString();
                        objp.Description = dt.Rows[0]["SaleInvoiceNotes"].ToString();
                        objp.IsLatterPad = dt.Rows[0]["IsLatterPad"].ToString() != "" ? Convert.ToBoolean(dt.Rows[0]["IsLatterPad"].ToString()) : false;
                    }
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        [ValidateInput(false)]
        public JsonResult InsertUpdateCompanyDetails(PropertyClass p)
        {
            string msg = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    var path = Path.Combine(Server.MapPath("../CompanyLogo"), fName);
                    mainPic.SaveAs(path);
                    p.CompanyLogo = "../CompanyLogo/" + fName;
                }
                else if (!string.IsNullOrEmpty(p.CompanyLogo))
                {
                    p.CompanyLogo = p.CompanyLogo;
                }
                else
                {
                    p.CompanyLogo = null;
                    //objP.FileExt = null;
                }
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                DataTable dt = objL.InsertUpdateCompanyDetails(p, "Proc_InsertUpdateComapny");
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

        public ActionResult UpdateCompanyDetails()
        {
            try
            {
                ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
                if (Request.QueryString["SSCode"] != null)
                {
                    objp.Action = "2";
                    objp.SSCode = Convert.ToString(Request.QueryString["SSCode"]);
                    DataTable dt = objL.GetSuperStockiestDetails(objp, "Proc_GetSSList");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objp.SSCode = dt.Rows[0]["SSCode"].ToString();
                        objp.SSName = dt.Rows[0]["SSName"].ToString();
                        objp.ContactPerson = dt.Rows[0]["ContactPerson"].ToString();
                        objp.ContactNo = dt.Rows[0]["ContactNo"].ToString();
                        objp.EmailAddress = dt.Rows[0]["emailAddress"].ToString();
                        objp.StCode = dt.Rows[0]["State_id"].ToString();
                        objp.PinCode = dt.Rows[0]["PinCode"].ToString();
                        objp.Address = dt.Rows[0]["Address"].ToString();
                        objp.CityName = dt.Rows[0]["CityName"].ToString();
                        objp.GstNo = dt.Rows[0]["GstNo"].ToString();
                        objp.PanNo = dt.Rows[0]["PanNo"].ToString();

                        objp.CompanyLogo = dt.Rows[0]["CompanyLogo"].ToString();
                        objp.AadhaarNo = dt.Rows[0]["AadhaarNo"].ToString();
                        objp.UserName = dt.Rows[0]["UserName"].ToString();
                        objp.Bankname = dt.Rows[0]["BankName"].ToString();
                        objp.BanKAccName = dt.Rows[0]["BankAccountName"].ToString();
                        objp.branchname = dt.Rows[0]["BankBranch"].ToString();
                        objp.accountno = dt.Rows[0]["AccountNumber"].ToString();
                        objp.ifsccode = dt.Rows[0]["IFSCCode"].ToString();
                        objp.InvoicePrefix = dt.Rows[0]["InvoicePrefix"].ToString();
                        objp.Description = dt.Rows[0]["SaleInvoiceNotes"].ToString();
                        objp.IsLatterPad = dt.Rows[0]["IsLatterPad"].ToString() != "" ? Convert.ToBoolean(dt.Rows[0]["IsLatterPad"].ToString()) : false;
                    }
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
    }
}