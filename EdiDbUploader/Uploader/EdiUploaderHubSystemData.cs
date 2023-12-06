using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;

namespace EdiDbUploader
{
    internal class EdiUploaderHubSystemData : EdiUploader
    {
        private Npgsql.NpgsqlCommand cmd = null;
        public override List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            var logList = new List<string>();
            foreach (EdiDocument ediDoc in ediDocumentList)
            {
                cmd = new NpgsqlCommand();
                cmd.Connection = OpenConnection();
                cmd.Transaction = BeginTransaction();
                var item = ediDoc as HubSystemData;
                try
                {
                    object alreadyCount = ExecuteScalar($"SELECT count(*) FROM edi.hub_system_data  WHERE file_id = '{item.FileId}' and seq= {item.Seq}");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        logList.Add($"HK: Transaction Number No:{item.FileId} is alread exist in table");
                        cmd.Transaction.Commit();
                        continue;
                    }
                    else
                    {
                        cmd = InsertSqCommandHubSystemData(item);
                        cmd.ExecuteNonQuery();
                    }
                    cmd.Transaction.Commit();
                    logList.Add($"OK: Transaction Number No:{item.FileId}");
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

        private NpgsqlCommand InsertSqCommandHubSystemData(HubSystemData item)
        {
            cmd.CommandText = "insert into edi.hub_system_data("
                + " file_id,seq,wh_activity_date,req_delivery_date,actual_delivery_date,warehouse,customer,order_id,po_no,sku,lot_code,received,shipped,damaged,hold,store,"
                + " memo,created_by,file_name"
                + ")values("
                + " @file_id,@seq,@wh_activity_date,@req_delivery_date,@actual_delivery_date,@warehouse,@customer,@order_id,@po_no,@sku,@lot_code,@received,@shipped,@damaged,@hold,@store,"
                + " @memo,@created_by,@file_name"
                + ")";
            cmd.Parameters.Clear();

            cmd.Parameters.Add(NewSafeParameter("@file_id", item.FileId));
            cmd.Parameters.Add(NewSafeParameter("@seq", item.Seq));
            cmd.Parameters.Add(NewSafeParameter("@wh_activity_date", item.WhActivityDate));
            cmd.Parameters.Add(NewSafeParameter("@req_delivery_date", item.ReqDeliveryDate));
            cmd.Parameters.Add(NewSafeParameter("@actual_delivery_date", item.ActualDeliveryDate));
            cmd.Parameters.Add(NewSafeParameter("@warehouse", item.Warehouse));
            cmd.Parameters.Add(NewSafeParameter("@customer", item.Customer));
            cmd.Parameters.Add(NewSafeParameter("@order_id", item.OrderId));
            cmd.Parameters.Add(NewSafeParameter("@po_no", item.PoNo));
            cmd.Parameters.Add(NewSafeParameter("@sku", item.Sku));
            cmd.Parameters.Add(NewSafeParameter("@lot_code", item.LotCode));
            cmd.Parameters.Add(NewSafeParameter("@received", item.Received));
            cmd.Parameters.Add(NewSafeParameter("@shipped", item.Shipped));
            cmd.Parameters.Add(NewSafeParameter("@damaged", item.Damaged));
            cmd.Parameters.Add(NewSafeParameter("@hold", item.Hold));
            cmd.Parameters.Add(NewSafeParameter("@store", item.Store));

            cmd.Parameters.Add(NewSafeParameter("@memo", item.Memo));
            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbfileUpload"));
            return cmd;
        }

    }
}