using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using OjasMart.Models;


namespace OjasMart.Controllers
{

    public class ChallanController : Controller
    {
        // GET: Challan
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        dbHelper obj = new dbHelper();
        public ActionResult Index()
        {
            //Session["SbNo"] = "1";
            string InvoiceType = "";
            if (Session["lInvoiceType"] != null)
            {
                InvoiceType = Session["lInvoiceType"].ToString();
            }

            string TableInnvoice = "", TableItem = "", TableBillNo = "";
            if (InvoiceType == "Tax Invoice")
            {
                if (Session["ChkStatus"] == "F")
                {
                    TableInnvoice = "SalesInvoiceDetail";
                    TableItem = "SalesInvoiceItemsDetail";
                    TableBillNo = "BillNos";
                }
                else if (Session["ChkStatus"] == "T")
                {
                    TableInnvoice = "SalesInvoiceDetail_BackUP";
                    TableItem = "SalesInvoiceItemsDetail_BackUP";
                    TableBillNo = "BillNos_BackUP";
                }
            }
            else if (InvoiceType == "Sales Invoice")
            {
                TableInnvoice = "SalesInvoiceDetail_SalesInvoice";
                TableItem = "SalesInvoiceItemsDetail_SalesInvoice";
                TableBillNo = "BillNos_SalesInvoice";

            }

            SqlConnection conn = new SqlConnection(obj.consStringNew);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;

            string Bills = Session["InvoiceNo"].ToString();
            string[] BillNos = Bills.Split('_');
            string companytin = "";

            string BillNo = Session["InvoiceNo"].ToString(), BillDate = "", AccountCode = "", DueOn = "", PORefNo = "", AccountDetail = "", Tax1 = "", Tax2 = "", DeliveryMan = "", Site = "", Address11 = "", paymode = "", phone = "";
            double GrossAmount = 0, Discount = 0, AdditionalDiscountPer = 0, AdditionalDiscount = 0, Tax1Amt = 0, Tax2Amt = 0, FinalAmount = 0, PaidAmount = 0, BalanceAmount = 0, Surcharge = 0, LoadingUnloadingCharge = 0, TotelMRP = 0, TotelSPrice = 0, CGST = 0, SGST = 0, IGST = 0;
            string Freight = "", Insurance = "";
            string PONo = "", PODate = "", DispatchThrough = "", vehicleNo = "", ConName = "", ConAddress = "", ConGSTIN = "", ConState = "", ConStateCode = "", TransMode = "", DateOfSupply = "", PlaceOfSupply = "", PlaceCode = "";
            double amtcgst = 0;
            try
            {
                //for (int p1 = 0; p1 < 2; p1++)
                //{
                Document doc = new Document(iTextSharp.text.PageSize.A4);

                MemoryStream memStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, memStream);
                writer.PageEvent = new PDFFooter();

                for (int z = 0; z < BillNos.Length; z++)
                {
                    BillNo = BillNos[z].ToString();
                    conn.Open();
                    cmd = new SqlCommand("Select ''  as  PODate,'' PONo,'' DispatchThrough,  convert(varchar,InvoiceDate,103) as InvoiceDate, AccountCode, AccountName, Address,convert(varchar,DueOn,103)  as  DueOn, PORefNo, GrossAmount,  Discount, AdditionalDiscountPer, AdditionalDiscount,  FinalAmount, PaidAmount, BalanceAmount, Tax1, Tax1Amt, Tax2, Tax2Amt,Surcharge,LoadingUnloadingCharge,DeliveryMan,Site,paymode,Freight,Insurance, SGST, CGST, IGST, vehicleNo ,[ConName],[ConAddress],[ConGSTIN],[ConState],[ConStateCode],[TransMode],[DateOfSupply],[PlaceOfSupply],[PlaceCode] from " + TableInnvoice + " where companyid='" + Session["companyid"].ToString() + "' and InvoiceNo='" + BillNo + "'", conn);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        BillDate = rdr["InvoiceDate"].ToString();
                        AccountCode = rdr["AccountCode"].ToString();
                        AccountDetail = rdr["AccountName"].ToString();
                        Address11 = rdr["Address"].ToString();

                        DueOn = rdr["DueOn"].ToString();
                        PORefNo = rdr["PORefNo"].ToString();
                        GrossAmount = double.Parse(rdr["GrossAmount"].ToString());
                        PONo = rdr["PONo"].ToString();
                        PODate = rdr["PODate"].ToString();
                        DispatchThrough = rdr["DispatchThrough"].ToString();
                        Discount = double.Parse(rdr["Discount"].ToString());
                        AdditionalDiscountPer = double.Parse(rdr["AdditionalDiscountPer"].ToString());
                        AdditionalDiscount = double.Parse(rdr["AdditionalDiscount"].ToString());

                        FinalAmount = double.Parse(rdr["FinalAmount"].ToString());
                        PaidAmount = double.Parse(rdr["PaidAmount"].ToString());
                        BalanceAmount = double.Parse(rdr["BalanceAmount"].ToString());

                        Tax1 = rdr["Tax1"].ToString();
                        Tax1Amt = double.Parse(rdr["Tax1Amt"].ToString());
                        Tax2 = rdr["Tax2"].ToString();
                        Tax2Amt = double.Parse(rdr["Tax2Amt"].ToString());
                        Surcharge = double.Parse(rdr["Surcharge"].ToString());
                        paymode = rdr["paymode"].ToString();
                        DeliveryMan = rdr["DeliveryMan"].ToString();
                        Site = rdr["Site"].ToString();
                        LoadingUnloadingCharge = double.Parse(rdr["LoadingUnloadingCharge"].ToString());
                        Freight = rdr["Freight"].ToString();
                        Insurance = rdr["Insurance"].ToString();

                        CGST = double.Parse(rdr["CGST"].ToString());
                        SGST = double.Parse(rdr["SGST"].ToString());
                        IGST = double.Parse(rdr["IGST"].ToString());
                        vehicleNo = rdr["vehicleNo"].ToString();

                        ConName = rdr["ConName"].ToString();
                        ConAddress = rdr["ConAddress"].ToString();
                        ConState = rdr["ConState"].ToString();
                        ConStateCode = rdr["ConStateCode"].ToString();
                        ConGSTIN = rdr["ConGSTIN"].ToString();
                        TransMode = rdr["TransMode"].ToString();
                        if (rdr["DateOfSupply"].ToString() != "")
                        {
                            DateOfSupply = Convert.ToDateTime(rdr["DateOfSupply"].ToString()).ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            DateOfSupply = "";
                        }
                        PlaceOfSupply = rdr["PlaceOfSupply"].ToString();
                        PlaceCode = rdr["PlaceCode"].ToString();
                    }
                    conn.Close();
                    GrossAmount = GrossAmount - Discount - AdditionalDiscount;

                    string TotalQuantity = "";
                    conn.Open();
                    cmd = new SqlCommand("Select SUM(Quantity) as TotalQty,measure_unit from " + TableItem + " where companyid='" + Session["companyid"].ToString() + "' and InvoiceNo='" + BillNo + "' group by measure_unit", conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        TotalQuantity = TotalQuantity + " " + rdr["TotalQty"].ToString() + " " + rdr["measure_unit"].ToString();
                    }
                    conn.Close();


                    double discount = 0;
                    conn.Open();
                    cmd = new SqlCommand("Select isnull(Sum(DiscountRs),0) as discount from " + TableItem + " where InvoiceNo='" + BillNo + "' and  companyid='" + Session["companyid"].ToString() + "' ", conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        discount = double.Parse(rdr["discount"].ToString());
                    }
                    conn.Close();

                    string companyId = "", company_name = "", Address = "", Fax = "", emailId = "", Website = "", Regnos = "", ContactNos = "", imagess = "", Panno = "", bankname = "", bankaccNo = "", bankAcName = "", txtIFscCode = "", statename = "", statecode = "";
                    string ContactNo1 = "", ContactNo2 = "", ContactNo3 = "", ContactNo4 = "", gsttinno = "";

                    conn.Open();
                    cmd = new SqlCommand("SELECT   Panno,     NameOnInvoice,  Address,    Fax, emailId, Website,ImageSize, ( Regno1+' '+Regno2+' '+' '+Regno3+' '+' '+Regno4+' '+ Regno5+' '+Regno6+' '+Regno7+' '+Regno8+' '+ Regno9+' '+ Regno10) as Regnos, ContactNo1,ContactNo2, ContactNo3,ContactNo4,*,isnull(GSTNo,'') GSTNo1,City FROM Add_Company where company_id='" + Session["companyid"].ToString() + "'", conn);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        companytin = rdr["Regno1"].ToString();
                        Panno = rdr["Panno"].ToString();
                        Session["company_name"] = rdr["NameOnInvoice"].ToString();
                        Session["City"] = rdr["City"].ToString();
                        company_name = rdr["NameOnInvoice"].ToString();
                        Session["Company_Name"] = company_name;
                        Address = rdr["Address"].ToString();
                        Fax = rdr["Fax"].ToString();
                        emailId = rdr["emailId"].ToString();
                        Website = rdr["Website"].ToString();
                        imagess = rdr["ImageSize"].ToString();
                        Regnos = rdr["Regnos"].ToString();
                        gsttinno = rdr["GSTNo1"].ToString();
                        //ContactNos = rdr["ContactNos"].ToString();
                        ContactNo1 = rdr["ContactNo1"].ToString();
                        ContactNo2 = rdr["ContactNo2"].ToString();
                        ContactNo3 = rdr["ContactNo3"].ToString();
                        ContactNo4 = rdr["ContactNo4"].ToString();
                        Panno = rdr["Panno"].ToString();
                        bankname = rdr["bankname"].ToString();
                        bankaccNo = rdr["bankaccNo"].ToString();
                        bankAcName = rdr["bankAcName"].ToString();
                        txtIFscCode = rdr["txtIFscCode"].ToString();
                        statecode = rdr["State_ID"].ToString();
                        statename = rdr["State"].ToString();
                    }
                    conn.Close();

                    string CustomerTinNo = "", customerregistrationno = "", CustAddress = "", CustStateName = "", CustStateCode = "";
                    conn.Open();
                    cmd = new SqlCommand("SELECT tin, phone, GSTNo, Address, StateName, StateCode from Accounts_Head where account_code='" + AccountCode + "'", conn);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        CustomerTinNo = rdr["tin"].ToString();
                        phone = rdr["phone"].ToString();
                        customerregistrationno = rdr["GSTNo"].ToString();
                        CustAddress = rdr["Address"].ToString();

                        CustStateName = rdr["StateName"].ToString();
                        CustStateCode = rdr["StateCode"].ToString();
                    }

                    conn.Close();

                    Session["company_name"] = company_name;
                    companyId = Session["companyid"].ToString();

                    MemoryStream memoryStream = new MemoryStream();
                    PdfWriter.GetInstance(doc, (Stream)memoryStream).PageEvent = (IPdfPageEvent)new PDFFooter();
                    PdfPTable tableLayout = new PdfPTable(15);

                    //PdfWriter.GetInstance(doc, memStream);

                    float[] headers = { 3, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
                    tableLayout.SetWidths(headers);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.HeaderRows = 0;

                    string imageFilePath = Server.MapPath(".") + "/img/logo.jpeg";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    for (int cp = 0; cp <= 0; cp++)
                    {
                        if (cp == 0)
                        {
                            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 6, 0))) { Border = 0, PaddingRight = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 15 });
                        }
                        else
                        {
                            tableLayout.AddCell(new PdfPCell(new Phrase("Customer Copy ", new Font(Font.GetFamilyIndex("HELVETICA"), 6, 0))) { Border = 0, PaddingRight = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 15 });
                        }

                        tableLayout.AddCell(new PdfPCell(jpg) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4, FixedHeight = 40, Rowspan = 3 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(company_name, new Font(Font.GetFamilyIndex("Arial"), 15, 1))) { Border = 0, PaddingRight = 30, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingRight = 30, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(Address + ", " + Session["City"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingRight = 30, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingRight = 30, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 });
                        if (Session["ChkStatus"] == "F")
                        {
                            tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN/UIN : " + gsttinno, new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingRight = 30, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9 });
                        }
                        else if (Session["ChkStatus"] == "T")
                        {
                            tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingRight = 30, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 9 });
                        }

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 0))) { Border = 0, PaddingRight = 30, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("INVOICE", new Font(Font.GetFamilyIndex("HELVETICA"), 12, 1))) { Border = 0, BorderWidthLeft = 1, PaddingTop = 7, BorderWidthRight = 1, BorderWidthTop = 1, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 12, Rowspan = 3 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Original for Receipient", new Font(Font.GetFamilyIndex("HELVETICA"), 6, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthTop = 1, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Duplicate for Supplier/Transporter", new Font(Font.GetFamilyIndex("HELVETICA"), 6, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Triplicate for Supplier", new Font(Font.GetFamilyIndex("HELVETICA"), 6, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Reverse Charge : ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Transportation Mode : " + TransMode, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Invoice No.         : " + BillNo, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Date of Supply           : " + DateOfSupply, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Invoice Date       : " + BillDate, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Place of Supply          : " + PlaceOfSupply + "    | State Code : " + PlaceCode, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("State                   : " + statename, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthTop = 1, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 15, FixedHeight = 10 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Details of Receiver | Billed to", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthLeft = 1, BorderWidthRight = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 8 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Consignee Details ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 0, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 7 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Name      : " + AccountDetail, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Name       :" + ConName, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 0, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Address  : " + CustAddress, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });


                        tableLayout.AddCell(new PdfPCell(new Phrase("Address  : " + ConAddress, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN     : " + (customerregistrationno != "" ? customerregistrationno : "N/A"), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN     : " + (ConGSTIN != "" ? ConGSTIN : "N/A"), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthBottom = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("State       : " + CustStateName + "       | State Code     :   " + CustStateCode, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthLeft = 1, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });


                        tableLayout.AddCell(new PdfPCell(new Phrase("State       : " + ConState + "       | State Code     :   " + ConStateCode, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 7 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { Border = 0, BorderWidthRight = 1, BorderWidthTop = 1, BorderWidthLeft = 1, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 15, FixedHeight = 15 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Sl No", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Name of Product / Services", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 4, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("HSN/SAC", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 2, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Qty.", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1, BackgroundColor = BaseColor.LIGHT_GRAY });
                        //
                        tableLayout.AddCell(new PdfPCell(new Phrase("Rate", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Distance", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("MRP", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("per", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Disc.%", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1, BackgroundColor = BaseColor.LIGHT_GRAY });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Total", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 2, BackgroundColor = BaseColor.LIGHT_GRAY });

                        double qtytot = 0, ratetot = 0, amttot = 0, discountot = 0, cgsttot = 0, sgsttot = 0, igsttot = 0;

                        string PrevMemoNo = "";
                        int i = 0, j = 0, p = 0, m = 1; ;
                        cmd = new SqlCommand("select ProductName,HSN, Cast(Quantity as float) as Quantity,measure_unit,MRP,SPrice,UnitCost as Rate,convert(decimal(18,2), isnull(TotalRate, (Quantity*UnitCost))) as Amount,DiscountRs,isnull(CGSTRate,0) CGSTRate,isnull(CGST,0) CGST,isnull(SGSTRate,0) SGSTRate,isnull(SGST,0) SGST,isnull(IGSTRate,0) IGSTRate,isnull(IGST,0) IGST,isnull(DiscountPer,0) DiscountPer, ChallanNo, VehicleNo, Distance, TotalRate from " + TableItem + " where InvoiceNo='" + BillNo + "' and  companyid='" + Session["companyid"].ToString() + "'  order by Sln", conn);
                        conn.Open();
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            i++;

                            if (m == 12)
                            {

                                tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 15, FixedHeight = 20 });

                                p = p + 1;
                                tableLayout.AddCell(new PdfPCell(new Phrase("SUBJECT TO " + Session["City"].ToString().ToUpper() + " JURISDICTION", new Font(Font.GetFamilyIndex("HELVETICA"), 6, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("Invoice No. " + BillNo, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 8 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("Dated ." + BillDate, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 7 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15, FixedHeight = 10 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(company_name, new Font(Font.GetFamilyIndex("Arial"), 15, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });

                                tableLayout.AddCell(new PdfPCell(new Phrase(Address + ", " + Session["City"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });
                                if (Session["ChkStatus"] == "F")
                                {
                                    tableLayout.AddCell(new PdfPCell(new Phrase("GSTIN/UIN : " + gsttinno, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });
                                }
                                else if (Session["ChkStatus"] == "T")
                                {
                                    tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });
                                }
                                i = i + 1;
                                tableLayout.AddCell(new PdfPCell(new Phrase("Mob :" + ContactNo1 + "," + ContactNo2, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("State Name :" + statename + ",Code :" + statecode, new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });


                                tableLayout.AddCell(new PdfPCell(new Phrase("TAX INVOICE", new Font(Font.GetFamilyIndex("HELVETICA"), 9, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("Party :" + AccountDetail + ", " + Address11 + ", " + phone + ", GSTIN/UIN:" + customerregistrationno + "\n\n", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 15 });

                                i = i + 1;
                                tableLayout.AddCell(new PdfPCell(new Phrase("Sl No", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1 });
                                tableLayout.AddCell(new PdfPCell(new Phrase("Description of Goods", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 4 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("HSN/SAC", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 2 });
                                tableLayout.AddCell(new PdfPCell(new Phrase("Qnt.", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1 });
                                //
                                tableLayout.AddCell(new PdfPCell(new Phrase("Rate", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("Distance", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("MRP", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1 });

                                tableLayout.AddCell(new PdfPCell(new Phrase("per", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1 });
                                tableLayout.AddCell(new PdfPCell(new Phrase("Disc.%", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 1 });
                                tableLayout.AddCell(new PdfPCell(new Phrase("Amount", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 1))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, Colspan = 2 });

                                i = i + 1;

                            }
                            tableLayout.AddCell(new PdfPCell(new Phrase(i.ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BorderWidthRight = 1, BorderWidthLeft = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["ProductName"].ToString() + "\nChallan No. : " + rdr["ChallanNo"].ToString() + "\nVehicle No. : " + rdr["VehicleNo"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Colspan = 4, BorderWidthRight = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["HSN"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BorderWidthRight = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["Quantity"].ToString() + " " + rdr["measure_unit"].ToString().ToLower() + ".", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 1, BorderWidthRight = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["Rate"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["Distance"].ToString() + " km", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["MRP"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["measure_unit"].ToString().ToLower() + ".", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });


                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["DiscountPer"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(rdr["Amount"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BorderWidthRight = 1 });
                            m = m + 1;

                            qtytot += double.Parse(rdr["Quantity"].ToString());
                            ratetot += double.Parse(rdr["Rate"].ToString());
                            amttot += double.Parse(rdr["Amount"].ToString());
                            discountot += double.Parse(rdr["DiscountRs"].ToString());
                            cgsttot += double.Parse(rdr["CGST"].ToString());
                            sgsttot += double.Parse(rdr["SGST"].ToString());
                            igsttot += double.Parse(rdr["IGST"].ToString());
                            TotelMRP += double.Parse(rdr["MRP"].ToString());
                            TotelSPrice += double.Parse(rdr["SPrice"].ToString());
                        }

                        conn.Close();
                        //cgsttot = (amttot * 14) / 100;
                        //sgsttot = (amttot * 14) / 100;


                        for (int k = 0; k <= 7 - i; k++)
                        {


                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BorderWidthLeft = 1, BorderWidthRight = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 4, BorderWidthRight = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BorderWidthRight = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 1, BorderWidthRight = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1 });

                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });
                            tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 2, BorderWidthRight = 1 });
                        }

                        //----------------------------------
                        i = i + 1;
                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BorderWidthLeft = 1, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 4, BorderWidthRight = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 1, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, FixedHeight = 150 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2, Padding = 2, BorderWidthRight = 1 });
                        i = i + 1;

                        cmd = new SqlCommand("with cte as (select  'SGST on Sale @ '+ cast(cast(SGSTRate  as Decimal(18,1)) as varchar) as SGSTCol,cast(cast(SGSTRate  as  Decimal(18,2)) as varchar) Rate,sum(SGST) SGST from " + TableItem + " where InvoiceNo='" + BillNo + "' and  companyid='" + Session["companyid"].ToString() + "' and SGST<>'0' group by SGSTRate union all select 'CGST on Sale @ '+ cast(cast(CGSTRate  as  Decimal(18,2)) as varchar) as SGSTCol,cast(cast(CGSTRate  as  Decimal(18,2)) as varchar) Rate,sum(CGST) SGST from " + TableItem + " where InvoiceNo='" + BillNo + "' and  companyid='" + Session["companyid"].ToString() + "' and CGST<>'0' group by CGSTRate Union all select 'IGST on Sale @ '+ cast(cast(IGSTRate  as  Decimal(18,2)) as varchar) as SGSTCol,cast(cast(IGSTRate  as  Decimal(18,2)) as varchar) Rate,sum(IGST) SGST from " + TableItem + " where InvoiceNo='" + BillNo + "' and  companyid='" + Session["companyid"].ToString() + "' and IGST<>'0' group by IGSTRate ) select * from cte  order by SGSTCol desc", conn);

                        //conn.Open();
                        //rdr = cmd.ExecuteReader();
                        //while (rdr.Read())
                        //{
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, BorderWidthLeft = 1, BorderWidthRight = 1 });
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(rdr["SGSTCol"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 4, BorderWidthRight = 1 });
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BorderWidthRight = 1 });
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 1, BorderWidthRight = 1 });
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 2 });

                        //    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1 });
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1 });

                        //    tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, BorderWidthRight = 1, Colspan = 1 });
                        //    tableLayout.AddCell(new PdfPCell(new Phrase(rdr["SGST"].ToString(), new Font(Font.GetFamilyIndex("HELVETICA"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BorderWidthRight = 1 });
                        //}
                        //conn.Close();

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("HELVETICA"), 7, 0))) { Border = 0, BorderWidthLeft = 1, FixedHeight = 15, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 15, BorderWidthBottom = 1, BorderWidthRight = 1, });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Tax Invoice Amount in Words", new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 9, BorderWidthLeft = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Total Amount Before Tax : ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthLeft = 1, BackgroundColor = BaseColor.LIGHT_GRAY, Colspan = 4, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(GrossAmount.ToString("0.00"), new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BackgroundColor = BaseColor.LIGHT_GRAY, BorderWidthRight = 1 });

                        //---------------------------------------------------------------------------
                        string amt = FinalAmount.ToString();
                        string AmtInWords = Converter.conversion(amt);
                        tableLayout.AddCell(new PdfPCell(new Phrase(AmtInWords + " Only", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 0, PaddingTop = 25, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 9, Rowspan = 4, BorderWidthLeft = 1, BorderWidthRight = 0, BorderWidthBottom = 1, });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Add : CGST : ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthLeft = 1, Colspan = 4, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(CGST.ToString("0.00"), new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthTop = 1, Padding = 2, Colspan = 2, BorderWidthRight = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Add : SGST  : ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthLeft = 1, Colspan = 4, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(SGST.ToString("0.00"), new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthTop = 1, Padding = 2, Colspan = 2, BorderWidthRight = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Add : IGST : ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthLeft = 1, Colspan = 4, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(IGST.ToString("0.00"), new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthTop = 1, Padding = 2, Colspan = 2, BorderWidthRight = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Total Amount : GST :", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor = BaseColor.LIGHT_GRAY, BorderWidthBottom = 1, Padding = 2, BorderWidthLeft = 1, Colspan = 4, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase((CGST + SGST + IGST).ToString("0.00"), new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthTop = 1, Padding = 2, Colspan = 2, BackgroundColor = BaseColor.LIGHT_GRAY, BorderWidthBottom = 1, BorderWidthRight = 1 });


                        tableLayout.AddCell(new PdfPCell(new Phrase(" : Bank Details : ", new Font(Font.GetFamilyIndex("ARIAL"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 6, BorderWidthLeft = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("ARIAL"), 8, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 3, BorderWidthLeft = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Total Amount After Tax : ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthLeft = 1, BackgroundColor = BaseColor.LIGHT_GRAY, Colspan = 4, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(FinalAmount.ToString("0.00"), new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 2, Colspan = 2, BackgroundColor = BaseColor.LIGHT_GRAY, BorderWidthRight = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Bank Account Number : " + bankaccNo, new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 0, PaddingTop = 25, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Colspan = 6, BorderWidthLeft = 1, BorderWidthRight = 0, BorderWidthBottom = 0, });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 3, BorderWidthLeft = 1, BorderWidthRight = 0, BorderWidthBottom = 0, });


                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthLeft = 1, Colspan = 4, BorderWidthRight = 0 });
                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthTop = 1, Padding = 2, Colspan = 2, BorderWidthRight = 1 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Bank Branch IFSC : " + txtIFscCode, new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, Colspan = 6, BorderWidthLeft = 1, BorderWidthRight = 0, BorderWidthBottom = 0, });

                        tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 0, PaddingTop = 25, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 3, BorderWidthLeft = 1, BorderWidthRight = 0, BorderWidthBottom = 0, });

                        tableLayout.AddCell(new PdfPCell(new Phrase("GST Payable on Reverse Charge ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthLeft = 1, Colspan = 4, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("0.00", new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthTop = 1, Padding = 2, Colspan = 2, BorderWidthRight = 1, BackgroundColor = BaseColor.LIGHT_GRAY });


                        tableLayout.AddCell(new PdfPCell(new Phrase(" : Terms and Condition :", new Font(Font.GetFamilyIndex("ARIAL"), 8, 1))) { Border = 0, BorderWidthTop = 1, PaddingTop = 25, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2, Colspan = 6, BorderWidthLeft = 1, BorderWidthRight = 0, BorderWidthBottom = 1, FixedHeight = 100, Rowspan = 2 });

                        tableLayout.AddCell(new PdfPCell(new Phrase("(Common Seal) ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, BorderWidthTop = 1, PaddingTop = 70, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3, BorderWidthLeft = 1, BorderWidthRight = 0, BorderWidthBottom = 1, Rowspan = 2 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Certified that the particulars given above are true and Correct ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 0))) { Border = 0, BorderWidthTop = 1, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 2, BorderWidthBottom = 0, BorderWidthLeft = 1, Colspan = 6, BorderWidthRight = 1 });
                        tableLayout.AddCell(new PdfPCell(new Phrase("For " + company_name, new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, PaddingTop = 25, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidthBottom = 1, BorderWidthLeft = 1, Colspan = 6, BorderWidthRight = 1 });


                        tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.GetFamilyIndex("ARIAL"), 7, 1))) { Border = 0, PaddingTop = 25, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidthBottom = 0, BorderWidthLeft = 0, Colspan = 15, BorderWidthRight = 0 });


                        tableLayout.AddCell(new PdfPCell { Border = 0, PaddingTop = 25, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidthBottom = 0, BorderWidthLeft = 0, Colspan = 15, BorderWidthRight = 0 });

                        conn.Close();

                        string taxamt = (cgsttot + sgsttot + igsttot).ToString();
                        string taxInWords = Converter.conversion(taxamt);

                        string senter = "";

                        doc.NewPage();

                    }
                    //  }
                    doc.Open(); doc.Add(tableLayout);
                    //Response.ClearContent();

                }

                //rdr.Close(); rdr.Dispose(); cmd.Dispose();
                doc.Close();

                Response.ClearContent(); Response.ClearHeaders(); Response.ContentType = "application/pdf"; Response.BinaryWrite(memStream.ToArray()); Response.Flush(); Response.Clear(); doc.Dispose(); memStream.Dispose();
            }
            //}


            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ex.Message + "');", true);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return View();
        }

        public class PDFFooter : PdfPageEventHelper
        {

            // This is the contentbyte object of the writer
            PdfContentByte cb;

            // we will put the final number of pages in a template
            PdfTemplate headerTemplate, footerTemplate;

            // this is the BaseFont we are going to use for the header / footer
            BaseFont bf = null;

            // This keeps track of the creation time
            DateTime PrintTime = DateTime.Now;


            #region Fields
            private string _header;
            #endregion

            #region Properties
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion


            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb = writer.DirectContent;
                    headerTemplate = cb.CreateTemplate(100, 100);
                    footerTemplate = cb.CreateTemplate(50, 50);
                }
                catch (DocumentException de)
                {

                }
                catch (System.IO.IOException ioe)
                {

                }
            }

            public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
            {


            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                headerTemplate.BeginText();
                headerTemplate.SetFontAndSize(bf, 10);
                headerTemplate.SetTextMatrix(0, 0);
                headerTemplate.ShowText((writer.PageNumber - 1).ToString());
                headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber - 1).ToString());
                footerTemplate.EndText();
            }

        }








        public class Converter
        {
            public static string conversion(string amount)
            {
                string Content = "";
                double m = Convert.ToInt64(Math.Floor(Convert.ToDouble(amount)));
                double l = Convert.ToDouble(amount);

                double j = (l - m) * 100;


                var beforefloating = NumberToWords(Convert.ToInt64(m));
                var afterfloating = NumberToWords(Convert.ToInt64(j));



                return Content = beforefloating + ' ' + " Rupees " + ' ' + afterfloating + ' ' + " Paise ";

            }
            public static string NumberToWords(long number)
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
    }
}