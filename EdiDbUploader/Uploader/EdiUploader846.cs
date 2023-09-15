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
            cmd.CommandText = "insert into edi.inquiry_846_dtl("
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
            cmd.Parameters.Add(NewSafeParameter("@hub_group_document_number", detail.HubGroupDocumentNumber));
            cmd.Parameters.Add(NewSafeParameter("@assgnd_no", detail.AssgndNo));
            cmd.Parameters.Add(NewSafeParameter("@sku", detail.Sku));
            cmd.Parameters.Add(NewSafeParameter("@lot_code", detail.LotCode));
            cmd.Parameters.Add(NewSafeParameter("@non_committed_in", detail.NonCommittedIn));
            cmd.Parameters.Add(NewSafeParameter("@non_committed_out", detail.NonCommittedOut));
            cmd.Parameters.Add(NewSafeParameter("@on_hand_quantity", detail.OnHandQuantity));
            cmd.Parameters.Add(NewSafeParameter("@inbound_pending", detail.InboundPending));
            cmd.Parameters.Add(NewSafeParameter("@outbound_pending", detail.OutboundPending));
            cmd.Parameters.Add(NewSafeParameter("@damaged_quantity", detail.DamagedQuantity));
            cmd.Parameters.Add(NewSafeParameter("@onhold_quantity", detail.OnHoldQuantity));
            cmd.Parameters.Add(NewSafeParameter("@available_quantity", detail.AvailableQuantity));
            cmd.Parameters.Add(NewSafeParameter("@total_inventory", detail.TotalInventory));
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
            cmd.Parameters.Add(NewSafeParameter("@hub_group_document_number", inquiry846.HubGroupDocumentNumber));
            cmd.Parameters.Add(NewSafeParameter("@date_expresses", inquiry846.DateExpresses));
            cmd.Parameters.Add(NewSafeParameter("@date_time_qualifier", inquiry846.DateTimeQualifier));
            cmd.Parameters.Add(NewSafeParameter("@date", inquiry846.Date));
            cmd.Parameters.Add(NewSafeParameter("@warehouse_name", inquiry846.WarehouseName));
            cmd.Parameters.Add(NewSafeParameter("@warehouse_id", inquiry846.WarehouseId));
            cmd.Parameters.Add(NewSafeParameter("@address_information", inquiry846.AddressInformation));
            cmd.Parameters.Add(NewSafeParameter("@city", inquiry846.City));
            cmd.Parameters.Add(NewSafeParameter("@state", inquiry846.State));
            cmd.Parameters.Add(NewSafeParameter("@zipcode", inquiry846.Zipcode));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));
            return cmd;
        }
    }
}
