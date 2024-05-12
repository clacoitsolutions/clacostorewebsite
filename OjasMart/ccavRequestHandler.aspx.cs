using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;

namespace OjasMart
{
    public partial class ccavRequestHandler : System.Web.UI.Page
    {
        CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "92A4E7045F525F426B94913122D361E5";//put in the 32bit alpha numeric key in the quotes provided here 	
        string ccaRequest = "";
        public string strEncRequest = "";
        public string strAccessCode = "AVKK82LD94CL83KKLC";// put the access key in the quotes provided here.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var amt = Request.Form["amount"];

                var cc = "tid=1714823441426&merchant_id=3396203&order_id=123654789&amount=1.00&currency=INR&redirect_url=http://192.168.0.89/MCPG.ASP.net.2.0.kit/ccavResponseHandler.aspx&cancel_url=http://192.168.0.96/mcpg_new/iframe/ccavResponseHandler.php&";
                //var k = "tid=1714809418134&merchant_id=3396203&order_id=123654789&amount=1.00&currency=INR&redirect_url=http://192.168.0.89/MCPG.ASP.net.2.0.kit/ccavResponseHandler.aspx&cancel_url=http://192.168.0.96/mcpg_new/iframe/ccavResponseHandler.php&";
                ccaRequest = cc;
                //foreach (string name in Request.Form)
                //{
                //    if (name != null)
                //    {
                //        if (!name.StartsWith("_"))
                //        {
                //            ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
                //            /* Response.Write(name + "=" + Request.Form[name]);
                //              Response.Write("</br>");*/
                //        }
                //    }
                //}

                strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            }
        }
    }
}

