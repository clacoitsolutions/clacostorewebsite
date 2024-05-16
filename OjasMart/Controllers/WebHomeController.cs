using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace OjasMart.Controllers
{
    public class WebHomeController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: WebHome
        public ActionResult Index(string StoreName, string StockistId)

        
        {
            try
            {
                DataSet ds = new DataSet();
                objp.Action = "1";
                ds = objL.GetDashboardProductsAll(objp, "proc_GetDashBoardProductsAll");
                string CustomerId = Convert.ToString(Session["mCode"]);
                string Action = "3";

                objp.cartCount = objL.getCartDetails(CustomerId, Action);

                Session["countcart"] = objp.cartCount.Rows.Count;
                //Session.Timeout = 30;

                if (StoreName != null && StockistId != null)
                {
                    Session["StockistId"] = StockistId;
                    Session["StoreName"] = StoreName;
                }
                else
                {
                    DataTable dt = new DataTable();

                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        Session["StockistId"] = ds.Tables[0].Rows[0]["SSCode"].ToString();
                        Session["StoreName"] = ds.Tables[0].Rows[0]["SSName"].ToString();
                    }
                }

                if (ds != null)
                {
                    if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        try
                        {

                            objp.dtMainCategory = ds.Tables[1];
                        }
                        catch (Exception ex) { }
                    }

                    if (ds != null && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        try
                        {
                            objp.dt = ds.Tables[2];
                            //for (int i = 0; i <.Count; i++)
                            //{
                            //    Console.Write(arlist[i] + ", ")

                            //        }
                            //foreach (var dr in ds.Tables[2].Rows[0]["ProductCode"].ToString())
                            //{
                            //    objp.ItemCode = 


                            //}
                        }
                        catch (Exception ex) { }

                    }

                    if (ds != null && ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        try
                        {
                            objp.dtLatest = ds.Tables[3];
                        }
                        catch (Exception ex) { }

                    }

                    if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                    {
                        try
                        {
                            objp.dtCatProduct = ds.Tables[4];
                        }
                        catch (Exception ex) { }

                    }

                    if (ds != null && ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                    {
                        try
                        {
                            objp.dtTopBanner = ds.Tables[5];
                        }
                        catch (Exception ex) { }

                    }

                    if (ds != null && ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                    {
                        try
                        {
                            objp.dtPromocode = ds.Tables[6];
                        }
                        catch (Exception ex) { }

                    }

                    if (ds != null && ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                    {
                        try
                        {
                            objp.dtOffer = ds.Tables[7];
                        }
                        catch (Exception ex) { }

                    }
                    //objp.Action = "6";
                    //objp.sizeList = objL.Proc_GetProductDetail_Updated(objp, "Proc_GetProductDetail_Updated");
                    objp.dtValueOffer = objL.GetActiveOffers(objp, "Proc_GetActiveOffers");


                }

            }
            catch (Exception ex)
            { }
            return View(objp);
        }


        public PartialViewResult GetproductCategoryWise(string CategoryId)
        {
            DataSet ds = new DataSet();
            objp.Action = "1";
            objp.StockistId = CategoryId;
            ds = objL.GetDashboardProductsAll(objp, "proc_GetDashBoardProductsAll");
            if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                try
                {
                    objp.dtCatProduct = ds.Tables[4];
                }
                catch (Exception ex) { }
            }
            return PartialView(objp);   
        }
        public ActionResult BindSidePopUpCat()
        {
            objp.dtPopupCat = objL.BindMainCategory();
            return View(objp);
        }
        public ActionResult BindSidePopUpCatMobile()
        {
            objp.dtPopupCat = objL.BindMainCategory();
            return View(objp);
        }
        public ActionResult BindMenuTopLevel()
        {
            objp.dtMenuTop = objL.BindMenuTop();
            return View(objp);
        }


        public ActionResult BindMainMenu()
        {
            objp.dtMenuTop = objL.BindMenuTop();
            return View(objp);


        }

        public ActionResult BindMenuSub(string PId)
        {
            objp.dtMenuSub = objL.BindMenuSubMenu(PId);
            return View(objp);
        }

        public ActionResult BindCatMenuSub(string PId)
        {

            objp.dtMenuSub = objL.BindMenuSubMenu(PId);

            return View(objp);
        }



        public ActionResult BindFooterCategory()
        {
            objp.dtMainCategory = objL.BindFooterCategory();
            return View(objp);
        }
        public ActionResult BindStore()
        {
            objp.dtSStockist = objL.BindSSDropDown1();
            return View(objp);
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult TermsCond()
        {
            return View();
        }
        public ActionResult ReturnPolicy()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult MemberShip()
        {

            DataTable dt = new DataTable();
            ClsMemberShip objcls = new ClsMemberShip();
            objcls.Action = "1";

            ViewBag.dtMemberShipType = PropertyClass.BindDDL(objL.MemberShipMaster(objcls, "Proc_MemberShipMaster"));
            return View();
        }
        [HttpPost]
        public ActionResult MemberShip(ClsMemberShip objcls)
        {
            objcls.Action = "1";
            PropertyClass p = new PropertyClass();
            ViewBag.dtMemberShipType = PropertyClass.BindDDL(objL.MemberShipMaster(objcls, "Proc_MemberShipMaster"));
            p.SSName = objcls.CustomerName;
            p.ContactNo = objcls.MobileNo;
            p.EmailAddress = objcls.EmailAddress;
            p.Action = "1";
            p.Password = objcls.Password;
            p.memberShipId = objcls.MemberShip;
            p.PayMode = objcls.PaymentMode;
            p.CardBarCode = objcls.MemberBarCode;
            DataTable dt = objL.InsertCustomerAccountNewMember(p, "Proc_InserCustomerAccountWeb");
            if (dt != null && dt.Rows.Count > 0)
            {
                objp.msg = dt.Rows[0]["msg"].ToString();
                TempData["msg"] = dt.Rows[0]["msg"].ToString();
                objp.strId = dt.Rows[0]["Id"].ToString();
                if (objp.strId == "1")
                {
                    Session["CompanyName"] = "Kalash Mart";
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
            objcls.MemberShip = "D";
            return View(objcls);
        }
        public ActionResult Career()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(ClsContactUs objcls)
        {
            DataTable dt = new DataTable();
            objcls.Action = "1";
            try
            {
                dt = objL.ContactUs(objcls, "Proc_ContactUs");
                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["msg"] = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    TempData["msg"] = "Some Error Occured";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Some Error Occured";
            }
            return View(objcls);
        }
        public ActionResult ApplyForFranchies()
        {
            return View();
        }


        #region Added by Anchal : 30-08-2022
        public ActionResult referrals()
        {
            return View();
        }
        #endregion



        public ActionResult ConsumerPolicy()
        {
            return View();
        }
        public ActionResult EPRCompliance()
        {
            return View();

        }
        public ActionResult Grievanceredressal()
        {
            return View();
        }
        public ActionResult Security()
        {
            return View();
        }
        public ActionResult TermOfUse()
        {
            return View();
        }
        public ActionResult Blog(string BlogId)
        {

            try
            {
                if (BlogId != null)
                {
                    objp.BlogId = BlogId;
                    objp.Action = "22";
                    objp.dt = objL.ManufacturerMaster(objp, "proc_ManufacturerMaster");

                }
                else
                {
                    objp.Action = "21";
                    objp.dt = objL.ManufacturerMaster(objp, "proc_ManufacturerMaster");
                }

            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }


        // our service section
        public ActionResult our_services()
        {
            return View();
        }

        [HttpPost]

        public ActionResult our_services(ServiceRequestModel Objt)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultNewCon"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("proc_InsertServiceRequest", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the stored procedure

                command.Parameters.AddWithValue("@ServiceType", Objt.ServiceType);
                command.Parameters.AddWithValue("@FullName", Objt.FullName);
                command.Parameters.AddWithValue("@MobileNumber", Objt.MobileNumber);
                command.Parameters.AddWithValue("@Email", Objt.Email);
                command.Parameters.AddWithValue("@Address", Objt.Address);


                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            string message = "Your service request has been submitted successfully! 🎉 Our team contact you within 10 minutes !!!THANK YOU  😊";
            TempData["SuccessMessage"] = message;



            return View(Objt);
        }


        [HttpGet]
        public ActionResult Search(string query)
        {
            // Query your data source (e.g., database) to fetch suggestions based on the 'query'

            // For demonstration, let's assume you have a static list of suggestions
            var suggestions = new List<string> { "apple", "banana", "orange", "pear", "pineapple", "grape", "kiwi", "strawberry" };

            // Filter suggestions based on the query
            var filteredSuggestions = suggestions.Where(s => s.ToLower().StartsWith(query.ToLower())).ToList();

            return Json(filteredSuggestions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult viewGetproductCtg(string CategoryId)
        {
            objp.Action = "1";
            objp.StockistId = CategoryId;
            DataSet ds = new DataSet();

            ds = objL.GetDashboardProductsAll(objp, "proc_GetDashBoardProductsAll");
            if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                try
                {
                    objp.dtCatProduct = ds.Tables[4];
                }
                catch (Exception ex) { }
            }
            return View(objp);
        }


        // best category 

        public PartialViewResult bestcategoryshow(string CategoryId)
        {
            DataSet ds = new DataSet();
            objp.Action = "1";
            objp.StockistId = CategoryId;
            ds = objL.GetDashboardProductsAll(objp, "proc_GetDashBoardProductsAll");
            if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                try
                {
                    objp.dtCatProduct = ds.Tables[4];
                }
                catch (Exception ex) { }
            }

            return PartialView(objp);
        }
        public PartialViewResult trandingProduct(string CategoryId)
        {
            DataSet ds = new DataSet();
            objp.Action = "1";
            objp.StockistId = CategoryId;
            ds = objL.GetDashboardProductsAll(objp, "proc_GetDashBoardProductsAll");
            if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                try
                {
                    objp.dtCatProduct = ds.Tables[4];
                }
                catch (Exception ex) { }
            }
            return PartialView(objp);
        }
        public PartialViewResult viewoffercard(string CategoryId)
        {
            DataSet ds = new DataSet();
            objp.Action = "1";
            objp.StockistId = CategoryId;
            ds = objL.GetDashboardProductsAll(objp, "proc_GetDashBoardProductsAll");
            if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                try
                {
                    objp.dtCatProduct = ds.Tables[4];
                }
                catch (Exception ex) { }
            }
            return PartialView(objp);
        }
        public PartialViewResult GetproductGroceryCategory()
        {
            DataSet ds = new DataSet();
            objp.Action = "1";
            objp.StockistId = "40269";
            ds = objL.GetDashboardProductsAll(objp, "proc_GetDashBoardProductsAll");
            if (ds != null && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                try
                {
                    objp.dtCatProduct = ds.Tables[4];
                }
                catch (Exception ex) { }
            }
            return PartialView(objp);
        }

    }
    
}