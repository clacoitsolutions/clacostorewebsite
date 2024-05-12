using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class OutLetController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: OutLet
        public ActionResult CreateOutLet()
        {
            ViewBag.SuperStocksList = PropertyClass.BindDDL(objL.BindSSDropDown());
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());

            if (Request.QueryString["OutLetCode"] != null)
            {
                objp.Action = "2";
                objp.OutLetCode = Convert.ToString(Request.QueryString["OutLetCode"]);
                DataTable dt = objL.GetOutLetDetails(objp, "Proc_GetOutLetList");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.SSCode = dt.Rows[0]["SSCode"].ToString();
                    objp.SSName = dt.Rows[0]["OutLetName"].ToString();
                    objp.OutLetCode = dt.Rows[0]["OutLetCode"].ToString();
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
        public JsonResult InsertUpdateOutLets(PropertyClass p)
        {
            string msg = "";
            try
            {               
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                DataTable dt = objL.InsertUpdateOutLets(p, "Proc_InsertUpdateOutLets");
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
        public ActionResult OutLetList()
        {
            objp.Action = "1";
            if(Session["Role"].ToString()=="2")
            {
                objp.EntryBy = Session["UserName"].ToString();
            }
            objp.dt = objL.GetOutLetDetails(objp, "Proc_GetOutLetList");
            return View(objp);
        }


        
    }
}