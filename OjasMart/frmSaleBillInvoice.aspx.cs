using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace OjasMart
{
    public partial class frmSaleBillInvoice : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultNewCon"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["InvoiceNo"] != null)
            {
                string InvoiceNo = Request.QueryString["InvoiceNo"].ToString();
                DataTable dt = getDetails(InvoiceNo);

                ReportViewer1.LocalReport.Refresh();
                ReportDataSource rpd = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Add(rpd);

                string mimeType;
                string Encodings;
                string extension;
                string[] streamIds;
                Warning[] warnings;
                byte[] byt = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out Encodings, out extension, out streamIds, out warnings);

                Response.Clear();
                Response.ContentType = mimeType;
                Response.BinaryWrite(byt);
                Response.Flush();
                Response.End();
            }
        }
        public DataTable getDetails(string InvoiceNo)
        {
            DataTable ds = new DataTable();
            try
            {
                con.Open();
                string sql = "Proc_GetSalesInvoiceDetails";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                cmd.Parameters.AddWithValue("@Action", "1");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return ds;
        }
    }
}