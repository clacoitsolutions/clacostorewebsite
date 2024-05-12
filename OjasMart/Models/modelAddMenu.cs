using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class modelAddMenu
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string MainMenuName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Url { get; set; }
        public Nullable<int> Priority { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string MenuIcon { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public List<modelAddMenu> menuList = new List<modelAddMenu>();
    }
}