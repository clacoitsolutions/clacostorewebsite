using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
namespace OjasMart
{
    public partial class ccavResponseHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string workingKey = "92A4E7045F525F426B94913122D361E5";//put in the 32bit alpha numeric key in the quotes provided here
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }

            for (int i = 0; i < Params.Count; i++)
            {
                Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
            }

        }
    }
}