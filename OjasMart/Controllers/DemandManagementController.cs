using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class DemandManagementController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: DemandManagement
        public ActionResult MakeDemand()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.StockiestList = PropertyClass.BindDDL(objL.BindStockiestDropDown());
            ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
            objp.eDate = DateTime.Today.ToString("dd-MMM-yyyy");
            if (Session["Role"].ToString() == "1")
            {
                objp.UserName = Convert.ToString(Session["CompanyCode"]);
            }
            else
            {
                objp.UserName = Convert.ToString(Session["UserName"]);
            }
            DataTable dt = objL.GetLogingCompanyStateCode(objp, "getLoginCompanyDetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.StateName = dt.Rows[0]["StateCode"].ToString();
            }
            return View(objp);
        }
        public JsonResult GetStockiestAccDetail(string AccountCode)
        {
            objp.UserName = AccountCode;
            DataTable dt = objL.GetLogingCompanyStateCode(objp, "getLoginCompanyDetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                //objp.AccountCode = dt.Rows[0]["account_code"].ToString();
                objp.StCode = dt.Rows[0]["StateCode"].ToString();
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertStockDemand(string DemandDate, string StockiestCode, decimal CgstAmt, decimal SgstAmt, decimal IgstAmt, decimal SubTotal, decimal NetTotal, List<ItemDetails> ItemList)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.InvoiceDate = Convert.ToDateTime(DemandDate);
                objp.SSCode = StockiestCode;
                objp.OutLetCode = Convert.ToString(Session["UserName"]);
                objp.CgstAmt = CgstAmt;
                objp.SgstAmt = SgstAmt;
                objp.IgstAmt = IgstAmt;
                objp.GrossPayable = SubTotal;
                objp.PayableAmt = NetTotal;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.BranchCode = Convert.ToString(Session["CompanyCode"]);
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Rate");
                dt.Columns.Add("cgstPer");
                dt.Columns.Add("cgstAmt");
                dt.Columns.Add("sgstPer");
                dt.Columns.Add("sgstAmt");
                dt.Columns.Add("igstPer");
                dt.Columns.Add("igstAmt");
                dt.Columns.Add("TotalAmount");

                foreach (var item in ItemList)
                {
                    dt.Rows.Add(item.ItemCode, item.Quantity, item.Rate, item.cgstPer, item.cgstAmt, item.sgstPer, item.sgstAmt, item.igstPer, item.igstAmt, item.TotalAmount);
                }

                DataTable dt1 = objL.InsertStockDemand(objp, "Proc_InsertDemandDetails", dt);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    msg = dt1.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StockDemandDetails()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            if (Session["Role"].ToString() == "1")
            {
                objp.UserName = Convert.ToString(Session["CompanyCode"]);
            }
            else
            {
                objp.UserName = Convert.ToString(Session["UserName"]);
            }
            objp.Action = "1";
            objp.Role = Convert.ToString(Session["Role"]);
            objp.dt = objL.GetStockDemandDetails(objp, "Proc_GetDemandList");
            decimal totgst = 0, totalAmt = 0, subTotal = 0;
            if (objp.dt != null && objp.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in objp.dt.Rows)
                {
                    totgst += dr["TotaltaxAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotaltaxAmount"].ToString()) : 0;
                    totalAmt += dr["TotalAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0;
                    subTotal += dr["SubTotal"].ToString() != "" ? Convert.ToDecimal(dr["SubTotal"].ToString()) : 0;
                }

                objp.Payablegst = totgst;
                objp.NetTotal = totalAmt;
                objp.GrossPayable = subTotal;
            }
            return View(objp);
        }
        public JsonResult StockDemandItemWiseDetails(string InvoiceNo)
        {
            PropertyClass model = new PropertyClass();
            DataTable dt = new DataTable();
            string CompanyCode = Convert.ToString(Session["CompanyCode"]);
            objp.Action = "2";
            objp.ItemCode = InvoiceNo;
            dt = objL.GetStockDemandDetails(objp, "Proc_GetDemandList");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyClass m = new PropertyClass();
                    m.ItemCode = dr["ItemCode"].ToString();
                    m.ItemName = dr["ItemName"].ToString();
                    m.HSNCode = dr["HSNCode"].ToString();
                    m.Quantity = dr["Quantity"].ToString() != "" ? Convert.ToDecimal(dr["Quantity"].ToString()) : 0;
                    m.PendingQuantity = dr["PendingQty"].ToString() != "" ? Convert.ToDecimal(dr["PendingQty"].ToString()) : 0;
                    m.TrfQuantity = dr["TrfQty"].ToString() != "" ? Convert.ToDecimal(dr["TrfQty"].ToString()) : 0;
                    m.Rate = dr["Rate"].ToString() != "" ? Convert.ToDecimal(dr["Rate"].ToString()) : 0;
                    m.Payablegst = dr["TotalTax"].ToString() != "" ? Convert.ToDecimal(dr["TotalTax"].ToString()) : 0;
                    m.NetTotal = dr["TotalAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0;
                    model.poList.Add(m);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferStock()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.StockiestList = PropertyClass.BindDDL(objL.BindStockiestDropDown());
            ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
            objp.eDate = DateTime.Today.ToString("dd-MMM-yyyy");
            if (Session["Role"].ToString() == "1")
            {
                objp.UserName = Convert.ToString(Session["CompanyCode"]);
            }
            else
            {
                objp.UserName = Convert.ToString(Session["UserName"]);
            }
            DataTable dt = objL.GetLogingCompanyStateCode(objp, "getLoginCompanyDetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.StateName = dt.Rows[0]["StateCode"].ToString();
            }
            if (Request.QueryString["DCode"] != null)
            {
                objp.OrderId = Convert.ToString(Request.QueryString["DCode"]);
                objp.Action = "1";
                DataTable dt1 = objL.GetStockTransferDetails(objp, "Proc_GetDetailForStockTransfer");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.SSName = dt1.Rows[0]["PartyName"].ToString();
                    objp.mDate = dt1.Rows[0]["demandDate"].ToString();
                    objp.OrderId = dt1.Rows[0]["DemandCode"].ToString();
                    objp.OutLetCode = dt1.Rows[0]["DemandBy"].ToString();
                    objp.SSCode = dt1.Rows[0]["StockiestCode"].ToString();
                    objp.PartyStateCode = dt1.Rows[0]["PartyStateCode"].ToString();
                }
                objp.Action = "2";
                objp.dt = objL.GetStockTransferDetails(objp, "Proc_GetDetailForStockTransfer");
            }
            return View(objp);
        }
        public JsonResult InsertStockTransferDetails(string TransferDate, string partyCode, decimal CgstAmt, decimal SgstAmt, decimal IgstAmt, decimal SubTotal, decimal NetTotal, decimal roundOff, string DemandCode,string TransferType, List<ItemDetails> ItemList)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.InvoiceDate = Convert.ToDateTime(TransferDate);
                objp.SSCode = partyCode;
                if (Session["Role"].ToString() == "1")
                {
                    objp.OutLetCode = Convert.ToString(Session["CompanyCode"]);
                }
                else
                {
                    objp.OutLetCode = Convert.ToString(Session["UserName"]);
                }
               
                objp.CgstAmt = CgstAmt;
                objp.SgstAmt = SgstAmt;
                objp.IgstAmt = IgstAmt;
                objp.GrossPayable = SubTotal;
                objp.PayableAmt = NetTotal;
                objp.RoundOffAmt = roundOff;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.BranchCode = Convert.ToString(Session["CompanyCode"]);
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.OrderId = DemandCode;
                objp.AccountType = TransferType;

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Rate");
                dt.Columns.Add("cgstPer");
                dt.Columns.Add("cgstAmt");
                dt.Columns.Add("sgstPer");
                dt.Columns.Add("sgstAmt");
                dt.Columns.Add("igstPer");
                dt.Columns.Add("igstAmt");
                dt.Columns.Add("TotalAmount");

                foreach (var item in ItemList)
                {
                    dt.Rows.Add(item.ItemCode, item.Quantity, item.Rate, item.cgstPer, item.cgstAmt, item.sgstPer, item.sgstAmt, item.igstPer, item.igstAmt, item.TotalAmount);
                }

                DataTable dt1 = objL.InsertStockTransferDetails(objp, "Proc_InsertStockTransferDetails", dt);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    //msg = dt1.Rows[0]["msg"].ToString();
                    msg = dt1.Rows[0]["Transfercode"].ToString();
                    
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }




        public JsonResult ReceiveStockTransferDetails(string transferCode, string ReceiveDate, string transferBy, decimal CgstAmt, decimal SgstAmt, decimal IgstAmt, decimal SubTotal, decimal NetTotal, decimal roundOff, string DemandCode, string TransferType, List<ItemDetails> ItemList)
        {
            string msg = "";
            try
            {
                objp.Action = "2";
                objp.InvoiceDate = Convert.ToDateTime(ReceiveDate);
               
                if (Session["Role"].ToString() == "1")
                {
                    objp.OutLetCode = Convert.ToString(Session["CompanyCode"]);
                }
                else
                {
                    objp.OutLetCode = Convert.ToString(Session["UserName"]);
                }
                objp.SSCode = objp.OutLetCode;
                objp.OutLetCode = transferBy;
                objp.TransferCode = transferCode;
                objp.CgstAmt = CgstAmt;
                objp.SgstAmt = SgstAmt;
                objp.IgstAmt = IgstAmt;
                objp.GrossPayable = SubTotal;
                objp.PayableAmt = NetTotal;
                objp.RoundOffAmt = roundOff;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.BranchCode = Convert.ToString(Session["CompanyCode"]);
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.OrderId = DemandCode;
                objp.AccountType = TransferType;

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Rate");
                dt.Columns.Add("cgstPer");
                dt.Columns.Add("cgstAmt");
                dt.Columns.Add("sgstPer");
                dt.Columns.Add("sgstAmt");
                dt.Columns.Add("igstPer");
                dt.Columns.Add("igstAmt");
                dt.Columns.Add("TotalAmount");

                foreach (var item in ItemList)
                {
                    dt.Rows.Add(item.ItemCode, item.Quantity, item.Rate, item.cgstPer, item.cgstAmt, item.sgstPer, item.sgstAmt, item.igstPer, item.igstAmt, item.TotalAmount);
                }

                DataTable dt1 = objL.InsertStockTransferDetails(objp, "Proc_InsertStockTransferDetails", dt);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    //msg = dt1.Rows[0]["msg"].ToString();
                    msg = dt1.Rows[0]["Receivecode"].ToString();

                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult StockTransferReport()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            if (Session["Role"].ToString() == "1")
            {
                objp.UserName = Convert.ToString(Session["CompanyCode"]);
            }
            else
            {
                objp.UserName = Convert.ToString(Session["UserName"]);
            }
            objp.Action = "1";
            objp.Role = Convert.ToString(Session["Role"]);
            objp.dt = objL.GetStockTransferReport(objp, "Proc_StockTransferDetails");
            decimal totgst = 0, totalAmt = 0, subTotal = 0;
            if (objp.dt != null && objp.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in objp.dt.Rows)
                {
                    totgst += dr["TotalTax"].ToString() != "" ? Convert.ToDecimal(dr["TotalTax"].ToString()) : 0;
                    totalAmt += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                    subTotal += dr["SubTotal"].ToString() != "" ? Convert.ToDecimal(dr["SubTotal"].ToString()) : 0;
                }

                objp.Payablegst = totgst;
                objp.NetTotal = totalAmt;
                objp.GrossPayable = subTotal;
            }
            return View(objp);
        }
        public JsonResult StockTransferItemWiseDetails(string InvoiceNo)
        {
            PropertyClass model = new PropertyClass();
            DataTable dt = new DataTable();
            string CompanyCode = Convert.ToString(Session["CompanyCode"]);
            objp.Action = "2";
            objp.StCode = InvoiceNo;
            dt = objL.GetStockTransferReport(objp, "Proc_StockTransferDetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyClass m = new PropertyClass();
                    m.ItemCode = dr["ItemCode"].ToString();
                    m.ItemName = dr["ItemName"].ToString();
                    m.HSNCode = dr["HSNCode"].ToString();
                    m.Quantity = dr["Quantity"].ToString() != "" ? Convert.ToDecimal(dr["Quantity"].ToString()) : 0;
                    m.Rate = dr["Rate"].ToString() != "" ? Convert.ToDecimal(dr["Rate"].ToString()) : 0;
                    m.Payablegst = dr["TotalTax"].ToString() != "" ? Convert.ToDecimal(dr["TotalTax"].ToString()) : 0;
                    m.NetTotal = dr["TotalAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0;
                    model.poList.Add(m);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckStockAvailibility(string ItemCode1,string VarId)
        {
            string msg = "";
            PropertyClass model = new PropertyClass();
            DataTable dt = new DataTable();
            objp.Action = "1";

            objp.ItemCode = ItemCode1;
            objp.Role = Convert.ToString(Session["Role"]);
            objp.VariationId = VarId;
            if (Session["Role"].ToString() == "1")
            {
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
            }
            if (Session["Role"].ToString()=="2")
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
                objp.Action = "2";
                objp.VariationId = null;
            }
            else
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
            }
            
          
            dt = objL.CheckStockBalance(objp, "Proc_GetStockBalance");
            if (dt != null && dt.Rows.Count > 0)
            {
                //msg = dt.Rows[0]["TotalUnit"].ToString();            
                msg = "1000";   // Change Stock Na ho tb bhi sale ho jaye     
            }
            else
            {
                msg = "1000";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult  ReceivedTranferedStock()
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            if (Session["Role"].ToString() == "1")
            {
                objp.UserName = Convert.ToString(Session["CompanyCode"]);
            }
            else
            {
                objp.UserName = Convert.ToString(Session["UserName"]);
            }
            objp.Action = "4";
            objp.Role = Convert.ToString(Session["Role"]);
            objp.dt = objL.GetStockTransferReport(objp, "Proc_StockTransferDetails");
            decimal totgst = 0, totalAmt = 0, subTotal = 0;
            if (objp.dt != null && objp.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in objp.dt.Rows)
                {
                    totgst += dr["TotalTax"].ToString() != "" ? Convert.ToDecimal(dr["TotalTax"].ToString()) : 0;
                    totalAmt += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                    subTotal += dr["SubTotal"].ToString() != "" ? Convert.ToDecimal(dr["SubTotal"].ToString()) : 0;
                }

                objp.Payablegst = totgst;
                objp.NetTotal = totalAmt;
                objp.GrossPayable = subTotal;
            }
            return View(objp);
            
        }

        public JsonResult StockReceivedItemWiseDetails(string InvoiceNo)
        {
            PropertyClass model = new PropertyClass();
            DataTable dt = new DataTable();
            string CompanyCode = Convert.ToString(Session["CompanyCode"]);
            objp.Action = "5";
            objp.StCode = InvoiceNo;
            dt = objL.GetStockTransferReport(objp, "Proc_StockTransferDetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyClass m = new PropertyClass();
                    m.ItemCode = dr["ItemCode"].ToString();
                    m.ReceiveCode = dr["ReceiveCode"].ToString();
                    m.ItemName = dr["ItemName"].ToString();
                    m.HSNCode = dr["HSNCode"].ToString();
                    m.Quantity = dr["Quantity"].ToString() != "" ? Convert.ToDecimal(dr["Quantity"].ToString()) : 0;
                    m.Rate = dr["Rate"].ToString() != "" ? Convert.ToDecimal(dr["Rate"].ToString()) : 0;
                    m.Payablegst = dr["TotalTax"].ToString() != "" ? Convert.ToDecimal(dr["TotalTax"].ToString()) : 0;
                    m.NetTotal = dr["TotalAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0;
                    model.poList.Add(m);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


    }
}