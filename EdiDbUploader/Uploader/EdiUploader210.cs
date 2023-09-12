using EdiDbUploader;
using FBH.EDI.Common.Model;
using Npgsql;
using System;

namespace EdiDbUploader
{
    internal class EdiUploader210 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var freight210 = ediDoc as FreightInvoice210;

            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.freight_210 where invoice_no = '{freight210.InvoiceNo}'");
            int count = Convert.ToInt32(alreadyCount);
            if (count > 0)
            {
                return $"NK: {freight210.InvoiceNo} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand210(freight210);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                foreach (FreightInvoice210Detail detail in freight210.Details)
                {
                    cmd = NewSqlCommand210Detail(detail);
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

        private NpgsqlCommand NewSqlCommand210Detail(FreightInvoice210Detail detail)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.invoice_dtl("
                    +"invoice_no, transaction_set_line_number, purchase_order_number, shipped_date, "
                    +"lading_line_item, lading_description, billedratedasquantity, weight, ladingquantity, "
                    +"freightrate, amountcharged, special_charge_or_allowance_cd"         
                    +")values("
                    + "@invoice_no, @transaction_set_line_number, @purchase_order_number, @shipped_date, "
                    + "@lading_line_item, @lading_description, @billedratedasquantity, @weight, @ladingquantity, "
                    + "@freightrate, @amountcharged, @special_charge_or_allowance_cd"
                    + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@invoice_no", detail.InvoiceNo));
            cmd.Parameters.Add(new NpgsqlParameter("@transaction_set_line_number", detail.TransactionSetLineNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@purchase_order_number", detail.PurchaseOrderNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@shipped_date", detail.ShippedDate));
            cmd.Parameters.Add(new NpgsqlParameter("@lading_line_item", detail.LadingLineItem));
            cmd.Parameters.Add(new NpgsqlParameter("@lading_description", detail.LadingDescription));
            cmd.Parameters.Add(new NpgsqlParameter("@billedratedasquantity", detail.BilledRatedAsQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@weight", detail.Weight));
            cmd.Parameters.Add(new NpgsqlParameter("@ladingquantity", detail.LadingQuantity));
            cmd.Parameters.Add(new NpgsqlParameter("@freightrate", detail.FreightRate));
            cmd.Parameters.Add(new NpgsqlParameter("@amountcharged", detail.AmountCharged));
            cmd.Parameters.Add(new NpgsqlParameter("@special_charge_or_allowance_cd", detail.SpecialChargeOrAllowanceCd));
            return cmd;
        }

        private NpgsqlCommand NewSqCommand210(FreightInvoice210 freight210)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.invoice_810("
                + "invoice_no, shipid_no, ship_method_of_payment, invoice_dt, amount_to_be_paid, po_number, vics_bol_no, "
                + "ship_from_company_name, ship_from_addrinfo, ship_from_city, ship_from_state, ship_from_zipcode, ship_from_countrycd, "
                + "ship_to_companyname, ship_to_addrinfo, ship_to_city, ship_to_state, ship_to_zipcode, ship_to_countrycd, "
                + "bill_to_companyname, bill_to_addrinfo, bill_to_city, bill_to_state, bill_to_zipcode, bill_to_countrycd, " 
                + "total_weight, total_weight_unit, weight_qualifier, amount_charged, bol_qty_in_cases, "
                + "created_by"
                + ")values("
                + "@invoice_no, @shipid_no, @ship_method_of_payment, @invoice_dt, @amount_to_be_paid, @po_number, @vics_bol_no, "
                + "@ship_from_company_name, @ship_from_addrinfo, @ship_from_city, @ship_from_state, @ship_from_zipcode, @ship_from_countrycd, "
                + "@ship_to_companyname, @ship_to_addrinfo, @ship_to_city, @ship_to_state, @ship_to_zipcode, @ship_to_countrycd, "
                + "@bill_to_companyname, @bill_to_addrinfo, @bill_to_city, @bill_to_state, @bill_to_zipcode, @bill_to_countrycd, "
                + "@total_weight, @total_weight_unit, @weight_qualifier, @amount_charged, @bol_qty_in_cases, "
                + "@created_by"
                + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@invoice_no", freight210.InvoiceNo));
            cmd.Parameters.Add(new NpgsqlParameter("@shipid_no", freight210.ShipIdNo));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_method_of_payment", freight210.ShipMethodOfPayment));
            cmd.Parameters.Add(new NpgsqlParameter("@invoice_dt", freight210.InvoiceDt));
            cmd.Parameters.Add(new NpgsqlParameter("@amount_to_be_paid", freight210.AmountToBePaid));
            cmd.Parameters.Add(new NpgsqlParameter("@po_number", freight210.PoNumber));
            cmd.Parameters.Add(new NpgsqlParameter("@vics_bol_no", freight210.VicsBolNo));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_from_company_name", freight210.ShipFromCompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_from_addrinfo", freight210.ShipFromAddrInfo));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_from_city", freight210.ShipFromCity));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_from_state", freight210.ShipFromState));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_from_zipcode", freight210.ShipFromZipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_from_countrycd", freight210.ShipFromCountryCd));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_to_companyname", freight210.ShipToCompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_to_addrinfo", freight210.ShipToAddrInfo));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_to_city", freight210.ShipToCity));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_to_state", freight210.ShipToState));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_to_zipcode", freight210.ShipToZipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_to_countrycd", freight210.ShipToCountryCd));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_companyname", freight210.BillToCompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_addrinfo", freight210.BillToAddrInfo));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_city", freight210.BillToCity));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_state", freight210.BillToState));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_zipcode", freight210.BillToZipcode));
            cmd.Parameters.Add(new NpgsqlParameter("@bill_to_countrycd", freight210.BillToCountryCd));
            cmd.Parameters.Add(new NpgsqlParameter("@total_weight", freight210.TotalWeight));
            cmd.Parameters.Add(new NpgsqlParameter("@total_weight_unit", freight210.TotalWeightUnit));
            cmd.Parameters.Add(new NpgsqlParameter("@weight_qualifier", freight210.WeightQualifier));
            cmd.Parameters.Add(new NpgsqlParameter("@amount_charged", freight210.AmountCharged));
            cmd.Parameters.Add(new NpgsqlParameter("@bol_qty_in_cases", freight210.BolQtyInCases));
            cmd.Parameters.Add(new NpgsqlParameter("@created_by", "DbUploader"));

            return cmd;
        }
    }
}