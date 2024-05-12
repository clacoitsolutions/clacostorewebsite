using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class ChequeClearanceController : Controller
    {
        // GET: ChequeClearance

        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        public ActionResult GetChequeReport(string FromDate, string Todate, string CustomerId, string txnId, string type)
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
                if (type != "1")
                {
                    objp.mDate = FromDate;
                    objp.eDate = Todate;
                    objp.CustomerId = (!string.IsNullOrEmpty(CustomerId)) ? CustomerId : null;
                    objp.Action = "1";
                    objp.dt = objL.GetChequeDetails(objp, "Proc_GetChqDetails");
                }
                else
                {
                    objp.txnId = txnId;
                    objp.Action = "2";
                    objp.dt1 = objL.GetChequeDetails(objp, "Proc_GetChqDetails");
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public JsonResult InsertChequeUpdateStatus(PropertyClass p)
        {
            try
            {
                string msg = "";
                DataTable dt = new DataTable();
                if (Session["Role"].ToString() == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                p.BranchCode = Convert.ToString(Session["UserName"]);
                p.EntryBy = Convert.ToString(Session["UserName"]);
                dt = objL.InsertChequeUpdateStatus(p, "Proc_ClearRejectChequeNew");
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
    }
}