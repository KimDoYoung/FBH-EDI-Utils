using FBH.EDI.Common.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
            File.WriteAllText(@"C:\\Users\\deHong\tmp\\1.txt", s);
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
                    result.AppendLine(PAGE_END);
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
            throw new NotImplementedException();
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

    }

}
