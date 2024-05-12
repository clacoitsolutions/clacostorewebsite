using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class VMVendorSignup
    {

        [Required(ErrorMessage = "* Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Please Enter Email Address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "* Please Provide valid mail Address")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "* Please Enter Mobile No.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "* Invalid Mobile Number")]
        public string ContactNo { get; set; }
        public string OTP { get; set; }
        public string OTPStatus { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsChecked { get; set; }
        public string ApproveStatus { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Conform Password")]
        public string ConformPassword { get; set; }
        public string GSTNo { get; set; }
        public string MSMENo { get; set; }
        public string vendorCity { get; set; }
        public string VenState { get; set; }
        public string Action { get; set; }
        public string VenId { get; set; }
        public string Address { get; set; }
    }
}