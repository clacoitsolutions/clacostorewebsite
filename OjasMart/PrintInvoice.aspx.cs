using iTextSharp.text;
using iTextSharp.text.pdf;
using OjasMart.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OjasMart
{

    public partial class PrintInvoice : System.Web.UI.Page
    {
        dbHelper db = new dbHelper();
        LogicClass objL = new LogicClass();
        PropertyClass objp = new PropertyClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["OrderId"]))
                    {
                        BindBillDetails();
                    }
                    else { Response.Redirect("Account/Index"); }
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
                objp.Action = "1";
                objp.OrderId = Convert.ToString(Request.QueryString["OrderId"]).Trim();
               


                DataTable dt = objL.GetOnlineInvoiceDetails(objp, "proc_PrintonlineInvoice");

               // DataTable dt = objL.GetSalesBillInvoiceDetails(objp, "Proc_GetPrintInvoiceDetails");
                if (dt != null && dt.Rows.Count > 0)
                {
                    #region a
                    try
                    {
                        Document doc = new Document(iTextSharp.text.PageSize.A4);
                        MemoryStream memStream = new MemoryStream();
                        PdfWriter.GetInstance(doc, memStream);
                        //iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle(220, 900);
                        //doc.SetPageSize(r);
                       
                        PdfPTable tableLayout = new PdfPTable(13);
                        float[] headers = { 6, 12, 1, 9,8, 7, 9, 8, 8, 8, 9, 8, 8 };
                        tableLayout.SetWidths(headers);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.HeaderRows = 0;
                        tableLayout.FooterRows = 0;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/logo111.png"));
                        jpg.ScaleToFit(120F, 120F);




                        tableLayout.AddCell(new PdfPCell(new Phrase("Tax Invoice/Bill  of Supply", new Font(Font.GetFamilyIndex("HELVETICA"), 20, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 13, BorderWidthRight = 1, BorderWidthLeft = 1 });




                        tableLayout.AddCell(new PdfPCell((jpg)) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4, BorderWidthRight = 1, BorderWidthLeft = 1, Rowspan = 5, PaddingTop = 15, BorderWidthBottom = 1, }) ;



                        tableLayout.AddCell(new PdfPCell(new Phrase("Claco", new Font(Font.GetFamilyIndex("HELVETICA"), 15, 1))) { Border = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9,BorderWidthRight = 1, BorderWidthLeft = 1 });
                      


                        tableLayout.AddCell(new PdfPCell(new Phrase("Lucknow, Uttar Pradesh, 226022", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) {HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9, BorderWidthRight=1, BorderWidthLeft=1, BorderWidthBottom = 0, BorderWidthTop = 0 });
                       

                        tableLayout.AddCell(new PdfPCell(new Phrase("Contact us @ +91-xxxxx   ", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthBottom = 0, BorderWidthTop = 0 });
                       
                        tableLayout.AddCell(new PdfPCell(new Phrase("Mail us at    ", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthBottom = 0, BorderWidthTop = 0 });

                        
                        tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN:    ", new Font(Font.GetFamilyIndex("HELVETICA"), 10, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 0 });

 



                        tableLayout.AddCell(new PdfPCell(new Phrase("Order Number. : " + Convert.ToString(dt.Rows[0]["OrderId"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 6, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Name : " + Convert.ToString(dt.Rows[0]["Name"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("Order Date. : " + Convert.ToString(dt.Rows[0]["OrderDate"])+" " + Convert.ToString(dt.Rows[0]["OrderTime"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 6, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });


                        tableLayout.AddCell(new PdfPCell(new Phrase("Address : " + Convert.ToString(dt.Rows[0]["Add1"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });




                     


                        tableLayout.AddCell(new PdfPCell(new Phrase("Mode : " + Convert.ToString(dt.Rows[0]["PaymentMode"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 6, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Mobile No. : " + Convert.ToString(dt.Rows[0]["MobileNo"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });



                        tableLayout.AddCell(new PdfPCell(new Phrase(" " , new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 6, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN: " + Convert.ToString(dt.Rows[0]["CGSTIN"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingLeft = 5 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("S.N.", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Items Name", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Qty", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });


                        tableLayout.AddCell(new PdfPCell(new Phrase("MRP", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });


                        tableLayout.AddCell(new PdfPCell(new Phrase("Rate", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        //tableLayout.AddCell(new PdfPCell(new Phrase("Sub Total", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("CGST", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("SGST", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("Amt", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1, BorderWidthTop = 1 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Rate %", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Amount", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Rate %", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Amount", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1, BorderWidthTop = 1 });


                        decimal totalcost = 0; decimal accamt = 0; decimal t_grossamt = 0; decimal t_gstamt = 0;

                        decimal totalgst = 0; decimal totalcorier = 0;

                        decimal SumTotalQuantity = 0; decimal TotalQuantity = 0; decimal TotalAmount = 0; decimal SumTotalAmount = 0;
                        decimal gstamount = 0;
                        decimal TotalMRP = 0;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int count = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                count++;
                                decimal.TryParse(Convert.ToString(dt.Rows[i]["Quantity"].ToString()), out TotalQuantity);
                                decimal.TryParse(Convert.ToString(dt.Rows[i]["UnitRate"].ToString()), out TotalAmount);
                                decimal.TryParse(Convert.ToString(dt.Rows[i]["GstAmount"].ToString()), out gstamount);
                                TotalAmount = TotalQuantity * TotalAmount;
                                SumTotalQuantity += TotalQuantity;
                                SumTotalAmount += TotalAmount;
                                totalgst += gstamount;
                                TotalMRP+= Convert.ToDecimal(dt.Rows[i]["TotalMRP"]);

                                //totalcost += Convert.ToDecimal(dt.Rows[i]["Quantity"]);

                                //t_grossamt += Convert.ToDecimal(dt.Rows[i]["GrossAmount"]);
                                //t_gstamt += Convert.ToDecimal(dt.Rows[i]["GSTAmt"]);


                                //OrderProductId = Convert.ToString(dt.Rows[i]["OrderProductId"]);
                                //OrderProductVarId = Convert.ToString(dt.Rows[i]["OrderProductVarId"]);





                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(count), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["ItemName"]) + ' ' + Convert.ToString(dt.Rows[i]["VarriationName"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["Quantity"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["MRP"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["UnitRate"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });


                                tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });
                                //tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(TotalAmount), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["CGstPer"]), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["CGstAmount"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["SGstPer"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["SGstAmount"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 1, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthTop = 1 });



                                tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[i]["TotalAmount"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1, BorderWidthTop = 1 });



                                








                            }
                        }

                        

                             tableLayout.AddCell(new PdfPCell(new Phrase("Total MRP", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });

                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(TotalMRP), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("Discount", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });



                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[0]["DiscountAmount"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("Sale Price", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });








                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(SumTotalAmount), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("Gst Charge", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });



                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(totalgst), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });





                     





                        tableLayout.AddCell(new PdfPCell(new Phrase("Delivery Charge", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });



                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[0]["DeliveryCharges"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });









                        tableLayout.AddCell(new PdfPCell(new Phrase("PromoCode", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 9, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });
                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[0]["CoupenAmount"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });



                        //tableLayout.AddCell(new PdfPCell(new Phrase("Convenience Charges", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });
                        //tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString("0"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 8, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });

                        //tableLayout.AddCell(new PdfPCell(new Phrase("Gst Charges", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });
                        //tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString("0"), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Colspan = 8, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("Payable Amount", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Colspan = 5, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, });
                        tableLayout.AddCell(new PdfPCell(new Phrase(Convert.ToString(dt.Rows[0]["NetPayable"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 8, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1, PaddingBottom = 3, Border = 0, BorderWidthLeft = 1, BorderWidthBottom = 1, BorderWidthRight = 1 });




                        tableLayout.AddCell(new PdfPCell(new Phrase("Amount(In Words)Rs. " + Converter.NumberToWords(Convert.ToInt32(dt.Rows[0]["NetPayable"])) +" "+ "only", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Colspan = 15, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 5, PaddingTop = 10, PaddingBottom = 10, Border = 0, BorderWidthLeft = 1, BorderWidthRight = 1 } );




                        // tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 4, PaddingRight = 20, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ------------------------------------------------------------------------------", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 13, BorderWidthLeft = 1, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Thanks for shopping with us ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 13, PaddingRight = 20, BorderWidthRight = 1, BorderWidthLeft = 1, });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Visit again for more details", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 13, BorderWidthLeft = 1, BorderWidthRight = 1, PaddingRight = 20 });





                        tableLayout.AddCell(new PdfPCell(new Phrase("Terms & Conditions" + Convert.ToString(""), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("" + Convert.ToString(""), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4, PaddingRight = 20, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthTop = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("The goods sold as are intended for end user consumption and not for re-sale" + Convert.ToString(""), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthRight = 0, PaddingRight = 20 });



                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4, BorderWidthLeft = 1, BorderWidthRight = 1, BorderWidthBottom = 0 });





                        //tableLayout.AddCell(new PdfPCell(new Phrase("Bank Name. " + Convert.ToString(dtCmp.Rows[0]["CompanyBankName"]), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthRight = 0 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("" + Convert.ToString(""), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthRight = 0, PaddingRight = 20 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4, BorderWidthLeft = 1, BorderWidthRight = 1, BorderWidthBottom = 0 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Goods once sold will not be taken back or exchange " + Convert.ToString(""), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthRight = 0, PaddingRight = 20 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4, BorderWidthLeft = 1, BorderWidthRight = 1, BorderWidthBottom = 0 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("" + Convert.ToString(""), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthRight = 0 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4, BorderWidthLeft = 1, BorderWidthRight = 1, BorderWidthBottom = 0 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("" + Convert.ToString(""), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthRight = 0 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4, BorderWidthLeft = 1, BorderWidthRight = 1, BorderWidthBottom = 0 });










                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 9, BorderWidthLeft = 1, BorderWidthBottom = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Authorized signatory", new Font(Font.GetFamilyIndex("HELVETICA"), 8, 0))) { Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthBottom = 1 });




                        doc.NewPage();
                        doc.Open(); doc.Add(tableLayout);

                        doc.Close();
                        Response.ClearContent(); Response.ClearHeaders(); Response.ContentType = "application/pdf"; Response.BinaryWrite(memStream.ToArray()); Response.Flush(); Response.Clear(); doc.Dispose(); memStream.Dispose();







































                        //#region Barcode

                        //string barCode = "";
                        //byte[] byteImage;
                        //iTextSharp.text.pdf.Barcode39 code128 = new iTextSharp.text.pdf.Barcode39();
                        //code128.CodeType = iTextSharp.text.pdf.Barcode.CODABAR;
                        //code128.ChecksumText = false;
                        //code128.GenerateChecksum = false;
                        //code128.StartStopText = false;
                        //code128.GuardBars = true;
                        //code128.Code = dt.Rows[0]["OrderId"].ToString();

                        //code128.BarHeight = 20;
                        //barCode = dt.Rows[0]["OrderId"].ToString();
                        //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

                        //using (MemoryStream ms = new MemoryStream())
                        //{
                        //    code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White).Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                        //    byteImage = ms.ToArray();
                        //    Convert.ToBase64String(byteImage);
                        //}

                        //iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(byteImage);
                        //jpg1.ScaleToFit(120f, 120f);

                        //#endregion


                        



                      
                        



                     
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