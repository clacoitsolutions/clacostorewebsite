using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OjasMart.Models
{
    public class PropertyClass
    {

        #region Mohd Nadeem
        public static List<SelectListItem> BindDDLwithbarcode(DataTable dt)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = Convert.ToString(item[1]),
                        Value = Convert.ToString(item[2])
                    });
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        }
        #endregion


        public static List<SelectListItem> BindDDL(DataTable dt)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = Convert.ToString(item[1]),
                        Value = Convert.ToString(item[0])
                    });
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        }
        public static string ConvertDataTabletoString(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
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
        public static string ConvertTableToList(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Hashtable[] pr = new Hashtable[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Hashtable ch = new Hashtable();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string columnName = Convert.ToString(dt.Columns[j]);
                        string columnValue = Convert.ToString(dt.Rows[i][columnName]);
                        ch.Add(columnName, columnValue);
                    }
                    pr[i] = ch;
                }
                return new JavaScriptSerializer().Serialize(pr);
            }
            return "False";
        }
        public static List<SelectListItem> BindDDL1(DataTable dt)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = Convert.ToString(item[2]),
                        Value = Convert.ToString(item[0])
                    });
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        }

        #region CRM
        public string EmployeeCode { get; set; }
        
       public string Sort { get; set; }
        public string EmployeeName { get; set; }
        public DateTime LeadDate { get; set; }
        public DateTime NextFollowUpDate { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string LeadId { get; set; }
        public string CustType { get; set; }
        public string startDate { get; set; }
        public string Dailydeal { get; set; }
        public string CatName1 { get; set; }
        public string  CatId1 { get; set; }
        public string SubCatId1 { get; set; }
         public string search { get; set; }
        public string EMPCodeNew { get; set; }
        public string EndDate { get; set; }
        public string LeadTitle { get; set; }
        public string CatId { get; set; }
        public string CompanyName { get; set; }
        //public string sort { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string dId { get; set; }
        public string DealName { get; set; }
        public string ToTime { get; set; }
        public string FromTime { get; set; }
        public string DisPrice { get; set; }
        public string Date { get; set; }
        public string closedby { get; set; }
        public decimal projectcost { get; set; }
        public decimal advanceamount { get; set; }
        public string leadby { get; set; }
        public string projectmode { get; set; }
        public bool isgst { get; set; }


        public string TotalReviews1 { get; set; }
        public string totalrow { get; set; }
        public string avg { get; set; }
        public string poor { get; set; }
        public string Average { get; set; }
        public string Good { get; set; }
        public string VeryGood { get; set; }
        public string Exellent { get; set; }



        public string attachment { get; set; }
        public string AddedDate { get; set; }
        public string chqddNo { get; set; }
        public DateTime chqddDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string TimeDuration { get; set; }
        public DateTime TransDate { get; set; }
        public string BankAccId { get; set; }
        public string AccountNumber { get; set; }
        public string BankBranch { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Taxtype11 { get; set; }
        public decimal NonTaxRate { get; set; }


        #endregion CRM

        #region HRM

        public Int64 LOPDays { get; set; }
        public Int64 ActualDays { get; set; }
        public string AccCode { get; set; }
        public string FatherName { get; set; }
        public string designation { get; set; }
        public string EmpCode { get; set; }
        public string AccName { get; set; }
        public string narration { get; set; }
        public string EPFNo { get; set; }
        public string ESINo { get; set; }
        public string strgender { get; set; }
        public string workemailid { get; set; }
        public decimal basicmonthly { get; set; }
        public decimal hramonthly { get; set; }
        public decimal conveyanceallowancemonthly { get; set; }
        public decimal fixedallowancemonthly { get; set; }
        public decimal monthlyctc { get; set; }
        public decimal annuallyctc { get; set; }
        public decimal BasicPer { get; set; }
        public decimal HRAPer { get; set; }

        public string Monthly_Salary_Based_On { get; set; }
        public string First_PayrollStartsOn { get; set; }

        public string MonthName { get; internal set; }
        public string Year { get; internal set; }
        public string WorkingDays { get; internal set; }


        #endregion HRM
        #region Manufacturing
        public static string ConvertDtToJSON(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                string[][] arr = new string[dt.Rows.Count][];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] cu = new string[dt.Columns.Count];

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        cu[j] = dt.Rows[i][j] == DBNull.Value ? "" : Convert.ToString(dt.Rows[i][j]);
                    }
                    arr[i] = cu;
                }
                string dtString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(arr);
                return dtString;
            }
            else
            {
                return "false";
            }
        }
        //km cls
        public DateTime ProductionDate { get; set; }
        public string ItemID { get; set; }
        public string strRawdata { get; set; }
        public string Rawmaterialid { get; set; }
        public string Rawquantity { get; set; }
        public string leakage { get; set; }
        public string Rawuom { get; set; }

        public string EntryDate { get; set; }
        public string InitialQty { get; set; }
        public string LowStock { get; set; }
        public string Servicetype { get; set; }
        public List<PropertyClass> BundleList { get; set; }


        #endregion Manufacturing

        public decimal ApplyMRPFrom { get; set; }
        public decimal ApplyMRPTo { get; set; }

        public List<PropertyClass> VarList { get; set; }
        public List<ClsProductDeatils> VarLists { get; set; }
        public string VoucherId { get; set; }
        public string otp { get; set; }
        public string BlogId { get; set; }
        public string PONo { get; set; }

        public DataSet dt22 { get; set; }
        public string vendorname { get; set; }
        public string AvgReviews { get; set; }
        public DataTable dtValueOffer {  get; set; }
        public string BlogImages { get; set; }
        public string TotalReviews { get; set; }
        public string FashionType { get; set; }
        public DataTable TableReview { get; set; }
        public DataSet dt01 { get; set; }
        public string AccountName { get; set; }
        public string FinancialYear { get; set; }
        public string close_to { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        //public int id { get; set; }
        public string parent_groupcode { get; set; }
        public string productQty { get; set; }
        public string AccountCode { get; set; }
         public string StockStatus { get; set; }
        public string AccountType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string OldPassword { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Action { get; set; }
        public string EntryBy { get; set; }
        public int Id { get; set; }
        public string MenuTitle { get; set; }
        public string Url { get; set; }
        public string iconClass { get; set; }
        public int Priority { get; set; }
        public string SubMenuTitle { get; set; }
        public int MainMenuId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string ItemCode { get; set; }
        public string ReceiveCode { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public string HSNCode { get; set; }
        public decimal PuchaseRate { get; set; }
        public decimal MRP { get; set; }
        public decimal GSTPer { get; set; }
        public decimal CessRate { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal SGSTPer { get; set; }
        public DateTime? MfgDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public string mDate { get; set; }
        public string eDate { get; set; }
        public string _Searchkey { get; set; }
        public decimal PendingQuantity { get; set; }
        public decimal TrfQuantity { get; set; }
        public decimal CoupenAmount { get; set; }
        public string iscoupenapplied { get; set; }
        public decimal deliverycharges { get; set; }
        public string Manufacturerid { get; set; }

        public string memberShipId { get; set; }
        public string ColorImg { get; set; }

        public string ColorImgMulti { get; set; }

        public string Positioncolorimg { get; set; }
        public string OutLetCode { get; set; }
        public string Comment { get; set; }
        public string ReasonId { get; set; }

        public string SSCode { get; set; }
        public string SSName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string paystatus { get; set; }

        public string EmailAddress { get; set; }
        public string PinCode { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string GstNo { get; set; }
        public string PanNo { get; set; }
        public string GstDoc { get; set; }
        public string PanDoc { get; set; }
        public string StCode { get; set; }
        public string TINNo { get; set; }

        public string Sdate { get; set; }
        public string Ldate { get; set; }
        public string ReturnDate { get; set; }
        [AllowHtml]
        public string ReturnReasondes { get; set; }
        public DataTable dtAddress { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string ProductSpacification { get; set; }
        public string CompanyCode { get; set; }
        public string UOM { get; set; }
        public string OrderId { get; set; }
        public string PartyStateCode { get; set; }
        public string Mtype { get; set; }
        public string Status { get; set; }
        public string deliveryboy { get; set; }
        public string deliveryboyReason { get; set; }

        public string ActiveStatus { get; set; }
        public string StockistName { get; set; }

        public string StockistId { get; set; }
        public string ItemBarCode { get; set; }
        public decimal PurchaseRate_Bulk { get; set; }
        public decimal PurchaseRate_Loose { get; set; }
        public decimal SaleRate_Bulk { get; set; }
        public decimal SaleRate_Loose { get; set; }
        public decimal StorePrice { get; set; }
        public decimal OnlinePrice { get; set; }

        //public decimal Gstper { get; set; }
        public decimal GstAmt { get; set; }
        public decimal Taxty { get; set; }


        public decimal Usedpoint { get; set; }
        public string BulkUOM { get; set; }
        public string LooseUOM { get; set; }
        public decimal BulkUOMQty { get; set; }
        public string CustomerId { get; set; }
        public string CardType { get; set; }
        public string msg { get; set; }
        public string WalletBalance { get; set; }
        public string strId { get; set; }

        //-------- DashBoard Count Parameters
        public string TotStokist { get; set; }
        public string TotOutlets { get; set; }
        public string TotPurchase { get; set; }
        public string TotDemands { get; set; }
        public string TotDelord { get; set; }
        public string TotDelivered { get; set; }
        public string todayTotDelivered { get; set; }
        public string Totcancelled { get; set; }
        public string todayTotcancelled { get; set; }
        public string TotReturned { get; set; }
        public string todayTotReturned { get; set; }
        public string TolProduct { get; set; }
        public string TolProducttoday { get; set; }
        public string todayPo { get; set; }
        public string todayPoAmt { get; set; }
        public string WalletBal { get; set; }
        public string CashBackBalance { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string Relation { get; set; }

        public string todayOnlineSale { get; set; }
        public string todayOnlineOrder { get; set; }
        public string todayOfflineSale { get; set; }
        public string todayOnlinePendOrd { get; set; }
        public string todayOnlineDelOrd { get; set; }
        public string OfferTitle { get; set; }
        public string OfferType { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string Address2 { get; set; }
        public decimal OnPurchaseAmount { get; set; }
        public decimal CashBackAmount { get; set; }
        public DateTime ValidStartDate { get; set; }
        public DateTime ValidEndDate { get; set; }
        public decimal Points { get; set; }
        public decimal AmountPerPoint { get; set; }
        public HttpPostedFileWrapper multipleImages { get; set; }
        public string locality { get; set; }
        public string landmark { get; set; }
        public string altmobileno { get; set; }
        public string IsFreeItem { get; set; }
        public string FreeItemCode { get; set; }
        public string FreeQty { get; set; }
        public string PurchaseBy { get; set; }
        public string IsCashbackApplied { get; set; }

        public string ProductName { get; set; }
        public string VendorCode { get; set; }
        public string MainCategoryCode { get; set; }
        public string SubCategoryCode { get; set; }
        public string ProductType { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string ColorCode { get; set; }
        public string SizeCode { get; set; }
        public string IsInventoryProduct { get; set; }
        public string RegularPrice { get; set; }
        public string SalePrice { get; set; }
        public string KeyWords { get; set; }
        public int CountCart { get; set; }
        public string TotalCountCart { get; set; }

        //
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
        public DataTable dtColor { get; set; }
        public string OfferCode { get; set; }
        public decimal OfferAmt { get; set; }
        //Size
        public string Size_Name { get; set; }
        public string Size_Value { get; set; }
        public DataTable dtSize { get; set; }

        #region  Parameters for Purchase Order
        public string BranchCode { get; set; }
        public string SupplierAccCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ShipmentPref { get; set; }
        public string PurchaseFile { get; set; }
        public string DeliveryTo { get; set; }
        public string TermsCond { get; set; }
        public string notes { get; set; }
        public decimal DiscPer { get; set; }
        public decimal DiscPer2 { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal CgstAmt { get; set; }

        public decimal SgstAmt { get; set; }
        public decimal IgstAmt { get; set; }
        public decimal PayableAmt { get; set; }
        public decimal GrossPayable { get; set; }
        public decimal NetTotal { get; set; }
        public decimal gstamount { get; set; }
        public decimal Payablegst { get; set; }
        public string IsFrieghtInclude { get; set; }
        public decimal FrieghtCharges { get; set; }
        public string InvoiceNo { get; set; }
        public string EwayBillNo { get; set; }
        public decimal RoundOffAmt { get; set; }
        public string PayMode { get; set; }
        public string ChqNo { get; set; }
        public DateTime ChqDate { get; set; }
        public string BanKAccName { get; set; }
        public string BankTransId { get; set; }
        public decimal PaidAmount { get; set; }
        public string txnId { get; set; }
        public string Purchase_taxIncludeExclude { get; set; }
        public string Sale_taxIncludeExclude { get; set; }
        public string InitialStockQty { get; set; }
        public string LowStockAlert { get; set; }

        public string PaytmTransId { get; set; }
        public string RespoCode { get; set; }
        public string RespMsg { get; set; }
        public string GateWayname { get; set; }
        public string Bankname { get; set; }

        public string AttributeId { get; set; }
        public string VariationId { get; set; }
        public string VariationList { get; set; }

        public string TransferCode { get; set; }

        public string ProductVariationsDetails { get; set; }

        #endregion

        public DataTable dt { get; set; }
        public DataTable dt1 { get; set; }
        public DataTable dt4 { get; set; }
        public DataTable dt3 { get; set; }
        public DataTable dt2 { get; set; }
        public DataTable dt5 { get; set; }
        public DataTable dtcombooffer { get; set; }
        public DataTable dtMainCategory { get; set; }
        public DataTable sizeList { get; set; }
        public DataTable ColorList { get; set; }
        public DataTable dtSStockist { get; set; }
        public DataTable dtPopupCat { get; set; }
        public DataTable dtLevel { get; set; }
        public DataTable dtLatest { get; set; }
        public DataTable dtCatProduct { get; set; }
        public DataTable dtTopBanner { get; set; }

        public DataTable dtMenuTop { get; set; }
        public DataTable dtMenuSub { get; set; }

        public DataTable dtPromocode { get; set; }
        public DataTable dtOffer { get; set; }



        //public string RefundAndreturnPolicy { get; set; }

        public string strOtp { get; set; }
        public string strmsg { get; set; }


        public string InvDetails { get; set; }


        public List<PropertyClass> poList = new List<PropertyClass>();
        internal DataTable cartCount;

        public PropertyClass()
        {
            this.ItemList = new List<SelectListItem>();
            this.SubCategoryitem = new List<SelectListItem>();
        }

        public List<SelectListItem> ItemList { get; set; }
        public List<SelectListItem> SubCategoryitem { get; set; }

        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public string Area { get; set; }
        public string DeliveryDays { get; set; }
        public string DelCharges { get; set; }
        public string MinDelCharges { get; set; }

        public string CompanyLogo { get; set; }
        public string AadhaarNo { get; set; }
        public string InvoicePrefix { get; set; }
        public bool IsLatterPad { get; set; }
        public string notetype { get; set; }
        public DateTime notedate { get; set; }
        public string invoicetype { get; set; }
        public decimal totalcessamount { get; set; }
        public string accountno { get; set; }
        public string ifsccode { get; set; }
        public string branchname { get; set; }


        public string ReturnAndRefundPolicy { get; set; }
        public string NoOfTimesRedeem { get; set; }


        public string SumAmt { get; set; }


        #region Accounting
        #region DayBookDashboard Proprty
        public string CashOpeningBal { get; set; }
        public string CashClosingBal { get; set; }
        public string CashRecived { get; set; }
        public string CashPayment { get; set; }
        public string BankOpeningBal { get; set; }
        public string BankClosingBal { get; set; }
        public string BankRecived { get; set; }
        public string BankPayment { get; set; }

        public string OpenBal { get; set; }
        public string CloseBal { get; set; }
        public string CrBal { get; set; }
        public string DrBal { get; set; }
        #endregion

        //MemberCard
        public string ProfilePicPath { get; set; }
        public string MemberBarCode { get; set; }
        public string CardValidFrom { get; set; }
        public string CardValidTo { get; set; }
        public string CardBarCode { get; set; }
        public HttpPostedFileBase ProfileImage { get; set; }
        public string UsedRefral { get;set; }
        #endregion

        public string apicall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
                return results;
            }
            catch
            {
                return "0";
            }
        }
        //public void sendsms(string MobileNo, string Message)
        //{
        //    try
        //    {
        //        string url = ConfigurationManager.AppSettings["smsRoute"].ToString();
        //        string result = apicall(url + "&number=" + MobileNo + "&message=" + Message);
        //    }
        //    catch
        //    {
        //    }
        //}




        public void SendSMS(string MobileNo, string Msg)
        {
            string strAPI = "";
            strAPI = "http://mysmsshop.in/http-api.php?username=prad11004&password=Saniya8957.&senderid=SIGMAS&route=1&number=" + MobileNo + "&message=" + Msg + "";

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(strAPI);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
        }
    }











    public class Imgcoloratt
    {
        public string attrcolorimg { get; set; }
    }

    public class DaliyDeal
    {
        public string Action { get; set; }

        public string dId { get; set; }
        public string DealName { get; set; }
        public string ToTime { get; set; }
        public string FromTime { get; set; }
        public string DisPrice { get; set; }
        public string Date { get; set; }
        public string Entryby { get; set; }
        

    }

    public class login
    {
        public string ContactNo { get; set; }
        public string Otp { get; set; }
        public string AccountType { get; set; }
    }

    public class Blogmaster
    {
        public int Action { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public string Image { get; set; }
        
        public string EntryBy { get; set; }
        public string PurchaseFile { get; set; }

        public string BlogImages { get; set; }
    }
    public class VariationList
    {
        public string StoreId { get; set; }
        public string variationId { get; set; }
        public string AttributeId { get; set; }
        public string RegularPrice { get; set; }
        public string SalePrice { get; set; }
    }
    public class MultiVarList
    {
        public string TermId { get; set; }
        public string TermName { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal GstPer { get; set; }
        public string GstType { get; set; }
        public decimal GstAmt { get; set; }
        public decimal SalePrice { get; set; }
        public string ItembarCode { get; set; }
        public string color { get; set; }
        public string Size { get; set; }
        public string ColorImg { get; set; }

        public string ColorImgMulti { get; set; }
        public string Quantity { get; set; }
        public string Positioncolorimg { get; set; }
    }
    public class ClsProductDeatils
    {
        public string ItemCode { get; set; }
    }
    public class ClsStoreDeatils
    {
        public DataTable dtStoreDeatils { get; set; }
    }
    public class ClsContactUs
    {
        public string Action { get; set; }
        public string SrNo { get; set; }
        public string FullName { get; set; }
        public string mobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DataTable dtContactUs { get; set; }
    }
    public class ClsApplyFranchise
    {
        public string Action { get; set; }
        public string SrNo { get; set; }
        public string FranchiseName { get; set; }
        public string OwnerName { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
    }

    public class ClsMemberShip
    {
        public string Action { get; set; }
        public string SrNo { get; set; }
        public string CompanyName { get; set; }
        public string CardId { get; set; }
        public string CardType { get; set; }
        public string CustomerName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string MemberShip { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string ProfilePicPath { get; set; }
        public string MemberBarCode { get; set; }
        public string CardValidFrom { get; set; }
        public string CardValidTo { get; set; }
        public HttpPostedFileBase ProfileImage { get; set; }
        public DataTable dtmemberShipReport { get; set; }

    }


    public class SalesPerson
    {
        public string StrId { get; set; }
        public string Action { get; set; }
        public string msg { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string AadharNo { get; set; }
        public string Pincode { get; set; }
        public string Emailid { get; set; }
        public string SalesPersonId { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string VolunteerId { get; set; }
        public DataTable dt { get; set; }
        public string itemlist { get; set; }
        public string fromdate { get; set; }
        public string toDate { get; set; }
    }

    public class CommonRes
    {
        public string Id { get; set; }
        public string msg { get; set; }
    }

    public class commonResponse
    {
        public bool Status { get; set; }
        public string msg { get; set; }
    }

    public class clsearchOrder
    {
        public string _Action { get; set; }
        public string _FromDate { get; set; }
        public string _ToDate { get; set; }
        public string _OrderId { get; set; }
        public string _OrderStatus { get; set; }
        public string _PayMode { get; set; }
        public string ddlOrder { get; set; }
        public string ddlcnclrturn { get; set; }
        public string customerId { get; set; }

        public string VendorList { get; set; }
        public DataTable _dtOrder { get; set; }
    }
    //public class cls_Color 
    //{
    //    public string ColorName { get; set; }
    //    public string ColorValue { get; set; }
    //    public DataTable dtColor { get; set; }
    //}
    public class VendorDetails
    {
        public string VendorId { get; set; }
        public string Mobile { get; set; }
        public string VendorName { get; set; }
        public string VendorStoreName { get; set; }
        public string VendorGSTNo { get; set; }
        public string pincode { get; set; }
        public string Address { get; set; }
        public string StateId { get; set; }
        public string CityId { get; set; }
        public string AdharNo { get; set; }
        public string PanNo { get; set; }
        public string BussnessStartDate { get; set; }
        public string AboutStore { get; set; }
        public string CourierStatus { get; set; }
        public string StoreImage { get; set; }
        public List<VendorDetails> vdlst { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
    // request model for service

    public class ServiceRequestModel
    {
        public string Action { get; set; }
        public string SrNo { get; set; }
        public string ServiceType { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool ISACTIVE { get; set; }
        public DateTime Date { get; set; }

        public DataTable dtServiceRequestModel { get; set; }
    }

    //Added by Tanu Gupta
    public class ClsVendorContactUs
    {
        public string Action { get; set; }
        public string Name { get; set; }
        public string mobileNo { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DataTable dtVenContactUs { get; set; }
        public DateTime Date { get; internal set; }
    }

}
