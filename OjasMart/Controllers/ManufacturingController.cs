using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using OjasMart.Models;
using System.Collections;

namespace OjasMart.Controllers
{
    public class ManufacturingController : Controller
    {
        // GET: Manufacturing
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        public ActionResult ConsumtionSetup(string Id,string Mode)
        {
            try
            {
                ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDownFinishedGoods(Session["CompanyCode"].ToString()));
                ViewBag.UOMList = PropertyClass.BindDDL(objL.BindUOMDropDown());

                objp.Action = "1";
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }

                if (Id != "" && Mode == "D")
                {
                    DataTable dt2 = objL.DelProductConsumption("4", Id);
                }
                objp.dt = objL.GetItemsForConsumption(objp, "proc_GetItemsForProductConsumption");
                objp.dt1 = objL.GetProductConsumption("2", null,Session["CompanyCode"].ToString());
               
            }
            catch(Exception ex)
            { }
            return View(objp);
        }

        public JsonResult SaveProductConsumption(PropertyClass pc)
        {
            string data = pc.strRawdata;
            string msg = "";
            Hashtable[] hs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Hashtable[]>(pc.strRawdata);
            DataTable dtItem = new DataTable(); 
            if (hs != null && hs.Length > 0)
            {

                dtItem.Columns.Add("RawItemid", typeof(string));
                dtItem.Columns.Add("Quantity", typeof(decimal));
                dtItem.Columns.Add("Unit", typeof(string));

                foreach (Hashtable item in hs)
                {
                    dtItem.Rows.Add(item["Rawmaterialid"], item["Rawquantity"], item["Rawuom"]);
                }
                DataTable dt2 = objL.SaveproductConsumption(pc, dtItem);
                if(dt2!=null && dt2.Rows.Count > 0)
                {
                    msg = "Successfully saved";
                }
                else
                {
                    msg = "Something went wrong!try again";
                }
            }

                   
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Makeproduct(string Id, string Mode)
        {
            try
            {
                ViewBag.ItemHeadList = PropertyClass.BindDDL(objL.BindItemheadDropDownFinishedGoods(Session["CompanyCode"].ToString()));
                ViewBag.UOMList = PropertyClass.BindDDL(objL.BindUOMDropDown());

                objp.Action = "1";
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }

                if (Id != "" && Mode == "D")
                {
                    DataTable dt2 = objL.GetFinalProduct("3", Id, Session["CompanyCode"].ToString());
                }
                if (Id != "")
                {
                    objp.dt = objL.GetProductConsumption("3", Id,Session["CompanyCode"].ToString());
                  if (objp.dt.Rows.Count > 0)
                    {
                        objp.UOM = objp.dt.Rows[0]["UOM"].ToString();
                        objp.ItemID = objp.dt.Rows[0]["ProductId"].ToString();
                    }
                }
                else
                {
                    objp.dt = objL.GetItemsForConsumption(objp, "proc_GetItemsForProductConsumption");
                }
                objp.dt1 = objL.GetFinalProduct("4", null,Session["CompanyCode"].ToString());

            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public JsonResult SaveFinalisedProduct(PropertyClass pc) 
        {
            string data = pc.strRawdata;
            string msg = "";
            Hashtable[] hs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Hashtable[]>(pc.strRawdata);
            DataTable dtItem = new DataTable();
            if (hs != null && hs.Length > 0)
            {

                dtItem.Columns.Add("RawItemid", typeof(string));
                dtItem.Columns.Add("Quantity", typeof(decimal));
                dtItem.Columns.Add("Unit", typeof(string));
                dtItem.Columns.Add("Leakage", typeof(string));
                foreach (Hashtable item in hs)
                {
                    dtItem.Rows.Add(item["Rawmaterialid"], item["Rawquantity"], item["Rawuom"], item["leakage"]);
                }
                DataTable dt2 = objL.SaveFinalProduct(pc, dtItem);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    msg = "Successfully saved";
                }
                else
                {
                    msg = "Something went wrong!try again";
                }
            }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public string GetproductItem(string ProductId)  
        {
            try
            {
                return PropertyClass.ConvertDtToJSON(objL.GetFinalProduct("5",ProductId,Session["CompanyCode"].ToString()));
            }
            catch (Exception exec)
            {
                return "false";
            }
        }

    }
}