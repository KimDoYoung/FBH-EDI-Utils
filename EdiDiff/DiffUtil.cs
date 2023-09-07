using FBH.EDI.Common;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Excel = Microsoft.Office.Interop.Excel;

namespace EdiDiff
{
    internal class DiffUtil
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;

        internal static object Hub210Merge(string hub210Path)
        {
            //hub210Path파일을 읽어서  route1로 리스트를 만든다.
            string path = hub210Path;

            List<Hub210Item> list1 = new List<Hub210Item>();
            List<Hub210Item> list2 = new List<Hub210Item>();

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];

            if (workbook.Worksheets.Count < 2)
            {
                throw new EdiException("적절하지 않은 Hub 210 Route1, Rout2 엑셀입니다.");
            }

            try
            {
                list1 = GetListFromHub210Route1(workbook.Worksheets[1]);
                MessageEventHandler?.Invoke(null, new MessageEventArgs($"Route1 reading complete, count :{list1.Count}"));
                
                list2 = GetListFromHub210Route2(workbook.Worksheets[2]);
                MessageEventHandler?.Invoke(null, new MessageEventArgs($"Route2 reading complete, count :{list2.Count}"));

                var intersectList = list1.Select(a => a.PoNo).Intersect(list2.Select(b => b.PoNo));
                MessageEventHandler?.Invoke(null, new MessageEventArgs("---- intersectList---"));
                foreach (var item in intersectList)
                {
                    MessageEventHandler?.Invoke(null, new MessageEventArgs(item.ToString()));
                }
                MessageEventHandler?.Invoke(null, new MessageEventArgs("----------------------"));

                var merge = new HashSet<Hub210Item>(list1, new Hub210ItemCompare());
                merge.UnionWith(list2);
                var merged = merge.ToList();
                //새로운 sheet추가
                worksheet = workbook.Sheets.Add(Type.Missing, Type.Missing, 3, Type.Missing);
                worksheet.Name = "Merge";
                //새로운 sheet에 merged list를 가지고 sheet를 만든다.
                CreateMergeSheet(worksheet, merged);

                MessageEventHandler?.Invoke(null, new MessageEventArgs($"Route2 reading complete, count :{list2.Count}"));

                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\hub210_merge_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiException(ex.Message);
            }
            finally
            {
                workbook.Close(false);
                app.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(app);
            }
        }

        private static void CreateMergeSheet(Worksheet worksheet, List<Hub210Item> merged)
        {
            worksheet.SetCell(1, "A", "PAYMENT DATE");
            worksheet.SetCell(1, "B", "PAYMENT DATE");
            worksheet.SetCell(1, "A", "PAYMENT DATE");
            worksheet.SetCell(1, "A", "PAYMENT DATE");
            worksheet.SetCell(1, "A", "PAYMENT DATE");
            worksheet.SetCell(1, "A", "PAYMENT DATE");
            worksheet.SetCell(1, "A", "PAYMENT DATE");
            worksheet.SetCell(1, "A", "PAYMENT DATE");

        }

        private static List<Hub210Item> GetListFromHub210Route2(Excel.Worksheet workSheet)
        {
            List<Hub210Item> list = new List<Hub210Item>();
            int row = 3;
            Hub210Item prev = new Hub210Item();
            while (true)
            {
                var po = workSheet.GetString(row, "B");
                if (string.IsNullOrEmpty(po)) break;

                Hub210Item item = new Hub210Item();
                item.SrcRouteNo = 2;

                item.PaymentDate = workSheet.GetString(row, "A");
                item.PoNo = workSheet.GetString(row, "B");
                item.PickUpDate= workSheet.GetString(row, "C");
                item.Product = workSheet.GetString(row, "D");
                item.Qty = ConvertToInteger(workSheet.GetString(row, "E"));
                item.Amount = ConvertToDecimal(workSheet.GetString(row, "G"));
                item.TotalUsd = workSheet.GetString(row, "H");
                item.DcNo = workSheet.GetString(row, "I");
                item.Address = workSheet.GetString(row, "J");

                if(prev != null && (prev.PoNo == item.PoNo))
                {
                    prev.Qty += item.Qty;
                    prev.Product += "," + item.Product;
                }
                else
                {
                    list.Add(item);
                    prev = item;
                    MessageEventHandler?.Invoke(null, new MessageEventArgs(item.ToString()));
                }
                row++;
            }
            return list;

        }

 

        private static List<Hub210Item> GetListFromHub210Route1(Excel.Worksheet workSheet)
        {
            List<Hub210Item> list = new List<Hub210Item>();
            int row = 4;
            while (true)
            {
                var po = workSheet.GetString(row, "F");
                if (string.IsNullOrEmpty(po)) break;

                Hub210Item item = new Hub210Item();
                item.SrcRouteNo = 1;

                item.PaymentDate = workSheet.GetString(row, "A");
                item.Amount  = ConvertToDecimal(workSheet.GetString(row, "B"));
                item.InvoiceDate= workSheet.GetString(row, "C");
                item.PaymentDue= workSheet.GetString(row, "D");
                item.InvoiceNo= workSheet.GetString(row, "E");
                item.PoNo= workSheet.GetString(row, "F");
                item.Qty = ConvertToInteger(workSheet.GetString(row, "G"));
                item.TotalUsd = workSheet.GetString(row, "H");
                item.DcNo= workSheet.GetString(row, "I");
                item.Address= workSheet.GetString(row, "J");
                list.Add(item);
                MessageEventHandler?.Invoke(null, new MessageEventArgs(item.ToString()));
                row++;
            }
            return list;
        }
        private static decimal? ConvertToDecimal(string v)
        {
            if (string.IsNullOrEmpty(v)) { return null; }
            return Convert.ToDecimal(v);
        }

        private static int? ConvertToInteger(string v)
        {
            if (string.IsNullOrEmpty(v)) { return null; }
            return Convert.ToInt32(v);
        }
        internal static string CreateResultExcel850945(string templateDiffPath, List<DiffItem> listResult)
        {

            MessageEventHandler?.Invoke(null, new MessageEventArgs($"{templateDiffPath} 생성 시작"));

            string path = templateDiffPath;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            worksheet.Name = "Qty-Diff";
            try
            {
                int row = 3;
                foreach (DiffItem item in listResult)
                {
                    worksheet.SetCell(row, "A", item.result, "@");
                    if(item.result == "불일치")
                    {
                        worksheet.SetColor($"A{row}", Color.Red, Color.Yellow);
                    }
                    worksheet.SetCell(row, "B", item.item850.Company, "@");
                    worksheet.SetCell(row, "C", item.item850.Seq, "@");
                    worksheet.SetCell(row, "D", item.item850.PoNumber, "@");
                    worksheet.SetCell(row, "E", CommonUtil.YmdFormat(item.item850.PoOrderDate), "@");
                    worksheet.SetCell(row, "F", item.item850.ItemNumber,"@");
                    worksheet.SetCell(row, "G", item.item850.QuantityCarton);
                    if (item.result == "불일치")
                    {
                        worksheet.SetColor($"G{row}", Color.Red, Color.Yellow);
                    }
                    if (item.item945 != null)
                    {
                        worksheet.SetCell(row, "H", CommonUtil.YmdFormat(item.item945.PickupDate), "@");
                        worksheet.SetCell(row, "I", item.item945.OrderNo, "@");
                        worksheet.SetCell(row, "J", item.item945.RetailersItemNo, "@");
                        worksheet.SetCell(row, "K", item.item945.PalletId, "@");
                        worksheet.SetCell(row, "L", item.item945.SkuNo, "@");
                        worksheet.SetCell(row, "M", item.item945.ActualQuantityShipped);
                        if (item.result == "불일치")
                        {
                            worksheet.SetColor($"M{row}", Color.Red, Color.Yellow);
                        }

                    }
                    row++;
                }
                //align set
                worksheet.SetAlign("D3", $"D{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("E3", $"E{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("F3", $"F{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("G3", $"G{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("I3", $"I{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("J3", $"J{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("K3", $"K{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("L3", $"L{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("M3", $"M{row}", XlHAlign.xlHAlignCenter);

                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\850945_diff_result_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiException(ex.Message);
            }
            finally
            {
                workbook.Close(false);
                app.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(app);
            }
        }

        internal static List<DiffItem> Diff850945(List<Item850> list850, List<Item945> list945)
        {
            List< DiffItem > resultList = new List< DiffItem >();
            foreach (var item850 in list850)
            {
                Item945 item945 = Find945(list945, item850.PoNumber, item850.ItemNumber);
                DiffItem diffItem = new DiffItem();
                diffItem.item850 = item850;
                if (item945 == null)
                {
                    diffItem.item945 = null;
                    diffItem.result = "발견못함";
                }else { 
                    diffItem.item945 = item945;

                    if ( item850.QuantityCarton.Trim()  != item945.ActualQuantityShipped.Trim())
                    {
                        diffItem.result = "불일치";
                    }
                    else
                    {
                        diffItem.result = "일치";
                    }
                }
                resultList.Add(diffItem);  
            }
            return resultList;
        }

        private static Item945 Find945(List<Item945> list945, string poNumber, string ItemNumber)
        {
            foreach (Item945 item945 in list945)
            {
                if( item945.PoNo.Trim() == poNumber.Trim())
                {
                    //string ssn = Regex.Replace(supplierStockNo, "\\D", "");
                    if (ItemNumber.Trim() == item945.RetailersItemNo.Trim())
                    {
                        return item945;
                    }
                }
            }

            return null;
        }

        internal static List<Item850> ReadExcel850(string path850)
        {

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path850);
            Excel.Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                List<Item850> list = new List<Item850>();   
                
                int row = 2;
                while (true)
                {
                    Item850 item = new Item850();
                    item.PoNumber=  worksheet.GetString(row, "C");
                    if (string.IsNullOrEmpty(item.PoNumber)) break;

                    item.Company = worksheet.GetString(row, "A");
                    item.Seq = worksheet.GetString(row, "B");
                    item.PoNumber = worksheet.GetString(row, "C");
                    item.PoOrderDate= worksheet.GetString(row, "D");
                    item.WoY= worksheet.GetString(row, "E");
                    item.ItemGTIN= worksheet.GetString(row, "F");
                    item.ItemNumber= worksheet.GetString(row, "G");

                    item.SupplierStockNo= worksheet.GetString(row, "H");
                    item.Item= worksheet.GetString(row, "I");
                    item.QuantityPack= worksheet.GetString(row, "J");
                    item.QuantityCarton= worksheet.GetString(row, "K");
                    item.Price= worksheet.GetString(row, "O");

                    item.SValue= worksheet.GetString(row, "S");
                    item.TValue= worksheet.GetString(row, "T");
                    item.UValue= worksheet.GetString(row, "U");

                    item.DcNo= worksheet.GetString(row, "V");
                    item.ShipTo = worksheet.GetString(row, "W") + " " + worksheet.GetCell(row, "X");
                    item.GLN= worksheet.GetString(row, "Y");
                    item.DeliveryRefNo = worksheet.GetString(row, "Z");
                    item.CarrierDetail= worksheet.GetString(row, "AA");
                    item.ShipPayment= worksheet.GetString(row, "AB");
                    item.ShipNotBefore= worksheet.GetString(row, "AC");
                    item.ShipNoLaterThan= worksheet.GetString(row, "AD");
                    item.MustArriveBy = worksheet.GetString(row, "AE");


                    list.Add(item);
                    row++;
                    if (row > 5000)
                    {
                        MsgBox.Error("적절하지 않은 850파일입니다. 5000라인이상임");
                        break;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new EdiException(ex.Message);
            }
            finally
            {
                workbook.Close(false);
                app.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(app);
            }
        }

        internal static List<Item945> ReadExcel945(string path945)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path945);
            Excel.Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                List<Item945> list = new List<Item945>();

                int row = 3;
                while (true)
                {
                    Item945 item = new Item945();
                    item.PoNo = worksheet.GetString(row, "C");
                    if (string.IsNullOrEmpty(item.PoNo)) break;

                    item.PickupDate= worksheet.GetString(row, "A");
                    item.OrderNo= worksheet.GetString(row, "B");
                    item.PoNo= worksheet.GetString(row, "C");
                    item.PalletId= worksheet.GetString(row, "D");
                    item.SkuNo= worksheet.GetString(row, "E");
                    item.RequestedQuantity= worksheet.GetString(row, "F");
                    item.ActualQuantityShipped= worksheet.GetString(row, "G");

                    item.LotNo= worksheet.GetString(row, "H");
                    item.RetailersItemNo = worksheet.GetString(row, "I");
                    item.Dc= worksheet.GetString(row, "J");


                    list.Add(item);
                    row++;
                    if (row > 5000)
                    {
                        MsgBox.Error("적절하지 않은 945 파일입니다. 5000라인이상임");
                        break;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new EdiException(ex.Message);
            }
            finally
            {
                workbook.Close(false);
                app.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(app);
            }
        }
        /// <summary>
        /// 메모리 해제
        /// </summary>
        /// <param name="obj"></param>
        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        internal static List<ItemInvoice> ReadInvoice(string invoicePath)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(invoicePath);
            Excel.Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                List<ItemInvoice> list = new List<ItemInvoice>();

                int row = 4;
                while (true)
                {
                    ItemInvoice item = new ItemInvoice();
                    item.WmPoBalJuDay = worksheet.GetString(row, "A");
                    if (string.IsNullOrEmpty(item.WmPoBalJuDay) ) break;

                    item.InvoiceFbhSangsinDay= worksheet.GetString(row, "B");
                    item.InvoiceBalHangDayWalmart= worksheet.GetString(row, "C");
                    item.YeSangDay= worksheet.GetString(row, "D");
                    item.IpGeongTongJiDay= worksheet.GetString(row, "E");
                    item.PoNo= worksheet.GetString(row, "F");
                    item.OrderPack= worksheet.GetString(row, "G");

                    item.TotalUsd= worksheet.GetString(row, "H");
                    item.DcNo= worksheet.GetString(row, "I");
                    item.DcAddr = worksheet.GetString(row, "J");


                    list.Add(item);
                    row++;
                    if (row > 5000)
                    {
                        MsgBox.Error("적절하지 않은 invoice 파일입니다. 5000라인이상임");
                        break;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new EdiException(ex.Message);
            }
            finally
            {
                workbook.Close(false);
                app.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(app);
            }
        }

        internal static List<ItemRLinvoice> ReadRlInvoice(string RLpath)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(RLpath);
            Excel.Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                List<ItemRLinvoice> list = new List<ItemRLinvoice>();

                int row = 4;
                while (true)
                {
                    ItemRLinvoice item = new ItemRLinvoice();
                    item.InvoiceClaimNumber = worksheet.GetString(row, "A");
                    if (string.IsNullOrEmpty(item.InvoiceClaimNumber)) break;

                    item.DivisionNumber = worksheet.GetString(row, "B");
                    item.StoreNumber= worksheet.GetString(row, "C");
                    item.DateClaimCode= worksheet.GetString(row, "D");
                    item.Amount= worksheet.GetString(row, "E");
                    item.MicroNumber= worksheet.GetString(row, "F");
                    item.CheckNumber= worksheet.GetString(row, "G");

                    item.CheckDate= worksheet.GetString(row, "H");
                    item.DeductionCode= worksheet.GetString(row, "I");
                    item.Status = worksheet.GetString(row, "J");


                    list.Add(item);
                    row++;
                    if (row > 5000)
                    {
                        MsgBox.Error("적절하지 않은 RL Invoice 파일입니다. 5000라인이상임");
                        break;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new EdiException(ex.Message);
            }
            finally
            {
                workbook.Close(false);
                app.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(app);
            }
        }

        internal static List<DiffInvoiceItem> DiffInvoice(List<ItemInvoice> listInvoice, List<ItemRLinvoice> listRLinvoice)
        {
            List<DiffInvoiceItem> resultList = new List<DiffInvoiceItem>();
            foreach (var invoice in listInvoice)
            {
                ItemRLinvoice rlItem = FindPoInRL(listRLinvoice, invoice.PoNo);
                DiffInvoiceItem diff = new DiffInvoiceItem();
                diff.invoice = invoice;
                if (rlItem == null)
                {
                    diff.RLinvoice = null;
                    diff.result = "N/A";
                }
                else
                {
                    diff.RLinvoice= rlItem;

                    if (invoice.TotalUsd.Trim() != rlItem.Amount.Trim())
                    {
                        diff.result = "불일치";
                    }
                    else
                    {
                        diff.result = "일치";
                    }
                    if(rlItem.CheckDate == null || rlItem.CheckDate.Trim().Length < 1)
                    {
                        diff.result += " N/D";
                    }
                }
                resultList.Add(diff);
            }
            return resultList;
        }

        private static ItemRLinvoice FindPoInRL(List<ItemRLinvoice> listRLinvoice, string poNo)
        {
            foreach (ItemRLinvoice item in listRLinvoice)
            {
                var po = item.InvoiceClaimNumber.Substring(0, item.InvoiceClaimNumber.Length - 3);
                if (po == poNo) return item;
            }
            return null;
        }

        internal static string CreateResultExcelInvoice(string template, List<DiffInvoiceItem> listResult)
        {
            MessageEventHandler?.Invoke(null, new MessageEventArgs($"{template} 생성 시작"));

            string path = template;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                int row = 3;
                foreach (DiffInvoiceItem item in listResult)
                {
                    worksheet.SetCell(row, "A", item.result, "@");
                    if (item.result != "일치")
                    {
                        worksheet.SetColor($"A{row}", Color.Red, Color.Yellow);
                    }
                    worksheet.SetCell(row, "B", CommonUtil.YmdFormat(item.invoice.WmPoBalJuDay), "@");
                    worksheet.SetCell(row, "C", CommonUtil.YmdFormat(item.invoice.InvoiceFbhSangsinDay), "@");
                    worksheet.SetCell(row, "D", item.invoice.TotalUsd, "@");
                    worksheet.SetCell(row, "E", item.invoice.PoNo, "@");
                    if (item.RLinvoice != null)
                    {
                        worksheet.SetCell(row, "F", CommonUtil.YmdFormat(item.RLinvoice.CheckDate), "@");
                        worksheet.SetCell(row, "G", item.RLinvoice.Amount, "@");
                        worksheet.SetCell(row, "H", item.RLinvoice.Status, "@");
                        if (item.result.Contains("불일치"))
                        {
                            worksheet.SetColor($"D{row}", Color.Red, Color.Yellow);
                            worksheet.SetColor($"G{row}", Color.Red, Color.Yellow);
                        }

                    }
                    row++;
                }
                //align set
                worksheet.SetAlign("D3", $"D{row}", XlHAlign.xlHAlignRight);
                worksheet.SetAlign("E3", $"E{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("F3", $"F{row}", XlHAlign.xlHAlignCenter);
                worksheet.SetAlign("G3", $"G{row}", XlHAlign.xlHAlignRight);
                worksheet.SetAlign("H3", $"H{row}", XlHAlign.xlHAlignCenter);

                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\invoice_diff_result_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiException(ex.Message);
            }
            finally
            {
                workbook.Close(false);
                app.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(app);
            }
        }


    }
}