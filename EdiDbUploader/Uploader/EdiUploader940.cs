using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiDbUploader.Uploader
{
    internal class EdiUploader940 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var so940 = ediDoc as ShippingOrder940;

            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.shipping_order_940 where oorder_id = '{so940.OrderId}'");
            int count = Convert.ToInt32(alreadyCount);
            if (count > 0)
            {
                return $"NK: {so940.OrderId} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand940(so940);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                foreach (ShippingOrder940Detail detail in so940.Details)
                {
                    cmd = NewSqlCommand940Detail(detail);
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

        private NpgsqlCommand NewSqlCommand940Detail(ShippingOrder940Detail detail)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.shipping_order_940_dtl("
                    + "order_id, seq, quantity_ordered, unit_of_measure, upc_code, sku, retailers_item_code, lot_number,"
                    +"scc14, free_form_description, retail_price, cost_price, misc1_number_of_pack, misc1_size_of_units,"
                    +"misc1_size_unit, misc1_color_description, misc2_number_of_pack, misc2_size_of_units, misc2_size_unit,"
                    +"misc2_color_description, misc3_number_of_pack, misc3_size_of_units, misc3_size_unit, misc3_color_description"
                    +")values("
                    + "@order_id, @seq, @quantity_ordered, @unit_of_measure, @upc_code, @sku, @retailers_item_code, @lot_number,"
                    + "@scc14, @free_form_description, @retail_price, @cost_price, @misc1_number_of_pack, @misc1_size_of_units,"
                    + "@misc1_size_unit, @misc1_color_description, @misc2_number_of_pack, @misc2_size_of_units, @misc2_size_unit,"
                    + "@misc2_color_description, @misc3_number_of_pack, @misc3_size_of_units, @misc3_size_unit, @misc3_color_description"
                    + ")";
            cmd.Parameters.Add(NewSafeParameter("@order_id", detail.OrderId));
            cmd.Parameters.Add(NewSafeParameter("@seq", detail.Seq));
            cmd.Parameters.Add(NewSafeParameter("@quantity_ordered", detail.QuantityOrdered));
            cmd.Parameters.Add(NewSafeParameter("@unit_of_measure", detail.UnitOfMeasure));
            cmd.Parameters.Add(NewSafeParameter("@upc_code", detail.UpcCode));
            cmd.Parameters.Add(NewSafeParameter("@sku", detail.Sku));
            cmd.Parameters.Add(NewSafeParameter("@retailers_item_code", detail.RetailersItemCode));
            cmd.Parameters.Add(NewSafeParameter("@lot_number", detail.LotNumber));
            cmd.Parameters.Add(NewSafeParameter("@scc14", detail.Scc14));
            cmd.Parameters.Add(NewSafeParameter("@free_form_description", detail.FreeFormDescription));
            cmd.Parameters.Add(NewSafeParameter("@retail_price", detail.RetailPrice));
            cmd.Parameters.Add(NewSafeParameter("@cost_price", detail.CostPrice));
            cmd.Parameters.Add(NewSafeParameter("@misc1_number_of_pack", detail.Misc1NumberOfPack));
            cmd.Parameters.Add(NewSafeParameter("@misc1_size_of_units", detail.Misc1SizeOfUnits));
            cmd.Parameters.Add(NewSafeParameter("@misc1_size_unit", detail.Misc1SizeUnit));
            cmd.Parameters.Add(NewSafeParameter("@misc1_color_description", detail.Misc1ColorDescription));
            cmd.Parameters.Add(NewSafeParameter("@misc2_number_of_pack", detail.Misc2NumberOfPack));
            cmd.Parameters.Add(NewSafeParameter("@misc2_size_of_units", detail.Misc2SizeOfUnits));
            cmd.Parameters.Add(NewSafeParameter("@misc2_size_unit", detail.Misc2SizeUnit));
            cmd.Parameters.Add(NewSafeParameter("@misc2_color_description", detail.Misc2ColorDescription));
            cmd.Parameters.Add(NewSafeParameter("@misc3_number_of_pack", detail.Misc3NumberOfPack));
            cmd.Parameters.Add(NewSafeParameter("@misc3_size_of_units", detail.Misc3SizeOfUnits));
            cmd.Parameters.Add(NewSafeParameter("@misc3_size_unit", detail.Misc3SizeUnit));
            cmd.Parameters.Add(NewSafeParameter("@misc3_color_description", detail.Misc3ColorDescription));
            return cmd;
        }

        private NpgsqlCommand NewSqCommand940(ShippingOrder940 so940)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.shipping_order_940("
                + "order_id, order_no, buyer_po_number, warehouse_info, ship_to, "
                +"reference_identification, requested_pickup_date, requested_delivery_date,"
                +"cancel_after_date, purchase_order_date, warehouse_carrier_info, order_group_id,"
                +"created_by"
                + ")values("
                + "@order_id, @order_no, @buyer_po_number, @warehouse_info, @ship_to, "
                + "@reference_identification, @requested_pickup_date, @requested_delivery_date,"
                + "@cancel_after_date, @purchase_order_date, @warehouse_carrier_info, @order_group_id,"
                + "@@created_by"
                + ")";
            cmd.Parameters.Add(NewSafeParameter("@order_id", so940.OrderId));
            cmd.Parameters.Add(NewSafeParameter("@order_no", so940.OrderNo));
            cmd.Parameters.Add(NewSafeParameter("@buyer_po_number", so940.BuyerPoNumber));
            cmd.Parameters.Add(NewSafeParameter("@warehouse_info", so940.WarehouseInfo));
            cmd.Parameters.Add(NewSafeParameter("@ship_to", so940.ShipTo));
            cmd.Parameters.Add(NewSafeParameter("@reference_identification", so940.ReferenceIdentification));
            cmd.Parameters.Add(NewSafeParameter("@requested_pickup_date", so940.RequestedPickupDate));
            cmd.Parameters.Add(NewSafeParameter("@requested_delivery_date", so940.RequestedDeliveryDate));
            cmd.Parameters.Add(NewSafeParameter("@cancel_after_date", so940.CancelAfterDate));
            cmd.Parameters.Add(NewSafeParameter("@purchase_order_date", so940.PurchaseOrderDate));
            cmd.Parameters.Add(NewSafeParameter("@warehouse_carrier_info", so940.WarehouseCarrierInfo));
            cmd.Parameters.Add(NewSafeParameter("@order_group_id", so940.OrderGroupId));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}
