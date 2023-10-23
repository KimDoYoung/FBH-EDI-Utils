using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;

namespace EdiDbUploader
{
    internal class EdiUploader820 : EdiUploader
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
                var item = ediDoc as Payment820;
                try
                {
                    var alreadyCount = ExecuteScalar($"select count(*) as count from edi.payment_820 p  where trace_id = '{item.TraceId}'");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        //logList.Add($"HK: {item.PoNo} is alread exist in table");
                        cmd?.Transaction?.Commit();
                        continue;
                    }

                    cmd = SqCommand820(item);
                    cmd.ExecuteNonQuery();

                    foreach (Payment820Detail detail in item.Details)
                    {
                        cmd = SqlCommand820Detail(detail);
                        cmd.ExecuteNonQuery();

                        cmd = SqlCommand820DetailInsertWalmart810Payment(item, detail);
                        cmd.ExecuteNonQuery();
                    }
                    cmd?.Transaction?.Commit();
                    logList.Add($"OK: {item.TraceId}");
                }
                catch (NpgsqlException ex)
                {
                    cmd.Transaction.Rollback();
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

        /// <summary>
        /// 기존의 walmart_810_payment에도 insert를 한다.
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        private NpgsqlCommand SqlCommand820DetailInsertWalmart810Payment(Payment820 item, Payment820Detail detail)
        {
            cmd.CommandText = "insert into edi.walmart_810_payment("
                + "po_number,invoice_no,dc_no,store_no,division,microfilm_no,invoice_dt,invoice_amount,date_paid,discount_usd,amount_paid_usd,deduction_code,memo,"
                + "file_name, created_by"
                + ")values("
                + "@po_number,@invoice_no,@dc_no,@store_no,@division,@microfilm_no,@invoice_dt,@invoice_amount,@date_paid,@discount_usd,@amount_paid_usd,@deduction_code,@memo,"
                + "@file_name, @created_by"
                + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@po_number", detail.PurchaseOrderNo));
            cmd.Parameters.Add(NewSafeParameter("@invoice_no", detail.InvoiceNo));
            cmd.Parameters.Add(NewSafeParameter("@dc_no", ""));
            cmd.Parameters.Add(NewSafeParameter("@store_no", ""));
            cmd.Parameters.Add(NewSafeParameter("@division", detail.DivicionId));
            cmd.Parameters.Add(NewSafeParameter("@microfilm_no", detail.MicrofileNo));
            cmd.Parameters.Add(NewSafeParameter("@invoice_dt", detail.InvoiceDt));
            cmd.Parameters.Add(NewSafeParameter("@invoice_amount", detail.AmountInvoice));
            cmd.Parameters.Add(NewSafeParameter("@date_paid", ""));
            cmd.Parameters.Add(NewSafeParameter("@discount_usd", detail.AmountOfTermsDiscount));
            cmd.Parameters.Add(NewSafeParameter("@amount_paid_usd", detail.AmountPaid));
            cmd.Parameters.Add(NewSafeParameter("@deduction_code", ""));
            cmd.Parameters.Add(NewSafeParameter("@memo", ""));
            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "dbupload"));

            return cmd;
        }

        private NpgsqlCommand SqlCommand820Detail( Payment820Detail detail)
        {
            cmd.CommandText = "insert into edi.payment_820_dtl("
                    +"trace_id,invoice_no,invoice_dt,entity_no,amount_paid," 
                    +"amount_invoice,amount_of_terms_discount,divicion_id,"
                    +"department_no,merchandise_type_code,purchase_order_no,"
                    +"store_no,microfile_no,adjustment_amount,adjustment_memo,"
                    +"adjustment_memo_type,cross_ref_code,microfilm_no_of_adjustment"
                +")values("
                + "@trace_id,@invoice_no,@invoice_dt,@entity_no,@amount_paid,"
                + "@amount_invoice,@amount_of_terms_discount,@divicion_id,"
                + "@department_no,@merchandise_type_code,@purchase_order_no,"
                + "@store_no,@microfile_no,@adjustment_amount,@adjustment_memo,"
                + "@adjustment_memo_type,@cross_ref_code,@microfilm_no_of_adjustment"
                + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@trace_id", detail.TraceId));
            cmd.Parameters.Add(NewSafeParameter("@invoice_no", detail.InvoiceNo));
            cmd.Parameters.Add(NewSafeParameter("@invoice_dt", detail.InvoiceDt));
            cmd.Parameters.Add(NewSafeParameter("@entity_no", detail.EntityNo));
            cmd.Parameters.Add(NewSafeParameter("@amount_paid", detail.AmountPaid));
            cmd.Parameters.Add(NewSafeParameter("@amount_invoice", detail.AmountInvoice));
            cmd.Parameters.Add(NewSafeParameter("@amount_of_terms_discount", detail.AmountOfTermsDiscount));
            cmd.Parameters.Add(NewSafeParameter("@divicion_id", detail.DivicionId));
            cmd.Parameters.Add(NewSafeParameter("@department_no", detail.DepartmentNo));
            cmd.Parameters.Add(NewSafeParameter("@merchandise_type_code", detail.MerchandiseTypeCode));
            cmd.Parameters.Add(NewSafeParameter("@purchase_order_no", detail.PurchaseOrderNo));
            cmd.Parameters.Add(NewSafeParameter("@store_no", detail.StoreNo));
            cmd.Parameters.Add(NewSafeParameter("@microfile_no", detail.MicrofileNo));
            cmd.Parameters.Add(NewSafeParameter("@adjustment_amount", detail.AdjustmentAmount));
            cmd.Parameters.Add(NewSafeParameter("@adjustment_memo", detail.AdjustmentMemo));
            cmd.Parameters.Add(NewSafeParameter("@adjustment_memo_type", detail.AdjustmentMemoType));
            cmd.Parameters.Add(NewSafeParameter("@cross_ref_code", detail.CrossRefCode));
            cmd.Parameters.Add(NewSafeParameter("@microfilm_no_of_adjustment", detail.MicrofilmNoOfAdjustment));
            return cmd;
        }

        private NpgsqlCommand SqCommand820(Payment820 item)
        {
            cmd.CommandText = "insert into edi.payment_820("
              + "trace_id, payment_method,payment_issuance_date, amount, credit_debit, "
              + "currency, payer, payer_location_no, payee_name,received_820_date, "
              + "received_820_time,created_by,"
              + "file_name"
              + ")values("
              + "@trace_id,@payment_method,@payment_issuance_date,@amount,@credit_debit,"
              + "@currency,@payer,@payer_location_no,@payee_name,@received_820_date,"
              + "@received_820_time, @created_by,"
              + "@file_name"
              + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@trace_id", item.TraceId));
            cmd.Parameters.Add(NewSafeParameter("@payment_method", item.PaymentMethod));
            cmd.Parameters.Add(NewSafeParameter("@payment_issuance_date", item.PaymentIssuanceDate));
            cmd.Parameters.Add(NewSafeParameter("@amount", item.Amount));
            cmd.Parameters.Add(NewSafeParameter("@credit_debit", item.CreditDebit));
            cmd.Parameters.Add(NewSafeParameter("@currency", item.Currency));
            cmd.Parameters.Add(NewSafeParameter("@payer", item.Payer));
            cmd.Parameters.Add(NewSafeParameter("@payer_location_no", item.PayerLocationNo));
            cmd.Parameters.Add(NewSafeParameter("@payee_name", item.PayeeName));
            cmd.Parameters.Add(NewSafeParameter("@received_820_date", item.Received820Date));
            cmd.Parameters.Add(NewSafeParameter("@received_820_time", item.Received820Time));
            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName)); 
            cmd.Parameters.Add(NewSafeParameter("@created_by", "Dbuploader"));
            return cmd;
        }
    }
}