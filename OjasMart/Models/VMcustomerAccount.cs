using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class VMcustomerAccount
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        [Required(ErrorMessage = "*Name is Required")]
        public string CustomerType { get; set; }

        public string SponsorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string Organization { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "*Mobile ID is Required")]
        public string Email { get; set; }
public string msg { get; set; }
        public string msg1 { get; set; }
        public string AltEmail { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        [Required(ErrorMessage = "*Mobile No. is Required")]
        public string Mobile1 { get; set; }
        public string Role { get; set; }
        public string Mobile2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Picture { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Required(ErrorMessage = "*Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*Confirm Password is Required")]
        public string ConfirmPassword { get; set; }
        public bool IsChecked { get; set; }

        private bool isverified;
        public int OTP { get; set; }
        public string Position { get; set; }
        public bool IsOtpVerified
        {
            get { return isverified; }
            set { isverified = value; }
        }
    }
}