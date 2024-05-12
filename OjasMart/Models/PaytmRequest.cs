using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class PaytmRequest
    {

        public string Cust_Id { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }
        public string OrderId { get; set; }
    }

    public class PaymentResponse1
    {
       public string Action { get; set; }
        public string MID { get; set; }
        public string ORDERID { get; set; }
        public string TXNAMOUNT { get; set; }
        public string CURRENCY { get; set; }
        public string TXNID { get; set; }
        public string BANKTXNID { get; set; }
        public string STATUS { get; set; }
        public string RESPCODE { get; set; }
        public string RESPMSG { get; set; }
        public string TXNDATE { get; set; }
        public string GATEWAYNAME { get; set; }
        public string BANKNAME { get; set; }
        public string PAYMENTMODE { get; set; }
        public string CHECKSUMHASH { get; set; }
        public string FormData { get; set; }
        public string strId { get; set; }
    }

    
}
