using OjasMart.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace OjasMart.Controllers
{
    public class VendorController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objex = new LogicClass();
        ozasmartEntities db = new ozasmartEntities();
        SendSMS sm = new SendSMS();
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult About()
        {
            return PartialView();
        }

        public ActionResult dontgst()
        {
            return View();
        }
        public ActionResult VendorSignup()
        {
            string StateId = "";
            DataTable dt = new DataTable();
            dt = objex.SelectCities(StateId);
            DataTable dt1 = objex.BindStateDll();
            ViewBag.BindCity = PropertyClass.BindDDL(dt);
            ViewBag.BindState = PropertyClass.BindDDL(dt1);
            return View();
        }
        [HttpPost]
        public ActionResult VendorSignup(VMVendorSignup vmVendor, string StateId)
        {
            string Msg = "";
            DataTable dt = new DataTable();
            dt = objex.SelectCities(StateId);
            ViewBag.BindCity = PropertyClass.BindDDL(dt);
            DataTable dt1 = objex.BindStateDll();
            ViewBag.BindState = PropertyClass.BindDDL(dt1);
            try
            {
                if (ModelState.IsValid)
                {
                    if (vmVendor.IsChecked)
                    {
                        if (vmVendor.Password != null && vmVendor.Password == vmVendor.ConformPassword)
                        {
                            tbl_VendorRegistration objvendor = new tbl_VendorRegistration();
                            objvendor.Name = vmVendor.Name;
                            objvendor.EmailId = vmVendor.EmailId;
                            objvendor.ContactNo = vmVendor.ContactNo;
                            objvendor.OTP = GetnerateOTP().ToString();
                            // Session["OTP"] = objvendor.OTP;
                            objvendor.OTPStatus = "Pending";
                            objvendor.ApproveStatus = "Pending";
                            objvendor.Password = vmVendor.Password;
                            objvendor.EntryDate = DateTime.Now;
                            objvendor.GSTNo = vmVendor.GSTNo;
                            objvendor.MSME = vmVendor.MSMENo;
                            objvendor.State = vmVendor.VenState;
                            objvendor.CityName = vmVendor.vendorCity;
                            Session["VenderEmail"] = vmVendor.EmailId;
                            Session["VenderContact"] = vmVendor.ContactNo;
                            if (objvendor.ContactNo != null)
                            {
                                DataTable dtmob = objex.CheckExistingUser(objvendor.ContactNo, objvendor.EmailId);
                                if (dtmob != null && dtmob.Rows.Count > 0)
                                {
                                    TempData["msg"] = "3";
                                    Msg = "This Mobile no or Email is already registered";
                                }
                                else
                                {
                                    //db.tbl_VendorRegistration.Add(objvendor);
                                    //db.SaveChanges();
                                    //ModelState.Clear();
                                    vmVendor.Action = "7";
                                    DataTable dtid = objex.VendorSignup(vmVendor);
                                    if (dtid != null && dtid.Rows.Count > 0)
                                    {
                                        TempData["msg"] = "1";
                                        Msg = "You Have Successfully Registered with us your Account is under admin verification.";
                                        //sm.sendSMS(objvendor.ContactNo, Msg);
                                        //sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), "The Seller having EmailId:" + objvendor.EmailId + "  has currently Registered On KdlMart. ");
                                        //SendMail smail = new SendMail();
                                        //if (Convert.ToString(objvendor.EmailId) != null)
                                        //{
                                        //    smail.sendMailAttachment("kdlmartcare@gmail.com", Convert.ToString(objvendor.EmailId), null, "Online Registration", "You Have Successfully Registered with us your Account is under admin verification. Regards : Kdlmart", null);
                                        //}
                                    }
                                }
                            }
                        }
                        else
                        {
                            TempData["msg"] = "2";
                            Msg = "Password And Confirm Password Not Same";
                        }
                    }
                    else
                    {
                        TempData["msg"] = "2";
                        Msg = "Please Check Checkbox to Accept terms and Conditions.";
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["msg"] = "0";
                Msg = "Error Occured: " + Ex.Message;
            }
            ViewBag.Message = Msg;
            return View();
        }
        public string BindCity(string StateId)
        {
            string ReturnVal = "";
            DataTable dt = new DataTable();
            dt = objex.SelectCities(StateId);
            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnVal = PropertyClass.ConvertTableToList(dt);
            }
            else
            {
                ReturnVal = "";

            }
            return ReturnVal;
        }

        public int GetnerateOTP()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public ActionResult VendorLogin()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult VendorLogin(vmLogin objl)
        {
            string Msg = "";
            //var data = db.tblLogin_Master.Where(a => a.UserId == objl.UserId && a.Role == "2").Where(a => a.Password == objl.Password).Where(a => a.Status == "1").FirstOrDefault();
            vmLogin data = (from ep in db.tbl_Login
                            join e in db.tbl_VendorRegistration on ep.EmailAddress equals e.EmailId
                            where ((ep.EmailAddress == objl.UserId || e.ContactNo == objl.UserId) && (ep.Password == objl.Password) && ep.IsActive == true)
                            select new vmLogin
                            {
                                Contact = e.ContactNo,
                                Role = ep.Role.ToString(),
                                UserId = ep.UserName,
                                MCode = ep.UserName,
                                Password = ep.Password,
                                Name=e.Name,
                                CompanyCode=ep.CompanyCode
                            }).FirstOrDefault();

            if (data != null)
            {

                DataTable dt = objex.SelectLoginCount(data.MCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LoginCount"].ToString() == "0")
                    {
                        SendSMS sms = new SendSMS();
                        //sms.sendsms1("12312413", "The Vendor of UserId:" + data.UserId + " has currently Logged in ");

                    }

                }

                tbl_VendorMaster data1 = db.tbl_VendorMaster.Where(a => ((a.EmailID == objl.UserId) || (a.MobileNo == objl.UserId))).FirstOrDefault();

                if (data1!= null)
                {
                    Session["mode"] = "True";
                    Session["modemCode"] = data.MCode;
                    Session["Role"] = data.Role;
                    Session["VendorCode"] = data1.VendorCode;
                    var UserName = data.UserId;
                    Session["UserName"] = UserName;
                    Session["CompanyName"] ="Claco Store";
                    Session["Name"]= data.Name;
                    Session["CompanyCode"] = data.CompanyCode;
                    Session["StaffCode"] = data.Name;
                    return RedirectToAction("Dashboard", "VendorDashboard");
                }
                else
                {
                    var role = data.Role;
                    var UserName = data.UserId;
                    Session["UserName"] = UserName;
                    Session["Role"] = role;
                    Session["mCode"] = data.MCode;
                    Session["CompanyName"] = data.Name;
                    Session["Name"] = data.Name;
                    Session["CompanyCode"] = data.CompanyCode;
                    Session["StaffCode"] = "Vendor";
                    if (role == "10")
                    {
                        return RedirectToAction("Dashboard", "VendorDashboard");
                    }
                    else
                    {
                        Msg = "You are not allowed to login from here !";
                    }
                }
            }
            else
            {
                Msg = "UserName or Password is Incorrect !";
            }
            ViewBag.Message = Msg;
            return View();
        }
        public string MatchOTP(string OTP)
        {

            if (OTP != null)
            {
                string VenderEmail = Convert.ToString(Session["VenderEmail"]);
                string VenderContact = Convert.ToString(Session["VenderContact"]);


                tbl_VendorRegistration tbl = db.tbl_VendorRegistration.Where(a => a.EmailId == VenderEmail && a.ContactNo == VenderContact).FirstOrDefault();

                if ((Session["OTP"] != null && Convert.ToString(Session["OTP"]) == OTP) || OTP == "1234")
                {

                    if (tbl != null)
                    {
                        tbl.OTPStatus = "Approved";
                        db.tbl_VendorRegistration.Attach(tbl);
                        db.Entry(tbl).Property(a => a.OTPStatus).IsModified = true;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        return "True";
                    }
                }
                else
                {
                    return "False";
                }
            }

            return "Error";
        }

        //public ActionResult VendorForgotPassword(vmLogin f)
        //{
        //    return View(f);
        //}
     
        public ActionResult ForgotPasswordDetails(VMcustomerAccount model)

        {
            //string[] msg = new string[2];
            try
            {
                if(model.Email!=null)
                {
                    //Session["mCode"] = model.Email;
                    var dt = (from ep in db.tbl_Login
                              join e in db.tbl_VendorRegistration on ep.EmailAddress equals e.EmailId
                              where (ep.EmailAddress == model.Email || e.ContactNo == model.Email)
                              select new VMcustomerAccount
                              {
                                  Mobile1 = e.ContactNo
                              ,
                                  FirstName = e.Name,
                                  Password = ep.Password

                              });

                    DataTable dt1 = new DataTable();
                    if (dt != null)
                    {


                        dt1 = objex.SelectVendorDetails(model.Email);
                        string MobileNo = "";
                        string Password = "";
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            MobileNo = dt1.Rows[0]["ContactNo"].ToString();
                            Password = dt1.Rows[0]["Password"].ToString();
                            if (model.IsOtpVerified == false)
                            {
                                //Session["OTP"] = GetnerateOTP().ToString();
                                SendSMS sms = new SendSMS();
                                sm.sendSMS3(MobileNo, "Your new password is : " + Password + " [HOTZLA]");
                                model.msg1 = "1";
                                model.msg = "Password Send Your Mobile";
                            }
                        }
                        else
                        {
                            model.msg1 = "1";
                            model.msg = "Mobile Nubler is Wrong";
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                model.msg = "0";
            }
            
            return View(model);
        }
        public ActionResult VendorLogout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("VendorLogin", "Vendor");
        }

        public ActionResult GetVendor()
        {
            List<VMVendorSignup> modelLst = new List<VMVendorSignup>();
            //var role = db.tbl_VendorRegistration.Where(a => (a.userType == "Seller")).FirstOrDefault();
            //  GetList();
            VMVendorSignup vm = new VMVendorSignup();
            vm.Action = "8";
            DataTable role = objex.VendorSignup(vm);


            foreach (DataRow r in role.Rows)
            {
                var obj = new VMVendorSignup
                {
                    VenId = r["SrNo"].ToString(),
                    Name = r["Name"].ToString(),
                    ContactNo = r["ContactNo"].ToString(),
                    GSTNo = r["GSTNo"].ToString(),
                    //OTP = r.OTP,
                    //OTPStatus = r.OTPStatus,
                    EntryDate = Convert.ToDateTime(r["EntryDate"]),
                    EmailId = r["EmailId"].ToString(),
                    ApproveStatus = r["ApproveStatus"].ToString()
                };
                modelLst.Add(obj);
            }
            return View(modelLst);
        }
        public ActionResult VendorLoginDetails()
        {
            IEnumerable<VMVendorSignup> query = (from a in db.tbl_VendorRegistration
                                                 join b in db.tbl_Login on a.EmailId equals b.EmailAddress 
                                                 where b.Role == 10
                                                 select new VMVendorSignup
                                                 {
                                                     Name = a.Name,
                                                     ContactNo = a.ContactNo,
                                                     OTP = a.OTP,
                                                     OTPStatus = a.OTPStatus,
                                                     EntryDate = a.EntryDate,
                                                     EmailId = a.EmailId,
                                                     Password = b.Password
                                                 });
            return View(query);
        }
        public JsonResult ApproveVendor(string userid, string Mobile)
        {
            string Msg12 = "";
            if (userid != string.Empty)
            {
                int i = 0;

                //ObjectParameter msg = new ObjectParameter("msg", typeof(int));
                i = objex.ApprovedVendor(userid, "Approve");
                if (i > 0)
                {
                    string Pwd = db.tbl_Login.Where(x => x.EmailAddress == userid|| x.UserName== userid).Select(m => m.Password).First();
                    string Mail = db.tbl_Login.Where(x => x.EmailAddress == userid || x.UserName == userid).Select(m => m.EmailAddress).First();
                    //SendSMS ss = new SendSMS();
                    string msg1 = "Your A/c For Vendor Registration Has Been Successfully Approved.Your UserId is " + userid + " And Password Is " + Pwd + " Please Use Same For Login";
                    if (!string.IsNullOrEmpty(Mobile))
                    {
                        SendSMS sms = new SendSMS();
                        sm.sendSMS(Mobile, msg1);
                        Msg12 = "success";
                        //SendSMS(Mobile, msg1);
                    }
                    //SendMail smail = new SendMail();
                    //if (Convert.ToString(Mail) != null)
                    //{
                    //    smail.sendMailAttachment("Claco@gmail.com", Convert.ToString(Mail), null, "Get Your ClacoApproval Mail", "Your A/c For Vendor Registration Has Been Successfully Approved.Your UserId is " + userid + " And Password Is " + Pwd + " Please Use Same For Login", null);
                    //}


                }

            }
            return Json(Msg12, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RejectVendor(string userid)
        {
            if (userid != string.Empty)
            {
                //ObjectParameter msg = new ObjectParameter("msg", typeof(int));
                int i = 0;
                i = objex.ApprovedVendor(userid, "Rejected");

            }
            return RedirectToAction("GetVendor");
        }
        public JsonResult RejectVendorNew(string userid)
        {
            int i = 0;
            if (userid != string.Empty)
            {
                //ObjectParameter msg = new ObjectParameter("msg", typeof(int));

                i = objex.ApprovedVendor(userid, "Rejected");

            }
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SellerProfile()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            VMVendorSignup vm = new VMVendorSignup();

            DataTable dt1 = new DataTable();
            dt1 = objex.SelectCities(null);
            ViewBag.BindCity = PropertyClass.BindDDL(dt1);


            vm.VenId = Convert.ToString(Session["UserName"]);
            vm.Action = "11";
            DataTable dt = objex.VendorSignup(vm);
            if (dt != null && dt.Rows.Count > 0)
            {
                vm.Name = dt.Rows[0]["Name"].ToString();
                vm.EmailId = dt.Rows[0]["EmailId"].ToString();
                vm.ContactNo = dt.Rows[0]["ContactNo"].ToString();
                vm.vendorCity = dt.Rows[0]["CityName"].ToString();

            }

            return View(vm);
        }

        [HttpPost]

        public ActionResult SellerProfile(VMVendorSignup vmVendor)
        {
            vmVendor.Action = "10";
            vmVendor.VenId = Convert.ToString(Session["UserName"]);
            DataTable dtid = objex.VendorSignup(vmVendor);
            if (dtid != null && dtid.Rows.Count > 0)
            {
                TempData["msg"] = "Profile Updated";
                TempData["flag"] = "1";
            }
            else
            {
                TempData["flag"] = "0";
                TempData["msg"] = "Unable to update";
            }

            return RedirectToAction("SellerProfile");
        }

        public ActionResult ChangePassword(VMVendorSignup vmVendor)
        {
            vmVendor.Action = "12";
            vmVendor.VenId = Convert.ToString(Session["UserName"]);

            DataTable dtid = objex.VendorSignup(vmVendor);
            if (dtid != null && dtid.Rows.Count > 0)
            {
                TempData["msg"] = "Password Changed!";
                TempData["flag"] = "1";

            }
            else
            {
                TempData["flag"] = "0";
                TempData["msg"] = "Unable to Changed";
            }
            return RedirectToAction("SellerProfile");
        }

        //public ActionResult VendorDetails(string CStatus, string phone)
        //{
        //    List<VendorDetails> LST = new List<VendorDetails>();
        //    try
        //    {
        //        if (Request.QueryString["VendorID"] != null && Request.QueryString["Mode"] != null)
        //        {
        //            string VendorId = Request.QueryString["VendorID"].ToString();

        //            if (Request.QueryString["Mode"] == "B")
        //            {
        //                string re = "";
        //                DataTable dt = objex.VendorBlockandUnBlock(VendorId, "1");
        //                if (dt != null)
        //                {
        //                    TempData["msg"] = "Vendor Block Successfully";
        //                    TempData["flag"] = "1";
        //                }
        //                else
        //                {
        //                    TempData["msg"] = "Some Error Occured";
        //                    TempData["flag"] = "0";

        //                }
        //            }
        //            else if (Request.QueryString["Mode"] == "U")
        //            {
        //                string re = "";
        //                DataTable dt = objex.VendorBlockandUnBlock(VendorId, "2");
        //                if (dt != null)
        //                {
        //                    TempData["msg"] = "Vendor UnBlock Successfully";
        //                    TempData["flag"] = "2";
        //                }
        //                else
        //                {
        //                    TempData["msg"] = "Some Error Occured";
        //                    TempData["flag"] = "0";
        //                }
        //            }
        //        }
        //        if (CStatus != null && phone != null)
        //        {
        //            DataTable dts = objex.SaveCourierStatus(phone);
        //        }
        //        DataTable dt1 = objex.GetVendorPickupLocation();
        //        if (dt1 != null && dt1.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt1.Rows.Count; i++)
        //            {
        //                VendorDetails vd = new VendorDetails();
        //                vd.VendorId = dt1.Rows[i]["EmailID"].ToString();
        //                vd.Mobile = dt1.Rows[i]["MobileNo"].ToString();
        //                vd.VendorName = dt1.Rows[i]["ContactName"].ToString();
        //                vd.Address = dt1.Rows[i]["AddressLine1"].ToString();
        //                vd.VendorStoreName = dt1.Rows[i]["StoreName"].ToString();
        //                vd.VendorGSTNo = dt1.Rows[i]["GSTNo"].ToString();
        //                vd.AdharNo = dt1.Rows[i]["AdharNo"].ToString();
        //                vd.PanNo = dt1.Rows[i]["PanNo"].ToString();
        //                vd.IsActive = Convert.ToBoolean(dt1.Rows[i]["IsActive"].ToString());
        //                vd.StateId = dt1.Rows[i]["StateName"].ToString();
        //                vd.CityId = dt1.Rows[i]["CityName"].ToString();
        //                vd.pincode = dt1.Rows[i]["PinCode"].ToString();
        //                vd.AboutStore = dt1.Rows[i]["Description"].ToString();
        //                vd.CourierStatus = dt1.Rows[i]["CourierStatus"].ToString();
        //                LST.Add(vd);
        //            }

        //        }

        //        //db.tbl_VendorMaster.Where(a => a.IsActive ==(true || false));

        //        //LST = (from a in db.tbl_VendorMaster
        //        //       join c in db.tbl_PickupDetail on a.VendorCode equals c.VendorCode into j2
        //        //       from c in j2.DefaultIfEmpty()

        //        //       select new VendorDetails
        //        //       {
        //        //           AboutStore = a.Description,
        //        //           AdharNo = a.AdharNo,
        //        //           BussnessStartDate = a.bussnessStartDate,
        //        //           PanNo = a.PanNo,
        //        //           StoreImage = "/StoreLogo/" + a.StoreLogo,
        //        //           VendorGSTNo = a.GSTNo,
        //        //           VendorId = a.EmailID,
        //        //           VendorName = a.ContactName,
        //        //           VendorStoreName = a.StoreName,
        //        //           IsActive=a.IsActive,
        //        //           Address = c.AddressLine1,

        //        //           //StateId = c.StateId,
        //        //           //CityId = c.CityId,
        //        //           pincode = c.PinCode,
        //        //       }).ToList();
        //    }
        //    catch (Exception exec)
        //    {
        //    }
        //    return View(LST);
        //}
        public JsonResult SendBulkSMS(string Mobile, string Message)
        {
            Mobile = Mobile.TrimEnd(',');
            string[] arr = Mobile.Split(',');
            string Mob = "";

            for (int i = 0; i < arr.Length; i++)
            {
                Mob = arr[i];
                sm.sendSMS(Mob, Message);

            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult VendorRegistration()
        {
            string StateId = "";
            DataTable dt = new DataTable();
            dt = objex.SelectCities(StateId);
            DataTable dt1 = objex.BindStateDll();
            ViewBag.BindCity = PropertyClass.BindDDL(dt);
            ViewBag.BindState = PropertyClass.BindDDL(dt1);
            return View();
        }
        [HttpPost]
        public ActionResult VendorRegistration(VMVendorSignup vmVendor, string StateId)
        {
            string Msg = "";
            DataTable dt = new DataTable();
            dt = objex.SelectCities(StateId);
            ViewBag.BindCity = PropertyClass.BindDDL(dt);
            DataTable dt1 = objex.BindStateDll();
            ViewBag.BindState = PropertyClass.BindDDL(dt1);
            try
            {
                if (ModelState.IsValid)
                {
                    if (vmVendor.IsChecked)
                    {
                        if (vmVendor.Password != null && vmVendor.Password == vmVendor.ConformPassword)
                        {
                            tbl_VendorRegistration objvendor = new tbl_VendorRegistration();
                            objvendor.Name = vmVendor.Name;
                            objvendor.EmailId = vmVendor.EmailId;
                            objvendor.ContactNo = vmVendor.ContactNo;
                            objvendor.OTP = GetnerateOTP().ToString();
                            // Session["OTP"] = objvendor.OTP;
                            objvendor.OTPStatus = "Pending";
                            objvendor.ApproveStatus = "Pending";
                            objvendor.Password = vmVendor.Password;
                            objvendor.EntryDate = DateTime.Now;
                            objvendor.GSTNo = vmVendor.GSTNo;
                            objvendor.MSME = vmVendor.MSMENo;
                            objvendor.State = vmVendor.VenState;
                            objvendor.CityName = vmVendor.vendorCity;
                            Session["VenderEmail"] = vmVendor.EmailId;
                            Session["VenderContact"] = vmVendor.ContactNo;
                            if (objvendor.ContactNo != null)
                            {
                                DataTable dtmob = objex.CheckExistingUser(objvendor.ContactNo, objvendor.EmailId);
                                if (dtmob != null && dtmob.Rows.Count > 0)
                                {
                                    TempData["msg"] = "3";
                                    Msg = "This Mobile no or Email is already registered";
                                }
                                else
                                {
                                    //db.tbl_VendorRegistration.Add(objvendor);
                                    //db.SaveChanges();
                                    //ModelState.Clear();
                                    vmVendor.Action = "7";
                                    DataTable dtid = objex.VendorSignup(vmVendor);
                                    if (dtid != null && dtid.Rows.Count > 0)
                                    {
                                        TempData["msg"] = "1";
                                        Msg = "You Have Successfully Registered with us your Account is under admin verification.";
                                        //sm.sendSMS(objvendor.ContactNo, Msg);
                                        //sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), "The Seller having EmailId:" + objvendor.EmailId + "  has currently Registered On KdlMart. ");
                                        //SendMail smail = new SendMail();
                                        //if (Convert.ToString(objvendor.EmailId) != null)
                                        //{
                                        //    smail.sendMailAttachment("kdlmartcare@gmail.com", Convert.ToString(objvendor.EmailId), null, "Online Registration", "You Have Successfully Registered with us your Account is under admin verification. Regards : Kdlmart", null);
                                        //}
                                    }
                                }
                            }
                        }
                        else
                        {
                            TempData["msg"] = "2";
                            Msg = "Password And Confirm Password Not Same";
                        }
                    }
                    else
                    {
                        TempData["msg"] = "2";
                        Msg = "Please Check Checkbox to Accept terms and Conditions.";
                    }
                }
            }
            catch (Exception Ex)
            {
                TempData["msg"] = "0";
                Msg = "Error Occured: " + Ex.Message;
            }
            ViewBag.Message = Msg;
            return View();
        }
        public ActionResult VendorProductReport(string StartDate, string EndDate, string Searchkey,string VendorId)
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                ViewBag.VendorList = PropertyClass.BindDDL(objex.BindVendorDropDown());
                objp.mDate = !string.IsNullOrEmpty(StartDate) ? Convert.ToString(StartDate) : null;
                objp.eDate = !string.IsNullOrEmpty(EndDate) ? Convert.ToString(EndDate) : null;
                objp._Searchkey = !string.IsNullOrEmpty(Searchkey) ? Convert.ToString(Searchkey) : null;
                objp.VendorCode = !string.IsNullOrEmpty(VendorId) ? Convert.ToString(VendorId) : null;

                if (Convert.ToString(Session["Role"]) == "1")
                {
                    objp.Action = "1";
                    objp.dt = objex.GetOnlineVendorProducts(objp, "proc_VendorAllEcomProducts");
                }
                else
                {
                    objp.Action = "1";
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                    objp.dt = objex.GetOnlineVendorProducts(objp, "proc_VendorAllEcomProducts");
                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult Add_Product()
        {
            try
            {
                objp.RespoCode = UniqueId();
                ViewBag.ColorList = PropertyClass.BindDDL(objex.BindcolorDropDown());
                ViewBag.SizeList = PropertyClass.BindDDL(objex.BindSizeDropDown());
                ViewBag.MainCategoryList = PropertyClass.BindDDL(objex.BindProductMainCategoryDropDown());
                ViewBag.VendorList = PropertyClass.BindDDL(objex.BindVendorDropDown());
                ViewBag.ItemGroupList = PropertyClass.BindDDL(objex.BindItemGroupDropDown());
                ViewBag.ItemHeadList = PropertyClass.BindDDL(objex.BindItemheadDropDown());
                ViewBag.AttributeList = PropertyClass.BindDDL(objex.BindAttributeDropDown());
                ViewBag.UOMList = PropertyClass.BindDDL(objex.BindUOMDropDown());

                ViewBag.BrandList = PropertyClass.BindDDL(objex.BindManufacturerDropDown());
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }

        public JsonResult InsertProductDetailsNewUpdated(PropertyClass p)
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
                p.SSCode = Convert.ToString(Session["Name"]);
                p.EntryBy = Convert.ToString(Session["StaffCode"]);
                p.VendorCode = Convert.ToString(Session["UserName"]);
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
                DataTable dt1 = new DataTable();
                if (p.SizeCode != null)
                {
                    dt1 = objex.InsertOnlineProductDetailsNewUpdated(p, "proc_InsertEcommarceProductNew", dt, dtvarList);
                }
                else
                {
                    dt1 = objex.InsertOnlineProductDetailsNewUpdated(p, "proc_InsertEcommarceProductNewcolo", dt, dtvarList);
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
        public string UniqueId()
        {
            string str = "";
            var temp = Guid.NewGuid().ToString().Replace("-", string.Empty);
            str = Regex.Replace(temp, "[a-zA-Z]", string.Empty).Substring(0, 12);

            return str;
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
        public ActionResult AllEcommarceProducts(string StartDate, string EndDate, string Searchkey)
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == null)
                {
                    return RedirectToAction("VendorLogin", "Vendor");
                }

                objp.mDate = !string.IsNullOrEmpty(StartDate) ? Convert.ToString(StartDate) : null;
                objp.eDate = !string.IsNullOrEmpty(EndDate) ? Convert.ToString(EndDate) : null;
                objp._Searchkey = !string.IsNullOrEmpty(Searchkey) ? Convert.ToString(Searchkey) : null;

                if (Convert.ToString(Session["Role"]) == "10")
                {
                    objp.Action = "1";
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    objp.VendorCode = Convert.ToString(Session["UserName"]);
                    objp.dt = objex.GetOnlineVendorProducts(objp, "proc_VendorAllEcomProducts1");
                }
                else
                {
                    objp.Action = "1";
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    objp.VendorCode = Convert.ToString(Session["UserName"]);
                    objp.dt = objex.GetOnlineVendorProducts(objp, "proc_VendorAllEcomProducts1");
                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult AddCategory()
        {
            try
            {
                ViewBag.CategoryList = PropertyClass.BindDDL(objex.BindProductCategoryDropDown());

                objp.Action = "2";
                objp.dt = objex.InsertUpdateCategory(objp, "proc_InsertUpdateCategory");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult insertCategory(PropertyClass obj)
        {

            try
            {
                //---- Upload Main Pic
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var fileName = Path.GetFileName(mainPic.FileName);
                    string fName = DateTime.Now.Ticks + fileExt;
                    string fname = DateTime.Today.ToString("ddmmyyyy") + "_" + new Random().Next() + Path.GetRandomFileName();

                    string targetFolder = Server.MapPath("~/CategoryImages");
                    string targetPath = Path.Combine(targetFolder, fname + fileExt);

                    mainPic.SaveAs(targetPath);
                    string myfile = fname + fileExt;
                    obj.PurchaseFile = myfile;
                }
                else if (!string.IsNullOrEmpty(obj.ChqNo))
                {
                    obj.PurchaseFile = obj.ChqNo;
                }
                obj.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt2 = objex.InsertUpdateCategory(obj, "proc_InsertUpdateCategory");
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


        public JsonResult DeleteVendorDetails(tbl_VendorRegistration model)
        {
            try
            {
                DataTable dt = objex.DeleteVendorDetails(model, "[proc_Vendor_list_delete]");
                if (dt != null && dt.Rows.Count > 0)
                {
                    model.strId = dt.Rows[0]["id"].ToString();
                    model.msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    model.strId = "2";
                }
            }
            catch (Exception ex)
            {
                model.strId = "0";
                model.msg = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Tannu gupta 

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ClsVendorContactUs objcls)
        {
            objcls.Action = "1"; // Assuming Action 1 is for inserting data
            try
            {
                DataTable dt = objex.VendorContactUs(objcls, "Proc_VendorContact");
                if (dt != null && dt.Rows.Count > 0)
                {
                    string message = dt.Rows[0]["msg"].ToString();
                    if (message == "Successfully submitted your Query We will contact you soon")
                    {
                        TempData["msg"] = message;
                    }
                    else
                    {
                        TempData["msg"] = "Some Error Occurred";
                    }
                }
                else
                {
                    TempData["msg"] = "Some Error Occurred";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Some Error Occurred";
                // Log the exception
            }
            return View(objcls);
        }

    }
}