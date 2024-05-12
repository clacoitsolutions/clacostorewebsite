using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using OjasMart.Models;
using System.Collections;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace OjasMart.Models
{
    public class clsSaleReturn
    {
        public clsSaleReturn()
        {
            this.InvList = new List<SelectListItem>();
        }
        public List<SelectListItem> InvList { get; set; }

        public string CustomerAccountCode { get; set; }

        public string InvDetails { get; set; }
        public int Action { get; set; }
        public string InvNo { get; set; }
        public string msg { get; set; }

        public string MobileNo { get; set; }
        public string CustomerName { get; set; }
        public string VoucherId { get; set; }
        public string InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string GrossPayable { get; set; }
        public string Totaltax { get; set; }
        public string NetTotal { get; set; }
        public string DiscountAmount { get; set; }
        public string PaymentMode { get; set; }


    }
}