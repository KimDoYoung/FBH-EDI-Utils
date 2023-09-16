using EdiDbUploader;
using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;

namespace EdiDbUploader
{
    public class EdiUploader210 : EdiUploader
    {
        public override List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            List<String> logList = new List<string>();

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            foreach (EdiDocument ediDoc in ediDocumentList)
            {
                tran = BeginTransaction();
                var freight210 = ediDoc as FreightInvoice210;
                try
                {
                    object alreadyCount = ExecuteScalar($"select count(*) as count from edi.freight_210 where invoice_no = '{freight210.InvoiceNo}'");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        logList.Add($"NK: {freight210.InvoiceNo} is alread exist in table");
                        tran.Commit();
                        continue;
                    }

                    cmd = NewSqCommand210(freight210);
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                    logList.Add($"OK: {freight210.InvoiceNo}");
                }
                catch (NpgsqlException ex)
                {
                    tran?.Rollback();
                    logList.Add("NK:" + ex.Message);
                }
                finally
                {
                    tran?.Dispose();
                    cmd?.Connection?.Close();
                }
            }
            return logList;
        }

        public override string Insert(EdiDocument ediDoc)
        {
            var freight210 = ediDoc as FreightInvoice210;

            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.freight_210 where invoice_no = '{freight210.InvoiceNo}'");
            int count = Convert.ToInt32(alreadyCount);
            if (count > 0)
            {
                return $"NK: {freight210.InvoiceNo} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand210(freight210);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                tran.Commit();
                return "OK";
            }
            catch (NpgsqlException ex)
            {
                tran?.Rollback();
                return "NK:" + ex.Message;
            }
            finally
            {
                tran?.Dispose();
                cmd.Connection?.Close();
            }
        }

  
        private NpgsqlCommand NewSqCommand210(FreightInvoice210 freight210)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.freight_210("
                + "invoice_no, ship_id_no, ship_method_of_payment, invoice_dt, amount_to_be_paid, po_number, vics_bol_no,"
                + "warehouse_name, warehouse_address, consignee_name, consignee_address, bill_to_name, bill_to_address,"
                + "total_weight, total_weight_unit, weight_qualifier, amount_charged, qty, dc_no, memo, "
                + "file_name, created_by"
                + ")values("
                + "@invoice_no, @ship_id_no, @ship_method_of_payment, @invoice_dt, @amount_to_be_paid, @po_number, @vics_bol_no, "
                + "@warehouse_name, @warehouse_address, @consignee_name, @consignee_address, @bill_to_name, @bill_to_address,"
                + "@total_weight, @total_weight_unit, @weight_qualifier, @amount_charged, @bol_qty_in_cases, @dc_no, @memo, "
                + "@file_name, @created_by"
                + ")";
            cmd.Parameters.Add(NewSafeParameter("@invoice_no", freight210.InvoiceNo));
            cmd.Parameters.Add(NewSafeParameter("@ship_id_no", freight210.ShipIdNo));
            cmd.Parameters.Add(NewSafeParameter("@ship_method_of_payment", freight210.ShipMethodOfPayment));
            cmd.Parameters.Add(NewSafeParameter("@invoice_dt", freight210.InvoiceDt));
            cmd.Parameters.Add(NewSafeParameter("@amount_to_be_paid", freight210.AmountToBePaid));
            cmd.Parameters.Add(NewSafeParameter("@po_number", freight210.PoNumber));
            cmd.Parameters.Add(NewSafeParameter("@vics_bol_no", freight210.VicsBolNo));

            cmd.Parameters.Add(NewSafeParameter("@warehouse_name", freight210.WarehouseName));
            cmd.Parameters.Add(NewSafeParameter("@warehouse_address", freight210.WarehouseAddress));
            cmd.Parameters.Add(NewSafeParameter("@consignee_name", freight210.ConsigneeName));
            cmd.Parameters.Add(NewSafeParameter("@consignee_address", freight210.ConsigneeAddress));
            cmd.Parameters.Add(NewSafeParameter("@bill_to_name", freight210.BillToName));
            cmd.Parameters.Add(NewSafeParameter("@bill_to_address", freight210.BillToAddress));

            cmd.Parameters.Add(NewSafeParameter("@total_weight", freight210.TotalWeight));
            cmd.Parameters.Add(NewSafeParameter("@total_weight_unit", freight210.TotalWeightUnit));
            cmd.Parameters.Add(NewSafeParameter("@weight_qualifier", freight210.WeightQualifier));
            cmd.Parameters.Add(NewSafeParameter("@amount_charged", freight210.AmountCharged));
            cmd.Parameters.Add(NewSafeParameter("@bol_qty_in_cases", freight210.Qty));

            cmd.Parameters.Add( NewSafeParameter("@dc_no", freight210.DcNo));
            cmd.Parameters.Add( NewSafeParameter("@memo", freight210.Memo));
            cmd.Parameters.Add( NewSafeParameter("@file_name", freight210.FileName));
            cmd.Parameters.Add( NewSafeParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}