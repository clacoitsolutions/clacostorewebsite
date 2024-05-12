using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OjasMart.Models;

namespace OjasMart
{
    public partial class frmResponceHandling : System.Web.UI.Page
    {
        string ResponseString = "";
        LogicClass objL = new LogicClass();
        PropertyClass objp = new PropertyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    foreach (string key in Request.Form.Keys)
                    {
                        ResponseString += Request.Form[key].ToString() + "_";
                        Response.Write(Request.Form[key].ToString() + "_");
                        Response.Write(key);
                    }
                    VerifyCheckSum();
                }
                catch
                {


                }
            }
        }
        protected void VerifyCheckSum()
        {
            try
            {
                
                string Order_Id = "";
                string Query = "";
                string TXNAMOUNT = "";
                TXNAMOUNT = Request.Form["TXNAMOUNT"].ToString();
                Order_Id = Request.Form["ORDERID"].ToString();
                if (TransactionAmountCheck(Order_Id, TXNAMOUNT) == false)
                {
                    Response.Redirect("FailedTranscation.aspx?txnId=" + Order_Id + "&&Amount=" + TXNAMOUNT + "");
                }
                decimal amount;
                string RESPCODE;
                string RESPMSG;
                string GATEWAYNAME;
                string TXNDATE;
                string TXNID;
                string BankTxnId;
                string bankName;
                string CardType;
                if (string.IsNullOrEmpty(Request.Form["TXNAMOUNT"]) == false)
                {
                    amount = Convert.ToDecimal(Request.Form["TXNAMOUNT"]);
                }
                else
                {
                    amount = 0;
                }
                if (string.IsNullOrEmpty(Request.Form["RESPCODE"]) == false)
                {
                    RESPCODE = Request.Form["RESPCODE"].ToString();
                }
                else
                {
                    RESPCODE = "";
                }
                if (string.IsNullOrEmpty(Request.Form["RESPMSG"]) == false)
                {
                    RESPMSG = Request.Form["RESPMSG"].ToString();
                }
                else
                {
                    RESPMSG = "";
                }
                if (string.IsNullOrEmpty(Request.Form["GATEWAYNAME"]) == false)
                {
                    GATEWAYNAME = Request.Form["GATEWAYNAME"].ToString();
                }
                else
                {
                    GATEWAYNAME = "FAILPAYTM";
                }
                if (string.IsNullOrEmpty(Request.Form["TXNDATE"]) == false)
                {
                    TXNDATE = Request.Form["TXNDATE"].ToString();
                }
                else
                {
                    TXNDATE = "";
                }
                if (string.IsNullOrEmpty(Request.Form["TXNID"]) == false)
                {
                    TXNID = Request.Form["TXNID"].ToString();
                }
                else
                {
                    TXNID = "";
                }
                string status = "";
                if (Request.Form["STATUS"].ToString() == "TXN_FAILURE")
                {
                    status = "failed";
                }
                else
                {
                    status = "success";
                }
                BankTxnId = (!string.IsNullOrEmpty(Request.Form["BANKTXNID"])) ? Request.Form["BANKTXNID"] : null;
                CardType = (!string.IsNullOrEmpty(Request.Form["PAYMENTMODE"])) ? Request.Form["PAYMENTMODE"] : null;
                bankName = (!string.IsNullOrEmpty(Request.Form["BANKNAME"])) ? Request.Form["BANKNAME"] : null;
                objp.Action = "1";
                objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                objp.CustomerId = Convert.ToString(Session["UserName"]);
                objp.PaidAmount = amount;
                objp.Status = status;
                objp.CardType = CardType;
                objp.EntryBy = Convert.ToString(Session["UserName"]);
                objp.txnId = Order_Id;
                objp.BankTransId = BankTxnId;
                objp.PaytmTransId = TXNID;
                objp.RespoCode = RESPCODE;
                objp.RespMsg = RESPMSG;
                objp.GateWayname = GATEWAYNAME;
                objp.Bankname = bankName;

                DataTable dt = objL.UpdateOnlineWalletRechargeInfo(objp, "Proc_UpdateWalletRechageStatusOnline");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (string key in Request.Form.Keys)
                    {
                        ResponseString += Request.Form[key].ToString() + "_";
                    }
                    ViewState["ResponseString"] = ResponseString;
                    String merchantKey = "sV&Z_3cTWQLw7QIL"; // Replace the with the Merchant Key provided by Paytm at the time of registration.

                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    string paytmChecksum = "";
                    foreach (string key in Request.Form.Keys)
                    {
                        parameters.Add(key.Trim(), Request.Form[key].Trim());
                    }

                    if (parameters.ContainsKey("CHECKSUMHASH"))
                    {
                        paytmChecksum = parameters["CHECKSUMHASH"];
                        parameters.Remove("CHECKSUMHASH");
                    }
                    if (paytm.CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
                    {
                        if (Request.Form["RESPCODE"].ToString() == "01")
                        {
                            objp.msg = dt.Rows[0]["id"].ToString();
                            objp.WalletBalance = dt.Rows[0]["WalletAmt"].ToString();                           
                            string CustName = dt.Rows[0]["CustName"].ToString();
                            string RechrgDate = dt.Rows[0]["RechrgDate"].ToString();

                            string MobNo = dt.Rows[0]["MobNo"].ToString();
                            if (MobNo.Trim() != "")
                            {
                                string msg1 = "Dear " + CustName + " Your Wallet Credited with Rs." + amount + "," + RechrgDate + " Your Wallet Balance is Rs." + objp.WalletBalance + ".";
                                objp.SendSMS(MobNo.Trim(), msg1);
                            }

                            Response.Redirect("SuccessTranscation.aspx?txnId=" + Order_Id + "&&Amount=" + TXNAMOUNT + "");
                        }
                        else
                        {
                            Response.Redirect("FailedTranscation.aspx?txnId=" + Order_Id + "&&Amount=" + TXNAMOUNT + "");
                        }
                    }
                    else
                    {
                        Response.Redirect("FailedTranscation.aspx?txnId=" + Order_Id + "&&Amount=" + TXNAMOUNT + "");
                    }

                }
                else
                {
                    Response.Redirect("FailedTranscation.aspx?txnId=" + Order_Id + "&&Amount=" + TXNAMOUNT + "");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");
            }
        }

        public Boolean TransactionAmountCheck(string Order_Id, string amount)
        {
            Boolean Status = false;

            DataTable dt = objL.CheckRechargeAmount(Order_Id);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToDecimal(dt.Rows[0]["RechargeAmount"].ToString()) != Convert.ToDecimal(amount))
                {
                    Status = false;
                }
                else if (Convert.ToDecimal(dt.Rows[0]["RechargeAmount"].ToString()) == Convert.ToDecimal(amount))
                {
                    Status = true;
                }

            }
            return Status;
        }
    }
}