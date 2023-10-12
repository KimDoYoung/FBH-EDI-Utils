using FBH.EDI.Common.Model;
using Npgsql;
using System.Collections.Generic;
using System;
using FBH.EDI.Common;

namespace EdiDbUploader
{
    internal class EdiUploaderDeliveryAppointments : EdiUploader
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
                var item = ediDoc as DeliveryAppointments;
#if DEBUG
                CommonUtil.Console("bol_no of item : " + item.BolNo);
#endif
                try
                {
                    object alreadyCount = ExecuteScalar($"SELECT count(*) FROM edi.delivery_appointments  WHERE bol_no =  '{item.BolNo}'");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        cmd = UpdateSqCommandDeliveryAppointments(item);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = InsertSqCommandDeliveryAppointments(item);
                        cmd.ExecuteNonQuery();
                    }

                    cmd.Transaction.Commit();
                    logList.Add($"OK: {item.BolNo}");
                }
                catch (NpgsqlException ex)
                {
                    cmd?.Transaction?.Rollback();
                    logList.Add("NK:" + ex.Message);
                }
                finally
                {
                    cmd.Transaction?.Dispose();
                    cmd?.Connection?.Close();
                    cmd?.Dispose();
                }
            }
            return logList;
        }

        private NpgsqlCommand InsertSqCommandDeliveryAppointments(DeliveryAppointments item)
        {
            cmd.CommandText = "insert into edi.delivery_appointments("
                + "bol_no,po_number,po_no_only,act_ship_dt,req_delivery,act_deliviery,delivery_appt,comments,memo,"
                + "file_name, created_by"
                + ")values("
                + "@bol_no,@po_number,@po_no_only,@act_ship_dt,@req_delivery,@act_deliviery,@delivery_appt,@comments,@memo,"
                + "@file_name, @created_by"
                + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@bol_no", item.BolNo));
            cmd.Parameters.Add(NewSafeParameter("@po_number", item.PoNumber));
            cmd.Parameters.Add(NewSafeParameter("@po_no_only", item.PoNoOnly));
            cmd.Parameters.Add(NewSafeParameter("@act_ship_dt", item.ActShipDt));
            cmd.Parameters.Add(NewSafeParameter("@req_delivery", item.ReqDelivery));
            cmd.Parameters.Add(NewSafeParameter("@act_deliviery", item.ActDeliviery));
            cmd.Parameters.Add(NewSafeParameter("@delivery_appt", item.DeliveryAppt));
            cmd.Parameters.Add(NewSafeParameter("@comments", item.Comments));
            cmd.Parameters.Add(NewSafeParameter("@memo", item.Memo));

            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbfileUpload"));
            return cmd;
        }

        private NpgsqlCommand UpdateSqCommandDeliveryAppointments(DeliveryAppointments item)
        {
            cmd.CommandText = "UPDATE edi.delivery_appointments SET "
                            + "po_number = @po_number,"
                            + "po_no_only = @po_no_only,"
                            + "act_ship_dt = @act_ship_dt,"
                            + "req_delivery = @req_delivery,"
                            + "act_deliviery = @act_deliviery,"
                            + "delivery_appt = @delivery_appt,"
                            + "comments = @comments,"
                            + "memo = @memo,"
                            + "file_name = @file_name,"
                            + "created_by = @created_by "
                            + "WHERE bol_no = @bol_no";

            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@bol_no", item.BolNo));
            cmd.Parameters.Add(NewSafeParameter("@po_number", item.PoNumber));
            cmd.Parameters.Add(NewSafeParameter("@po_no_only", item.PoNoOnly));
            cmd.Parameters.Add(NewSafeParameter("@act_ship_dt", item.ActShipDt));
            cmd.Parameters.Add(NewSafeParameter("@req_delivery", item.ReqDelivery));
            cmd.Parameters.Add(NewSafeParameter("@act_deliviery", item.ActDeliviery));
            cmd.Parameters.Add(NewSafeParameter("@delivery_appt", item.DeliveryAppt));
            cmd.Parameters.Add(NewSafeParameter("@comments", item.Comments));
            cmd.Parameters.Add(NewSafeParameter("@memo", item.Memo));

            cmd.Parameters.Add(NewSafeParameter("@file_name", item.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbfileUpload"));
            return cmd;

        }
    }
}