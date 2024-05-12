using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;
using System.Runtime.Remoting;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Script.Serialization;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace OjasMart.Controllers
{
    public class ProductDetailController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: ProductDetail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowProduct_detail(string ProductCategory, string proId, string title, string CatId ,string VarId)
         {
            try
            {
                objp.MainCategoryName = ProductCategory;
                objp.ProductName = title;
                objp.MainCategoryCode = CatId;
                //objp.VariationId = VarId;
                objp.VariationId = null;
                if (!string.IsNullOrEmpty(proId))
                {
                    objp.ItemCode = proId;
                    objp.Action = "1";
                    if (Session["StockistId"]!=null)
                    {
                        objp.StockistId = Session["StockistId"].ToString();
                    }
                    else
                    {
                        objp.StockistId = null;
                    }
                  DataTable dt = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        //objp.Dailydeal = dt.Rows[0]["DealName"].ToString();
                        //objp.Id = Convert.ToInt32(dt.Rows[0]["dId"]);
                        //objp.FromTime = dt.Rows[0]["FromTime"].ToString();
                        //objp.ToTime = dt.Rows[0]["ToTime"].ToString();
                        //objp.DisPrice = dt.Rows[0]["DisPrice"].ToString();
                        //objp.startDate = dt.Rows[0]["Date"].ToString();


                        objp.ItemCode = dt.Rows[0]["ProductCode"].ToString();
                        objp.ProductName = dt.Rows[0]["ProductName"].ToString();
                        objp.RegularPrice = dt.Rows[0]["RegularPrice"].ToString();
                        objp.SalePrice = dt.Rows[0]["salevalue"].ToString();
                        objp.PurchaseFile = dt.Rows[0]["ProductMainImageUrl"].ToString();
                        objp.vendorname = dt.Rows[0]["Name"].ToString();
                        objp.DiscPer = Convert.ToDecimal(dt.Rows[0]["Discper"]);
                        objp.Description = dt.Rows[0]["ProductDescription"].ToString();
                        objp.ProductSpacification = dt.Rows[0]["ProductSpecification"].ToString();
                        objp.ProductType = dt.Rows[0]["ProductType"].ToString();
                        objp.ReturnAndRefundPolicy = dt.Rows[0]["RefundAndreturnPolicy"].ToString();
                        objp.FashionType = dt.Rows[0]["FashionType"].ToString();
                        objp.StockStatus = dt.Rows[0]["StockStatus"].ToString();
                    }
                    objp.Action = "72";
                    objp.dt3 = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                    if (objp.dt3 != null && objp.dt3.Rows.Count > 0)
                    {    
                        objp.TotalReviews = objp.dt3.Rows[0]["totRew"].ToString();
                        objp.AvgReviews = objp.dt3.Rows[0]["Avg"].ToString();
                    }
                    else
                    {
                        objp.TotalReviews = "0";
                        objp.AvgReviews = "0";
                    }
                    objp.Action = "2";
                    objp.dt2 = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");


                    objp.Action = "3";
                    objp.dtCatProduct = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");


                    objp.Action = "4";
                    objp.dt1 = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");

                  

                    objp.Action = "5";
                    objp.dt = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");

                    //if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    //{
                    //    objp.dt2 = ds.Tables[1];
                    //}
                    //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    //{
                    //    objp.dt1 = ds.Tables[2];
                    //}
                    //if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    //{
                    //    objp.dtCatProduct = ds.Tables[3];
                    //}
                   if(objp.VariationId != null && objp.VariationId != "")
                    {
                        objp.Action = "6";
                        objp.sizeList = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                        objp.Action = "7";
                        objp.ColorList = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                    }
                   else
                    {
                        objp.Action = "74";
                        objp.ColorList = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                        objp.Action = "61";
                        objp.sizeList = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                  
                    }
                    objp.Action = "71";
                    objp.TableReview = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");

                    objp.Action = "73";
                    objp.dt4 = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                    
                    if (objp.dt4 != null && objp.dt4.Rows.Count > 0)
                    {
                        objp.TotalReviews1 = objp.dt4.Rows[0]["totalreview"].ToString();
                        objp.totalrow = objp.dt4.Rows[0]["totalrow"].ToString();
                        objp.avg = objp.dt4.Rows[0]["avg"].ToString();
                        objp.poor = objp.dt4.Rows[0]["poor"].ToString();
                        objp.Average = objp.dt4.Rows[0]["Average"].ToString();
                        objp.Good = objp.dt4.Rows[0]["Good"].ToString();
                        objp.VeryGood = objp.dt4.Rows[0]["VeryGood"].ToString();
                        objp.Exellent = objp.dt4.Rows[0]["Exellent"].ToString();
                       
                    }
                    else
                    {
                        objp.TotalReviews1 = "0";
                        objp.totalrow = "0";
                        objp.avg = "0";
                        objp.poor = "0";
                        objp.Average = "0";
                        objp.Good = "0";
                        objp.VeryGood = "0";
                        objp.Exellent = "0";
                    }
                }
                var nwDt = DateTime.Now.ToShortDateString();

            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult Getcolorlistbysize(string ProductId, string variationname)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            //Atributeproductcls nobj = new Atributeproductcls();
            try
            {
                 
                dt = objL.Proc_productcolorlist(ProductId, variationname, "Proc_GetProductDetail_Updated");
                if (dt.Rows.Count > 0)
                {
                    objp.ColorImg = ConvertDataTabletoString(dt);
                }

                dt1 = objL.Proc_productcolorimagelist(ProductId, variationname, "Proc_GetProductDetail_Updated");
                if (dt1.Rows.Count > 0)
                {
                    objp.Positioncolorimg = ConvertDataTabletoString(dt1);
                }

            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchCategory(string CatName,string CatId, string SubCatId ,string search ,string sort,string Min ,string Max)
       {
            try
            {
                if(search != null)
                {
                    objp.search = search;
                }
                else
                {
                    objp.search = "";
                }
                objp.CatName1 = CatName;
                objp.CatId1 = CatId;
                objp.SubCatId1 = SubCatId;

                objp.Sort = sort;
                objp.Min = Min;
                objp.Max = Max;

                if (CatId != "0" && SubCatId =="0")
                {
                    objp.MainCategoryName = CatName;
                    objp.MainCategoryCode = CatId;
                    //replace Action 2 same Procedure
                    objp.Action = "8";
                    DataSet ds = objL.GetSingleProductDetail(objp, "proc_GetSingleProductView");
                    if (ds != null)
                    {
                        objp.dt = ds.Tables[0];
                    }
                    ViewBag.MainCategoryCode = CatId;
                    
                }

                else if (CatId != "0" && SubCatId != "0")
                {
                    objp.MainCategoryName = CatName;
                    objp.MainCategoryCode = SubCatId;
                    //replace Action 2 same Procedure
                    objp.Action = "9";
                    DataSet ds = objL.GetSingleProductDetail(objp, "proc_GetSingleProductView");
                    if (ds != null)
                    {
                        objp.dt = ds.Tables[0];
                    }
                    ViewBag.MainCategoryCode = CatId;
                    ViewBag.subcategoryId = "0";
                }
                else
                {

                    objp.MainCategoryName = CatName;
                    objp.MainCategoryCode = CatId;
                    objp.Action = "10";
                    DataSet ds = objL.GetSingleProductDetail(objp, "proc_GetSingleProductView");
                    if (ds != null && ds.Tables != null)
                    {
                        objp.dt = ds.Tables[0];
                    }
                    ViewBag.MainCategoryCode = "0";
                }

            }
            catch(Exception ex)
            { }
            return View(objp);
        }
        public ActionResult SearchStockiestProduct(string StoreName, string StockistId)
        {
            try
            {
                objp.StockistName = StoreName;
                objp.StockistId = StockistId;
                objp.Action = "3";
                DataSet ds = objL.GetStockWiseProductDetail(objp, "proc_GetSingleProductView");
                if (ds != null)
                {
                    objp.dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public PartialViewResult VariationPrice(string VariationId,string discount)
        {
            objp.StockistName = VariationId;
            objp.StockistId = discount;
            objp.Action = "1";
            DataSet ds = objL.getProductVariationPrice(objp, "Proc_getVraitionProductPrice");
            if (ds != null)
            {
                objp.dt = ds.Tables[0];
            }
            return PartialView(objp);
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

        public JsonResult AddToWhishlist(string ProductId, string EntryBy)
        {
            try
            {

                DataTable dt1 = new DataTable();
                EntryBy = Convert.ToString(Session["UserName"]);
                if (string.IsNullOrEmpty(EntryBy))
                {

                    return Json(new { redirectToLogin = true });
                }

                dt1 = objL.Proc_AddToWhishlist(ProductId, EntryBy, "Proc_GetProductDetail_Updated");
                if (dt1.Rows.Count > 0)
                {
                    objp.msg = dt1.Rows[0]["id"].ToString();
                }
                else
                {
                    objp.msg = "First Login";
                }
            }
            catch (Exception ex)
            {
                objp.msg = "Something went wrong";
            }
            return Json(objp);
        }
    }
}