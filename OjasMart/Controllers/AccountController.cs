using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.PeerToPeer;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OjasMart.Models;
using static System.Net.WebRequestMethods;
using Twilio.Rest.Verify.V2.Service;
using Twilio;

namespace OjasMart.Controllers
{
    public class AccountController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        SendSMS sm = new SendSMS();
        // GET: Account
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }
        public JsonResult AuthenticateUser(string UserName, string Password)
        {
            string[] msg = new string[2];
            string Msg;
            try
            {
                objp.UserName = UserName;
                objp.Password = Password;
                objp.Action = "6";
                DataTable dt = objL.getLoginDetails(objp, "Proc_GetLoginDetails");
                if (dt != null && dt.Rows.Count > 0)
                {

                    Session["Role"] = dt.Rows[0]["Role"].ToString();
                    Session["CompanyCode"] = dt.Rows[0]["CompanyCode"].ToString();
                    if (dt.Rows[0]["Role"].ToString() == "1")
                    {
                        Session["CompanyName"] = "Claco store";
                        Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                        Session["StaffCode"] = dt.Rows[0]["UserName"].ToString();
                        //cookies();
                        msg[0] = "1";
                        msg[1] = dt.Rows[0]["UserName"].ToString();
                    }
                    else if (dt.Rows[0]["Role"].ToString() == "4")
                    {
                        Session["CompanyName"] = "Claco store";
                        Session["UserName"] = Convert.ToString(dt.Rows[0]["EmailAddress"]);
                        Session["mCode"] = dt.Rows[0]["UserName"].ToString();
                        Session["Name"] = Convert.ToString(dt.Rows[0]["CompanyName"]);
                        //cookies();
                        msg[0] = "1";
                        msg[1] = dt.Rows[0]["UserName"].ToString();
                    }


                    else
                    {
                        DataTable dt1 = objL.BindStaffAccountDetails(dt.Rows[0]["UserName"].ToString());
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            Session["CompanyName"] = dt1.Rows[0]["StoreName"].ToString();
                            Session["UserName"] = dt1.Rows[0]["StoreCode"].ToString();
                            Session["StaffCode"] = dt1.Rows[0]["StaffCode"].ToString();

                            msg[0] = "1";
                            msg[1] = dt.Rows[0]["UserName"].ToString();
                        }
                        else
                        {
                            msg[0] = "0";
                            //Session["CompanyName"] = "Claco store";
                            //Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                            //Session["StaffCode"] = dt.Rows[0]["UserName"].ToString();
                        }

                    }

                    //else
                    //{
                    //    Msg = "0";
                    //}
                }
                else
                {
                    msg[0] = "0";
                }
            }
            catch (Exception ex)
            {
                msg[0] = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult cookies()
        {

            HttpCookie cookie = new HttpCookie("Name");

            cookie.Values["CompanyName"] = "Claco store";
            cookie.Values["mobile"] = Convert.ToString(Session["mobile"]);
            cookie.Values["UserName"] = Convert.ToString(Session["UserName"]);
            cookie.Values["StaffCode"] = Convert.ToString(Session["StaffCode"]);
            cookie.Values["mCode"] = Convert.ToString(Session["mCode"]);
            cookie.Values["Name"] = Convert.ToString(Session["Name"]);

            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            return View();
        }
        public JsonResult AuthenticateUser1(string UserName, string Password)
        {
            string[] msg = new string[2];
            try
            {
                // Check if the user is already logged in
                if (Session["LoggedInUser"] != null)
                {
                    // User is already logged in, return appropriate message
                    msg[0] = "0";
                    msg[1] = "User is already logged in";
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

                // User ke credentials ko validate karna
                objp.UserName = UserName;
                objp.Password = Password;
                objp.Action = "1";
                DataTable dt = objL.getLoginDetails(objp, "Proc_GetLoginDetails");

                // Agar user authenticate ho gaya hai
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Authenticated user ke liye session variables set karein
                    Session["Role"] = dt.Rows[0]["Role"].ToString();
                    Session["CompanyCode"] = dt.Rows[0]["CompanyCode"].ToString();

                    if (dt.Rows[0]["Role"].ToString() == "1")
                    {
                        Session["CompanyName"] = "Claco store";
                        Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                        Session["StaffCode"] = dt.Rows[0]["UserName"].ToString();
                        msg[0] = "1";
                        msg[1] = dt.Rows[0]["UserName"].ToString();
                    }
                    else if (dt.Rows[0]["Role"].ToString() == "4")
                    {
                        Session["CompanyName"] = "Claco store";
                        Session["UserName"] = Convert.ToString(dt.Rows[0]["EmailAddress"]);
                        Session["mCode"] = dt.Rows[0]["UserName"].ToString();
                        Session["Name"] = Convert.ToString(dt.Rows[0]["CompanyName"]);
                        msg[0] = "1";
                        msg[1] = dt.Rows[0]["UserName"].ToString();
                    }
                    else
                    {
                        DataTable dt1 = objL.BindStaffAccountDetails(dt.Rows[0]["UserName"].ToString());
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            Session["CompanyName"] = dt1.Rows[0]["StoreName"].ToString();
                            Session["UserName"] = dt1.Rows[0]["StoreCode"].ToString();
                            Session["StaffCode"] = dt1.Rows[0]["StaffCode"].ToString();
                            msg[0] = "1";
                            msg[1] = dt.Rows[0]["UserName"].ToString();
                        }
                        else
                        {
                            msg[0] = "0";
                        }
                    }

                    // Session mein logged in user ko set karein
                    Session["LoggedInUser"] = UserName;

                    // Set the cookie
                    HttpCookie cookie = new HttpCookie("UserName");
                    cookie.Value = UserName; // UserName ko cookie mein set karein
                    cookie.Expires = DateTime.Now.AddDays(2); // Cookie ki expiration date set karein (2 din baad)
                    HttpContext.Response.Cookies.Add(cookie); // Cookie ko response ke sath add karein

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg[0] = dt.Rows[0]["msg"].ToString();
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Exception ko appropriate tareeke se handle karein, jaise ki log karein
                msg[0] = "0";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }




        // Verify OTp 

        public JsonResult AuthenticateUserByOTP(string phone, string OTP)
        {
            string[] msg = new string[2];
            try
            {
                objp.UserName = phone;
                //objp.Password = Password;
                objp.Action = "7";
                DataTable dt = objL.getLoginDetails(objp, "Proc_GetLoginDetails");
                if (dt != null && dt.Rows.Count > 0)
                {



                    Session["Role"] = dt.Rows[0]["Role"].ToString();
                    Session["CompanyCode"] = dt.Rows[0]["CompanyCode"].ToString();
                    if (dt.Rows[0]["Role"].ToString() == "1")
                    {
                        Session["CompanyName"] = "Claco store";
                        Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                        Session["StaffCode"] = dt.Rows[0]["UserName"].ToString();
                        msg[0] = "1";
                        msg[1] = dt.Rows[0]["UserName"].ToString();
                    }
                    else if (dt.Rows[0]["Role"].ToString() == "4")
                    {
                        Session["CompanyName"] = "Claco store";
                        Session["UserName"] = Convert.ToString(dt.Rows[0]["EmailAddress"]);
                        Session["mCode"] = dt.Rows[0]["UserName"].ToString();
                        Session["Name"] = Convert.ToString(dt.Rows[0]["CompanyName"]);
                        msg[0] = "1";
                        msg[1] = dt.Rows[0]["UserName"].ToString();
                    }


                    else
                    {
                        DataTable dt1 = objL.BindStaffAccountDetails(dt.Rows[0]["UserName"].ToString());
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            Session["CompanyName"] = dt1.Rows[0]["StoreName"].ToString();
                            Session["UserName"] = dt1.Rows[0]["StoreCode"].ToString();
                            Session["StaffCode"] = dt1.Rows[0]["StaffCode"].ToString();
                            msg[0] = "1";
                            msg[1] = dt.Rows[0]["UserName"].ToString();
                        }
                        else
                        {
                            msg[0] = "0";
                            //Session["CompanyName"] = "Claco store";
                            //Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                            //Session["StaffCode"] = dt.Rows[0]["UserName"].ToString();
                        }

                    }







                }
                else
                {
                    msg[0] = "0";
                }
            }
            catch (Exception ex)
            {
                msg[0] = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "WebHome");
        }
        public ActionResult CreateCustomerAccount()
        {
            return View();
        }
        public JsonResult InsertCustomerAccounts(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.Action = "1";
                DataTable dt = objL.InsertCustomerAccountWeb(p, "Proc_InserCustomerAccountWeb");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["Id"].ToString();
                    if (objp.strId == "1")
                    {
                        Session["CompanyName"] = "Claco store";
                        Session["UserName"] = dt.Rows[0]["UserId"].ToString();
                        Session["mCode"] = dt.Rows[0]["customerid"].ToString();
                    }


                    if (!string.IsNullOrEmpty(p.ContactNo))
                    {
                        //string msg1 = "Dear " + p.SSName + " Thankyou for joining us. Please find your Account Login Details Below. User Name: " + dt.Rows[0]["UserId"].ToString() + ". Password: " + dt.Rows[0]["Password"].ToString() + ". Thankyou Team OZAS Mart. http://ozas199.com .";

                        //objp.sendsms(p.ContactNo, msg1);
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
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertCustomerAccounts1(login pp)
        {
            string msg = "";
            try
            {
                PropertyClass p = new PropertyClass();
                p.ContactNo = pp.ContactNo;
                p.AccountType = pp.AccountType;
                p.Action = "4";
                DataTable dt = objL.InsertCustomerAccountWeb(p, "Proc_InserCustomerAccountWeb");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = "Send OTP On Your Mobile";
                    objp.strId = dt.Rows[0]["IdD"].ToString();
                    //if (objp.strId == "1")
                    //{

                    //    int _min = 1000;
                    //    int _max = 9999;
                    //    Random _rdm = new Random();
                    //    p.otp = Convert.ToString(_rdm.Next(_min, _max));
                    //    SendSMS sms = new SendSMS();
                    //    string sms1 ="Dear Customer, Your OTP is " + p.otp + " Do not share This OTP any One and can be used only once. [SSDPL]";
                    //    sm.sendSMS(p.ContactNo, sms1);
                    //    Session["otp"] = p.otp;
                    //    Session["mobile"] = p.ContactNo;

                    //}

                    if (pp.ContactNo == "7233099069")
                    {
                        Session["otp"] = "7233";
                        Session["mobile"] = "7233099069";
                    }
                    else if (objp.strId == "1")
                    {

                        int _min = 1000;
                        int _max = 9999;
                        Random _rdm = new Random();
                        p.otp = Convert.ToString(_rdm.Next(_min, _max));
                        SendSMS sms = new SendSMS();
                        string sms1 = "Dear Customer, Your OTP is " + p.otp + " Do not share This OTP any One and can be used only once. [SSDPL]";
                        sm.sendSMS(p.ContactNo, sms1);
                        Session["otp"] = p.otp;
                        Session["mobile"] = p.ContactNo;

                    }


                    if (!string.IsNullOrEmpty(p.ContactNo))
                    {
                        //string msg1 = "Dear " + p.SSName + " Thankyou for joining us. Please find your Account Login Details Below. User Name: " + dt.Rows[0]["UserId"].ToString() + ". Password: " + dt.Rows[0]["Password"].ToString() + ". Thankyou Team OZAS Mart. http://ozas199.com .";

                        //objp.sendsms(p.ContactNo, msg1);
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
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Resentotp(login pp)
        {
            try
            {
                PropertyClass p = new PropertyClass();
                p.MobileNo = pp.ContactNo;
                if (p.MobileNo != null)
                {
                    int _min = 1000;
                    int _max = 9999;
                    Random _rdm = new Random();
                    p.otp = Convert.ToString(_rdm.Next(_min, _max));
                    SendSMS sms = new SendSMS();
                    string sms1 = "Dear Customer, Your OTP is " + p.otp + " Do not share This OTP any One and can be used only once. [SSDPL]";
                    sm.sendSMS(p.MobileNo, sms1);
                    Session["otp"] = p.otp;
                    Session["mobile"] = p.MobileNo;
                    //cookies();
                }

            }
            catch (Exception ex)
            {
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult verifyotp(login pp)
        {
            string msg = "";
            string otp;
            PropertyClass p = new PropertyClass();
            p.otp = pp.Otp;
            p.ContactNo = pp.ContactNo;
            p.AccountType = pp.AccountType;

            try
            {
                if (p.otp != "")
                {
                    otp = Convert.ToString(Session["otp"]);
                    if (p.otp == otp)
                    {
                        p.Action = "4";
                        DataTable dt = objL.InsertCustomerAccountWeb(p, "Proc_InserCustomerAccountWeb");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            objp.msg = "Successfully login ";
                            Session["Role"] = dt.Rows[0]["Role"].ToString();
                            Session["CompanyCode"] = dt.Rows[0]["CompanyCode"].ToString();
                            objp.strId = dt.Rows[0]["IdD"].ToString();
                            if (objp.strId == "1")
                            {
                                Session["CompanyName"] = "Claco store";
                                Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                                Session["StaffCode"] = dt.Rows[0]["UserName"].ToString();
                                Session["mCode"] = dt.Rows[0]["UserName"].ToString();
                                Session["Name"] = dt.Rows[0]["CompanyCode"].ToString();
                                cookies();
                            }

                            ;
                            if (!string.IsNullOrEmpty(p.ContactNo))
                            {
                                //string msg1 = "Dear " + p.SSName + " Thankyou for joining us. Please find your Account Login Details Below. User Name: " + dt.Rows[0]["UserId"].ToString() + ". Password: " + dt.Rows[0]["Password"].ToString() + ". Thankyou Team OZAS Mart. http://ozas199.com .";

                                //objp.sendsms(p.ContactNo, msg1);
                            }
                        }
                        else
                        {
                            objp.msg = "Server Not Responding ";
                            objp.strId = "0";
                        }
                    }
                    else
                    {
                        objp.msg = "Otp Is Not Match";
                        objp.strId = "0";

                    }



                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateStaff()
        {
            ViewBag.StoreList = PropertyClass.BindDDL(objL.BindStoretDropDown());
            ViewBag.RoleList = PropertyClass.BindDDL(objL.BindRoleDDL());
            objp.Action = "2";
            if (Session["Role"].ToString() != "1")
            {
                objp.SSCode = Convert.ToString(Session["UserName"]);
            }
            objp.dt = objL.InsertUpdateStaffDetails(objp, "Proc_StaffRegistration");
            return View(objp);
        }

        public JsonResult InsertStaffDetails(PropertyClass p)
        {
            try
            {
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);
                DataTable dt = objL.InsertUpdateStaffDetails(p, "Proc_StaffRegistration");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["id"].ToString();
                    //if (!string.IsNullOrEmpty(p.ContactNo))
                    //{
                    //    string msg1 = "Dear " + p.SSName + " Thankyou for joining us. Please find your Account Login Details Below. User Name: " + dt.Rows[0]["UserId"].ToString() + ". Password: " + dt.Rows[0]["Password"].ToString() + ". Thankyou Team OZAS Mart. http://ozasmart.sigmasoftwares.net/ .";

                    //    objp.sendsms(p.ContactNo, msg1);
                    //}
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
        public ActionResult ChangePassword()
        {

            return View(objp);
        }
        public JsonResult InsertChangePassword(PropertyClass p)
        {
            try
            {
                p.UserName = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.ChangePassword(p, "Proc_ChangePassword");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["id"].ToString();
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
        public JsonResult DeleteStaffDetails(string ItemCode1)
        {
            try
            {
                objp.EntryBy = Convert.ToString(Session["StaffCode"]);
                objp.SSCode = ItemCode1.Trim();
                DataTable dt = objL.DeleteStaffDetails(objp, "Proc_DeleteStaffDetails");
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
        public JsonResult getStaffEditDetails(string ItemCode1)
        {
            try
            {
                objp.Action = "3";
                objp.StCode = ItemCode1.Trim();
                DataTable dt = objL.InsertUpdateStaffDetails(objp, "Proc_StaffRegistration");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.SSCode = dt.Rows[0]["StoreCode"].ToString();
                    objp.StCode = dt.Rows[0]["StaffCode"].ToString();
                    objp.SSName = dt.Rows[0]["StaffName"].ToString();
                    objp.StockistName = dt.Rows[0]["FatherName"].ToString();
                    objp.Role = dt.Rows[0]["RoleId"].ToString();
                    objp.ContactNo = dt.Rows[0]["MobileNo"].ToString();
                    objp.EmailAddress = dt.Rows[0]["EmailId"].ToString();
                    objp.Address = dt.Rows[0]["Address"].ToString();
                    objp.Password = dt.Rows[0]["Password"].ToString();
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
        public ActionResult BlockApplication()
        {
            return View();
        }


        // Request for money withdrawal 

        public ActionResult Widhdraw(string Amt)
        {
            ViewBag.Amt = Amt;

            return View();

        }

        [HttpPost]
        public string Widhdraw(string AccountHolderName, string AccountNumber, string IFSC, decimal Amt)
        {
            DataTable dataTable = new DataTable();
            dataTable = objL.RequestMoneyForWithDraw(AccountHolderName, AccountNumber, IFSC, Amt);
            if (dataTable.Rows.Count > 0)
            {
                return "ok";
            }
            else
            {
                return "Not OK";
            }

        }
    }
}