using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        ozasmartEntities db = new ozasmartEntities();
        public ActionResult Dashboard()
        {
            try
            {
                objp.Action = "1";
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Index", "Account");
                }
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.UserName = Convert.ToString(Session["UserName"]);
                objp.Role = Convert.ToString(Session["Role"]);
                DataSet ds = objL.BindDashBoardData(objp, "Proc_BindDashBoard");
                if (ds != null)
                {
                    objp.TotStokist = ds.Tables[0].Rows[0]["total"].ToString();
                    objp.TotOutlets = ds.Tables[1].Rows[0]["total"].ToString();
                    objp.TotPurchase = ds.Tables[2].Rows[0]["total"].ToString();
                    objp.TotDemands = ds.Tables[3].Rows[0]["total"].ToString();

                    objp.todayPo = ds.Tables[4].Rows[0]["total"].ToString();
                    objp.todayPoAmt = ds.Tables[5].Rows[0]["total"].ToString();
                    Session["LastLogedIn"] = ds.Tables[6].Rows[0]["LastLogedin"].ToString();

                    objp.dt = ds.Tables[7];


                    objp.todayOnlineOrder = ds.Tables[8].Rows[0]["total"].ToString();
                    objp.todayOnlineSale = ds.Tables[9].Rows[0]["total"].ToString();
                    objp.todayOfflineSale = ds.Tables[10].Rows[0]["total"].ToString();
                    objp.todayOnlinePendOrd = ds.Tables[11].Rows[0]["total"].ToString();
                    //vandana
                    objp.TotDelord = ds.Tables[12].Rows[0]["total"].ToString();
                    objp.todayOnlineDelOrd = ds.Tables[13].Rows[0]["total"].ToString();

                    objp.TotDelivered = ds.Tables[14].Rows[0]["total"].ToString();
                    objp.todayTotDelivered = ds.Tables[15].Rows[0]["total"].ToString();
                    objp.Totcancelled = ds.Tables[16].Rows[0]["total"].ToString();
                    objp.todayTotcancelled = ds.Tables[17].Rows[0]["total"].ToString();
                    objp.TotReturned = ds.Tables[18].Rows[0]["total"].ToString();
                    objp.todayTotReturned = ds.Tables[19].Rows[0]["total"].ToString();
                    objp.TolProduct = ds.Tables[20].Rows[0]["total"].ToString();
                    objp.TolProducttoday = ds.Tables[21].Rows[0]["total"].ToString();
                }
                if (Session["Role"].ToString() == "4")
                {
                    objp.Action = "2";
                    DataSet ds1 = objL.BindDashBoardData(objp, "Proc_BindDashBoard");
                    if (ds1 != null)
                    {
                        //objp.WalletBal = ds1.Tables[0].Rows[0]["WalletBal"].ToString();
                        Session["WalletBal"] = ds1.Tables[0].Rows[0]["WalletBal"].ToString();

                        objp.WalletBal = ds1.Tables[0].Rows[0]["WalletBal"].ToString();
                        objp.TotPurchase = ds1.Tables[1].Rows[0]["TotPurchase"].ToString();
                        objp.CashBackBalance = ds1.Tables[2].Rows[0]["CashBackBal"].ToString();
                    }
                    objp.Action = "3";
                    objp.dt1 = objL.BindDashBoardDatadt(objp, "Proc_BindDashBoard");

                }
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        [ChildActionOnly]
        public ActionResult GetMenuList()
        {
            try
            {
                //if (Session["Role"] == null)
                //{
                //    return RedirectToAction("Index", "Authentication");
                //    //return Content.Url("~/Account/Login");
                //}

                int RoleId = Convert.ToInt32(Session["Role"].ToString());
                modelMenuDetails model = new modelMenuDetails();
                var mstMenuPermission = db.tbl_MenuConfiguration.Where(m => m.RoleId == RoleId).Select(s => new { s.RoleId, s.MainMenuId, s.SubMenuId }).ToList();

                var MId = from fb in db.tbl_MenuConfiguration where fb.RoleId == RoleId select fb.MainMenuId;
                var result = from aa in db.tbl_MainMenu where MId.Contains(aa.Id) && aa.IsVisible==true select aa;

                var SMId = from fb in db.tbl_MenuConfiguration where fb.RoleId == RoleId select fb.SubMenuId;
                var Subresult = from aaa in db.tbl_SubMenu where SMId.Contains(aaa.Id) && aaa.IsVisible == true select aaa;

                var tMid = from fb in db.tbl_MenuConfiguration where fb.RoleId == RoleId select fb.ThiredLevelMenuid;

                var mstMainMenu = result.Select(s => new { s.Id, s.MainMenuName, s.Priority, s.MenuIcon, s.Url }).OrderBy(x => x.Priority).ToList();

                var mstMenu = Subresult.Select(s => new { s.MainMenuId, s.Id, s.SubMenuName, s.Priority, s.url, s.MenuIcon }).OrderBy(x => x.Priority).ToList();


                model.MainMenuList = (from mainmenu in mstMainMenu
                                      select new MainMenuModel1
                                      {
                                          MenuId = mainmenu.Id,
                                          MenuName = mainmenu.MainMenuName,
                                          MenuUrl = mainmenu.Url,
                                          Menuicon = mainmenu.MenuIcon,
                                          SubMenuModelList = (from menu in mstMenu
                                                              where menu.MainMenuId == mainmenu.Id
                                                              select new SubMenuModel
                                                              {
                                                                  MenuId = menu.Id,
                                                                  MenuName = menu.SubMenuName,
                                                                  MenuUrl = menu.url,
                                                                  Menuicon = menu.MenuIcon

                                                              }).ToList()
                                      }).ToList();
                return PartialView("GetMenuList", model);
            }
            catch (Exception ex)
            {

                return PartialView(new modelMenuDetails()
                {
                    MainMenuList = new List<MainMenuModel1>()
                });
            }

        }
        public ActionResult CustomerReport()
        {
            try
            {
                objp.Action = "1";
                objp.dt = objL.GetCustomerDetails(objp, "Proc_GetCustomerDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public ActionResult CustomerProfile()
        {
            try
            {
                ViewBag.StateList = PropertyClass.BindDDL(objL.BindStateDropDown());
                objp.Action = "2";
                if (Request.QueryString["CustCode"] != null)
                {
                    objp.OutLetCode = Convert.ToString(Request.QueryString["CustCode"]);
                }
                else if (Session["UserName"] != null)
                {
                    objp.OutLetCode = Convert.ToString(Session["UserName"]);
                }
                DataTable dt = objL.GetCustomerDetails(objp, "Proc_GetCustomerDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.SSCode = dt.Rows[0]["CustomerId"].ToString();
                    objp.SSName = dt.Rows[0]["Name"].ToString();
                    objp.Mtype = dt.Rows[0]["Mtype"].ToString();
                    objp.ActiveStatus = dt.Rows[0]["ActiveStatus"].ToString();
                    objp.ContactNo = dt.Rows[0]["MobileNo"].ToString();
                    objp.EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
                    objp.Address = dt.Rows[0]["Address"].ToString();
                    objp.StateName = dt.Rows[0]["State_name"].ToString();
                    objp.StCode = dt.Rows[0]["StateId"].ToString();
                    objp.GstNo = dt.Rows[0]["GSTIN"].ToString();
                    objp.PanNo = dt.Rows[0]["PanNo"].ToString();
                    objp.CityName = dt.Rows[0]["PanNo"].ToString();
                }
                objp.Action = "3";
                objp.dt= objL.GetCustomerDetails(objp, "Proc_GetCustomerDetails");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult ApproveAccount(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.Action = "1";
                p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                DataTable dt = objL.ApproveCustomerAccount(p, "proc_ActivateCustomerAccount");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCustomerProfile(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.Action = "2";
                //p.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                p.UserName = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.InsertCustomerAccountWeb(p, "Proc_InserCustomerAccountWeb");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddOnMember()
        {
            try
            {
                objp.Action = "2";
                objp.CustomerId = Convert.ToString(Session["UserName"]);
                objp.dt = objL.InsertAddOnMembers(objp, "Proc_InsertAddOnMembers");
            }
            catch (Exception ex)
            {

            }
            return View(objp);
        }
        public JsonResult InsertAddOnMember(PropertyClass p)
        {
            string msg = "";
            try
            {
                p.CustomerId = Convert.ToString(Session["UserName"]);
                DataTable dt = objL.InsertAddOnMembers(p, "Proc_InsertAddOnMembers");
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
        public ActionResult MainPage()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult TermsCondition()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}