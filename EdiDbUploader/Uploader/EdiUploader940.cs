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
        private NpgsqlCommand cmd = null;
        public override List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            var logList = new List<string>();
            foreach (EdiDocument ediDoc in ediDocumentList)
            {
                cmd = new NpgsqlCommand();
                NpgsqlTransaction tran = null;
                var item = ediDoc as ShippingOrder940;
                try
                {
                    var alreadyCount = ExecuteScalar($"select count(*) as count from edi.shipping_order_940 where order_id = '{item.OrderId}'");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        logList.Add($"NK: {item.OrderId} is alread exist in table");
                        continue;
                    }
                    tran = BeginTransaction();
                    cmd.Transaction = tran;
                    cmd = SqCommand940(item);
                    cmd.ExecuteNonQuery();
                    //detail insert
                    foreach (ShippingOrder940Detail detail in item.Details)
                    {
                        cmd = SqlCommand940Detail(detail);
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    logList.Add($"OK: {item.OrderId}");
                }
                catch (NpgsqlException ex)
                {
                    tran?.Rollback();
                    logList.Add("NK:" + ex.Message);
                }
                finally
                {
                    tran?.Dispose();
                    cmd?.Connection?.Close();
                    cmd?.Dispose();
                }
            }
            return logList;
        }

        private NpgsqlCommand SqlCommand940Detail(ShippingOrder940Detail detail)
        {
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
            cmd.Parameters.Clear();
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

        private NpgsqlCommand SqCommand940(ShippingOrder940 so940)
        {
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.shipping_order_940("
                + "order_id, order_no, balsong_chasu, buyer_po_number, warehouse_info, ship_to, "
                + "reference_identification, requested_pickup_date, requested_delivery_date,"
                +"cancel_after_date, purchase_order_date, warehouse_carrier_info, order_group_id, memo, file_name,"
                +"created_by"
                + ")values("
                + "@order_id, @order_no,@balsong_chasu, @buyer_po_number, @warehouse_info, @ship_to, "
                + "@reference_identification, @requested_pickup_date, @requested_delivery_date,"
                + "@cancel_after_date, @purchase_order_date, @warehouse_carrier_info, @order_group_id, @memo, @file_name,"
                + "@created_by"
                + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@order_id", so940.OrderId));
            cmd.Parameters.Add(NewSafeParameter("@order_no", so940.OrderNo));
            cmd.Parameters.Add(NewSafeParameter("@balsong_chasu", so940.BalsongChasu));
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
            cmd.Parameters.Add(NewSafeParameter("@memo", so940.Memo));
            cmd.Parameters.Add(NewSafeParameter("@file_name", so940.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}
