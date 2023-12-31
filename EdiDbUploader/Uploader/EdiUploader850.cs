﻿using FBH.EDI.Common.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EdiDbUploader
{
    internal class EdiUploader850 : EdiUploader
    {
        private NpgsqlCommand cmd = null;
        public override List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            var logList = new List<string>();
            foreach (EdiDocument ediDoc in ediDocumentList)
            {
                cmd = new NpgsqlCommand();
                cmd.Connection = OpenConnection();
                var item = ediDoc as PurchaseOrder850;
                try
                {
                    var alreadyCount = ExecuteScalar($"select count(*) as count from edi.po_850 where po_no = '{item.PoNo}'");
                    int count = Convert.ToInt32(alreadyCount);
                    if (count > 0)
                    {
                        logList.Add($"HK: {item.PoNo} is alread exist in table");
                        cmd?.Transaction?.Commit();
                        continue;
                    }

                    cmd = SqCommand850(item);
                    cmd.ExecuteNonQuery();

                    foreach (PurchaseOrder850Detail detail in item.Details)
                    {
                        cmd = SqlCommand850Detail(detail);
                        cmd.ExecuteNonQuery();
                    }
                    foreach(PurchaseOrder850Allowance allowance in item.Allowences)
                    {
                        cmd = SqlCommand850Allowance(allowance);
                        cmd.ExecuteNonQuery();
                    }
                    cmd?.Transaction?.Commit();
                    logList.Add($"OK: {item.PoNo}");
                }
                catch (NpgsqlException ex)
                {
                    cmd.Transaction.Rollback();
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

        private NpgsqlCommand SqlCommand850Allowance(PurchaseOrder850Allowance allowance)
        {

            cmd.CommandText = "insert into edi.po_850_allowance("
                    + "po_no, seq, charge, desc_cd, amount, handling_cd, percent"
                    + ")values("
                    + "@po_no, @seq, @charge, @desc_cd, @amount, @handling_cd, @percent"
                    + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@po_no", allowance.PoNo));
            cmd.Parameters.Add(NewSafeParameter("@seq", allowance.Seq));
            cmd.Parameters.Add(NewSafeParameter("@charge", allowance.Charge));
            cmd.Parameters.Add(NewSafeParameter("@desc_cd", allowance.DescCd));
            cmd.Parameters.Add(NewSafeParameter("@amount", allowance.Amount));
            cmd.Parameters.Add(NewSafeParameter("@handling_cd", allowance.HandlingCd));
            cmd.Parameters.Add(NewSafeParameter("@percent", allowance.Percent));
            return cmd;

        }

        private NpgsqlCommand SqlCommand850Detail(PurchaseOrder850Detail detail)
        {
            cmd.CommandText = "insert into edi.po_850_dtl("
                    + "po_no, seq, line, qty, company_id, msrmnt, unit_price, gtin13, retailer_item_no, vendor_item_no, description, extended_cost"
                    + ")values("
                    + "@po_no, @seq, @line, @qty,(SELECT company_id FROM codes.stocks s WHERE retail_item_no = @retailer_item_no), @msrmnt, @unit_price, @gtin13, @retailer_item_no, @vendor_item_no, @description, @extended_cost"
                    + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@po_no", detail.PoNo));
            cmd.Parameters.Add(NewSafeParameter("@seq", detail.Seq));
            cmd.Parameters.Add(NewSafeParameter("@line", detail.Line));
            cmd.Parameters.Add(NewSafeParameter("@qty", detail.Qty));
            //cmd.Parameters.Add(NewSafeParameter("@company_id", detail.CompanyId));
            cmd.Parameters.Add(NewSafeParameter("@msrmnt", detail.Msrmnt));
            cmd.Parameters.Add(NewSafeParameter("@unit_price", detail.UnitPrice));
            cmd.Parameters.Add(NewSafeParameter("@gtin13", detail.Gtin13));
            cmd.Parameters.Add(NewSafeParameter("@retailer_item_no", detail.RetailerItemNo));
            cmd.Parameters.Add(NewSafeParameter("@vendor_item_no", detail.VendorItemNo));
            cmd.Parameters.Add(NewSafeParameter("@description", detail.Description));
            cmd.Parameters.Add(NewSafeParameter("@extended_cost", detail.ExtendedCost));
            return cmd;
        }

        private NpgsqlCommand SqCommand850(PurchaseOrder850 po850)
        {
            cmd.CommandText = "insert into edi.po_850("
               + "po_no, po_dt, promotion_deal_no, department_no, vendor_no, order_type, net_day, "
               + "delivery_ref_no, ship_not_before, ship_no_later, must_arrive_by, carrier_detail, "
               + "location, ship_payment, description, note, "
               + "bt_gln, dc_no, bt_nm, bt_addr, bt_city, bt_state, bt_zip, bt_country, "
               + "st_gln, st_nm, st_addr, st_city, st_state, st_zip, st_country, "
               + "week_of_year, memo, file_name, "
               + "created_by,yyyy"
               + ")values("
               + "@po_no, @po_dt, @promotion_deal_no, @department_no, @vendor_no, @order_type, @net_day, "
               + "@delivery_ref_no, @ship_not_before, @ship_no_later, @must_arrive_by, @carrier_detail, "
               + "@location, @ship_payment, @description, @note, "
               + "@bt_gln, @dc_no, @bt_nm, @bt_addr, @bt_city, @bt_state, @bt_zip, @bt_country, "
               + "@st_gln, @st_nm, @st_addr, @st_city, @st_state, @st_zip, @st_country, "
               + "(select woy from codes.week_of_year woy where @po_dt between start_day and to_char( to_date(start_day ,'YYYYMMDD') + INTERVAL '6 days', 'YYYYMMDD')),"
               + "@memo, @file_name,"
               + "@created_by,"
               + "(select year from codes.week_of_year woy where @po_dt between start_day and to_char( to_date(start_day ,'YYYYMMDD') + INTERVAL '6 days', 'YYYYMMDD'))"
               + ")";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(NewSafeParameter("@po_no", po850.PoNo));
            cmd.Parameters.Add(NewSafeParameter("@po_dt", po850.PoDt));
            cmd.Parameters.Add(NewSafeParameter("@promotion_deal_no", po850.PromotionDealNo));
            cmd.Parameters.Add(NewSafeParameter("@department_no", po850.DepartmentNo));
            cmd.Parameters.Add(NewSafeParameter("@vendor_no", po850.VendorNo));
            cmd.Parameters.Add(NewSafeParameter("@order_type", po850.OrderType));
            cmd.Parameters.Add(NewSafeParameter("@net_day", po850.NetDay));
            cmd.Parameters.Add(NewSafeParameter("@delivery_ref_no", po850.DeliveryRefNo));
            cmd.Parameters.Add(NewSafeParameter("@ship_not_before", po850.ShipNotBefore));
            cmd.Parameters.Add(NewSafeParameter("@ship_no_later", po850.ShipNoLater));
            cmd.Parameters.Add(NewSafeParameter("@must_arrive_by", po850.MustArriveBy));
            cmd.Parameters.Add(NewSafeParameter("@carrier_detail", po850.CarrierDetail));
            cmd.Parameters.Add(NewSafeParameter("@location", po850.Location));
            cmd.Parameters.Add(NewSafeParameter("@ship_payment", po850.ShipPayment));
            cmd.Parameters.Add(NewSafeParameter("@description", po850.Description));
            cmd.Parameters.Add(NewSafeParameter("@note", po850.Note));
            cmd.Parameters.Add(NewSafeParameter("@bt_gln", po850.BtGln));
            cmd.Parameters.Add(NewSafeParameter("@dc_no", po850.DcNo));
            cmd.Parameters.Add(NewSafeParameter("@bt_nm", po850.BtNm));
            cmd.Parameters.Add(NewSafeParameter("@bt_addr", po850.BtAddr));
            cmd.Parameters.Add(NewSafeParameter("@bt_city", po850.BtCity));
            cmd.Parameters.Add(NewSafeParameter("@bt_state", po850.BtState));
            cmd.Parameters.Add(NewSafeParameter("@bt_zip", po850.BtZip));
            cmd.Parameters.Add(NewSafeParameter("@bt_country", po850.BtCountry));
            cmd.Parameters.Add(NewSafeParameter("@st_gln", po850.StGln));
            cmd.Parameters.Add(NewSafeParameter("@st_nm", po850.StNm));
            cmd.Parameters.Add(NewSafeParameter("@st_addr", po850.StAddr));
            cmd.Parameters.Add(NewSafeParameter("@st_city", po850.StCity));
            cmd.Parameters.Add(NewSafeParameter("@st_state", po850.StState));
            cmd.Parameters.Add(NewSafeParameter("@st_zip", po850.StZip));
            cmd.Parameters.Add(NewSafeParameter("@st_country", po850.StCountry));
            //cmd.Parameters.Add(NewSafeParameter("@company_name", po850.CompanyName));
            //cmd.Parameters.Add(NewSafeParameter("@week_of_year", po850.WeekOfYear));
            cmd.Parameters.Add(NewSafeParameter("@memo", po850.Memo));
            cmd.Parameters.Add(NewSafeParameter("@file_name", po850.FileName));
            cmd.Parameters.Add(NewSafeParameter("@created_by", "DbUploader"));
            return cmd;
        }
    }
}