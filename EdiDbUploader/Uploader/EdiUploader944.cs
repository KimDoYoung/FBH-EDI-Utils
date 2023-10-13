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
        private NpgsqlCommand cmd = null;
        public override List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            List<String> logList = new List<string>();
            foreach (EdiDocument ediDoc in ediDocumentList)
            {
                cmd = new NpgsqlCommand();
                cmd.Connection = OpenConnection();
                cmd.Transaction = BeginTransaction();
                var item = ediDoc as Transfer944;
                try
                {
                    var where = $" and hub_groups_order_number='{item.HubGroupsOrderNumber}'";
                    var alreadyCount = ExecuteScalar($"select count(*) as count from edi.transfer_944 where 1=1 {where}");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        logList.Add($"HK: {item.HubGroupsOrderNumber} 이미 존재합니다.");
                        cmd?.Transaction.Commit();
                        continue;
                    }

                    cmd = SqCommand944(item);
                    cmd.ExecuteNonQuery();

                    //detail insert
                    foreach (Transfer944Detail detail in item.Details)
                    {
                        cmd = SqlCommand944Detail(detail);
                        cmd.ExecuteNonQuery();
                    }

                    cmd?.Transaction.Commit();
                    logList.Add($"OK: {item.HubGroupsOrderNumber}");
                }
                catch (NpgsqlException ex)
                {
                    cmd?.Transaction?.Rollback();
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
      

        private NpgsqlCommand SqlCommand944Detail(Transfer944Detail detail)
        {
            cmd.CommandText = "insert into edi.transfer_944_dtl("
                    + "hub_groups_order_number, assigned_number, receipt_date, stock_receipt_quantity_received, "
                    + "stock_receipt_unit_of_measure_code, stock_receipt_sku, stock_receipt_lot_batch_code, exception_quantity,"
                    + "exception_unit_of_measure_code, exception_receiving_condition_code, exception_lot_batch_code, exception_damage_condition" 
                    + ")values("
                    + "@hub_groups_order_number, @assigned_number, @receipt_date, @stock_receipt_quantity_received, "
                    + "@stock_receipt_unit_of_measure_code, @stock_receipt_sku, @stock_receipt_lot_batch_code, @exception_quantity,"
                    + "@exception_unit_of_measure_code, @exception_receiving_condition_code, @exception_lot_batch_code, @exception_damage_condition"
                    + ")";
            cmd.Parameters.Clear();
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

        private NpgsqlCommand SqCommand944(Transfer944 tr944)
        {
            cmd.CommandText = "insert into edi.transfer_944("
                + "hub_groups_order_number, receipt_date, customer_order_id, customers_bol_number, hub_groups_warehousename, "
                +"hub_groups_customers_warehouse_id, destination_address_information, destination_city, destination_state, "
                +"destination_zipcode, origin_company_name, shipper_company_id, origin_address_information, origin_city, "
                +"origin_state, origin_zipcode, scheduled_delivery_date, transportation_method_type_code, standard_carrier_alpha_code, "
                +"quantity_received, number_of_units_shipped, quantity_damaged_on_hold, memo, file_name," 
                +"created_by"
                + ")values("
                + "@hub_groups_order_number, @receipt_date, @customer_order_id, @customers_bol_number, @hub_groups_warehousename, "
                + "@hub_groups_customers_warehouse_id, @destination_address_information, @destination_city, @destination_state, "
                + "@destination_zipcode, @origin_company_name, @shipper_company_id, @origin_address_information, @origin_city, "
                + "@origin_state, @origin_zipcode, @scheduled_delivery_date, @transportation_method_type_code, @standard_carrier_alpha_code, "
                + "@quantity_received, @number_of_units_shipped, @quantity_damaged_on_hold, @memo, @file_name,"
                + "@created_by"
                + ")";
            cmd.Parameters.Clear();
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
            cmd.Parameters.Add(NewSafeParameter("@standard_carrier_alpha_code", tr944.StandardCarrierAlphaCode));
            cmd.Parameters.Add(NewSafeParameter("@quantity_received", tr944.QuantityReceived));
            cmd.Parameters.Add(NewSafeParameter("@number_of_units_shipped", tr944.NumberOfUnitsShipped));
            cmd.Parameters.Add(NewSafeParameter("@quantity_damaged_on_hold", tr944.QuantityDamagedOnHold));
            cmd.Parameters.Add(NewSafeParameter("@memo", tr944.Memo));
            cmd.Parameters.Add(NewSafeParameter("@file_name", tr944.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}
