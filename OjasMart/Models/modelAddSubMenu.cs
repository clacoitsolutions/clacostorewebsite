using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjasMart.Models
{
    public class modelAddSubMenu
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*Required")]
        public int MainMenuId { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string SubMenuName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string url { get; set; }
        public Nullable<int> Priority { get; set; }
        public string MenuIcon { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public string MainMenuName { get; set; }
        public modelAddSubMenu()
        {
            this.MainMenuList = new List<SelectListItem>();
        }
        public List<SelectListItem> MainMenuList { get; set; }
        public bool IsMenuSelect { get; set; }        
        public List<modelAddSubMenu> submenuList = new List<modelAddSubMenu>();
    }
}