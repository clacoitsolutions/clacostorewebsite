using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using OjasMart.Models;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Runtime.Remoting;
using System.Configuration;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;

namespace OjasMart.Controllers
{
    public class EcommarceController : Controller
    {
        // GET: Ecommarce
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        public ActionResult EcommarceCustomerDashboard()
        {
            try
            {
                objp.Action = "1";
                objp.dt = objL.BindEcommarceDashboard(objp, "Proc_BindEcommarceDashboard");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult ItemEditforEccomarce()
        {
            ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
            ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());

            if (Request.QueryString["ProductId"] != null)
            {
                objp.Action = "2";
                objp.ItemCode = Convert.ToString(Request.QueryString["ProductId"]);
                DataTable dt = objL.GetItemHeadDetails(objp, "Proc_GetItemHeadDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.ItemCode = dt.Rows[0]["ItemCode"].ToString();
                    objp.ItemName = dt.Rows[0]["ItemName"].ToString();
                    objp.HSNCode = dt.Rows[0]["HSNCode"].ToString();
                    objp.MRP = dt.Rows[0]["MRP"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["MRP"].ToString()) : 0;
                    objp.OnlinePrice = dt.Rows[0]["OnlinePrice"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["OnlinePrice"].ToString()) : 0;
                    objp.GroupCode = dt.Rows[0]["ItemGroup"].ToString();

                }
            }
            return View(objp);
        }
        public JsonResult GetOnlineItemDetails(string ItemCode1)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.ItemCode = ItemCode1;
                DataTable dt = objL.GetOnlineProductDetails(objp, "Proc_GetOnlineItemDetail");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.StorePrice = dt.Rows[0]["StorePrice"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["StorePrice"].ToString()) : 0;
                    objp.OnlinePrice = dt.Rows[0]["OnlinePrice"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["OnlinePrice"].ToString()) : 0;
                    objp.ItemCode = dt.Rows[0]["ItemCode"].ToString();
                    objp.GroupCode = dt.Rows[0]["ItemGroup"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        // [ValidateInput(false)]
        public JsonResult InsertProductDetails(PropertyClass p)
        {
            try
            {
                //int c = p.multipleImages.ContentLength;
                HttpFileCollectionBase files = Request.Files;
                int cnt = files.Count;
                DataTable dt = new DataTable();
                dt.Columns.Add("ImageUrl");

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    string fileExt = Path.GetExtension(file.FileName);
                    var fileName = Path.GetFileName(file.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;

                    var path = Path.Combine(Server.MapPath("../productImages"), fName);
                    string fiName = "../productImages/" + fName;
                    file.SaveAs(path);
                    //productImages
                    if (i == 0)
                    {
                        p.Url = fiName;
                    }
                    dt.Rows.Add(fiName);
                }

                p.Action = "1";
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.SSCode = Convert.ToString(Session["UserName"]);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);
                DataTable dt1 = objL.InsertOnlineProductDetails(p, "proc_InsertEcommarceProduct", dt);
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
        [HttpPost]
        public ActionResult SaveDropzoneJsUploadedFiles()
        {
            bool isSavedSuccessfully = false;

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];

                //You can Save the file content here

                isSavedSuccessfully = true;
            }

            return Json(new { Message = string.Empty });

        }
        public ActionResult ShowProductDetail()
        {
            try
            {
                if (Request.QueryString["PCode"] != null)
                {
                    objp.Action = "2";
                    objp.ItemCode = Convert.ToString(Request.QueryString["PCode"]);
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    objp.dt = objL.GetEcoomarceProductDescription(objp, "Proc_BindEcommarceDashboard");
                    objp.Action = "3";
                    objp.dt1 = objL.GetEcoomarceProductDescription(objp, "Proc_BindEcommarceDashboard");

                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertToCartList(PropertyClass p)
        {
            try
            {
                //int c = p.multipleImages.ContentLength;            

                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.CustomerId = Convert.ToString(Session["UserName"]);
                DataTable dt1 = objL.InsertCartList(p, "ProcInsertCartList");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.strId = dt1.Rows[0]["id"].ToString();
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
        public PartialViewResult _pBindCartList()
        {
            if (Session["Role"].ToString() == "4")
            {
                objp.Action = "2";
                objp.CustomerId = Convert.ToString(Session["UserName"].ToString());
                objp.dt = objL.InsertCartList(objp, "ProcInsertCartList");
                decimal totAmt = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        totAmt += dr["TotalPrice"].ToString() != "" ? Convert.ToDecimal(dr["TotalPrice"].ToString()) : 0;
                    }
                }
                objp.NetTotal = totAmt;

                objp.Action = "3";
                DataTable dt = objL.InsertCartList(objp, "ProcInsertCartList");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.TotStokist = dt.Rows[0]["tot"].ToString();
                }
            }
            return PartialView(objp);
        }
        public ActionResult ItemCheckout()
        {
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            string CustomerId = Convert.ToString(Session["UserName"]);
            objp.CustomerId = Convert.ToString(Session["UserName"].ToString());
            objp.dt = objL.GetCustomerAddressDetail(CustomerId);
            if (Convert.ToString(Request.QueryString["type"]) == "C")
            {
                objp.Action = "2";
                objp.dt1 = objL.InsertCartList(objp, "ProcInsertCartList");
                decimal totAmt = 0;
                if (objp.dt1 != null && objp.dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt1.Rows)
                    {
                        totAmt += dr["TotalPrice"].ToString() != "" ? Convert.ToDecimal(dr["TotalPrice"].ToString()) : 0;
                    }
                }
                objp.NetTotal = totAmt;
            }
            return View(objp);
        }
        public JsonResult InsertDeliveryAddress(PropertyClass p)
        {
            try
            {
                //int c = p.multipleImages.ContentLength;            
                p.Action = "1";
                //p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.CustomerId = Convert.ToString(Session["mCode"]);
                DataTable dt1 = objL.InsertDeliveryAddress(p, "proc_InsertDeliveryAddress");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.strId = dt1.Rows[0]["id"].ToString();
                    //objp.dt = objL.GetCustomerAddressDetail(p.CustomerId);
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

        //Delete Delevery Address
        public JsonResult DeleteDeleveryAddress(string data, string data1, string data2)
        {

            DataTable dt = objL.DeleteDeleveryAddress(data, data1, data2);
            if (dt != null && dt.Rows.Count > 0)
            {
                // If the deletion was successful
                return Json(new { success = true, message = "Delevery address deleted successfully." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // If there was an error or the deletion failed
                return Json(new { success = false, message = "Error occurred while deleting the delevery address." }, JsonRequestBehavior.AllowGet);
            }
        }
        //Update Delevery Address
        [HttpPost]
        public JsonResult UpdateDeliveryAddress(string customercode, string name, string number, string pincode, string cityid, string address, string stateid, string landmark)
        {

            DataTable dt = objL.UpdateDeliveryAddress(customercode, name, number, pincode, cityid, address, stateid, landmark);
            if (dt != null && dt.Rows.Count > 0)
            {
                // If the deletion was successful
                return Json(new { success = true, message = "Delevery address deleted successfully." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // If there was an error or the deletion failed
                return Json(new { success = false, message = "Error occurred while deleting the delevery address." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ShowCartDetails()
        {
            try
            {
                objp.Action = "2";
                objp.CustomerId = Convert.ToString(Session["UserName"].ToString());
                objp.dt = objL.InsertCartList(objp, "ProcInsertCartList");
                decimal totAmt = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        totAmt += dr["TotalPrice"].ToString() != "" ? Convert.ToDecimal(dr["TotalPrice"].ToString()) : 0;
                    }
                }
                objp.NetTotal = totAmt;
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertOnlineOrder(decimal GrossPayable, decimal deliverycharges, string iscoupenapplied, decimal CoupenAmount, decimal DiscAmt, string DeliveryTo, string PayMode, string Status, decimal NetTotal, List<ItemDetails> ItemList, List<FreeItemList> FreeItemList, decimal gstAmount)
        {
            try
            {
                objp.Action = "1";
                objp.CustomerId = Convert.ToString(Session["mCode"]);
                // objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.CompanyCode = "OZ00001";

                objp.GrossPayable = GrossPayable;
                objp.deliverycharges = deliverycharges;
                objp.iscoupenapplied = iscoupenapplied;
                objp.CoupenAmount = CoupenAmount;
                objp.DiscAmt = DiscAmt;
                objp.DeliveryTo = DeliveryTo;
                objp.PayMode = PayMode;
                objp.Status = Status;
                objp.NetTotal = NetTotal;
                objp.gstamount = gstAmount;
                if (Session["StockistId"] != null)
                {
                    objp.StockistId = Session["StockistId"].ToString();
                }
                else
                {
                    objp.StockistId = null;
                }


                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UnitRate");
                dt.Columns.Add("TotalAmount");
                dt.Columns.Add("VariationId");
                dt.Columns.Add("Aff_CustomerId");
                dt.Columns.Add("GstAmount");
                dt.Columns.Add("GSTRate");
                dt.Columns.Add("TotalGst");
                dt.Columns.Add("regrate");
                dt.Columns.Add("discount_amt");
                dt.Columns.Add("color");
                dt.Columns.Add("Size");

                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        dt.Rows.Add(item.ItemCode, item.Quantity, item.Rate, item.TotalAmount, item.Reason, "", item.GstAmount, item.GSTRate, item.TotalGst, item.regrate, item.discount_amt, item.color, item.Size);
                    }
                }

                DataTable dtfreeitem = new DataTable();
                dtfreeitem.Columns.Add("ItemCode");
                dtfreeitem.Columns.Add("qty");
                dtfreeitem.Columns.Add("varIdL");
                if (FreeItemList != null)
                {
                    foreach (var freeitem in FreeItemList)
                    {
                        dtfreeitem.Rows.Add(freeitem.ItemCode, freeitem.qty, freeitem.varIdL);
                    }
                }



                DataTable dt1 = objL.InsertOnlineOrderDetails(objp, "Proc_InsertOnlineOrder", dt, dtfreeitem);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.strId = dt1.Rows[0]["id"].ToString();
                    objp.OrderId = dt1.Rows[0]["OrderId"].ToString();
                    objp.EmailAddress = dt1.Rows[0]["EmailAddress"].ToString();
                    objp.MobileNo = dt1.Rows[0]["MobileNo"].ToString();
                    objp.Amount = Convert.ToDecimal(dt1.Rows[0]["NetPayable"]);
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

        public ActionResult PrintInvoice()
        {
            try
            {

                if (Request.QueryString["OrderId"] != null)
                {
                    objp.Action = "1";
                    objp.OrderId = Convert.ToString(Request.QueryString["OrderId"]);
                    objp.dt = objL.GetOnlineInvoiceDetails(objp, "proc_PrintonlineInvoice");
                    objp.Action = "4";
                    objp.dtcombooffer = objL.GetOnlineInvoiceDetails(objp, "proc_PrintonlineInvoice");
                }
                else
                {
                    return RedirectToAction("EcommarceCustomerDashboard", "Ecommarce");
                }

            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult OnlineOrderReport(string StartDate, string EndDate)
        {
            try
            {
                objp.Action = "2";

                if (Convert.ToString(Session["Role"]) == "9")
                {
                    objp.deliveryboy = Convert.ToString(Session["CompanyCode"]);
                    ViewBag.DeliverBoy = BindDDL(objL.BindSalesPersonDLLByFranchise(Convert.ToString(Session["CompanyCode"])));
                }
                else
                {
                    objp.deliveryboy = null;
                    ViewBag.DeliverBoy = BindDDL(objL.BindSalesPersonDLL());
                }


                if (Convert.ToString(Session["Role"]) == "4")
                {
                    objp.CustomerId = Convert.ToString(Session["UserName"]).ToString();
                }
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.StockistId = Convert.ToString(Session["UserName"]).ToString();
                }
                objp.mDate = StartDate;
                objp.eDate = EndDate;
                objp.dt = objL.GetOnlineInvoiceDetails(objp, "proc_PrintonlineInvoice");
                decimal totAmt = 0;
                if (objp.dt != null && objp.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in objp.dt.Rows)
                    {
                        totAmt += dr["NetPayable"].ToString() != "" ? Convert.ToDecimal(dr["NetPayable"].ToString()) : 0;
                    }
                    objp.NetTotal = totAmt;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult OrderItemDetails(string OrderId)
        {
            PropertyClass model = new PropertyClass();
            DataTable dt = new DataTable();
            objp.Action = "3";
            objp.OrderId = OrderId;
            dt = objL.GetOnlineInvoiceDetails(objp, "proc_PrintonlineInvoice");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyClass m = new PropertyClass();
                    m.OrderId = dr["OrderId"].ToString();
                    m.ItemName = dr["ItemName"].ToString();
                    m.Color = dr["Color"].ToString();
                    m.Size = dr["Size"].ToString();
                    m.Quantity = dr["Quantity"].ToString() != "" ? Convert.ToDecimal(dr["Quantity"].ToString()) : 0;
                    m.Rate = dr["OnlinePrice"].ToString() != "" ? Convert.ToDecimal(dr["OnlinePrice"].ToString()) : 0;
                    m.NetTotal = dr["TotalAmount"].ToString() != "" ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0;
                    m.gstamount = dr["GstAmount"].ToString() != "" ? Convert.ToDecimal(dr["GstAmount"].ToString()) : 0;
                    m.VariationId = dr["VarriationName"].ToString();
                    model.poList.Add(m);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCategory()
        {
            try
            {
                ViewBag.CategoryList = PropertyClass.BindDDL(objL.BindProductCategoryDropDown());

                objp.Action = "2";
                objp.dt = objL.InsertUpdateCategory(objp, "proc_InsertUpdateCategory");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult GetDetailsForCategoryEdit(string SrNo)
        {
            try
            {
                objp.Action = "3";
                objp.strId = SrNo.Trim();
                DataTable dt = objL.InsertUpdateCategory(objp, "proc_InsertUpdateCategory");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["SrNo"].ToString();
                    objp.ItemName = dt.Rows[0]["ProductCategory"].ToString();
                    objp.GroupCode = dt.Rows[0]["ParrentCategoryId"].ToString();
                    objp.PurchaseFile = dt.Rows[0]["ImageFile"].ToString();
                    objp.BatchNo = dt.Rows[0]["CategoryImage"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertCategory(PropertyClass obj, string CatImage)
        {

            try
            {
                //---- Upload Main Pic
                if (CatImage != null)
                {

                    obj.PurchaseFile = CatImage;
                }
                else if (!string.IsNullOrEmpty(obj.ChqNo))
                {
                    obj.PurchaseFile = obj.ChqNo;
                }
                obj.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt2 = objL.InsertUpdateCategory(obj, "proc_InsertUpdateCategory");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    obj.msg = dt2.Rows[0]["msg"].ToString();
                    obj.strId = dt2.Rows[0]["id"].ToString();
                }
                else
                {
                    obj.msg = "Server not Responding";
                    obj.strId = "0";
                }

            }
            catch (Exception ex)
            {
                obj.msg = ex.Message;
                obj.strId = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewProduct()
        {
            try
            {

                objp.RespoCode = UniqueId();
                ViewBag.MainCategoryList = PropertyClass.BindDDL(objL.BindProductMainCategoryDropDown());

                ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
                ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
                ViewBag.AttributeList = PropertyClass.BindDDL(objL.BindAttributeDropDown());
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult insertProductVariation(string RequestId, string AttrId, List<AppliedOfferList> ItemList)
        {
            PropertyClass obj = new PropertyClass();
            try
            {
                DataTable dtVar = new DataTable();
                dtVar.Columns.Add("VariationId");
                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        dtVar.Rows.Add(item.OfferCode);
                    }
                }
                obj.Action = "1";
                obj.RespoCode = RequestId;
                obj.AttributeId = AttrId;
                DataTable dt2 = objL.InsertProductVariations(obj, "proc_InsertTempAttributes", dtVar);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    obj.strId = dt2.Rows[0]["id"].ToString();
                    if (obj.strId == "1")
                    {
                        obj.Action = "2";
                        DataTable dt = objL.InsertProductVariations(obj, "proc_InsertTempAttributes", dtVar);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                PropertyClass m = new PropertyClass();
                                m.VariationId = dr["Varriations"].ToString();
                                m.ItemName = dr["AttributeName"].ToString();
                                m.AccountCode = dr["AttributeId"].ToString();
                                obj.poList.Add(m);
                            }
                        }

                    }
                }
                else
                {
                    obj.msg = "Server not Responding";
                    obj.strId = "0";
                }
            }
            catch (Exception ex)
            {
                obj.msg = ex.Message;
                obj.strId = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAttributeData(string SrNo)
        {
            PropertyClass model = new PropertyClass();
            DataTable dt = new DataTable();

            dt = objL.getAttrData(SrNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyClass m = new PropertyClass();
                    m.ItemCode = dr["SrNo"].ToString();
                    m.ItemName = dr["VarriationName"].ToString();
                    m.AccountCode = dr["AttributeId"].ToString();
                    model.poList.Add(m);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }



        public JsonResult getItemsBySubCatId(string CatId)
        {

            PropertyClass st = new PropertyClass();
            try
            {
                DataTable dt = objL.BindSubCategoryDll(CatId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        st.SubCategoryitem.Add(new SelectListItem { Text = Convert.ToString(dr["ProductCategory"]), Value = Convert.ToString(dr["SrNo"]) });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new SelectList(st.SubCategoryitem, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
        // [ValidateInput(false)]
        public JsonResult InsertProductDetailsNew(PropertyClass p)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ImageUrl");

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var fileName = Path.GetFileName(mainPic.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string ImagePathThumb = String.Format("/productImagesThumble/{0}{1}", fname, fileExt);
                    string ImagePath1 = String.Format("/productImages/{0}{1}", fname, fileExt);
                    using (var img = System.Drawing.Image.FromStream(mainPic.InputStream))
                    {
                        SaveToFolder(img, fileName, fileExt, new Size(300, 300), ImagePathThumb);
                        SaveToFolder(img, fileName, fileExt, new Size(600, 600), ImagePath1);
                    }
                    string myfile = fname + fileExt;
                    p.Url = myfile;
                }
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    string fileExt = Path.GetExtension(file.FileName);
                    var fileName = Path.GetFileName(file.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string ImagePathThumb = String.Format("/productImagesThumble/{0}{1}", fname, fileExt);
                    string ImagePath1 = String.Format("/productImages/{0}{1}", fname, fileExt);
                    using (var img = System.Drawing.Image.FromStream(file.InputStream))
                    {
                        SaveToFolder(img, fileName, fileExt, new Size(300, 300), ImagePathThumb);
                        SaveToFolder(img, fileName, fileExt, new Size(600, 600), ImagePath1);
                    }
                    string myfile = fname + fileExt;
                    dt.Rows.Add(myfile);
                }
                p.Action = "1";
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.SSCode = Convert.ToString(Session["UserName"]);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);

                JavaScriptSerializer js = new JavaScriptSerializer();
                List<VariationList> result = js.Deserialize<List<VariationList>>(p.VariationList);

                DataTable dtvarList = new DataTable();
                dtvarList.Columns.Add("StoreId");
                dtvarList.Columns.Add("AttributeId");
                dtvarList.Columns.Add("VariationId");
                dtvarList.Columns.Add("RegularPrice");
                dtvarList.Columns.Add("SalePrice");
                if (result != null && result.Count > 0)
                {
                    foreach (var itm in result)
                    {
                        dtvarList.Rows.Add(itm.StoreId, itm.AttributeId, itm.variationId, itm.RegularPrice, itm.SalePrice);
                    }
                }
                DataTable dt1 = new DataTable();
                if (p.SizeCode != null)
                {
                    dt1 = objL.InsertOnlineProductDetailsNewUpdated(p, "proc_InsertEcommarceProductNew", dt, dtvarList);
                }
                else
                {
                    dt1 = objL.InsertOnlineProductDetailsNewUpdated(p, "proc_InsertEcommarceProductNewcolo", dt, dtvarList);
                }
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
        public ActionResult AllEcommarceProducts(string StartDate, string EndDate,string Searchkey)
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == null)
                {
                    return RedirectToAction("Index", "Account");
                }

                objp.mDate = !string.IsNullOrEmpty(StartDate) ? Convert.ToString(StartDate) : null;
                objp.eDate = !string.IsNullOrEmpty(EndDate) ? Convert.ToString(EndDate) : null;
                objp._Searchkey = !string.IsNullOrEmpty(Searchkey) ? Convert.ToString(Searchkey) : null;

                if (Convert.ToString(Session["Role"]) == "1")
                {
                    objp.Action = "1";
                    objp.dt = objL.GetOnlineProducts(objp, "proc_AllEcomProducts");
                }
                else
                {
                    objp.Action = "1";
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                    objp.dt = objL.GetOnlineProducts(objp, "proc_AllEcomProducts");
                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public ActionResult AddAttribute()
        {
            try
            {
                objp.Action = "2";
                objp.dt = objL.AttributeMaster(objp, "proc_AttributeMaster");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult insertAttribute(PropertyClass obj)
        {

            try
            {
                DataTable dt2 = objL.AttributeMaster(obj, "proc_AttributeMaster");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    obj.msg = dt2.Rows[0]["msg"].ToString();
                    obj.strId = dt2.Rows[0]["id"].ToString();
                }
                else
                {
                    obj.msg = "Server not Responding";
                    obj.strId = "0";
                }

            }
            catch (Exception ex)
            {
                obj.msg = ex.Message;
                obj.strId = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailsForEdit(string SrNo)
        {
            try
            {
                objp.Action = "4";
                objp.RespoCode = SrNo.Trim();
                DataTable dt = objL.AttributeMaster(objp, "proc_AttributeMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.RespoCode = dt.Rows[0]["SrNo"].ToString();
                    objp.BanKAccName = dt.Rows[0]["AttributeName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddVarriation()
        {
            try
            {
                ViewBag.CategoryList = PropertyClass.BindDDL(objL.BindAttributeDropDown());
                objp.Action = "2";
                objp.dt = objL.VariationMaster(objp, "proc_VariationMaster");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public JsonResult GetDetailsForEditVariation(string SrNo)
        {
            try
            {
                objp.Action = "4";
                objp.RespoCode = SrNo.Trim();
                DataTable dt = objL.VariationMaster(objp, "proc_VariationMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.RespoCode = dt.Rows[0]["SrNo"].ToString();
                    objp.AccountCode = dt.Rows[0]["AttributeId"].ToString();
                    objp.DepartmentName = dt.Rows[0]["VarriationName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertVarriations(PropertyClass obj)
        {

            try
            {
                DataTable dt2 = objL.VariationMaster(obj, "proc_VariationMaster");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    obj.msg = dt2.Rows[0]["msg"].ToString();
                    obj.strId = dt2.Rows[0]["id"].ToString();
                }
                else
                {
                    obj.msg = "Server not Responding";
                    obj.strId = "0";
                }

            }
            catch (Exception ex)
            {
                obj.msg = ex.Message;
                obj.strId = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public string UniqueId()
        {
            string str = "";
            var temp = Guid.NewGuid().ToString().Replace("-", string.Empty);
            str = Regex.Replace(temp, "[a-zA-Z]", string.Empty).Substring(0, 12);

            return str;
        }
        public JsonResult UpdateProductFeatureStatus(PropertyClass obj)
        {

            try
            {
                DataTable dt2 = objL.UpdateProductFeatureStatus(obj, "proc_UpdateProductFeatureStatus");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    obj.msg = dt2.Rows[0]["msg"].ToString();
                    obj.strId = dt2.Rows[0]["id"].ToString();
                }
                else
                {
                    obj.msg = "Server not Responding";
                    obj.strId = "0";
                }

            }
            catch (Exception ex)
            {
                obj.msg = ex.Message;
                obj.strId = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            finalSize = new Size((int)(newSize.Width), (int)(newSize.Height));
            return finalSize;
        }
        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }

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



        public ActionResult ManageOrder(string OrderId)
        {
            try
            {
                if (!string.IsNullOrEmpty(OrderId))
                {

                    if (Convert.ToString(Session["Role"]) == "9")
                    {
                        objp.deliveryboy = Convert.ToString(Session["CompanyCode"]);
                        ViewBag.DeliverBoy = BindDDL(objL.BindSalesPersonDLLByFranchise(Convert.ToString(Session["CompanyCode"])));
                    }
                    else
                    {
                        objp.deliveryboy = null;
                        ViewBag.DeliverBoy = BindDDL(objL.BindSalesPersonDLL());
                    }



                    objp.Action = "3";
                    objp.CustomerId = OrderId;
                    DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard",OrderId);
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            objp.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                            objp.mDate = ds.Tables[0].Rows[0]["OrdDate"].ToString();
                            objp.GrossPayable = Convert.ToDecimal(ds.Tables[0].Rows[0]["GrossAmount"].ToString());
                            objp.deliverycharges = Convert.ToDecimal(ds.Tables[0].Rows[0]["DeliveryCharges"].ToString());
                            objp.NetTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["NetPayable"].ToString());
                            objp.DiscAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["DiscountAmount"].ToString());
                            objp.PayMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                            objp.Status = ds.Tables[0].Rows[0]["DeliveryStatus"].ToString();
                            objp.SSName = ds.Tables[0].Rows[0]["Name"].ToString();
                            objp.Address = ds.Tables[0].Rows[0]["FullAddress"].ToString();
                            objp.ContactNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                            objp.paystatus = ds.Tables[0].Rows[0]["Paystatus"].ToString();
                            objp.eDate = ds.Tables[0].Rows[0]["CancelDate"].ToString();
                            objp.Description = ds.Tables[0].Rows[0]["CancelReason"].ToString();
                            objp.ReturnDate = ds.Tables[0].Rows[0]["ReturnDate"].ToString();
                            objp.ReturnReasondes = ds.Tables[0].Rows[0]["ReturnReason"].ToString();
                            objp.EwayBillNo = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                            objp.gstamount = Convert.ToDecimal(ds.Tables[0].Rows[0]["GstAmount"].ToString());
                            objp.CoupenAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["CoupenAmount"].ToString());
                        }
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            objp.dt1 = ds.Tables[1];
                        }
                        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            objp.dtcombooffer = ds.Tables[2];
                        }

                    }
                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public JsonResult updateOrderStatus(PropertyClass p)
        {
            try
            {               
                DataTable dt = objL.InsertCancelRequest(p, "proc_CancelOrder");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    if (objp.strId == "1")
                    {
                        objp.PayMode = dt.Rows[0]["payMode"].ToString();
                    }
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
                objp.msg = ex.Message;
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add_Product()
        {
            try
            {
                objp.RespoCode = UniqueId();
                ViewBag.ColorList = PropertyClass.BindDDL(objL.BindcolorDropDown());
                ViewBag.SizeList = PropertyClass.BindDDL(objL.BindSizeDropDown());
                ViewBag.MainCategoryList = PropertyClass.BindDDL(objL.BindProductMainCategoryDropDown());
                ViewBag.VendorList = PropertyClass.BindDDL(objL.BindVendorDropDown());
                ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
                ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
                ViewBag.AttributeList = PropertyClass.BindDDL(objL.BindAttributeDropDown());
                ViewBag.UOMList = PropertyClass.BindDDL(objL.BindUOMDropDown());

                ViewBag.BrandList = PropertyClass.BindDDL(objL.BindManufacturerDropDown());
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public JsonResult InsertProductDetailsNewUpdated(PropertyClass p, string MainImage,string fumultiFile)
        {
            try
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("Images");


                if (MainImage != null)
                {
                    p.Url = MainImage;
                }

                string[] multipleFiles = fumultiFile.Split(',');

                
                foreach (string columnName in multipleFiles)
                {
                    dt.Rows.Add(columnName);
                }



                //HttpFileCollectionBase files = Request.Files;
                //for (int i = 0; i < files.Count; i++)
                //{
                //    HttpPostedFileBase file = files[i];

                //    string fileExt = Path.GetExtension(file.FileName);
                //    var fileName = Path.GetFileName(file.FileName);
                //    string fName = DateTime.Now.Ticks + fileExt;
                //    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                //    string ImagePathThumb = String.Format("/productImagesThumble/{0}{1}", fname, fileExt);
                //    string ImagePath1 = String.Format("/productImages/{0}{1}", fname, fileExt);
                //    using (var img = System.Drawing.Image.FromStream(file.InputStream))
                //    {
                //        SaveToFolder(img, fileName, fileExt, new Size(300, 300), ImagePathThumb);
                //        SaveToFolder(img, fileName, fileExt, new Size(600, 600), ImagePath1);
                //    }
                //    string myfile = fname + fileExt;

                //    dt.Rows.Add(myfile);
                //}
                p.Action = "1";

                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.SSCode = Convert.ToString(Session["UserName"]);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);

                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = 2147483647;

                List<MultiVarList> result = js.Deserialize<List<MultiVarList>>(p.VariationList);

                DataTable dtvarList = new DataTable();
                dtvarList.Columns.Add("TermId");
                dtvarList.Columns.Add("TermName");
                dtvarList.Columns.Add("PurchaseRate");
                dtvarList.Columns.Add("MRP");
                dtvarList.Columns.Add("Discount");
                dtvarList.Columns.Add("GstPer");
                dtvarList.Columns.Add("GstType");
                dtvarList.Columns.Add("GstAmt");
                dtvarList.Columns.Add("SalePrice");
                dtvarList.Columns.Add("ItembarCode");
                dtvarList.Columns.Add("Color");
                dtvarList.Columns.Add("Quantity");
                dtvarList.Columns.Add("ColorImg");
                dtvarList.Columns.Add("Positioncolorimg");
                if (result != null && result.Count > 0)
                {
                    foreach (var itm in result)
                    {
                        string itempath = itm.ColorImg;
                        //var imgpath = Convert.FromBase64String(itempath);  
                        var imgpathnew = itempath.Split(',');
                        string ImageUrl = "";
                        if (imgpathnew.Count() > 1)
                        {
                            itempath = imgpathnew[1];
                            Image img = Base64ToImage(itempath);
                            string pathnew = Path.Combine("/ColorImage/", "color_" + DateTime.Now.Ticks + ".Jpeg");

                            SavefileToFolder(img, new Size(600, 600), pathnew);

                            //img.Save(Server.MapPath(pathnew));
                            ImageUrl = pathnew;

                        }
                        else
                        {
                            ImageUrl = imgpathnew[0];
                        }

                        List<Imgcoloratt> resultimg = js.Deserialize<List<Imgcoloratt>>(itm.ColorImgMulti);

                        string ImageUrl1 = null;
                        foreach (var imgcolor in resultimg)
                        {
                            string itempath1 = imgcolor.attrcolorimg;
                            //var imgpath = Convert.FromBase64String(itempath);  
                            var imgpathnew1 = itempath1.Split(',');

                            if (imgpathnew1.Count() > 1)
                            {
                                itempath1 = imgpathnew1[1];
                                Image img1 = Base64ToImage(itempath1);
                                string pathnew2 = Path.Combine("/ColorImage/", "color_" + DateTime.Now.Ticks + ".Jpeg");

                                SavefileToFolder(img1, new Size(600, 600), pathnew2);
                                //img1.Save(Server.MapPath(pathnew1));
                                ImageUrl1 = ImageUrl1 + "," + pathnew2;

                            }
                            else
                            {
                                ImageUrl1 = imgpathnew1[0];
                            }

                        }

                        if (!string.IsNullOrEmpty(ImageUrl1))
                        {
                            ImageUrl1 = ImageUrl1.Substring(1);
                        }

                        dtvarList.Rows.Add(itm.TermId, itm.TermName, itm.PurchaseRate, itm.MRP, itm.Discount, itm.GstPer, itm.GstType, itm.GstAmt, itm.SalePrice, itm.ItembarCode, itm.color, itm.Quantity, ImageUrl, ImageUrl1);
                    }
                }
                p.ReturnAndRefundPolicy = p.ProductSpacification;
                DataTable dt1 = new DataTable();
                if (p.SizeCode != null)
                {
                    
                    
                    dt1 = objL.InsertOnlineProductDetailsNewUpdated(p, "proc_InsertEcommarceProductNew", dt, dtvarList);
                }
                else
                {
                    dt1 = objL.InsertOnlineProductDetailsNewUpdated(p, "proc_InsertEcommarceProductNewcolo", dt, dtvarList);
                }
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

        private void SavefileToFolder(Image img, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }
        private void SaveToFolder(Image img, object fileName, object fileExt, Size size, string pathnew)
        {
            throw new NotImplementedException();
        }

        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        public JsonResult AutoCompleteItem(string ActionFilter, string itemGroup)
        {           
            List<PropertyClass> objnew = new List<PropertyClass>();
            objp.ItemName = !string.IsNullOrEmpty(itemGroup) ? itemGroup : null;
          
            DataTable ds2 = objL.GetAutoCompeleteItems(objp, "BindAutoCompletevariation");
            if (ds2 != null && ds2.Rows.Count > 0)
            {
                foreach (DataRow dr in ds2.Rows)
                {
                    PropertyClass obj22 = new PropertyClass();
                    obj22.VariationId = Convert.ToString(dr["SrNo"]);
                    obj22.GroupName = Convert.ToString(dr["VarriationName"]);
                    objnew.Add(obj22);
                }
            }
            objp.VarList = objnew;
            return Json(objp.VarList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult update_Product(string ProductId)
        {
            try
            {
                objp.RespoCode = UniqueId();
                ViewBag.VendorList = PropertyClass.BindDDL(objL.BindVendorDropDown());
                ViewBag.MainCategoryList = PropertyClass.BindDDL(objL.BindProductMainCategoryDropDown());
                ViewBag.ColorList = PropertyClass.BindDDL(objL.BindcolorDropDown());
                ViewBag.SizeList = PropertyClass.BindDDL(objL.BindSizeDropDown());
                ViewBag.ItemGroupList = PropertyClass.BindDDL(objL.BindItemGroupDropDown());
                ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDown());
                ViewBag.AttributeList = PropertyClass.BindDDL(objL.BindAttributeDropDown());
                ViewBag.UOMList = PropertyClass.BindDDL(objL.BindUOMDropDown());
                ViewBag.BrandList = PropertyClass.BindDDL(objL.BindManufacturerDropDown());
                ViewBag.SubCategory= PropertyClass.BindDDL(objL.BindSubCategoryDllNew());
                ViewBag.Dailydeal = PropertyClass.BindDDL(objL.BindDailydealDllNew());

                if (!string.IsNullOrEmpty(ProductId))
                {
                    objp.Action = "2";
                    objp.ItemCode = ProductId;

                    objp.dt22 = objL.DeleteProductDetails(objp, "proc_EditDeleteProduct");                   
                            if (objp.dt22 != null && objp.dt22.Tables[0].Rows.Count > 0)
                            {
                                objp.ItemCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ProductCode"]);
                                objp.Description = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ProductDescription"]);
                                objp.PurchaseFile = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ProductPic"]);
                                objp.ItemName = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ProductName"]);
                                objp.MainCategoryCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["MainCategoryCode"]);
                                objp.SubCategoryCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["SubCategoryCode"]);
                                objp.VendorCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["VendorId"]);
                                objp.ProductType = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ProductType"]);
                                objp.SalePrice = Convert.ToString(objp.dt22.Tables[0].Rows[0]["SalePrice"]);
                                objp.RegularPrice  = Convert.ToString(objp.dt22.Tables[0].Rows[0]["RegularPrice"]);

                                objp.DiscPer = Convert.ToDecimal(objp.dt22.Tables[0].Rows[0]["DiscPer"]);
                                objp.GSTPer = Convert.ToDecimal(objp.dt22.Tables[0].Rows[0]["GSTRate"]);
                                objp.CessRate = Convert.ToDecimal(objp.dt22.Tables[0].Rows[0]["CessRate"]);
                                objp.PurchaseRate_Loose = Convert.ToDecimal(objp.dt22.Tables[0].Rows[0]["PurchaseRate_Loose"]);

                                objp.Manufacturerid = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ManufacturerId"]);
                                objp.ItemBarCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ItemBarCode"]);
                                objp.HSNCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["HSNCode"]);
                                objp.LowStockAlert = Convert.ToString(objp.dt22.Tables[0].Rows[0]["LowStockAlert"]);
                                objp.UOM = Convert.ToString(objp.dt22.Tables[0].Rows[0]["LooseUOM"]);

                                objp.Purchase_taxIncludeExclude = Convert.ToString(objp.dt22.Tables[0].Rows[0]["Purchase_TaxIncludeExclude"]);
                                objp.Sale_taxIncludeExclude = Convert.ToString(objp.dt22.Tables[0].Rows[0]["Sale_TaxIncludeExclude"]);
                                objp.ReturnAndRefundPolicy= Convert.ToString(objp.dt22.Tables[0].Rows[0]["RefundAndreturnPolicy"]);
                                objp.GstAmt = Convert.ToDecimal(objp.dt22.Tables[0].Rows[0]["GstAmount"]);
                                objp.Taxtype11 = Convert.ToString(objp.dt22.Tables[0].Rows[0]["Taxstatus"]);                            
                                objp.SizeCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["SizeCode1"]);
                                objp.ColorCode = Convert.ToString(objp.dt22.Tables[0].Rows[0]["ColorCode1"]);
                                objp.Dailydeal = Convert.ToString(objp.dt22.Tables[0].Rows[0]["DailydealId"]);

                    }
                  
                    objp.Action = "3";
                    objp.dt01 = objL.DeleteProductDetails(objp, "proc_EditDeleteProduct");
                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public JsonResult DeleteProductDetails(PropertyClass obj)
        {

            try
            {
                obj.BranchCode = Convert.ToString(Session["CompanyCode"]);
                obj.EntryBy = Convert.ToString(Session["UserName"]);

                if (obj.VarLists != null)
                {
                    foreach (var item in obj.VarLists)
                    {
                        obj.BranchCode = Convert.ToString(Session["CompanyCode"]);
                        obj.EntryBy = Convert.ToString(Session["UserName"]);
                        //item.M_Financial = Convert.ToInt32(Session["financialyear"]);
                        ////obj.BranchCode = Convert.ToString(Session["CompanyCode"]);
                        //obj.CreatedBy = Convert.ToInt32(Session["usercode"]);
                        obj.dt2 = objL.DeleteProductDetails1(obj, item.ItemCode, "proc_EditDeleteProduct");
                       
                    }
                }
                else
                {
                    obj.VarLists = null;
                }
                //DataTable dt2 = objL.DeleteProductDetails(obj, "proc_EditDeleteProduct");
                if (obj.dt2 != null && obj.dt2.Rows.Count > 0)
                {
                    obj.msg = obj.dt2.Rows[0]["msg"].ToString();
                    obj.strId = obj.dt2.Rows[0]["id"].ToString();
                    
                    //obj.msg = "Succusss";
                    //obj.strId = "1";
                    obj.dt2 = null;
                }
                else
                {
                    obj.msg = "Server not Responding";
                    obj.strId = "0";
                }

            }
            catch (Exception ex)
            {
                obj.msg = ex.Message;
                obj.strId = "0";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult UpdateProductDetails(PropertyClass p, string MainImage, string fumultiFile)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ImageUrl");


                if (MainImage != null)
                {
                    p.Url = MainImage;
                }

                string[] multipleFiles = fumultiFile.Split(',');


                foreach (string columnName in multipleFiles)
                {
                    dt.Rows.Add(columnName);
                }

                p.Action = "1";
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.SSCode = Convert.ToString(Session["UserName"]);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);

                JavaScriptSerializer js = new JavaScriptSerializer();
                List<MultiVarList> result = js.Deserialize<List<MultiVarList>>(p.VariationList);

                DataTable dtvarList = new DataTable();
                dtvarList.Columns.Add("TermId");
                dtvarList.Columns.Add("TermName");
                dtvarList.Columns.Add("PurchaseRate");
                dtvarList.Columns.Add("MRP");
                dtvarList.Columns.Add("Discount");
                dtvarList.Columns.Add("GstPer");
                dtvarList.Columns.Add("GstType");
                dtvarList.Columns.Add("GstAmt");
                dtvarList.Columns.Add("SalePrice");
                dtvarList.Columns.Add("ItembarCode");
                if (result != null && result.Count > 0)
                {
                    foreach (var itm in result)
                    {
                        dtvarList.Rows.Add(itm.TermId, itm.TermName, itm.PurchaseRate, itm.MRP, itm.Discount, itm.GstPer, itm.GstType, itm.GstAmt, itm.SalePrice, itm.ItembarCode);
                    }
                }
                p.ReturnAndRefundPolicy = p.ProductSpacification;

                DataTable dt1 = objL.UpdateOnlineProductDetails(p, "proc_UpdateEcommarceProduct", dt, dtvarList);
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

        public JsonResult SaveReview(string ReiewStatus, string CustmerName, string ReiewDis, string ProductId, HttpPostedFileBase image)
        {
            string msg = "";
            try
            {
                string CustomerId = null;
                if (Session["UserName"] != null)
                {
                    CustomerId = Convert.ToString(Session["UserName"]);
                }
                if (image != null)
                {

                    string fileName = Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/UploadFile/"), fileName);
                    image.SaveAs(filePath);

                }

                DataTable dt = objL.InsertReview(CustomerId, CustmerName, ReiewDis, ProductId, ReiewStatus, image.FileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception)
            {
                // Handle exceptions
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutofillItems(string Prefix)
        {
            ClsSearch cls = new ClsSearch();
            DataTable dt = objL.KeywordSearch(Prefix);
            List<ClsSearch> lst = new List<ClsSearch>();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsSearch cls1 = new ClsSearch();
                    cls1.Found = dt.Rows[i]["ProductName"].ToString();
                    lst.Add(cls1);
                }
                cls.lst = lst;
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        public ActionResult OrderReport(string fromdate, string todate, string OrderId)

        {
            clsearchOrder ord = new clsearchOrder();
            ord._Action = "1";
            ord._FromDate = !string.IsNullOrEmpty(fromdate) ? fromdate : fromdate;
            ord._ToDate = !string.IsNullOrEmpty(todate) ? todate : todate;
            ord._OrderId = !string.IsNullOrEmpty(OrderId) ? OrderId : OrderId;

            ord._dtOrder = objL.Proc_GetOrderReport(ord);

            return View(ord);
        }
        public ActionResult FilterOrderReport(string ddlOrder)
        {
            clsearchOrder ord = new clsearchOrder();
            ord._Action = "1";
            ord.ddlOrder = !string.IsNullOrEmpty(ddlOrder) ? ddlOrder : ddlOrder;

            ord._dtOrder = objL.Proc_GetFilterOrderReport(ord);

            return View(ord);
        }
        public ActionResult Cancelreturnorder(string ddlcnclrturn, string ddlOrder)
        {
            clsearchOrder ord = new clsearchOrder();
            ord._Action = "2";
            ord.ddlOrder = !string.IsNullOrEmpty(ddlOrder) ? ddlOrder : ddlOrder;
            ord.ddlcnclrturn = !string.IsNullOrEmpty(ddlcnclrturn) ? ddlcnclrturn : ddlcnclrturn;
            ord._dtOrder = objL.Proc_GetFilterOrderReport(ord);
            return View(ord);
        }


        public ActionResult Payment_history_Report(string customerId, string ddlOrder)
        {
            clsearchOrder ord = new clsearchOrder();
            ViewBag.customerIdList = PropertyClass.BindDDL(objL.BindcustomerIdListDropDown());
            ord._Action = "3";
            ord.customerId = !string.IsNullOrEmpty(customerId) ? customerId : customerId;
            ord._dtOrder = objL.Proc_GetFilterOrderReport(ord);
            return View(ord);
        }
        public ActionResult Vendor_payout_Report(string VendorList, string ddlOrder)
        {
            clsearchOrder ord = new clsearchOrder();
            ViewBag.VendorList = PropertyClass.BindDDL(objL.BindVendorDropDown());
            ord._Action = "4";
            ord.customerId = !string.IsNullOrEmpty(VendorList) ? VendorList : VendorList;
            ord._dtOrder = objL.Proc_GetFilterOrderReport(ord);
            return View(ord);
        }

        //public ActionResult Sale_Purchase_Reprt(string VendorList, string ddlOrder)
        //{
        //    clsearchOrder ord = new clsearchOrder();
        //    ViewBag.VendorList = PropertyClass.BindDDL(objL.BindVendorDropDown());
        //    ord._Action = "4";
        //    ord.customerId = !string.IsNullOrEmpty(VendorList) ? VendorList : VendorList;
        //    ord._dtOrder = objL.Proc_GetFilterOrderReport(ord);
        //    return View(ord);
        //}


        // for get data for service 


        [HttpGet]
        public ActionResult View_Services()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultNewCon"].ConnectionString;
            List<ServiceRequestModel> serviceDataList = new List<ServiceRequestModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("proc_GetServiceData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ServiceRequestModel serviceData = new ServiceRequestModel();
                    serviceData.SrNo = rdr["Id"].ToString();
                    serviceData.ServiceType = rdr["ServiceType"].ToString();
                    serviceData.FullName = rdr["FullName"].ToString();
                    serviceData.MobileNumber = rdr["MobileNumber"].ToString();
                    serviceData.Email = rdr["Email"].ToString();
                    serviceData.Address = rdr["Address"].ToString();
                    serviceData.Date = Convert.ToDateTime(rdr["RequestDate"]);
                    serviceDataList.Add(serviceData);
                }
                con.Close();
            }
            return View(serviceDataList);
        }



        //Added By Tanu Gupta
        [HttpGet]
        public ActionResult Vendor_ContactDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultNewCon"].ConnectionString;
            List<ClsVendorContactUs> VCDataList = new List<ClsVendorContactUs>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Proc_VendorContact", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "2"); // Specify the action as '2' for retrieving data
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClsVendorContactUs VCData = new ClsVendorContactUs();
                    VCData.Name = rdr["name"].ToString();
                    VCData.mobileNo = rdr["mobile"].ToString();
                    VCData.Email = rdr["email"].ToString();
                    VCData.Message = rdr["message"].ToString();
                    VCData.Date = Convert.ToDateTime(rdr["EntryDate"]);
                    VCDataList.Add(VCData);
                }
                con.Close();
            }
            return View(VCDataList);
        }



    }






    public class ClsSearch
    {
        public string Found { get; set; }
        public string Found_Id { get; set; }
        public List<ClsSearch> lst { get; set; }
    }



}