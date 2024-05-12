using OjasMart.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OjasMart.Controllers
{
    public class VoucherPrintController : Controller
    {
        // GET: VoucherPrint

        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        public ActionResult printVoucher()
        {
            //Session["SbNo"] = "1";
            if (Request.QueryString["VoucherNo"] != null)
            {
                string VoucherNo = Convert.ToString(Request.QueryString["VoucherNo"]), BillNo = "", Date = "", AccountCode = "", AccountDetail = "", Mode = "", narration = "";

                string companyId = "", company_name = "", Address = "", Fax = "", emailId = "", Website = "", Regnos = "", ContactNos = "", imagess = "",vType="",compLogo="";

                double amount = 0;
                objp.InvoiceNo = VoucherNo;
                try
                {
                    if (Convert.ToString(Session["Role"]) == "2")
                    {
                        objp.CompanyCode = Convert.ToString(Session["UserName"]);
                    }
                    else
                    {
                        objp.CompanyCode = Convert.ToString(Session["CompanyCode"]);
                    }
                    DataTable dt = objL.GetVoucherPrintData(objp, "proc_GetVoucherPrintData");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Date = dt.Rows[0]["vDate"].ToString();
                        AccountCode = dt.Rows[0]["AccountCode"].ToString();
                        AccountDetail = dt.Rows[0]["AccountName"].ToString();
                        Mode = dt.Rows[0]["PaymentMode"].ToString();
                        narration = dt.Rows[0]["Narration"].ToString();
                        amount = double.Parse(dt.Rows[0]["Amount"].ToString());
                        vType= dt.Rows[0]["VoucherType"].ToString();


                        company_name = dt.Rows[0]["SSName"].ToString();
                        Address = dt.Rows[0]["Address"].ToString();
                        Fax = "";
                        emailId = dt.Rows[0]["emailAddress"].ToString();
                        Website = "http://kalash.sigmasoftwares.net/";
                        imagess = "";
                        Regnos = dt.Rows[0]["GstNo"].ToString();
                        ContactNos = dt.Rows[0]["ContactNo"].ToString();
                        compLogo = dt.Rows[0]["CompanyLogo"].ToString();
                    }

                    Session["company_name"] = company_name;
                    companyId = objp.CompanyCode;

                    Document doc = new Document(iTextSharp.text.PageSize.A4);
                    PdfPTable tableLayout = new PdfPTable(10);
                    MemoryStream memStream = new MemoryStream();
                    //PdfWriter.GetInstance(doc, memStream);

                    PdfWriter writer = PdfWriter.GetInstance(doc, memStream);
                    //writer.PageEvent = new PDFFooter();

                    float[] headers = { 6, 24, 9, 12, 10, 8, 8, 11, 6, 14 };
                    tableLayout.SetWidths(headers);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.HeaderRows = 12;
                    tableLayout.FooterRows = 1;

                    string imageFilePath = "";
                    imageFilePath = Server.MapPath(compLogo);

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                    jpg.ScaleToFit(130.5f, 110.5f);

                    tableLayout.AddCell(new PdfPCell((jpg)) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2, Rowspan = 3 });

                    tableLayout.AddCell(new PdfPCell(new Phrase(company_name, new Font(Font.GetFamilyIndex("HELVETICA"), 25, 3))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });

                    tableLayout.AddCell(new PdfPCell(new Phrase(Address, new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });

                    tableLayout.AddCell(new PdfPCell(new Phrase(vType, new Font(Font.GetFamilyIndex("HELVETICA"), 12, 3))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8, PaddingLeft = 70 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Email:" + emailId, new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Ph.:" + ContactNos, new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 5 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Website:" + Website, new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Fax:" + Fax, new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 5 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN:" + Regnos, new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 10 });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Voucher No.:" + VoucherNo, new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 2, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Date:" + Date, new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 2, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 6 });



                    tableLayout.AddCell(new PdfPCell(new Phrase("To,", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("A/C Code:" + AccountCode, new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });


                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(AccountDetail, new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9 });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 8, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Mode:" + Mode, new Font(Font.GetFamilyIndex("HELVETICA"), 10, 0))) { Border = 0, PaddingBottom = 8, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 9 });


                    tableLayout.AddCell(new PdfPCell(new Phrase("SNo", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, PaddingBottom = 3, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Particular", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 1, PaddingBottom = 3, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY, Colspan = 8 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Amount", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY });




                    //-----------------------Header End--------------------------------------------

                    //---For Footer space--------------------------
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Colspan = 10, FixedHeight = 70 });
                    //-------------------------------


                    int i = 1;


                    tableLayout.AddCell(new PdfPCell(new Phrase(i.ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(narration, new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Colspan = 8 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(amount.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { BorderWidthRight = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2 });


                    int amt = (int)amount;
                    string AmtInWords = Converter.NumberToWords(amt);


                    tableLayout.AddCell(new PdfPCell(new Phrase("_______________________________________________________", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 2))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 10 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Amount in words:" + AmtInWords, new Font(Font.GetFamilyIndex("HELVETICA"), 9, 2))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Amount:", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 });
                    tableLayout.AddCell(new PdfPCell(new Phrase(amount.ToString("0.00"), new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 });

                    //---------------------------------------------------------------Footer
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 2))) { Border = 0, FixedHeight = 20, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 10 });


                    tableLayout.AddCell(new PdfPCell(new Phrase("1. All disputes are subject to Lucknow Jurisdiction.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 6 });
                    tableLayout.AddCell(new PdfPCell(new Phrase("For " + System.Web.HttpContext.Current.Session["company_name"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 4, PaddingRight = 40 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("2. E. & O.E.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 6 });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Authorised Signatory", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, PaddingTop = 16, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 4, PaddingRight = 40 });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Software Design & Developed By Sigma Softwares    Call:9956973891  Visit:www.sigmasoftwares.org", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, PaddingTop = 4, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 10, PaddingRight = 40 });

                    doc.NewPage();
                    doc.Open(); doc.Add(tableLayout); doc.Close();
                    Response.ClearContent(); Response.ClearHeaders(); Response.ContentType = "application/pdf"; Response.BinaryWrite(memStream.ToArray()); Response.Flush(); Response.Clear(); doc.Dispose(); memStream.Dispose();
                }


                catch (Exception ex)
                {
                    Page p = new Page();
                    ScriptManager.RegisterStartupScript(p, GetType(), "showalert", "alert('" + ex.Message + "');", true);
                }
                finally
                {
                   
                }
            }
            return View();
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

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
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
    public class PDFFooter1 : PdfPageEventHelper
    {

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(10);

            float[] headers = { 4, 10, 9, 10, 28, 8, 8, 7, 6, 10 };
            tabFot.SetWidths(headers);
            tabFot.WidthPercentage = 100;

            PdfPCell cell;
            tabFot.TotalWidth = 550F;

            //----Footer Start---------------------------



            //string Dr1 = "Dr. Divakar Srivastava", Dr2 = "Dr. B.R.Srivastava", PrintedBy = System.Web.HttpContext.Current.Session["UserId"].ToString();

            tabFot.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 2))) { Border = 0, FixedHeight = 20, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 10 });


            tabFot.AddCell(new PdfPCell(new Phrase("1. All disputes are subject to Lucknow Jurisdiction.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 5 });
            tabFot.AddCell(new PdfPCell(new Phrase("For " + System.Web.HttpContext.Current.Session["company_name"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 5, PaddingRight = 40 });

            tabFot.AddCell(new PdfPCell(new Phrase("2. E. & O.E.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 10 });

            tabFot.AddCell(new PdfPCell(new Phrase("Authorised Signatory", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, PaddingTop = 16, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 10, PaddingRight = 40 });


            tabFot.AddCell(new PdfPCell(new Phrase("Software Design & Developed By Sigma Softwares    Call:9956973891  Visit:www.sigmasoftwares.org", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, PaddingTop = 4, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 10, PaddingRight = 40 });

            //----Footer End---------------------------
            //cell = new PdfPCell(new Phrase("Footer"));
            //tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(1, -1, 40, 100, writer.DirectContent);
        }

        //write on close of document
        //public override void OnCloseDocument(PdfWriter writer, Document document)
        //{
        //    base.OnCloseDocument(writer, document);
        //}
    }
}