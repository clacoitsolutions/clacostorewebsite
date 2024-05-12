using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OjasMart
{
    public partial class FailedTranscation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                lblTxnId.Text = Convert.ToString(Request.QueryString["txnId"]);
                lblAmount.Text = Convert.ToString(Request.QueryString["Amount"]);
            }
        }
    }
}