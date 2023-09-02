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

namespace EdiUtils.Common
{
    internal class ExcelUtils
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;

        #region Warehouse Shipping Order 945 작업
        internal static WarehouseShippingOrder945 GetWarehouseShippingOrder945(System.Data.DataTable table)
        {
            WarehouseShippingOrder945 wso945 = new WarehouseShippingOrder945();

            wso945.CustomerOrderId = table.CellAsString("B4");
            wso945.ActualPickupDate = table.CellAsString("B5");
            wso945.VicsBOL= table.CellAsString("B6");
            wso945.HubGroupsOrderNumber= table.CellAsString("B7");
            wso945.PurchaseOrderNumber= table.CellAsString("B8");
            wso945.MaterVicsBol= table.CellAsString("B9");
            wso945.LinkSequenceNumber= table.CellAsString("B10");

            wso945.SfCompanyName= table.CellAsString("D4");
            wso945.SfSellerBuyer= table.CellAsString("D5");
            wso945.SfLocationIdCode= table.CellAsString("D6");
            wso945.SfAddressInfo= table.CellAsString("D7");
            wso945.SfCity= table.CellAsString("D8");
            wso945.SfZipcode= table.CellAsString("D9");
            wso945.SfCountryCode= table.CellAsString("D10");

            wso945.StCompanyName = table.CellAsString("F4");
            wso945.StSellerBuyer = table.CellAsString("F5");
            wso945.StLocationIdCode = table.CellAsString("F6");
            wso945.StAddressInfo= table.CellAsString("F7");
            wso945.StCity = table.CellAsString("F8");
            wso945.StZipcode = table.CellAsString("F9");
            wso945.StCountryCode = table.CellAsString("F10");

            wso945.MfCompanyName = table.CellAsString("H4");
            wso945.MfSellerBuyer = table.CellAsString("H5");
            wso945.MfLocationIdCode = table.CellAsString("H6");
            wso945.MfAddressInfo = table.CellAsString("H7");
            wso945.MfCity = table.CellAsString("H8");
            wso945.MfZipcode = table.CellAsString("H9");
            wso945.MfCountryCode = table.CellAsString("H10");

            wso945.BtCompanyName = table.CellAsString("J4");
            wso945.BtSellerBuyer = table.CellAsString("J5");
            wso945.BtLocationIdCode = table.CellAsString("J6");
            wso945.BtAddressInfo = table.CellAsString("J7");
            wso945.BtCity = table.CellAsString("J8");
            wso945.BtZipcode = table.CellAsString("J9");
            wso945.BtCountryCode = table.CellAsString("J10");

            wso945.ProNumber = table.CellAsString("B14");
            wso945.MasterBolNumber = table.CellAsString("B15");
            wso945.ServiceLevel = table.CellAsString("B16");
            wso945.DeliveryAppointmentNumber = table.CellAsString("B17");
            wso945.PurchaseOrderDate = table.CellAsString("B18");

            wso945.TransportationMode = table.CellAsString("D14");
            wso945.CarriersScacCode = table.CellAsString("D15");
            wso945.CarriersName = table.CellAsString("D16");
            wso945.PaymentMethod= table.CellAsString("D17");

            wso945.TotalUnitsShipped = table.CellAsString("B21");
            wso945.TotalWeightShipped = table.CellAsString("B22");
            wso945.LadingQuantity = table.CellAsString("B23");
            wso945.UnitOrBasisForMeasurementCode = table.CellAsString("B24");

            //details
            int rowIndex = 28;
            string value;
            while (rowIndex < table.Rows.Count)
            {
                WarehouseShippingOrder945Detail detail945 = new WarehouseShippingOrder945Detail();
                //마지막 라인 체크
                value = table.Rows[rowIndex][0].ToString();
                if (IsValidCellValue(value) == false) break;

                DataRow row = table.Rows[rowIndex];

                detail945.AssignedNumber = row.CellAsInteger("A");
                detail945.PalletId = row.CellAsString("B");
                detail945.CarrierTrackingNumber= row.CellAsString("C");
                detail945.ShipmentStatus= row.CellAsString("D");
                detail945.RequestedQuantity= row.CellAsString("E");
                detail945.ActualQuantityShipped= row.CellAsString("F");
                detail945.DifferenceBetweenActualAndRequested = row.CellAsString("G");
                detail945.UnitOrBasisMeasurementCode = row.CellAsString("H");
                detail945.UpcCode = row.CellAsString("I");
                detail945.SkuNo = row.CellAsString("J");
                detail945.LotBatchCode= row.CellAsString("K");
                detail945.TotalWeightForItemLine= row.CellAsString("L");
                detail945.RetailersItemNumber= row.CellAsString("M");
                detail945.LineNumber= row.CellAsString("N");
                detail945.ExpirationDate= row.CellAsString("O");

                wso945.Details.Add(detail945);

                rowIndex++;
            }

            return wso945;
        }

        #endregion

        #region Freight Invoice 210 작업
        /// <summary>
        /// freight invoice 210 리스트 파일을 만든다.
        /// </summary>
        /// <param name="template210Path"></param>
        /// <param name="freightInvoice210s"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static string CreateList210(string template210Path, List<FreightInvoice210> freightInvoice210s, IConfig config)
        {
            string path = template210Path;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                int row = 3;
                foreach (var invoice in freightInvoice210s)
                {
                    
                    //A payment date
                    //B amount
                    worksheet.SetCell(row, "C", YmdFormat(invoice.InvoiceDt));
                    //D
                    worksheet.SetCell(row, "E", invoice.InvoiceNo, "@");
                    worksheet.SetCell(row, "F", invoice.PoNumber, "@");
                    worksheet.SetCell(row, "G", invoice.BolQtyInCases );
                    worksheet.SetCell(row, "H", invoice.GetTotalFreightRate());
                    var dc = ExtractDcNo(invoice.ShipToCompanyName);
                    worksheet.SetCell(row, "I", dc);
                    var addr = $"{invoice.ShipToCompanyName}, {invoice.ShipToAddrInfo}, {invoice.ShipToCity}, {invoice.ShipToState}, {invoice.ShipToZipcode}";
                    worksheet.SetCell(row, "J", addr);

                    row++;
                    MessageEventHandler?.Invoke(null, new MessageEventArgs($"{invoice.ExcelFileName} to row"));
                }


                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\810_List_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiUtilsException(ex.Message);
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
        /// DataTable의 내용으로 FreightInvoice210 을 만든다.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static FreightInvoice210 GetFreightInvoice210(System.Data.DataTable table, IConfig config)
        {
            FreightInvoice210 invoice210 = new FreightInvoice210();

            invoice210.InvoiceNo = table.CellAsString("B4");
            invoice210.ShipIdNo = table.CellAsString("B5");
            invoice210.ShipMethodOfPayment = table.CellAsString("B6");
            invoice210.InvoiceDt= table.CellAsString("B7");
            invoice210.AmountToBePaid = table.CellAsInteger("B8") ;
            
            invoice210.PoNumber = table.CellAsString("D4");
            invoice210.VicsBolNo= table.CellAsString("D5");

            invoice210.ShipFromCompanyName= table.CellAsString("F4");
            invoice210.ShipFromAddrInfo= table.CellAsString("F5");
            invoice210.ShipFromCity= table.CellAsString("F6");
            invoice210.ShipFromState= table.CellAsString("F7");
            invoice210.ShipFromZipcode= table.CellAsString("F8");
            invoice210.ShipFromCountryCd= table.CellAsString("F9");

            invoice210.ShipToCompanyName = table.CellAsString("H4");
            invoice210.ShipToAddrInfo = table.CellAsString("H5");
            invoice210.ShipToCity = table.CellAsString("H6");
            invoice210.ShipToState = table.CellAsString("H7");
            invoice210.ShipToZipcode = table.CellAsString("H8");
            invoice210.ShipToCountryCd = table.CellAsString("H9");

            invoice210.BillToCompanyName = table.CellAsString("J4");
            invoice210.BillToAddrInfo = table.CellAsString("J5");
            invoice210.BillToCity = table.CellAsString("J6");
            invoice210.BillToState = table.CellAsString("J7");
            invoice210.BillToZipcode = table.CellAsString("J8");
            invoice210.BillToCountryCd = table.CellAsString("J9");

            invoice210.TotalWeight = table.CellAsDecimal("B13");
            invoice210.TotalWeightUnit = table.CellAsString("C13");
            invoice210.WeightQualifier= table.CellAsString("B14");
            invoice210.AmountCharged= table.CellAsInteger("B15");
            invoice210.BolQtyInCases = table.CellAsInteger("B16");

            //details
            int rowIndex = 18;
            string value;
            while (rowIndex < table.Rows.Count)
            {
                FreightInvoice210Detail detail210 = new FreightInvoice210Detail();
                //마지막 라인 체크
                value = table.Rows[rowIndex][0].ToString();
                if (IsValidCellValue(value) == false) break;
                
                DataRow row = table.Rows[rowIndex];

                detail210.InvoiceNo = invoice210.PoNumber;
                detail210.TransactionSetLineNumber = row.CellAsInteger("A");
                detail210.PurchaseOrderNumber = row.CellAsString("B");
                detail210.ShippedDate = row.CellAsString("C");
                detail210.LadingLineItem = row.CellAsString("D");
                detail210.LadingDescription = row.CellAsString("E");
                detail210.BilledRatedAsQuantity = row.CellAsInteger("F") ;
                detail210.Weight =  row.CellAsDecimal("H") ;
                detail210.LadingQuantity = row.CellAsInteger("I");
                detail210.FreightRate  =  row.CellAsDecimal("J") ;
                detail210.AmountCharged = row.CellAsInteger("K");
                detail210.SpecialChargeOrAllowanceCd = row.CellAsString("L");

                invoice210.Details.Add(detail210);
                rowIndex++; 
            }

            return invoice210;
        }
        #endregion

        internal static string CreateList945(string template945Path, List<WarehouseShippingOrder945> warehouseShippingOrders, IConfig config)
        {
            string path = template945Path;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                int row = 3;
                foreach (var o945 in warehouseShippingOrders)
                {
                    foreach (var detail in o945.Details)
                    {
                        worksheet.SetCell(row, "A", YmdFormat(o945.ActualPickupDate));
                        worksheet.SetCell(row, "B", o945.HubGroupsOrderNumber);
                        worksheet.SetCell(row, "C", o945.PurchaseOrderNumber);
                        worksheet.SetCell(row, "D", detail.AssignedNumber);
                        worksheet.SetCell(row, "E", detail.SkuNo);
                        worksheet.SetCell(row, "F", detail.RequestedQuantity);
                        worksheet.SetCell(row, "G", detail.ActualQuantityShipped); 
                        worksheet.SetCell(row, "H", detail.LotBatchCode); 
                        worksheet.SetCell(row, "I", detail.RetailersItemNumber); 
                        worksheet.SetCell(row, "J", o945.StCompanyName);

                        row++;
                    }
                    worksheet.Range["A" + (row - 1), "J" + (row - 1)].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                }


                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\945_List_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiUtilsException(ex.Message);
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
        /// PO850각각으로 810 Invoice 엑셀 각각을 만든다.
        /// </summary>
        /// <param name="template810Invoice"></param>
        /// <param name="po850"></param>
        /// <returns>만들어진 810invoice path</returns>
        /// <exception cref="EdiUtilsException"></exception>
        internal static string Po850ToInvoice810(string template810Invoice, PurchaseOrder850 po850)
        {

            WeekOfYearKroger kroger = new WeekOfYearKroger();


            string path = template810Invoice;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                
                worksheet.SetCell(3, "B", po850.PoNo);
                int chasu = kroger.GetWeekOfYear(po850.PoDt);
                if(chasu < 0)
                {
                    throw new EdiUtilsException("Kroger 주차수를 구하는데 실패했습니다.");
                }
                var invoiceNo = po850.PoNo + "WM" + String.Format("{0:00}", chasu) + DateTime.Now.ToString("yMMdd");
                worksheet.SetCell(4, "B", invoiceNo); 

                worksheet.SetCell(3, "E", po850.DepartmentNo);
                worksheet.SetCell(4, "E", po850.VendorNo,"@");
                worksheet.SetCell(5, "E", po850.OrderType,"@"); //merchandise type = order type

                worksheet.SetCell(3, "H", "USD");
                worksheet.SetCell(4, "H", po850.NetDay);
                worksheet.SetCell(5, "H", po850.ShipPayment); //FOB = ship payment

                worksheet.SetCell(6, "B", "FB HOLDINGS INC.");
                worksheet.SetCell(7, "B", "SEOUL");
                worksheet.SetCell(8, "B", "GANGNAM-GU");
                worksheet.SetCell(9, "B", "06045","@");
                worksheet.SetCell(10, "B", "KR");

                worksheet.SetCell(6, "E", po850.BtNm);
                worksheet.SetCell(7, "E", po850.BtGln, "@");
                //worksheet.SetCell(8, "E", $"{po850.BtAddr}, {po850.BtCity}, {po850.BtState}, {po850.BtZip}, {po850.BtCountry}");
                worksheet.SetCell(8, "E", $"{po850.BtAddr}");

                //allowence
                int row = 3;
                foreach (var allowence in po850.Allowences)
                {
                    worksheet.SetCell(row, "I", allowence.Charge);
                    worksheet.SetCell(row, "J", allowence.DescCd);
                    worksheet.SetCell(row, "K", allowence.Amount);
                    worksheet.SetCell(row, "L", allowence.HandlingCd);
                    row++;
                }

                //detail
                row = 15;
                foreach (var detail in po850.Details)
                {
                    worksheet.SetCell(row, "A", detail.Qty);
                    worksheet.SetCell(row, "B", detail.Msrmnt);
                    worksheet.SetCell(row, "C", detail.UnitPrice);
                    worksheet.SetCell(row, "D", detail.Gtin13,"@");
                    worksheet.Range["E" + row].Formula = $"=C{row}*A{row}";
                    row++;
                }
                worksheet.Range["A12"].Formula = $"=(SUM(E15:E1000)*100)-SUM(K3:K13)";
                

                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\810_invoice_{po850.PoNo}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiUtilsException(ex.Message);
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
        /// Invoice 810 리스트 엑셀 파일을 만듬
        /// </summary>
        /// <param name="template810Path"></param>
        /// <param name="invoice810s"></param>
        /// <param name="purchaseOrder850s"></param>
        /// <param name="ssDate"></param>
        /// <returns></returns>
        internal static string CreateList810(string template810Path, List<Invoice810> invoice810s, List<PurchaseOrder850> purchaseOrder850s, string ssDate)
        {
            string path = template810Path;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                int row = 3;
                foreach (var invoice in invoice810s)
                {
                    var dc = "";
                    var poDt = "";
                    var po850 = FindPo850(purchaseOrder850s, invoice.PoNo);
                    if (po850 != null)
                    {
                        dc = po850.BtNm;
                        poDt = po850.PoDt;
                    }
                    var dcNo = ExtractDcNo(po850);

                    worksheet.SetCell(row, "A", poDt);
                    worksheet.SetCell(row, "B", YmdFormat(ssDate));
                    worksheet.SetCell(row, "C", "");
                    worksheet.SetCell(row, "D", "");

                    worksheet.SetCell(row, "E", "");
                    worksheet.SetCell(row, "F", invoice.PoNo);
                    worksheet.SetCell(row, "G", invoice.SumQty());
                    worksheet.SetCell(row, "H", invoice.TtlAmt / 100);

                    worksheet.SetCell(row, "I", dcNo, "@");
                    worksheet.SetCell(row, "J", dc);

                    //format
                    worksheet.Range["C" + row].Formula = $"=B{row} + $C$2";
                    worksheet.Range["D" + row].Formula = $"=C{row} + $D$2";

                    row++;
                }


                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\810_List_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiUtilsException(ex.Message);
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
        /// 850 List for 매출액
        /// </summary>
        /// <param name="template850Path"></param>
        /// <param name="poList"></param>
        /// <returns></returns>
        internal static string CreateList850_2(string template850Path, List<PurchaseOrder850> poList, IConfig config)
        {
            string path = template850Path;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                //1.리스트 쉬트
                CreateList850_2_Sheet1(poList, config, worksheet);

                //WeekOfList를 구한다.
                List<string> woyList = GetWeekOfYearList(poList);

                foreach (var woy in woyList)
                {
                    //새로운 쉬트 생성
                    worksheet = workbook.Worksheets["StaticTemplate1"];
                    worksheet.Copy(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                    worksheet = workbook.Sheets[workbook.Sheets.Count];
                    worksheet.Name = woy;

                    //2.통계쉬트
                    CreateList850_2_Sheet2(poList, config, worksheet, woy);
                }

                //template sheet 삭제
                app.DisplayAlerts = false;
                workbook.Worksheets["StaticTemplate1"].Delete();
                app.DisplayAlerts = true;



                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\850_List_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiUtilsException(ex.Message);
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


        private static void CreateList850_2_Sheet2(List<PurchaseOrder850> poList, IConfig config, Worksheet worksheet, string woy)
        {
            worksheet.SetCell(4, "B", woy); //Week No

            int iWoy = Convert.ToInt32(woy.Substring(1));

            //var poDts = from p in poList
            //            where p.WeekOfYear == iWoy && (p.CompanyName == "Walmart" || p.CompanyName == "WM.COM")
            //            select p.PoDt;
            //발행일을 구한다.
            var poDt = StaticPoDt(poList, iWoy, "Walmart_WM.COM");
            worksheet.SetCell(4, "C", YmdFormat(poDt), "@");
            poDt = StaticPoDt(poList, iWoy, "Kroger");
            worksheet.SetCell(4, "I", YmdFormat(poDt), "@");

            // wallmart offline 갯수
            var count = StaticCartonSum(poList, iWoy, config, "Walmart", "Mandarin"); 
            worksheet.SetCell(4, "E", count);

            count = StaticCartonSum(poList, iWoy, config, "Walmart", "Mango"); 
            worksheet.SetCell(4, "F", count);

            count = StaticCartonSum(poList, iWoy, config, "Walmart", "Pineapple");
            worksheet.SetCell(4, "G", count);

            //wm.com on line갯수
            count = StaticCartonSum(poList, iWoy, config, "WM.COM", "Mandarin"); 
            worksheet.SetCell(5, "E", count);

            count = StaticCartonSum(poList, iWoy, config, "WM.COM", "Mango"); 
            worksheet.SetCell(5, "F", count);

            count = StaticCartonSum(poList, iWoy, config, "WM.COM", "Pineapple"); 
            worksheet.SetCell(5, "G", count);

            //kroger off line갯수
            count = StaticCartonSum(poList, iWoy, config, "Kroger", "Mandarin");
            worksheet.SetCell(4, "K", count);

            count = StaticCartonSum(poList, iWoy, config, "Kroger", "Mango");
            worksheet.SetCell(4, "L", count);

            count = StaticCartonSum(poList, iWoy, config, "Kroger", "Pineapple");
            worksheet.SetCell(4, "M", count);

            //매출액
            decimal money = StaticSaleMoney(poList, iWoy, config, "Walmart");
            worksheet.SetCell(4, "H", money);

            money = StaticSaleMoney(poList, iWoy, config, "WM.COM");
            worksheet.SetCell(5, "H", money);

            money = StaticSaleMoney(poList, iWoy, config, "Kroger");
            worksheet.SetCell(4, "N", money);


            //주차별 회사별 DC별  과일별 주문수 
            List<DcCarton> list1 = StaticsDcCartonList(poList, config, iWoy, CONST.Walmart);
            List<DcCarton> list2 = StaticsDcCartonList(poList, config, iWoy, CONST.Kroger);
            List<DcCarton> list3 = StaticsDcCartonList(poList, config, iWoy, CONST.WMCOM);

            int row = 4;
            worksheet.SetCell(2, "R", $"Listing PO W{iWoy}");
            foreach (DcCarton dcCarton in list1)
            {
                worksheet.SetCell(row + 1, "P", dcCarton.DC);
                worksheet.SetCell(row + 1, "Q", "감귤");
                worksheet.SetCell(row + 1, "R", dcCarton.OrangeCount == 0 ? "-" : dcCarton.OrangeCount.ToString());

                worksheet.SetCell(row + 2, "P", dcCarton.DC);
                worksheet.SetCell(row + 2, "Q", "망고");
                worksheet.SetCell(row + 2, "R", dcCarton.MangoCount == 0 ? "-" : dcCarton.MangoCount.ToString());

                worksheet.SetCell(row + 3, "P", dcCarton.DC);
                worksheet.SetCell(row + 3, "Q", "파인애플");
                worksheet.SetCell(row + 3, "R", dcCarton.PineappleCount == 0 ? "-" : dcCarton.PineappleCount.ToString());

                row += 3;
            }

            row = 4;
            worksheet.SetCell(2, "V", $"Listing PO W{iWoy}");
            foreach (DcCarton dcCarton in list2)
            {
                worksheet.SetCell(row + 1, "T", dcCarton.DC);
                worksheet.SetCell(row + 1, "U", "감귤");
                worksheet.SetCell(row + 1, "V", dcCarton.OrangeCount == 0 ? "-" : dcCarton.OrangeCount.ToString());

                worksheet.SetCell(row + 2, "T", dcCarton.DC);
                worksheet.SetCell(row + 2, "U", "망고");
                worksheet.SetCell(row + 2, "V", dcCarton.MangoCount == 0 ? "-" : dcCarton.MangoCount.ToString());

                worksheet.SetCell(row + 3, "T", dcCarton.DC);
                worksheet.SetCell(row + 3, "U", "파인애플");
                worksheet.SetCell(row + 3, "V", dcCarton.PineappleCount == 0 ? "-" : dcCarton.PineappleCount.ToString());

                row += 3;
            }
            row = 4;
            worksheet.SetCell(2, "Z", $"Listing PO W{iWoy}");
            foreach (DcCarton dcCarton in list3)
            {
                worksheet.SetCell(row + 1, "X", dcCarton.DC);
                worksheet.SetCell(row + 1, "Y", "감귤");
                worksheet.SetCell(row + 1, "Z", dcCarton.OrangeCount == 0 ? "-" : dcCarton.OrangeCount.ToString());

                worksheet.SetCell(row + 2, "X", dcCarton.DC);
                worksheet.SetCell(row + 2, "Y", "망고");
                worksheet.SetCell(row + 2, "Z", dcCarton.MangoCount == 0 ? "-" : dcCarton.MangoCount.ToString());

                worksheet.SetCell(row + 3, "X", dcCarton.DC);
                worksheet.SetCell(row + 3, "Y", "파인애플");
                worksheet.SetCell(row + 3, "Z", dcCarton.PineappleCount == 0 ? "-" : dcCarton.PineappleCount.ToString() );

                row += 3;
            }
        }

        //주차별 회사별  DC과일갯수
        private static List<DcCarton> StaticsDcCartonList(List<PurchaseOrder850> poList, IConfig config, int iWoy, string companyName)
        {
            List<DcCarton> dcList = new List<DcCarton>();
            foreach (var p in poList)
            {
                if (p.WeekOfYear == iWoy)
                {
                    if (companyName != p.CompanyName) continue;

                    var dc = ExtractDcNo(p);
                    DcCarton dcCarton = new DcCarton(dc); //만약 1주에 2,3개의 PO가 있다면 생성말고 찾기로
                    foreach (var detail in p.Details)
                    {
                        
                        var amount = BizRule.IsNoQty(p.CompanyName) ? detail.Qty : detail.Qty / 6;
                        
                        var item = config.Get($"{detail.Gtin13}");
                        if (item.ToLower().Contains("mandarin"))
                        {
                            dcCarton.OrangeCount += amount;
                        }else if (item.ToLower().Contains("mango"))
                        {
                            dcCarton.MangoCount += amount;
                        }else if (item.ToLower().Contains("pineapple"))
                        {
                            dcCarton.PineappleCount += amount;
                        }
                        
                    }
                    dcList.Add(dcCarton);
                }
            }
            return dcList;
        }

        private static decimal StaticSaleMoney(List<PurchaseOrder850> poList, int iWoy, IConfig config, string companyName)
        {
            decimal sum = 0;
            foreach (var p in poList)
            {
                if (p.WeekOfYear == iWoy)
                {
                    if (companyName != p.CompanyName) continue;
                    
                    foreach (var detail in p.Details)
                    {
                        decimal unitPrice = detail.UnitPrice;
                        int qty = detail.Qty;
                        sum += Convert.ToDecimal(p.GetSaleMoney(unitPrice, qty));
                    }
                }
            }
            return sum;
        }

        private static int StaticCartonSum(List<PurchaseOrder850> poList, int iWoy, IConfig config, string companyName, string itemName)
        {
            int sum = 0;
            foreach (var p in poList)
            {
                if (p.WeekOfYear == iWoy)
                {
                    if (companyName != p.CompanyName) continue;
                    foreach (var detail in p.Details)
                    {
                        var pItem = config.Get($"{detail.Gtin13}");
                        if (pItem.Contains(itemName))
                        {
                            //sum += (detail.Qty / 6);
                            sum += BizRule.IsNoQty(p.CompanyName) ? detail.Qty : detail.Qty / 6;
                        }
                    } 
                }
            }
            return sum;

        }

        private static string StaticPoDt(List<PurchaseOrder850> poList, int woy, string companyName)
        {
            List<string> poDtList1 = new List<string>();
            foreach (var p in poList)
            {
                if (p.WeekOfYear == woy)
                {
                    if (companyName.Contains(p.CompanyName))
                    {
                        poDtList1.Add(p.PoDt);
                    }
                }
            }

            if(poDtList1.Count > 0)
            {
                poDtList1.Sort();
                return poDtList1[0];
            }
            return "";
        }

        private static List<string> GetWeekOfYearList(List<PurchaseOrder850> poList)
        {
            List<string> list = new List<string>();
            foreach(PurchaseOrder850 po in poList)
            {
                var woy = "W" + po.WeekOfYear;
                if ( list.IndexOf(woy) < 0)
                {
                    list.Add(woy);
                }
            }
            list.Sort();
            return list;
        }

        private static void CreateList850_2_Sheet1(List<PurchaseOrder850> poList, IConfig config, Worksheet worksheet)
        {
            int row = 2;
            int seq = 1;
            var prevCompanyName = "";
            foreach (var po in poList)
            {
                MessageEventHandler?.Invoke(null, new MessageEventArgs($"{po.ExcelFileName} -> excel row"));

                if (po.CompanyName != prevCompanyName)
                {
                    seq = 1;
                }
                foreach (var detail in po.Details)
                {
                    worksheet.SetCell(row, "A", po.CompanyName);
                    worksheet.SetCell(row, "B", seq);
                    worksheet.SetCell(row, "C", po.PoNo, "@");
                    worksheet.SetCell(row, "D", YmdFormat(po.PoDt));
                    worksheet.SetCell(row, "E", po.WeekOfYear);
                    worksheet.SetCell(row, "F", detail.Gtin13, "@");
                    worksheet.SetCell(row, "G", detail.RetailerItemNo, "@");

                    worksheet.SetCell(row, "H", detail.VendorItemNo);
                    worksheet.SetCell(row, "I", config.Get($"{detail.Gtin13}")); // item
                    worksheet.SetCell(row, "J", BizRule.IsNoQty(po.CompanyName) ? "" : detail.Qty.ToString()); //qty
                    worksheet.SetCell(row, "K", BizRule.IsNoQty(po.CompanyName) ? detail.Qty : detail.Qty / 6, "0"); //carton
                    //L, M, N 없슴
                    //단가가 3.0이하면 수량에 아니라면 carton으로 곱한다
                    decimal price = (BizRule.IsNoQty(po.CompanyName) ? detail.Qty : detail.Qty / 6) * detail.UnitPrice;
                    worksheet.SetCell(row, "O", price, "0.00");

                    //P, Q, R 없슴
                    worksheet.SetCell(row, "S", po.GetSCellValue());
                    worksheet.SetCell(row, "T", po.GetTCellValue());

                    worksheet.SetCell(row, "U", po.GetSaleMoney(detail.UnitPrice, detail.Qty), "0.00"); //매출액

                    //worksheet.Range["U" + row].Formula = $"= O{row} - (S{row} + T{row})";
                    //worksheet.Range["U" + row].NumberFormat = "0.00";

                    worksheet.SetCell(row, "V", ExtractDcNo(po), "@"); //DC
                    worksheet.SetCell(row, "W", po.BtNm);
                    if (po.CompanyName == "Kroger")
                    {
                        worksheet.SetCell(row, "X", $"{po.StAddr}, {po.StCity}, {po.StState}, {po.StZip}, {po.StCountry}");
                    }
                    else
                    {
                        worksheet.SetCell(row, "X", $"{po.BtAddr}, {po.BtCity}, {po.BtState}, {po.BtZip}, {po.BtCountry}");
                    }
                

                    worksheet.SetCell(row, "Y", po.BtGln, "@");
                    worksheet.SetCell(row, "Z", po.DeliveryRefNo);

                    worksheet.SetCell(row, "AA", po.CarrierDetail);
                    worksheet.SetCell(row, "AB", po.ShipPayment);
                    worksheet.SetCell(row, "AC", YmdFormat(po.ShipNotBefore));
                    worksheet.SetCell(row, "AD", YmdFormat(po.ShipNoLater));
                    worksheet.SetCell(row, "AE", YmdFormat(po.MustArriveBy));

                    if (po.CompanyName == "Kroger")
                    {
                        worksheet.SetCell(row, "AF", $"{po.BtAddr}, {po.BtCity}, {po.BtState}, {po.BtZip}, {po.BtCountry}");
                    }

                    //formula
                    //= O12 - (S12 + T12)


                    //정렬
                    worksheet.Range["AA" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    worksheet.Range["AC" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    worksheet.Range["AD" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    worksheet.Range["AE" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                    row++;
                }
                //라인그리기
                worksheet.Range["A" + (row - 1), "AE" + (row - 1)].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                prevCompanyName = po.CompanyName;
                seq++;
            }

            MessageEventHandler?.Invoke(null, new MessageEventArgs($"sheet1에 PO List 작성완료"));
        }


        private static string GetTCellValue(PurchaseOrder850 po)
        {
            if(po.Allowences.Count == 0) { return ""; }
            if(po.Allowences.Count >= 2)
            {
                return string.Format("{0:000.00}",po.Allowences[1].Amount / 100F);
            }
            return "";
        }

        private static string GetSCellValue(PurchaseOrder850 po)
        {
            if (po.Allowences.Count == 0) { return ""; }
            if (po.Allowences.Count >= 2)
            {
                return string.Format("{0:000.00}", po.Allowences[0].Amount / 100F);
            }
            return "";

        }

        internal static string CreateList850(string template850Path,  List<PurchaseOrder850> purchaseOrder850s)
        {
            string path = template850Path;

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(path);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                int row = 2; int seq = 1;
                foreach (var po850 in purchaseOrder850s)
                {
                    
                    foreach (var detail in po850.Details)
                    {
                        worksheet.SetCell(row, "A", seq);
                        worksheet.SetCell(row, "B", po850.PoNo,"@");
                        worksheet.SetCell(row, "C", YmdFormat(po850.PoDt));
                        worksheet.SetCell(row, "E", detail.Gtin13, "@");
                        worksheet.SetCell(row, "F", detail.RetailerItemNo, "@");

                        worksheet.SetCell(row, "G", detail.VendorItemNo);
                        worksheet.SetCell(row, "H", "");
                        worksheet.SetCell(row, "I", detail.Qty);
                        worksheet.SetCell(row, "J", detail.Qty/6);
                        worksheet.SetCell(row, "N", ExtractDcNo(po850), "@");

                        worksheet.SetCell(row, "R", po850.BtNm);
                        worksheet.SetCell(row, "S", $"{po850.BtAddr}, {po850.BtCity}, {po850.BtState}, {po850.BtZip}, {po850.BtCountry}");
                        worksheet.SetCell(row, "T", po850.BtGln, "@");
                        worksheet.SetCell(row, "U", po850.DeliveryRefNo);
                        worksheet.SetCell(row, "V", po850.CarrierDetail);

                        worksheet.SetCell(row, "W", po850.ShipPayment);
                        worksheet.SetCell(row, "X", YmdFormat(po850.ShipNotBefore));
                        worksheet.SetCell(row, "Y", YmdFormat(po850.ShipNoLater));
                        worksheet.SetCell(row, "Z", YmdFormat(po850.MustArriveBy));

                        //수식
                        var f =   $"= IF(G{row} = \"P60523\",\"Sunkist Pineapple\", IF(G{row}=\"M60851\",\"Sunkist Mandarin Orange\", IF(G{row}=\"M60516\",\"Sunkist Mango\")))";
                        Range r = worksheet.Range["H" + row];
                        r.Formula = f;
                        //텍스트
                        r = worksheet.Range["E" + row];
                        r.NumberFormat = "@"; //텍스트
                        //정렬
                        worksheet.Range["V" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["X" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["Y" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["Z" + row].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        row++;
                    }
                    //라인그리기
                    worksheet.Range["A"+ (row-1), "Z"+(row-1)].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    seq++;
                }

                var dir = Path.GetDirectoryName(path);
                var time = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var outputPath = $"{dir}\\850_List_{time}.xlsx";
                worksheet.SaveAs(outputPath, XlFileFormat.xlWorkbookDefault);
                return outputPath;
            }
            catch (Exception ex)
            {
                throw new EdiUtilsException(ex.Message);
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

        private static object YmdFormat(string s)
        {
            string t = Regex.Replace(s, @"\D", "");
            if (t.Length != 8) return s;
            return t.Substring(0, 4) + "-" + t.Substring(4, 2) + "-" + t.Substring(6);
        }

        /// <summary>
        /// s에서 숫자문자 4개를 뽑아서 리턴한다.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static object ExtractDcNo(string s)
        {
            var  resultString = Regex.Match(s, @"\d{4}").Value;
            return resultString;
        }

        private static string ExtractDcNo(PurchaseOrder850 po)
        {
            //excel파일명에서 구한다.
            if(string.IsNullOrEmpty(po.ExcelFileName) == false)
            {
                string[] tmp = po.ExcelFileName.Split('_');
                if(tmp.Length == 2)
                {
                    var match = Regex.Match(tmp[0], @"\d{3,5}", RegexOptions.IgnoreCase);
                    if (match.Success) return tmp[0];
                }
            }
            //내용에서 구한다.
            var company = po.CompanyName;
            var resultString ="";
            if (company.ToLower().Equals("walmart") || company.ToLower().Equals("wm.com"))
            {
                var btNm = po.BtNm;
                resultString = Regex.Match(btNm, @"\d{4}").Value;
            }else if (company.ToLower().Equals("kroger"))
            {
                var stAddr = po.StAddr;
                resultString = Regex.Match(stAddr, @"#\d{3}").Value;
                if (resultString.Length > 0)
                {
                    resultString = resultString.Substring(1);
                }
                else
                {
                    resultString = Regex.Match(stAddr, @"#\d{3, 5}").Value;
                    return resultString;
                }
            }
            return resultString;
            
        }

        private static string GetBaljuDate(List<PurchaseOrder850> purchaseOrder850s, string poNo)
        {
            foreach (var po in purchaseOrder850s)
            {
                if (po.PoNo == poNo)
                {
                    return po.PoDt;
                }
            }
            return "";
        }


        /// <summary>
        /// invoice810 엑셀파일로부터 DataTable을 만든다.
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static System.Data.DataTable DataTableFromExcel(string excelFilePath)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(excelFilePath);
            Excel.Worksheet worksheet = workbook.Worksheets[1];
            try
            {
                Excel.Range range = worksheet.UsedRange;
                return GetDataTable(range);
            }
            catch (Exception e)
            {
                CommonUtil.Console(e.Message);
                return null;
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
        /// DataTable로부터 Invoice810클래스를 생성
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static Invoice810 GetInvoice810(System.Data.DataTable table)
        {
            Invoice810 invoice810 = new Invoice810();
            invoice810.PoNo = GetCellValue(table, "B3");
            invoice810.DepartmentNo = GetCellValue(table, "E3");
            invoice810.Currency = GetCellValue(table, "H3");

            invoice810.InvoiceNo = GetCellValue(table, "B4");
            invoice810.VendorNo= GetCellValue(table, "E4");
            invoice810.NetDay = GetCellValue(table, "H4");

            invoice810.McdType = GetCellValue(table, "E5");
            invoice810.Fob= GetCellValue(table, "H5");

            invoice810.SupplierNm = GetCellValue(table, "B6");
            invoice810.SupplierCity = GetCellValue(table, "B7");
            invoice810.SupplierState = GetCellValue(table, "B8");
            invoice810.SupplierZip = GetCellValue(table, "B9");
            invoice810.SupplierCountry = GetCellValue(table, "B10");

            invoice810.ShipToNm = GetCellValue(table, "E6");
            invoice810.ShipToGln= GetCellValue(table, "E7");
            invoice810.ShipToAddr= GetCellValue(table, "E8");

            string s = GetCellValue(table, "A12");
            if (string.IsNullOrEmpty(s))
            {
                invoice810.TtlAmt = 0;
            }
            else { 
                invoice810.TtlAmt = Convert.ToDecimal(s);
            }
            //details
            int rowIndex = 14;
            int seq = 1;
            string value;
            while (rowIndex < table.Rows.Count)
            {
                Invoice810Detail detail810 = new Invoice810Detail();
                value = table.Rows[rowIndex][0].ToString();
                if (IsValidCellValue(value) == false) break;

                detail810.PoNo = invoice810.PoNo;
                detail810.Seq = seq;
                detail810.Qty = CommonUtil.ToInteger(table.Rows[rowIndex][0]);
                detail810.Msrmnt = table.Rows[rowIndex][1].ToString();
                detail810.UnitPrice = CommonUtil.ToDecimal(table.Rows[rowIndex][2]);
                detail810.Gtin13 = table.Rows[rowIndex][3].ToString();
                detail810.LineTtl = CommonUtil.ToDecimal( table.Rows[rowIndex][4] );

                invoice810.Details.Add(detail810);
                rowIndex++; seq++;
            }

            return invoice810;
        }

        internal static PurchaseOrder850 FindPo850(List<PurchaseOrder850> list, string poNo)
        {
            foreach (var item in list)
            {
                if (item.PoNo == poNo) return item;
            }
            return null;
        }
        /// <summary>
        /// DataTable로부터 Purchase850 생성
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static PurchaseOrder850 GetPurchaceOrder850(System.Data.DataTable table, IConfig config)
        {
            PurchaseOrder850 po = new PurchaseOrder850();
            po.PoNo = GetCellValue(table, "B4");
            po.PoDt = GetCellValue(table, "B5");
            po.PromotionDealNo = GetCellValue(table, "B6");
            po.DepartmentNo = GetCellValue(table, "B7");
            po.VendorNo = GetCellValue(table, "B8");
            po.OrderType = GetCellValue(table, "B9");
            po.NetDay = GetCellValueAsInt(table, "B10");
            
            po.DeliveryRefNo = GetCellValue(table, "D4");

            po.BtGln= GetCellValue(table, "F4");
            po.BtNm = GetCellValue(table, "F5");
            po.BtAddr = GetCellValue(table, "F6");
            po.BtCity = GetCellValue(table, "F7");
            po.BtState = GetCellValue(table, "F8");
            po.BtZip = GetCellValue(table, "F9");
            po.BtCountry = GetCellValue(table, "F10");

            po.StGln = GetCellValue(table, "H4");
            po.StNm = GetCellValue(table, "H5");
            po.StAddr = GetCellValue(table, "H6");
            po.StCity = GetCellValue(table, "H7");
            po.StState = GetCellValue(table, "H8");
            po.StZip = GetCellValue(table, "H9");
            po.StCountry = GetCellValue(table, "H10");

            po.ShipNotBefore = GetCellValue(table, "B12");
            po.ShipNoLater = GetCellValue(table, "B13");
            po.MustArriveBy = GetCellValue(table, "B14");

            po.CarrierDetail = GetCellValue(table, "E12");
            po.ShipPayment = GetCellValue(table, "E13");

            po.Location = GetCellValue(table, "H12");
            po.Description = GetCellValue(table, "H13");
            po.Note = GetCellValue(table, "A16");

            //detail
            int rowIndex = 18;
            string value;
            while (rowIndex < table.Rows.Count)
            {
                PurchaseOrder850Detail poDetail = new PurchaseOrder850Detail();

                value = table.Rows[rowIndex][0].ToString();
                if (IsValidCellValue(value) == false) break;

                poDetail.PoNo = table.Rows[rowIndex][0].ToString();
                poDetail.Line = table.Rows[rowIndex][1].ToString();
                poDetail.Qty = CommonUtil.ToInteger(table.Rows[rowIndex][2]);
                poDetail.Msrmnt = table.Rows[rowIndex][3].ToString();
                poDetail.UnitPrice = CommonUtil.ToDecimal(table.Rows[rowIndex][4]);
                poDetail.Gtin13 = table.Rows[rowIndex][5].ToString();
                poDetail.RetailerItemNo = table.Rows[rowIndex][6].ToString();
                poDetail.VendorItemNo = table.Rows[rowIndex][7].ToString();
                poDetail.Description = table.Rows[rowIndex][8].ToString();
                poDetail.ExtendedCost = CommonUtil.ToDecimal(table.Rows[rowIndex][9].ToString());

                po.Details.Add(poDetail);
                rowIndex++; 
            }
            //allowences
            rowIndex = 3;
            while (rowIndex < table.Rows.Count)
            {
                PurchaseOrder850Allowance allowence = new PurchaseOrder850Allowance();

                value = table.Rows[rowIndex][10].ToString();
                if (IsValidCellValue(value) == false) break;

                allowence.Charge = table.Rows[rowIndex][10].ToString();
                allowence.DescCd = table.Rows[rowIndex][11].ToString();
                allowence.Amount= CommonUtil.ToInteger(table.Rows[rowIndex][12]);
                allowence.HandlingCd = table.Rows[rowIndex][13].ToString();
                allowence.Percent= CommonUtil.ToDecimal(table.Rows[rowIndex][14].ToString());

                po.Allowences.Add(allowence);
                rowIndex++;
            }

            //companyname
            po.CompanyName = GetCompanyName(po,config);
            WeekOfYearKroger kroger = new WeekOfYearKroger();
            po.WeekOfYear = kroger.GetWeekOfYear(po.PoDt);


            return po;

        }

        private static string GetCompanyName(PurchaseOrder850 po, IConfig config)
        {
            string name = config.Get(po.BtGln.Substring(0, 7));
            if (name.Contains("_"))
            {
                string[] tmp = name.Split('_');
                if( po.PromotionDealNo.Contains("ONLINE"))
                {
                    return tmp[1].Trim();
                }
                return tmp[0].Trim();
            }
            return name;

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
        private static System.Data.DataTable GetDataTable(Excel.Range excelRange)
        {
            DataRow row;
            System.Data.DataTable dt = new System.Data.DataTable();
            int rowCount = excelRange.Rows.Count; //get row count of excel data

            int colCount = excelRange.Columns.Count; // get column count of excel data

            //컬럼명
            for (int j = 1; j <= colCount; j++)
            {
                dt.Columns.Add("Col" + j);
            }

            //Get Row Data of Excel

            int colIndex; 
            for (int i = 1; i <= rowCount; i++) 
            {
                row = dt.NewRow(); 
                colIndex = 0;
                for (int j = 1; j <= colCount; j++)
                {
                    //check if cell is empty
                    if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
                    {
                        row[colIndex] = excelRange.Cells[i, j].Value2.ToString().Trim();
                    }
                    else
                    {
                        row[colIndex] = "";
                    }
                    colIndex++;
                }
                dt.Rows.Add(row); //add row to DataTable
            }

            return dt;
        }

        private static RowCol<int, int> GetRowColFromCellName(string cellName)
        {
            string cellNamePattern = @"([A-Z]{1,2})([0-9]{1,2})";
            Regex r = new Regex(cellNamePattern, RegexOptions.IgnoreCase);

            Match m = r.Match(cellName);
            string row = "";
            string col = "";
            if (m.Success)
            {
                if (m.Groups.Count == 3)
                {
                    Group gCol = m.Groups[1];
                    Group gRow = m.Groups[2];
                    col = gCol.ToString();
                    row = gRow.ToString();
                }
            }
            if (row.Length > 0 && col.Length > 0)
            {
                int indexRow = Convert.ToInt32(row) - 1;
                int indexCol = ConvertExcelColNameToIndex(col);
                CommonUtil.Console(indexRow + ", " + indexCol);
                return new RowCol<int, int>(indexRow, indexCol);
            }
            return null;

        }

        private static int ConvertExcelColNameToIndex(string col)
        {
            if (col.Length == 1)
            {
                return Convert.ToChar(col) - 'A';
            }else if(col.Length == 2)
            {
                //AA index => 26+ 0 
                return ((Convert.ToChar(col[0]) - 'A' + 1) * 26) + (Convert.ToChar(col[1]) - 'A') + 1;

            }
            else
            {
                throw new EdiUtilsException("엑셀 셀문자열을 column index로 변환하는데 실패했습니다");
            }
        }


        private static bool IsValidCellValue(object o)
        {
            if (o == null) return false;
            string value = o.ToString();
            if (value.Trim().Length < 1) return false;
            return true;
        }

        /// <summary>
        /// table에서 엑셀의 CellName에 해당하는 cellName으로 값을 찾아서 리턴
        /// </summary>
        /// <param name="table">엑셀로 부터 만들어진 DataTable</param>
        /// <param name="cellName">A3, C4와 같은 엑셀셀이름</param>
        /// <returns></returns>
        private static string GetCellValue(System.Data.DataTable table, string cellName)
        {
            RowCol<int, int> rc = GetRowColFromCellName(cellName);

            return table.Rows[rc.row][rc.col].ToString();
        }

        private static int GetCellValueAsInt(System.Data.DataTable table, string cellName, int defaultValue=0)
        {
            RowCol<int, int> rc = GetRowColFromCellName(cellName);

            var v =  table.Rows[rc.row][rc.col].ToString();
            try
            {
                return Convert.ToInt32(v);
            }
            catch (Exception)
            {

                return defaultValue;
            }
        }

    }
}
