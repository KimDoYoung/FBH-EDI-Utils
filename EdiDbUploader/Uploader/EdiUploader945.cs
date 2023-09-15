using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiDbUploader.Uploader
{
    internal class EdiUploader945 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var sa945 = ediDoc as ShippingAdvice945;

            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.shipping_945 where customer_order_id = '{sa945.CustomerOrderId}'");
            int count = Convert.ToInt32(alreadyCount);
            if (count > 0)
            {
                return $"NK: {sa945.CustomerOrderId} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand945(sa945);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                foreach (ShippingAdvice945Detail detail in sa945.Details)
                {
                    cmd = NewSqlCommand945Detail(detail);
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

        private NpgsqlCommand NewSqlCommand945Detail(ShippingAdvice945Detail detail)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.shipping_945_dtl("
                        +"customer_order_id, assigned_number, pallet_id, carrier_tracking_number, shipment_status, "
                        + "requested_quantity, actual_quantity_shipped, difference_between_actual_and_requested, "
                        + "unit_or_basis_measurement_code, upc_code, sku_no, lot_batch_code, total_weight_for_item_line, "
                        +"retailers_item_number, line_number, expiration_date" 
                    +")values("
                        + "@customer_order_id, @assigned_number, @pallet_id, @carrier_tracking_number, @shipment_status, "
                        + "@requested_quantity, @actual_quantity_shipped, @difference_between_actual_and_requested, "
                        + "@unit_or_basis_measurement_code, @upc_code, @sku_no, @lot_batch_code, @total_weight_for_item_line, "
                        + "@retailers_item_number, @line_number, @expiration_date"
                    + ")";
            cmd.Parameters.Add(NewSafeParameter("@customer_order_id", detail.CustomerOrderId));
            cmd.Parameters.Add(NewSafeParameter("@assigned_number", detail.AssignedNumber));
            cmd.Parameters.Add(NewSafeParameter("@pallet_id", detail.PalletId));
            cmd.Parameters.Add(NewSafeParameter("@carrier_tracking_number", detail.CarrierTrackingNumber));
            cmd.Parameters.Add(NewSafeParameter("@shipment_status", detail.ShipmentStatus));
            cmd.Parameters.Add(NewSafeParameter("@requested_quantity", detail.RequestedQuantity));
            cmd.Parameters.Add(NewSafeParameter("@actual_quantity_shipped", detail.ActualQuantityShipped));
            cmd.Parameters.Add(NewSafeParameter("@difference_between_actual_and_requested", detail.DifferenceBetweenActualAndRequested));
            cmd.Parameters.Add(NewSafeParameter("@unit_or_basis_measurement_code", detail.UnitOrBasisMeasurementCode));
            cmd.Parameters.Add(NewSafeParameter("@upc_code", detail.UpcCode));
            cmd.Parameters.Add(NewSafeParameter("@sku_no", detail.SkuNo));
            cmd.Parameters.Add(NewSafeParameter("@lot_batch_code", detail.LotBatchCode));
            cmd.Parameters.Add(NewSafeParameter("@total_weight_for_item_line", detail.TotalWeightForItemLine));
            cmd.Parameters.Add(NewSafeParameter("@retailers_item_number", detail.RetailersItemNumber));
            cmd.Parameters.Add(NewSafeParameter("@line_number", detail.LineNumber));
            cmd.Parameters.Add(NewSafeParameter("@expiration_date", detail.ExpirationDate));
            return cmd;
        }

        private NpgsqlCommand NewSqCommand945(ShippingAdvice945 sa945)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.shipping_945("
                + "customer_order_id, actual_pickup_date, vics_bol, hub_groups_order_number, purchase_order_number, "
                + "mater_vics_bol, link_sequence_number, sf_company_name, sf_seller_buyer, sf_location_id_code, "
                + "sf_address_info, sf_city, sf_state, sf_zipcode, sf_country_code, st_company_name, st_seller_buyer, "
                + "st_location_id_code, st_address_info, st_city, st_state, st_zipcode, st_country_code, mf_company_name, "
                + "mf_seller_buyer, mf_location_id_code, mf_address_info, mf_city, mf_state, mf_zipcode, mf_country_code, "
                + "bt_company_name, bt_seller_buyer, bt_location_id_code, bt_address_info, bt_city, bt_state, bt_zipcode,"
                + "bt_country_code, pro_number, master_bol_number, service_level, delivery_appointment_number, purchase_order_date, "
                + "transportation_mode, carriers_scac_code, carriers_name, payment_method, allowance_or_charge_total_amount, total_units_shipped,"
                + "total_weight_shipped, lading_quantity, unit_or_basis_for_measurement_code, memo, file_name, "
                + "created_by"
                + ")values("
                + "@customer_order_id, @actual_pickup_date, @vics_bol, @hub_groups_order_number, @purchase_order_number, "
                + "@mater_vics_bol, @link_sequence_number, @sf_company_name, @sf_seller_buyer, @sf_location_id_code, "
                + "@sf_address_info, @sf_city, @sf_state, @sf_zipcode, @sf_country_code, @st_company_name, @st_seller_buyer, "
                + "@st_location_id_code, @st_address_info, @st_city, @st_state, @st_zipcode, @st_country_code, @mf_company_name, "
                + "@mf_seller_buyer, @mf_location_id_code, @mf_address_info, @mf_city, @mf_state, @mf_zipcode, @mf_country_code, "
                + "@bt_company_name, @bt_seller_buyer, @bt_location_id_code, @bt_address_info, @bt_city, @bt_state, @bt_zipcode,"
                + "@bt_country_code, @pro_number, @master_bol_number, @service_level, @delivery_appointment_number, @purchase_order_date, "
                + "@transportation_mode, @carriers_scac_code, @carriers_name, @payment_method, @allowance_or_charge_total_amount, @total_units_shipped,"
                + "@total_weight_shipped, @lading_quantity, @unit_or_basis_for_measurement_code, @memo, @file_name,"
                + "@created_by"
                + ")";
            cmd.Parameters.Add(NewSafeParameter("@customer_order_id", sa945.CustomerOrderId));
            cmd.Parameters.Add(NewSafeParameter("@actual_pickup_date", sa945.ActualPickupDate));
            cmd.Parameters.Add(NewSafeParameter("@vics_bol", sa945.VicsBOL));
            cmd.Parameters.Add(NewSafeParameter("@hub_groups_order_number", sa945.HubGroupsOrderNumber));
            cmd.Parameters.Add(NewSafeParameter("@purchase_order_number", sa945.PurchaseOrderNumber));
            cmd.Parameters.Add(NewSafeParameter("@mater_vics_bol", sa945.MaterVicsBol));
            cmd.Parameters.Add(NewSafeParameter("@link_sequence_number", sa945.LinkSequenceNumber));
            cmd.Parameters.Add(NewSafeParameter("@sf_company_name", sa945.SfCompanyName));
            cmd.Parameters.Add(NewSafeParameter("@sf_seller_buyer", sa945.SfSellerBuyer));
            cmd.Parameters.Add(NewSafeParameter("@sf_location_id_code", sa945.SfLocationIdCode));
            cmd.Parameters.Add(NewSafeParameter("@sf_address_info", sa945.SfAddressInfo));
            cmd.Parameters.Add(NewSafeParameter("@sf_city", sa945.SfCity));
            cmd.Parameters.Add(NewSafeParameter("@sf_state", sa945.SfState));
            cmd.Parameters.Add(NewSafeParameter("@sf_zipcode", sa945.SfZipcode));
            cmd.Parameters.Add(NewSafeParameter("@sf_country_code", sa945.SfCountryCode));
            cmd.Parameters.Add(NewSafeParameter("@st_company_name", sa945.StCompanyName));
            cmd.Parameters.Add(NewSafeParameter("@st_seller_buyer", sa945.StSellerBuyer));
            cmd.Parameters.Add(NewSafeParameter("@st_location_id_code", sa945.StLocationIdCode));
            cmd.Parameters.Add(NewSafeParameter("@st_address_info", sa945.StAddressInfo));
            cmd.Parameters.Add(NewSafeParameter("@st_city", sa945.StCity));
            cmd.Parameters.Add(NewSafeParameter("@st_state", sa945.StState));
            cmd.Parameters.Add(NewSafeParameter("@st_zipcode", sa945.StZipcode));
            cmd.Parameters.Add(NewSafeParameter("@st_country_code", sa945.StCountryCode));
            cmd.Parameters.Add(NewSafeParameter("@mf_company_name", sa945.MfCompanyName));
            cmd.Parameters.Add(NewSafeParameter("@mf_seller_buyer", sa945.MfSellerBuyer));
            cmd.Parameters.Add(NewSafeParameter("@mf_location_id_code", sa945.MfLocationIdCode));
            cmd.Parameters.Add(NewSafeParameter("@mf_address_info", sa945.MfAddressInfo));
            cmd.Parameters.Add(NewSafeParameter("@mf_city", sa945.MfCity));
            cmd.Parameters.Add(NewSafeParameter("@mf_state", sa945.MfState));
            cmd.Parameters.Add(NewSafeParameter("@mf_zipcode", sa945.MfZipcode));
            cmd.Parameters.Add(NewSafeParameter("@mf_country_code", sa945.MfCountryCode));
            cmd.Parameters.Add(NewSafeParameter("@bt_company_name", sa945.BtCompanyName));
            cmd.Parameters.Add(NewSafeParameter("@bt_seller_buyer", sa945.BtSellerBuyer));
            cmd.Parameters.Add(NewSafeParameter("@bt_location_id_code", sa945.BtLocationIdCode));
            cmd.Parameters.Add(NewSafeParameter("@bt_address_info", sa945.BtAddressInfo));
            cmd.Parameters.Add(NewSafeParameter("@bt_city", sa945.BtCity));
            cmd.Parameters.Add(NewSafeParameter("@bt_state", sa945.BtState));
            cmd.Parameters.Add(NewSafeParameter("@bt_zipcode", sa945.BtZipcode));
            cmd.Parameters.Add(NewSafeParameter("@bt_country_code", sa945.BtCountryCode));
            cmd.Parameters.Add(NewSafeParameter("@pro_number", sa945.ProNumber));
            cmd.Parameters.Add(NewSafeParameter("@master_bol_number", sa945.MasterBolNumber));
            cmd.Parameters.Add(NewSafeParameter("@service_level", sa945.ServiceLevel));
            cmd.Parameters.Add(NewSafeParameter("@delivery_appointment_number", sa945.DeliveryAppointmentNumber));
            cmd.Parameters.Add(NewSafeParameter("@purchase_order_date", sa945.PurchaseOrderDate));
            cmd.Parameters.Add(NewSafeParameter("@transportation_mode", sa945.TransportationMode));
            cmd.Parameters.Add(NewSafeParameter("@carriers_scac_code", sa945.CarriersScacCode));
            cmd.Parameters.Add(NewSafeParameter("@carriers_name", sa945.CarriersName));
            cmd.Parameters.Add(NewSafeParameter("@payment_method", sa945.PaymentMethod));
            cmd.Parameters.Add(NewSafeParameter("@allowance_or_charge_total_amount", sa945.AllowanceOrChargeTotalAmount));
            cmd.Parameters.Add(NewSafeParameter("@total_units_shipped", sa945.TotalUnitsShipped));
            cmd.Parameters.Add(NewSafeParameter("@total_weight_shipped", sa945.TotalWeightShipped));
            cmd.Parameters.Add(NewSafeParameter("@lading_quantity", sa945.LadingQuantity));
            cmd.Parameters.Add(NewSafeParameter("@unit_or_basis_for_measurement_code", sa945.UnitOrBasisForMeasurementCode));
            cmd.Parameters.Add(NewSafeParameter("@memo", sa945.Memo));
            cmd.Parameters.Add(NewSafeParameter("@file_name", sa945.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}
