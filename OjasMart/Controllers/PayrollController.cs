using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OjasMart.Models;
using System.IO;
using System.Configuration;

namespace OjasMart.Controllers
{
    public class PayrollController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: Payroll
        public static string marchantkey = ConfigurationManager.AppSettings["MerchantKey"].ToString();
        public static string marchantId = ConfigurationManager.AppSettings["MerchantID"].ToString();


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PaytmRequest r)
        {
            String callbackUrl = ConfigurationManager.AppSettings["CallBackUrl"].ToString();
            Dictionary<String, String> paytmParams = new Dictionary<String, String>();
            paytmParams.Add("MID", marchantId);
            paytmParams.Add("CHANNEL_ID", ConfigurationManager.AppSettings["ChannelID"].ToString());
            paytmParams.Add("WEBSITE", ConfigurationManager.AppSettings["Website"].ToString());
            paytmParams.Add("CUST_ID", r.Cust_Id);
            paytmParams.Add("MOBILE_NO", r.Mobile);
            paytmParams.Add("EMAIL", r.Email);
            paytmParams.Add("ORDER_ID", r.OrderId);
            paytmParams.Add("INDUSTRY_TYPE_ID", ConfigurationManager.AppSettings["IndustryType"].ToString());
            paytmParams.Add("TXN_AMOUNT", r.Amount);
            paytmParams.Add("CALLBACK_URL", callbackUrl);

            string paytmChecksum = Paytm.Checksum.generateSignature(paytmParams, marchantkey);
            // string transactionURL = ConfigurationManager.AppSettings["transactionURL"].ToString();
            // string transactionURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + r.OrderId;
            string transactionURL = "https://securegw.paytm.in//theia/processTransaction?orderid=" + r.OrderId;
            try
            {
                string outputHTML = "<html>";
                outputHTML += ("<head>");
                outputHTML += ("<title>Merchant Checkout Page</title>");
                outputHTML += ("</head>");
                outputHTML += ("<body>");
                outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
                outputHTML += "<form method='post' action='" + transactionURL + "' name='f1'>";
                outputHTML += "<table border='1'>";
                outputHTML += "<tbody>";
                foreach (string key in paytmParams.Keys)
                {
                    outputHTML += "<input type='hidden' name='" + key + "' value='" + paytmParams[key] + "'>'";
                }
                outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + paytmChecksum + "'>";
                outputHTML += "</tbody>";
                outputHTML += "</table>";
                outputHTML += "<script type='text/javascript'>";
                outputHTML += "document.f1.submit();";
                outputHTML += "</script>";
                outputHTML += "</form>";
                outputHTML += "</body>";
                outputHTML += "</html>";
                ViewBag.list = outputHTML;
                return View("PaymentPage");
            }
            catch (Exception ex)
            {
                Response.Write("Exception message: " + ex.Message.ToString());
            }
            return View();
        }
        public ActionResult PaymentPage()
        {
            return View();
        }
        public ActionResult PaymentResponse()
        {
            string Msg12 = "";
          //  BussinessLayer bus = new BussinessLayer();
            PaymentResponse1 r = new PaymentResponse1();
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                foreach (string key in Request.Form.Keys)
                {
                    parameters.Add(key.Trim(), Request.Form[key].Trim());
                }
                DataTable dtNew = new DataTable();
                r.FormData = Request.Form.ToString();

              //  dtNew = bus.InsertRequestForm(r, "Proc_paymentKeyByOrderId");

                if (Convert.ToString(parameters["STATUS"]) == "TXN_SUCCESS")
                {
                    r.TXNID = parameters["TXNID"];
                    r.GATEWAYNAME = parameters["GATEWAYNAME"];
                    r.BANKNAME = parameters["BANKNAME"];
                }
                else
                {
                    r.BANKNAME = "Not Found..";
                    r.TXNID = "Not Found";
                    r.GATEWAYNAME = "Not Found";
                }

                r.Action = "1";
                if (!string.IsNullOrEmpty(parameters["ORDERID"]))
                {
                    r.ORDERID = parameters["ORDERID"];
                }
                else
                {
                    r.ORDERID = "";
                }
                if (!string.IsNullOrEmpty(parameters["MID"]))
                {
                    r.MID = parameters["MID"];
                }
                else
                {
                    r.MID = "Not Found.";
                }
                if (!string.IsNullOrEmpty(parameters["TXNAMOUNT"]))
                {
                    r.TXNAMOUNT = parameters["TXNAMOUNT"];
                }
                else
                {
                    r.TXNAMOUNT = "Not Found..";

                }
                if (!string.IsNullOrEmpty(parameters["CURRENCY"]))
                {
                    r.CURRENCY = parameters["CURRENCY"];
                }
                else
                {
                    r.CURRENCY = "Not Found";
                }
                if (!string.IsNullOrEmpty(parameters["STATUS"]))
                {
                    r.STATUS = parameters["STATUS"];
                }
                else
                {
                    r.STATUS = "failed";
                }
                if (!string.IsNullOrEmpty(parameters["RESPCODE"]))
                {
                    r.RESPCODE = parameters["RESPCODE"];
                }
                else
                {
                    r.RESPCODE = "";
                }
                if (!string.IsNullOrEmpty(parameters["RESPCODE"]))
                {
                    r.RESPMSG = parameters["RESPMSG"];
                }
                else
                {
                    r.RESPMSG = "Not Found..";
                }
                if (!string.IsNullOrEmpty(parameters["BANKTXNID"]))
                {
                    r.BANKTXNID = parameters["BANKTXNID"];
                }
                else
                {
                    r.BANKTXNID = "Not Found";
                }
                DataTable dt = objL.InsertUpdatePaytmResponse(r, "Proc_PaytmTransaction");

                ViewBag.Msg = "0";
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Id"].ToString() == "1")
                    {
                        Session["MobileNo"] = dt.Rows[0]["mcode"].ToString();
                        if (dt.Rows[0]["PaymentStatus"].ToString() == "success")
                        {

                            TempData["Msg"] = "http://www.kifayatistore.com/Checkout/SucessfullOrder?OrderId=" +r.ORDERID;
                             // TempData["Msg"] = "/Checkout/SucessfullOrder?OrderId=" + r.ORDERID;
                            //  DataTable dt4 = bus.GetEticket(r.ORDERID);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                              //  Msg12 = "<p><h3>Dear</h3> " + dt.Rows[0]["Name"].ToString() + ",Order Id :" + dt.Rows[0]["OrderId"].ToString() + "</p>";
                              

                             //   Msg12 += "<p><h3>Mobile No</h3> :" + dt.Rows[0]["mcode"].ToString() + ",<h3>PaymentStatus</h3> :" + dt.Rows[0]["PaymentStatus"].ToString() +"</p>";
                             
                             //   Msg12 += "<p><h3>Amount</h3> :" + Convert.ToDecimal(dt.Rows[0]["NetPayable"]) + "</p>";
                              //  Msg12 += "<p><h2>Payment Mode</h2> :" + dt.Rows[0]["PaymentMode"].ToString() + "</p>";
                               // Msg12 += "<p><h2>GateWayName</h2> :" + dt.Rows[0]["GateWayName"].ToString() + "</p>";
                             //   Msg12 += "<p>BankName :" + dt.Rows[0]["BankName"].ToString() + "</p>";
                               // Msg12 += "<p>RespMsg :" + dt.Rows[0]["RespMsg"].ToString() + "</p>";

                                // Msg12 += "<p> Date/Time : " + dt.Rows[0]["BookingDate"].ToString() + "  " + dt4.Rows[0]["Starttime"].ToString() + "</p>";

                                //  Msg12 += "<p>Reporting Time :" + dt4.Rows[0]["ReportTime"].ToString() + " .</p>";
                                //Msg12 += "<p>Seat No" + dt4.Rows[0]["SeatNo"].ToString() + "  Amount Paid :" + dt4.Rows[0]["BookingAmount"].ToString();
                                //    //Msg12 += "<p>Reporting At : Ravidas Ghat, Through Ravidas Park, Nagwa, near Lanka,Varanasi. </p>";

                                //    //Msg12 += "<p>Important: </p>";
                                //    //Msg12 += "<p>1. Please board on time.Latecomers will miss the cruise. </p><p> 2. PI carry a valid photo ID. </p> <p>3. No outside food. \n Helpline No.- 6392028999.</p>";
                                //    //Msg12 += "<p>Location . https://goo.gl/maps/97EmoqYAKgG3G5jj9 </p>";
                                //    //Msg12 += "<p>Happy Cruise! </p>";

                                //    //Msg12 += "Thanks and Regards \n Alaknanda Cruiseline Private Limited.";
                               // ViewBag.Message = Msg12;
                            }
                            Session["MobileNo"] = dt.Rows[0]["mcode"].ToString();
                            //if (dt.Rows[0]["PaymentStatus"].ToString() == "success" && dt4.Rows[0]["Tour"].ToString() == "Alaknanda")
                            //{
                            //    //Old Details

                            //    //AllClasses.SendSMS(Convert.ToString(Session["MobileNo"]), "Dear Travelers, Wish you a happy and enjoyable trip on " + dt.Rows[0]["CruiseName"].ToString() + ". Your reporting time is " + dt.Rows[0]["SlotName"].ToString() + " on " + dt.Rows[0]["BookingDate"].ToString() + "Click For More Details: https://alaknandacruise.com/Eticket/Index/" + r.ORDERID + "" + " at Alaknanda Jetty, Assi Ghat, Varanasi. For any clarification En-route feel free to reach us @ +91-6392028999 Mr. Rajat. Team Alaknanda");
                            //   // AllClasses.SendSMS(Convert.ToString(Session["MobileNo"]), "ALAKNANDA CRUISELINE BOOKING INFO https://alaknandacruise.com/" + r.ORDERID.Substring(r.ORDERID.Length - 5) + "");
                            //    //AllClasses.SendSMS(Convert.ToString(Session["MobileNo"]), "Dear Valuable Guest, You have received a message from Alaknanda Cruiseline click to view details https://alaknandacruise.com/ETicket.aspx?BookingId=" + r.ORDERID + "");

                            //}

                            if (dt.Rows[0]["PaymentStatus"].ToString() == "success")
                            {

                               // Msg12 = "https://alaknandacruise.com/Eticket/Index?Id=" + r.ORDERID;
                               // AllClasses.sendMailAttachment("", dt4.Rows[0]["CustomerEmail"].ToString(), "nordic.cruise.vnsi@gmail.com", "Alaknanda Cruise Line - Ticket Details", Msg12, null);
                                //AllClasses.sendMailAttachment("", "nordic.cruise.vnsi@gmail.com", "nordic.cruise.vnsi@gmail.com", "Alaknanda Cruise Line - Ticket Details", Msg12, null);
                                return RedirectToAction("SucessfullOrder", "Checkout", new { OrderId = r.ORDERID });
                            }
                        }
                        else if (dt.Rows[0]["PaymentStatus"].ToString() == "failed")
                        {
                            //sm.sendSMS(Session["MobileNo"].ToString(), "Your Order failed.Your Order Id is = " + r.ORDERID + " and Order Amount=" + r.TXNAMOUNT + "and Paytm_TXNID=" + dt.Rows[0]["Paytm_TXNID"].ToString() + " .\n Happy Shopping ! \n 24x7 shop india ");
                            //sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), "A Order has been failed .The Order Id is = " + r.ORDERID + " and Order Amount=" + r.TXNAMOUNT + " and Paytm_TXNID=" + dt.Rows[0]["Paytm_TXNID"].ToString() + " .\n Happy Shopping ! \n 24x7 shop india ");

                            return RedirectToAction("Index", "Checkout", new { id = r.ORDERID });
                        }

                        else if (dt.Rows[0]["PaymentStatus"].ToString() == "Pending")
                        {
                           // sm.sendSMS(Session["MobileNo"].ToString(), "Your Order is Pending.Your Order Id is = " + r.ORDERID + " and Order Amount=" + r.TXNAMOUNT + " and Paytm_TXNID=" + dt.Rows[0]["Paytm_TXNID"].ToString() + " .\n Happy Shopping ! \n 24x7 shop india ");
                           // sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), "A Order has been Arrived that's is amount pending .The Order Id is = " + r.ORDERID + " and Order Amount=" + r.TXNAMOUNT + " and Paytm_TXNID=" + dt.Rows[0]["Paytm_TXNID"].ToString() + " .\n Happy Shopping ! \n 24x7 shop india ");
                            return RedirectToAction("Index", "Checkout", new { id = r.ORDERID });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(r);
        }


        public ActionResult DesignationMaster()
        {
            try
            {
                objp.Action = "2";
                objp.dt = objL.DesignationMaster(objp, "proc_DesignationMaster");
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult InserUpdateDesignation(PropertyClass p)
        {
            string msg = "";
            try
            {
                DataTable dt = objL.DesignationMaster(p, "proc_DesignationMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["Id"].ToString();
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
        public JsonResult GetDesignationDetailsForEdit(string ItemCode1)
        {
            try
            {
                objp.Action = "3";
                objp.AccCode = ItemCode1.Trim();
                DataTable dt = objL.DesignationMaster(objp, "proc_DesignationMaster");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.ItemCode = dt.Rows[0]["DesignationCode"].ToString();
                    objp.UOM = dt.Rows[0]["DesignationName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SalaryAllowanceDeductionSetup()
        {
            try
            {
                objp.Action = "1";
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "1")
                {
                    objp.CompanyCode = null;
                }
                objp.dt = objL.InsertSalaryComponent(objp, "proc_InsertUpdateSalaryComponents");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult GetSalaryComponentDetailsForEdit(string ItemCode1)
        {
            try
            {
                objp.Action = "3";
                objp.AccCode = ItemCode1.Trim();
                DataTable dt = objL.InsertSalaryComponent(objp, "proc_InsertUpdateSalaryComponents");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.AccCode = dt.Rows[0]["H_Code"].ToString();
                    objp.AccountType = dt.Rows[0]["H_Type"].ToString();
                    objp.MenuTitle = dt.Rows[0]["H_Name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InserUpdateSalaryComponents(PropertyClass p)
        {
            string msg = "";
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Session["Role"].ToString() == "1")
                {
                    p.BranchCode = Convert.ToString(Session["CompanyCode"]);
                }
                else
                {
                    p.BranchCode = Convert.ToString(Session["UserName"]);
                }
                p.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.InsertSalaryComponent(p, "proc_InsertUpdateSalaryComponents");
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
        public ActionResult AddEmployee(string Empcode)
        {
            try
            {
                ViewBag.DesignationList = PropertyClass.BindDDL(objL.BindDesignationDropDown());
                if (!string.IsNullOrEmpty(Empcode))
                {
                    objp.EmpCode = Empcode;
                    objp.Action = "3";
                   DataTable dt = objL.EmployeeDetails(objp, "proc_EmployeeDetails");
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        objp.EmployeeName = dt.Rows[0]["Employee_Name"].ToString();
                        objp.FatherName = dt.Rows[0]["Employee_Father_Name"].ToString();
                        objp.strgender = dt.Rows[0]["Gender"].ToString();
                        objp.eDate = dt.Rows[0]["Date_of_Joining"].ToString();
                        objp.designation = dt.Rows[0]["Designation"].ToString();
                        objp.workemailid = dt.Rows[0]["WorkEmailId"].ToString();
                        objp.EPFNo = dt.Rows[0]["EPFNo"].ToString();
                        objp.ESINo = dt.Rows[0]["ESINo"].ToString();
                        objp.annuallyctc = Convert.ToDecimal(dt.Rows[0]["AnnuallyCTC"].ToString());
                        objp.BasicPer = Convert.ToDecimal(dt.Rows[0]["BasicPer"].ToString());
                        objp.HRAPer = Convert.ToDecimal(dt.Rows[0]["HRAPer"].ToString());
                        objp.conveyanceallowancemonthly = Convert.ToDecimal(dt.Rows[0]["ConveyanceAllowanceMonthly"].ToString());
                        objp.fixedallowancemonthly = Convert.ToDecimal(dt.Rows[0]["FixedAllowanceMonthly"].ToString());
                        objp.EmailId = dt.Rows[0]["PersonalEmailId"].ToString();
                        objp.ContactNo = dt.Rows[0]["Contact_Number"].ToString();
                        objp.mDate = dt.Rows[0]["DOB"].ToString();
                        objp.CityName = dt.Rows[0]["City"].ToString();
                        objp.StateName = dt.Rows[0]["State"].ToString();
                        objp.Address = dt.Rows[0]["Address"].ToString();
                        objp.PinCode = dt.Rows[0]["PinCode"].ToString();

                        objp.AccountNumber = dt.Rows[0]["AccountNumber"].ToString();
                        objp.AccountName = dt.Rows[0]["AccountHolderName"].ToString();
                        objp.BanKAccName = dt.Rows[0]["BankName"].ToString();
                        objp.ifsccode = dt.Rows[0]["IFSCCode"].ToString();
                        objp.AccountType = dt.Rows[0]["AccountType"].ToString();
                        objp.EmpCode = dt.Rows[0]["EmployeeCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult InserEmployeeDetails(PropertyClass p)
        {

            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                p.BranchCode = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.AddEmployee(p, "proc_AddEmployee");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["Id"].ToString();
                    objp.AccCode = dt.Rows[0]["EmpCode"].ToString();
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
        public ActionResult EmployeeList()
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "1")
                {
                    objp.CompanyCode = null;
                }
                objp.Action = "1";
                objp.dt = objL.EmployeeDetails(objp, "proc_EmployeeDetails");
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult EmployeeProfile()
        {
            try
            {
                if (Request.QueryString["EmpCode"] != null)
                {
                    if (Convert.ToString(Session["Role"]) == "2")
                    {
                        objp.CompanyCode = Convert.ToString(Session["UserName"]);
                    }
                    else
                    {
                        objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    }
                    if (Convert.ToString(Session["Role"]) == "1")
                    {
                        objp.CompanyCode = null;
                    }

                    objp.EmpCode = Convert.ToString(Request.QueryString["EmpCode"]);
                    objp.Action = "2";
                    objp.dt = objL.EmployeeDetails(objp, "proc_EmployeeDetails");
                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult PayScheduleSetup()
        {
            ViewBag.MonthListList = PropertyClass.BindDDL1(objL.BindMonthYearDDl(objp, "proc_bindMonthyear"));
            if (Convert.ToString(Session["Role"]) == "2")
            {
                objp.CompanyCode = Convert.ToString(Session["UserName"]);
            }
            else
            {
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
            }
            DataTable dt = objL.CheckPayScheduleUpdate(objp.CompanyCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.TotDemands = dt.Rows[0]["PayCount"].ToString();
            }
            else
            {
                objp.TotDemands = "0";
            }
            return View(objp);
        }
        public JsonResult InsertPayRollSchedule(string Monthly_Salary_Based_On, string First_PayrollStartsOn, string WorkingDays, List<WorkingDaysofWeek> ItemList)
        {
            string msg = "";
            try
            {
                objp.Action = "1";
                objp.Monthly_Salary_Based_On = Monthly_Salary_Based_On;
                objp.First_PayrollStartsOn = First_PayrollStartsOn;

                objp.MonthName = First_PayrollStartsOn.Substring(0, First_PayrollStartsOn.IndexOf("-"));
                objp.Year = First_PayrollStartsOn.Substring(First_PayrollStartsOn.LastIndexOf('-') + 1);

                objp.WorkingDays = WorkingDays;
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
                DataTable dt = new DataTable();
                dt.Columns.Add("Daye_Name");
                foreach (var item in ItemList)
                {
                    dt.Rows.Add(item.Day_Name);
                }
                DataTable dt1 = objL.InsertPaySchedule(objp, "proc_InsertPaySchedule", dt);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.strId = dt1.Rows[0]["Id"].ToString();
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
        public ActionResult PayRun()
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                objp.Action = "1";
                DataTable dt = objL.GetPayRunDetails(objp, "proc_GetDetailsForPayRun");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.MonthName = dt.Rows[0]["Month"].ToString();
                    objp.Year = dt.Rows[0]["Year"].ToString();
                    objp.todayPo = dt.Rows[0]["totEmp"].ToString();
                    objp.Description = dt.Rows[0]["NetPay"].ToString();
                    objp.Status = dt.Rows[0]["Status"].ToString();
                    objp.AccCode = dt.Rows[0]["PayCode"].ToString();
                }
                else
                {
                    objp.strId = "1";
                }
                objp.Action = "3";
                objp.dt = objL.GetPayRunDetails(objp, "proc_GetDetailsForPayRun");

            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult InserPayRunDetails(PropertyClass p)
        {

            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                if (Convert.ToString(Session["Role"]) == "1")
                {
                    p.BranchCode = Convert.ToString(Session["CompanyCode"]);
                }
                else
                {
                    p.BranchCode = Convert.ToString(Session["UserName"]);
                }
                p.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.InsertPayRunDetails(p, "proc_InsertPayRunDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.AccCode = dt.Rows[0]["pCode"].ToString();
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
        public ActionResult PayRunUpdateDetail(string pCode, string EmployeeCode, string CompanyCode)
        {
            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    objp.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }

                objp.Action = "2";
                if (Request.QueryString["pCode"] != null)
                {
                    objp.AccCode = Convert.ToString(Request.QueryString["pCode"]);
                }
                else
                {
                    objp.AccCode = pCode;
                }
                objp.dt = objL.GetPayRunDetails(objp, "proc_GetDetailsForPayRun");

                objp.Action = "4";
                DataTable dt = objL.GetPayRunDetails(objp, "proc_GetDetailsForPayRun");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.MonthName = dt.Rows[0]["Month"].ToString();
                    objp.Year = dt.Rows[0]["Year"].ToString();
                    objp.todayPo = dt.Rows[0]["totEmp"].ToString();
                    objp.Description = dt.Rows[0]["NetPay"].ToString();
                    objp.Status = dt.Rows[0]["Status"].ToString();
                    objp.AccCode = dt.Rows[0]["payCode"].ToString();
                }
                if (!string.IsNullOrEmpty(EmployeeCode))
                {
                    ViewBag.EarningListList = PropertyClass.BindDDL(objL.BindEarningsDDl());
                    ViewBag.DeductionList = PropertyClass.BindDDL(objL.BindDeductionDDl());
                    objp.CompanyCode = CompanyCode;
                    objp.EmpCode = EmployeeCode;
                    objp.AccCode = pCode;
                    objp.Action = "1";
                    objp.dt1 = objL.BindEmployee_SalaryDetails(objp, "proc_getEmployeeEarningDeductions");
                    decimal totAmt = 0;
                    if (objp.dt1 != null && objp.dt1.Rows.Count > 0)
                    {

                        foreach (DataRow dr in objp.dt1.Rows)
                        {
                            totAmt += dr["Amount"].ToString() != "" ? Convert.ToDecimal(dr["Amount"].ToString()) : 0;
                        }
                    }
                    objp.NetTotal = totAmt;
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public JsonResult ApprovePayRoll(PropertyClass p)
        {

            try
            {
                if (Convert.ToString(Session["Role"]) == "2")
                {
                    p.CompanyCode = Convert.ToString(Session["UserName"]);
                }
                else
                {
                    p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                }
                p.EntryBy = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.ApprovePayRunDetails(p, "proc_ApproveRejectPayRoll");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.AccCode = dt.Rows[0]["pCode"].ToString();
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
        public PartialViewResult _pBindEmployeeSalaryDetails(string EmployeeCode, string CompanyCode, string PayCode)
        {
            
            objp.CompanyCode = CompanyCode;
            objp.EmpCode = EmployeeCode;
            objp.AccCode = PayCode;
            objp.Action = "1";
            objp.dt = objL.BindEmployee_SalaryDetails(objp, "proc_getEmployeeEarningDeductions");
            decimal totAmt = 0;
            if (objp.dt != null && objp.dt.Rows.Count > 0)
            {

                foreach (DataRow dr in objp.dt.Rows)
                {
                    totAmt += dr["Amount"].ToString() != "" ? Convert.ToDecimal(dr["Amount"].ToString()) : 0;
                }
            }
            objp.NetTotal = totAmt;
            return PartialView(objp);
        }
        public ActionResult getPaySlip()
        {
            try
            {
                if(Request.QueryString["CompanyCode"] != null && Request.QueryString["PayCode"] != null && Request.QueryString["EmpCode"] != null)
                {
                    objp.Action = "2";
                    objp.CompanyCode = Convert.ToString(Request.QueryString["CompanyCode"]);
                    objp.AccCode = Convert.ToString(Request.QueryString["PayCode"]);
                    objp.EmpCode = Convert.ToString(Request.QueryString["EmpCode"]);

                    objp.dt = objL.BindEmployee_SalaryDetails(objp, "proc_getEmployeeEarningDeductions");
                    decimal totAmt = 0;
                    if (objp.dt != null && objp.dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in objp.dt.Rows)
                        {
                            totAmt += dr["Amount"].ToString() != "" ? Convert.ToDecimal(dr["Amount"].ToString()) : 0;
                        }
                    }
                    objp.NetTotal = totAmt;
                }
                
            }
            catch(Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult UpdatepayRoll(PropertyClass p)
        {
            try
            {
                DataTable dt = objL.updatepayRun(p, "Proc_UpdatePayRun");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.AccCode = dt.Rows[0]["PayCode"].ToString();
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
    }
    public class WorkingDaysofWeek
    {
        public string Day_Name { get; set; }
    }
}