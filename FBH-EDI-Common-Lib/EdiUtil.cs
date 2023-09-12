using FBH.EDI.Common.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FBH.EDI.Common
{
    public class EdiUtil
    {
        public static EdiDocument EdiDocumentFromFile(string ediFile)
        {
            if (ediFile.EndsWith("xlsx"))
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Open(ediFile);
                Excel.Worksheet worksheet = workbook.Worksheets[1];
                try
                {
                    var o = worksheet.GetCell("A1");
                    var a1 = o.ToString().Trim().ToUpper();
                    if (a1.Contains("FREIGHT"))
                    {
                        return Create210(worksheet);
                    }
                    else if (a1.Contains("INQUIRY"))
                    {
                        return Create846(worksheet);
                    }
                    else if (a1.Contains("PURCHASE"))
                    {
                        return Create850(worksheet);
                    }
                    else if (a1.Contains("WAREHOUSE"))
                    {
                        return Create945(worksheet);
                    }
                    else if (a1.Contains("INVOICE"))
                    {
                        return Create810(worksheet);
                    }
                    else
                    {
                        throw new EdiException($"알려지지 않은 EDI 문서 타입입니다.{ediFile}");
                    }
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
            else if (ediFile.EndsWith("pdf"))
            {
                return Create210FromPdf(ediFile);
            }
            return null;
        }

        private static EdiDocument Create210FromPdf(string ediFile)
        {
            throw new NotImplementedException();
        }

        private static Invoice810 Create810(Worksheet worksheet)
        {
            Invoice810 invoice810 = new Invoice810();
            invoice810.PoNo = worksheet.GetString("B3");
            invoice810.DepartmentNo = worksheet.GetString("E3");
            invoice810.Currency = worksheet.GetString("H3");

            invoice810.InvoiceNo = worksheet.GetString("B4");
            invoice810.VendorNo = worksheet.GetString("E4");
            invoice810.NetDay = worksheet.GetString("H4");

            invoice810.McdType = worksheet.GetString("E5");
            invoice810.Fob = worksheet.GetString("H5");

            invoice810.SupplierNm = worksheet.GetString("B6");
            invoice810.SupplierCity = worksheet.GetString("B7");
            invoice810.SupplierState = worksheet.GetString("B8");
            invoice810.SupplierZip = worksheet.GetString("B9");
            invoice810.SupplierCountry = worksheet.GetString("B10");

            invoice810.ShipToNm = worksheet.GetString("E6");
            invoice810.ShipToGln = worksheet.GetString("E7");
            invoice810.ShipToAddr = worksheet.GetString("E8");

            string s = worksheet.GetString("A12");
            if (string.IsNullOrEmpty(s))
            {
                invoice810.TtlAmt = 0;
            }
            else
            {
                invoice810.TtlAmt = Convert.ToDecimal(s);
            }
            //details
            int row = 15;
            int seq = 1;
            string value;
            while (row < 5000)
            {
                Invoice810Detail detail810 = new Invoice810Detail();
                value = worksheet.GetString(row,"A");
                if (CommonUtil.IsValidCellValue(value) == false) break;

                detail810.PoNo = invoice810.PoNo;
                detail810.Seq = seq;
                detail810.Qty = CommonUtil.ToIntOrNull(worksheet.GetString(row, "A"));
                detail810.Msrmnt = worksheet.GetString(row, "B");
                detail810.UnitPrice = CommonUtil.ToDecimalOrNull(worksheet.GetString(row, "C"));
                detail810.Gtin13 = worksheet.GetString(row, "D");
                detail810.LineTtl = CommonUtil.ToDecimalOrNull(worksheet.GetString(row, "E"));

                invoice810.Details.Add(detail810);
                row++; seq++;
            }

            return invoice810;
        }

        private static ShippingAdvice945 Create945(Worksheet worksheet)
        {
            ShippingAdvice945 wso945 = new ShippingAdvice945();

            wso945.CustomerOrderId = worksheet.GetString("B4");
            wso945.ActualPickupDate = worksheet.GetString("B5");
            wso945.VicsBOL = worksheet.GetString("B6");
            wso945.HubGroupsOrderNumber = worksheet.GetString("B7");
            wso945.PurchaseOrderNumber = worksheet.GetString("B8");
            wso945.MaterVicsBol = worksheet.GetString("B9");
            wso945.LinkSequenceNumber = worksheet.GetString("B10");

            wso945.SfCompanyName = worksheet.GetString("D4");
            wso945.SfSellerBuyer = worksheet.GetString("D5");
            wso945.SfLocationIdCode = worksheet.GetString("D6");
            wso945.SfAddressInfo = worksheet.GetString("D7");
            wso945.SfCity = worksheet.GetString("D8");
            wso945.SfZipcode = worksheet.GetString("D9");
            wso945.SfCountryCode = worksheet.GetString("D10");

            wso945.StCompanyName = worksheet.GetString("F4");
            wso945.StSellerBuyer = worksheet.GetString("F5");
            wso945.StLocationIdCode = worksheet.GetString("F6");
            wso945.StAddressInfo = worksheet.GetString("F7");
            wso945.StCity = worksheet.GetString("F8");
            wso945.StZipcode = worksheet.GetString("F9");
            wso945.StCountryCode = worksheet.GetString("F10");

            wso945.MfCompanyName = worksheet.GetString("H4");
            wso945.MfSellerBuyer = worksheet.GetString("H5");
            wso945.MfLocationIdCode = worksheet.GetString("H6");
            wso945.MfAddressInfo = worksheet.GetString("H7");
            wso945.MfCity = worksheet.GetString("H8");
            wso945.MfZipcode = worksheet.GetString("H9");
            wso945.MfCountryCode = worksheet.GetString("H10");

            wso945.BtCompanyName = worksheet.GetString("J4");
            wso945.BtSellerBuyer = worksheet.GetString("J5");
            wso945.BtLocationIdCode = worksheet.GetString("J6");
            wso945.BtAddressInfo = worksheet.GetString("J7");
            wso945.BtCity = worksheet.GetString("J8");
            wso945.BtZipcode = worksheet.GetString("J9");
            wso945.BtCountryCode = worksheet.GetString("J10");

            wso945.ProNumber = worksheet.GetString("B14");
            wso945.MasterBolNumber = worksheet.GetString("B15");
            wso945.ServiceLevel = worksheet.GetString("B16");
            wso945.DeliveryAppointmentNumber = worksheet.GetString("B17");
            wso945.PurchaseOrderDate = worksheet.GetString("B18");

            wso945.TransportationMode = worksheet.GetString("D14");
            wso945.CarriersScacCode = worksheet.GetString("D15");
            wso945.CarriersName = worksheet.GetString("D16");
            wso945.PaymentMethod = worksheet.GetString("D17");

            wso945.TotalUnitsShipped = worksheet.GetString("B21");
            wso945.TotalWeightShipped = worksheet.GetString("B22");
            wso945.LadingQuantity = worksheet.GetString("B23");
            wso945.UnitOrBasisForMeasurementCode = worksheet.GetString("B24");

            //details
            int row= 29;
            while (true)
            {
                ShippingAdvice945Detail detail945 = new ShippingAdvice945Detail();
                //마지막 라인 체크
                var value = worksheet.GetString(row, "A");
                if (CommonUtil.IsValidCellValue(value) == false) break;


                detail945.AssignedNumber = CommonUtil.ToIntOrNull(worksheet.GetString(row,"A"));
                detail945.PalletId = worksheet.GetString(row, "B");
                detail945.CarrierTrackingNumber = worksheet.GetString(row, "C");
                detail945.ShipmentStatus = worksheet.GetString(row, "D");
                detail945.RequestedQuantity = worksheet.GetString(row, "E");
                detail945.ActualQuantityShipped = worksheet.GetString(row, "F");
                detail945.DifferenceBetweenActualAndRequested = worksheet.GetString(row, "G");
                detail945.UnitOrBasisMeasurementCode = worksheet.GetString(row, "H");
                detail945.UpcCode = worksheet.GetString(row, "I");
                detail945.SkuNo = worksheet.GetString(row, "J");
                detail945.LotBatchCode = worksheet.GetString(row, "K");
                detail945.TotalWeightForItemLine = worksheet.GetString(row, "L");
                detail945.RetailersItemNumber = worksheet.GetString(row, "M");
                detail945.LineNumber = worksheet.GetString(row, "N");
                detail945.ExpirationDate = worksheet.GetString(row, "O");

                wso945.Details.Add(detail945);

                row++;
                if (row > 5000)
                {
                    throw new EdiException("parsing fail 5000 row over");
                }

            }

            return wso945;
        }

        private static PurchaseOrder850 Create850(Worksheet worksheet)
        {
            PurchaseOrder850 po = new PurchaseOrder850();
            po.PoNo = worksheet.GetString("B4");
            po.PoDt = worksheet.GetString("B5");
            po.PromotionDealNo = worksheet.GetString("B6");
            po.DepartmentNo = worksheet.GetString("B7");
            po.VendorNo = worksheet.GetString("B8");
            po.OrderType = worksheet.GetString("B9");
            po.NetDay = CommonUtil.ToIntOrNull( worksheet.GetString("B10") );

            po.DeliveryRefNo = worksheet.GetString("D4");

            po.BtGln = worksheet.GetString("F4");
            po.BtNm = worksheet.GetString("F5");
            po.BtAddr = worksheet.GetString("F6");
            po.BtCity = worksheet.GetString("F7");
            po.BtState = worksheet.GetString("F8");
            po.BtZip = worksheet.GetString("F9");
            po.BtCountry = worksheet.GetString("F10");

            po.StGln = worksheet.GetString("H4");
            po.StNm = worksheet.GetString("H5");
            po.StAddr = worksheet.GetString("H6");
            po.StCity = worksheet.GetString("H7");
            po.StState = worksheet.GetString("H8");
            po.StZip = worksheet.GetString("H9");
            po.StCountry = worksheet.GetString("H10");

            po.ShipNotBefore = worksheet.GetString("B12");
            po.ShipNoLater = worksheet.GetString("B13");
            po.MustArriveBy = worksheet.GetString("B14");

            po.CarrierDetail = worksheet.GetString("E12");
            po.ShipPayment = worksheet.GetString("E13");

            po.Location = worksheet.GetString("H12");
            po.Description = worksheet.GetString("H13");
            po.Note = worksheet.GetString("A16");

            //detail
            int row = 19;
            while (row < 5000)
            {
                PurchaseOrder850Detail poDetail = new PurchaseOrder850Detail();

                var value = worksheet.GetString(row, "A");
                if (CommonUtil.IsValidCellValue(value) == false) break;

                poDetail.PoNo = po.PoNo;
                poDetail.Line = worksheet.GetString(row, "B");
                poDetail.Qty = CommonUtil.ToIntOrNull(worksheet.GetString(row, "C"));
                poDetail.Msrmnt = worksheet.GetString(row, "D");
                poDetail.UnitPrice = CommonUtil.ToDecimalOrNull(worksheet.GetString(row, "E"));
                poDetail.Gtin13 = worksheet.GetString(row, "F");
                poDetail.RetailerItemNo = worksheet.GetString(row, "G");
                poDetail.VendorItemNo = worksheet.GetString(row, "H");
                poDetail.Description = worksheet.GetString(row, "I");
                poDetail.ExtendedCost = CommonUtil.ToDecimalOrNull(worksheet.GetString(row, "J"));

                po.Details.Add(poDetail);
                row++;
            }
            //allowences
            row = 4;
            while (row < 5000)
            {
                PurchaseOrder850Allowance allowence = new PurchaseOrder850Allowance();

                var value = worksheet.GetString(row, "K");
                if (CommonUtil.IsValidCellValue(value) == false) break;

                allowence.Charge = worksheet.GetString(row, "K");
                allowence.DescCd = worksheet.GetString(row, "L");
                allowence.Amount = CommonUtil.ToInteger(worksheet.GetString(row, "M"));
                allowence.HandlingCd = worksheet.GetString(row, "N");
                allowence.Percent = CommonUtil.ToDecimal(worksheet.GetString(row, "O"));

                po.Allowences.Add(allowence);
                row++;
            }

            //companyname
            //po.CompanyName = GetCompanyName(po, config);
            //WeekOfYearKroger kroger = new WeekOfYearKroger();
            //po.WeekOfYear = kroger.GetWeekOfYear(po.PoDt);


            return po;

        }

        private static EdiDocument Create846(Worksheet worksheet)
        {
            throw new NotImplementedException();
        }

        private static FreightInvoice210 Create210(Worksheet worksheet)
        {
            FreightInvoice210 invoice210 = new FreightInvoice210();

            invoice210.InvoiceNo = worksheet.GetString("B4");
            invoice210.ShipIdNo = worksheet.GetString("B5");
            invoice210.ShipMethodOfPayment = worksheet.GetString("B6");
            invoice210.InvoiceDt = worksheet.GetString("B7");
            invoice210.AmountToBePaid = CommonUtil.ToIntOrNull(worksheet.GetString("B8"));

            invoice210.PoNumber = worksheet.GetString("D4");
            invoice210.VicsBolNo = worksheet.GetString("D5");

            invoice210.ShipFromCompanyName = worksheet.GetString("F4");
            invoice210.ShipFromAddrInfo = worksheet.GetString("F5");
            invoice210.ShipFromCity = worksheet.GetString("F6");
            invoice210.ShipFromState = worksheet.GetString("F7");
            invoice210.ShipFromZipcode = worksheet.GetString("F8");
            invoice210.ShipFromCountryCd = worksheet.GetString("F9");

            invoice210.ShipToCompanyName = worksheet.GetString("H4");
            invoice210.ShipToAddrInfo = worksheet.GetString("H5");
            invoice210.ShipToCity = worksheet.GetString("H6");
            invoice210.ShipToState = worksheet.GetString("H7");
            invoice210.ShipToZipcode = worksheet.GetString("H8");
            invoice210.ShipToCountryCd = worksheet.GetString("H9");

            invoice210.BillToCompanyName = worksheet.GetString("J4");
            invoice210.BillToAddrInfo = worksheet.GetString("J5");
            invoice210.BillToCity = worksheet.GetString("J6");
            invoice210.BillToState = worksheet.GetString("J7");
            invoice210.BillToZipcode = worksheet.GetString("J8");
            invoice210.BillToCountryCd = worksheet.GetString("J9");

            invoice210.TotalWeight = CommonUtil.ToDecimalOrNull(worksheet.GetString("B13"));
            invoice210.TotalWeightUnit = worksheet.GetString("C13");
            invoice210.WeightQualifier = worksheet.GetString("B14");
            invoice210.AmountCharged = CommonUtil.ToIntOrNull(worksheet.GetString("B15"));
            invoice210.BolQtyInCases = CommonUtil.ToIntOrNull(worksheet.GetString("B16"));

            //details
            int row = 19;
            while (true)
            {
                FreightInvoice210Detail detail210 = new FreightInvoice210Detail();
                //마지막 라인 체크
                var value = worksheet.GetString(row, "A");
                if (string.IsNullOrEmpty(value)) break;
                
                detail210.InvoiceNo = invoice210.PoNumber;
                detail210.TransactionSetLineNumber = CommonUtil.ToIntOrNull(worksheet.GetString(row,"A"));
                detail210.PurchaseOrderNumber = worksheet.GetString("B");
                detail210.ShippedDate = worksheet.GetString("C");
                detail210.LadingLineItem = worksheet.GetString("D");
                detail210.LadingDescription = worksheet.GetString("E");
                detail210.BilledRatedAsQuantity = CommonUtil.ToIntOrNull(worksheet.GetString("F"));
                detail210.Weight = CommonUtil.ToDecimalOrNull(worksheet.GetString("H"));
                detail210.LadingQuantity = CommonUtil.ToIntOrNull(worksheet.GetString("I"));
                detail210.FreightRate = CommonUtil.ToDecimalOrNull(worksheet.GetString("J"));
                detail210.AmountCharged = CommonUtil.ToIntOrNull(worksheet.GetString("K"));
                detail210.SpecialChargeOrAllowanceCd = worksheet.GetString("L");

                invoice210.Details.Add(detail210);
                row++;
                if(row > 5000) {
                    throw new EdiException("detail210 parsing fail 5000 row over");
                }
            }

            return invoice210;
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
