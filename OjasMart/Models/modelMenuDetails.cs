using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class modelMenuDetails
    {
        public List<MainMenuModel1> MainMenuList { get; set; }
    }
    public class MainMenuModel1
    {
        public string MenuName { get; set; }
        public int MenuId { get; set; }
        public string MenuUrl { get; set; }
        public string Menuicon { get; set; }
        public int? Priority { get; set; }
        public List<SubMenuModel> SubMenuModelList { get; set; }
    }

    public class SubMenuModel
    {
        public string MenuName { get; set; }
        public int MenuId { get; set; }
        public string MenuUrl { get; set; }
        public string Menuicon { get; set; }
        public List<ThiredMenuModel> ThiredMenuModelList { get; set; }

    }
    public class ThiredMenuModel
    {
        public string ThiredMenuName { get; set; }
        public int ThiredMenuId { get; set; }
        public string ThiredMenuUrl { get; set; }
        public string ThiredMenuicon { get; set; }

    }
}