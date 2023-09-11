using EdiDbUploader;
using FBH.EDI.Common.Model;
using Npgsql;

namespace EdiDbUploader
{
    public class EdiUploade810 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var invoice810 = ediDoc as Invoice810;
            //var insertSql = @"insert into edi.invoice_810 (invoice_no, created_by) values(@invoice_no, 'EdiDbUploader')";
            
            //NpgsqlCommand cmd = new NpgsqlCommand(insertSql, OpenConnection());
            //cmd.Parameters.AddWithValue("@invoice_no", invoice810.InvoiceNo);
            
            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd  = NewSqCommand810(invoice810);
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
    }
}