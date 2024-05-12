using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class ProductManagementController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: ProductManagement
        public ActionResult GeneratePurchaseOrder()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.SupplierList = PropertyClass.BindDDL(objL.BindSupplierDropDown());
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
            ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
            ViewBag.ModeList = PropertyClass.BindDDL(objL.BindModeDropDown1());
            ViewBag.ModeList1 = PropertyClass.BindDDL(objL.BindBranchList(""));

            objp.dt = objL.BindItemheadDropDown1(Session["UserName"].ToString());
            ViewBag.ItemHeadList = PropertyClass.BindDDL1(objL.BindItemheadDropDown1(Session["UserName"].ToString()));

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
        public JsonResult InsertSupplierAccounts(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.Action = "1";
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                DataTable dt = objL.InsertSuppliersAccount(p, "Proc_CreateSuppliersAccount");
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
        public JsonResult GetItemHeadDetail(string ItemCode1, string AccType, string BillType,string VarId)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.ItemCode = ItemCode1;
                objp.VariationId = VarId;

                DataSet ds = new DataSet();
                ds=objL.GetItemDetailsForSimpleAndAttribute(objp, "proc_GetItemDetails");
                if (ds!=null && ds.Tables!=null && ds.Tables[0]!=null)
                {
                    // DataTable dt = objL.GetItemDetails(objp, "proc_GetItemDetails");
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (BillType == "Sale")
                        {
                            if (AccType.Trim() == "Retail")
                            {
                                objp.Rate = dt.Rows[0]["SaleRate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["SaleRate"].ToString()) : 0;
                                //objp.UOM = dt.Rows[0]["LooseUnitName"].ToString();
                            }
                            else
                            {
                                objp.Rate = dt.Rows[0]["SaleRate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["SaleRate"].ToString()) : 0;
                                //objp.UOM = dt.Rows[0]["BulkUnitName"].ToString();
                            }
                        }
                        if (BillType == "Purchase")
                        {
                            if (AccType.Trim() == "Retail")
                            {
                                objp.Rate = dt.Rows[0]["PurchaseRate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["PurchaseRate"].ToString()) : 0;
                                //objp.UOM = dt.Rows[0]["LooseUnitName"].ToString();
                            }
                            else
                            {
                                objp.Rate = dt.Rows[0]["PurchaseRate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["PurchaseRate"].ToString()) : 0;
                                //objp.UOM = dt.Rows[0]["BulkUnitName"].ToString();
                            }
                        }
                        objp.UOM = dt.Rows[0]["UnitName"].ToString();
                        objp.CGSTPer = dt.Rows[0]["CgtsRate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["CgtsRate"].ToString()) : 0;
                        objp.SGSTPer = dt.Rows[0]["Sgstrate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["Sgstrate"].ToString()) : 0;
                        objp.GSTPer = dt.Rows[0]["GSTRate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["GSTRate"].ToString()) : 0;

                        objp.CgstAmt = dt.Rows[0]["Sgstrate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["Sgstrate"].ToString()) : 0;
                        objp.SgstAmt = dt.Rows[0]["GSTRate"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["GSTRate"].ToString()) : 0;
                        objp.MRP = dt.Rows[0]["MRP"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["MRP"].ToString()) : 0;
                        objp.HSNCode = dt.Rows[0]["HSNCode"].ToString();

                        objp.ItemCode = ItemCode1;
                        objp.VariationId = dt.Rows[0]["VariationId"].ToString();
                        objp.Purchase_taxIncludeExclude = dt.Rows[0]["Purchase_TaxIncludeExclude"].ToString();
                        objp.Sale_taxIncludeExclude = dt.Rows[0]["Sale_TaxIncludeExclude"].ToString();
                        objp.ProductVariationsDetails = PropertyClass.ConvertDataTabletoString(ds.Tables[1]);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetItemRef()
        {
            try
            {
                objp.Action = "1";
                
                DataTable ds = new DataTable();
                ds = objL.BindItemheadDropDown1(Session["UserName"].ToString());
                if (ds != null && ds.Rows.Count>0)
                {
                    
                        
                        objp.ProductVariationsDetails = PropertyClass.ConvertDataTabletoString(ds);
                    
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        
        [ValidateInput(false)]
        public JsonResult InsertPurchaseOrder(string SupplierAccCode, string InvoiceDate, string ShipmentPref, string StateId, string DeliveryTo, string TermsCond, string notes, decimal DiscPer, decimal DiscAmt, decimal CgstAmt, decimal SgstAmt, decimal IgstAmt, decimal PayableAmt, decimal GrossPayable, decimal NetTotal, decimal Payablegst, string InvoiceNo, decimal RoundOffAmt, string PayMode, List<ItemDetails> ItemList)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.SupplierAccCode = SupplierAccCode;
                objp.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                objp.ShipmentPref = ShipmentPref;
                objp.StateId = 09;
                objp.DeliveryTo = DeliveryTo;
                objp.TermsCond = TermsCond;
                objp.notes = notes;
                objp.DiscPer = DiscPer;
                objp.DiscAmt = DiscAmt;
                objp.CgstAmt = CgstAmt;
                objp.SgstAmt = SgstAmt;
                objp.IgstAmt = IgstAmt;
                objp.PayableAmt = PayableAmt;
                objp.GrossPayable = GrossPayable;
                objp.NetTotal = NetTotal;
                objp.Payablegst = Payablegst;
                objp.InvoiceNo = InvoiceNo;
                objp.RoundOffAmt = RoundOffAmt;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.BranchCode = Convert.ToString(Session["CompanyCode"]);
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.PayMode = PayMode;

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("HSNCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Rate");
                dt.Columns.Add("DiscountPer");
                dt.Columns.Add("DiscountPer2");
                dt.Columns.Add("DiscountAmt");
                dt.Columns.Add("cgstPer");
                dt.Columns.Add("cgstAmt");
                dt.Columns.Add("sgstPer");
                dt.Columns.Add("sgstAmt");
                dt.Columns.Add("igstPer");
                dt.Columns.Add("igstAmt");
                dt.Columns.Add("TotalAmount");
                dt.Columns.Add("UOM");
                dt.Columns.Add("MRP");
                dt.Columns.Add("TaxableAmount");
                dt.Columns.Add("Profit");
                dt.Columns.Add("Profit_Per");
                dt.Columns.Add("varId");

                foreach (var item in ItemList)
                {
                    //dt.Rows.Add(item.ItemCode, item.HSNCode, item.Quantity, item.Rate, item.DiscountPer, item.DiscountPer2, item.DiscountAmt, item.cgstPer, item.cgstAmt, item.sgstPer, item.sgstAmt, item.igstPer, item.igstAmt, item.TotalAmount, item.UOM, item.MRP, item.TaxableAmount, item.Reason, item.Profit, item.Profit_Per);
                    dt.Rows.Add(item.ItemCode, item.HSNCode, item.Quantity, item.Rate, item.DiscountPer, item.DiscountPer2, item.DiscountAmt, item.cgstPer, item.cgstAmt, item.sgstPer, item.sgstAmt, item.igstPer, item.igstAmt, item.TotalAmount, item.UOM, item.MRP, item.TaxableAmount, item.Profit, item.Profit_Per,item.VariationId);
                }


                DataTable dt1 = objL.InsertPurchaseOrder(objp, "Proc_InsertPurchaseOrder", dt);
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

        public ActionResult TransferedStockPosition(string StartDate, string EndDate, string StoreCode) 
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            objp.Action = "2";
           
            if (Convert.ToString(Session["Role"].ToString()) == "2")
            {
               // objp.Action = "1"; //Old
                objp.Action = "3"; //Change By Ravi

                objp.CompanyCode = Convert.ToString(Session["UserName"]);
                objp.StCode = "Admin";
            }
            else
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
                objp.StCode = Convert.ToString(Session["UserName"]);
            }
            objp.Role = Convert.ToString(Session["Role"]);

            //objp.StCode = StoreCode==""?null: StoreCode;
            objp.Sdate = StartDate == "" ? null : StartDate;
            objp.Ldate = EndDate == "" ? null : EndDate;


            objp.dt = objL.GetStockDetails(objp, "Proc_StockDetail");


            ViewBag.StoreList = PropertyClass.BindDDL(objL.AllStore());


            return View(objp);
        }

        public ActionResult StockPosition(string StartDate,string EndDate,string StoreCode)
        {
            objp.Action = "2";
            if (Session["Role"].ToString() == "1")
            {
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.StCode = "Admin";
            }
           
            else
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
                objp.StCode = Convert.ToString(Session["UserName"]);
            }
            objp.Role = Convert.ToString(Session["Role"]);

            //objp.StCode = StoreCode==""?null: StoreCode;
            objp.Sdate = StartDate == "" ? null : StartDate;
            objp.Ldate = EndDate == "" ? null : EndDate;
            
            objp.dt = objL.GetStockDetails(objp, "Proc_StockDetail");


            ViewBag.StoreList  = PropertyClass.BindDDL(objL.AllStore());


            return View(objp);
        }
        public ActionResult PurchaseBillDetails()
        {
            objp.Action = "1";
            if (Convert.ToString(Session["UserName"])!=null)
            {
                objp.EntryBy = Convert.ToString(Session["UserName"]);
            }
         
            objp.dt = objL.GetPurchaseBillDetails(objp, "Proc_getPurchaseBills");
            decimal totgst = 0, totalAmt = 0;
            if (objp.dt != null && objp.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in objp.dt.Rows)
                {
                    totgst += dr["TotalPayableGST"].ToString() != "" ? Convert.ToDecimal(dr["TotalPayableGST"].ToString()) : 0;
                    totalAmt += dr["NetTotal"].ToString() != "" ? Convert.ToDecimal(dr["NetTotal"].ToString()) : 0;
                }

                objp.Payablegst = totgst;
                objp.NetTotal = totalAmt;
            }
            return View(objp);
        }

        public JsonResult PurchaseBillItemWiseDetails(string InvoiceNo)
        {
            PropertyClass model = new PropertyClass();
            DataTable dt = new DataTable();
            string CompanyCode = Convert.ToString(Session["CompanyCode"]);
            objp.Action = "2";
            objp.ItemCode = InvoiceNo;
            dt = objL.GetPurchaseBillDetails(objp, "Proc_getPurchaseBills");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyClass m = new PropertyClass();
                    m.ItemCode = dr["ItemCode"].ToString();
                    m.ItemName = dr["ItemName"].ToString();
                    m.HSNCode = dr["HSNCode"].ToString();
                    m.UOM = dr["Size"].ToString();
                    m.Quantity = dr["Quantity"].ToString() != "" ? Convert.ToDecimal(dr["Quantity"].ToString()) : 0;
                    m.Rate = dr["Rate"].ToString() != "" ? Convert.ToDecimal(dr["Rate"].ToString()) : 0;
                    m.DiscPer = dr["DiscountPer"].ToString() != "" ? Convert.ToDecimal(dr["DiscountPer"].ToString()) : 0;
                    m.DiscPer2 = dr["DiscountPer2"].ToString() != "" ? Convert.ToDecimal(dr["DiscountPer2"].ToString()) : 0;
                    m.DiscAmt = dr["DiscountAmount"].ToString() != "" ? Convert.ToDecimal(dr["DiscountAmount"].ToString()) : 0;
                    m.Payablegst = dr["TaxAmount"].ToString() != "" ? Convert.ToDecimal(dr["TaxAmount"].ToString()) : 0;
                    m.GrossPayable = dr["TaxableAmount"].ToString() != "" ? Convert.ToDecimal(dr["TaxableAmount"].ToString()) : 0;
                    m.NetTotal = dr["NetAmount"].ToString() != "" ? Convert.ToDecimal(dr["NetAmount"].ToString()) : 0;
                    model.poList.Add(m);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierAccDetail(string AccountCode)
        {
            DataTable dt = new DataTable();
            dt = objL.GetSupplierAccDetail(AccountCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.AccountCode = dt.Rows[0]["account_code"].ToString();
                objp.StCode = dt.Rows[0]["StateCode"].ToString();
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockTransfer()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            string userCode = "";
            userCode = Convert.ToString(Session["UserName"]);
            ViewBag.StockiestList = PropertyClass.BindDDL(objL.BindStockiestNewDropDown(userCode));
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
            // ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
            ViewBag.ItemHeadList = PropertyClass.BindDDL1(objL.BindItemheadDropDown1(Session["UserName"].ToString()));
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
        public ActionResult ReturnStock()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
            ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
            objp.eDate = DateTime.Today.ToString("dd-MMM-yyyy");
            return View(objp);
        }
        public JsonResult InsertReturnStock(string ReturnDate, List<ItemDetails> ItemList)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.InvoiceDate = Convert.ToDateTime(ReturnDate);
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.UserName = Convert.ToString(Session["CompanyCode"]);
                objp.SSCode = Convert.ToString(Session["UserName"]);
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Reason");

                foreach (var item in ItemList)
                {
                    dt.Rows.Add(item.ItemCode, item.Quantity, item.Reason);
                }
                if (dt == null || dt.Rows.Count > 0)
                {
                    objp.strId = "2";
                }
                DataTable dt1 = objL.InsertReturnStock(objp, "Proc_InsertReturnStock", dt);
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
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePurchaseBillDetail(string InvoiceNo)
        {
            try
            {
                string CompanyCode = null;
                string BranchCode = null;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    CompanyCode = Convert.ToString(Session["UserName"]);
                    BranchCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    BranchCode = Convert.ToString(Session["UserName"]);
                }

                objp.CompanyCode = CompanyCode;
                objp.BranchCode = BranchCode;

                objp.EntryBy = Convert.ToString(Session["UserName"]);
                objp.InvoiceNo = InvoiceNo;
                DataTable dt = objL.DeletePurchaseBillDetail(objp, "Proc_DeletePurchaseOrderDetail");
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

        public ActionResult ReceiveTransferStock(string ChallanNo)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            string userCode = "";
            userCode = Convert.ToString(Session["UserName"]);
            ViewBag.StockiestList = PropertyClass.BindDDL(objL.BindWareHouseDropDown(userCode));
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());

            //ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
            // ViewBag.ItemHeadList = PropertyClass.BindDDL1(objL.BindItemheadDropDown1(Session["UserName"].ToString()));
            ViewBag.ItemHeadList = PropertyClass.BindDDL1(objL.BindItemheadDropDown1(null));
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

        public string TransferedStockDetails(string ChallanNo)
        {
            string ReturnValue = "";
            objp.Action = "3";
            objp.InvoiceNo = ChallanNo;
            objp.UserName = Convert.ToString(Session["UserName"]);
            DataSet ds = new DataSet();
            ds = objL.BindStockDetails(objp, "Proc_GetDetailForStockTransfer");
            DataTable dtForStockdetails = new DataTable();
            DataTable dtForStockItemDetails = new DataTable();
            if (ds!=null && ds.Tables!=null && ds.Tables.Count>0)
            {
                if (ds.Tables[0].Rows[0]["NewID"] !=null && ds.Tables[0].Rows[0]["NewID"].ToString()== "EXIST")
                {
                    ReturnValue = "GENERATE|";
                }

               else if (ds.Tables[0].Rows[0]["NewID"] != null && ds.Tables[0].Rows[0]["NewID"].ToString() == "NOTEXIST")
                {
                    ReturnValue = "NOTEXIST|";
                }
                else
                {
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        ReturnValue = "True|" + PropertyClass.ConvertDtToJSON(ds.Tables[0]) + "|" + PropertyClass.ConvertDataTabletoString(ds.Tables[1]);
                    }
                    else
                    {
                        ReturnValue = "False|";
                    }
                }
            }

            
            return ReturnValue;
        }
        #region Account[09102020]
        public JsonResult GetBankAccounts(string GroupCode)
        {
            string CompanyCode = null, BranchCode = null;
            if (Convert.ToString(Session["Role"]) == "2")
            {
                CompanyCode = Convert.ToString(Session["UserName"]);
                BranchCode = Convert.ToString(Session["UserName"]);
            }
            else
            {
                CompanyCode = Convert.ToString(Session["CompanyCode"]);
                BranchCode = Convert.ToString(Session["CompanyCode"]);
            }
            if (Convert.ToString(Session["Role"]) == "3")
            {
                BranchCode = Convert.ToString(Session["UserName"]);
            }
            DataTable dt = objL.BindBankAccountsDropDown(CompanyCode, GroupCode, BranchCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow ds in dt.Rows)
                {
                    objp.ItemList.Add(new SelectListItem { Text = Convert.ToString(ds["account_name"]), Value = Convert.ToString(ds["account_code"]) });
                }
            }

            return Json(new SelectList(objp.ItemList, "Value", "Text"));
        }
        #endregion


        #region Divanshu Shakya

        #region Update Purchase Return

        public ActionResult UpdatePurchaseOrder(string PurchaseNo)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.SupplierList = PropertyClass.BindDDL(objL.BindSupplierDropDown());
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
            //ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());

            // objp.dt = objL.BindItemheadDropDown1(Session["UserName"].ToString());
            ViewBag.ItemHeadList = PropertyClass.BindDDL1(objL.BindItemheadDropDown1(Session["UserName"].ToString()));
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



            PropertyClass objR = new PropertyClass();
            try
            {
                DataTable ds = new DataTable();
                ds = objL.GetInvSearch(4, PurchaseNo);
                if (ds != null && ds.Rows.Count > 0)
                {
                    objR.msg = "1";
                   
                    objR.SupplierAccCode = Convert.ToString(ds.Rows[0]["SupplierAccountCode"]);
                    objR.InvoiceDate = Convert.ToDateTime(ds.Rows[0]["InvoiceDate"]);
                    objR.InvoiceNo = Convert.ToString(ds.Rows[0]["InvoiceNo"]);
                    objR.GrossPayable = Convert.ToDecimal(ds.Rows[0]["GrossPayable"]);
                    objR.Payablegst = Convert.ToDecimal(ds.Rows[0]["TotalPayableGST"]);
                    objR.NetTotal = Convert.ToDecimal(ds.Rows[0]["NetTotal"]);
                    objR.DiscAmt = Convert.ToDecimal(ds.Rows[0]["DiscountAmount"]);

                    //DataTable dss = new DataTable();
                    //dss = objL.GetInvSearch(5, PurchaseNo);
                    //if (dss != null && dss.Rows.Count > 0)
                    //{
                    //    objR.msg = "1";
                    //    objR.InvDetails = ConvertDataTabletoString(dss);
                    //    Session["InvDetails"] = objR.InvDetails;
                    //}
                    //else
                    //{
                    //    objR.msg = "0";
                    //}
                    

                }
                else
                {
                    objR.msg = "0";
                }
            }
            catch (Exception ex)
            {
                objR.msg = "0";
            }

            objp = objR;
            


            return View(objp);
        }

        public JsonResult GetPurchaseInvoiceDetails(string InvNo)
        {
            PropertyClass objR = new PropertyClass();
            try
            {
                DataTable ds = new DataTable();
                ds = objL.GetInvSearch(4, InvNo);
                if (ds != null && ds.Rows.Count > 0)
                {
                    objR.msg = "1";

                    DataTable dss = new DataTable();
                    dss = objL.GetInvSearch(5, InvNo);
                    if (dss != null && dss.Rows.Count > 0)
                    {
                        objR.msg = "1";

                        objR.InvDetails = ConvertDataTabletoString(dss);

                    }
                    else
                    {
                        objR.msg = "0";
                    }





                }
                else
                {
                    objR.msg = "0";
                }
            }
            catch (Exception ex)
            {
                objR.msg = "0";
            }
            return Json(objR, JsonRequestBehavior.AllowGet);
        }

        public static string ConvertDataTabletoString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        [ValidateInput(false)]
        public JsonResult UpdatePurchaseOrderDetails(string PONo, string SupplierAccCode, string InvoiceDate, string ShipmentPref, string StateId, string DeliveryTo, string TermsCond, string notes, decimal DiscPer, decimal DiscAmt, decimal CgstAmt, decimal SgstAmt, decimal IgstAmt, decimal PayableAmt, decimal GrossPayable, decimal NetTotal, decimal Payablegst, string InvoiceNo, decimal RoundOffAmt, string PayMode, List<ItemDetails> ItemList)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.PONo = PONo;
                objp.SupplierAccCode = SupplierAccCode;
                objp.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                objp.ShipmentPref = ShipmentPref;
                objp.StateId = 09;
                objp.DeliveryTo = DeliveryTo;
                objp.TermsCond = TermsCond;
                objp.notes = notes;
                objp.DiscPer = DiscPer;
                objp.DiscAmt = DiscAmt;
                objp.CgstAmt = CgstAmt;
                objp.SgstAmt = SgstAmt;
                objp.IgstAmt = IgstAmt;
                objp.PayableAmt = PayableAmt;
                objp.GrossPayable = GrossPayable;
                objp.NetTotal = NetTotal;
                objp.Payablegst = Payablegst;
                objp.InvoiceNo = InvoiceNo;
                objp.RoundOffAmt = RoundOffAmt;
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.BranchCode = Convert.ToString(Session["CompanyCode"]);
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.PayMode = PayMode;

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("HSNCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Rate");
                dt.Columns.Add("DiscountPer");
                dt.Columns.Add("DiscountPer2");
                dt.Columns.Add("DiscountAmt");
                dt.Columns.Add("cgstPer");
                dt.Columns.Add("cgstAmt");
                dt.Columns.Add("sgstPer");
                dt.Columns.Add("sgstAmt");
                dt.Columns.Add("igstPer");
                dt.Columns.Add("igstAmt");
                dt.Columns.Add("TotalAmount");
                dt.Columns.Add("UOM");
                dt.Columns.Add("MRP");
                dt.Columns.Add("TaxableAmount");
                dt.Columns.Add("Profit");
                dt.Columns.Add("Profit_Per");
                dt.Columns.Add("varId");

                foreach (var item in ItemList)
                {
                    //dt.Rows.Add(item.ItemCode, item.HSNCode, item.Quantity, item.Rate, item.DiscountPer, item.DiscountPer2, item.DiscountAmt, item.cgstPer, item.cgstAmt, item.sgstPer, item.sgstAmt, item.igstPer, item.igstAmt, item.TotalAmount, item.UOM, item.MRP, item.TaxableAmount, item.Reason, item.Profit, item.Profit_Per);
                    dt.Rows.Add(item.ItemCode, item.HSNCode, item.Quantity, item.Rate, item.DiscountPer, item.DiscountPer2, item.DiscountAmt, item.cgstPer, item.cgstAmt, item.sgstPer, item.sgstAmt, item.igstPer, item.igstAmt, item.TotalAmount, item.UOM, item.MRP, item.TaxableAmount, item.Profit, item.Profit_Per, item.VariationId);
                }


                DataTable dt1 = objL.InsertPurchaseOrder(objp, "Proc_UpdatePurchaseOrder", dt);
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





        #endregion


        #endregion




        #region Maya Maurya
        [ValidateInput(false)]
        public JsonResult UpdateDataFromProductSale(string barCode, string VarId, string MRP, int Action, string ItemCode)
        {
            decimal C_Mrp = 0;
            decimal.TryParse(Convert.ToString(MRP), out C_Mrp);

            DataTable dt = new DataTable();
            try
            {
                dt = objL.UpdateDataFromProductSale(barCode, VarId, C_Mrp, Action, ItemCode);
                if (dt != null && dt.Rows.Count > 0)
                { 
                        objp.Id = 1;
                        objp.msg = Convert.ToString(dt.Rows[0]["msg"]); 

                }
                else
                {
                    objp.Id = 0;
                    objp.msg = "record not found";
                }
            }
            catch (Exception ex)
            {
                objp.Id = 0;
                objp.msg = "something went wrong!!";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        } 
        #endregion













    }


    public class ItemDetails
    {
        public string ItemCode { get; set; }
        public string HSNCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountPer2 { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal cgstPer { get; set; }
        public decimal cgstAmt { get; set; }
        public decimal sgstPer { get; set; }
        public decimal sgstAmt { get; set; }
        public decimal igstPer { get; set; }
        public decimal igstAmt { get; set; }
        public decimal TotalAmount { get; set; }
        public string UOM { get; set; }
        public decimal MRP { get; set; }
        public decimal TaxableAmount { get; set; }
        public string Reason { get; set; }
        public string Profit { get; set; }
        public string Profit_Per { get; set; }
        public string VariationId { get; set; }

        public string OfferCode { get; set; }
        public decimal GstAmount { get; set; }
        public decimal GSTRate { get; set; }
        public decimal TotalGst { get; set; }
        public decimal regrate { get; set; }
        public decimal discount_amt { get; set; }
        public string color { get; set; }
        public string Size { get; set; }
    }

    public class FreeItemList
    {
        public string ItemCode { get; set; }
        public string qty { get; set; }
        public string varIdL { get; set; }
        


    }


    public class AppliedOfferList
    {
        public string OfferCode { get; set; }
    }
}