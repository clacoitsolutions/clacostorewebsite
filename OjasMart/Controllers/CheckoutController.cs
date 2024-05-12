using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class CheckoutController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: Checkout
        public ActionResult Index(string cName, string pCode, string cId, string pQty, string cType, string AttrId, string varId)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString() != "4")
                {
                    return RedirectToAction("login", "CustomerAccount");
                }
            }
            else
            {
                return RedirectToAction("Index", "CustomerAccount");
            }


            objp.MainCategoryCode = cId;
            objp.productQty = pQty;
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
            if (cType == "p")
            {
                if (!string.IsNullOrEmpty(pCode))
                {
                    objp.ItemCode = pCode;
                    objp.Action = "1";
                    objp.VariationId = varId;
                    DataSet ds = objL.GetSingleProductDetail(objp, "proc_GetSingleProductView");
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            objp.dt2 = ds.Tables[0];
                            decimal totReg = 0, totSale = 0, totSaving = 0;
                            if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                            {
                                foreach (DataRow dr in objp.dt2.Rows)
                                {
                                    totReg += dr["RegularPrice"].ToString() != "" ? Convert.ToDecimal(dr["RegularPrice"].ToString()) : 0;
                                    totSale += dr["SalePrice"].ToString() != "" ? Convert.ToDecimal(dr["SalePrice"].ToString()) : 0;
                                }
                                totReg = totReg * Convert.ToInt32(pQty);
                                totSale = totSale * Convert.ToInt32(pQty);
                                totSaving = totReg - totSale;
                                objp.todayPoAmt = totSaving.ToString("0.00");
                                objp.NetTotal = totSale;
                            }
                        }
                        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            objp.dt1 = ds.Tables[2];
                        }
                    }
                }
            }
            else
            {
                string kalashCCookie = "", IPAddress = null;
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
                    if ((!string.IsNullOrEmpty(IPAddress)))
                    {
                        objL.InsertTempCartToreal(Session["mCode"].ToString(), IPAddress);
                    }
                    string CustCode = Session["mCode"].ToString();
                    objp.dt2 = objL.getCartDetails(CustCode, "3");
                    objp.dtcombooffer = objL.Proc_GetComboOffer(CustCode, "1");

                    decimal totReg = 0, totSale = 0, totSaving = 0,totalgst=0;
                    int prodQty = 0;
                    if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in objp.dt2.Rows)
                        {
                            totReg += dr["Totalprice"].ToString() != "" ? Convert.ToDecimal(dr["Totalprice"].ToString()) : 0;
                            totSale += dr["PayableAmt"].ToString() != "" ? Convert.ToDecimal(dr["PayableAmt"].ToString()) : 0;
                        prodQty += dr["Quantity"].ToString() != "" ? Convert.ToInt32(dr["Quantity"].ToString()) : 0;
                            totalgst += dr["TotalGst"].ToString() != "" ? Convert.ToDecimal(dr["TotalGst"].ToString()) : 0;

                        }


                        totSaving = totSale - totReg;

                        decimal Paid_amt = 0;
                        DataTable dt = new ConnectionClass().getPincodeBiseDeliveryCharges(3, null,Convert.ToString(totReg), "Proc_getPincodeBiseDeliveryCharges");
                        
                        decimal devivery_charge = 0;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            decimal.TryParse(Convert.ToString(dt.Rows[0]["Deliverycharge"]), out devivery_charge);
                        }

                        //totReg = totReg * Convert.ToInt32(prodQty);
                        //totSale = totSale * Convert.ToInt32(prodQty);
                        objp.todayPoAmt = totSaving.ToString("0.00");
                        objp.NetTotal = totReg;
                        objp.deliverycharges = devivery_charge;
                        objp.gstamount = totalgst;
                        Paid_amt = totReg + devivery_charge+ totalgst;
                        objp.PaidAmount = Paid_amt;
                    }

                }
                else if (!string.IsNullOrEmpty(IPAddress))
                {
                    string CustCode = IPAddress;
                    objp.dt2 = objL.getCartDetails(CustCode, "2");
                    decimal totReg = 0, totSale = 0, totSaving = 0;
                    int prodQty = 0;
                    if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in objp.dt2.Rows)
                        {
                            totReg += dr["Totalprice"].ToString() != "" ? Convert.ToDecimal(dr["Totalprice"].ToString()) : 0;
                            totSale += dr["PayableAmt"].ToString() != "" ? Convert.ToDecimal(dr["PayableAmt"].ToString()) : 0;
                            prodQty += dr["Quantity"].ToString() != "" ? Convert.ToInt32(dr["Quantity"].ToString()) : 0;
                        }
                        //totReg = totReg * Convert.ToInt32(prodQty);
                        //totSale = totSale * Convert.ToInt32(prodQty);
                        totSaving = totSale - totReg;
                        objp.todayPoAmt = totSaving.ToString("0.00");
                        objp.NetTotal = totReg;
                    }
                }
            }
            if (Session["mCode"] != null)
            {
                objp.CustomerId = Convert.ToString(Session["mCode"].ToString());
                objp.dt = objL.GetCustomerAddressDetail(objp.CustomerId);
            }
            return View(objp);
        }

        public JsonResult SendOTP(string MobileNo)
        {
            string st = "";
            if (!string.IsNullOrEmpty(MobileNo))
            {
                st = RandomCode();
                PropertyClass objp = new PropertyClass();
                objp.SendSMS(MobileNo, "Your OTP Verification Code is :" + st);
            }
            else
            {
                st = "0";
            }
            return Json(st, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckUserLogin(string MobileNo)
        {
            string st = "";

            if (!string.IsNullOrEmpty(MobileNo))
            {
                DataTable dt = objL.CheckuserLogin(MobileNo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    st = dt.Rows[0]["EmailAddress"].ToString();
                }
                else
                {
                    st = "0";
                }
            }
            else
            {
                st = "0";
            }
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SucessfullOrder(string OrderId)
        {
            try
            {
                if (!string.IsNullOrEmpty(OrderId))
                {
                    DataTable dt = objL.GetOrderConfirmDetail(OrderId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        objp.OrderId = dt.Rows[0]["OrderId"].ToString();
                        objp.PayMode = dt.Rows[0]["PaymentMode"].ToString();
                        objp.SSName = dt.Rows[0]["Name"].ToString();
                        objp.ContactNo = dt.Rows[0]["MobileNo"].ToString();
                        objp.OfferType = dt.Rows[0]["AddressType"].ToString();
                        objp.Address = dt.Rows[0]["Address"].ToString();
                        objp.PayableAmt = dt.Rows[0]["NetPayable"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["NetPayable"].ToString()) : 0;
                    }
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public string RandomCode()
        {
            string code = "";
            Random generator = new Random();
            code = generator.Next(0, 999999).ToString("D6");
            return code;

        }

        public JsonResult GetPromoCode(string PromoCode,decimal Orderamount )
        {
            string msg = "";
            PropertyClass model = new PropertyClass();
            try
            {
                model.HSNCode = PromoCode;
                model.Action = "1";
                model.Amount = Orderamount;
                DataTable dt = objL.GetPromoCode(model, "proc_GetPromoCode");
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["PromoCode"].ToString() != "")
                    {
                        model.RespoCode = "1";
                        model.ItemCode = dt.Rows[0]["PromoCode"].ToString();
                        model.OfferType = dt.Rows[0]["DiscountType"].ToString();
                        model.DiscPer = dt.Rows[0]["DiscountValue"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["DiscountValue"].ToString()) : 0;
                    }
                    else
                    {
                        model.RespoCode = "0";
                    }

                }
                else
                {
                    model.RespoCode = "0";
                }
            }
            catch (Exception ex)
            {
                model.RespoCode = "0";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        //Tanu Gupta Mycart

        public ActionResult MyCart(string cName, string pCode, string cId, string pQty, string cType, string AttrId, string varId)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString() != "4")
                {
                    return RedirectToAction("login", "CustomerAccount");
                }
            }
            else
            {
                return RedirectToAction("Index", "CustomerAccount");
            }


            objp.MainCategoryCode = cId;
            objp.productQty = pQty;
            ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
            if (cType == "p")
            {
                if (!string.IsNullOrEmpty(pCode))
                {
                    objp.ItemCode = pCode;
                    objp.Action = "1";
                    objp.VariationId = varId;
                    DataSet ds = objL.GetSingleProductDetail(objp, "proc_GetSingleProductView");
                    if (ds != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            objp.dt2 = ds.Tables[0];
                            decimal totReg = 0, totSale = 0, totSaving = 0;
                            if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                            {
                                foreach (DataRow dr in objp.dt2.Rows)
                                {
                                    totReg += dr["RegularPrice"].ToString() != "" ? Convert.ToDecimal(dr["RegularPrice"].ToString()) : 0;
                                    totSale += dr["SalePrice"].ToString() != "" ? Convert.ToDecimal(dr["SalePrice"].ToString()) : 0;
                                }
                                totReg = totReg * Convert.ToInt32(pQty);
                                totSale = totSale * Convert.ToInt32(pQty);
                                totSaving = totReg - totSale;
                                objp.todayPoAmt = totSaving.ToString("0.00");
                                objp.NetTotal = totSale;
                            }
                        }
                        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            objp.dt1 = ds.Tables[2];
                        }
                    }
                }
            }
            else
            {
                string kalashCCookie = "", IPAddress = null;
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
                    if ((!string.IsNullOrEmpty(IPAddress)))
                    {
                        objL.InsertTempCartToreal(Session["mCode"].ToString(), IPAddress);
                    }
                    string CustCode = Session["mCode"].ToString();
                    objp.dt2 = objL.getCartDetails(CustCode, "3");
                    TempData["Cart"] = objp.dt2.Rows.Count;
                    objp.dtcombooffer = objL.Proc_GetComboOffer(CustCode, "1");

                    decimal totReg = 0, totSale = 0, totSaving = 0, totalgst = 0;
                    int prodQty = 0;
                    if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in objp.dt2.Rows)
                        {
                            totReg += dr["Totalprice"].ToString() != "" ? Convert.ToDecimal(dr["Totalprice"].ToString()) : 0;
                            totSale += dr["PayableAmt"].ToString() != "" ? Convert.ToDecimal(dr["PayableAmt"].ToString()) : 0;
                            prodQty += dr["Quantity"].ToString() != "" ? Convert.ToInt32(dr["Quantity"].ToString()) : 0;
                            totalgst += dr["TotalGst"].ToString() != "" ? Convert.ToDecimal(dr["TotalGst"].ToString()) : 0;

                        }


                        totSaving = totSale - totReg;

                        decimal Paid_amt = 0;
                        DataTable dt = new ConnectionClass().getPincodeBiseDeliveryCharges(3, null, Convert.ToString(totReg), "Proc_getPincodeBiseDeliveryCharges");

                        decimal devivery_charge = 0;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            decimal.TryParse(Convert.ToString(dt.Rows[0]["Deliverycharge"]), out devivery_charge);
                        }

                        //totReg = totReg * Convert.ToInt32(prodQty);
                        //totSale = totSale * Convert.ToInt32(prodQty);
                        objp.todayPoAmt = totSaving.ToString("0.00");
                        objp.NetTotal = totReg;
                        objp.deliverycharges = devivery_charge;
                        objp.gstamount = totalgst;
                        Paid_amt = totReg + devivery_charge + totalgst;
                        objp.PaidAmount = Paid_amt;
                    }

                }
                else if (!string.IsNullOrEmpty(IPAddress))
                {
                    string CustCode = IPAddress;
                    objp.dt2 = objL.getCartDetails(CustCode, "2");
                    decimal totReg = 0, totSale = 0, totSaving = 0;
                    int prodQty = 0;
                    if (objp.dt2 != null && objp.dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in objp.dt2.Rows)
                        {
                            totReg += dr["Totalprice"].ToString() != "" ? Convert.ToDecimal(dr["Totalprice"].ToString()) : 0;
                            totSale += dr["PayableAmt"].ToString() != "" ? Convert.ToDecimal(dr["PayableAmt"].ToString()) : 0;
                            prodQty += dr["Quantity"].ToString() != "" ? Convert.ToInt32(dr["Quantity"].ToString()) : 0;
                        }
                        //totReg = totReg * Convert.ToInt32(prodQty);
                        //totSale = totSale * Convert.ToInt32(prodQty);
                        totSaving = totSale - totReg;
                        objp.todayPoAmt = totSaving.ToString("0.00");
                        objp.NetTotal = totReg;
                    }
                }
            }
            if (Session["mCode"] != null)
            {
                objp.CustomerId = Convert.ToString(Session["mCode"].ToString());
                objp.dt = objL.GetCustomerAddressDetail(objp.CustomerId);
            }
            return View(objp);
             
        }



    }
}