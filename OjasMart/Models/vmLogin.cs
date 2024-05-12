using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class vmLogin
    {
        public int SrNo { get; set; }
        public string MCode { get; set; }
        [Required(ErrorMessage = "Please Enter UserId/Email Id")]
        public string UserId { get; set; }
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Contact { get; set; }
        public string Name { get; set; }
        public string CompanyCode { get; set; }
        public bool Rememberme { get; set; }
    }
}