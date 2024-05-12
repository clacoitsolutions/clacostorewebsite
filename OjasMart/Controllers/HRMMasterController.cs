using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using OjasMart.Models;

namespace OjasMart.Controllers
{
    public class HRMMasterController : Controller
    {
        PropertyClass objP = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: HRMMaster
        public ActionResult AddEmployee()
        {
            return View();
        }
        public ActionResult EmployeeDetails() 
        {
            return View();
        }
        public JsonResult BindState()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            try
            {
                DataTable dt = new DataTable();
                objP.Action = "1";
                dt = objL.BindDropDowns(objP, "proc_BindDropdown");
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
        public JsonResult BindDepartments()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            try
            {
                DataTable dt = new DataTable();
                objP.Action = "2";
                dt = objL.BindDropDowns(objP, "proc_BindDropdown");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow ds in dt.Rows)
                    {
                        PropertyClass obj = new PropertyClass();
                        obj.Id = Convert.ToInt32(ds["Id"]);
                        obj.DepartmentName = Convert.ToString(ds["DepartmentName"]);
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
        public JsonResult BindRoles()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            try
            {
                DataTable dt = new DataTable();
                objP.Action = "3";
                dt = objL.BindDropDowns(objP, "proc_BindDropdown");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow ds in dt.Rows)
                    {
                        PropertyClass obj = new PropertyClass();
                        obj.Id = Convert.ToInt32(ds["Id"]);
                        obj.RoleName = Convert.ToString(ds["Role"]);
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UpdatePayroll()
        {
            return View();
        }
        public ActionResult UploadPayroll()
        {
            return View();
        }
        public ActionResult SalarySlip()
        {
            return View();
        }
        public ActionResult LeaveEntry()
        {
            return View();
        }
        public ActionResult LeaveDetails()
        {
            return View();
        }
        public ActionResult LeaveApproval()
        {
            return View();
        }

        //public ActionResult UploadImage(string MobileNo)
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        // HttpFileCollectionBase files = Request.Files;
        //        HttpPostedFileBase mainPic = Request.Files[0];
        //        string fileExt = Path.GetExtension(mainPic.FileName);
        //        string fName = MobileNo + "_" + DateTime.Now.Ticks + fileExt;
        //        var path = Path.Combine(Server.MapPath("../EmployeeImage"), fName);
        //        //objP.FirstName = EmpName;
        //        objP.MobileNo = MobileNo;
        //        objP.Action = "2";
        //        objP.PicPath = "../EmployeeImage/" + fName;
        //        int r = objL.updateEmployeeImage(objP);
        //        mainPic.SaveAs(path);


        //    }
        //    if (Request.IsAjaxRequest())
        //    {
        //        return Json("", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return View("AddEmployee");
        //    }
        //}
        //public ActionResult BindEmployees()
        //{
        //    List<PropertyClass> list = new List<PropertyClass>();
        //    try
        //    {
        //        objP.Action = "3";
        //        DataTable dt = objL.BindDropDowns(objP, "[Proc_EmployeeMaster]");
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                PropertyClass obj = new PropertyClass();
        //                obj.EmployeeCode = dr["EmployeeCode"].ToString();
        //                obj.FirstName = dr["EmpName"].ToString();
        //                obj.PicPath = dr["pic"].ToString();
        //                obj.DepartmentName = dr["DepartmentName"].ToString();
        //                obj.StateName = dr["State_name"].ToString();
        //                obj.FatherName = dr["FatherName"].ToString();
        //                obj.Gender = dr["Gender"].ToString();
        //                obj.MobileNo = dr["MobileNo"].ToString();
        //                obj.EmailId = dr["EmailId"].ToString();
        //                obj.Address = dr["Address"].ToString();
        //                obj.DtBirth = dr["DOB"].ToString();
        //                obj.jDate = dr["JoiningDate"].ToString();
        //                obj.Salary = dr["CTC"].ToString();
        //                obj.PrvCompany = dr["PrvCompany"].ToString();
        //                obj.TotalExp = dr["Experiance"].ToString();
        //                list.Add(obj);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult DetailedView(string EmpCode)
        //{
        //    PropertyClass obj = new PropertyClass();
        //    try
        //    {
        //        objP.EmployeeCode = EmpCode;
        //        objP.Action = "4";
        //        DataTable dt = objL.getEMployeeDetail(objP, "[Proc_EmployeeMaster]");
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            obj.EmployeeCode = dt.Rows[0]["EmployeeCode"].ToString();
        //            obj.EmpName = dt.Rows[0]["EmpName"].ToString();
        //            obj.PicPath = dt.Rows[0]["pic"].ToString();
        //            obj.DepartmentName = dt.Rows[0]["DepartmentName"].ToString();
        //            obj.StateName = dt.Rows[0]["State_name"].ToString();
        //            obj.FatherName = dt.Rows[0]["FatherName"].ToString();
        //            obj.Gender = dt.Rows[0]["Gender"].ToString();
        //            obj.MobileNo = dt.Rows[0]["MobileNo"].ToString();
        //            obj.EmailId = dt.Rows[0]["EmailId"].ToString();
        //            obj.Address = dt.Rows[0]["Address"].ToString();
        //            obj.DtBirth = dt.Rows[0]["DOB"].ToString();
        //            obj.jDate = dt.Rows[0]["JoiningDate"].ToString();
        //            obj.Salary = dt.Rows[0]["CTC"].ToString();
        //            obj.PrvCompany = dt.Rows[0]["PrvCompany"].ToString();
        //            obj.TotalExp = dt.Rows[0]["Experiance"].ToString();
        //            obj.FirstName = dt.Rows[0]["FirstName"].ToString();
        //            obj.LastName = dt.Rows[0]["LastName"].ToString();
        //            obj.StateId = dt.Rows[0]["StateId"].ToString() == "" ? 0 : int.Parse(dt.Rows[0]["StateId"].ToString());
        //            obj.CityName = dt.Rows[0]["City"].ToString();
        //            obj.AltMobileNo = dt.Rows[0]["AltMobileNo"].ToString();
        //        }

        //        DataTable dt1 = new DataTable();
        //        objP.Action = "6";
        //        dt1 = objL.getEMployeeDetail(objP, "[Proc_EmployeeMaster]");
        //        if (dt1 != null && dt1.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt1.Rows)
        //            {
        //                PropertyClass p = new PropertyClass();
        //                p.TicketId = dr["ProjectId"].ToString();
        //                p.LeadTitle = dr["ProjectTitle"].ToString();
        //                p.DtBirth = dr["StartDate"].ToString();
        //                p.jDate = dr["ExpDel"].ToString();
        //                p.Status = dr["Status"].ToString();
        //                p.projectmode = dr["ProjectMode"].ToString();
        //                p.CategoryName = dr["CategoryName"].ToString();
        //                obj.lst.Add(p);
        //            }
        //        }
        //        DataTable dt2 = new DataTable();
        //        objP.Action = "7";
        //        dt2 = objL.getEMployeeDetail(objP, "[Proc_EmployeeMaster]");
        //        if (dt2 != null && dt2.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt2.Rows)
        //            {
        //                PropertyClass p = new PropertyClass();
        //                p.TicketId = dr["TicketId"].ToString();
        //                p.TicketTitle = dr["Title"].ToString();
        //                p.TicketPriority = dr["Priority"].ToString();
        //                p.Status = dr["Status"].ToString();
        //                p.OnStatus = dr["OnStatus"].ToString();
        //                p.jDate = dr["TicketRaiseDate"].ToString();
        //                p.LeadTitle = dr["ProjectTitle"].ToString();
        //                obj.lst1.Add(p);
        //            }
        //        }
        //        objP.Action = "8";
        //        DataSet ds = objL.getEmpDetailsds(objP, "Proc_EmployeeMaster");
        //        if (ds != null)
        //        {
        //            obj.TotalProjectsCount = ds.Tables[0].Rows[0]["TotProject"].ToString() != "" ? int.Parse(ds.Tables[0].Rows[0]["TotProject"].ToString()) : 0;
        //            obj.TotalLeadsCount = ds.Tables[1].Rows[0]["TotTkt"].ToString() != "" ? int.Parse(ds.Tables[1].Rows[0]["TotTkt"].ToString()) : 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return View(obj);

        //}
    }
}