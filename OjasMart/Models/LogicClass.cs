using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace OjasMart.Models
{

    public class LogicClass
    {
        PropertyClass objp = new PropertyClass();
        dbHelper objdb = new dbHelper();
        public DateTime? ConvertStringDateTo_Datetime(string date)
        {
            DateTime? dt = new DateTime();

            if (date != "" && date != null)
            {
                string[] arrDate = date.Split('/');
                int year = Convert.ToInt32(arrDate[2]);
                int month = Convert.ToInt32(arrDate[1]);
                int day = Convert.ToInt32(arrDate[0]);
                dt = new DateTime(year, month, day);
            }
            else
            {
                dt = null;
            }

            return dt;
        }

        public DataTable getLoginDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName",Objp.UserName),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public int InsertMenuDetails(PropertyClass objP)
        {
            int r = 0;
            try
            {
                string sql = "Proc_MenuConfiguration";
                SqlParameter[] sqlParams = {
                new SqlParameter("@MenuId",SqlDbType.Int),
                new SqlParameter("@Menutitle",SqlDbType.VarChar),
                new SqlParameter("@MenuUrl",SqlDbType.VarChar),
                new SqlParameter("@MenuiconClass",SqlDbType.VarChar),
                new SqlParameter("@Priority",SqlDbType.Int),
                new SqlParameter("@SubMenuName",SqlDbType.Int),
                new SqlParameter("@Action",SqlDbType.VarChar),
                new SqlParameter("@MainMenuId",SqlDbType.Int)

            };
                sqlParams[0].Value = objP.Id;
                sqlParams[1].Value = objP.MenuTitle;
                sqlParams[2].Value = objP.Url;
                sqlParams[3].Value = objP.iconClass;
                sqlParams[4].Value = objP.Priority;
                sqlParams[5].Value = objP.SubMenuTitle;
                sqlParams[6].Value = objP.Action;
                sqlParams[7].Value = objP.MainMenuId;
                r = objdb.ExecuteNonQueryProc(sql, sqlParams);
                return r;
            }
            catch (Exception ex)
            {
                return r;
            }
        }
        public int InsertSubMenuDetails(PropertyClass objP)
        {
            int r = 0;
            try
            {
                string sql = "Proc_MenuConfiguration";
                SqlParameter[] sqlParams = {
                new SqlParameter("@mainmenuid",SqlDbType.Int),
                new SqlParameter("@submenuname",SqlDbType.VarChar),
                new SqlParameter("@MenuUrl",SqlDbType.VarChar),
                new SqlParameter("@priority",SqlDbType.Int),
                new SqlParameter("@MenuiconClass",SqlDbType.VarChar),
                new SqlParameter("@Action",SqlDbType.VarChar)
            };
                sqlParams[0].Value = objP.MainMenuId;
                sqlParams[1].Value = objP.SubMenuTitle;
                sqlParams[2].Value = objP.Url;
                sqlParams[3].Value = objP.Priority;
                sqlParams[4].Value = objP.iconClass;
                sqlParams[5].Value = objP.Action;
                r = objdb.ExecuteNonQueryProc(sql, sqlParams);
                return r;
            }
            catch (Exception ex)
            {
                return r;
            }
        }
        public DataTable BindMainMenu(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BindDropDowns(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertItemHead(PropertyClass Objp, string ProcName, DataTable dt12)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@itemname",Objp.ItemName),
                new SqlParameter("@batchno",Objp.BatchNo),
                new SqlParameter("@hsncode",Objp.HSNCode),
                new SqlParameter("@mrp",Objp.MRP),
                new SqlParameter("@gstper",Objp.GSTPer),
                new SqlParameter("@cgstper",Objp.CGSTPer),
                new SqlParameter("@sgstper",Objp.SGSTPer),
                new SqlParameter("@mfgdate",Objp.MfgDate),
                new SqlParameter("@expirydate",Objp.ExpiryDate),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@ProductCode",Objp.ItemCode),
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@ItemBarCode",Objp.ItemBarCode),
                new SqlParameter("@PurchaseRate_Bulk",Objp.PurchaseRate_Bulk),
                new SqlParameter("@PurchaseRate_Loose",Objp.PurchaseRate_Loose),
                new SqlParameter("@SaleRate_Bulk",Objp.SaleRate_Bulk),
                new SqlParameter("@SaleRate_Loose",Objp.SaleRate_Loose),
                new SqlParameter("@StorePrice",Objp.StorePrice),
                new SqlParameter("@OnlinePrice",Objp.OnlinePrice),
                new SqlParameter("@BulkUOM",Objp.BulkUOM),
                new SqlParameter("@LooseUOM",Objp.LooseUOM),
                new SqlParameter("@BulkUOMQty",Objp.BulkUOMQty),
                new SqlParameter("@ItemGroup",Objp.GroupCode),
                new SqlParameter("@PurchasetaxIncludeExclude",Objp.Purchase_taxIncludeExclude),
                new SqlParameter("@SaletaxIncludeExclude",Objp.Sale_taxIncludeExclude),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@InitialQuantity",Objp.InitialQty),
                new SqlParameter("@LowStockalert",Objp.LowStock),
                new SqlParameter("@Entrydate",Objp.EntryDate),
                new SqlParameter("@Inventorytype",Objp.AccountType),
                new SqlParameter("@Servicetype",Objp.Servicetype),
                new SqlParameter("@BranchCode",Objp.BranchCode),
                new SqlParameter("@tblItemBundle",dt12),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetItemHeadDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ItemCode",Objp.ItemCode),
                 new SqlParameter("@CompanyCode",Objp.CompanyCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertUpdateSuperStockiest(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@stockiestCode",Objp.SSCode),
                new SqlParameter("@ssname",Objp.SSName),
                new SqlParameter("@contactperson",Objp.ContactPerson),
                new SqlParameter("@contactno",Objp.ContactNo),
                new SqlParameter("@emailaddress",Objp.EmailAddress),
                new SqlParameter("@pincode",Objp.PinCode),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@statecode",Objp.StateId),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@gstno",Objp.GstNo),
                new SqlParameter("@panno",Objp.PanNo),
                new SqlParameter("@gstdocument",Objp.GstDoc),
                new SqlParameter("@pandocument",Objp.PanDoc),
                new SqlParameter("@companyId",Objp.CompanyCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetSuperStockiestDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@SSCode",Objp.SSCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BindUSerProfile(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@UserCode",Objp.UserName),
                new SqlParameter("@Mtype",Objp.Mtype),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertUpdateOutLets(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OCode",Objp.OutLetCode),
                new SqlParameter("@ssCode",Objp.SSCode),
                new SqlParameter("@OutLetName",Objp.SSName),
                new SqlParameter("@contactperson",Objp.ContactPerson),
                new SqlParameter("@contactno",Objp.ContactNo),
                new SqlParameter("@emailaddress",Objp.EmailAddress),
                new SqlParameter("@pincode",Objp.PinCode),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@statecode",Objp.StateId),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@gstno",Objp.GstNo),
                new SqlParameter("@panno",Objp.PanNo),
                new SqlParameter("@gstdocument",Objp.GstDoc),
                new SqlParameter("@pandocument",Objp.PanDoc),
                new SqlParameter("@companyId",Objp.CompanyCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetOutLetDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OutLetCode",Objp.OutLetCode),
                new SqlParameter("@UserId",Objp.EntryBy),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertSuppliersAccount(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@Name",Objp.SSName),
                new SqlParameter("@MobileNo",Objp.ContactNo),
                new SqlParameter("@Address",Objp.Address),
                new SqlParameter("@City",Objp.CityName),
                new SqlParameter("@StateId",Objp.StateId),
                new SqlParameter("@PinCode",Objp.PinCode),
                new SqlParameter("@gstNo",Objp.GstNo),
                new SqlParameter("@PanNo",Objp.PanNo),
                new SqlParameter("@TINNo",Objp.TINNo),
                new SqlParameter("@Description",Objp.Description),
                new SqlParameter("@company_code",Objp.CompanyCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertPurchaseOrder(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@branchcode",Objp.BranchCode),
                new SqlParameter("@supplieraccountcode",Objp.SupplierAccCode),
                new SqlParameter("@invoicedate",Objp.InvoiceDate),
                new SqlParameter("@shipmentpreference",Objp.ShipmentPref),
                new SqlParameter("@stateid",Objp.StateId),
                new SqlParameter("@purchasefile",Objp.PurchaseFile),
                new SqlParameter("@deliverto",Objp.DeliveryTo),
                new SqlParameter("@termscondiotions",Objp.TermsCond),
                new SqlParameter("@notes",Objp.notes),
                new SqlParameter("@discountpersentage",Objp.DiscPer),
                new SqlParameter("@discountamount",Objp.DiscAmt),
                new SqlParameter("@cgstamount",Objp.CgstAmt),
                new SqlParameter("@sgstamount",Objp.SgstAmt),
                new SqlParameter("@igstamount",Objp.IgstAmt),
                new SqlParameter("@payableamount",Objp.PayableAmt),
                new SqlParameter("@grosspayable",Objp.GrossPayable),
                new SqlParameter("@nettotal",Objp.NetTotal),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@totalpayablegst",Objp.Payablegst),
                new SqlParameter("@isfreightinclude",Objp.IsFrieghtInclude),
                new SqlParameter("@freightcharges",Objp.FrieghtCharges),
                new SqlParameter("@gstin",Objp.GstNo),
                new SqlParameter("@billingaddress",Objp.Address),
                new SqlParameter("@invoiceno",Objp.InvoiceNo),
                new SqlParameter("@ewaybillno",Objp.EwayBillNo),
                new SqlParameter("@roundoffamt",Objp.RoundOffAmt),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@typePurchaseOrderItemInsert",dt),


                new SqlParameter("@PONo",Objp.PONo),


            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertStockDemand(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@branchcode",Objp.BranchCode),
                new SqlParameter("@demanddate",Objp.InvoiceDate),
                new SqlParameter("@stockiestcode",Objp.SSCode),
                new SqlParameter("@demandby",Objp.OutLetCode),
                new SqlParameter("@subtotal",Objp.GrossPayable),
                new SqlParameter("@cgstamount",Objp.CgstAmt),
                new SqlParameter("@sgstamount",Objp.SgstAmt),
                new SqlParameter("@igstamount",Objp.IgstAmt),
                new SqlParameter("@totalamount",Objp.PayableAmt),
                new SqlParameter("@typeDemantItemInsert",dt),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertStockTransferDetails(PropertyClass Objp, string ProcName, DataTable dt)
        {

            //Action 2 for receive Stock
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@TransferCode1",Objp.TransferCode),
                new SqlParameter("@transferdate",Objp.InvoiceDate),
                new SqlParameter("@Receivedate",Objp.InvoiceDate),
                new SqlParameter("@partycode",Objp.SSCode),
                new SqlParameter("@transferby",Objp.OutLetCode),
                new SqlParameter("@subtotal",Objp.GrossPayable),
                new SqlParameter("@cgstamount",Objp.CgstAmt),
                new SqlParameter("@sgstamount",Objp.SgstAmt),
                new SqlParameter("@igstamount",Objp.IgstAmt),
                new SqlParameter("@roundoffamount",Objp.RoundOffAmt),
                new SqlParameter("@nettotal",Objp.PayableAmt),
                new SqlParameter("@DemandCode",Objp.OrderId),
                new SqlParameter("@TransferType",Objp.AccountType),
                new SqlParameter("@EntryBy",Objp.OutLetCode),
                new SqlParameter("@typeDemantItemInsert",dt),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetStockDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@StoreId",Objp.StCode),
                new SqlParameter("@Sdate",Objp.Sdate),
                new SqlParameter("@Ldate",Objp.Ldate)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetPurchaseBillDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OrderNo",Objp.ItemCode),
                    new SqlParameter("@Entryby",Objp.EntryBy)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetLogingCompanyStateCode(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserCode",Objp.UserName),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetStockDemandDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@UserCode",Objp.UserName),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@DemandCode",Objp.ItemCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetStockTransferDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@DemandCode",Objp.OrderId),
                new SqlParameter("@CompanyCode",Objp.UserName),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetStockTransferReport(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@UserCode",Objp.UserName),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@TransferCode",Objp.StCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable CheckStockBalance(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@ItemCode",Objp.ItemCode),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@VarId",Objp.VariationId),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataTable BindStateDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS State_id,'Select State' AS State_name UNION ALL SELECT State_id,State_name+' ('+State_Abr+')' State_name FROM State_Master";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindSSDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS SSCode,'Select Super Stockiest' AS SSName UNION ALL SELECT SSCode,SSName FROM tbl_SuperStockiest WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindSSDropDown1()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT SSCode,SSName FROM tbl_SuperStockiest WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable selectStore()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT top(1) SSCode,SSName FROM tbl_SuperStockiest WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable AllStore()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT 'Admin' AS SSCode,'Admin' AS SSCode UNION ALL SELECT SSCode,SSName FROM tbl_SuperStockiest WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindSupplierDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS account_code,'Select Supplier Account' AS account_name UNION ALL SELECT account_code,account_name FROM Accounts_Head WHERE group_code=26";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindSupplierDropDownNew()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS account_code,'All' AS account_name UNION ALL SELECT account_code,account_name FROM Accounts_Head WHERE group_code=26";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindCustomerDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CustomerId,'Select Customer Account' AS Name UNION ALL SELECT CustomerId,Name FROM tbl_CustomerMaster WHERE IsActive=1 and ActiveStatus='A'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindCustomerDropDownNew(string UserCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CustomerId,'Select Customer Account' AS Name UNION ALL SELECT CustomerId,Name FROM tbl_CustomerMaster WHERE IsActive=1 and ActiveStatus='A' and CenterCode='" + UserCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindCustomerDropDownNew()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CustomerId,'All' AS Name UNION ALL SELECT CustomerId,Name FROM tbl_CustomerMaster WHERE IsActive=1 and ActiveStatus='A'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        //internal DataTable BindItemheadDropDown()
        //{
        //    DataTable dt = new DataTable();
        //    string Query = "SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT a.ItemCode,ItemName+' '+isnull(c.VarriationName,'') ItemName FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId=c.SrNo WHERE a.IsActive=1";
        //    dt = objdb.ExecAdaptorDataTable(Query);
        //    return dt;
        //}
        internal DataTable BindItemheadDropDown()
        {
            DataTable dt = new DataTable();
            // string Query = "SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT CASE WHEN a.ProductType='Simple Product' THEN a.ItemCode ELSE b.VariationId END AS ItemCode,ItemName+' '+isnull(c.VarriationName,'') ItemName FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId=c.SrNo WHERE a.IsActive=1";
            string Query = "SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT a.ItemCode AS ItemCode, CASE WHEN a.ProductType='Simple Product' THEN a.ItemName ELSE a.ItemName END AS ItemName ,ItemName+' '+ isnull(c.VarriationName,'') ItemName FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId=c.SrNo WHERE  a.IsActive=1SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT a.ItemCode AS ItemCode, CASE WHEN a.ProductType='Simple Product' THEN a.ItemName ELSE a.ItemName END AS ItemName --,ItemName+' '+ isnull(c.VarriationName,'') ItemName FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId=c.SrNo WHERE  a.IsActive=1SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT a.ItemCode AS ItemCode, CASE WHEN a.ProductType='Simple Product' THEN a.ItemName ELSE a.ItemName END AS ItemName,ItemName+' '+ isnull(c.VarriationName,'') ItemName FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId=c.SrNo WHERE  a.IsActive=1SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT a.ItemCode AS ItemCode, CASE WHEN a.ProductType='Simple Product' THEN a.ItemName ELSE a.ItemName END AS ItemName ,ItemName+' '+ isnull(c.VarriationName,'') ItemName FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId=c.SrNo WHERE  a.IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindItemheadDropDown1(string userId)
        {
            DataTable dt = new DataTable();
            string Query = "";
            if (string.IsNullOrEmpty(userId))
            {
                Query = "SELECT '' as itmId,'' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT a.ItemCode AS itmId,CASE WHEN a.ProductType='Simple Product' THEN a.SrNo ELSE b.VariationId END AS ItemCode,ItemName+' '+isnull(c.VarriationName,'') ItemName FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId=c.SrNo WHERE  a.IsActive=1";
            }
            else
            {
                Query = "SELECT '' as itmId,'' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT a.ItemCode AS itmId,CASE WHEN a.ProductType='Simple Product' THEN a.SrNo ELSE a.SrNo END AS ItemCode,isnull(a.ItemName,'') ItemName FROM tbl_ItemHeadMaster a --LEFT JOIN tbl_ProductVariations b ON a.ItemCode=b.ProductId LEFT JOIN tbl_Variations  c ON b.VariationId=c.SrNo WHERE  a.IsActive=1 and a.EntryBy= '" + userId + "'";
            }
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindcustomerIdListDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CustomerId,'Select State' AS CustomerName UNION ALL SELECT CustomerCode,Name\r\n FROM tbl_DeliveryAddressDetails";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindItemheadDropDownOffer(string userId)
        {
            DataTable dt = new DataTable();
            string Query = "";
            if (string.IsNullOrEmpty(userId))
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,ItemName + ' ' + isnull(c.VarriationName, '') ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.SrNo ELSE b.VariationId END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1";
            }
            else
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,ItemName + ' ' + isnull(c.VarriationName, '') ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.SrNo ELSE b.VariationId END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1 and a.EntryBy= '" + userId + "'";
            }
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }


        internal DataTable BindItemheadByGroupDropDown(string GroupCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT ItemCode,ItemName FROM tbl_ItemHeadMaster WHERE IsActive=1 AND ItemGroup='" + GroupCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindItemheadByGroupDropDown1(string GroupCode, string BranchCode)
        {
            DataTable dt = new DataTable();

            string Query = "SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT ItemCode,ItemName FROM tbl_ItemHeadMaster WHERE IsActive=1 AND ItemGroup='" + GroupCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindUOMDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' UnitCode,'Select UOM' UnitName UNION ALL SELECT UnitCode,UnitName FROM tbl_UnitofMesurment WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindItemGroupDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS SrNo,'Select Item Group' AS ItemGroupName UNION ALL SELECT SrNo,ItemGroupName FROM tbl_ItemGroup WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        public DataTable binditemnew(string CompanyId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '0' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT ItemCode,ItemName FROM tbl_ItemHeadMaster WHERE CompanyCode='" + CompanyId + "' AND Inventorytype<>'Bundle'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindItemGroupDropDown(string CompanyId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS SrNo,'Select Item Group' AS ItemGroupName UNION ALL SELECT SrNo,ItemGroupName FROM tbl_ItemGroup WHERE IsActive=1 and CompanyId='" + CompanyId + "' or EntryBy='" + CompanyId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindAccountGroupDLL()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS group_code,'Select Group Code' AS group_name UNION ALL SELECT group_code,group_name FROM Accounts_Group WHERE parent_groupcode=0";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindStockiestDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CompanyCode,'Select Stockiest' CompanyName UNION ALL SELECT CompanyCode,CompanyName+' (Warehouse)' AS CompanyName FROM tbl_CompanyDetails UNION ALL SELECT SSCode AS CompanyCode,SSName AS SSName FROM tbl_SuperStockiest WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindStockiestNewDropDown(string userCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CompanyCode,'Select' CompanyName UNION ALL SELECT CompanyCode,CompanyName FROM viewGetLogDetails WHERE Comptype NOT IN ('Customer','Owned') AND CompanyCode != '" + userCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindWareHouseDropDown(string userCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CompanyCode,'All' CompanyName UNION ALL SELECT CompanyCode,CompanyName+' (Warehouse)' AS CompanyName FROM tbl_CompanyDetails UNION ALL SELECT OutLetCode AS CompanyCode,OutLetName+' (OutLet)' AS SSName FROM tbl_OutLetMaster WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindStockiestDropDownNew()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CompanyCode,'All' CompanyName UNION ALL SELECT CompanyCode,CompanyName+' (Warehouse)' AS CompanyName FROM tbl_CompanyDetails UNION ALL SELECT SSCode AS CompanyCode,SSName+' (SS)' AS SSName FROM tbl_SuperStockiest WHERE IsActive=1 UNION ALL SELECT OutLetCode AS CompanyCode,OutLetName+' (OutLet)' AS SSName FROM tbl_OutLetMaster WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindStoretDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS CompanyCode,'Select Store' CompanyName UNION ALL SELECT CompanyCode,CompanyName+' (Warehouse)' AS CompanyName FROM tbl_CompanyDetails UNION ALL SELECT SSCode AS CompanyCode,SSName+' (SS)' AS SSName FROM tbl_SuperStockiest WHERE IsActive=1 UNION ALL SELECT OutLetCode AS CompanyCode,OutLetName+' (OutLet)' AS SSName FROM tbl_OutLetMaster WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetItemHeadDetail(string ItemCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT a.*,b.UnitName AS BulkUnitName,c.UnitName AS LooseUnitName FROM tbl_ItemHeadMaster a Left JOIN tbl_UnitofMesurment b ON a.BulkUOM=b.UnitCode Left JOIN tbl_UnitofMesurment c ON a.LooseUOM=c.UnitCode WHERE ItemCode='" + ItemCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetSupplierAccDetail(string AccountCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT * FROM Accounts_Head WHERE group_code=26 AND account_code='" + AccountCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindRoleDDL()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS Id,'Select Designation' Role UNION ALL SELECT Id,Role FROM mst_Role WHERE IsActive=1 AND type='sub'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetCustomerAccDetail(string AccountCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT b.MobileNo,a.account_code,b.StateId as StateCode,b.AccountType,(isnull((SELECT WalletAmount FROM tbl_WalletMaster WHERE CustomerId=a.userid),0)-(SELECT isnull(sum(DrAmount),0) FROM tbl_TransactionDetails WHERE PaymentType='Sale' AND CustomerId=a.userid AND AccountCode NOT IN (89))) AS WalletBal,isnull((SELECT (isnull(aa.CashBackAmount,0)+isnull(aa.Points,0))-(SELECT isnull(sum(Amount),0) FROM tbl_CahbackRedeemHistory WHERE CustomerId=aa.CustomerId) FROM tbl_WalletCashback aa LEFT JOIN tbl_CustomerMaster bb ON aa.CustomerId=bb.CustomerId WHERE aa.CustomerId=a.userid),0) CasbckAmt FROM Accounts_Head a JOIN tbl_CustomerMaster b ON a.userid=b.CustomerId  WHERE group_code=27 AND a.userid='" + AccountCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetCustomerAddressDetail(string CustomerId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT a.SrNo,Name,Address,AddressType,MobileNo,PinCode,Locality,CityName,b.State_name,(a.Address+','+isnull(a.Locality,'')+','+a.CityName+','+b.State_name+' - ') AS compAdd,CASE WHEN a.IsDefaultAccount=1 THEN 'Default' ELSE '' END AS  IsDefaultAccount FROM tbl_DeliveryAddressDetails a JOIN State_Master b ON a.StateId=b.State_id WHERE a.CustomerCode='" + CustomerId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetCustomerAccDetailByMobiles(string MobileNo)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT * FROM tbl_CustomerMaster WHERE IsActive=1 AND ActiveStatus='A' AND MobileNo='" + MobileNo + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable FetchItemByBarCode(string BarCode, string VarId, decimal MRP, int Action, string ItemCode)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Barcode",BarCode),
                new SqlParameter("@VarId",VarId),
                new SqlParameter("@MRP",MRP),
                new SqlParameter("@ItemCode",ItemCode)
            };
            dt = objdb.ExecProcDataTable("GetBarcodeDetails", param);
            return dt;

        }
        internal DataTable GetCustomerMobile(string CustomerId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT MobileNo,Name,CustomerId FROM tbl_CustomerMaster WHERE CustomerId='" + CustomerId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable CheckAddOnMebers(string CustomerId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT MobileNo FROM tbl_AddOnMembers WHERE MobileNo='" + CustomerId + "' AND IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable CHeckCashBackAmtBalance(string CustomerId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT isnull((isnull(a.CashBackAmount,0)+isnull(a.Points,0)),0) CashbackAmt FROM tbl_WalletCashback a LEFT JOIN tbl_CustomerMaster b ON a.CustomerId=b.CustomerId WHERE a.CustomerId='" + CustomerId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindProductCategoryDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' SrNo,'Select' ProductCategory UNION ALL SELECT SrNo,ProductCategory FROM tbl_MainCategory WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        //vandana pandey
        internal DataTable BindcolorDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' SrNo,'Select' AS ColorName UNION ALL SELECT    Id AS SrNo ,ColorName FROM mst_Color WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindSizeDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' SrNo,'Select' AS Size_Name UNION ALL SELECT    Id AS SrNo ,Size_Name FROM mst_size WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindProductMainCategoryDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' SrNo,'Select Main Category' ProductCategory UNION ALL SELECT SrNo,ProductCategory FROM tbl_MainCategory WHERE isnull(ParrentCategoryId,'0')='0' AND IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindSubCategoryDll(string Id)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT 0 AS SrNo,'Select Sub Category' AS ProductCategory UNION ALL SELECT SrNo, ProductCategory FROM tbl_MainCategory WHERE IsActive = 1 and ParrentCategoryId ='" + Id + "' AND ParrentCategoryId<>'0'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindSubCategoryDllNew()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT 0 AS SrNo,'Select Sub Category' AS ProductCategory UNION ALL SELECT SrNo, ProductCategory FROM tbl_MainCategory WHERE IsActive = 1  AND ParrentCategoryId<>'0'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindDailydealDllNew()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT 0 AS Did,'Select Dail yDeal' AS DailyDealName UNION ALL SELECT Did, Dealname FROM mst_dailyDeal WHERE IsActive = 1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindAttributeDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT 0 AS SrNo,'Select Attribute' AttributeName UNION ALL SELECT SrNo,AttributeName FROM tbl_Attributes WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindManufacturerDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT 0 AS SrNo,'Select' ManufacturerName UNION ALL SELECT SrNo,ManufacturerName FROM tbl_ManufacturerMaster WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        public DataTable UpdateAccountStatus(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserCode",Objp.UserName),
                new SqlParameter("@Mtype",Objp.Mtype),
                new SqlParameter("@Status",Objp.Status),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataSet BindDashBoardData(PropertyClass Objp, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@userCode",Objp.UserName),
                new SqlParameter("@RoleId",Objp.Role),
            };
            dtt = objdb.ExecProcDataSet(ProcName, param);
            return dtt;
        }
        public DataTable BindDashBoardDatadt(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@userCode",Objp.UserName),
                new SqlParameter("@RoleId",Objp.Role),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertCustomerAccount(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@name",Objp.SSName),
                new SqlParameter("@mobileno",Objp.ContactNo),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@stateid",Objp.StateId),
                new SqlParameter("@gstin",Objp.GstNo),
                new SqlParameter("@panno",Objp.PanNo),
                new SqlParameter("@company_code",Objp.CompanyCode),
                new SqlParameter("@AccountType",Objp.AccountType),
                new SqlParameter("@EmailId",Objp.EmailAddress),
                new SqlParameter("@CenterCode",Objp.SSCode)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertSalesOrder(PropertyClass Objp, string ProcName, DataTable dt, DataTable dt1)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@branchcode",Objp.BranchCode),
                new SqlParameter("@CustomerAccountCode",Objp.SupplierAccCode),
                new SqlParameter("@invoicedate",Objp.InvoiceDate),
                new SqlParameter("@stateid",Objp.StateId),
                new SqlParameter("@deliverto",Objp.DeliveryTo),
                new SqlParameter("@termscondiotions",Objp.TermsCond),
                new SqlParameter("@notes",Objp.notes),
                new SqlParameter("@discountpersentage",Objp.DiscPer),
                new SqlParameter("@discountamount",Objp.DiscAmt),
                new SqlParameter("@cgstamount",Objp.CgstAmt),
                new SqlParameter("@sgstamount",Objp.SgstAmt),
                new SqlParameter("@igstamount",Objp.IgstAmt),
                new SqlParameter("@payableamount",Objp.PayableAmt),
                new SqlParameter("@grosspayable",Objp.GrossPayable),
                new SqlParameter("@nettotal",Objp.NetTotal),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@totalpayablegst",Objp.Payablegst),
                new SqlParameter("@isfreightinclude",Objp.IsFrieghtInclude),
                new SqlParameter("@freightcharges",Objp.FrieghtCharges),
                new SqlParameter("@gstin",Objp.GstNo),
                new SqlParameter("@billingaddress",Objp.Address),
                new SqlParameter("@ewaybillno",Objp.EwayBillNo),
                new SqlParameter("@roundoffamt",Objp.RoundOffAmt),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@ChqNo",Objp.ChqNo),
                new SqlParameter("@ChqDate",Objp.ChqDate),
                new SqlParameter("@BankAccountName",Objp.BanKAccName),
                new SqlParameter("@BankTransId",Objp.BankTransId),
                new SqlParameter("@PaidAmount",Objp.PaidAmount),
                new SqlParameter("@PurchaseBy",Objp.PurchaseBy),

                new SqlParameter("@IsCashbackApplied",Objp.IsCashbackApplied),
                new SqlParameter("@CshbkAmt",Objp.CashBackAmount),
                  new SqlParameter("@OfferCode1",Objp.OfferCode),
                  new SqlParameter("@OfferAmt1",Objp.OfferAmt),
                new SqlParameter("@typePurchaseOrderItemInsert",dt),
                new SqlParameter("@type_OfferList",dt1),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetSalesBillDetails(PropertyClass Objp, string ProcName)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                startDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                endDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
        {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@UserId",Objp.UserName),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@InvoiceNo",Objp.InvoiceNo),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@MobileNo",Objp.ContactNo),
                new SqlParameter("@StaffCode",Objp.EntryBy)

        };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetSalesBillInvoiceDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@InvoiceNo",Objp.InvoiceNo)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertCustomerAccountWeb(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@name",Objp.SSName),
                new SqlParameter("@EmailId",Objp.EmailAddress),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@mobileno",Objp.ContactNo),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@stateid",Objp.StCode),
                new SqlParameter("@gstin",Objp.GstNo),
                new SqlParameter("@panno",Objp.PanNo),
                new SqlParameter("@CustId",Objp.UserName),
                new SqlParameter("@AccountType",Objp.AccountType),
                new SqlParameter("@UsedReferal",Objp.UsedRefral),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertCustomerAccountNewMember(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@name",Objp.SSName),
                new SqlParameter("@EmailId",Objp.EmailAddress),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@mobileno",Objp.ContactNo),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@stateid",Objp.StCode),
                new SqlParameter("@gstin",Objp.GstNo),
                new SqlParameter("@panno",Objp.PanNo),
                new SqlParameter("@CustId",Objp.UserName),
                new SqlParameter("@AccountType",Objp.AccountType),
                new SqlParameter("@memberShipId",Objp.memberShipId),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@EntryBy",Objp.CustomerId),
                new SqlParameter("@CardValidFrom",Objp.CardValidFrom),
                new SqlParameter("@CardValidTo",Objp.CardValidTo),
                new SqlParameter("@cardBarCode",Objp.CardBarCode),
                new SqlParameter("@MeMberPic",Objp.ProfilePicPath),
                new SqlParameter("@MemberShipType","MemberShip"),
                  new SqlParameter("@CardId",Objp.memberShipId)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable GetCustomerDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CustomerCode",Objp.OutLetCode),
                new SqlParameter("@UserId",Objp.UserName),
                new SqlParameter("@RoleId",Objp.Role)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable ApproveCustomerAccount(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CustomerId",Objp.OutLetCode),
                new SqlParameter("@Status",Objp.Status),
                new SqlParameter("@CompanyCode",Objp.CompanyCode)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetPurchaseReport(PropertyClass Objp, string ProcName)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                startDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                endDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@AccountCode",Objp.AccountCode),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@UserCode",Objp.UserName),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@StaffCode",Objp.EntryBy)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertUpdateItemGroupMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@GroupCode",Objp.GroupCode),
                new SqlParameter("@GroupName",Objp.GroupName),
                new SqlParameter("@EntryBy",Objp.EntryBy),
                new SqlParameter("@CompanyCode",Objp.CompanyCode)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        //vandana pandey
        #region Size 

        internal DataTable mst_SaveUpdate_Size(PropertyClass objAcc)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objAcc.Action),
                new SqlParameter("@Size_Name",objAcc.Size_Name),
                new SqlParameter("@Size_Value",objAcc.Size_Value),
                new SqlParameter("@EntryBy",objAcc.EntryBy),
                new SqlParameter("@Id",objAcc.Id)

            };
            dt = objdb.ExecProcDataTable("[mst_SaveUpdate_Size]", parm);
            return dt;
        }

        #endregion
        #region Colour 

        internal DataTable mst_SaveUpdate_Colour(PropertyClass objAcc)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",objAcc.Action),
                new SqlParameter("@ColorName",objAcc.ColorName),
                new SqlParameter("@ColorValue",objAcc.ColorValue),
                new SqlParameter("@EntryBy",objAcc.EntryBy),
                new SqlParameter("@Id",objAcc.Id)

            };
            dt = objdb.ExecProcDataTable("[mst_SaveUpdate_Colour]", parm);
            return dt;
        }

        #endregion
        //
        public DataTable InsertWalletRecharge(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@CenterCode",Objp.SSCode),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@PaidAmount",Objp.PaidAmount),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@PaymentStatus",Objp.Status),
                new SqlParameter("@ReferenceId",Objp.BankTransId),
                new SqlParameter("@CardType",Objp.CardType),
                new SqlParameter("@EntryBy",Objp.EntryBy)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetWalletRechargeDetails(PropertyClass Objp, string ProcName)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                startDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                endDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@CenterCode",Objp.SSCode),
                new SqlParameter("@RoleId",Objp.Role),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@MobileNo",Objp.ContactNo),
                new SqlParameter("@StaffCode",Objp.EntryBy)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertAddOnMembers(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@customerid",Objp.CustomerId),
                new SqlParameter("@membername",Objp.SSName),
                new SqlParameter("@mobileno",Objp.ContactNo),
                new SqlParameter("@relation",Objp.Relation),
                new SqlParameter("@BlockStatus",Objp.Status)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertUpdateUOMpMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@UOMCode",Objp.ItemCode),
                new SqlParameter("@unitname",Objp.UOM)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable InsertUpdateDailyDealspMaster(DaliyDeal Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@dId",Objp.dId),
                new SqlParameter("@DealName",Objp.DealName),
                new SqlParameter("@ToTime ",Objp.@ToTime ),
                new SqlParameter("@FromTime ",Objp.FromTime ),
                new SqlParameter("@DisPrice",Objp.DisPrice),
                new SqlParameter("@Date",Objp.@Date),
                new SqlParameter("@Entryby",Objp.@Entryby)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable InsertUpdateStaffDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@staffname",Objp.SSName),
                new SqlParameter("@fathername",Objp.StockistName),
                new SqlParameter("@roleid",Objp.Role),
                new SqlParameter("@companyid",Objp.CompanyCode),
                new SqlParameter("@storecode",Objp.SSCode),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@MobileNo",Objp.ContactNo),
                new SqlParameter("@EmailId",Objp.EmailAddress),
                new SqlParameter("@Address",Objp.Address),
                new SqlParameter("@stfCode",Objp.StCode),
                new SqlParameter("@password",Objp.Password),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataTable BindStaffAccountDetails(string staffCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT a.StaffCode,a.StoreCode,(a.StaffName+' ('+b.CompanyName+')') AS StoreName FROM tbl_StaffDetails a JOIN viewGetLogDetails b ON a.StoreCode=b.CompanyCode where a.StaffCode='" + staffCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable CheckRechargeAmount(string TxnId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT * FROM tbl_WalletRecharge WHERE TransactionId='" + TxnId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetOnlineCustomerTxnDetail(string OrderId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT a.OrderId,a.NetPayable,b.Name,b.MobileNo,b.EmailAddress,'Product' AS PInfo FROM tbl_OnlineOrderDetail a JOIN tbl_CustomerMaster b ON a.CustomerId=b.CustomerId WHERE a.OrderId='" + OrderId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        public DataTable GetOnlineWalletRechargeInfo(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@TxnId",Objp.txnId),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataTable GetItemrate(string ItemId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Isnull(SaleRate_Loose,0) Itemrate FROM tbl_ItemHeadMaster WHERE IsActive=1 and ItemCode='" + ItemId + "' ";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindItemheadDropDownPurchase(string CompanyId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT ItemCode,ItemName FROM tbl_ItemHeadMaster WHERE IsActive=1 and CompanyCode='" + CompanyId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        public DataTable UpdateOnlineWalletRechargeInfo(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@PaidAmount",Objp.PaidAmount),
                new SqlParameter("@PaymentStatus",Objp.Status),
                new SqlParameter("@CardType",Objp.CardType),
                new SqlParameter("@EntryBy",Objp.EntryBy),
                new SqlParameter("@txnId",Objp.txnId),
                new SqlParameter("@BankTransID",Objp.BankTransId),
                new SqlParameter("@PaytmTransId",Objp.PaytmTransId),
                new SqlParameter("@RespoCode",Objp.RespoCode),
                new SqlParameter("@RespMsg",Objp.RespMsg),
                new SqlParameter("@GateWayname",Objp.GateWayname),
                new SqlParameter("@Bankname",Objp.Bankname),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable DeleteItemHeadDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ItemCode",Objp.ItemCode),
                new SqlParameter("@DeleteBy",Objp.EntryBy),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable ChangePassword(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {   
                new SqlParameter("@UserId",Objp.UserName),
                new SqlParameter("@OldPassword",Objp.OldPassword),
                new SqlParameter("@NewPassword",Objp.Password),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertReturnStock(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@returndate",Objp.InvoiceDate),
                new SqlParameter("@returnby",Objp.SSCode),
                new SqlParameter("@returnto",Objp.UserName),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@type_InsertReturnItems",dt),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertOfferMaster(PropertyClass Objp, string ProcName)
        {
            DateTime? stDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                stDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                endDate = Convert.ToDateTime(Objp.eDate);
            }


            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@offertitle",Objp.OfferTitle),
                new SqlParameter("@offertype",Objp.OfferType),
                new SqlParameter("@itemcode",Objp.ItemCode),
                new SqlParameter("@purchaseamount",Objp.OnPurchaseAmount),
                new SqlParameter("@cashbackamount",Objp.CashBackAmount),
                new SqlParameter("@validstartdate",stDate),
                new SqlParameter("@validenddate",endDate),
                new SqlParameter("@points",Objp.Points),
                new SqlParameter("@amountperpoint",Objp.AmountPerPoint),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@CenterCode",Objp.BranchCode),
                new SqlParameter("@isFirstPurchase",Objp.Status),
                new SqlParameter("@Ocode",Objp.RespoCode),
                new SqlParameter("@IsFreeItem",Objp.IsFreeItem),
                new SqlParameter("@FreeItemCode",Objp.FreeItemCode),
                new SqlParameter("@FreeQuantity",Objp.FreeQty),
                new SqlParameter("@OfferFor",Objp.AccountType),
                new SqlParameter("@DiscountType",Objp.CardType),
                new SqlParameter("@DiscountValue",Objp.DiscPer),
                new SqlParameter("@imageFile",Objp.PurchaseFile),
                new SqlParameter("@PromoCode",Objp.ItemBarCode),
                new SqlParameter("@ApplyMRPFrom",Objp.ApplyMRPFrom),
                new SqlParameter("@ApplyMRPTo",Objp.ApplyMRPTo),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetOfferDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OfferId",Objp.RespoCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable DeleteStaffDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@StaffCode",Objp.SSCode),
                new SqlParameter("@DeleteBy",Objp.EntryBy),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetOnlineProductDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ItemCode",Objp.ItemCode),
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable BindEcommarceDashboard(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertOnlineProductDetails(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@centercode",Objp.SSCode),
                new SqlParameter("@productgroupcode",Objp.GroupCode),
                new SqlParameter("@productcode",Objp.ItemCode),
                new SqlParameter("@onlineprice",Objp.OnlinePrice),
                new SqlParameter("@stockquantity",Objp.Quantity),
                new SqlParameter("@productdescription",Objp.Description),
                new SqlParameter("@productmainimageurl",Objp.Url),
                new SqlParameter("@isavilableforsale",Objp.IsActive),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@ProductSpecification",Objp.ProductSpacification),
                new SqlParameter("@type_InsertProductImageUrl",dt),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetEcoomarceProductDescription(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ItemCode",Objp.ItemCode),
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@GroupCode",Objp.GroupCode),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertCartList(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@customerid",Objp.CustomerId),
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@productid",Objp.ItemCode),
                new SqlParameter("@quantity",Objp.Quantity),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@St",Objp.Status),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertDeliveryAddress(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@customercode",Objp.CustomerId),
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@name",Objp.SSName),
                new SqlParameter("@mobileno",Objp.ContactNo),
                new SqlParameter("@pincode",Objp.PinCode),
                new SqlParameter("@locality",Objp.locality),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@stateid",Objp.StCode),
                new SqlParameter("@landmark",Objp.landmark),
                new SqlParameter("@altmobileno",Objp.altmobileno),
                new SqlParameter("@addresstype",Objp.OfferType),
                   new SqlParameter("@latitude",Objp.latitude),
                new SqlParameter("@longitude",Objp.longitude),
                new SqlParameter("@adress2",Objp.Address2),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetActiveOffers(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetCashbackWallet(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@CenterCode",Objp.BranchCode),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@MobileNo",Objp.ContactNo),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable InsertOnlineOrderDetails(PropertyClass Objp, string ProcName, DataTable dt, DataTable dtfreeitem)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@company_code",Objp.CompanyCode),
                new SqlParameter("@customerid",Objp.CustomerId),
                new SqlParameter("@grossamount",Objp.GrossPayable),
                new SqlParameter("@deliverycharges",Objp.deliverycharges),
                new SqlParameter("@iscoupenapplied",Objp.iscoupenapplied),
                new SqlParameter("@coupenamount",Objp.CoupenAmount),
                new SqlParameter("@discountamount",Objp.DiscAmt),
                new SqlParameter("@deliveryaddressid",Objp.DeliveryTo),
                new SqlParameter("@paymentmode",Objp.PayMode),
                new SqlParameter("@paymentstatus",Objp.Status),
                new SqlParameter("@NetPayable",Objp.NetTotal),
                new SqlParameter("@GSTAmount",Objp.gstamount),
                new SqlParameter("@StockiestId",Objp.StockistId),
                new SqlParameter("@type_OnlineOrderItem",dt),
                new SqlParameter("@type_OnlineOrderFreeItem",dtfreeitem),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        internal DataTable InsertUpdatePaytmResponse(PaymentResponse1 obj, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
                {
                 new SqlParameter("@Action",obj.Action),
                 new SqlParameter("@MID",obj.MID),
                 new SqlParameter("@ORDERID",obj.ORDERID),
                 new SqlParameter("@TXNAMOUNT",obj.TXNAMOUNT),
                 new SqlParameter("@CURRENCY",obj.CURRENCY),
                 new SqlParameter("@TXNID",obj.TXNID),
                 new SqlParameter("@BANKTXNID",obj.BANKTXNID),
                 new SqlParameter("@STATUS",obj.STATUS),
                 new SqlParameter("@RESPCODE",obj.RESPCODE),
                 new SqlParameter("@RESPMSG",obj.RESPMSG),
                 new SqlParameter("@TXNDATE",obj.TXNDATE),
                 new SqlParameter("@GATEWAYNAME",obj.GATEWAYNAME),
                 new SqlParameter("@BANKNAME",obj.BANKNAME),
                 new SqlParameter("@PAYMENTMODE",obj.PAYMENTMODE),
                 new SqlParameter("@CHECKSUMHASH",obj.CHECKSUMHASH),
                 new SqlParameter("@Formdata",obj.FormData)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetOnlineInvoiceDetails(PropertyClass Objp, string ProcName)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                startDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                endDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OrderOd",Objp.OrderId),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@StockiestId",Objp.StockistId),
                new SqlParameter("@deliveryboy",Objp.deliveryboy),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertUpdateCategory(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@RefId",Objp.strId),
                new SqlParameter("@ProductCategory",Objp.ItemName),
                new SqlParameter("@ParrentCategoryId",Objp.GroupCode),
                new SqlParameter("@CategoryImage",Objp.PurchaseFile),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertOnlineProductDetailsNew(PropertyClass Objp, string ProcName, DataTable dt, DataTable dt1)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@centercode",Objp.SSCode),
                new SqlParameter("@productgroupcode",Objp.GroupCode),
                new SqlParameter("@productcode",Objp.ItemCode),
                new SqlParameter("@onlineprice",Objp.OnlinePrice),
                new SqlParameter("@stockquantity",Objp.Quantity),
                new SqlParameter("@productdescription",Objp.Description),
                new SqlParameter("@productmainimageurl",Objp.Url),
                new SqlParameter("@isavilableforsale",Objp.IsActive),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@ProductSpecification",Objp.ProductSpacification),
                new SqlParameter("@type_InsertProductImageUrl",dt),
                new SqlParameter("@ProductName",Objp.ProductName),
                new SqlParameter("@MainCategoryCode",Objp.MainCategoryCode),
                new SqlParameter("@SubCategoryCode",Objp.SubCategoryCode),
                new SqlParameter("@ProductType",Objp.ProductType),
                new SqlParameter("@IsInventoryProduct",Objp.IsInventoryProduct),
                new SqlParameter("@RegularPrice",Objp.RegularPrice),
                new SqlParameter("@SalePrice",Objp.SalePrice),
                new SqlParameter("@DiscPer",Objp.DiscPer),
                new SqlParameter("@KeyWords",Objp.KeyWords),
                new SqlParameter("@type_ProductVariationList",dt1),
                new SqlParameter("@GSTRate",Objp.GSTPer),
                new SqlParameter("@CessRate",Objp.CessRate)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        //public DataTable InsertOnlineProductDetailsNewUpdated(PropertyClass Objp, string ProcName, DataTable dt, DataTable dt1)
        //{
        //    DataTable dtt = new DataTable();
        //    DataTable dtSizeCodeList = new DataTable();
        //    DataTable dtColorCodeList = new DataTable();
        //    if (Objp.SizeCode != null)
        //    {
        //        string _SizeCode = Convert.ToString(Objp.SizeCode);
        //        string[] arr_SizeCode = _SizeCode.Split(',');


        //        dtSizeCodeList.Columns.Add("SizeId");

        //        for (int i = 0; i < arr_SizeCode.Length; i++)
        //        {
        //            dtSizeCodeList.Rows.Add(arr_SizeCode[i].ToString());
        //        }
        //    }
        //    else
        //    {

        //        dtSizeCodeList = null;
        //    }
        //    if (Objp.ColorCode != null)
        //    {
        //        string _ColorCode = Convert.ToString(Objp.ColorCode);
        //        //string fy1 = "";

        //        dtColorCodeList.Columns.Add("ColorId");
        //        string[] arr_ColorCode = _ColorCode.Split(',');
        //        for (int i = 0; i < arr_ColorCode.Length; i++)
        //        {
        //            dtColorCodeList.Rows.Add(arr_ColorCode[i].ToString());
        //        }
        //    }
        //    else
        //    {

        //        dtColorCodeList = null;
        //    }
        //    //for (int i = 0; i < array1.Length; i++)
        //    //{
        //    //    fy1 = array1[i].ToString() + "\n";
        //    //}
        //    SqlParameter[] param = new SqlParameter[]
        //    {
        //        new SqlParameter("@Action",Objp.Action),
        //        new SqlParameter("@companycode",Objp.CompanyCode),
        //        new SqlParameter("@centercode",Objp.SSCode),
        //        new SqlParameter("@productgroupcode",Objp.GroupCode),
        //        new SqlParameter("@productcode",Objp.ItemCode),
        //        new SqlParameter("@onlineprice",Objp.OnlinePrice),
        //        new SqlParameter("@stockquantity",Objp.Quantity),
        //        new SqlParameter("@productdescription",Objp.Description),
        //        new SqlParameter("@productmainimageurl",Objp.Url),
        //        new SqlParameter("@isavilableforsale",Objp.IsActive),
        //        new SqlParameter("@entryby",Objp.EntryBy),
        //        new SqlParameter("@ProductSpecification",Objp.ProductSpacification),
        //        new SqlParameter("@type_InsertProductImageUrl",dt),
        //        new SqlParameter("@ProductName",Objp.ProductName),
        //        new SqlParameter("@MainCategoryCode",Objp.MainCategoryCode),

        //        new SqlParameter("@SubCategoryCode",Objp.SubCategoryCode),
        //        new SqlParameter("@ProductType",Objp.ProductType),
        //        new SqlParameter("@IsInventoryProduct",Objp.IsInventoryProduct),
        //        new SqlParameter("@RegularPrice",Objp.RegularPrice),
        //        new SqlParameter("@SalePrice",Objp.SalePrice),
        //        new SqlParameter("@DiscPer",Objp.DiscPer),
        //        new SqlParameter("@KeyWords",Objp.KeyWords),
        //        new SqlParameter("@type_ProductVariationList",null),
        //        new SqlParameter("@GSTRate",Objp.GSTPer),
        //        new SqlParameter("@CessRate",Objp.CessRate),
        //        new SqlParameter("@ItemBarCode",Objp.ItemBarCode),
        //        new SqlParameter("@PurchaseRate",Objp.PurchaseRate_Loose),
        //        new SqlParameter("@isPurchasetaxInclude",Objp.Purchase_taxIncludeExclude),
        //        new SqlParameter("@isSaletaxInclude",Objp.Sale_taxIncludeExclude),
        //        new SqlParameter("@HSNCode",Objp.HSNCode),
        //        new SqlParameter("@Manufacturerid",Objp.Manufacturerid),
        //        new SqlParameter("@UOMId",Objp.UOM),
        //        new SqlParameter("@LowStockAlert",Objp.LowStockAlert),
        //        new SqlParameter("@type_InsertMultiVarient",dt1),
        //        new SqlParameter("@RefundAndreturnPolicy",Objp.ReturnAndRefundPolicy),
        //        new SqlParameter("@taxtype",Objp.Taxtype11),
        //        new SqlParameter("@gstamt",Objp.GstAmt),
        //        new SqlParameter("@GstExcludeRate",Objp.NonTaxRate),
        //         new SqlParameter("@ColorCode",dtColorCodeList),
        //          new SqlParameter("@SizeCode",dtSizeCodeList),
        //              new SqlParameter("@VendorCode",Objp.VendorCode),
        //    };
        //    dtt = objdb.ExecProcDataTable(ProcName, param);
        //    return dtt;
        //}

        public DataTable InsertOnlineProductDetailsNewUpdated(PropertyClass Objp, string ProcName, DataTable dt, DataTable dt1)
        {
            DataTable dtt = new DataTable();
            //DataTable dtSizeCodeList = new DataTable();
            //DataTable dtColorCodeList = new DataTable();
            //if (Objp.SizeCode != null)
            //{
            //    string _SizeCode = Convert.ToString(Objp.SizeCode);
            //    string[] arr_SizeCode = _SizeCode.Split(',');


            //    dtSizeCodeList.Columns.Add("SizeId");

            //    for (int i = 0; i < arr_SizeCode.Length; i++)
            //    {
            //        dtSizeCodeList.Rows.Add(arr_SizeCode[i].ToString());
            //    }
            //}
            //else
            //{
            //    dtSizeCodeList = null;
            //}

            //if (Objp.ColorCode != null)
            //{
            //    string _ColorCode = Convert.ToString(Objp.ColorCode);
            //    //string fy1 = "";
            //    string[] arr_ColorCode = _ColorCode.Split(',');
            //    dtColorCodeList.Columns.Add("ColorId");

            //    for (int i = 0; i < arr_ColorCode.Length; i++)
            //    {
            //        dtColorCodeList.Rows.Add(arr_ColorCode[i].ToString());
            //    }
            //}
            //else
            //{
            //    dtColorCodeList = null;

            //}
            if (Objp.MainCategoryCode == null)
            {
                Objp.MainCategoryCode = "0";
            }
            else
            {

            }
            if (Objp.SizeCode != null || Objp.ColorCode!= null)
            {
                SqlParameter[] param = new SqlParameter[]
                {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@centercode",Objp.SSCode),
                new SqlParameter("@productgroupcode",Objp.GroupCode),
                new SqlParameter("@productcode",Objp.ItemCode),
                new SqlParameter("@onlineprice",Objp.OnlinePrice),
                new SqlParameter("@stockquantity",Objp.Quantity),
                new SqlParameter("@productdescription",Objp.Description),
                new SqlParameter("@productmainimageurl",Objp.Url),
                new SqlParameter("@isavilableforsale",Objp.IsActive),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@ProductSpecification",Objp.ProductSpacification),
                new SqlParameter("@type_InsertProductImageUrl",dt),
                new SqlParameter("@ProductName",Objp.ProductName),
                new SqlParameter("@MainCategoryCode",Objp.MainCategoryCode),
                new SqlParameter("@SubCategoryCode",Objp.SubCategoryCode),
                new SqlParameter("@ProductType",Objp.ProductType),
                new SqlParameter("@IsInventoryProduct",Objp.IsInventoryProduct),
                new SqlParameter("@RegularPrice",Objp.RegularPrice),
                new SqlParameter("@SalePrice",Objp.SalePrice),
                new SqlParameter("@DiscPer",Objp.DiscPer),
                new SqlParameter("@KeyWords",Objp.KeyWords),
                new SqlParameter("@type_ProductVariationList",null),
                new SqlParameter("@GSTRate",Objp.GSTPer),
                new SqlParameter("@CessRate",Objp.CessRate),
                new SqlParameter("@ItemBarCode",Objp.ItemBarCode),
                new SqlParameter("@PurchaseRate",Objp.PurchaseRate_Loose),
                new SqlParameter("@isPurchasetaxInclude",Objp.Purchase_taxIncludeExclude),
                new SqlParameter("@isSaletaxInclude",Objp.Sale_taxIncludeExclude),
                new SqlParameter("@HSNCode",Objp.HSNCode),
                new SqlParameter("@Manufacturerid",Objp.Manufacturerid),
                new SqlParameter("@UOMId",Objp.UOM),
                new SqlParameter("@LowStockAlert",Objp.LowStockAlert),
                new SqlParameter("@type_InsertMultiVarient",dt1),
                new SqlParameter("@RefundAndreturnPolicy",Objp.ReturnAndRefundPolicy),
                new SqlParameter("@taxtype",Objp.Taxtype11),
                new SqlParameter("@gstamt",Objp.GstAmt),
                new SqlParameter("@GstExcludeRate",Objp.NonTaxRate),
                 new SqlParameter("@ColorCode",Objp.ColorCode),
                  new SqlParameter("@SizeCode",Objp.SizeCode),
                   new SqlParameter("@VendorCode",Objp.VendorCode),
                new SqlParameter("@FashionType",Objp.FashionType),

                };
                dtt = objdb.ExecProcDataTable(ProcName, param);

            }
            else
            {
                SqlParameter[] param = new SqlParameter[]
               {
                new SqlParameter("@Action", Objp.Action),
                new SqlParameter("@companycode", Objp.CompanyCode),
                new SqlParameter("@centercode", Objp.SSCode),
                new SqlParameter("@productgroupcode", Objp.GroupCode),
                new SqlParameter("@productcode", Objp.ItemCode),
                new SqlParameter("@onlineprice", Objp.OnlinePrice),
                new SqlParameter("@stockquantity", Objp.Quantity),
                new SqlParameter("@productdescription", Objp.Description),
                new SqlParameter("@productmainimageurl", Objp.Url),
                new SqlParameter("@isavilableforsale", Objp.IsActive),
                new SqlParameter("@entryby", Objp.EntryBy),
                new SqlParameter("@ProductSpecification", Objp.ProductSpacification),
                new SqlParameter("@type_InsertProductImageUrl", dt),
                new SqlParameter("@ProductName", Objp.ProductName),
                new SqlParameter("@MainCategoryCode", Objp.MainCategoryCode),
                new SqlParameter("@SubCategoryCode", Objp.SubCategoryCode),
                new SqlParameter("@ProductType", Objp.ProductType),
                new SqlParameter("@IsInventoryProduct", Objp.IsInventoryProduct),
                new SqlParameter("@RegularPrice", Objp.RegularPrice),
                new SqlParameter("@SalePrice", Objp.SalePrice),
                new SqlParameter("@DiscPer", Objp.DiscPer),
                new SqlParameter("@KeyWords", Objp.KeyWords),
                new SqlParameter("@type_ProductVariationList", null),
                new SqlParameter("@GSTRate", Objp.GSTPer),
                new SqlParameter("@CessRate", Objp.CessRate),
                new SqlParameter("@ItemBarCode", Objp.ItemBarCode),
                new SqlParameter("@PurchaseRate", Objp.PurchaseRate_Loose),
                new SqlParameter("@isPurchasetaxInclude", Objp.Purchase_taxIncludeExclude),
                new SqlParameter("@isSaletaxInclude", Objp.Sale_taxIncludeExclude),
                new SqlParameter("@HSNCode", Objp.HSNCode),
                new SqlParameter("@Manufacturerid", Objp.Manufacturerid),
                new SqlParameter("@UOMId", Objp.UOM),
                new SqlParameter("@LowStockAlert", Objp.LowStockAlert),
                new SqlParameter("@type_InsertMultiVarient", dt1),
                new SqlParameter("@RefundAndreturnPolicy", Objp.ReturnAndRefundPolicy),
                new SqlParameter("@taxtype", Objp.Taxtype11),
                new SqlParameter("@gstamt", Objp.GstAmt),
                new SqlParameter("@GstExcludeRate", Objp.NonTaxRate),
                 new SqlParameter("@VendorCode",Objp.VendorCode),
                   new SqlParameter("@FashionType",Objp.FashionType),
            };
                dtt = objdb.ExecProcDataTable(ProcName, param);

            }
            return dtt;
        }
        public DataTable UpdateOnlineProductDetails(PropertyClass Objp, string ProcName, DataTable dt, DataTable dt1)
        {
            DataTable dtt = new DataTable();
            DataTable dtSizeCodeList = new DataTable();
            DataTable dtColorCodeList = new DataTable();
            if (Objp.SizeCode != null)
            {
                string _SizeCode = Convert.ToString(Objp.SizeCode);
                string[] arr_SizeCode = _SizeCode.Split(',');


                dtSizeCodeList.Columns.Add("SizeId");

                for (int i = 0; i < arr_SizeCode.Length; i++)
                {
                    dtSizeCodeList.Rows.Add(arr_SizeCode[i].ToString());
                }
            }
            else
            {
                dtSizeCodeList = null;
            }

            if (Objp.ColorCode != null)
            {
                string _ColorCode = Convert.ToString(Objp.ColorCode);
                //string fy1 = "";
                string[] arr_ColorCode = _ColorCode.Split(',');
                dtColorCodeList.Columns.Add("ColorId");

                for (int i = 0; i < arr_ColorCode.Length; i++)
                {
                    dtColorCodeList.Rows.Add(arr_ColorCode[i].ToString());
                }
            }
            else
            {
                dtColorCodeList = null;

            }
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@centercode",Objp.SSCode),
                new SqlParameter("@productgroupcode",Objp.GroupCode),
                new SqlParameter("@ItemCode",Objp.ItemCode),
                new SqlParameter("@onlineprice",Objp.OnlinePrice),
                new SqlParameter("@stockquantity",Objp.Quantity),
                new SqlParameter("@productdescription",Objp.Description),
                new SqlParameter("@productmainimageurl",Objp.Url),
                new SqlParameter("@isavilableforsale",Objp.IsActive),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@ProductSpecification",Objp.ProductSpacification),
                new SqlParameter("@type_InsertProductImageUrl",dt),
                new SqlParameter("@ProductName",Objp.ProductName),
                new SqlParameter("@MainCategoryCode",Objp.MainCategoryCode),
                new SqlParameter("@VendorId",Objp.VendorCode),
                new SqlParameter("@SubCategoryCode",Objp.SubCategoryCode),
                new SqlParameter("@ProductType",Objp.ProductType),
                new SqlParameter("@IsInventoryProduct",Objp.IsInventoryProduct),
                new SqlParameter("@RegularPrice",Objp.RegularPrice),
                new SqlParameter("@SalePrice",Objp.SalePrice),
                new SqlParameter("@DiscPer",Objp.DiscPer),
                new SqlParameter("@KeyWords",Objp.KeyWords),
                new SqlParameter("@type_ProductVariationList",null),
                new SqlParameter("@GSTRate",Objp.GSTPer),
                new SqlParameter("@CessRate",Objp.CessRate),
                new SqlParameter("@ItemBarCode",Objp.ItemBarCode),
                new SqlParameter("@PurchaseRate",Objp.PurchaseRate_Loose),
                new SqlParameter("@isPurchasetaxInclude",Objp.Purchase_taxIncludeExclude),
                new SqlParameter("@isSaletaxInclude",Objp.Sale_taxIncludeExclude),
                new SqlParameter("@HSNCode",Objp.HSNCode),
                new SqlParameter("@Manufacturerid",Objp.Manufacturerid),
                new SqlParameter("@UOMId",Objp.UOM),
                new SqlParameter("@LowStockAlert",Objp.LowStockAlert),
                new SqlParameter("@type_InsertMultiVarient",dt1),
                new SqlParameter("@RefundAndreturnPolicy",Objp.ReturnAndRefundPolicy),
                new SqlParameter("@taxtype",Objp.Taxtype11),
                new SqlParameter("@DailydealId",Objp.Dailydeal),
                new SqlParameter("@gstamt",Objp.GstAmt),
                new SqlParameter("@GstExcludeRate",Objp.NonTaxRate),
                new SqlParameter("@ColorCode",dtColorCodeList),
                new SqlParameter("@SizeCode", dtSizeCodeList),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataTable InsertReview(string CustomerId, string CustomerName, string Description, string ProductCode, string ReviewStatus, string image=null)
        {

            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
          new SqlParameter("@CustomerId",CustomerId),
          new SqlParameter("@CustomerName",CustomerName),
          new SqlParameter("@Description",Description),
          new SqlParameter("@ProductCode",ProductCode),
          new SqlParameter("@reviewstatus",ReviewStatus),
          new SqlParameter("@image",image),

      };
            dt = objdb.ExecProcDataTable("[proc_addReview]", parm);
            return dt;
        }
        public DataTable GetOnlineProducts(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@StartDate",Objp.mDate),
                new SqlParameter("@EndDate",Objp.eDate),
                new SqlParameter("@CenterCode",Objp.CompanyCode),
                new SqlParameter("@Searchkey",Objp._Searchkey),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetOnlineVendorProducts(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@StartDate",Objp.mDate),
                new SqlParameter("@EndDate",Objp.eDate),
                new SqlParameter("@CenterCode",Objp.CompanyCode),
                new SqlParameter("@Searchkey",Objp._Searchkey),
                 new SqlParameter("@VendorId",Objp.VendorCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetOnlineProducts1(string FromDate, string todate, string CenterCode, string Action, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@StartDate",FromDate),
                new SqlParameter("@EndDate",todate),
                 new SqlParameter("@CenterCode",CenterCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        internal DataTable BindMainCategory()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT SrNo,ProductCategory,'../CategoryImages/'+CategoryImage AS CatImage FROM tbl_MainCategory WHERE ParrentCategoryId=0 AND IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }


        internal DataTable BindFooterCategory()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Top 5  SrNo,ProductCategory,'../CategoryImages/'+CategoryImage AS CatImage FROM tbl_MainCategory WHERE ParrentCategoryId=0 AND IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }



        internal DataTable BindAllCategory()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS SrNo,'Select Category' AS ProductCategory UNION ALL SELECT SrNo,ProductCategory FROM tbl_MainCategory WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindMenuTop()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Top 10 SrNo,ProductCategory,'../CategoryImages/'+CategoryImage AS CatImg,(SELECT count(1) FROM tbl_MainCategory WHERE ParrentCategoryId=a.SrNo AND IsActive=1)cnt FROM tbl_MainCategory a WHERE ParrentCategoryId=0 AND IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindMenuSubMenu(string ParrentCat)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT SrNo,ProductCategory,ParrentCategoryId,'../CategoryImages/'+CategoryImage AS CatImg FROM tbl_MainCategory WHERE ParrentCategoryId <>0 AND IsActive=1 AND ParrentCategoryId='" + ParrentCat + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        public DataTable GetDashboardProducts(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
               new SqlParameter("@StockId",Objp.StockistId)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataSet GetDashboardProductsAll(PropertyClass Objp, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
               new SqlParameter("@StockId",Objp.StockistId)
            };
            dtt = objdb.ExecProcDataSet(ProcName, param);
            return dtt;
        }
        public DataSet GetSingleProductDetail(PropertyClass Objp, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ProductCode",Objp.ItemCode),
                new SqlParameter("@CatId",Objp.MainCategoryCode),
                new SqlParameter("@varId",Objp.VariationId),
                new SqlParameter("@StockiestId",Objp.StockistId),
                new SqlParameter("@SearchText",Objp.MainCategoryName),
                new SqlParameter("@search",Objp.search),
               new SqlParameter("@Sort",Objp.Sort),
               new SqlParameter("@Min",Objp.Min),
               new SqlParameter("@Max",Objp.Max)


            };
            dtt = objdb.ExecProcDataSet(ProcName, param);
            return dtt;
        }

        public DataSet GetStockWiseProductDetail(PropertyClass Objp, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ProductCode",null),
                new SqlParameter("@CatId",Objp.StockistId),
                new SqlParameter("@varId",null)
            };
            dtt = objdb.ExecProcDataSet(ProcName, param);
            return dtt;
        }

        internal DataTable InsertUpdateCart(string Action, string CustomerId, string IpAddress, string ProductId, int Qty, string AttrId = null, string varId = null, string SizeId = null, string color = null)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@CustomerId",CustomerId),
                new SqlParameter("@IpAddress",IpAddress),
                new SqlParameter("@ProductId",ProductId),
                new SqlParameter("@Quantity",Qty),
                new SqlParameter("@AttrId",AttrId),
                new SqlParameter("@varId",varId),
                  new SqlParameter("@SizeId",SizeId),
                   new SqlParameter("@ColorId",color)
            };
            dt = objdb.ExecProcDataTable("[proc_InsertUpdateCartDetails]", parm);
            return dt;
        }
        internal DataTable InsertTempCartToreal(string CustomerId, string IpAddress)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@IpAddress",IpAddress),
                new SqlParameter("@CustomerId",CustomerId)
            };
            dt = objdb.ExecProcDataTable("[proc_insertTempCartList]", parm);
            return dt;
        }
        internal DataTable getCartDetails(string CustomerId, string Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@CustomerCode",CustomerId)
            };
            dt = objdb.ExecProcDataTable("[proc_getCartDetails]", parm);
            return dt;
        }

        internal DataTable getwishlist(string EntryBy, string Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@CustomerCode",EntryBy)
            };
            dt = objdb.ExecProcDataTable("[proc_getCartDetails]", parm);
            return dt;
        }

        internal DataTable RemoveFromCart(string CustomerId, string Action, string ProductID)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@UserCode",CustomerId),
                new SqlParameter("@ProductID",ProductID),
            };
            dt = objdb.ExecProcDataTable("[proc_RemoveFromCart]", parm);
            return dt;
        }
        internal DataTable AttributeMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@RefId",Objp.RespoCode),
                new SqlParameter("@attributename",Objp.BanKAccName),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        internal DataTable VariationMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@RefId",Objp.RespoCode),
                new SqlParameter("@attributeid",Objp.AccountCode),
                new SqlParameter("@varriationname",Objp.DepartmentName),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        internal DataTable InsertProductVariations(PropertyClass Objp, string ProcName, DataTable dt1)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@RequestId",Objp.RespoCode),
                new SqlParameter("@AttributeId",Objp.AttributeId),
                new SqlParameter("@VariationId",Objp.VariationId),
                new SqlParameter("@type_VariationList",dt1),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        internal DataTable getAttrData(string AttrId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT a.SrNo,b.AttributeName+'-'+VarriationName AS VarriationName,AttributeId FROM tbl_Variations a JOIN tbl_Attributes b ON a.AttributeId=b.SrNo WHERE a.IsActive=1 AND a.AttributeId='" + AttrId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable UpdateProductFeatureStatus(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ProductId",Objp.ItemCode),
                new SqlParameter("@Status",Objp.Status),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        internal DataTable CheckuserLogin(string MobileNo)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT EmailAddress FROM tbl_Login WHERE EmailAddress='" + MobileNo + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetBranch()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT SSCode,SSName FROM tbl_SuperStockiest WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindItemheadDropDownFinishedGoods(string Companycode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS ItemCode,'Select Item' AS ItemName UNION ALL SELECT ItemCode,ItemName FROM tbl_ItemHeadMaster WHERE IsActive=1 and CompanyCode=Isnull('" + Companycode + "',CompanyCode) AND (Inventorytype='Finished Good' or Inventorytype='Common Good')";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable GetOrderConfirmDetail(string Orderid)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT a.OrderId,a.PaymentMode,b.Name,b.MobileNo,b.AddressType,b.Address+' '+isnull(b.LandMark,'')+' ,'+b.CityName+'-'+b.PinCode AS Address,a.NetPayable FROM tbl_OnlineOrderDetail a JOIN tbl_DeliveryAddressDetails b ON a.DeliveryAddressId = b.SrNo WHERE OrderId = '" + Orderid + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataSet GetCustomerDashBoard(PropertyClass Objp, string ProcName, string OrderId)
        {
            DataSet dt = new DataSet();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@deliveryboy",Objp.deliveryboy),
                new SqlParameter("@MobileNo",Objp.MobileNo),
                new SqlParameter("@orderId",OrderId)
            };
            dt = objdb.ExecProcDataSet(ProcName, parm);
            return dt;
        }
        internal DataTable GetCustomerDashBoardnew(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@deliveryboy",Objp.deliveryboy),
                new SqlParameter("@MobileNo",Objp.MobileNo),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        internal DataTable GetMyprofile(PropertyClass al)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                   new SqlParameter("@Action",al.Action),
                new SqlParameter("@CustomerId",al.CustomerId),
                  new SqlParameter("@Name",al.SSName),
                    new SqlParameter("@MobileNo",al.ContactNo),
                      new SqlParameter("@Email",al.EmailAddress),
                        new SqlParameter("@Address",al.Address),

            };
            dt = objdb.ExecProcDataTable("[proc_BindCustomerDashBoard]", parm);
            return dt;
        }
        internal DataTable BannerMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@BannerType",Objp.OfferType),
                new SqlParameter("@BannerTitle",Objp.OfferTitle),
                new SqlParameter("@DiscountType",Objp.CardType),
                new SqlParameter("@DiscountValue",Objp.DiscPer),
                new SqlParameter("@CategoryId",Objp.MainCategoryCode),
                new SqlParameter("@BannerImage",Objp.PurchaseFile),
                new SqlParameter("@EntryBy",Objp.EntryBy),
                new SqlParameter("@RefId",Objp.RespoCode),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        internal DataTable BlogMaster(Blogmaster Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@id",Objp.Id),
                new SqlParameter("@Title ",Objp.Title),
                new SqlParameter("@Image",Objp.PurchaseFile),
                new SqlParameter("@Description ",Objp.Description),
                new SqlParameter("@EntryBy",Objp.EntryBy),

            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        internal DataTable InsertCancelRequest(PropertyClass Objp, string ProcName)
        {
            DateTime? delDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                delDate = Convert.ToDateTime(Objp.mDate);
            }
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OrderId",Objp.OrderId),
                new SqlParameter("@CancelReason",Objp.Description),
                new SqlParameter("@updatedate",delDate),
                new SqlParameter("@status",Objp.Status),
                new SqlParameter("@deliveryboy",Objp.deliveryboy),
                new SqlParameter("@deliveryboyReason",Objp.deliveryboyReason),
                new SqlParameter("@deliveryby",Objp.DeliveryTo),
                new SqlParameter("@paymentCollectionBy",Objp.SupplierAccCode),
                new SqlParameter("@paystatus",Objp.paystatus),
                new SqlParameter("@ReturnReason",Objp.Description),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        internal DataTable ManufacturerMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ManufacturerName",Objp.GroupName),
                new SqlParameter("@ManufacturerImage",Objp.PurchaseFile),
                new SqlParameter("@EntryBy",Objp.EntryBy),
                new SqlParameter("@RefId",Objp.RespoCode),
                new SqlParameter("@BlogId",Objp.BlogId)
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        internal DataTable GetAutoCompeleteItems(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Item",Objp.ItemName)
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        internal DataTable DeleteProductDetails1(PropertyClass Objp, string ItemCode, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ProductId",ItemCode),
                new SqlParameter("@CompanyCode",Objp.BranchCode),
                new SqlParameter("@EntryBy",Objp.EntryBy)

            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        internal DataSet DeleteProductDetails(PropertyClass Objp, string ProcName)
        {
            DataSet dt = new DataSet();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ProductId",Objp.ItemCode),
                new SqlParameter("@CompanyCode",Objp.BranchCode),
                new SqlParameter("@EntryBy",Objp.EntryBy)

            };
            dt = objdb.ExecProcDataSet(ProcName, parm);
            return dt;
        }

        internal DataTable GetItemDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ItemCode",Objp.ItemCode),
                new SqlParameter("@VarId",Objp.VariationId),

            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        internal DataSet GetItemDetailsForSimpleAndAttribute(PropertyClass Objp, string ProcName)
        {
            DataSet dt = new DataSet();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@ItemCode",Objp.ItemCode),
                new SqlParameter("@VarId",Objp.VariationId),

            };
            dt = objdb.ExecProcDataSet(ProcName, parm);
            return dt;
        }

        public DataTable DeletePurchaseBillDetail(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@BranchCode",Objp.BranchCode),
                new SqlParameter("@InvoiceNo",Objp.InvoiceNo),
                new SqlParameter("@DeleteBy",Objp.EntryBy),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetPromoCode(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@PromoCode",Objp.HSNCode),
                new SqlParameter("@Orderamount",Objp.Amount),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataTable InsertUpdatePincode(PropertyClass obj, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@PinCode",obj.PinCode),
                    new SqlParameter("@Area",obj.Area),
                    new SqlParameter("@DeliveryDays",obj.DeliveryDays),
                    new SqlParameter("@DeliveryCharges",!string.IsNullOrEmpty(obj.DelCharges)?Convert.ToDecimal(obj.DelCharges):0),
                    new SqlParameter("@MinDelAmount",!string.IsNullOrEmpty(obj.MinDelCharges)?Convert.ToDecimal(obj.MinDelCharges):0),
                    new SqlParameter("@SrNo",obj.RespoCode),
                    new SqlParameter("@Status",obj.Status)
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        public DataTable InsertUpdateCompanyDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@stockiestCode",Objp.SSCode),
                new SqlParameter("@ssname",Objp.SSName),
                new SqlParameter("@contactperson",Objp.ContactPerson),
                new SqlParameter("@contactno",Objp.ContactNo),
                new SqlParameter("@emailaddress",Objp.EmailAddress),
                new SqlParameter("@pincode",Objp.PinCode),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@statecode",Objp.StateId),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@gstno",Objp.GstNo),
                new SqlParameter("@panno",Objp.PanNo),
                new SqlParameter("@gstdocument",Objp.GstDoc),
                new SqlParameter("@pandocument",Objp.PanDoc),
                new SqlParameter("@companyId",Objp.CompanyCode),
                new SqlParameter("@CompanyLogo",Objp.CompanyLogo),
                new SqlParameter("@AadhaarNo",Objp.AadhaarNo),
                new SqlParameter("@UserName",Objp.UserName),
                new SqlParameter("@BankName",Objp.Bankname),
                new SqlParameter("@BankAccountName",Objp.BanKAccName),
                new SqlParameter("@BankBranch",Objp.branchname),
                new SqlParameter("@AccountNumber",Objp.accountno),
                new SqlParameter("@IFSCCode",Objp.ifsccode),
                new SqlParameter("@InvoicePrefix",Objp.InvoicePrefix),
                new SqlParameter("@SaleInvoiceNotes",Objp.Description),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@IsLatterPad",Objp.IsLatterPad)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        //public string apicall(string url)
        //{
        //    HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        //    try
        //    {
        //        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
        //        StreamReader sr = new StreamReader(httpres.GetResponseStream());
        //        string results = sr.ReadToEnd();
        //        sr.Close();
        //        return results;
        //    }
        //    catch
        //    {
        //        return "0";
        //    }
        //}
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
        public DataTable AccountGroupMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@group_name",Objp.GroupName),
                new SqlParameter("@parent_groupcode",Objp.parent_groupcode),
                new SqlParameter("@close_to",Objp.close_to),
                new SqlParameter("@userid",Objp.EntryBy),
                new SqlParameter("@GroupId",Objp.GroupCode),
                new SqlParameter("@FinancialYear",Objp.FinancialYear),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable AccountHeadMaster(PropertyClass Objp, string ProcName)
        {
            DateTime? EffectiveDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                EffectiveDate = Convert.ToDateTime(Objp.mDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@account_name",Objp.AccountName),
                new SqlParameter("@group_code",Objp.GroupCode),
                new SqlParameter("@pan",Objp.PanNo),
                new SqlParameter("@opening_amt",Objp.CoupenAmount),
                new SqlParameter("@name",Objp.AccountName),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@city",Objp.CityName),
                new SqlParameter("@mobile",Objp.ContactNo),
                new SqlParameter("@email",Objp.EmailAddress),
                new SqlParameter("@comapanyid",Objp.CompanyCode),
                new SqlParameter("@userid",Objp.EntryBy),
                new SqlParameter("@gstno",Objp.GstNo),
                new SqlParameter("@accountno",Objp.accountno),
                new SqlParameter("@ifsccode",Objp.ifsccode),
                new SqlParameter("@branch",Objp.branchname),
                new SqlParameter("@statecode",Objp.StCode),
                new SqlParameter("@BankName",Objp.BanKAccName),
                new SqlParameter("@AccCode",Objp.AccountCode),
                new SqlParameter("@OpeningType",Objp.OfferType),
                new SqlParameter("@EffectiveDate",EffectiveDate),
                new SqlParameter("@FinancialYear",Objp.FinancialYear),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;
            return FinYear.Trim();
        }
        internal DataTable BindCloseUnderGroups()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Id,Title FROM mst_AccountGroupCloseTo WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindAccountGroupDLLAll()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS group_code,'Select Group Code' AS group_name UNION ALL SELECT group_code,group_name FROM Accounts_Group";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }


        #region CRM
        public DataTable getDetailsdt(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
             {
              //new SqlParameter("@startdate",Objp.startDate),
              // new SqlParameter("@enddate",Objp.EndDate),
              // new SqlParameter("@leadby",Objp.leadby),
              // new SqlParameter("@closedby",Objp.closedby),
              // new SqlParameter("@status",Objp.Status),
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@UserId",Objp.UserName),
                new SqlParameter("@Role",Objp.RoleId)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetLeadSummaryReport(PropertyClass Objp, string ProcName)
        {
            DateTime? fDate = null;
            DateTime? tDate = null;
            if (!string.IsNullOrEmpty(Objp.startDate))
            {
                fDate = Convert.ToDateTime(Objp.startDate);
            }
            if (!string.IsNullOrEmpty(Objp.EndDate))
            {
                tDate = Convert.ToDateTime(Objp.EndDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@EmpCode",Objp.EmployeeCode),
                new SqlParameter("@StartDate",fDate),
                new SqlParameter("@EndDate",tDate),
                new SqlParameter("@Status",Objp.Status),
                new SqlParameter("@CompanyCode",Objp.CompanyName),
                  new SqlParameter("@LeadId",Objp.LeadId),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertLeadDetails(PropertyClass objP, DataTable dt)
        {
            DataTable dtt = new DataTable();
            try
            {
                string sql = "Proc_LeadMaster";
                SqlParameter[] sqlParams = {
                new SqlParameter("@Action",SqlDbType.VarChar),
                new SqlParameter("@name",SqlDbType.VarChar),
                new SqlParameter("@mobileno",SqlDbType.VarChar),
                new SqlParameter("@altmobileno",SqlDbType.VarChar),
                new SqlParameter("@emailid",SqlDbType.VarChar),
                new SqlParameter("@address",SqlDbType.VarChar),
                new SqlParameter("@stateid",SqlDbType.Int),
                new SqlParameter("@cityid",SqlDbType.VarChar),
                new SqlParameter("@entryby",SqlDbType.VarChar),
                new SqlParameter("@CompanyName",SqlDbType.VarChar),
                new SqlParameter("@employeecode",SqlDbType.VarChar),
                new SqlParameter("@leadtitle",SqlDbType.VarChar),
                new SqlParameter("@category",SqlDbType.Int),
                new SqlParameter("@description",SqlDbType.VarChar),
                new SqlParameter("@leaddate",SqlDbType.Date),
                new SqlParameter("@status",SqlDbType.VarChar),
                new SqlParameter("@nextfollowupdate",SqlDbType.Date),
                new SqlParameter("@CustomerType",SqlDbType.VarChar),
                new SqlParameter("@CustomerCode",SqlDbType.VarChar),
                   new SqlParameter("@type_LeadItems",SqlDbType.Structured),

            };
                sqlParams[0].Value = objP.Action;
                sqlParams[1].Value = objP.SSName;
                sqlParams[2].Value = objP.MobileNo;
                sqlParams[3].Value = objP.altmobileno;
                sqlParams[4].Value = objP.EmailId;
                sqlParams[5].Value = objP.Address;
                sqlParams[6].Value = objP.StateId;
                sqlParams[7].Value = objP.CityName;
                sqlParams[8].Value = objP.EntryBy;
                sqlParams[9].Value = objP.CompanyName;
                sqlParams[10].Value = objP.EMPCodeNew;
                sqlParams[11].Value = objP.LeadTitle;
                sqlParams[12].Value = objP.Id;
                sqlParams[13].Value = objP.Description;
                sqlParams[14].Value = objP.LeadDate;
                sqlParams[15].Value = objP.Status;
                sqlParams[16].Value = objP.NextFollowUpDate;
                sqlParams[17].Value = objP.CustType;
                sqlParams[18].Value = objP.CustomerId;
                //sqlParams[19].Value = dt;
                dtt = objdb.ExecProcDataTable(sql, sqlParams);
                return dtt;
            }
            catch (Exception ex)
            {
                return dtt;
            }
        }

        public DataTable getFollowUps(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@LeadId",Objp.LeadId)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public int InsertFollowUpDetails(PropertyClass objP)
        {
            int r = 0;
            try
            {
                string sql = "proc_FollowUpMaster";
                SqlParameter[] sqlParams = {
                new SqlParameter("@Action",SqlDbType.VarChar),
                new SqlParameter("@LeadId",SqlDbType.VarChar),
                new SqlParameter("@employeecode",SqlDbType.VarChar),
                new SqlParameter("@followupdate",SqlDbType.Date),
                new SqlParameter("@nextfollowupdate",SqlDbType.Date),
                new SqlParameter("@comments",SqlDbType.VarChar),
                new SqlParameter("@status",SqlDbType.VarChar),
                new SqlParameter("@entryby",SqlDbType.VarChar),
                new SqlParameter("@closedby",SqlDbType.VarChar),
                new SqlParameter("@projectcost",SqlDbType.Decimal),
                new SqlParameter("@advanceamount",SqlDbType.Decimal),
                new SqlParameter("@projectmode",SqlDbType.VarChar),
                new SqlParameter("@isgst",SqlDbType.Bit),
                new SqlParameter("@paymode",SqlDbType.VarChar),
                new SqlParameter("@projectdescription",SqlDbType.VarChar),
                new SqlParameter("@projecttitle",SqlDbType.VarChar),
                new SqlParameter("@startdate",SqlDbType.Date),
                new SqlParameter("@expdeliverydate",SqlDbType.Date),

                 new SqlParameter("@ChqDDNo",SqlDbType.VarChar),
                new SqlParameter("@ChqDDDate",SqlDbType.Date),
                new SqlParameter("@IFSCCode",SqlDbType.VarChar),
                new SqlParameter("@BankAccName",SqlDbType.VarChar),
                new SqlParameter("@TRANSACTIONID",SqlDbType.VarChar),
                new SqlParameter("@TRANSACTIONDate",SqlDbType.Date),
                new SqlParameter("@BankAccId",SqlDbType.VarChar)
                 //new SqlParameter("@type_LeadItems",SqlDbType.Structured)

            };
                sqlParams[0].Value = objP.Action;
                sqlParams[1].Value = objP.LeadId;
                sqlParams[2].Value = objP.EmployeeCode;
                sqlParams[3].Value = objP.FollowUpDate;
                sqlParams[4].Value = objP.NextFollowUpDate;
                sqlParams[5].Value = objP.Description;
                sqlParams[6].Value = objP.Status;
                sqlParams[7].Value = objP.EntryBy;
                sqlParams[8].Value = objP.closedby;
                sqlParams[9].Value = objP.projectcost;
                sqlParams[10].Value = objP.advanceamount;
                sqlParams[11].Value = objP.projectmode;
                sqlParams[12].Value = objP.isgst;
                sqlParams[13].Value = objP.PayMode;
                sqlParams[14].Value = objP.ProductSpacification;
                sqlParams[15].Value = objP.LeadTitle;
                sqlParams[16].Value = objP.LeadDate;
                sqlParams[17].Value = objP.ExpiryDate;

                sqlParams[18].Value = objP.chqddNo;
                sqlParams[19].Value = objP.chqddDate;
                sqlParams[20].Value = objP.ifsccode;
                sqlParams[21].Value = objP.BanKAccName;
                sqlParams[22].Value = objP.BankTransId;
                sqlParams[23].Value = objP.TransDate;
                sqlParams[24].Value = objP.BankAccId;
                //sqlParams[25].Value = objP.dt1;
                r = objdb.ExecuteNonQueryProc(sql, sqlParams);
                return r;
            }
            catch (Exception ex)
            {
                return r;
            }
        }



        #endregion CRM

        #region HRM

        public DataTable BindMonthYearDDl(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        internal DataTable CheckPayScheduleUpdate(string CompanyId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT count(PayCode) AS PayCount FROM tbl_PayRunDetails WHERE CompanyCode='" + CompanyId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindDesignationDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS DesignationCode,'Select' AS DesignationName UNION ALL SELECT DesignationCode,DesignationName FROM tbl_DesignationMaster WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        public DataTable AddEmployee(PropertyClass Objp, string ProcName)
        {
            DateTime? BirthDate = null;
            DateTime? joinDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                BirthDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                joinDate = Convert.ToDateTime(Objp.eDate);
            }

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@centercode",Objp.BranchCode),
                new SqlParameter("@employee_name",Objp.AccountName),
                new SqlParameter("@employee_father_name",Objp.FatherName),
                new SqlParameter("@gender",Objp.strgender),
                new SqlParameter("@date_of_joining",joinDate),
                new SqlParameter("@designation",Objp.designation),
                new SqlParameter("@workemailid",Objp.workemailid),
                new SqlParameter("@entryby",Objp.BranchCode),
                new SqlParameter("@EPFNo",Objp.EPFNo),
                new SqlParameter("@ESINo",Objp.ESINo),

                new SqlParameter("@EmpCode",Objp.EmpCode),
                new SqlParameter("@PersonalEmailId",Objp.EmailAddress),
                new SqlParameter("@Contact_Number",Objp.ContactNo),
                new SqlParameter("@DOB",BirthDate),
                new SqlParameter("@City",Objp.CityName),
                new SqlParameter("@State",Objp.StateName),
                new SqlParameter("@Address",Objp.Address),
                new SqlParameter("@PinCode",Objp.PinCode),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@AccountHolderName",Objp.AccName),
                new SqlParameter("@BankName",Objp.Bankname),
                new SqlParameter("@AccountNumber",Objp.accountno),
                new SqlParameter("@IFSCCode",Objp.ifsccode),
                new SqlParameter("@AccountType",Objp.AccountType),
                new SqlParameter("@basicmonthly",Objp.basicmonthly),
                new SqlParameter("@hramonthly",Objp.hramonthly),
                new SqlParameter("@conveyanceallowancemonthly",Objp.conveyanceallowancemonthly),
                new SqlParameter("@fixedallowancemonthly",Objp.fixedallowancemonthly),
                new SqlParameter("@monthlyctc",Objp.monthlyctc),
                new SqlParameter("@annuallyctc",Objp.annuallyctc),
                new SqlParameter("@PanNo",Objp.PanNo),
                new SqlParameter("@BasicPer",Objp.BasicPer),
                new SqlParameter("@HRAPer",Objp.HRAPer),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable EmployeeDetails(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@EmployeeCode",Objp.EmpCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataSet BindStockDetails(PropertyClass Objp, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@userCode",Objp.UserName),
                new SqlParameter("@ChallanNo",Objp.InvoiceNo),

            };
            dtt = objdb.ExecProcDataSet(ProcName, param);
            return dtt;
        }


        public DataTable InsertSalaryComponent(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@branchcode",Objp.BranchCode),
                new SqlParameter("@h_type",Objp.AccountType),
                new SqlParameter("@h_name",Objp.MenuTitle),
                new SqlParameter("@entryby",Objp.EntryBy),
                 new SqlParameter("@RefCode",Objp.AccCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable DesignationMaster(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@RefCode",Objp.AccCode),
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@designationname",Objp.OfferTitle),
                new SqlParameter("@entryby",Objp.EntryBy),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertPaySchedule(PropertyClass Objp, string ProcName, DataTable dt)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@centercode",Objp.BranchCode),
                new SqlParameter("@monthly_salary_base",Objp.Monthly_Salary_Based_On),
                new SqlParameter("@companyworking_days",Objp.WorkingDays),
                new SqlParameter("@startfirstpayrollon",Objp.First_PayrollStartsOn),

                 new SqlParameter("@MonthName",Objp.MonthName),
                new SqlParameter("@Year",Objp.Year),
                new SqlParameter("@table_WorkingDaysofWeek",dt)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetPayRunDetails(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@MonthName",Objp.MonthName),
                new SqlParameter("@Year",Objp.Year),
                new SqlParameter("@pCode",Objp.AccCode)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;

        }
        public DataTable InsertPayRunDetails(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@companycode",Objp.CompanyCode),
                new SqlParameter("@centercode",Objp.BranchCode),
                new SqlParameter("@month",Objp.MonthName),
                new SqlParameter("@year",Objp.Year),
                new SqlParameter("@entryby",Objp.EntryBy),
                 new SqlParameter("@pCode",Objp.AccCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        internal DataTable BindEarningsDDl()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS Code,'Select' AS name UNION ALL SELECT H_Name AS Code,H_Name AS name FROM tbl_Salary_Allowance_DeductionSetup WHERE H_Type='Earning' AND isActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindDeductionDDl()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS Code,'Select' AS name UNION ALL SELECT H_Name AS Code,H_Name AS name FROM tbl_Salary_Allowance_DeductionSetup WHERE H_Type='Deduction' AND isActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        public DataTable BindEmployee_SalaryDetails(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@EmployeeCode",Objp.EmpCode),
                new SqlParameter("@PayCode",Objp.AccCode)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable ApprovePayRunDetails(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@PayCode",Objp.AccCode),
                new SqlParameter("@ApproveBy",Objp.EntryBy),
                new SqlParameter("@Status",Objp.Status),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable updatepayRun(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@PayCode",Objp.AccCode),
                new SqlParameter("@EmpCode",Objp.EmpCode),
                new SqlParameter("@LOPDays",Objp.LOPDays),
                new SqlParameter("@ActualDays",Objp.ActualDays),
                new SqlParameter("@month",Objp.MonthName),
                new SqlParameter("@year",Objp.Year),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        #endregion HRM

        #region Manufacturing
        public DataTable DelProductConsumption(string Action, string Id)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Id",Id)
            };
            dtt = objdb.ExecProcDataTable("[Proc_ProductConsumption]", param);
            return dtt;
        }
        public DataTable GetItemsForConsumption(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetProductConsumption(string Action, string Id, string company)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Id",Id),
                new SqlParameter("@Companycode",company)
            };
            dtt = objdb.ExecProcDataTable("[Proc_ProductConsumption]", param);
            return dtt;
        }

        public DataTable SaveproductConsumption(PropertyClass Objp, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProductId",Objp.ItemID),
                new SqlParameter("@Uom",Objp.UOM),
                new SqlParameter("@type_ConsumptedItem",dt),
                new SqlParameter("@Action","1")
            };
            dtt = objdb.ExecProcDataTable("Proc_ProductConsumption", param);
            return dtt;
        }
        public DataTable GetFinalProduct(string Action, string Id, string company)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Id",Id),
                 new SqlParameter("@companyCode",company)
            };
            dtt = objdb.ExecProcDataTable("[Proc_FinalProductEntry]", param);
            return dtt;
        }
        public DataTable SaveFinalProduct(PropertyClass Objp, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProductId",Objp.ItemID),
                new SqlParameter("@Uom",Objp.UOM),
                  new SqlParameter("@Quantity",Objp.Quantity),
                   new SqlParameter("@Pdate",Objp.ProductionDate),
                new SqlParameter("@type_FinalizeItem",dt),
                new SqlParameter("@Action","1")
            };
            dtt = objdb.ExecProcDataTable("Proc_FinalProductEntry", param);
            return dtt;
        }

        #endregion Manufacturing

        public DataTable ContactUs(ClsContactUs Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@SrNo",Objp.SrNo),
                new SqlParameter("@FullName",Objp.FullName),
                new SqlParameter("@MobileNo",Objp.mobileNo),
                new SqlParameter("@EmailAddress",Objp.EmailAddress),
                new SqlParameter("@Subject",Objp.Subject),
                new SqlParameter("@Message",Objp.Message),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable MemberShipMaster(ClsMemberShip objcls, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",objcls.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertNewMemberShip(ClsMemberShip objcls, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",objcls.Action)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        public DataTable InsertMemberShipMaster(ClsMemberShip objcls, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@Action",objcls.Action),
                new SqlParameter("@FullName",objcls.CustomerName),
                new SqlParameter("@ContactNo",objcls.MobileNo),
                new SqlParameter("@EmailAddress",objcls.EmailAddress),
                new SqlParameter("@Password",objcls.Password),
                new SqlParameter("@MembershipType",objcls.MemberShip)
            };
            dt = objdb.ExecProcDataTable(ProcName, sp);
            return dt;
        }


        internal DataTable Proc_GetComboOffer(string CustomerID, string Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",Action),
                    new SqlParameter("@CustomerID",CustomerID),
            };
            dt = objdb.ExecProcDataTable("Proc_GetComboOffer", parm);
            return dt;
        }




        #region Accounting

        public DataTable GetVoucherPrintData(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@VoucherId",Objp.InvoiceNo),
                new SqlParameter("@CompanyCode",Objp.CompanyCode)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        internal DataTable BindBankAccountsDropDown(string CompanyId, string GroupCode, string BranchCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS account_code,'Select' AS account_name UNION ALL SELECT account_code,(account_name+isnull(+' ('+BankName+')','')) account_name FROM Accounts_Head  WHERE group_code='" + GroupCode + "' and(company_code='" + CompanyId + "' OR company_code='" + BranchCode + "')-- AND userid='" + BranchCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        public DataTable getjournalvoucher(PropertyClass Objp, string ProcName)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                startDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                endDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@VoucherType",Objp.AccountCode),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                new SqlParameter("@BranchCode",Objp.BranchCode),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        internal DataTable BindAllAcountsSelect(string CompanyCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT ''account_code,'All'account_name UNION ALL SELECT account_code,account_name account_name FROM Accounts_Head WHERE company_code='" + CompanyCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }


        public DataTable SelectAppliedMemberShipMember()
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

            };
            // dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertChequeUpdateStatus(PropertyClass Objp, string ProcName)
        {
            DateTime? StartDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                StartDate = Convert.ToDateTime(Objp.mDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@TransId",Objp.txnId),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@branchcode",Objp.CompanyCode),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@ChequeStatus",Objp.Status),
                new SqlParameter("@ChequeClearDate",StartDate),
                new SqlParameter("@Comments",Objp.Description),
                new SqlParameter("@PayType",Objp.OfferType),
                new SqlParameter("@BounceAmount",Objp.PayableAmt),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable GetChequeDetails(PropertyClass Objp, string ProcName)
        {
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                StartDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                EndDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@StartDate",StartDate),
                new SqlParameter("@EndDate",EndDate),
                new SqlParameter("@PartyCode",Objp.CustomerId),
                new SqlParameter("@TxnId",Objp.txnId)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataTable BindBranchListSearch(string CompanyId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' Id,'All' Branch UNION ALL SELECT SSCode Id,SSName Branch FROM viewBranchList WHERE SSCode='" + CompanyId + "' UNION ALL SELECT SSCode Id,SSName Branch FROM viewBranchList WHERE CompanyCode='" + CompanyId + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        public DataTable BindDayBookReport(PropertyClass Objp, string ProcName)
        {
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                StartDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                EndDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@StartDate",StartDate),
                new SqlParameter("@EndDate",EndDate),
                new SqlParameter("@CompanyCodes",Objp.CompanyCode),
                new SqlParameter("@CenterCode",Objp.BranchCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataSet BindDayBookData(PropertyClass Objp, string ProcName)
        {
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                StartDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                EndDate = Convert.ToDateTime(Objp.eDate);
            }
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@StartDate",StartDate),
                new SqlParameter("@EndDate",EndDate),
                new SqlParameter("@CompanyCodes",Objp.CompanyCode),
                new SqlParameter("@CenterCode",Objp.BranchCode),

            };
            dtt = objdb.ExecProcDataSet(ProcName, param);
            return dtt;
        }

        internal DataTable BindBranchList(string CompanyId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' Id,'Select' Branch UNION ALL SELECT id Id,BankName Branch FROM mst_BankAccountMaster";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }


        public DataTable GetChequeReport(PropertyClass Objp, string ProcName)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                startDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                endDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@AccountCode",Objp.AccountCode),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
               new SqlParameter("@Status",Objp.Status)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataTable BindBranchAccountHeadList(string CompanyId, string BranchCode)
        {
            string sql = "";
            string Query = "";
            if (!string.IsNullOrEmpty(BranchCode))
            {
                sql += " AND comapanyid=isnull('" + BranchCode + "',comapanyid)";
            }
            DataTable dt = new DataTable();
            if (CompanyId == "C106")
            {
                Query = "SELECT '' account_code,'Select' account_name UNION ALL SELECT account_code,account_name FROM Accounts_Head WHERE (company_code='" + CompanyId + "'or company_code='OZ00001')  AND group_code NOT IN (16,4) " + sql + "";
            }
            else
            {
                Query = "SELECT '' account_code,'Select' account_name UNION ALL SELECT account_code,account_name FROM Accounts_Head WHERE (company_code='" + CompanyId + "')  AND group_code NOT IN (16,4) " + sql + "";
            }
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindModeDropDown1()
        {
            DataTable dt = new DataTable();
            //string Query = "SELECT '' group_code,'Select'group_name UNION ALL SELECT group_code,group_name FROM Accounts_Group WHERE group_code IN(16,18)";
            string Query = "SELECT '' group_code,'Select'group_name UNION ALL SELECT CAST(group_code AS VARCHAR)group_code,group_name FROM Accounts_Group WHERE group_code IN(16,18)  UNION ALL SELECT 'CREDIT' group_code,'CREDIT'group_name";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        internal DataTable BindBranchAccountHeadListAll(string CompanyId, string BranchCode)
        {
            DataTable dt = new DataTable();
            string sql = null;
            string Query = null;
            if (!string.IsNullOrEmpty(BranchCode))
            {
                sql = " AND (comapanyid=isnull('" + BranchCode + "',comapanyid) or userid=isnull('" + BranchCode + "',userid))";
            }
            if (CompanyId == "C106")
            {
                Query = "SELECT '' account_code,'Select' account_name UNION ALL SELECT account_code,(account_name+isnull(+' ('+BankName+')','')) account_name FROM Accounts_Head WHERE (company_code='" + CompanyId + "' or company_code='OZ00001') " + sql + "";
            }
            else
            {
                Query = "SELECT '' account_code,'Select' account_name UNION ALL SELECT account_code,(account_name+isnull(+' ('+BankName+')','')) account_name FROM Accounts_Head WHERE (company_code='" + CompanyId + "') " + sql + "";
            }

            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        public DataTable InsertJournalVoucherDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@BranchCode",Objp.BranchCode),
                new SqlParameter("@VoucherDate",Objp.InvoiceDate),
                new SqlParameter("@FinancialYear",Objp.FinancialYear),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@AccountCode",Objp.AccountCode),
                new SqlParameter("@AccountName",Objp.AccountName),
                new SqlParameter("@PaidAmount",Objp.PaidAmount),
                new SqlParameter("@EntryBy",Objp.EntryBy),
                new SqlParameter("@AccCode",Objp.AccCode),
                new SqlParameter("@Accname",Objp.AccName),
                new SqlParameter("@Amount",Objp.PaidAmount),
                new SqlParameter("@Narration",Objp.narration),
                new SqlParameter("@vType",Objp.AccountType)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable BindAccountBalance(PropertyClass Objp, string ProcName)
        {

            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyCode",Objp.CompanyCode),
                 new SqlParameter("@AccountCode",Objp.AccountCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable InsertVouchers(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DateTime? ChqDate = null;
            DateTime? BankTxnDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                ChqDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                BankTxnDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@BranchCode",Objp.BranchCode),
                new SqlParameter("@VoucherDate",Objp.InvoiceDate),
                new SqlParameter("@FinancialYear",Objp.FinancialYear),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@AccountCode",Objp.AccountCode),
                new SqlParameter("@AccountName",Objp.AccountName),
                new SqlParameter("@PaidAmount",Objp.PaidAmount),
                new SqlParameter("@GroupCode",Objp.GroupCode),
                new SqlParameter("@ChqNo",Objp.ChqNo),
                new SqlParameter("@ChqDate",ChqDate),
                new SqlParameter("@BankAccountName",Objp.BanKAccName),
                new SqlParameter("@BankTransId",Objp.txnId),
                new SqlParameter("@BankTransDate",BankTxnDate),
                new SqlParameter("@EntryBy",Objp.EntryBy),
                new SqlParameter("@type_InsertVoucher",dt)
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable BindTrialBalanceReport(PropertyClass Objp, string ProcName)
        {
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                StartDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                EndDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@FromDate",StartDate),
                new SqlParameter("@EndDate",EndDate),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@GroupCode",Objp.GroupCode),
                new SqlParameter("@AccountCode",Objp.AccountCode),
                new SqlParameter("@CenterCode",Objp.BranchCode),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable GetPLReport(PropertyClass Objp, string ProcName)
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                fromDate = Convert.ToDateTime(Objp.mDate);
            }
            if (!string.IsNullOrEmpty(Objp.eDate))
            {
                toDate = Convert.ToDateTime(Objp.eDate);
            }
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@FromDate",fromDate),
                new SqlParameter("@EndDate",toDate),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@CenterCode",Objp.BranchCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        internal DataTable BindCashBankAccountHeadList(string CompanyId, string BranchCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' account_code,'Select' account_name UNION ALL SELECT account_code,account_name FROM Accounts_Head WHERE company_code='" + CompanyId + "' AND userid=isnull('" + BranchCode + "',userid) AND group_code IN (16,17,18)";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        #endregion


        public DataTable ForGotPassword(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@UserId",Objp.UserName),
                new SqlParameter("@OldPassword",Objp.OldPassword),
                new SqlParameter("@NewPassword",Objp.Password),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        #region Sale Return

        public DataTable GetInvSearch(int Action, string Id)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Id",Id)

            };
            dtt = objdb.ExecProcDataTable("GetInvSearch", param);
            return dtt;
        }

        public DataTable InsertSalesOrderReturn(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CompanyId",Objp.CompanyCode),
                new SqlParameter("@branchcode",Objp.BranchCode),
                new SqlParameter("@CustomerAccountCode",Objp.SupplierAccCode),


                new SqlParameter("@grosspayable",Objp.GrossPayable),
                new SqlParameter("@nettotal",Objp.NetTotal),
                new SqlParameter("@entryby",Objp.EntryBy),
                new SqlParameter("@totalpayablegst",Objp.Payablegst),



                new SqlParameter("@typePurchaseOrderItemInsert",dt),
                new SqlParameter("@InvoiceNo",Objp.InvoiceNo),
                new SqlParameter("@VoucherId",Objp.VoucherId),


            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        #endregion

        #region Prahlad singh
        internal DataTable InsertUpdateDeliveryCharges(PropertyClass obj, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@id",obj.Id),
                    new SqlParameter("@DeliveryCharge",!string.IsNullOrEmpty(obj.DelCharges)?Convert.ToDecimal(obj.DelCharges):0),
                    new SqlParameter("@MRPFrom",!string.IsNullOrEmpty(obj.MinDelCharges)?Convert.ToDecimal(obj.MinDelCharges):0),
                    new SqlParameter("@MRPTo",!string.IsNullOrEmpty(obj.DeliveryTo)?Convert.ToDecimal(obj.DeliveryTo):0),
                    new SqlParameter("@EntryBy","Admin"),
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        internal DataTable InsertUpdateDeliveryChargeswithPincode(PropertyClass obj, DataTable dtpincode)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@id",obj.strId),
                    new SqlParameter("@type_pincodeList",dtpincode)
            };
            dt = objdb.ExecProcDataTable("proc_deliverycharge_withpincode", parm);
            return dt;
        }
        #region PointsOffer
        internal DataTable InsertUpdatePointsOffer(PropertyClass obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@id",obj.Id),
                    new SqlParameter("@ItemCode",obj.ItemCode),
                    new SqlParameter("@Points",obj.Points),
                    new SqlParameter("@FromDate",obj.startDate),
                    new SqlParameter("@ToDate",obj.EndDate),
                    new SqlParameter("@CreateBy",obj.EntryBy),

            };
            dt = objdb.ExecProcDataTable("proc_saveupdatepointsOffer", parm);
            return dt;
        }

        #endregion




        #endregion


        #region Maya Maurya

        internal DataTable UpdateDataFromProductSale(string BarCode, string VarId, decimal MRP, int Action, string ItemCode)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@Barcode",BarCode),
                new SqlParameter("@VarId",VarId),
                new SqlParameter("@MRP",MRP),
                new SqlParameter("@ItemCode",ItemCode)
            };
            dt = objdb.ExecProcDataTable("Proc_UpdateDataFromProductSale", param);
            return dt;
        }
        #endregion






        #region Mohd Nadeem 06-08-2022

        internal DataTable BindItemheadDropDownOffercreatenewoffer(string userId)
        {
            DataTable dt = new DataTable();
            string Query = "";
            if (string.IsNullOrEmpty(userId))
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,  case when a.ProductType = 'Simple Product' THEN(a.ItemName + ' ' + isnull(a.ItemBarCode, '')) ELSE(a.ItemName + ' ' + isnull(c.VarriationName, '') + ' ' + isnull(b.BarCode, 0)) END as ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.ItemCode ELSE b.VariationId END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1";
            }
            else
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,  case when a.ProductType = 'Simple Product' THEN(a.ItemName + ' ' + isnull(a.ItemBarCode, '')) ELSE(a.ItemName + ' ' + isnull(c.VarriationName, '') + ' ' + isnull(b.BarCode, 0)) END as ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.ItemCode ELSE b.VariationId END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1  and a.EntryBy= '" + userId + "'";
            }
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }



        internal DataTable BindItemheadDropDown1creatoffer(string userId)
        {
            DataTable dt = new DataTable();
            string Query = "";
            if (string.IsNullOrEmpty(userId))
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,  case when a.ProductType = 'Simple Product' THEN(a.ItemName + ' ' + isnull(a.ItemBarCode, '')) ELSE(a.ItemName + ' ' + isnull(c.VarriationName, '') + ' ' + isnull(b.BarCode, 0)) END as ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.ItemCode ELSE convert(varchar(50),isnull(b.SrNo,0)) END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1";
            }
            else
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,  case when a.ProductType = 'Simple Product' THEN(a.ItemName + ' ' + isnull(a.ItemBarCode, '')) ELSE(a.ItemName + ' ' + isnull(c.VarriationName, '') + ' ' + isnull(b.BarCode, 0)) END as ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.ItemCode ELSE convert(varchar(50),isnull(b.SrNo,0)) END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1 and a.EntryBy= '" + userId + "'";
            }
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }


        internal DataTable BindItemheadDropDown1pointoffer(string userId)
        {
            DataTable dt = new DataTable();
            string Query = "";
            if (string.IsNullOrEmpty(userId))
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,  case when a.ProductType = 'Simple Product' THEN(a.ItemName + ' ' + isnull(a.ItemBarCode, '')) ELSE(a.ItemName + ' ' + isnull(c.VarriationName, '') + ' ' + isnull(b.BarCode, 0)) END as ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.ItemCode ELSE convert(varchar(50),isnull(b.SrNo,0)) END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1";
            }
            else
            {
                Query = "SELECT '' as itmId,'Select Item' AS ItemName,'' AS ItemCode UNION ALL SELECT a.ItemCode AS itmId,  case when a.ProductType = 'Simple Product' THEN(a.ItemName + ' ' + isnull(a.ItemBarCode, '')) ELSE(a.ItemName + ' ' + isnull(c.VarriationName, '') + ' ' + isnull(b.BarCode, 0)) END as ItemName,CASE WHEN a.ProductType = 'Simple Product' THEN a.ItemCode ELSE convert(varchar(50),isnull(b.SrNo,0)) END AS ItemCode FROM tbl_ItemHeadMaster a LEFT JOIN tbl_ProductVariations b ON a.ItemCode = b.ProductId LEFT JOIN tbl_Variations c ON b.VariationId = c.SrNo WHERE a.IsActive = 1 and a.EntryBy= '" + userId + "'";
            }
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }




        internal DataTable InsertUpdateRedeem(PropertyClass obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@RedeemPoint",obj.Points),
                    new SqlParameter("@NoOfTimesRedeem",obj.NoOfTimesRedeem),

                    new SqlParameter("@EntryBy",obj.EntryBy),

            };
            dt = objdb.ExecProcDataTable("proc_redeemmaster", parm);
            return dt;
        }


        internal DataTable DeleteReportRedeem(PropertyClass obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action",obj.Action),
                    new SqlParameter("@Id",obj.RespoCode),

                    new SqlParameter("@EntryBy",obj.EntryBy),

            };
            dt = objdb.ExecProcDataTable("proc_redeemmaster", parm);
            return dt;
        }



        #endregion


        #region Added by Anchal 

        public DataTable mst_SaveUpdateComboOffer(int Action, string ItemCode, string FreeItemCode, int FreeQuantity, int ItemQuantity, string EntryBy, int Id)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@ItemCode",ItemCode),
                new SqlParameter("@FreeItemCode",FreeItemCode),
                new SqlParameter("@Quantity",FreeQuantity),
                new SqlParameter("@ItemQuantity",ItemQuantity),
                new SqlParameter("@EntryBy",EntryBy),
                new SqlParameter("@Id",Id),

            };
            dtt = objdb.ExecProcDataTable("mst_SaveUpdateComboOffer", param);
            return dtt;
        }

        public DataSet getProductVariationPrice(PropertyClass Objp, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@VariationId",Objp.StockistName),
                new SqlParameter("@Discountrate",Objp.StockistId)
            };
            dtt = objdb.ExecProcDataSet(ProcName, param);
            return dtt;
        }
        public DataTable Proc_GetProductDetail_Updated(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@productId",Objp.ItemCode),
                new SqlParameter("@VariationId",Objp.VariationId),
                new SqlParameter("@CatId",Objp.MainCategoryCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable Proc_productcolorimagelist(string ProductId, string variationname, string proc_name)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","101"),
                new SqlParameter("@ProductId",ProductId),
                new SqlParameter("@sizetype",variationname),

            };
            dtt = objdb.ExecProcDataTable(proc_name, param);
            return dtt;
        }

        public DataTable Proc_AddToWhishlist(string ProductId, string EntryBy, string proc_name)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","721"),
                new SqlParameter("@ProductId",ProductId),
                new SqlParameter("@VariationId",EntryBy),

            };
            dtt = objdb.ExecProcDataTable(proc_name, param);
            return dtt;
        }
        public DataTable Proc_productcolorlist(string ProductId, string variationname, string proc_name)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","100"),
                new SqlParameter("@ProductId",ProductId),
                new SqlParameter("@sizetype",variationname),

            };
            dtt = objdb.ExecProcDataTable(proc_name, param);
            return dtt;
        }
        internal DataTable insertSalesPerson(SalesPerson obj)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@Action",obj.Action),
                new SqlParameter("@Name",obj.Name),
                new SqlParameter("@ContactNo",obj.ContactNo),
                new SqlParameter("@Address",obj.Address),
                new SqlParameter("@AadharNo",obj.AadharNo),
                new SqlParameter("@EmailId",obj.Emailid),
                new SqlParameter("@UserName",obj.userName),
                new SqlParameter("@Password",obj.Password),
                    new SqlParameter("@Pincode",obj.Pincode),
                new SqlParameter("@RefCode",obj.SalesPersonId),
            };
            dt = objdb.ExecProcDataTable("proc_InsertSalePersons", sp);
            return dt;
        }

        internal DataTable BindSalesPersonDLLByFranchise(string fCode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS SpCode,'Select Delivery Boy' AS name UNION ALL SELECT SpCode,Name+'-'+SpCode AS name FROM tbl_SalesPersonMaster WHERE IsActive = 1 AND SpCode = '" + fCode + "'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable BindSalesPersonDLL()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS SpCode,'Select Delivery Boy' AS name UNION ALL SELECT SpCode,Name+'-'+SpCode AS name FROM tbl_SalesPersonMaster WHERE IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }

        public DataTable KeywordSearch(string Prefix)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@Action","1"),
                     new SqlParameter("@Searching",Prefix)
                };
            dt = objdb.ExecProcDataTable("GetProductSearchingApi", param);
            return dt;
        }

        public DataTable Proc_GetProductDetail_Varaiation(string ItemCode, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","1"),
                new SqlParameter("@productId",ItemCode),
            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable Proc_GetOrderReport(clsearchOrder sod)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","1"),
                new SqlParameter("@FromDate",sod._FromDate),
                new SqlParameter("@ToDate",sod._ToDate),
                new SqlParameter("@OrderId",sod._OrderId),
            };
            dtt = objdb.ExecProcDataTable("Proc_GetOrderReport", param);
            return dtt;
        }
        public DataTable Proc_GetFilterOrderReport(clsearchOrder sod)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",sod._Action),
                new SqlParameter("@ddlOrder",sod.ddlOrder),
               new SqlParameter("@ddlcnclrturn",sod.ddlcnclrturn),
                    new SqlParameter("@customerId",sod.customerId),
                        new SqlParameter("@Vendorid",sod.VendorList)

            };
            dtt = objdb.ExecProcDataTable("Proc_GetFilterOrderReport", param);
            return dtt;
        }

        #endregion
        #region vandana pandey
        internal DataTable SelectCities(string StateId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@StateId",StateId)
            };
            dt = objdb.ExecProcDataTable("GetCity", sp);
            return dt;
        }
        internal DataTable BindStateDll()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' AS StateId,'Select State' AS StateName UNION ALL SELECT State_Id as StateId,State_name as StateName  FROM State_Master";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable CheckExistingUser(string Mobile, string Email)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT * from tbl_Login  WHERE  EmailAddress = '" + Email + "' and Role='10'";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        internal DataTable VendorSignup(VMVendorSignup al)
        {
            DataTable dt1 = new DataTable();
            try
            {
                if (al.Action == "4")
                {
                    int pass = new Random().Next(10000, 99999);
                    al.Password = pass.ToString();

                }


                SqlParameter[] sp = new SqlParameter[] {
                     new SqlParameter("@Action",SqlDbType.VarChar),
                    new SqlParameter("@name",SqlDbType.VarChar),
                     new SqlParameter("@emailid",SqlDbType.VarChar),
                         new SqlParameter("@contactno",SqlDbType.VarChar),
                    new SqlParameter("@fulladdress",SqlDbType.VarChar),
                     new SqlParameter("@password",SqlDbType.VarChar),
                         new SqlParameter("@cityname",SqlDbType.VarChar),
                             new SqlParameter("@userType",SqlDbType.VarChar),
                                 new SqlParameter("@Oldpassword",SqlDbType.VarChar),
                              new SqlParameter("@id",SqlDbType.VarChar),
                              new SqlParameter("@gstno",SqlDbType.VarChar),
                              new SqlParameter("@state",SqlDbType.VarChar),


                };
                sp[0].Value = al.Action;
                sp[1].Value = al.Name;
                sp[2].Value = al.EmailId;
                sp[3].Value = al.ContactNo;
                sp[4].Value = al.Address;
                sp[5].Value = al.Password;
                sp[6].Value = al.vendorCity;
                sp[7].Value = "Seller";
                sp[8].Value = al.OTP;
                sp[9].Value = al.VenId;
                sp[10].Value = al.GSTNo;
                sp[11].Value = al.VenState;
                dt1 = objdb.ExecProcDataTable("GetVendor", sp);

            }
            catch (Exception ex)
            {
                dt1 = null;
            }
            return dt1;

        }
        internal DataTable SelectLoginCount(string mcode)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Isnull(LoginCount,0)LoginCount FROM tbl_Login where IsActive=1 and UserName='" + mcode + "' ";
            try
            {
                dt = objdb.ExecAdaptorDataTable(Query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LoginCount"].ToString() == "0")
                    {
                        string Query1 = "Update tbl_Login set LoginCount=1 where UserName='" + mcode + "' ";
                        DataTable dt1 = objdb.ExecAdaptorDataTable(Query1);
                    }
                }
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        internal DataTable SelectVendorDetails(string Member_id)
        {
            DataTable dt = new DataTable();
            string Query = " SELECT* FROM tbl_Login master join tbl_VendorRegistration VDetails ON master.EmailAddress = VDetails.EmailId WHERE (master.EmailAddress = '" + Member_id + "' or VDetails.ContactNo='" + Member_id + "')  and Master.IsActive = 1";

            try
            {
                dt = objdb.ExecAdaptorDataTable(Query);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        internal DataTable VendorBlockandUnBlock(string VendorId, string Action)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@VendorId",VendorId),
              new SqlParameter("@Action",Action)
            };
            dt = objdb.ExecProcDataTable("[BlockUnBlockVendor]", parm);
            return dt;
        }
        internal DataTable SaveCourierStatus(string Phone)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@phone",Phone),
              new SqlParameter("@Action","1")
            };
            dt = objdb.ExecProcDataTable("[saveCourierStatus]", parm);
            return dt;
        }
        internal DataTable SelectUserDetails(string Member_id)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT *  FROM tbl_Login master LEFT join MemberDetails MDetails ON master.UserName=MDetails.Member_Id WHERE master.EmailAddress = '" + Member_id + "' or MDetails.mobile='" + Member_id + "' or master.UserName='" + Member_id + "' ";
            try
            {
                dt = objdb.ExecAdaptorDataTable(Query);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        //internal DataTable GetVendorPickupLocation()
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] parm = new SqlParameter[] {

        //    };
        //    dt = objdb.ExecProcDataTable("[VendorPickupDetail]", parm);
        //    return dt;
        //}

        public int ApprovedVendor(string emailid, string status)
        {
            int r = 0;
            try
            {
                string sql = "ApprovedVendor";
                SqlParameter[] sqlParams = {
                new SqlParameter("@Emailid",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),


            };
                sqlParams[0].Value = emailid;
                sqlParams[1].Value = status;



                r = objdb.ExecuteNonQueryProc(sql, sqlParams);
                return r;
            }
            catch (Exception ex)
            {
                return r;
            }
        }
        internal DataTable BindVendorDropDown()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT '' VendorId,'Select Vendor' Name UNION ALL SELECT VendorId,Name FROM tbl_VendorRegistration WHERE ApproveStatus='Approved'  AND IsActive=1";
            dt = objdb.ExecAdaptorDataTable(Query);
            return dt;
        }
        #endregion

        public DataTable DeleteVendorDetails(tbl_VendorRegistration Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@VendorId",Objp.VendorId)

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        internal DataSet GetLevelDashBoard(PropertyClass objp, string ProcName)
        {
            DataSet d = new DataSet();
            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Action",objp.Action),
             new SqlParameter("@CustomerId",objp.CustomerId),
            };
            d = objdb.ExecProcDataSet(ProcName, parm);
            return d;
        }


        public DataTable getBonus(string customerid)
        {
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@customerID",customerid)
            };
            return objdb.ExecProcDataTable("GetBonus", p);
        }

        internal DataTable RequestMoneyForWithDraw(string AccountHolder, string Account, String IFSC, decimal amt)
        {
            SqlParameter[] p = new SqlParameter[]
            {
               new SqlParameter("@AccountHolderName",AccountHolder),
               new SqlParameter("@AccountNumber",Account),
               new SqlParameter("@IFSCCode",IFSC),
               new SqlParameter("@AMT",amt),
               new SqlParameter("@Action",1)
            };
            return objdb.ExecProcDataTable("Proc_RequestForAMT", p);
        }

        #region Anoop Kumar
        public DataTable RemoveWishlistProduct(PropertyClass objp, string paroc, string ProductId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@Action",objp.Action),
                new SqlParameter("@ProductId",ProductId),
                new SqlParameter("@CustomerId",objp.CustomerId)
            };
            dt = objdb.ExecProcDataTable(paroc, sp);
            return dt;
        }
        #endregion
        public bool ChangePassword(string phone, string otp, string pass)
        {
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@UserId",phone),
                new SqlParameter("@NewPassword",pass),
            };
            int i = objdb.ExecuteNonQueryProc("ChangePassword", p);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable DeleteDeleveryAddress(string CustomerId, string mobileno, string pincode)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@Action",4),
                new SqlParameter("@customercode",CustomerId),
                new SqlParameter("@mobileno",mobileno),
                new SqlParameter("@pincode",pincode)
            };
            return objdb.ExecProcDataTable("[proc_InsertDeliveryAddress]", sp);
        }
        public DataTable UpdateDeliveryAddress(string customercode, string name, string number, string pincode, string cityid, string address, string stateid, string landmark)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@Action",'3'),
                new SqlParameter("@customercode",customercode),
                new SqlParameter("@name",name),
                new SqlParameter("@mobileno",number),
                new SqlParameter("@pincode",pincode),
                new SqlParameter("@cityname",cityid),
                new SqlParameter("@address",address),
                new SqlParameter("@stateid",stateid),
                new SqlParameter("@landmark",landmark),
            };
            return objdb.ExecProcDataTable("[proc_InsertDeliveryAddress]", sp);
        }


        // 



        public void InsertRefundData(string refundReason, string refundComments, string accountNumber, string ifscCode, HttpPostedFileBase replaceImageRefund,string orderid)
        {
            try
            {
                // Validate input data if necessary

                // Process uploaded image
                string imagePath = null;
                if (replaceImageRefund != null && replaceImageRefund.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(replaceImageRefund.FileName);
                    var path = Path.Combine("~/ProductReturnIMG", fileName); // Adjust the path as per your requirement
                    imagePath = path;

                    // Save the uploaded image
                    replaceImageRefund.SaveAs(HttpContext.Current.Server.MapPath(path));
                }


                SqlParameter[] p = new SqlParameter[]
                {
                new SqlParameter("@OrderId",orderid),
                new SqlParameter("@AccountNumber",accountNumber),
                new SqlParameter("@IfscCode",ifscCode),
                new SqlParameter("@CancelReason",refundReason),
                new SqlParameter("@Image",replaceImageRefund.FileName),
                new SqlParameter("@Action",'5')

                };

                // Now you can insert the data into your business layer
                objdb.ExecuteNonQueryProc("proc_CancelOrder", p);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Added by Tanu Gupta
        public DataTable VendorContactUs(ClsVendorContactUs VCon, string ProcName)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@Action", VCon.Action),
            new SqlParameter("@Name", VCon.Name),
            new SqlParameter("@Mobile", VCon.mobileNo),
            new SqlParameter("@Email", VCon.Email),
            new SqlParameter("@Message", VCon.Message),
                };
                return objdb.ExecProcDataTable(ProcName, param);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }


        //end


    }
}