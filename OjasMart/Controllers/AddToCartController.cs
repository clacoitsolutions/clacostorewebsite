using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{

    public class AddToCartController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: AddToCart
        public ActionResult ShowCartSummary(string ProductId, string IPAddress, string Qty, string AttrId, string VarId, string Type, string SizeId, string color)
        {
            string kalashCCookie = "";
            AttrId = !string.IsNullOrEmpty(AttrId) ? AttrId : null;
            VarId = !string.IsNullOrEmpty(VarId) ? VarId : "0";
            if (IPAddress == null || IPAddress.ToString() == "")
            {
                if (Request.Cookies["Kalash"] != null)
                {
                    kalashCCookie = Request.Cookies["Kalash"].Value.ToString();
                }
                else
                {
                    kalashCCookie = "KLSH" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
                    HttpCookie myCookie = new HttpCookie("Kalash");
                    myCookie.Value = kalashCCookie;
                    myCookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(myCookie);

                }
                IPAddress = kalashCCookie;
            }
            int Quantity = !string.IsNullOrEmpty(Qty) ? Convert.ToInt32(Qty) : 1;

            if (Session["mCode"] != null)
            {
                if ((!string.IsNullOrEmpty(ProductId)))
                {
                    if (Type == "update")
                    {
                        string CustomerCode = Session["mCode"].ToString();

                        objL.InsertUpdateCart("4", CustomerCode, null, ProductId, Quantity, AttrId, VarId, SizeId, color);
                    }
                    else
                    {
                        string CustomerCode = Session["mCode"].ToString();

                        objL.InsertUpdateCart("1", CustomerCode, null, ProductId, Quantity, AttrId, VarId, SizeId, color);
                    }

                }
                if ((!string.IsNullOrEmpty(IPAddress)))
                {
                    objL.InsertTempCartToreal(Session["mCode"].ToString(), IPAddress);
                }
                string CustCode = Session["mCode"].ToString();
                objp.dt = objL.getCartDetails(CustCode, "1");
                objp.CountCart = objp.dt.Rows.Count;
            }
            else if ((!string.IsNullOrEmpty(ProductId)) && (!string.IsNullOrEmpty(IPAddress)))
            {
                if (Type == "update")
                {
                    string CustCode = IPAddress.ToString();
                    objL.InsertUpdateCart("3", null, CustCode, ProductId, Quantity, AttrId, VarId, SizeId, color);
                }
                else
                {
                    string CustCode = IPAddress.ToString();
                    objL.InsertUpdateCart("2", null, CustCode, ProductId, Quantity, AttrId, VarId, SizeId, color);
                }
                objp.dt = objL.getCartDetails(IPAddress, "2");
                objp.CountCart = objp.dt.Rows.Count;

            }
            else
            {
                objp.dt = objL.getCartDetails(IPAddress, "2");
                objp.CountCart = objp.dt.Rows.Count;
            }
            decimal totalamt = 0, regAmt = 0, totSaving = 0;
            if (objp.dt != null && objp.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in objp.dt.Rows)
                {
                    totalamt += dr["totalprice"].ToString().Trim() != "" ? Convert.ToDecimal(dr["totalprice"].ToString().Trim()) : 0;
                    regAmt += dr["salePrice"].ToString().Trim() != "" ? Convert.ToDecimal(dr["salePrice"].ToString().Trim()) : 0;
                }
            }
            totSaving = regAmt - totalamt;
            objp.todayPoAmt = totalamt.ToString("0.00");
            objp.WalletBal = totSaving.ToString("0.00");
            objp.TotalCountCart = Convert.ToString(objp.dt.Rows.Count);

            if(Session["UserName"] != null)
            {
                objp.EntryBy = Session["UserName"].ToString();
                objp.dt3 = objL.getwishlist(objp.EntryBy, "25");
                objp.TotalCountCart = Convert.ToString(objp.dt3.Rows.Count);

            }
            else
            {
                objp.TotalCountCart = "0";
            }

            return View(objp);
        }

        public ActionResult CartCount(string ProductId, string IPAddress, string Qty)
        {
            string kalashCCookie = "";
            if (IPAddress == null || IPAddress.ToString() == "")
            {
                if (Request.Cookies["Kalash"] != null)
                {
                    kalashCCookie = Request.Cookies["Kalash"].Value.ToString();
                }
                else
                {
                    kalashCCookie = "KLSH" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
                    HttpCookie myCookie = new HttpCookie("Kalash");
                    myCookie.Value = kalashCCookie;
                    myCookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(myCookie);
                }
                IPAddress = kalashCCookie;
            }
            int Quantity = !string.IsNullOrEmpty(Qty) ? Convert.ToInt32(Qty) : 1;

            if (Session["mCode"] != null)
            {
                if ((!string.IsNullOrEmpty(ProductId)))
                {
                    string CustomerCode = Session["mCode"].ToString();

                    objL.InsertUpdateCart("1", CustomerCode, null, ProductId, Quantity);

                }
                if ((!string.IsNullOrEmpty(IPAddress)))
                {
                    objL.InsertTempCartToreal(Session["mCode"].ToString(), IPAddress);
                }
                string CustCode = Session["mCode"].ToString();
                objp.dt = objL.getCartDetails(CustCode, "1");
                objp.CountCart = objp.dt.Rows.Count;
            }
            else if ((!string.IsNullOrEmpty(ProductId)) && (!string.IsNullOrEmpty(IPAddress)))
            {
                string CustCode = IPAddress.ToString();
                objL.InsertUpdateCart("2", null, CustCode, ProductId, Quantity);

                objp.dt = objL.getCartDetails(IPAddress, "2");
                objp.CountCart = objp.dt.Rows.Count;
            }
            else
            {
                objp.dt = objL.getCartDetails(IPAddress, "2");
                objp.CountCart = objp.dt.Rows.Count;
            }
            decimal totalamt = 0;
            if (objp.dt != null && objp.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in objp.dt.Rows)
                {
                    totalamt += dr["PayableAmt"].ToString().Trim() != "" ? Convert.ToDecimal(dr["PayableAmt"].ToString().Trim()) : 0;
                }
            }
            objp.todayPoAmt = totalamt.ToString("0.00");
            objp.TotalCountCart = Convert.ToString(objp.dt.Rows.Count);
            return View(objp);
        }


        public ActionResult Wishlistcount(string ProductId, string IPAddress, string Qty)
        {
            if (Session["UserName"] != null)
            {

            objp.EntryBy = Session["UserName"].ToString();
            objp.dt3 = objL.getwishlist(objp.EntryBy, "25");
            objp.TotalCountCart = Convert.ToString(objp.dt3.Rows.Count);
            }
            else
            {
                objp.TotalCountCart = "0";
            }

            return View(objp);
        }


        public ActionResult RemoveFromCart(string ProductId,string IPAddress)
        {
            string Msg = "";
            try
            {
                string kalashCCookie = "";
                if (IPAddress == null || IPAddress.ToString() == "")
                {
                    if (Request.Cookies["Kalash"] != null)
                    {
                        kalashCCookie = Request.Cookies["Kalash"].Value.ToString();
                    }
                    else
                    {
                        kalashCCookie = "KLSH" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
                        HttpCookie myCookie = new HttpCookie("Kalash");
                        myCookie.Value = kalashCCookie;
                        myCookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(myCookie);
                    }
                    IPAddress = kalashCCookie;
                }
                if (Session["mCode"] != null)
                {
                    DataTable dt = objL.RemoveFromCart(Convert.ToString(Session["mCode"]), "1", ProductId);
                    if(dt != null && dt.Rows.Count>0)
                    {
                        Msg = dt.Rows[0]["id"].ToString();
                    }
                }
                else
                {
                    DataTable dt = objL.RemoveFromCart(IPAddress, "2", ProductId);
                    Msg = dt.Rows[0]["id"].ToString();
                }               
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
    }
}