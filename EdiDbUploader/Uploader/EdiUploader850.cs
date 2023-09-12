using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EdiDbUploader
{
    internal class EdiUploader850 : EdiUploader
    {
        public override string Insert(EdiDocument ediDoc)
        {
            var po850 = ediDoc as PurchaseOrder850;

            object alreadyCount = ExecuteScalar($"select count(*) as count from edi.po_850 where po_no = '{po850.PoNo}'");
            int count = Convert.ToInt32(alreadyCount);
            if (count > 0)
            {
                return $"NK: {po850.PoNo} is alread exist in table";
            }

            NpgsqlTransaction tran = null;
            NpgsqlCommand cmd = null;
            try
            {
                tran = BeginTransaction();
                cmd = NewSqCommand850(po850);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                foreach (PurchaseOrder850Detail detail in po850.Details)
                {
                    cmd = NewSqlCommand850Detail(detail);
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                }
                foreach (PurchaseOrder850Allowance allowance in po850.Allowences)
                {
                    cmd = NewSqlCommand850Allowance(allowance);
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

        private NpgsqlCommand NewSqlCommand850Allowance(PurchaseOrder850Allowance allowance)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.po_850_dtl("
                    + "po_no, charge, desc_cd, amount, handling_cd, percent"
                    + ")values("
                    + "@po_no, @charge, @desc_cd, @amount, @handling_cd, @percent"
                    + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@po_no", allowance.PoNo));
            cmd.Parameters.Add(new NpgsqlParameter("@charge", allowance.Charge));
            cmd.Parameters.Add(new NpgsqlParameter("@desc_cd", allowance.DescCd));
            cmd.Parameters.Add(new NpgsqlParameter("@amount", allowance.Amount));
            cmd.Parameters.Add(new NpgsqlParameter("@handling_cd", allowance.HandlingCd));
            cmd.Parameters.Add(new NpgsqlParameter("@percent", allowance.Percent));
            return cmd;

        }

        private NpgsqlCommand NewSqlCommand850Detail(PurchaseOrder850Detail detail)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.po_850("
                    + "po_no, line, company_id, msrmnt, unit_price, gtin13, retailer_item_no, vendor_item_no, description, extended_cost"
                    + ")values("
                    + "@po_no, @line, @company_id, @msrmnt, @unit_price, @gtin13, @retailer_item_no, @vendor_item_no, @description, @extended_cost"
                    + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@po_no", detail.PoNo));
            cmd.Parameters.Add(new NpgsqlParameter("@line", detail.Line));
            cmd.Parameters.Add(new NpgsqlParameter("@company_id", detail. CompanyId));
            cmd.Parameters.Add(new NpgsqlParameter("@msrmnt", detail.Msrmnt));
            cmd.Parameters.Add(new NpgsqlParameter("@unit_price", detail.UnitPrice));
            cmd.Parameters.Add(new NpgsqlParameter("@gtin13", detail.Gtin13));
            cmd.Parameters.Add(new NpgsqlParameter("@retailer_item_no", detail.RetailerItemNo));
            cmd.Parameters.Add(new NpgsqlParameter("@vendor_item_no", detail.VendorItemNo));
            cmd.Parameters.Add(new NpgsqlParameter("@description", detail.Description));
            cmd.Parameters.Add(new NpgsqlParameter("@extended_cost", detail.ExtendedCost));
            return cmd;
        }

        private NpgsqlCommand NewSqCommand850(PurchaseOrder850 po850)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "insert into edi.invoice_810("
               + "po_no, po_dt, promotion_deal_no, department_no, vendor_no, order_type, net_day, "
               + "delivery_ref_no, ship_not_before, ship_no_later, must_arrive_by, carrier_detail, "
               + "location, ship_payment, description, note, "
               + "bt_gln, bt_nm, bt_addr, bt_city, bt_state, bt_zip, bt_country, "
               + "st_gln, st_nm, st_addr, st_city, st_state, st_zip, st_country, "
               + "company_name, week_of_year, "
               + "created_by"
               + ")values("
               + "@po_no, @po_dt, @promotion_deal_no, @department_no, @vendor_no, @order_type, @net_day, "
               + "@delivery_ref_no, @ship_not_before, @ship_no_later, @must_arrive_by, @carrier_detail, "
               + "@location, @ship_payment, @description, @note, "
               + "@bt_gln, @bt_nm, @bt_addr, @bt_city, @bt_state, @bt_zip, @bt_country, "
               + "@st_gln, @st_nm, @st_addr, @st_city, @st_state, @st_zip, @st_country, "
               + "@company_name, @week_of_year, "
               + "@created_by"
               + ")";
            cmd.Parameters.Add(new NpgsqlParameter("@po_no", po850.PoNo));
            cmd.Parameters.Add(new NpgsqlParameter("@po_dt", po850.PoDt));
            cmd.Parameters.Add(new NpgsqlParameter("@promotion_deal_no", po850.PromotionDealNo));
            cmd.Parameters.Add(new NpgsqlParameter("@department_no", po850.DepartmentNo));
            cmd.Parameters.Add(new NpgsqlParameter("@vendor_no", po850.VendorNo));
            cmd.Parameters.Add(new NpgsqlParameter("@order_type", po850.OrderType));
            cmd.Parameters.Add(new NpgsqlParameter("@net_day", po850.NetDay));
            cmd.Parameters.Add(new NpgsqlParameter("@delivery_ref_no", po850.DeliveryRefNo));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_not_before", po850.ShipNotBefore));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_no_later", po850.ShipNoLater));
            cmd.Parameters.Add(new NpgsqlParameter("@must_arrive_by", po850.MustArriveBy));
            cmd.Parameters.Add(new NpgsqlParameter("@carrier_detail", po850.CarrierDetail));
            cmd.Parameters.Add(new NpgsqlParameter("@location", po850.Location));
            cmd.Parameters.Add(new NpgsqlParameter("@ship_payment", po850.ShipPayment));
            cmd.Parameters.Add(new NpgsqlParameter("@description", po850.Description));
            cmd.Parameters.Add(new NpgsqlParameter("@note", po850.Note));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_gln", po850.BtGln));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_nm", po850.BtNm));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_addr", po850.BtAddr));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_city", po850.BtCity));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_state", po850.BtState));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_zip", po850.BtZip));
            cmd.Parameters.Add(new NpgsqlParameter("@bt_country", po850.BtCountry));
            cmd.Parameters.Add(new NpgsqlParameter("@st_gln", po850.StGln));
            cmd.Parameters.Add(new NpgsqlParameter("@st_nm", po850.StNm));
            cmd.Parameters.Add(new NpgsqlParameter("@st_addr", po850.StAddr));
            cmd.Parameters.Add(new NpgsqlParameter("@st_city", po850.StCity));
            cmd.Parameters.Add(new NpgsqlParameter("@st_state", po850.StState));
            cmd.Parameters.Add(new NpgsqlParameter("@st_zip", po850.StZip));
            cmd.Parameters.Add(new NpgsqlParameter("@st_country", po850.StCountry));
            cmd.Parameters.Add(new NpgsqlParameter("@company_name", po850.CompanyName));
            cmd.Parameters.Add(new NpgsqlParameter("@week_of_year", po850.WeekOfYear));
            cmd.Parameters.Add(new NpgsqlParameter("@created_by", "DbUploader"));
            return cmd;
        }
    }
}