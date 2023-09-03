-- purchase order 850
DROP TABLE IF EXISTS po_850 CASCADE ;
CREATE TABLE IF NOT EXISTS po_850 
(
	po_no varchar(30) not null,
	po_dt varchar(8) not null,
	promotion_deal_no varchar(100) null,
	department_no  varchar(100) null,
	vendor_no  varchar(100) null,
	order_type  varchar(100) null,
	net_day  int null,
	delivery_ref_no  varchar(100) null,
	ship_not_before  varchar(100) null,
	ship_no_later  varchar(100) null,
	must_arrive_by  varchar(100) null,
	carrier_detail  varchar(100) null,
	location  varchar(100) null,
	ship_payment  varchar(100) null,
	description  varchar(100) null,
	note  varchar(100) null,
	bt_gln  varchar(100) null,
	bt_nm  varchar(100) null,
	bt_addr  varchar(100) null,
	bt_city  varchar(100) null,
	bt_state  varchar(100) null,
	bt_zip  varchar(100) null,
	bt_country  varchar(100) null,
	st_gln  varchar(100) null,
	st_nm  varchar(100) null,
	st_addr  varchar(100) null,
	st_city  varchar(100) null,
	st_state  varchar(100) null,
	st_zip  varchar(100) null,
	st_country  varchar(100) null,
	company_name varchar(100) null,
	week_of_year int null,
	created_by varchar(30) not null,
	created_on timestamp not null default CURRENT_TIMESTAMP,
	last_update_by varchar(30) null,
	last_update_on timestamp null	
	primary key( po_no)
 );
DROP TABLE IF EXISTS po_850_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS po_850_dtl
(
	po_no varchar(30) not null,
	line varchar(10) not null,
	msrmnt varchar(100) null,
	unit_price decimal(8,2) null,
	gtin13 varchar(100) null,
	retailer_Item_No varchar(100) null,
	Vendor_Item_No varchar(100) null,
	Description varchar(100) null,
	Extended_Cost  decimal(10,2),
	primary key(po_no, line)
);
DROP TABLE IF EXISTS po_850_allowance CASCADE ;
CREATE TABLE IF NOT EXISTS po_850_allowance
(
	po_no varchar(30) not null,
	charge varchar(100) not null,
	desc_cd varchar(100) null,
	amount int null,
	handling_cd varchar(100) null,
	percent decimal(8,2) null,
	primary key (po_no, charge)
);

