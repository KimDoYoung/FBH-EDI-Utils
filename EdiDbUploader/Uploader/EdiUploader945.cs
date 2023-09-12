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
            cmd.Parameters.Add(new NpgsqlParameter("@customer_order_id", detail.CustomerOrderId));
            cmd.Parameters.Add(new NpgsqlParameter("@assigned_number", detail.AssignedNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@pallet_id", detail.PalletId));
            cmd.Parameters.Add(new NpgsqlParameter("@carrier_tracking_number", detail.CarrierTrackingNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@shipment_status", detail.ShipmentStatus));
            cmd.Parameters.Add(new NpgsqlParameter("@requested_quantity", detail.RequestedQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@actual_quantity_shipped", detail.ActualQuantityShipped));
            cmd.Parameters.Add(new NpgsqlParameter("@difference_between_actual_and_requested", detail.DifferenceBetweenActualAndRequested));
            cmd.Parameters.Add(new NpgsqlParameter("@unit_or_basis_measurement_code", detail.UnitOrBasisMeasurementCode));
            cmd.Parameters.Add(new NpgsqlParameter("@upc_code", detail.UpcCode));
            cmd.Parameters.Add(new NpgsqlParameter("@sku_no", detail.SkuNo));
            cmd.Parameters.Add(new NpgsqlParameter("@lot_batch_code", detail.LotBatchCode));
            cmd.Parameters.Add(new NpgsqlParameter("@total_weight_for_item_line", detail.TotalWeightForItemLine));
            cmd.Parameters.Add(new NpgsqlParameter("@retailers_item_number", detail.RetailersItemNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@line_number", detail.LineNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@expiration_date", detail.ExpirationDate));
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
                + "total_weight_shipped, lading_quantity, unit_or_basisvfor_measurement_code, "
                + "created_by"
                + ")values("
                + "@customer_order_id, @actual_pickup_date, @vics_bol, @hub_groups_order_number, @purchase_order_number, @"
                + "@mater_vics_bol, @link_sequence_number, @sf_company_name, @sf_seller_buyer, @sf_location_id_code, @"
                + "@sf_address_info, @sf_city, @sf_state, @sf_zipcode, @sf_country_code, @st_company_name, @st_seller_buyer, @"
                + "@st_location_id_code, @st_address_info, @st_city, @st_state, @st_zipcode, @st_country_code, @mf_company_name, @"
                + "@mf_seller_buyer, @mf_location_id_code, @mf_address_info, @mf_city, @mf_state, @mf_zipcode, @mf_country_code, @"
                + "@bt_company_name, @bt_seller_buyer, @bt_location_id_code, @bt_address_info, @bt_city, @bt_state, @bt_zipcode,"
                + "@bt_country_code, @pro_number, @master_bol_number, @service_level, @delivery_appointment_number, @purchase_order_date, @"
                + "@transportation_mode, @carriers_scac_code, @carriers_name, @payment_method, @allowance_or_charge_total_amount, @total_units_shipped,"
                + "@total_weight_shipped, @lading_quantity, @unit_or_basisvfor_measurement_code,"
                + "@created_by"
                + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@customer_order_id", sa945.CustomerOrderId));
            cmd.Parameters.Add(new NpgsqlParameter("@actual_pickup_date", sa945.ActualPickupDate));
            cmd.Parameters.Add(new NpgsqlParameter("@vics_bol", sa945.VicsBOL));
            cmd.Parameters.Add(new NpgsqlParameter("@hub_groups_order_number", sa945.HubGroupsOrderNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@purchase_order_number", sa945.PurchaseOrderNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@mater_vics_bol", sa945.MaterVicsBol));
            cmd.Parameters.Add(new NpgsqlParameter("@link_sequence_number", sa945.LinkSequenceNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_company_name", sa945.SfCompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_seller_buyer", sa945.SfSellerBuyer));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_location_id_code", sa945.SfLocationIdCode));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_address_info", sa945.SfAddressInfo));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_city", sa945.SfCity));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_state", sa945.SfState));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_zipcode", sa945.SfZipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@sf_country_code", sa945.SfCountryCode));
            cmd.Parameters.Add(new NpgsqlParameter("@st_company_name", sa945.StCompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@st_seller_buyer", sa945.StSellerBuyer));
            cmd.Parameters.Add(new NpgsqlParameter("@st_location_id_code", sa945.StLocationIdCode));
            cmd.Parameters.Add(new NpgsqlParameter("@st_address_info", sa945.StAddressInfo));
            cmd.Parameters.Add(new NpgsqlParameter("@st_city", sa945.StCity));
            cmd.Parameters.Add(new NpgsqlParameter("@st_state", sa945.StState));
            cmd.Parameters.Add(new NpgsqlParameter("@st_zipcode", sa945.StZipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@st_country_code", sa945.StCountryCode));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_company_name", sa945.MfCompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_seller_buyer", sa945.MfSellerBuyer));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_location_id_code", sa945.MfLocationIdCode));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_address_info", sa945.MfAddressInfo));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_city", sa945.MfCity));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_state", sa945.MfState));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_zipcode", sa945.MfZipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@mf_country_code", sa945.MfCountryCode));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_company_name", sa945.BtCompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_seller_buyer", sa945.BtSellerBuyer));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_location_id_code", sa945.BtLocationIdCode));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_address_info", sa945.BtAddressInfo));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_city", sa945.BtCity));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_state", sa945.BtState));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_zipcode", sa945.BtZipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_country_code", sa945.BtCountryCode));
            cmd.Parameters.Add(new NpgsqlParameter("@pro_number", sa945.ProNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@master_bol_number", sa945.MasterBolNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@service_level", sa945.ServiceLevel));
            cmd.Parameters.Add(new NpgsqlParameter("@delivery_appointment_number", sa945.DeliveryAppointmentNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@purchase_order_date", sa945.PurchaseOrderDate));
            cmd.Parameters.Add(new NpgsqlParameter("@transportation_mode", sa945.TransportationMode));
            cmd.Parameters.Add(new NpgsqlParameter("@carriers_scac_code", sa945.CarriersScacCode));
            cmd.Parameters.Add(new NpgsqlParameter("@carriers_name", sa945.CarriersName));
            cmd.Parameters.Add(new NpgsqlParameter("@payment_method", sa945.PaymentMethod));
            cmd.Parameters.Add(new NpgsqlParameter("@allowance_or_charge_total_amount", sa945.AllowanceOrChargeTotalAmount));
            cmd.Parameters.Add(new NpgsqlParameter("@total_units_shipped", sa945.TotalUnitsShipped));
            cmd.Parameters.Add(new NpgsqlParameter("@total_weight_shipped", sa945.TotalWeightShipped));
            cmd.Parameters.Add(new NpgsqlParameter("@lading_quantity", sa945.LadingQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@unit_or_basis_for_measurement_code", sa945.UnitOrBasisForMeasurementCode));
            cmd.Parameters.Add(new NpgsqlParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}
