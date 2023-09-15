using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiDbUploader.Uploader
{
    internal class EdiUploader944 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var tr944 = ediDoc as Transfer944;

            var where = $"hub_groups_order_number='{tr944.HubGroupsOrderNumber}'"
                        + $" and receipt_date={tr944.ReceiptDate}"
                        + $" and customer_order_id={tr944.CustomerOrderId}";
            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.transfer_944 where 1=1 {where}'");
            int count = Convert.ToInt32(alreadyCount);
            if (count > 0)
            {
                return $"NK: {tr944.HubGroupsOrderNumber}-{tr944.ReceiptDate}-{tr944.CustomerOrderId} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand944(tr944);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                foreach (Transfer944Detail detail in tr944.Details)
                {
                    cmd = NewSqlCommand944Detail(detail);
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

        private NpgsqlCommand NewSqlCommand944Detail(Transfer944Detail detail)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.transfer_944_dtl("
                    + "hub_groups_order_number, assigned_number, receipt_date, stock_receipt_quantity_received, "
                    + "stock_receipt_unit_of_measure_code, stock_receipt_sku, stock_receipt_lot_batch_code, exception_quantity,"
                    + "exception_unit_of_measure_code, exception_receiving_condition_code, exception_lot_batch_code, exception_damage_condition" 
                    + ")values("
                    + "@hub_groups_order_number, @assigned_number, @receipt_date, @stock_receipt_quantity_received, "
                    + "@stock_receipt_unit_of_measure_code, @stock_receipt_sku, @stock_receipt_lot_batch_code, @exception_quantity,"
                    + "@exception_unit_of_measure_code, @exception_receiving_condition_code, @exception_lot_batch_code, @exception_damage_condition"
                    + ")";
            cmd.Parameters.Add(NewSafeParameter("@hub_groups_order_number", detail.HubGroupsOrderNumber));
            cmd.Parameters.Add(NewSafeParameter("@assigned_number", detail.AssignedNumber));
            cmd.Parameters.Add(NewSafeParameter("@receipt_date", detail.ReceiptDate));
            cmd.Parameters.Add(NewSafeParameter("@stock_receipt_quantity_received", detail.StockReceiptQuantityReceived));
            cmd.Parameters.Add(NewSafeParameter("@stock_receipt_unit_of_measure_code", detail.StockReceiptUnitOfMeasureCode));
            cmd.Parameters.Add(NewSafeParameter("@stock_receipt_sku", detail.StockReceiptSku));
            cmd.Parameters.Add(NewSafeParameter("@stock_receipt_lot_batch_code", detail.StockReceiptLotBatchCode));
            cmd.Parameters.Add(NewSafeParameter("@exception_quantity", detail.ExceptionQuantity));
            cmd.Parameters.Add(NewSafeParameter("@exception_unit_of_measure_code", detail.ExceptionUnitOfMeasureCode));
            cmd.Parameters.Add(NewSafeParameter("@exception_receiving_condition_code", detail.ExceptionReceivingConditionCode));
            cmd.Parameters.Add(NewSafeParameter("@exception_lot_batch_code", detail.ExceptionLotBatchCode));
            cmd.Parameters.Add(NewSafeParameter("@exception_damage_condition", detail.ExceptionDamageCondition));
            return cmd;
        }

        private NpgsqlCommand NewSqCommand944(Transfer944 tr944)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.transfer_944("
                + "hub_groups_order_number, receipt_date, customer_order_id, customers_bol_number, hub_groups_warehousename, "
                +"hub_groups_customers_warehouse_id, destination_address_information, destination_city, destination_state, "
                +"destination_zipcode, origin_company_name, shipper_company_id, origin_address_information, origin_city, "
                +"origin_state, origin_zipcode, scheduled_delivery_date, transportation_method_type_code, standard_carriervalpha_code, "
                +"quantity_received, number_of_units_shipped, quantity_damaged_on_hold," 
                +"created_by"
                + ")values("
                + "@hub_groups_order_number, @receipt_date, @customer_order_id, @customers_bol_number, @hub_groups_warehousename, "
                + "@hub_groups_customers_warehouse_id, @destination_address_information, @destination_city, @destination_state, "
                + "@destination_zipcode, @origin_company_name, @shipper_company_id, @origin_address_information, @origin_city, "
                + "@origin_state, @origin_zipcode, @scheduled_delivery_date, @transportation_method_type_code, @standard_carriervalpha_code, "
                + "@quantity_received, @number_of_units_shipped, @quantity_damaged_on_hold,"
                + "@created_by"
                + ")";
            cmd.Parameters.Add(NewSafeParameter("@hub_groups_order_number", tr944.HubGroupsOrderNumber));
            cmd.Parameters.Add(NewSafeParameter("@receipt_date", tr944.ReceiptDate));
            cmd.Parameters.Add(NewSafeParameter("@customer_order_id", tr944.CustomerOrderId));
            cmd.Parameters.Add(NewSafeParameter("@customers_bol_number", tr944.CustomersBolNumber));
            cmd.Parameters.Add(NewSafeParameter("@hub_groups_warehousename", tr944.HubGroupsWarehouseName));
            cmd.Parameters.Add(NewSafeParameter("@hub_groups_customers_warehouse_id", tr944.HubGroupsCustomersWarehouseId));
            cmd.Parameters.Add(NewSafeParameter("@destination_address_information", tr944.DestinationAddressInformation));
            cmd.Parameters.Add(NewSafeParameter("@destination_city", tr944.DestinationCity));
            cmd.Parameters.Add(NewSafeParameter("@destination_state", tr944.DestinationState));
            cmd.Parameters.Add(NewSafeParameter("@destination_zipcode", tr944.DestinationZipcode));
            cmd.Parameters.Add(NewSafeParameter("@origin_company_name", tr944.OriginCompanyName));
            cmd.Parameters.Add(NewSafeParameter("@shipper_company_id", tr944.ShipperCompanyId));
            cmd.Parameters.Add(NewSafeParameter("@origin_address_information", tr944.OriginAddressInformation));
            cmd.Parameters.Add(NewSafeParameter("@origin_city", tr944.OriginCity));
            cmd.Parameters.Add(NewSafeParameter("@origin_state", tr944.OriginState));
            cmd.Parameters.Add(NewSafeParameter("@origin_zipcode", tr944.OriginZipcode));
            cmd.Parameters.Add(NewSafeParameter("@scheduled_delivery_date", tr944.ScheduledDeliveryDate));
            cmd.Parameters.Add(NewSafeParameter("@transportation_method_type_code", tr944.TransportationMethodTypeCode));
            cmd.Parameters.Add(NewSafeParameter("@standard_carriervalpha_code", tr944.StandardCarrierAlphaCode));
            cmd.Parameters.Add(NewSafeParameter("@quantity_received", tr944.QuantityReceived));
            cmd.Parameters.Add(NewSafeParameter("@number_of_units_shipped", tr944.NumberOfUnitsShipped));
            cmd.Parameters.Add(NewSafeParameter("@quantity_damaged_on_hold", tr944.QuantityDamagedOnHold));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}
