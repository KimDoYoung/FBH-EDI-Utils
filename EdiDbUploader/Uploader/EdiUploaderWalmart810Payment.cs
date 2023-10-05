﻿using FBH.EDI.Common.Model;
using Npgsql;
using System.Collections.Generic;
using System;

namespace EdiDbUploader
{
    internal class EdiUploaderWalmart810Payment : EdiUploader
    {
        private NpgsqlCommand cmd = null;
        public override List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            var logList = new List<string>();
            cmd = new NpgsqlCommand();
            foreach (EdiDocument ediDoc in ediDocumentList)
            {
                NpgsqlTransaction tran = BeginTransaction();
                var item = ediDoc as Walmart810Payment;
                try
                {
                    object alreadyCount = ExecuteScalar($"SELECT count(*) FROM edi.walmart_810_payment WHERE po_number  = '{item.PoNumber}'");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        cmd = UpdateSqCommandWalmart810Payments(item);
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = InsertSqCommandWalmart810Payments(item);
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    logList.Add($"OK: {item.PoNumber}");
                }
                catch (NpgsqlException ex)
                {
                    tran?.Rollback();
                    cmd?.Dispose();
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

        private NpgsqlCommand UpdateSqCommandWalmart810Payments(Walmart810Payment item)
        {
            cmd.Connection = OpenConnection();
            cmd.CommandText = "UPDATE edi.walmart_810_payment SET"
                + " invoice_no = @invoice_no, "
                + " dc_no = @dc_no, "
                + " store_no = @store_no, "
                + " division = @division, "
                + " microfilm_no = @microfilm_no, "
                + " invoice_dt = @invoice_dt, "
                + " invoice_amount = @invoice_amount, "
                + " date_paid = @date_paid, "
                + " discount_usd = @discount_usd, "
                + " amount_paid_usd = @amount_paid_usd, "
                + " deduction_code = @deduction_code, "
                + " memo = @memo, "
                + " file_name = @file_name, "
                + " created_by = @created_by "
                + " WHERE po_number = @po_number";

            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@po_number", item.PoNumber));
            cmd.Parameters.Add(NewSafeParameter("@invoice_no", item.InvoiceNo));
            cmd.Parameters.Add(NewSafeParameter("@dc_no", item.DcNo));
            cmd.Parameters.Add(NewSafeParameter("@store_no", item.StoreNo));
            cmd.Parameters.Add(NewSafeParameter("@division", item.Division));
            cmd.Parameters.Add(NewSafeParameter("@microfilm_no", item.MicrofilmNo));
            cmd.Parameters.Add(NewSafeParameter("@invoice_dt", item.InvoiceDt));
            cmd.Parameters.Add(NewSafeParameter("@invoice_amount", item.InvoiceAmount));
            cmd.Parameters.Add(NewSafeParameter("@date_paid", item.DatePaid));
            cmd.Parameters.Add(NewSafeParameter("@discount_usd", item.DiscountUsd));
            cmd.Parameters.Add(NewSafeParameter("@amount_paid_usd", item.AmountPaidUsd));
            cmd.Parameters.Add(NewSafeParameter("@deduction_code", item.DeductionCode));
            cmd.Parameters.Add(NewSafeParameter("@memo", item.Memo));

            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbfileUpload"));
            return cmd;

        }

        private NpgsqlCommand InsertSqCommandWalmart810Payments(Walmart810Payment item)
        {
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.walmart_810_payment("
                + "po_number,invoice_no,dc_no,store_no,division,microfilm_no,invoice_dt,invoice_amount,date_paid,discount_usd,amount_paid_usd,deduction_code,memo,"
                + "file_name, created_by"
                + ")values("
                + "@po_number,@invoice_no,@dc_no,@store_no,@division,@microfilm_no,@invoice_dt,@invoice_amount,@date_paid,@discount_usd,@amount_paid_usd,@deduction_code,@memo,"
                + "@file_name, @created_by"
                + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@po_number", item.PoNumber));
            cmd.Parameters.Add(NewSafeParameter("@invoice_no", item.InvoiceNo));
            cmd.Parameters.Add(NewSafeParameter("@dc_no", item.DcNo));
            cmd.Parameters.Add(NewSafeParameter("@store_no", item.StoreNo));
            cmd.Parameters.Add(NewSafeParameter("@division", item.Division));
            cmd.Parameters.Add(NewSafeParameter("@microfilm_no", item.MicrofilmNo));
            cmd.Parameters.Add(NewSafeParameter("@invoice_dt", item.InvoiceDt));
            cmd.Parameters.Add(NewSafeParameter("@invoice_amount", item.InvoiceAmount));
            cmd.Parameters.Add(NewSafeParameter("@date_paid", item.DatePaid));
            cmd.Parameters.Add(NewSafeParameter("@discount_usd", item.DiscountUsd));
            cmd.Parameters.Add(NewSafeParameter("@amount_paid_usd", item.AmountPaidUsd));
            cmd.Parameters.Add(NewSafeParameter("@deduction_code", item.DeductionCode));
            cmd.Parameters.Add(NewSafeParameter("@memo", item.Memo));

            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbfileUpload"));
            return cmd;
        }
    }
}