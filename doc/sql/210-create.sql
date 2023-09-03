
-- 210
DROP TABLE IF EXISTS freight_210 CASCADE ;
CREATE TABLE IF NOT EXISTS freight_210
(
	invoice_no  varchar(30) not null,
	shipid_no  varchar(100) null,
	ship_method_of_payment  varchar(100) null,
	invoice_dt  varchar(100) null,
	amount_to_be_paid  int,
	po_number  varchar(100) null,
	vics_bol_no  varchar(100) null,
	ship_from_company_name varchar(100) null,
	ship_from_addrinfo varchar(100) null,
	ship_from_city varchar(100) null,
	ship_from_state varchar(100) null,
	ship_from_zipcode varchar(100) null,
	ship_from_countrycd varchar(100) null,
	ship_to_companyname varchar(100) null,
	ship_to_addrinfo varchar(100) null,
	ship_to_city varchar(100) null,
	ship_to_state varchar(100) null,
	ship_to_zipcode varchar(100) null,
	ship_to_countrycd varchar(100) null,
	bill_to_companyname varchar(100) null,
	bill_to_addrinfo varchar(100) null,
	bill_to_city varchar(100) null,
	bill_to_state varchar(100) null,
	bill_to_zipcode varchar(100) null,
	bill_to_countrycd varchar(100) null,
	total_weight decimal(10,2) null,
	total_weight_unit decimal(10,2)null,
	weight_qualifier  varchar(100)null,
	amount_charged  int,
	bol_qty_in_cases int,
	created_by varchar(30) not null,
	created_on timestamp not null default CURRENT_TIMESTAMP,
	last_update_by varchar(30) null,
	last_update_on timestamp null,
	primary key(invoice_no)
);
DROP TABLE IF EXISTS freight_210_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS freight_210_dtl
(
	invoice_no varchar(30) not null,
	transaction_set_line_number  varchar(100) null,
	purchase_order_number  varchar(100) null,
	shipped_date  varchar(100) null,
	lading_line_item  varchar(100) null,
	lading_description  varchar(100) null,
	billedratedasquantity int null,
	weight decimal(10,2) null, 
	ladingquantity  int null,
	freightrate  decimal(10,2) null,
	amountcharged  int null,
	special_charge_or_allowance_cd  varchar(100) null,
	primary key(invoice_no, transaction_set_line_number)
);
