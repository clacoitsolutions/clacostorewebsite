using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjasMart.Models;
using System.Data;

namespace OjasMart.Controllers
{
    public class MenuConfigurationController : Controller
    {
        PropertyClass objP = new PropertyClass();
        LogicClass objL = new LogicClass();
        ozasmartEntities db = new ozasmartEntities();
        public ActionResult AddMainMenu()
        {
            return View();
        }
        public JsonResult insertMainMenuDetails(string MenuTitle, string url, string iconclass, int Priority)
        {
            string msg = "";
            try
            {
                objP.MenuTitle = MenuTitle;
                objP.Url = url;
                objP.iconClass = iconclass;
                objP.Priority = Priority;
                objP.Action = "1";
                int r = objL.InsertMenuDetails(objP);
                msg = "1";
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddSubMenu()
        {
            return View();
        }
        public JsonResult BindMainMenus()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            try
            {
                DataTable dt = new DataTable();
                objP.Action = "2";
                dt = objL.BindMainMenu(objP, "Proc_MenuConfiguration");
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow ds in dt.Rows)
                    {
                        PropertyClass obj = new PropertyClass();
                        obj.Id = Convert.ToInt32(ds["id"]);
                        obj.MenuTitle = Convert.ToString(ds["MainMenuName"]);
                        list.Add(obj);
                    }
                    //list = dt.AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertSubMenuDetails(int MainmenuId, string MenuTitle, string url, string iconclass, int Priority)
        {
            string msg = "";
            try
            {
                objP.MainMenuId = MainmenuId;
                objP.SubMenuTitle = MenuTitle;
                objP.Url = url;
                objP.iconClass = iconclass;
                objP.Priority = Priority;
                objP.Action = "3";
                int r = objL.InsertSubMenuDetails(objP);
                msg = "1";
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserPermission(int? RoleId)
        {
            TempData["flag"] = null;
            MenuListModel model = new MenuListModel();
            try
            {
                foreach (var st in db.mst_Role)
                {
                    model.RoleList.Add(new SelectListItem { Text = st.Role, Value = st.Id.ToString() });
                }
                if (RoleId != null)
                {
                    //string role = db.tblLogin_Master.FirstOrDefault(x => x.UserId == id).Role.ToLower();
                    var mstMenuPermission = db.tbl_MenuConfiguration.Where(m => m.RoleId == RoleId).Select(s => new { s.RoleId, s.MainMenuId, s.SubMenuId, s.ThiredLevelMenuid }).ToList();

                    var mstMainMenu = db.tbl_MainMenu.Where(m => m.IsVisible == true).Select(s => new { s.Id, s.MainMenuName, s.Priority }).OrderBy(x => x.Priority).ToList();

                    var mstMenu = db.tbl_SubMenu.Where(m => m.IsVisible == true).Select(s => new { s.MainMenuId, s.Id, s.SubMenuName, s.Priority }).OrderBy(x => x.Priority).ToList();

                    model.MainMenuList = (from mainmenu in mstMainMenu
                                          select new MainMenuModel
                                          {
                                              MainMenuId = mainmenu.Id,
                                              MainMenu = mainmenu.MainMenuName,
                                              IsMainMenuSelect = mstMenuPermission.FirstOrDefault(p => p.MainMenuId == mainmenu.Id) == null ? false : true,

                                              MenuList = (from menu in mstMenu
                                                          where menu.MainMenuId == mainmenu.Id
                                                          select new modelAddSubMenu
                                                          {
                                                              Id = menu.Id,
                                                              SubMenuName = menu.SubMenuName,
                                                              IsMenuSelect = mstMenuPermission.FirstOrDefault(p => p.SubMenuId == menu.Id) == null ? false : true,

                                                          }).ToList()
                                          }).ToList();
                    model.RoleId = RoleId ?? 0;
                }
                return View(model);

            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult UserPermission(MenuListModel menu)
        {
            int flag = 0;
            string Msg = "";
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("SignIn", "Authentication");
                }
                string entryUserMId = Session["UserName"].ToString();
                using (var db = new ozasmartEntities())
                {
                    //  using (var transaction = db.Database.BeginTransaction())
                    // {
                    List<tbl_MenuConfiguration> MstAdminMenuPermissionList = new List<tbl_MenuConfiguration>();
                    foreach (var mainItem in menu.MainMenuList)
                    {
                        if (mainItem.IsMainMenuSelect)
                            MstAdminMenuPermissionList.Add(new tbl_MenuConfiguration { RoleId = menu.RoleId, MainMenuId = mainItem.MainMenuId, SubMenuId = null, ThiredLevelMenuid = null, EntryDate = DateTime.Now, EntryBy = entryUserMId });

                        if (mainItem.MenuList != null && mainItem.MenuList.Count > 0)
                        {
                            foreach (var Item in mainItem.MenuList)
                            {
                                if (Item.IsMenuSelect)
                                    MstAdminMenuPermissionList.Add(new tbl_MenuConfiguration { RoleId = menu.RoleId, MainMenuId = mainItem.MainMenuId, SubMenuId = Item.Id, ThiredLevelMenuid = null, EntryDate = DateTime.Now, EntryBy = entryUserMId });

                            }


                        }
                    }

                    var permissionList = db.tbl_MenuConfiguration.Where(x => x.RoleId == menu.RoleId).ToList();
                    for (int i = 0; i < permissionList.Count; i++)
                    {
                        db.tbl_MenuConfiguration.Remove(permissionList[i]);

                        db.SaveChanges();
                    }
                    for (int k = 0; k < MstAdminMenuPermissionList.Count; k++)
                    {
                        db.tbl_MenuConfiguration.Add(MstAdminMenuPermissionList[k]);
                        db.SaveChanges();
                    }
                    Msg = "Permission change successfully";
                    flag = 1;
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                flag = 0;
            }
            TempData["flag"] = flag;
            return RedirectToAction("UserPermission", "MenuConfiguration");
        }
        public JsonResult BindRole()
        {
            try
            {
                List<PropertyClass> list = new List<PropertyClass>();
                try
                {
                    DataTable dt = new DataTable();
                    objP.Action = "4";
                    dt = objL.BindMainMenu(objP, "Proc_MenuConfiguration");
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow ds in dt.Rows)
                        {
                            PropertyClass obj = new PropertyClass();
                            obj.RoleId = Convert.ToInt32(ds["Id"]);
                            obj.RoleName = Convert.ToString(ds["Role"]);
                            list.Add(obj);
                        }
                        //list = dt.AsEnumerable().ToList();
                    }
                }
                catch (Exception ex)
                {

                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        // GET: MenuConfiguration
        public ActionResult Index()
        {
            return View();
        }
    }
}