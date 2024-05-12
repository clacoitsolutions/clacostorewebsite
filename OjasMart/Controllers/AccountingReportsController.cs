using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class AccountingReportsController : Controller
    {
        // GET: AccountingReports

        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        public ActionResult DayBookReport(string StartDate, string EndDate, string CompanyCode, string BillBy)
        {
            try
            {
                objp.mDate = !string.IsNullOrEmpty(StartDate) ? StartDate : DateTime.Today.ToString("dd-MMM-yyyy");
                objp.eDate = !string.IsNullOrEmpty(EndDate) ? EndDate : DateTime.Today.ToString("dd-MMM-yyyy");
                if (Session["Role"].ToString() == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (!string.IsNullOrEmpty(CompanyCode))
                {
                    objp.CompanyCode = !string.IsNullOrEmpty(CompanyCode) ? CompanyCode : null;
                }
                if (Convert.ToString(Session["Role"]) == "1" || Convert.ToString(Session["Role"]) == "2")
                {
                    if (!string.IsNullOrEmpty(BillBy))
                    {
                        objp.BranchCode = BillBy;
                    }
                    else
                    {
                        objp.BranchCode = null;
                    }
                }
                else
                {
                    objp.BranchCode = Convert.ToString(Session["UserName"]);
                }
                // ViewBag.SSList = PropertyClass.BindDDL(objL.BindBranchListSearch(objp.CompanyCode));
                ViewBag.SSList = PropertyClass.BindDDL(objL.BindStockiestNewDropDown(objp.CompanyCode));
                objp.Action = "1";
                objp.dt = objL.BindDayBookReport(objp, "proc_DayBookReport");

                decimal CrAmount = 0, DrAmount = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        CrAmount += Convert.ToString(dr["Payment"]) != "" ? Convert.ToDecimal(dr["Payment"]) : 0;
                        DrAmount += Convert.ToString(dr["Revived"]) != "" ? Convert.ToDecimal(dr["Revived"]) : 0;
                    }
                }
                objp.NetTotal = CrAmount;
                objp.CoupenAmount = DrAmount;

                objp.Action = "2";
                DataSet ds = objL.BindDayBookData(objp, "proc_DayBookReport");
                if (ds != null)
                {
                    // Cash
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objp.CashOpeningBal = ds.Tables[0].Rows[0][0].ToString();
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        objp.CashClosingBal = ds.Tables[1].Rows[0][0].ToString();
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        objp.CashRecived = ds.Tables[2].Rows[0][0].ToString();
                    }
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        objp.CashPayment = ds.Tables[3].Rows[0][0].ToString();
                    }

                    // bank

                    if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                    {
                        objp.BankOpeningBal = ds.Tables[4].Rows[0][0].ToString();
                    }
                    if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                    {
                        objp.BankClosingBal = ds.Tables[5].Rows[0][0].ToString();
                    }
                    if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                    {
                        objp.BankRecived = ds.Tables[6].Rows[0][0].ToString();
                    }
                    if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                    {
                        objp.BankPayment = ds.Tables[7].Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult GenerateReceiptVoucher()
        {
            try
            {
                string CompanyCode = null, BranchCode = null;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "3")
                {
                    BranchCode = Convert.ToString(Session["UserName"]);
                }
                objp.CompanyCode = CompanyCode;
                // ViewBag.BranchList = PropertyClass.BindDDL(objL.BindBranchList(CompanyCode));
                ViewBag.BranchList = PropertyClass.BindDDL(objL.BindStockiestNewDropDown(objp.CompanyCode));
                ViewBag.AccountList = PropertyClass.BindDDL(objL.BindBranchAccountHeadList(CompanyCode, BranchCode));
                objp.mDate = DateTime.Now.ToString("dd-MMM-yyyy");
                objp.FinancialYear = objL.GetCurrentFinancialYear();
                ViewBag.ModeList = PropertyClass.BindDDL(objL.BindModeDropDown1());
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult GeneratePaymentVoucher()
        {
            try
            {
                string CompanyCode = null, BranchCode = null;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "3")
                {
                    BranchCode = Convert.ToString(Session["UserName"]);
                }
                objp.CompanyCode = CompanyCode;
                // ViewBag.BranchList = PropertyClass.BindDDL(objL.BindBranchList(CompanyCode));
                ViewBag.BranchList = PropertyClass.BindDDL(objL.BindStockiestNewDropDown(CompanyCode));
                ViewBag.AccountList = PropertyClass.BindDDL(objL.BindBranchAccountHeadList(CompanyCode, BranchCode));
                objp.mDate = DateTime.Now.ToString("dd-MMM-yyyy");
                objp.FinancialYear = objL.GetCurrentFinancialYear();
                ViewBag.ModeList = PropertyClass.BindDDL(objL.BindModeDropDown1());
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult GenerateJournalVoucher()
        {
            try
            {
                string CompanyCode = null, BranchCode = null;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "3")
                {
                    BranchCode = Convert.ToString(Session["UserName"]);
                }
                objp.CompanyCode = CompanyCode;
               // ViewBag.BranchList = PropertyClass.BindDDL(objL.BindBranchList(CompanyCode));
                ViewBag.BranchList = PropertyClass.BindDDL(objL.BindStockiestNewDropDown(objp.CompanyCode));
                ViewBag.AccountList = PropertyClass.BindDDL(objL.BindBranchAccountHeadListAll(CompanyCode, BranchCode));
                objp.mDate = DateTime.Now.ToString("dd-MMM-yyyy");
                objp.FinancialYear = objL.GetCurrentFinancialYear();
                ViewBag.ModeList = PropertyClass.BindDDL(objL.BindModeDropDown1());
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public JsonResult InsertJournalVoucherDetails(string InvoiceDate, string FinancialYear, string PayMode, string AccountCode, string AccountName, decimal PaidAmount, string vType, string AccCode, string AccName, string narration)
        {
            string msg = "";
            try
            {

                objp.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                objp.FinancialYear = FinancialYear;
                objp.PayMode = PayMode;
                objp.AccountCode = AccountCode;
                objp.AccountName = AccountName;
                objp.PaidAmount = PaidAmount;
                objp.AccCode = AccCode;
                objp.AccName = AccName;
                objp.narration = narration;
                objp.AccountType = vType;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }

                if (Session["Role"].ToString() == "1")
                {
                    objp.BranchCode = Convert.ToString(Session["CompanyCode"]);
                }
                else
                {
                    objp.BranchCode = Convert.ToString(Session["UserName"]);
                }
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);

                string Proc_Name = "proc_InsertJournalVoucher";
                DataTable dt1 = objL.InsertJournalVoucherDetails(objp, Proc_Name);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    if (vType == "Journal")
                    {
                        objp.strId = "1";
                        objp.msg = "Journal Voucher Successfully Saved";
                        objp.InvoiceNo = dt1.Rows[0]["InvNo"].ToString();
                    }
                    else if (vType == "Contra")
                    {
                        objp.strId = "1";
                        objp.msg = "Contra Voucher Successfully Saved";
                        objp.InvoiceNo = dt1.Rows[0]["InvNo"].ToString();
                    }
                }
                else
                {
                    objp.msg = "0";
                }
            }
            catch (Exception ex)
            {
                objp.msg = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountBalance(string AccCode)
        {
            try
            {
                if (Session["Role"].ToString() == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                objp.AccountCode = AccCode;
                objp.Action = "1";
                DataTable dt = objL.BindAccountBalance(objp, "proc_GetAccBalance");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.WalletBalance = dt.Rows[0]["BalAmt"].ToString();
                    objp.OfferType = dt.Rows[0]["opening_type"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertVoucherDetails(string InvoiceDate, string FinancialYear, string PayMode, string AccountCode, string AccountName, decimal PaidAmount, string GroupCode, string ChqNo, string mDate, string BankAccName, string txnId, string eDate, List<AccLists1> ItemList, string vType)
        {
            string msg = "";
            try
            {
                objp.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                objp.FinancialYear = FinancialYear;
                objp.PayMode = PayMode;
                objp.ChqNo = ChqNo;
                objp.mDate = mDate;
                objp.eDate = eDate;
                objp.BanKAccName = BankAccName;
                objp.BankTransId = txnId;
                objp.GroupCode = GroupCode;
                objp.AccountCode = AccountCode;
                objp.AccountName = AccountName;
                objp.PaidAmount = PaidAmount;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Session["Role"].ToString() == "1")
                {
                    objp.BranchCode = Convert.ToString(Session["CompanyCode"]);
                }
                else
                {
                    objp.BranchCode = Convert.ToString(Session["UserName"]);
                }
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo");
                dt.Columns.Add("AccCode");
                dt.Columns.Add("AccName");
                dt.Columns.Add("Amount");
                dt.Columns.Add("TDSPer");
                dt.Columns.Add("TDSAmount");
                dt.Columns.Add("NetAmount");
                dt.Columns.Add("narration");
                int i = 0;
                foreach (var item in ItemList)
                {
                    i++;
                    dt.Rows.Add(i, item.AccCode, item.AccName, item.Amount, item.TDSPer, item.TDSAmount, item.NetAmount, item.narration);
                }
                string Proc_Name = null;
                if (vType == "R")
                {
                    Proc_Name = "proc_InsertReceiptVoucherNew";
                }
                else if (vType == "P")
                {
                    Proc_Name = "proc_InsertPaymentVoucherNew";
                }
                //DataTable dt1 = objL.InsertSalesOrder(objp, "Proc_InsertSalesOrder", dt);
                DataTable dt1 = objL.InsertVouchers(objp, Proc_Name, dt);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    if (vType == "R")
                    {
                        objp.strId = "1";
                        objp.msg = "Receipt Voucher Successfully Saved";
                        objp.InvoiceNo = dt1.Rows[0]["InvNo"].ToString();
                    }
                    else if (vType == "P")
                    {
                        objp.strId = "1";
                        objp.msg = "Payment Voucher Successfully Saved";
                        objp.InvoiceNo = dt1.Rows[0]["InvNo"].ToString();
                    }
                }
                else
                {
                    objp.msg = "0";
                }
            }
            catch (Exception ex)
            {
                objp.msg = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrialBalanceReport(string StartDate, string EndDate, string CompanyCode, string GroupCode, string AccountCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(StartDate))
                {

                }
                objp.mDate = !string.IsNullOrEmpty(StartDate) ? StartDate : DateTime.Today.ToString("dd-MMM-yyyy");
                objp.eDate = !string.IsNullOrEmpty(EndDate) ? EndDate : DateTime.Today.ToString("dd-MMM-yyyy");
                if (Session["Role"].ToString() == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (!string.IsNullOrEmpty(CompanyCode))
                {
                    objp.CompanyCode = !string.IsNullOrEmpty(CompanyCode) ? CompanyCode : null;
                }
                objp.GroupCode = GroupCode;
                objp.AccountCode = AccountCode;
                objp.Action = "1";
                objp.dt = objL.BindTrialBalanceReport(objp, "PROC_GETTRIALBALANCEREPORT");

                decimal CrAmount = 0, DrAmount = 0, OpenBal = 0, CloseBal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        CrAmount += Convert.ToString(dr["Cr"]) != "" ? Convert.ToDecimal(dr["Cr"]) : 0;
                        DrAmount += Convert.ToString(dr["Dr"]) != "" ? Convert.ToDecimal(dr["Dr"]) : 0;
                        OpenBal += Convert.ToString(dr["OpeningBalanceCash"]) != "" ? Convert.ToDecimal(dr["OpeningBalanceCash"]) : 0;
                        CloseBal += Convert.ToString(dr["CloseBal"]) != "" ? Convert.ToDecimal(dr["CloseBal"]) : 0;
                    }
                }
                objp.CrBal = CrAmount.ToString("0.00");
                objp.DrBal = CrAmount.ToString("0.00");
                objp.OpenBal = OpenBal.ToString("0.00");
                objp.CloseBal = CloseBal.ToString("0.00");

            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult LedgerDetail()
        {
            try
            {
                if (Request.QueryString["AccCode"] != null && Request.QueryString["fDate"] != null && Request.QueryString["eDate"] != null && Request.QueryString["AccName"] != null)
                {
                    objp.AccountCode = Convert.ToString(Request.QueryString["AccCode"]);
                    objp.mDate = Convert.ToString(Request.QueryString["fDate"]);
                    objp.eDate = Convert.ToString(Request.QueryString["eDate"]);
                    objp.AccountName = Convert.ToString(Request.QueryString["AccName"]);

                    if (Session["Role"].ToString() == "2")
                    {
                        objp.CompanyCode = Convert.ToString(Session["UserName"]);
                    }
                    else
                    {
                        objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    }

                    objp.Action = "3";
                    objp.dt = objL.BindTrialBalanceReport(objp, "PROC_GETTRIALBALANCEREPORT");
                    decimal CrAmount = 0, DrAmount = 0, OpenBal = 0, CloseBal = 0;
                    if (objp.dt != null && objp.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in objp.dt.Rows)
                        {
                            CrAmount += Convert.ToString(dr["CrAmount"]) != "" ? Convert.ToDecimal(dr["CrAmount"]) : 0;
                            DrAmount += Convert.ToString(dr["DrAmount"]) != "" ? Convert.ToDecimal(dr["DrAmount"]) : 0;
                            //OpenBal += Convert.ToString(dr["OpeningBalanceCash"]) != "" ? Convert.ToDecimal(dr["OpeningBalanceCash"]) : 0;
                            //CloseBal += Convert.ToString(dr["CloseBal"]) != "" ? Convert.ToDecimal(dr["CloseBal"]) : 0;
                        }
                    }
                    objp.CrBal = CrAmount.ToString("0.00");
                    objp.DrBal = DrAmount.ToString("0.00");
                    //objp.OpenBal = OpenBal.ToString("0.00");
                    //objp.CloseBal = CloseBal.ToString("0.00");

                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult AccountLedgerReport(string startDate, string EndDate, string AccountCode, string AccountName)
        {
            try
            {

                objp.AccountCode = AccountCode;
                objp.mDate = startDate;
                objp.eDate = EndDate;
                objp.AccountName = AccountName;
                if (Session["Role"].ToString() == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "3")
                {
                    objp.BranchCode = Convert.ToString(Session["UserName"]);
                }
                ViewBag.AccountList = PropertyClass.BindDDL(objL.BindBranchAccountHeadListAll(objp.CompanyCode, objp.BranchCode));
                objp.Action = "3";
                objp.dt = objL.BindTrialBalanceReport(objp, "PROC_GETTRIALBALANCEREPORT");
                decimal CrAmount = 0, DrAmount = 0, OpenBal = 0, CloseBal = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        CrAmount += Convert.ToString(dr["CrAmount"]) != "" ? Convert.ToDecimal(dr["CrAmount"]) : 0;
                        DrAmount += Convert.ToString(dr["DrAmount"]) != "" ? Convert.ToDecimal(dr["DrAmount"]) : 0;
                        //OpenBal += Convert.ToString(dr["OpeningBalanceCash"]) != "" ? Convert.ToDecimal(dr["OpeningBalanceCash"]) : 0;
                        //CloseBal += Convert.ToString(dr["CloseBal"]) != "" ? Convert.ToDecimal(dr["CloseBal"]) : 0;
                    }
                }
                objp.CrBal = CrAmount.ToString("0.00");
                objp.DrBal = DrAmount.ToString("0.00");
                //objp.OpenBal = OpenBal.ToString("0.00");
                //objp.CloseBal = CloseBal.ToString("0.00");


            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult ProfitLossStatement(string startDate, string EndDate, string Type)
        {
            try
            {


                objp.mDate = (!string.IsNullOrEmpty(startDate) ? startDate : DateTime.Today.ToString("dd-MMM-yyyy"));
                objp.eDate = (!string.IsNullOrEmpty(EndDate) ? EndDate : DateTime.Today.ToString("dd-MMM-yyyy"));
                if (Session["Role"].ToString() == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                objp.Action = "1";
                objp.dt = objL.GetPLReport(objp, "Proc_GetProfitLossStatement");

                decimal totalExp = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        totalExp += dr["Amount"].ToString() != "" ? Convert.ToDecimal(dr["Amount"].ToString()) : 0;
                    }
                    objp.NetTotal = totalExp;
                }
                objp.dt1 = objL.GetPLReport(objp, "Proc_GetProfitLossStatementIncome");
                decimal totalIncome = 0;
                if (objp.dt1 != null && objp.dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt1.Rows)
                    {
                        totalIncome += dr["Amount"].ToString() != "" ? Convert.ToDecimal(dr["Amount"].ToString()) : 0;
                    }
                    objp.PayableAmt = totalIncome;
                }
                bool IsProfit = totalIncome >= totalExp ? true : false;
                decimal totBal = 0, BalAmt = 0;
                if (IsProfit)
                {
                    totBal = totalIncome - totalExp;
                    BalAmt = totBal + totalExp;
                }
                else
                {
                    totBal = totalExp - totalIncome;
                    BalAmt = totBal + totalIncome;
                }
                objp.IsActive = IsProfit;
                objp.CloseBal = totBal.ToString();
                objp.todayPoAmt = BalAmt.ToString("0.00");
                if (!string.IsNullOrEmpty(Type))
                {
                    objp.Action = "2";
                    objp.dt2 = objL.GetPLReport(objp, "Proc_GetProfitLossStatement");
                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public ActionResult GetBalanceSheet(string startDate, string EndDate, string Type)
        {
            try
            {


                objp.mDate = (!string.IsNullOrEmpty(startDate) ? startDate : DateTime.Today.ToString("dd-MMM-yyyy"));
                objp.eDate = (!string.IsNullOrEmpty(EndDate) ? EndDate : DateTime.Today.ToString("dd-MMM-yyyy"));
                if (Session["Role"].ToString() == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                objp.Action = "1";
                objp.dt = objL.GetPLReport(objp, "proc_GetBalanceSheet");

                objp.Action = "2";
                objp.dt1 = objL.GetPLReport(objp, "proc_GetBalanceSheet");

                decimal totalExp = 0;
                if (objp.dt1 != null && objp.dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt1.Rows)
                    {
                        if (!Convert.ToString(dr["Bal"]).Contains('-'))
                        {
                            totalExp += dr["Bal"].ToString() != "" ? Convert.ToDecimal(dr["Bal"].ToString()) : 0;
                        }

                    }
                    objp.NetTotal = totalExp;
                }
                decimal totalIncome = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        if (!Convert.ToString(dr["Bal"]).Contains('-'))
                        {
                            totalIncome += dr["Bal"].ToString() != "" ? Convert.ToDecimal(dr["Bal"].ToString()) : 0;
                        }
                    }
                    objp.PayableAmt = totalIncome;
                }
                bool IsProfit = totalIncome >= totalExp ? true : false;
                decimal totBal = 0, BalAmt = 0;
                if (IsProfit)
                {
                    totBal = totalIncome - totalExp;
                    BalAmt = totBal + totalExp;
                }
                else
                {
                    totBal = totalExp - totalIncome;
                    BalAmt = totBal + totalIncome;
                }
                objp.IsActive = IsProfit;
                objp.CloseBal = totBal.ToString();
                objp.todayPoAmt = BalAmt.ToString("0.00");

            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult CashBookReport(string startDate, string EndDate, string BillBy)
        {
            try
            {
                objp.mDate = (!string.IsNullOrEmpty(startDate) ? startDate : DateTime.Today.ToString("dd-MMM-yyyy"));
                objp.eDate = (!string.IsNullOrEmpty(EndDate) ? EndDate : DateTime.Today.ToString("dd-MMM-yyyy"));
                if (Session["Role"].ToString() == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "1" || Convert.ToString(Session["Role"]) == "2")
                {
                    if (!string.IsNullOrEmpty(BillBy))
                    {
                        objp.BranchCode = BillBy;
                    }
                    else
                    {
                        objp.BranchCode = null;
                    }
                }
                else
                {
                    objp.BranchCode = Convert.ToString(Session["UserName"]);
                }
                ViewBag.SSList = PropertyClass.BindDDL(objL.BindBranchList(objp.CompanyCode));
                objp.Action = "1";
                objp.dt = objL.GetPLReport(objp, "proc_getCashBook");
                decimal totalIncome = 0, totalExp = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        if (Convert.ToString(dr["type"]) == "Dr")
                        {
                            totalIncome += dr["DrAmount"].ToString() != "" ? Convert.ToDecimal(dr["DrAmount"].ToString()) : 0;
                        }
                        else
                        {
                            totalExp += dr["DrAmount"].ToString() != "" ? Convert.ToDecimal(dr["DrAmount"].ToString()) : 0;
                        }
                    }
                    objp.PayableAmt = totalIncome;
                    objp.NetTotal = totalExp;
                }
                bool IsProfit = totalIncome >= totalExp ? true : false;
                decimal totBal = 0, BalAmt = 0;
                if (IsProfit)
                {
                    totBal = totalIncome - totalExp;
                    BalAmt = totBal + totalExp;
                }
                else
                {
                    totBal = totalExp - totalIncome;
                    BalAmt = totBal + totalIncome;
                }
                objp.IsActive = IsProfit;
                objp.CloseBal = totBal.ToString();
                objp.todayPoAmt = BalAmt.ToString("0.00");

            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        #region Generate Contra Voucher
        public ActionResult GenerateContraVoucher()
        {
            try
            {
                string CompanyCode = null, BranchCode = null;
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "3")
                {
                    BranchCode = Convert.ToString(Session["UserName"]);
                }
                objp.CompanyCode = CompanyCode;
                //ViewBag.BranchList = PropertyClass.BindDDL(objL.BindBranchList(CompanyCode));
               
                    ViewBag.BranchList = PropertyClass.BindDDL(objL.BindStockiestNewDropDown(CompanyCode));
                //  ViewBag.AccountList = PropertyClass.BindDDL(objL.BindCashBankAccountHeadList(CompanyCode, BranchCode));
                ViewBag.AccountList = PropertyClass.BindDDL(objL.BindBranchAccountHeadList(CompanyCode, BranchCode));
                objp.mDate = DateTime.Now.ToString("dd-MMM-yyyy");
                objp.FinancialYear = objL.GetCurrentFinancialYear();
                ViewBag.ModeList = PropertyClass.BindDDL(objL.BindModeDropDown1());
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }


        #endregion 
    }



    public class AccLists
    {
        public int SrNo { get; set; }
        public string AccCode { get; set; }
        public string AccName { get; set; }
        public decimal Amount { get; set; }
        public string narration { get; set; }
    }
    public class AccLists1
    {
        public int SrNo { get; set; }
        public string AccCode { get; set; }
        public string AccName { get; set; }
        public decimal Amount { get; set; }
        public decimal TDSPer { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string narration { get; set; }
    }
}