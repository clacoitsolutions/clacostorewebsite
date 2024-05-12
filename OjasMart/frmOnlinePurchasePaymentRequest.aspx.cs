using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OjasMart.Models;
using System.Data;

namespace OjasMart
{
    public partial class frmOnlinePurchasePaymentRequest : System.Web.UI.Page
    {
        PaytmPayment paid = new PaytmPayment();
        LogicClass objL = new LogicClass();
        PropertyClass objp = new PropertyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            string txnId = "";
            if (Request.QueryString["txnId"] != null)
            {
                txnId = Convert.ToString(Request.QueryString["txnId"]);
                InsertMethod(txnId);
            }
            //InsertMethod(txnId);
        }
        public void InsertMethod(string txnId)
        {
            try
            {
                string data = "";
                string HashKey = "";
                string chks = "False";
                string TRID = "";
                string txnid1 = "";
                if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
                {
                    Random rnd = new Random();
                    TRID = rnd.ToString() + DateTime.Now;
                    string strHash = Generatehash512(TRID);
                    txnid1 = strHash.ToString().Substring(0, 20).ToUpper();
                }
                else
                {
                    txnid1 = Request.Form["txnid"];
                }
                //BookingRequest("1251155012", "1000210456", "100", "Janu Hassan", "jaanu.san2010@gmail.com", "7355644826", "Item", "../frmResponceHandling.aspx", "../frmResponceHandling.aspx", txnid1);

                if (!string.IsNullOrEmpty(txnId))
                {
                    objp.Action = "1";
                    objp.txnId = txnId;
                    DataTable dt = objL.GetOnlineCustomerTxnDetail(txnId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        BookingRequest(dt.Rows[0]["OrderId"].ToString(), dt.Rows[0]["OrderId"].ToString(), dt.Rows[0]["NetPayable"].ToString(), dt.Rows[0]["Name"].ToString(), dt.Rows[0]["EmailAddress"].ToString(), dt.Rows[0]["MobileNo"].ToString(), dt.Rows[0]["PInfo"].ToString(), "../frmResponceHandling.aspx", "../frmResponceHandling.aspx", txnid1);
                    }
                    else
                    {
                        Response.Write("Invalid transaction");
                    }
                }
                else
                {
                    Response.Write("Invalid transaction");
                }
            }
            catch
            {
            }
        }
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }
        public void BookingRequest(string TID, string BookingID, string Amount, string Name, string email, string phone, string productinfo, string surl, string furl, string txnid2)
        {
            try
            {
                paid.EpaytmPayment(TID, BookingID, Convert.ToDecimal(Amount), phone, email);
            }

            catch (Exception ex)
            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

            }

        }
    }
}