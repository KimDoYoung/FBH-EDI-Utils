using FBH.EDI.Common.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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
            
            return null;
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

        private static Inquiry846 Create846(Worksheet worksheet)
        {
            Inquiry846 inquiry846 = new Inquiry846();
            inquiry846.HubGroupDocumentNumber = worksheet.GetString("B4");
            inquiry846.DateExpresses = worksheet.GetString("B5");
            inquiry846.DateTimeQualifier = worksheet.GetString("D4");
            inquiry846.Date = worksheet.GetString("D5");

            inquiry846.WarehouseName = worksheet.GetString("F4");
            inquiry846.WarehouseId= worksheet.GetString("F5");
            inquiry846.AddressInformation= worksheet.GetString("F6");
            inquiry846.City= worksheet.GetString("F7");
            inquiry846.State= worksheet.GetString("F8");
            inquiry846.Zipcode= worksheet.GetString("F9");

            int row = 20;
            while (row < 5000)
            {
                Inquiry846Detail detail = new Inquiry846Detail();
                //마지막 라인 체크
                var value = worksheet.GetString(row, "A");
                if (string.IsNullOrEmpty(value)) break;

                detail.AssgndNo = worksheet.GetString(row, "A");
                detail.Sku= worksheet.GetString(row, "B");
                detail.LotCode = worksheet.GetString(row, "C");
                detail.NonCommittedIn= CommonUtil.ToIntOrNull( worksheet.GetString(row, "D"));
                detail.NonCommittedOut= CommonUtil.ToIntOrNull(worksheet.GetString(row, "E"));
                detail.OnHandQuantity= CommonUtil.ToIntOrNull(worksheet.GetString(row, "F"));
                detail.InboundPending = CommonUtil.ToIntOrNull(worksheet.GetString(row, "G"));
                detail.OutboundPending= CommonUtil.ToIntOrNull(worksheet.GetString(row, "H"));
                detail.DamagedQuantity= CommonUtil.ToIntOrNull(worksheet.GetString(row, "I"));
                detail.OnHoldQuantity= CommonUtil.ToIntOrNull(worksheet.GetString(row, "J"));
                detail.AvailableQuantity= CommonUtil.ToIntOrNull(worksheet.GetString(row, "K"));
                detail.TotalInventory = CommonUtil.ToIntOrNull(worksheet.GetString(row, "L"));

                inquiry846.Details.Add(detail);
                row++;
            }

            return inquiry846;
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

            invoice210.WarehouseName = worksheet.GetString("F4");
            var ShipFromAddrInfo = worksheet.GetString("F5");
            var ShipFromCity = worksheet.GetString("F6");
            var ShipFromState = worksheet.GetString("F7");
            var ShipFromZipcode = worksheet.GetString("F8");
            var ShipFromCountryCd = worksheet.GetString("F9");
            invoice210.WarehouseAddress = $"{ShipFromAddrInfo}, {ShipFromCity}, {ShipFromState}, {ShipFromZipcode}, {ShipFromZipcode}";

            invoice210.ConsigneeName = worksheet.GetString("H4");
            var ShipToAddrInfo = worksheet.GetString("H5");
            var ShipToCity = worksheet.GetString("H6");
            var ShipToState = worksheet.GetString("H7");
            var ShipToZipcode = worksheet.GetString("H8");
            var ShipToCountryCd = worksheet.GetString("H9");
            invoice210.ConsigneeAddress = $"{ShipToAddrInfo}, {ShipToCity}, {ShipToState}, {ShipToZipcode}, {ShipToCountryCd}";

            invoice210.BillToName = worksheet.GetString("J4"); 
            var BillToAddrInfo = worksheet.GetString("J5");
            var BillToCity = worksheet.GetString("J6");
            var BillToState = worksheet.GetString("J7");
            var BillToZipcode = worksheet.GetString("J8");
            var BillToCountryCd = worksheet.GetString("J9");
            invoice210.BillToAddress = $"{BillToAddrInfo}, {BillToCity}, {BillToState}, {BillToZipcode}, {BillToCountryCd}";

            invoice210.TotalWeight = CommonUtil.ToDecimalOrNull(worksheet.GetString("B13"));
            invoice210.TotalWeightUnit = worksheet.GetString("C13");
            invoice210.WeightQualifier = worksheet.GetString("B14");
            invoice210.AmountCharged = CommonUtil.ToDecimalOrNull(worksheet.GetString("B15"));
            invoice210.Qty = CommonUtil.ToIntOrNull(worksheet.GetString("B16"));


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

        /// <summary>
        /// poNo에 문자열이 포함되어 있는 경우 숫자만을 뽑아낸다.
        /// 1. PO 숫자5개로 뽑아 있으면 그것으로
        /// 2. 없으면 받은 문자 그대로
        /// </summary>
        /// <param name="poNo"></param>
        /// <returns></returns>
        public static string ExtractPo(string poNo)
        {
            var resultString = Regex.Match(poNo, @"PO\s*\d{5}").Value;
            if(string.IsNullOrEmpty(resultString))
            {
                return poNo;
            }
            return resultString;
        }
    }
}
