using FBH.EDI.Common.Model;
using Npgsql;
using System.Collections.Generic;
using System;

namespace EdiDbUploader
{
    internal class EdiUploaderAgingOrigin: EdiUploader
    {
        private NpgsqlCommand cmd = null;
        public override List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            var logList = new List<string>();
            foreach (EdiDocument ediDoc in ediDocumentList)
            {
                cmd = new NpgsqlCommand();
                cmd.Connection = OpenConnection();
                cmd.Transaction = BeginTransaction();
                var item = ediDoc as AgingOrigin;
                try
                {
                    object alreadyCount = ExecuteScalar($"SELECT count(*) FROM edi.aging_origin ao WHERE transaction_number = '{item.TransactionNumber}' ");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        logList.Add($"HK: Transaction Number No:{item.TransactionNumber} is alread exist in table");
                        cmd.Transaction.Commit();
                        continue;
                    }
                    else
                    {
                        cmd = InsertSqCommandAgingOrigin(item);
                        cmd.ExecuteNonQuery();
                    }
                    cmd.Transaction.Commit();
                    logList.Add($"OK: Transaction Number No:{item.TransactionNumber}");
                }
                catch (NpgsqlException ex)
                {
                    cmd?.Transaction.Rollback();
                    logList.Add("NK:" + ex.Message);
                }
                finally
                {
                    cmd?.Transaction?.Dispose();
                    cmd?.Connection?.Close();
                    cmd?.Dispose();
                }
            }
            return logList;
        }

        private NpgsqlCommand InsertSqCommandAgingOrigin(AgingOrigin item)
        {
            //cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.aging_origin("
                + " bill_to_customer,bill_to_site,customer_account_number,transaction_number,transaction_date,"
                + " transaction_type,due_date,aging_days,dpd_bucket,current_amount,"
                + " original_amount,line_haul,fuel,discount,accessorial_xxx,receipt_number,"
                + " ship_date,origin,destination,po_number,sales_order_number,shipping_reference,"
                + " source_system_invoice_number,ref1_ref2_ref3,invoice_notes,manifest_id,ship_reference,"
                + " shipped,unit_no_equip_container_size_seal,weight_class_commodity_pieces,erp_edi_pegasus,"
                + " customer_email_id,"
                + " memo,file_name,created_by"
                + ")values("
                + " @bill_to_customer,@bill_to_site,@customer_account_number,@transaction_number,@transaction_date,"
                + " @transaction_type,@due_date,@aging_days,@dpd_bucket,@current_amount,"
                + " @original_amount,@line_haul,@fuel,@discount,@accessorial_xxx,@receipt_number,"
                + " @ship_date,@origin,@destination,@po_number,@sales_order_number,@shipping_reference,"
                + " @source_system_invoice_number,@ref1_ref2_ref3,@invoice_notes,@manifest_id,@ship_reference,"
                + " @shipped,@unit_no_equip_container_size_seal,@weight_class_commodity_pieces,@erp_edi_pegasus,"
                + " @customer_email_id,"
                + " @memo,@file_name,@created_by"
                + ")";
            cmd.Parameters.Clear();

            cmd.Parameters.Add(NewSafeParameter("@bill_to_customer", item.BillToCustomer));
            cmd.Parameters.Add(NewSafeParameter("@bill_to_site", item.BillToSite));
            cmd.Parameters.Add(NewSafeParameter("@customer_account_number", item.CustomerAccountNumber));
            cmd.Parameters.Add(NewSafeParameter("@transaction_number", item.TransactionNumber));
            cmd.Parameters.Add(NewSafeParameter("@transaction_date", item.TransactionDate));
            cmd.Parameters.Add(NewSafeParameter("@transaction_type", item.TransactionType));
            cmd.Parameters.Add(NewSafeParameter("@due_date", item.DueDate));
            cmd.Parameters.Add(NewSafeParameter("@aging_days", item.AgingDays));
            cmd.Parameters.Add(NewSafeParameter("@dpd_bucket", item.DpdBucket));
            cmd.Parameters.Add(NewSafeParameter("@current_amount", item.CurrentAmount));
            cmd.Parameters.Add(NewSafeParameter("@original_amount", item.OriginalAmount));
            cmd.Parameters.Add(NewSafeParameter("@line_haul", item.LineHaul));
            cmd.Parameters.Add(NewSafeParameter("@fuel", item.Fuel));
            cmd.Parameters.Add(NewSafeParameter("@discount", item.Discount));
            cmd.Parameters.Add(NewSafeParameter("@accessorial_xxx", item.Accessorial_XXX));
            cmd.Parameters.Add(NewSafeParameter("@receipt_number", item.ReceiptNumber));
            cmd.Parameters.Add(NewSafeParameter("@ship_date", item.ShipDate));
            cmd.Parameters.Add(NewSafeParameter("@origin", item.Origin));
            cmd.Parameters.Add(NewSafeParameter("@destination", item.Destination));
            cmd.Parameters.Add(NewSafeParameter("@po_number", item.PoNumber));
            cmd.Parameters.Add(NewSafeParameter("@sales_order_number", item.SalesOrderNumber));
            cmd.Parameters.Add(NewSafeParameter("@shipping_reference", item.ShippingReference));
            cmd.Parameters.Add(NewSafeParameter("@source_system_invoice_number", item.SourceSystemInvoiceNumber));
            cmd.Parameters.Add(NewSafeParameter("@ref1_ref2_ref3", item.Ref1Ref2Ref3));
            cmd.Parameters.Add(NewSafeParameter("@invoice_notes", item.InvoiceNotes));
            cmd.Parameters.Add(NewSafeParameter("@manifest_id", item.ManifestId));
            cmd.Parameters.Add(NewSafeParameter("@ship_reference", item.ShipReference));
            cmd.Parameters.Add(NewSafeParameter("@shipped", item.Shipped));
            cmd.Parameters.Add(NewSafeParameter("@unit_no_equip_container_size_seal", item.UnitNoEquipContainerSizeSeal));
            cmd.Parameters.Add(NewSafeParameter("@weight_class_commodity_pieces", item.WeightClassCommodityPieces));
            cmd.Parameters.Add(NewSafeParameter("@erp_edi_pegasus", item.ErpEdiPegasus));
            cmd.Parameters.Add(NewSafeParameter("@customer_email_id", item.CustomerEmailId));

            cmd.Parameters.Add(NewSafeParameter("@memo", item.Memo));
            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbfileUpload"));
            return cmd;
        }


    }
}