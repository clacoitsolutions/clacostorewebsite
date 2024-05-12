using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjasMart.Models
{
    public class MenuListModel
    {
        [Required(ErrorMessage = "Please select Role")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please select Role")]
        public int RoleId { get; set; }
        public int MId { get; set; }

        public List<modelAddMenu> MainmenuList = new List<modelAddMenu>();
        public MenuListModel()
        {
            this.RoleList = new List<SelectListItem>();
        }
        public List<SelectListItem> RoleList { get; set; }
        public List<MainMenuModel> MainMenuList { get; set; }
    }
    public class MainMenuModel
    {
        public int MainMenuId { get; set; }
        public string MainMenu { get; set; }
        public bool IsMainMenuSelect { get; set; }
        public List<modelAddSubMenu> MenuList { get; set; }
    }
}