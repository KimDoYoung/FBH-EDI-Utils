using EdiName.Common;
using EdiRename.Common;
using kr.co.kalpa.common.CommonUtil;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using Excel = Microsoft.Office.Interop.Excel;

namespace EdiName
{
    internal class RenameUtils
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;

        internal static NameProperties ReadExcelAndExtractNameProperties(string excelPath)
        {

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(excelPath);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                var ediTypeNo = GetEdiTypeNo(worksheet);
                var invoice_or_po = GetInvoiceOrPoNo(worksheet, ediTypeNo);
                NameProperties np = new NameProperties(ediTypeNo, invoice_or_po); 
                return np;
            }
            catch (Exception ex)
            {
                throw new EdiRenameException(ex.Message);
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

        private static string GetInvoiceOrPoNo(Worksheet worksheet, string ediTypeNo)
        {
            if (ediTypeNo == "210")
            {
                var date = worksheet.GetCell("B7");
                var invoice = worksheet.GetCell("B4");
                return $"{date}_{invoice}";
            }
            else if (ediTypeNo == "846")
            {
                var date = worksheet.GetCell("B5");
                var hubGroupDocNo = worksheet.GetCell("B4");
                return $"{date}_{hubGroupDocNo}";
            }
            else if (ediTypeNo == "850")
            {
                var date = worksheet.GetCell("B5");
                var poNo = worksheet.GetCell("B4");
                return $"{date}_{poNo}";
            }
            else if (ediTypeNo == "945")
            {
                var date = worksheet.GetCell("B5");
                var customerOrderId = worksheet.GetCell("B8");
                return $"{date}_{customerOrderId}";
            }
            else if (ediTypeNo == "810")
            {
                string invoiceNo = worksheet.GetCell("B4").ToString();
                string date = invoiceNo.Substring(invoiceNo.Length - 8);
                var customerOrderId = worksheet.GetCell("B3");
                return $"{date}_{customerOrderId}";
            }
            else
            {
                return "Unknown";
            }
        }

        private static string GetEdiTypeNo(Worksheet worksheet)
        {
            var o = worksheet.GetCell("A1") ;
            var a1 = o.ToString().Trim().ToUpper();
            if (a1.Contains("FREIGHT")) {
                return "210";
            }
            else if (a1.Contains("INQUIRY"))
            {
                return "846";
            } else if (a1.Contains("PURCHASE"))
            {
                return "850";
            } else if (a1.Contains("WAREHOUSE"))
            {
                return "945";
            }
            else if (a1.Contains("INVOICE"))
            {
                return "810";
            
            }else
            {
                MessageEventHandler?.Invoke(null, new MessageEventArgs($"A1 value : {a1}"));
                throw new EdiRenameException("알려지지 않은 EDI 문서 타입입니다");
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
