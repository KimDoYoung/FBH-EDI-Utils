-- 
-- 810
--
DROP TABLE IF EXISTS invoice_810 CASCADE ;
CREATE TABLE IF NOT EXISTS invoice_810 
(
	invoice_no VARCHAR(30) PRIMARY KEY,	 
	po_no varchar(30) null,
	supplier_nm  varchar(100) null,
	supplier_city varchar(100) null,
	supplier_state varchar(100) null,
	supplier_zip varchar(100) null,
	supplier_country varchar(100) null,
	department_no varchar(100) null,
	currency varchar(100) null,
	vendor_no varchar(100) null,
	net_day varchar(100) null,
	mcd_type varchar(100) null,
	fob varchar(100) null,
	ship_to_nm varchar(100) null,
	ship_to_gln varchar(100) null,
	ship_to_addr varchar(100) null,
	ttl_amt decimal(10,2) null,
	created_by varchar(30) not null,
	created_on timestamp not null default CURRENT_TIMESTAMP,
	last_update_by varchar(30) null,
	last_update_on timestamp null
);

DROP TABLE IF EXISTS invoice_810_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS invoice_810_dtl 
(
	invoice_no VARCHAR(30) not null,	
	seq int not null,
	po_no varchar(30) not null,
    qty int not null,
	msrmnt varchar(100) null,
	unit_price decimal(10,2) null,
	gtin13 varchar(100) null,
	line_ttl decimal(10,2) null,
	primary key(invoice_no, seq)
);
