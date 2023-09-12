using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace EdiDbUploader.Uploader
{
    internal class EdiUploader846 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var inquiry846 = ediDoc as Inquiry846;

            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.inquiry_846 where hub_group_document_number = '{inquiry846.HubGroupDocumentNumber}'");
            int count = Convert.ToInt32(alreadyCount);
            if (count > 0)
            {
                return $"NK: {inquiry846.HubGroupDocumentNumber} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand846(inquiry846);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                foreach (Inquiry846Detail detail in inquiry846.Details)
                {
                    cmd = NewSqlCommand846Detail(detail);
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

        private NpgsqlCommand NewSqlCommand846Detail(Inquiry846Detail detail)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.invoice_dtl("
                    + "hub_group_document_number, assgnd_no, sku, "
                    + "lot_code, non_committed_in, non_committed_out, on_hand_quantity, "
                    + "inbound_pending, outbound_pending, damaged_quantity, onhold_quantity, "
                    + "available_quantity, total_inventory"
                    + ")values("
                    + "@hub_group_document_number, @assgnd_no, @sku, "
                    + "@lot_code, @non_committed_in, @non_committed_out, @on_hand_quantity, "
                    + "@inbound_pending, @outbound_pending, @damaged_quantity, @onhold_quantity, "
                    + "@available_quantity, @total_inventory"
                    + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@hub_group_document_number", detail.HubGroupDocumentNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@assgnd_no", detail.AssgndNo));
            cmd.Parameters.Add(new NpgsqlParameter("@sku", detail.Sku));
            cmd.Parameters.Add(new NpgsqlParameter("@lot_code", detail.LotCode));
            cmd.Parameters.Add(new NpgsqlParameter("@non_committed_in", detail.NonCommittedIn));
            cmd.Parameters.Add(new NpgsqlParameter("@non_committed_out", detail.NonCommittedOut));
            cmd.Parameters.Add(new NpgsqlParameter("@on_hand_quantity", detail.OnHandQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@inbound_pending", detail.InboundPending));
            cmd.Parameters.Add(new NpgsqlParameter("@outbound_pending", detail.OutboundPending));
            cmd.Parameters.Add(new NpgsqlParameter("@damaged_quantity", detail.DamagedQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@onhold_quantity", detail.OnHoldQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@available_quantity", detail.AvailableQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@total_inventory", detail.TotalInventory));
            return cmd;
        }

        private NpgsqlCommand NewSqCommand846(Inquiry846 inquiry846)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.inquiry_846("
                    + "hub_group_document_number, date_expresses, date_time_qualifier, date, warehouse_name, warehouse_id, address_information, city, state, zipcode, created_by"
                    + ")values("
                    + "@hub_group_document_number, @date_expresses, @date_time_qualifier, @date, @warehouse_name, @warehouse_id, @address_information, @city, @state, @zipcode, @created_by"
                    + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@hub_group_document_number", inquiry846.HubGroupDocumentNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@date_expresses", inquiry846.DateExpresses));
            cmd.Parameters.Add(new NpgsqlParameter("@date_time_qualifier", inquiry846.DateTimeQualifier));
            cmd.Parameters.Add(new NpgsqlParameter("@date", inquiry846.Date));
            cmd.Parameters.Add(new NpgsqlParameter("@warehouse_name", inquiry846.WarehouseName));
            cmd.Parameters.Add(new NpgsqlParameter("@warehouse_id", inquiry846.WarehouseId));
            cmd.Parameters.Add(new NpgsqlParameter("@address_information", inquiry846.AddressInformation));
            cmd.Parameters.Add(new NpgsqlParameter("@city", inquiry846.City));
            cmd.Parameters.Add(new NpgsqlParameter("@state", inquiry846.State));
            cmd.Parameters.Add(new NpgsqlParameter("@zipcode", inquiry846.Zipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@created_by", "DbUploader"));
            return cmd;
        }
    }
}
