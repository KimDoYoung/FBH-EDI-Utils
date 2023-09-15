using EdiDbUploader;
using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Drawing;

namespace EdiDbUploader
{
    public class EdiUploader810 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var invoice810 = ediDoc as Invoice810;

            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.invoice_810 where invoice_no = '{invoice810.InvoiceNo}'");
            int count = Convert.ToInt32(alreadyCount);
            if( count > 0 )
            {
                return $"NK: {invoice810.InvoiceNo} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand810(invoice810);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                foreach (Invoice810Detail detail in invoice810.Details)
                {
                    cmd = NewSqlCommand810Detail(detail);
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                }

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

        private NpgsqlCommand NewSqlCommand810Detail(Invoice810Detail detail)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.invoice_810_dtl("
                    + "invoice_no,	seq, 	po_no,	qty,	 msrmnt, unit_price,	gtin13,	line_ttl"
                    + ")values("
                    + "@invoice_no,	@seq,	@po_no,	@qty,	@msrmnt,	 @unit_price,	@gtin13, 	@line_ttl"
                    + ")";
            cmd.Parameters.Add(NewSafeParameter("@invoice_no", detail.InvoiceNo));
            cmd.Parameters.Add(NewSafeParameter("@seq", detail.Seq));
            cmd.Parameters.Add(NewSafeParameter("@po_no", detail.PoNo));
            cmd.Parameters.Add(NewSafeParameter("@qty", detail.Qty));
            cmd.Parameters.Add(NewSafeParameter("@msrmnt", detail.Msrmnt));
            cmd.Parameters.Add(NewSafeParameter("@unit_price", detail.UnitPrice));
            cmd.Parameters.Add(NewSafeParameter("@gtin13", detail.Gtin13));
            cmd.Parameters.Add(NewSafeParameter("@line_ttl", detail.LineTtl));
            return cmd;
        }

        private NpgsqlCommand NewSqCommand810(Invoice810 invoice810)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.invoice_810("
                + "invoice_no, po_no,"
                + "supplier_nm, supplier_city, supplier_state, supplier_zip, supplier_country,"
                + "department_no, currency, vendor_no, net_day, mcd_type, fob,"
                + "ship_to_nm, ship_to_gln, ship_to_addr, ttl_amt, memo, file_name,"
                + "created_by"
                + ")values("
                + "@invoice_no, @po_no,"
                + "@supplier_nm, @supplier_city, @supplier_state, @supplier_zip, @supplier_country,"
                + "@department_no, @currency, @vendor_no, @net_day, @mcd_type, @fob,"
                + "@ship_to_nm, @ship_to_gln, @ship_to_addr, @ttl_amt, @memo, @file_name,"
                + "@created_by"
                + ")";
            cmd.Parameters.Add(NewSafeParameter("@invoice_no", invoice810.InvoiceNo));
            cmd.Parameters.Add(NewSafeParameter("@po_no", invoice810.PoNo));
            cmd.Parameters.Add(NewSafeParameter("@supplier_nm", invoice810.SupplierNm));
            cmd.Parameters.Add(NewSafeParameter("@supplier_city", invoice810.SupplierCity));
            cmd.Parameters.Add(NewSafeParameter("@supplier_state", invoice810.SupplierState));
            cmd.Parameters.Add(NewSafeParameter("@supplier_zip", invoice810.SupplierZip));
            cmd.Parameters.Add(NewSafeParameter("@supplier_country", invoice810.SupplierCountry));
            cmd.Parameters.Add(NewSafeParameter("@department_no", invoice810.DepartmentNo));
            cmd.Parameters.Add(NewSafeParameter("@currency", invoice810.Currency));
            cmd.Parameters.Add(NewSafeParameter("@vendor_no", invoice810.VendorNo));
            cmd.Parameters.Add(NewSafeParameter("@net_day", invoice810.NetDay));
            cmd.Parameters.Add(NewSafeParameter("@mcd_type", invoice810.McdType));
            cmd.Parameters.Add(NewSafeParameter("@fob", invoice810.Fob));
            cmd.Parameters.Add(NewSafeParameter("@ship_to_nm", invoice810.ShipToNm));
            cmd.Parameters.Add(NewSafeParameter("@ship_to_gln", invoice810.ShipToGln));
            cmd.Parameters.Add(NewSafeParameter("@ship_to_addr", invoice810.ShipToAddr));
            cmd.Parameters.Add(NewSafeParameter("@ttl_amt", invoice810.TtlAmt));
            cmd.Parameters.Add(NewSafeParameter("@memo", invoice810.Memo));
            cmd.Parameters.Add(NewSafeParameter("@file_name", invoice810.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}