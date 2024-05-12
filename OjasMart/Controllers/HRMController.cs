using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OjasMart.Models;
using System.IO;

namespace OjasMart.Controllers
{
    public class HRMController : Controller
    {
        // GET: HRM
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmployee(string Empcode)
        {
            try
            {
                ViewBag.DesignationList = PropertyClass.BindDDL(objL.BindDesignationDropDown());
                if (!string.IsNullOrEmpty(Empcode))
                {
                    objp.EmpCode = Empcode;
                    objp.Action = "3";
                    DataTable dt = objL.EmployeeDetails(objp, "proc_EmployeeDetails");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objp.EmployeeName = dt.Rows[0]["Employee_Name"].ToString();
                        objp.FatherName = dt.Rows[0]["Employee_Father_Name"].ToString();
                        objp.strgender = dt.Rows[0]["Gender"].ToString();
                        objp.eDate = dt.Rows[0]["Date_of_Joining"].ToString();
                        objp.designation = dt.Rows[0]["Designation"].ToString();
                        objp.workemailid = dt.Rows[0]["WorkEmailId"].ToString();
                        objp.EPFNo = dt.Rows[0]["EPFNo"].ToString();
                        objp.ESINo = dt.Rows[0]["ESINo"].ToString();
                        objp.annuallyctc = Convert.ToDecimal(dt.Rows[0]["AnnuallyCTC"].ToString()==""?"0": dt.Rows[0]["AnnuallyCTC"].ToString());
                        objp.BasicPer = Convert.ToDecimal(dt.Rows[0]["BasicPer"].ToString()==""?"0": dt.Rows[0]["BasicPer"].ToString());
                        objp.HRAPer = Convert.ToDecimal(dt.Rows[0]["HRAPer"].ToString()==""?"0": dt.Rows[0]["HRAPer"].ToString());
                        objp.conveyanceallowancemonthly = Convert.ToDecimal(dt.Rows[0]["ConveyanceAllowanceMonthly"].ToString()==""?"0": dt.Rows[0]["ConveyanceAllowanceMonthly"].ToString());
                        objp.fixedallowancemonthly = Convert.ToDecimal(dt.Rows[0]["FixedAllowanceMonthly"].ToString()==""?"0": dt.Rows[0]["FixedAllowanceMonthly"].ToString());
                        objp.EmailId = dt.Rows[0]["PersonalEmailId"].ToString();
                        objp.ContactNo = dt.Rows[0]["Contact_Number"].ToString();
                        objp.mDate = dt.Rows[0]["DOB"].ToString();
                        objp.CityName = dt.Rows[0]["City"].ToString();
                        objp.StateName = dt.Rows[0]["State"].ToString();
                        objp.Address = dt.Rows[0]["Address"].ToString();
                        objp.PinCode = dt.Rows[0]["PinCode"].ToString();

                        objp.AccountNumber = dt.Rows[0]["AccountNumber"].ToString();
                        objp.AccountName = dt.Rows[0]["AccountHolderName"].ToString();
                        objp.BanKAccName = dt.Rows[0]["BankName"].ToString();
                        objp.ifsccode = dt.Rows[0]["IFSCCode"].ToString();
                        objp.AccountType = dt.Rows[0]["AccountType"].ToString();
                        objp.EmpCode = dt.Rows[0]["EmployeeCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult InserEmployeeDetails(PropertyClass p)
        {

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
                p.BranchCode = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.AddEmployee(p, "proc_AddEmployee");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["Id"].ToString();
                    objp.AccCode = dt.Rows[0]["EmpCode"].ToString();
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
        public ActionResult EmployeeList()
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
                if (Convert.ToString(Session["Role"]) == "1")
                {
                    objp.CompanyCode = null;
                }
                objp.Action = "1";
                objp.dt = objL.EmployeeDetails(objp, "proc_EmployeeDetails");
                ViewBag.Branchlist = PropertyClass.BindDDL(objL.GetBranch());
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult EmployeeProfile()
        {
            try
            {
                if (Request.QueryString["EmpCode"] != null)
                {
                    if (Convert.ToString(Session["Role"]) == "2")
                    {
                        objp.CompanyCode = Convert.ToString(Session["UserName"]);
                    }
                    else
                    {
                        objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    }
                    if (Convert.ToString(Session["Role"]) == "1")
                    {
                        objp.CompanyCode = null;
                    }

                    objp.EmpCode = Convert.ToString(Request.QueryString["EmpCode"]);
                    objp.Action = "2";
                    objp.dt = objL.EmployeeDetails(objp, "proc_EmployeeDetails");
                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public JsonResult AllotBranch(string branchid,string EmpId)
        {
            string msg = "";
            objp.EmpCode = EmpId;
            objp.CompanyCode = branchid;
            objp.Action = "4";
            DataTable dt=objL.EmployeeDetails(objp, "proc_EmployeeDetails");
            if(dt!=null && dt.Rows.Count > 0)
            {
                msg = dt.Rows[0]["msg"].ToString();
            }
            else
            {
                msg = "Unable to Update branch!Server not responding";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DesignationMaster()
        {
            try
            {
                objp.Action = "2";
                objp.dt = objL.DesignationMaster(objp, "proc_DesignationMaster");
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult InserUpdateDesignation(PropertyClass p)
        {
            string msg = "";
            try
            {
                DataTable dt = objL.DesignationMaster(p, "proc_DesignationMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["Id"].ToString();
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
        public JsonResult GetDesignationDetailsForEdit(string ItemCode1)
        {
            try
            {
                objp.Action = "3";
                objp.AccCode = ItemCode1.Trim();
                DataTable dt = objL.DesignationMaster(objp, "proc_DesignationMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.ItemCode = dt.Rows[0]["DesignationCode"].ToString();
                    objp.UOM = dt.Rows[0]["DesignationName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

    }
}