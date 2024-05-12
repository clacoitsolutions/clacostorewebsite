using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using OjasMart.Models;
using System.Data.SqlClient;
using System.IO;

namespace OjasMart
{
    public partial class frmSaleInvoiceThurmalPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dbHelper db = new dbHelper();
            LogicClass objL = new LogicClass();
            PropertyClass objp = new PropertyClass();
            string InvoiceType = "";
            if (Request.QueryString["InvoiceNo"] != null)
            {
                objp.InvoiceNo = Request.QueryString["InvoiceNo"].ToString();
            }

            DataTable dt = objL.GetSalesBillInvoiceDetails(objp, "Proc_GetPrintInvoiceDetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                #region a
                string Bills = ""; //Session["InvoiceNo"].ToString();
                string[] BillNos = null;// Bills.Split('_');
                try
                {
                    Document doc = new Document(iTextSharp.text.PageSize.A4);
                    MemoryStream memStream = new MemoryStream();
                    PdfWriter.GetInstance(doc, memStream);
                    iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle(250, 900);
                    doc.SetPageSize(r);
                    doc.SetMargins(30f, 30f, 0f, 0f);
                    PdfPTable tableLayout = new PdfPTable(4);
                    float[] headers = { 40, 20, 20, 20 };
                    tableLayout.SetWidths(headers);
                    tableLayout.WidthPercentage = 125;
                    //tableLayout.HeaderRows = 11;
                    //tableLayout.FooterRows = 2;

                    string mrpstring = "";
                    string imageFilePath = Server.MapPath(".") + "/img/logo111.png";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    //string imageFilePath = Server.MapPath(".") + "/img/logo.jpg";
                    //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    SqlDataReader dr;
                    //string SRNO=Session["SRNO"].ToString(), TableNo = "", Date = DateTime.Now.ToString("dd-MMM-yyyy");
                    //string TableNo = "1", KOTNo = "1", Date = DateTime.Now.ToString("dd-MMM-yyyy");
                    tableLayout.AddCell(new PdfPCell((jpg)) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 5 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["SaleComp"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 15, 2))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["SaleAddress"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 2))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(InvoiceType, new Font(Font.GetFamilyIndex("HELVETICA"), 12, 4))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN/UIN:" + dt.Rows[0]["CompanyGSTNo"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Ph : " + dt.Rows[0]["SaleMobile"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["SaleEmailAdd"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("Bill No :" + BillNo +"                                       Date : " + BillDate, new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Bill No :" + dt.Rows[0]["InvoiceNo"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Date : " + dt.Rows[0]["InvoiceDate"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 2))) { Border = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("To,", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 2))) { Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("   " + dt.Rows[0]["Name"].ToString() + " - " + dt.Rows[0]["CustMobileNo"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("   " + dt.Rows[0]["CustAddress"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Particular", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Qty", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Rate", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("MRP", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("Dis", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Amt", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });

                    int i = 0, j = 0;
                    decimal totamo = 0, mrp = 0, rate = 0; ;

                    decimal stprice = 0, stpriceSGL = 0, TotalSaving = 0;


                    for (int k = 0; k < dt.Rows.Count; k++)
                    {

                        if (dt.Rows[k]["Sale_TaxIncludeExclude"].ToString() == "Include")
                        {
                            tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["ItemName"].ToString() + "**", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Border = 0 });
                        }
                        else
                        {
                            tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["ItemName"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Border = 0 });
                        }
                        decimal qty = (!string.IsNullOrEmpty(dt.Rows[k]["Quantity"].ToString()) ? Convert.ToDecimal(dt.Rows[k]["Quantity"].ToString()) : 0);
                        tableLayout.AddCell(new PdfPCell(new Phrase(qty.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["Rate"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                        // mrp += double.Parse(ds1.Tables[0].Rows[k]["FAmount"].ToString()) * double.Parse(ds1.Tables[0].Rows[k]["Quantity"].ToString());
                        rate += decimal.Parse(dt.Rows[k]["Rate"].ToString());
                        //storeprice...

                        //tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["MRP"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });

                        decimal mr = 0, rt = 0;

                        decimal.TryParse(Convert.ToString(dt.Rows[k]["MRP"]),out mr);
                        decimal.TryParse(Convert.ToString(dt.Rows[k]["Rate"]), out rt);
                        mrp += mr;

                        decimal tot = mr - rt;
                        




                        //End Storeprice
                        //tableLayout.AddCell(new PdfPCell(new Phrase(tot.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });

                        // tableLayout.AddCell(new PdfPCell(new Phrase(ds1.Tables[0].Rows[k]["DiscountRs"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                        mrpstring = dt.Rows[k]["MRP"].ToString();
                        //if (ds1.Tables[0].Rows[k]["QtyType"].ToString() != "MRP")
                        //{
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(ds1.Tables[0].Rows[k]["Amount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0 });
                        //    totamo += double.Parse(ds1.Tables[0].Rows[k]["Amount"].ToString());
                        //}
                        //else
                        //{
                        double tmrp = double.Parse(dt.Rows[k]["Rate"].ToString()) * double.Parse(dt.Rows[k]["Quantity"].ToString());
                        tableLayout.AddCell(new PdfPCell(new Phrase(tmrp.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0 });
                        totamo += decimal.Parse(dt.Rows[k]["TaxableAmount"].ToString());
                        TotalSaving += tot;
                        //}
                        //}
                        //rdr.Close();
                        //conn.Close();
                    }
                  
                    //if (discount > 0)
                    //{
                    //    tableLayout.AddCell(new PdfPCell(new Phrase("Discount" + "(-):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    //    tableLayout.AddCell(new PdfPCell(new Phrase(discount.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //if (mrpstring != "MRP")
                    //{

                    //}
                    //cmd = new SqlCommand("select Tax,SUM(VatAmt) as VatAmt from "+TableItem+" where InvoiceNo='" + BillNo + "' and  companyid='" + Session["companyid"].ToString() + "' and ISNULL(Tax,'')!='' group by Tax order by Tax", conn);
                    //conn.Open();
                    //rdr = cmd.ExecuteReader();
                    //while (rdr.Read())
                    //{
                    //    tableLayout.AddCell(new PdfPCell(new Phrase(rdr["Tax"].ToString() + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    // tableLayout.AddCell(new PdfPCell(new Phrase(rdr["VatAmt"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //conn.Close();

                    //if (Surcharge > 0)
                    //{
                    //    tableLayout.AddCell(new PdfPCell(new Phrase("Surcharge" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    //    tableLayout.AddCell(new PdfPCell(new Phrase(Surcharge.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}

                    //    if (Tax1 != "")
                    //{
                    // tableLayout.AddCell(new PdfPCell(new Phrase(Tax1 + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    // tableLayout.AddCell(new PdfPCell(new Phrase(Tax1Amt.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //    if (Tax2 != "")
                    //{
                    // tableLayout.AddCell(new PdfPCell(new Phrase(Tax2 + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    // tableLayout.AddCell(new PdfPCell(new Phrase(Tax2Amt.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //if (LoadingUnloadingCharge > 0)
                    //{
                    //    tableLayout.AddCell(new PdfPCell(new Phrase("Loading & Unloading Charges" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    //    tableLayout.AddCell(new PdfPCell(new Phrase(LoadingUnloadingCharge.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //if (Freight > 0)
                    //{
                    //    tableLayout.AddCell(new PdfPCell(new Phrase("Freight" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    //    tableLayout.AddCell(new PdfPCell(new Phrase(Freight.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}

                    decimal totgstAmt = 0, totcgstAmt = 0, totSgstAmt = 0, totIgstAmt = 0, netAmount = 0;
                    totSgstAmt = (!string.IsNullOrEmpty(dt.Rows[0]["SGSTAmount"].ToString()) ? Convert.ToDecimal(dt.Rows[0]["SGSTAmount"].ToString()) : 0);
                    totcgstAmt = (!string.IsNullOrEmpty(dt.Rows[0]["CGSTAmount"].ToString()) ? Convert.ToDecimal(dt.Rows[0]["CGSTAmount"].ToString()) : 0);
                    totIgstAmt = (!string.IsNullOrEmpty(dt.Rows[0]["IGSTAmount"].ToString()) ? Convert.ToDecimal(dt.Rows[0]["IGSTAmount"].ToString()) : 0);
                    totgstAmt = (totSgstAmt + totcgstAmt + totIgstAmt);
                    netAmount = Convert.ToDecimal(totamo);

                    totamo = totamo - totSgstAmt - totcgstAmt;
                    tableLayout.AddCell(new PdfPCell(new Phrase("Sub Total:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, PaddingLeft = 0 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(totamo.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 1, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Total Disc.:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, PaddingLeft = 0 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["TotalDiscount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 1, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Net Total:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, PaddingLeft = 0 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(netAmount.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 1, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    if (TotalSaving > 0)
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase("Total Savings" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(TotalSaving.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }

                    tableLayout.AddCell(new PdfPCell(new Phrase("Paid(-):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, PaddingLeft = 0 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["PaidAmount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 1, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    decimal BalAmt = Convert.ToDecimal(dt.Rows[0]["NetTotal"].ToString()) - Convert.ToDecimal(dt.Rows[0]["PaidAmount"].ToString());

                    tableLayout.AddCell(new PdfPCell(new Phrase("Balance:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, PaddingLeft = 0 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(BalAmt.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 1, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    //tableLayout.AddCell(new PdfPCell(new Phrase("Total Saving:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 1, PaddingLeft = 0 });

                    //TotalSaving = mrp - stpriceSGL;

                    //tableLayout.AddCell(new PdfPCell(new Phrase(TotalSaving.ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 1, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    //if (SGST > 0)
                    //{
                    tableLayout.AddCell(new PdfPCell(new Phrase("SGST Paid" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["SGSTAmount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //if (CGST > 0)
                    //{
                    tableLayout.AddCell(new PdfPCell(new Phrase("CGST Paid" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["CGSTAmount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //if (IGST > 0)
                    //{
                    //tableLayout.AddCell(new PdfPCell(new Phrase("IGST Paid" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["IGSTAmount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}
                    //if (CESS > 0)
                    //{
                    //tableLayout.AddCell(new PdfPCell(new Phrase("CESS Paid" + "(+):", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Border = 0, PaddingLeft = 0 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase(CESS.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    //}

                    decimal FinalAmount = dt.Rows[0]["NetTotal"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["NetTotal"].ToString()) : 0;

                    int amt = (int)Math.Round(FinalAmount, 0);
                    string AmtInWords = Converter.NumberToWords(amt);

                    tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 10 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("In Words : Rupees " + AmtInWords + " Only", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 5, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 10 });
                    // tableLayout.AddCell(new PdfPCell(new Phrase("IGST % : "+IGSTAmt.ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 6, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 10 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Pay Mode : " + dt.Rows[0]["PaymentMode"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 3, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Cashier : " + dt.Rows[0]["Cashier"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 3, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tableLayout.AddCell(new PdfPCell(new Phrase("** Tax Included.", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 3, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 3, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 10 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("NOTE :", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 5))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 5, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 10 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("1. Delievery will be after clearing the cheque.", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("2. Delievery will be after full payment recieved.", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 10 });
                    //tableLayout.AddCell(new PdfPCell(new Phrase("This is a system generated invoice. it doesn't need any signature.", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 6 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 4, Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 10 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Kalash Infra Private Limited", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("2nd Floor Basant Tower Alambagh Lucknow UP – 226005", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Call : 0522-4332920, 9511400124/147", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 });

                    //tableLayout.AddCell(new PdfPCell(new Phrase("Write Your Feedback", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 6 });

                    //tableLayout.AddCell(new PdfPCell(new Phrase("Email:aagmansuparmart@gmail.com ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 6 });
                    //------------------------------------------

                    doc.NewPage();
                    doc.Open(); doc.Add(tableLayout);

                    doc.Close();
                    Response.ClearContent(); Response.ClearHeaders(); Response.ContentType = "application/pdf"; Response.BinaryWrite(memStream.ToArray()); Response.Flush(); Response.Clear(); doc.Dispose(); memStream.Dispose();
                }

                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ex.Message + "');", true);
                }
                finally
                {

                }
                #endregion
            }

        }

        public class Converter
        {

            public static string NumberToWords(int number)
            {
                if (number == 0)
                    return "zero";

                if (number < 0)
                    return "minus " + NumberToWords(Math.Abs(number));

                string words = "";

                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " Lakh ";
                    number %= 1000000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " Thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " Hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "")
                        words += " ";

                    var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                    var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words += " " + unitsMap[number % 10];
                    }
                }

                return words;
            }
        }
    }
}