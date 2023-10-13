using FBH.EDI.Common.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace FBH.EDI.Common.ExcelPdfUtils
{
    /// <summary>
    /// iTextSharp 5.5.13.3 사용
    /// 
    /// pdffile-> hug210r2 list-> 201 list
    /// </summary>
    public static class PdfUtil
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;

        private const string PAGE_START = "---->";
        private const string PAGE_END = "<----";
        public static List<Hub210Item> Hub210ListFromPdf(string pdfFileName)
        {
            var s = ExtractTextFromPDF(pdfFileName);
#if DEBUG
            string docPath= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            File.WriteAllText(docPath + @"\1.txt",s);
#endif            
            List<string[]> pages = SplitPages(s);
            List<Hub210Item> list = new List<Hub210Item>();
            foreach (var pageLines in pages)
            {
                Hub210Item item = Hub210FromPageLines(pageLines);
                MessageEventHandler?.Invoke(null, new MessageEventArgs($"invoice no : {item.InvoiceNo} parsing OK"));
                item.ExcelFileName = pdfFileName;
                list.Add(item);
            }
            return list;
        }
        public static List<FreightInvoice210> Freight210ListFromPdf(string pdfFile)
        {
            var s = ExtractTextFromPDF(pdfFile);
#if DEBUG
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            File.WriteAllText(docPath + @"\1.txt", s);
#endif            
            List<string[]> pages = SplitPages(s);
            List<FreightInvoice210> list = new List<FreightInvoice210>();
            foreach (var pageLines in pages)
            {
                FreightInvoice210 item = FreightInvoice210FromPageLines(pageLines);
                list.Add(item);
            }
            return list;
        }


        internal static string ExtractTextFromPDF(string pdfFileName)
        {
            StringBuilder result = new StringBuilder();
            using (PdfReader reader = new PdfReader(pdfFileName))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    result.AppendLine(PAGE_START);
                    ITextExtractionStrategy strategy =    new SimpleTextExtractionStrategy();
                    //ITextExtractionStrategy strategy =    new LocationTextExtractionStrategy();
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                    result.Append(pageText);
                    result.AppendLine("\r\n"+PAGE_END);
                }
            }
            return result.ToString();
        }
        /// <summary>
        /// pdf가 7장이라면 7개의 각 페이지별로 string[]를 만들어서 리스트로 리턴한다
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal static List<string[]> SplitPages(string s)
        {
            string[] lines = s.Split('\n');
            var list = new List<string[]>();
            List<string> pageList = null;
            foreach (string line in lines)
            {
                if (line.Trim().Length < 1) continue;
                var t =  Regex.Replace(line, @"[\u00A0]", " "); //remove nbsp;
                t = t.Trim();
                if(t == PAGE_START)
                {
                    pageList = new List<string>();
                    continue;
                }else if(t == PAGE_END)
                {
                    list.Add( pageList.ToArray() );
                    continue;
                }
                pageList.Add(t);
            }
            return list;
        }
        private static FreightInvoice210 FreightInvoice210FromPageLines(string[] pageLines)
        {
            FreightInvoice210 item = new FreightInvoice210();
            bool IsConsignee = false;
            bool IsConsignee_FirstLine = false;
            bool IsTotal = false;
            bool IsShipper = false;
            bool IsShipper_FirstLine = false;
            foreach (string line in pageLines)
            {
                if (line.Contains("INVOICE DATE:") && line.Contains("PICK-UP DATE:"))
                {
                    //INVOICE DATE: 8/10/2023 PICK-UP DATE: 8/9/2023
                    string REGEX_INVOICE_PICKUP = @"INVOICE DATE:\s+(?<invoice>[0-9/]{8,10})\s+PICK-UP DATE:\s+(?<pickupdate>[0-9/]{8,10})";
                    var dt = Regex.Match(line, REGEX_INVOICE_PICKUP).Groups["invoice"].Value;
                    item.InvoiceDt = Regex.Replace(CommonUtil.MdyToYmd(dt), "\\D", "");

                    //item.= Regex.Match(line, REGEX_INVOICE_PICKUP).Groups["pickupdate"].Value;
                }
                else if (line.Contains("INVOICE#") && line.Contains("Hub Group BOL #"))
                {
                    //INVOICE#: 11029915 Hub Group BOL #: 35809-13830770
                    string REGEX_INVOICE_BOL = @"INVOICE#:\s+(?<invoiceNo>[0-9]+) Hub Group BOL #: (?<bolNo>[0-9\-]+)";
                    item.InvoiceNo = Regex.Match(line, REGEX_INVOICE_BOL).Groups["invoiceNo"].Value;
                    item.VicsBolNo = Regex.Match(line, REGEX_INVOICE_BOL).Groups["bolNo"].Value;
                }
                else if (line.Contains("PAYMENT DUE BY:"))
                {
                    //PAYMENT DUE BY: 9 / 9 / 2023
                    //string REGEX_MDY = @"\b(?<mdy>[\d/]{8,10})\b";
                    //item.PaymentDue = Regex.Match(line, REGEX_MDY).Groups["mdy"].Value;
                }
                else if (line.Contains("PO Number:"))
                {
                    //PO Number: 1929943146
                    //string REGEX_PO = @"\b(?<po>[a-zA-Z0-9]+)\b";
                    //item.PoNo = Regex.Match(line, REGEX_PO).Groups["po"].Value;
                    string[] tmp = line.Split(':');
                    if (tmp.Length > 1)
                    {
                        item.PoNumber = tmp[1].Trim();
                    }
                }
                else if (line.StartsWith("TOTAL AMOUNT DUE:"))
                {
                    //TOTAL AMOUNT DUE: $38.15
                    string REGEX_AMOUNT = @"\$(?<amount>[0-9,]+\.[0-9]{2})\b";
                    var s = Regex.Match(line, REGEX_AMOUNT).Groups["amount"].Value.Replace(",", "");
                    item.AmountCharged = Convert.ToDecimal(s);
                    item.AmountToBePaid = item.AmountCharged;
                }
                else if (line.StartsWith("SHIPPER"))
                {
                    IsShipper = true;
                    IsShipper_FirstLine = true;
                }
                else if (line.StartsWith("CONSIGNEE"))
                {
                    IsShipper = false;
                    IsConsignee = true;
                    IsConsignee_FirstLine = true;
                }
                else if (IsConsignee && line.StartsWith("FOR QUESTIONS ABOUT THIS INVOICE"))
                {
                    //DcNo추출
                    string REGEX_DCNO = @"DC\s*(?<dcno>[0-9]{2,5})[\sA-Z]";
                    item.DcNo = Regex.Match(item.ConsigneeName, REGEX_DCNO).Groups["dcno"].Value;
                    IsConsignee = false;
                    continue;
                }
                else if (IsShipper)
                {
                    if (IsShipper_FirstLine)
                    {
                        item.WarehouseName = line;
                        IsShipper_FirstLine = false;
                    }
                    else
                    {
                        item.WarehouseAddress+= line + " ";
                    }
                }
                else if (IsConsignee)
                {
                    if (IsConsignee_FirstLine)
                    {
                        item.ConsigneeName = line;
                        IsConsignee_FirstLine = false;
                    }
                    else
                    {
                        item.ConsigneeAddress += line + " ";
                    }

                }
                else if (line.Contains(" Mango "))
                {
                    var qty = SplitAndPick(line, " ", 0);
                    item.Memo += $"Mango({qty}) ";
                }
                else if (line.Contains(" Pineapple "))
                {
                    var qty = SplitAndPick(line, " ", 0);
                    item.Memo += $"Pineapple({qty}) ";
                }
                else if (line.Contains(" Mandarin "))
                {
                    var qty = SplitAndPick(line, " ", 0);
                    item.Memo += $"Mandarin({qty}) ";
                }
                else if (line.Contains("Total:"))
                {
                    IsTotal = true;
                }
                else if (IsTotal)
                {
                    var line2 = Regex.Replace(line, @"\s+", " ");
                    var total = SplitAndPick(line2, " ", 0);
                    item.Qty = Convert.ToInt16(total);
                    item.TotalWeight = CommonUtil.ToDecimalOrNull( SplitAndPick(line2, " ", 1) );
                    IsTotal = false;
                }

            }

            return item;
        }

        public static Hub210Item Hub210FromPageLines(string[] pdfOneLines)
        {
            Hub210Item item = new Hub210Item();
            bool IsShipper = false;
            bool IsTotal = false;
            foreach (string line in pdfOneLines)
            {
                if (line.Contains("INVOICE DATE:") && line.Contains("PICK-UP DATE:"))
                {
                    //INVOICE DATE: 8/10/2023 PICK-UP DATE: 8/9/2023
                    string REGEX_INVOICE_PICKUP = @"INVOICE DATE:\s+(?<invoice>[0-9/]{8,10})\s+PICK-UP DATE:\s+(?<pickupdate>[0-9/]{8,10})";
                    item.InvoiceDate = Regex.Match(line, REGEX_INVOICE_PICKUP).Groups["invoice"].Value;
                    item.PickUpDate = Regex.Match(line, REGEX_INVOICE_PICKUP).Groups["pickupdate"].Value;
                }
                else if (line.Contains("INVOICE#") && line.Contains("Hub Group BOL #"))
                {
                    //INVOICE#: 11029915 Hub Group BOL #: 35809-13830770
                    string REGEX_INVOICE_BOL = @"INVOICE#:\s+(?<invoiceNo>[0-9]+) Hub Group BOL #: (?<bolNo>[0-9\-]+)";
                    item.InvoiceNo = Regex.Match(line, REGEX_INVOICE_BOL).Groups["invoiceNo"].Value;
                    item.HubBolNo = Regex.Match(line, REGEX_INVOICE_BOL).Groups["bolNo"].Value;
                }
                else if (line.Contains("PAYMENT DUE BY:"))
                {
                    //PAYMENT DUE BY: 9 / 9 / 2023
                    string REGEX_MDY = @"\b(?<mdy>[\d/]{8,10})\b";
                    item.PaymentDue = Regex.Match(line, REGEX_MDY).Groups["mdy"].Value;
                }
                else if (line.Contains("PO Number:"))
                {
                    //PO Number: 1929943146
                    //string REGEX_PO = @"\b(?<po>[a-zA-Z0-9]+)\b";
                    //item.PoNo = Regex.Match(line, REGEX_PO).Groups["po"].Value;
                    string[] tmp = line.Split(':');
                    if(tmp.Length > 1) {
                        item.PoNo = tmp[1].Trim();
                    }
                }
                else if (line.StartsWith("TOTAL AMOUNT DUE:"))
                {
                    //TOTAL AMOUNT DUE: $38.15
                    string REGEX_AMOUNT = @"\$(?<amount>[0-9,]+\.[0-9]{2})\b";
                    var s = Regex.Match(line, REGEX_AMOUNT).Groups["amount"].Value.Replace(",", "");
                    item.Amount = Convert.ToDecimal(s);
                }
                else if (line.StartsWith("CONSIGNEE"))
                {
                    IsShipper = true;
                }
                else if (IsShipper && line.StartsWith("FOR QUESTIONS ABOUT THIS INVOICE"))
                {
                    //DcNo추출
                    string REGEX_DCNO = @"DC\s*(?<dcno>[0-9]{2,5})[\sA-Z]";
                    item.DcNo = Regex.Match(item.Address, REGEX_DCNO).Groups["dcno"].Value;
                    IsShipper = false;
                    continue;
                }
                else if (IsShipper)
                {
                    item.Address += line + " ";
                }
                else if (line.Contains(" Mango "))
                {
                    var qty = SplitAndPick(line, " ", 0);
                    item.Product += $"Mango({qty}) ";
                }
                else if (line.Contains(" Pineapple "))
                {
                    var qty = SplitAndPick(line, " ", 0);
                    item.Product += $"Pineapple({qty}) ";
                }
                else if (line.Contains(" Mandarin "))
                {
                    var qty = SplitAndPick(line, " ", 0);
                    item.Product += $"Mandarin({qty}) ";
                }
                else if (line.Contains("Total:"))
                {
                    IsTotal = true;
                }
                else if (IsTotal)
                {
                    var total = SplitAndPick(line, " ", 0);
                    item.Qty = Convert.ToInt16(total);
                    IsTotal = false;
                }

            }
            return item;
        }

        private static string SplitAndPick(string line, string splitChars, int index)
        {
            string[] tmp = line.Split(splitChars.ToCharArray());
            if(tmp.Length > index)
            {
                return tmp[index];
            }
            return "";
        }

        //----------------------------------------------------------------------------
        // PO PDF
        //----------------------------------------------------------------------------
        /// <summary>
        /// pdf로 된 PO 문서를 해석해서 리스트로 만들어 리턴한다.
        /// </summary>
        /// <param name="ediFile"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static List<PurchaseOrder850> PurchaseOrder850ListFromPdf(string pdfFileName)
        {
            var s = ExtractTextFromPDF(pdfFileName);
#if DEBUG
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            File.WriteAllText(docPath + @"\po.txt", s);
#endif            
            List<string[]> pages = SplitPages(s);
            List<PurchaseOrder850> list = new List<PurchaseOrder850>();
            foreach (var pageLines in pages)
            {
                if (pageLines.Length < 10) continue;
                PurchaseOrder850 item = Po850FromPageLines(pageLines);
                MessageEventHandler?.Invoke(null, new MessageEventArgs($"PO # : {item.PoNo} parsing OK"));
                item.ExcelFileName = pdfFileName;
                list.Add(item);
            }
            return list;
        }

        private static PurchaseOrder850 Po850FromPageLines(string[] pageLines)
        {
            PurchaseOrder850 item = new PurchaseOrder850();
            var s = "";
            var addressList = new List<string>();
            var IsShipTo = false;
            var IsBillTo = false;
            var IsNote = false;
            var IsDetails = false;
            var IsAllowance = false;
            foreach (string line in pageLines)
            {
                if (line.StartsWith("Purchase Order Number"))
                {
                    item.PoNo = line.Substring("Purchase Order Number".Length).Trim();
                }
                else if (line.StartsWith("Purchase Order Date"))
                {
                    s = line.Substring("Purchase Order Date".Length).Trim();
                    item.PoDt = CommonUtil.MdyToYmd(s).Replace("-", "");
                }
                else if (line.StartsWith("Delivery Reference Number"))
                {
                    s = line.Substring("Delivery Reference Number".Length);
                    item.DeliveryRefNo = s.Trim();
                }
                else if (line.StartsWith("Ship Not Before"))
                {
                    s = line.Substring("Ship Not Before".Length);
                    item.ShipNotBefore = CommonUtil.MdyToYmd(s).Replace("-", "");
                }
                else if (line.StartsWith("Ship No Later Than"))
                {
                    s = line.Substring("Ship No Later Than".Length);
                    item.ShipNoLater = CommonUtil.MdyToYmd(s).Replace("-", "");
                }
                else if (line.StartsWith("Must Arrive By"))
                {
                    s = line.Substring("Must Arrive By".Length);
                    item.MustArriveBy = CommonUtil.MdyToYmd(s).Replace("-", "");
                }
                else if (line.StartsWith("Order Type"))
                {
                    s = line.Substring("Order Type".Length);
                    item.OrderType = s.Trim();
                }
                else if (line.StartsWith("Department"))
                {
                    s = line.Substring("Department".Length);
                    item.DepartmentNo = s.Trim();
                }
                else if (line.StartsWith("Carrier"))
                {
                    s = line.Substring("Carrier".Length);
                    item.CarrierDetail = s.Trim();
                }
                else if(line.StartsWith("Promotional Event"))
                {
                    s = line.Substring("Promotional Event".Length);
                    item.PromotionDealNo = s.Trim();
                }
                else if (line.StartsWith("Ship To"))
                {
                    addressList.Clear();
                    IsShipTo = true;
                    continue;
                }
                else if(line.StartsWith("Payment Terms"))
                {
                    s = line.Substring("Promotional Event".Length);
                    Regex pattern = new Regex(@"NET\s*(?<days>\d{1,2})");
                    Match match = pattern.Match(s.Trim());
                    item.NetDay =  CommonUtil.ToIntOrNull(match.Groups["days"].Value);
                }
                else if (line.StartsWith("Supplier Number"))
                {
                    s = line.Substring("Supplier Number".Length);
                    item.VendorNo = s.Trim();
                }
                else if(line.StartsWith("F.O.B.") && line.Contains("Ship Point") == false)
                {
                    s = line.Substring("F.O.B.".Length);
                    item.ShipPayment = s.Trim();
                }
                else if (line.StartsWith("Bill To"))
                {
                    IsShipTo = false;
                    if (addressList.Count > 0) item.StNm = addressList[0].Trim();
                    if (addressList.Count > 1) item.StAddr = addressList[1].Trim();
                    if (addressList.Count > 2)
                    {
                        //String[] addrItem = Regex.Split(addressList[2].Trim(), @"[, ]+"); 
                        string[] addrItem = addressList[2].Trim().Split(',');
                        if (addrItem.Length > 0) item.StCity = addrItem[0];
                        s = Regex.Replace(addressList[2].Trim(), @"[, ]+", "");
                        item.StCountry = CommonUtil.SubstringFromLast(s, 0, 2);
                        item.StZip = CommonUtil.SubstringFromLast(s, 2, 5);
                        item.StState = CommonUtil.SubstringFromLast(s, 7, 2);
                    }
                    if (addressList.Count > 3)
                    {
                        if (addressList[3].Trim().StartsWith("GLN"))
                        {
                            s = addressList[3].Substring("GLN".Length).Trim();
                            item.StGln = s;
                        }
                    }
                    item.DcNo = BizRule.ExtractDc(item.StNm);
                    addressList.Clear();
                    IsBillTo = true;
                    continue;
                }
                else if (line.StartsWith("Supplier") && line.Contains("Number") == false)
                {
                    IsBillTo = false;
                    if (addressList.Count > 0) item.BtNm = addressList[0].Trim();
                    if (addressList.Count > 1) item.BtAddr = addressList[1].Trim();
                    if (addressList.Count > 2)
                    {
                        //String[] addrItem = Regex.Split(addressList[2].Trim(), @"[, ]+");
                        string[] addrItem = addressList[2].Trim().Split(',');
                        if(addrItem.Length > 0) item.BtCity = addrItem[0].Trim();
                        s = Regex.Replace(addressList[2].Trim(), @"[, ]+", "");
                        item.BtCountry = CommonUtil.SubstringFromLast(s, 0, 2);
                        item.BtZip = CommonUtil.SubstringFromLast(s, 2, 5);
                        item.BtState = CommonUtil.SubstringFromLast(s, 7, 2);

                    }
                    if (addressList.Count > 3)
                    {
                        if (addressList[3].Trim().StartsWith("GLN"))
                        {
                            s = addressList[3].Substring("GLN".Length).Trim();
                            item.BtGln = s;
                        }
                    }
                    addressList.Clear();
                    continue;
                }
                else if (line.StartsWith("Order Instructions"))
                {
                    IsNote = true;
                    addressList.Clear();
                }
                else if (line.StartsWith("Line Item GTIN"))
                {
                    IsNote = false;
                    item.Note = String.Join("\n", addressList.ToArray());
                    IsDetails = true;
                    addressList.Clear();
                    continue;
                }
                else if (line.StartsWith("Allowance / Charge")) //detail parsing
                {
                    IsDetails = false;
                    int seq = 1;
                    foreach (string detailLine in addressList)
                    {
                        //Line	QTY	Measurement	UnitPrice	GTIN-13	Retailer's Item Number	Vender's Item Number	Description	Extended Cost

                        PurchaseOrder850Detail detail = new PurchaseOrder850Detail();
                        item.Details.Add(detail);
                        string[] tmp = detailLine.Split(' ');
                        detail.PoNo = item.PoNo;
                        detail.Seq = seq++;
                        if (tmp.Length > 0) detail.Line = tmp[0].Trim();
                        if (tmp.Length > 1) detail.RetailerItemNo = tmp[1].Trim();
                        if (tmp.Length > 2) detail.Gtin13 = tmp[2].Trim();
                        if (tmp.Length > 3) {; }
                        if (tmp.Length > 4) detail.VendorItemNo = tmp[4].Trim();
                        if (tmp.Length > 5) {; }
                        if (tmp.Length > 6) {; }
                        if (tmp.Length > 7) { detail.Qty = CommonUtil.ToIntOrNull(tmp[7].Trim()); }
                        if (tmp.Length > 8) { detail.Msrmnt = tmp[8].Trim(); }
                        if (tmp.Length > 9) {; }
                        if (tmp.Length > 10) {; }
                        if (tmp.Length > 11) {; }
                        if (tmp.Length > 12) { detail.UnitPrice = CommonUtil.ToDecimalOrNull(tmp[12].Trim()); }
                        if (tmp.Length > 13) { detail.ExtendedCost = CommonUtil.ToDecimalOrNull(tmp[13].Trim()); }

                    }
                    IsAllowance = true;
                    addressList.Clear();
                    continue;
                }
                else if (line.StartsWith("Total Order Amount"))
                {
                    IsAllowance = false;
                    int seq = 1;
                    foreach (string detailLine in addressList)
                    {
                        //Allowance/Charge	Description Code	Amount	Handing Code	Percent
                        PurchaseOrder850Allowance allowance = new PurchaseOrder850Allowance();
                        allowance.PoNo = item.PoNo;
                        allowance.Seq = seq++;
                        allowance.HandlingCd = "02"; // pdf에 없슴, 그냥 하드코딩
                        string[] tmp = detailLine.Split(' ');
                        if(tmp.Length > 0)
                        {
                            if (tmp[0] == "Allowance")
                            {
                                allowance.Charge = "A";
                            }else
                            {
                                allowance.Charge = "C";
                            }
                        }
                        if(tmp.Length> 2)
                        {
                            s = tmp[tmp.Length - 2].Replace("%", "");
                            allowance.Percent = CommonUtil.ToDecimal(s);
                        }
                        //amount
                        s = Regex.Replace(tmp[tmp.Length - 1], @"[()-\.]", "");
                        allowance.Amount = CommonUtil.ToInteger(s);

                        //code
                        s = "";
                        for(int i=1; i< tmp.Length - 2; i++)
                        {
                            s += tmp[i] + " ";
                        }

                        allowance.DescCd = AllowanceCodeTable.findCode(s);
                        item.Allowences.Add(allowance);
                    }
                    continue;
                }

                if (IsShipTo || IsBillTo || IsNote || IsDetails || IsAllowance)
                {
                    addressList.Add(line.Trim());
                    continue;
                }
            }
            return item;
        }
    }

}
