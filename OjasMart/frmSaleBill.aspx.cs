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
    public partial class frmSaleBill : System.Web.UI.Page
    {
        // Created by Er. Divanshu Shakya

        dbHelper db = new dbHelper();
        LogicClass objL = new LogicClass();
        PropertyClass objp = new PropertyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["InvoiceNo"] != null)
                    {
                        objp.InvoiceNo = Request.QueryString["InvoiceNo"].ToString();
                        BindBillDetails();
                    }
                    else
                    {
                        Response.Redirect("Account/Index");
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("Account/Index");
                }
            }
            
        }

        #region Page load events

        public void BindBillDetails()
        {
            try
            {
                DataTable dt = objL.GetSalesBillInvoiceDetails(objp, "Proc_GetPrintInvoiceDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    #region a
                    try
                    {
                        Document doc = new Document(iTextSharp.text.PageSize.A4);
                        MemoryStream memStream = new MemoryStream();
                        PdfWriter.GetInstance(doc, memStream);
                        iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle(220, 900);
                        doc.SetPageSize(r);
                        doc.SetMargins(30f, 30f, 0f, 0f);
                        PdfPTable tableLayout = new PdfPTable(5);
                        float[] headers = { 10,45, 15, 20, 20 };
                        tableLayout.SetWidths(headers);
                        tableLayout.WidthPercentage = 125;


                        #region Barcode

                        string barCode = "";
                        byte[] byteImage;
                        iTextSharp.text.pdf.Barcode39 code128 = new iTextSharp.text.pdf.Barcode39();
                        code128.CodeType = iTextSharp.text.pdf.Barcode.CODABAR;
                        code128.ChecksumText = false;
                        code128.GenerateChecksum = false;
                        code128.StartStopText = false;
                        code128.GuardBars = true;
                        code128.Code = dt.Rows[0]["InvoiceNo"].ToString();

                        code128.BarHeight = 20;
                        barCode = dt.Rows[0]["InvoiceNo"].ToString();
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White).Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                            byteImage = ms.ToArray();
                            Convert.ToBase64String(byteImage);
                        }

                        iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(byteImage);
                        jpg1.ScaleToFit(120f, 120f);

                        #endregion


                        #region Header

                        string imageFilePath = Server.MapPath(".") + "/img/logo111.png";
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                        tableLayout.AddCell(new PdfPCell((jpg)) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 5, PaddingTop = 30 });
                        
                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["SaleComp"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 11, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["SaleAddress"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Lucknow - 226003", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("*****OM*****", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN/UIN:" + dt.Rows[0]["CompanyGSTNo"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("MO. NO. " + dt.Rows[0]["SaleMobile"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["SaleEmailAdd"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Sale Bill", new Font(Font.GetFamilyIndex("HELVETICA"), 11, 1))) { Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 5,BorderWidthTop=1 });



                        #endregion


                        #region Invoice Details

                        tableLayout.AddCell(new PdfPCell(new Phrase("A/c Of:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Inv No:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3});
                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["PaymentMode"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["InvoiceNo"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 });


                        //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 });
                        tableLayout.AddCell(new PdfPCell((jpg1)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingBottom = 0, BorderWidthRight = 0, PaddingTop = 0, BorderWidthBottom = 0, PaddingLeft = 0, Colspan = 5 });


                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["Name"].ToString() + " - " + dt.Rows[0]["CustMobileNo"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2});
                        tableLayout.AddCell(new PdfPCell(new Phrase("Date: "+ dt.Rows[0]["InvoiceDate"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 });
                      

                        tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[0]["CustAddress"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2});
                        tableLayout.AddCell(new PdfPCell(new Phrase("Time: "+ dt.Rows[0]["InvoiceTime"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3});



                        #endregion

                        #region Item Details

                        tableLayout.AddCell(new PdfPCell(new Phrase("SN", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Item Name", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Qty.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                        tableLayout.AddCell(new PdfPCell(new Phrase("MRP", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Amt.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, BorderColor = iTextSharp.text.BaseColor.BLACK });

                        decimal TotalQuantity = 0, SumTotalQuantity = 0, TotalAmount = 0, SumTotalAmount = 0;
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(k+Convert.ToInt32(1)), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                            tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["ItemName"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                            tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["Quantity"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                            tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["DiscMRP"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                            tableLayout.AddCell(new PdfPCell(new Phrase(dt.Rows[k]["TotalAmount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                            decimal.TryParse(Convert.ToString(dt.Rows[k]["Quantity"].ToString()),out TotalQuantity);
                            decimal.TryParse(Convert.ToString(dt.Rows[k]["TotalAmount"].ToString()), out TotalAmount);
                            SumTotalQuantity += TotalQuantity;
                            SumTotalAmount += TotalAmount;
                        }
                        
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT,Colspan=1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Totals:", new Font(Font.GetFamilyIndex("HELVETICA"), 11, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT,Colspan=1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(SumTotalQuantity), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT,Colspan=1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(SumTotalAmount), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT,Colspan=2 });


                        //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("OfferCode:" + dt.Rows[0]["OfferCode"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 });


                        //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("OfferAmt:" + dt.Rows[0]["OfferDiscAmt"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 });


                        int amt = (int)Math.Round(SumTotalAmount, 0);
                        string AmtInWords = Converter.NumberToWords(amt);
                        tableLayout.AddCell(new PdfPCell(new Phrase("( "+ AmtInWords + " )", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });


                        //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                        //tableLayout.AddCell(new PdfPCell(new Phrase("Cash Tendered:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 1 });
                        //tableLayout.AddCell(new PdfPCell(new Phrase("₹ 787.00", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 });
                        //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                        //tableLayout.AddCell(new PdfPCell(new Phrase("Change Returned:", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 1 });
                        //tableLayout.AddCell(new PdfPCell(new Phrase("₹ 787.00", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 });

                        #endregion

                        #region Footer

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5,BorderWidthBottom=1,PaddingTop=10 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("1. All major Debit Credit cards and Sodexo passes are aceepred.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("2. E.& O.E.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("3. only packed items will be exchanged within 7 days with bill except.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("4. Exchanged only 2pm to 9pm.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 ,PaddingBottom=10});

                        tableLayout.AddCell(new PdfPCell(new Phrase("A Software By: Sigma IT Softwares,Lucknow. Ph:0522-4009986,8957542194", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });

                        #endregion

                        

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
                else
                {
                    Response.Redirect("Account/Index");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("Account/Index");
            }
        }

        #endregion
        
        #region Number convert to words

        public class Converter
        {

            public static string NumberToWords(int number)
            {
                string words = "";
                try
                {
                    if (number == 0)
                        return "zero";

                    if (number < 0)
                        return "minus " + NumberToWords(Math.Abs(number));



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
                }
                catch (Exception ex)
                { }
                return words;
            }
        }

        #endregion




    }
}