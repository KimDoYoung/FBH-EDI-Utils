using EdiDbUploader;
using FBH.EDI.Common.Model;
using Npgsql;
using System;

namespace EdiDbUploader
{
    internal class EdiUploader210 : EdiUploader
    {
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
                + "invoice_no, shipid_no, ship_method_of_payment, invoice_dt, amount_to_be_paid, po_number, vics_bol_no,"
                + "warehouse_name, warehouse_address, consignee_name, consignee_address, bill_to_name, bill_to_address,"
                + "total_weight, total_weight_unit, weight_qualifier, amount_charged, qty, "
                + "created_by"
                + ")values("
                + "@invoice_no, @shipid_no, @ship_method_of_payment, @invoice_dt, @amount_to_be_paid, @po_number, @vics_bol_no, "
                + "@warehouse_name, @warehouse_address, @consignee_name, @consignee_address, @bill_to_name, @bill_to_address,"
                + "@total_weight, @total_weight_unit, @weight_qualifier, @amount_charged, @bol_qty_in_cases, "
                + "@created_by"
                + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@invoice_no", freight210.InvoiceNo));
            cmd.Parameters.Add(new NpgsqlParameter("@shipid_no", freight210.ShipIdNo));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_method_of_payment", freight210.ShipMethodOfPayment));
            cmd.Parameters.Add(new NpgsqlParameter("@invoice_dt", freight210.InvoiceDt));
            cmd.Parameters.Add(new NpgsqlParameter("@amount_to_be_paid", freight210.AmountToBePaid));
            cmd.Parameters.Add(new NpgsqlParameter("@po_number", freight210.PoNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@vics_bol_no", freight210.VicsBolNo));

            cmd.Parameters.Add(new NpgsqlParameter("@warehouse_name", freight210.WarehouseName));
            cmd.Parameters.Add(new NpgsqlParameter("@warehouse_address", freight210.WarehouseAddress));
            cmd.Parameters.Add(new NpgsqlParameter("@consignee_name", freight210.ConsigneeName));
            cmd.Parameters.Add(new NpgsqlParameter("@consignee_address", freight210.ConsigneeAddress));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_name", freight210.BillToName));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_address", freight210.BillToAddress));

            cmd.Parameters.Add(new NpgsqlParameter("@total_weight", freight210.TotalWeight));
            cmd.Parameters.Add(new NpgsqlParameter("@total_weight_unit", freight210.TotalWeightUnit));
            cmd.Parameters.Add(new NpgsqlParameter("@weight_qualifier", freight210.WeightQualifier));
            cmd.Parameters.Add(new NpgsqlParameter("@amount_charged", freight210.AmountCharged));
            cmd.Parameters.Add(new NpgsqlParameter("@bol_qty_in_cases", freight210.Qty));
            cmd.Parameters.Add(new NpgsqlParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}