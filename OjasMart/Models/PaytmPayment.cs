using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace OjasMart.Models
{
    public class PaytmPayment
    {
        paytm.CheckSum pt = new paytm.CheckSum();

        public void EpaytmPayment(string ORDER_ID, string CUST_ID, decimal TXN_AMOUNT, string MOBILE_NO, string EMAIL)
        {
            //string MID = "OxcNaG18439334637134";
            string MID = "LhCnEZ84716866917246";
            string REQUEST_TYPE = "DEFAULT";
            string CHANNEL_ID = "WEB";
            string INDUSTRY_TYPE_ID = "Retail";
            string WEBSITE = "DEFAULT";

            //string MID = "nWSyuq15120022384664";
            //string REQUEST_TYPE = "DEFAULT";
            //string CHANNEL_ID = "WEB";
            //string INDUSTRY_TYPE_ID = "Retail";
            //string WEBSITE = "UPSTDC";
            //String merchantKey = "#tXXL5%Tyt2Q@mz&";

            String merchantKey = "sV&Z_3cTWQLw7QIL";
            //String merchantKey = "grVWXimuHbBeQ##t";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", MID);
            parameters.Add("CHANNEL_ID", CHANNEL_ID);
            parameters.Add("INDUSTRY_TYPE_ID", INDUSTRY_TYPE_ID);
            parameters.Add("WEBSITE", WEBSITE);
            parameters.Add("EMAIL", EMAIL);
            parameters.Add("MOBILE_NO", MOBILE_NO);
            parameters.Add("CUST_ID", CUST_ID);
            parameters.Add("ORDER_ID", ORDER_ID);
            parameters.Add("TXN_AMOUNT", TXN_AMOUNT.ToString());
            //parameters.Add("CALLBACK_URL", "http://localhost:9349/frmResponceHandling.aspx");
            parameters.Add("CALLBACK_URL", "http://ozas199.com/frmResponceHandling.aspx");
            //parameters.Add("CALLBACK_URL", "http://www.dillimarts.in/EResponse.aspx"); 
            //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);
            //string paytmURL = "https://secure.paytm.in/oltp-web/processTransaction?orderid=" + ORDER_ID;
            //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + ORDER_ID;
            //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + ORDER_ID;
            string paytmURL = "https://securegw.paytm.in//theia/processTransaction?orderid=" + ORDER_ID; 
            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            HttpContext.Current.Response.Write(outputHTML);

        }


        public void EpaytmPaymentResponse(string ORDER_ID)
        {
            string value = "https://secure.paytm.in/oltp/HANDLER_INTERNAL/getTxnStatus?JsonData=";

            Dictionary<string, string> innerrequest = new Dictionary<string, string>();
            Dictionary<string, string> outerrequest = new Dictionary<string, string>();



            innerrequest.Add("MID", "nWSyuq15120022384664");

            innerrequest.Add("ORDERID", ORDER_ID);
            // innerrequest.Add("CALLBACK_URL", "http://localhost:44413/DilliMart/EResponse.aspx");

            String first_jason = new JavaScriptSerializer().Serialize(innerrequest);


            first_jason = first_jason.Replace("\\", "").Replace(":\"{", ":{").Replace("}\",", "},");

            string Check = paytm.CheckSum.generateCheckSum("W_hE7Svuy_VuNgOX", innerrequest);
            string correct_check = HttpContext.Current.Server.UrlEncode(Check);

            innerrequest.Add("CHECKSUMHASH", correct_check);


            String final = new JavaScriptSerializer().Serialize(innerrequest);
            final = final.Replace("\\", "").Replace(":\"{", ":{").Replace("}\",", "},");

            String url = value + final;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);



            request.Headers.Add("ContentType", "application/json");
            request.Method = "POST";

            using (StreamWriter requestWriter2 = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter2.Write(final);

            }

            string responseData = string.Empty;



            using (StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();

                HttpContext.Current.Response.Write(responseData);
                HttpContext.Current.Response.Write("Requested Json= " + final);

            }


        }
       
    }

}