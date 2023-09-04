using FBH.EDI.Common;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using Excel = Microsoft.Office.Interop.Excel;

namespace EdiDiff
{
    internal class DiffUtil
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;

        internal static string CreateResultExcel(string templateDiffPath, List<DiffItem> listResult)
        {

            MessageEventHandler?.Invoke(null, new MessageEventArgs($"{templateDiffPath} 생성 시작"));

            string path = templateDiffPath;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                int row = 3;
                foreach (DiffItem item in listResult)
                {
                    worksheet.SetCell(row, "A", item.result, "@");
                    worksheet.SetCell(row, "B", item.item850.Company, "@");
                    worksheet.SetCell(row, "C", item.item850.Seq, "@");
                    worksheet.SetCell(row, "D", item.item850.PoNumber, "@");
                    worksheet.SetCell(row, "E", CommonUtil.YmdFormat(item.item850.PoOrderDate), "@");
                    worksheet.SetCell(row, "F", item.item850.QuantityCarton, "@");
                    if (item.item945 != null)
                    {
                        worksheet.SetCell(row, "G", item.item945.PickupDate, "@");
                        worksheet.SetCell(row, "H", item.item945.OrderNo, "@");
                        worksheet.SetCell(row, "I", item.item945.PalletId, "@");
                        worksheet.SetCell(row, "J", item.item945.SkuNo, "@");
                        worksheet.SetCell(row, "K", item.item945.ActualQuantityShipped, "@");
                    }
                    row++;

                }
                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\diff_result_{time}.xlsx";
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

        internal static List<DiffItem> Diff(List<Item850> list850, List<Item945> list945)
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

    }
}