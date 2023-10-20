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
                    }
                    cmd?.Transaction?.Commit();
                    //logList.Add($"OK: {item.PoNo}");
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

        private NpgsqlCommand SqlCommand820Detail(Payment820Detail detail)
        {
            throw new NotImplementedException();
        }


        private NpgsqlCommand SqCommand820(Payment820 item)
        {
            throw new NotImplementedException();
        }
    }
}