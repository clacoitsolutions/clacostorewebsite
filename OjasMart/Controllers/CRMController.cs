using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;

namespace OjasMart.Controllers
{
    public class CRMController : Controller
    {
        // GET: CRM
       
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult createnewlead()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            string CompanyCode = null;
            if (Convert.ToString(Session["Role"]) == "2")
            {
                CompanyCode = Convert.ToString(Session["UserName"]);
            }
            else
            {
                CompanyCode = Convert.ToString(Session["CompanyCode"]);
            }
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
            objp.Action = "4";
            ViewBag.Unit = PropertyClass.BindDDL(objL.BindDropDowns(objp, "proc_BindDropdown"));
            ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDownPurchase(CompanyCode));

            return View();
        }
        public JsonResult BindState()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            try
            {
                DataTable dt = new DataTable();
                objp.Action = "1";
                dt = objL.BindDropDowns(objp, "proc_BindDropdown");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow ds in dt.Rows)
                    {
                        PropertyClass obj = new PropertyClass();
                        obj.StateId = Convert.ToInt32(ds["State_id"]);
                        obj.StateName = Convert.ToString(ds["State_name"]);
                        list.Add(obj);
                    }
                    //list = dt.AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetItemrate(string ItemId)
        {
            string Rate = "0";
            DataTable dt = objL.GetItemrate(ItemId);
            if (dt != null && dt.Rows.Count > 0)
            {
                Rate = dt.Rows[0]["Itemrate"].ToString();
            }

            return Json(Rate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindEmployees()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            try
            {
                DataTable dt = new DataTable();
                objp.Action = "5";
                dt = objL.BindDropDowns(objp, "proc_BindDropdown");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow ds in dt.Rows)
                    {
                        PropertyClass obj = new PropertyClass();
                        obj.EmployeeCode = Convert.ToString(ds["EmployeeCode"]);
                        obj.EmployeeName = Convert.ToString(ds["Name"]);
                        list.Add(obj);
                    }
                    //list = dt.AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LeadReport(string EmpCode, string StartDate, string EndDate, string Status, string LeadId)
        {
            try
            {
                if (Session["Role"] != null)
                {
                    objp.Action = "2";
                    if (Session["Role"].ToString() != "1")
                    {
                        objp.EmployeeCode = Session["UserName"].ToString();
                    }
                    else
                    {
                        objp.EmployeeCode = EmpCode != "" ? EmpCode : null;
                    }
                    if (LeadId == "")
                    {
                        LeadId = null;
                    }
                    else
                    {
                        objp.LeadId = LeadId;
                    }
                    //objp.startDate = (!string.IsNullOrEmpty(StartDate)) ? StartDate : Convert.ToString(DateTime.Today);
                    //objp.EndDate = (!string.IsNullOrEmpty(EndDate)) ? EndDate : Convert.ToString(DateTime.Today);
                    objp.startDate = (!string.IsNullOrEmpty(StartDate)) ? StartDate : null;
                    objp.EndDate = (!string.IsNullOrEmpty(EndDate)) ? EndDate : null;
                    objp.Status = Status != "" ? Status : null;

                    objp.dt = objL.GetLeadSummaryReport(objp, "[Proc_GetLeadSummaryReport]");

                    if (!string.IsNullOrEmpty(LeadId))
                    {
                        objp.Action = "3";
                        objp.dt1 = objL.GetLeadSummaryReport(objp, "[Proc_GetLeadSummaryReport]");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

     
        public ActionResult FollowUpList(string LeadId)
        {

            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                string CompanyCode = null;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
                objp.Action = "4";
                ViewBag.Unit = PropertyClass.BindDDL(objL.BindDropDowns(objp, "proc_BindDropdown"));
                ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDownPurchase(CompanyCode));



                objp.Action = "4";
                objp.RoleId = int.Parse(Session["Role"].ToString());
                objp.UserName = Session["UserName"].ToString();
                DataTable dt = objL.getDetailsdt(objp, "[Proc_LeadMaster]");
                objp.dt1 = dt;

                if (!string.IsNullOrEmpty(LeadId))
                {
                    objp.Action = "3";
                    objp.LeadId = LeadId;
                    objp.dt2 = objL.GetLeadSummaryReport(objp, "[Proc_GetLeadSummaryReport]");
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }


        public JsonResult InsertLeadDetails(PropertyClass p)
        {
            string strid = "";
            DataTable dt1 = new DataTable();
            if (p.CatId != null)
            {
                Hashtable[] hs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Hashtable[]>(p.CatId);
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("ItemCode", typeof(string));
                dtnew.Columns.Add("QTY", typeof(decimal));
                dtnew.Columns.Add("unit", typeof(int));
                dtnew.Columns.Add("Rate", typeof(decimal));

                if (hs != null && hs.Length > 0)
                {
                    foreach (Hashtable item in hs)
                    {
                        dtnew.Rows.Add(item["ItemName"], item["Qty"], item["unit"], item["rate"]);
                    }
                }
                dt1 = dtnew;
            }
            p.Action = "1";
            DataTable dtt = objL.InsertLeadDetails(p, dt1);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                strid = dtt.Rows[0]["id"].ToString();

            }
            else
            {
                strid = "0";
            }

            return Json(strid, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFolloups(string LeadId)
        {
            PropertyClass prop = new PropertyClass();
            try
            {
                DataTable dt = new DataTable();
                objp.Action = "1";
                objp.LeadId = LeadId;
                dt = objL.getFollowUps(objp, "proc_FollowUpMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    prop.LeadId = dt.Rows[0]["LeadId"].ToString();
                    prop.LeadTitle = dt.Rows[0]["LeadTitle"].ToString();
                    prop.EmployeeName = dt.Rows[0]["Name"].ToString();
                    prop.MobileNo = dt.Rows[0]["MobileNo"].ToString();
                    prop.EmailId = dt.Rows[0]["EmailId"].ToString();
                    prop.NetTotal = Convert.ToDecimal(dt.Rows[0]["totalprice"].ToString());
                    prop.FollowUpDate = Convert.ToDateTime(dt.Rows[0]["FollowupDate"].ToString());
                    prop.Description = Regex.Replace(dt.Rows[0]["Description"].ToString(), "<[^>]*>"," ");
                    prop.Status = dt.Rows[0]["Status"].ToString();
                    prop.EmployeeCode = dt.Rows[0]["EmployeeCode"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(prop, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindAccountDetails()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            try
            {
                DataTable dt = new DataTable();
                objp.Action = "9";
                dt = objL.BindDropDowns(objp, "proc_BindDropdown");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow ds in dt.Rows)
                    {
                        PropertyClass obj = new PropertyClass();
                        obj.BankAccId = Convert.ToString(ds["Id"]);
                        obj.BanKAccName = Convert.ToString(ds["AccName"]);
                        list.Add(obj);
                    }
                    //list = dt.AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult InsertFollowUp(string LeadId, string EmployeeCode, DateTime NextFollowUpDate, string Description, string Status, string closedby, decimal projectcost, decimal advanceamount, string projectmode, bool isgst, string paymode, string ProjectDescription, string ProjectTitle, DateTime StartDate, DateTime DelDate, string CatId, string ChqDDNo, DateTime chqDDDate, string IFSCCode, string BankAccName, string TransId, DateTime TransDate, string bankAccId, string MobileNO)
        {
            string msg = "";
            objp.CatId = CatId;
            try
            {

                if (objp.CatId != null)
                {
                    Hashtable[] hs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Hashtable[]>(objp.CatId);
                    DataTable dtnew = new DataTable();
                    dtnew.Columns.Add("ItemCode", typeof(string));
                    dtnew.Columns.Add("QTY", typeof(decimal));
                    dtnew.Columns.Add("unit", typeof(int));
                    dtnew.Columns.Add("Rate", typeof(decimal));

                    if (hs != null && hs.Length > 0)
                    {
                        foreach (Hashtable item in hs)
                        {
                            dtnew.Rows.Add(item["ItemName"], item["Qty"], item["unit"], item["rate"]);
                        }
                    }
                    objp.dt1 = dtnew;
                }
                //objP.DepartmentName = DepartmentName;
                //objP.EntryBy = Session["UserName"].ToString();
                objp.Action = "2";
                objp.LeadId = LeadId;
                objp.EmployeeCode = EmployeeCode;
                objp.FollowUpDate = DateTime.MinValue;
                objp.NextFollowUpDate = NextFollowUpDate;
                objp.Description = Description;
                objp.Status = Status;
                objp.EntryBy = Session["UserName"].ToString();
                objp.closedby = closedby;
                objp.projectcost = projectcost;
                objp.advanceamount = advanceamount;
                objp.projectmode = projectmode;
                objp.isgst = isgst;
                objp.PayMode = paymode;
                objp.ProductSpacification = ProjectDescription;
                objp.LeadTitle = ProjectTitle;
                objp.LeadDate = StartDate;
                objp.ExpiryDate = DelDate;

                objp.chqddNo = ChqDDNo;
                objp.chqddDate = chqDDDate;
                objp.ifsccode = IFSCCode;
                objp.BanKAccName = BankAccName;
                objp.BankTransId = TransId;
                objp.TransDate = TransDate;
                objp.BankAccId = bankAccId;

                int r = objL.InsertFollowUpDetails(objp);
                msg = "1";

                if (Status.Trim() == "Close")
                {
                    if (!string.IsNullOrEmpty(MobileNO))
                    {
                        string closedate = NextFollowUpDate.ToString("dd-MMM-yyyy");
                        string message = "Dear Customer, Your Project is Successfully closed with SigmaIT Softwares and Designers Pvt. Ltd. on " + closedate + ", Thankyou for choosing us.";
                        //objL.sendsms(MobileNO, message);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LeadSummary(string LeadId)
        {

            objp.Action = "2";
            objp.LeadId = LeadId;
            objp.dt = objL.GetLeadSummaryReport(objp, "[Proc_GetLeadSummaryReport]");

            if (!string.IsNullOrEmpty(LeadId))
            {
                objp.Action = "3";
                objp.LeadId = LeadId;
                //objp.dt1 = objL.GetLeadSummaryReport(objp, "[Proc_GetLeadSummaryReport]");
                objp.dt1 = null;
                objp.Action = "4";
                objp.dt2 = objL.GetLeadSummaryReport(objp, "[Proc_GetLeadSummaryReport]");
            }
            return View(objp);
        }


    }
}