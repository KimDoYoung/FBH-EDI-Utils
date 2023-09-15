--
-- EDI Documents Tables
--

DROP TABLE IF EXISTS edi.invoice_810 CASCADE ;
CREATE TABLE IF NOT EXISTS edi.invoice_810 
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
	memo varchar(500) NULL,
	created_by varchar(30) not null,
	created_on timestamp not null default CURRENT_TIMESTAMP,
	last_update_by varchar(30) null,
	last_update_on timestamp null
);

DROP TABLE IF EXISTS edi.invoice_810_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS edi.invoice_810_dtl 
(
	invoice_no VARCHAR(30) not null,	
	seq int not null,
	po_no varchar(30) not null,
    qty int not null,
	msrmnt varchar(100) null,
	unit_price decimal(10,2) null,
	gtin13 varchar(100) null,
	line_ttl decimal(10,2) null,
	primary key(invoice_no, seq),
	CONSTRAINT fk_invoice_no FOREIGN KEY (invoice_no) 
	    REFERENCES edi.invoice_810(invoice_no) ON DELETE CASCADE ON UPDATE CASCADE
);

-- purchase order 850
DROP TABLE IF EXISTS edi.po_850 CASCADE ;
CREATE TABLE IF NOT EXISTS edi.po_850 
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
	last_update_on timestamp null,
	memo varchar(500) NULL,
	primary key( po_no)
 );
DROP TABLE IF EXISTS edi.po_850_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS edi.po_850_dtl
(
	po_no varchar(30) not null,
	line varchar(10) not null,
	company_id int NOT NULL, 
	msrmnt varchar(100) null,
	unit_price decimal(8,2) null,
	gtin13 varchar(100) null,
	retailer_Item_No varchar(100) null,
	Vendor_Item_No varchar(100) null,
	Description varchar(100) null,
	Extended_Cost  decimal(10,2),
	primary key(po_no, line),
	CONSTRAINT fk_po_no_dtl FOREIGN KEY (po_no) 
	    REFERENCES edi.po_850(po_no) ON DELETE CASCADE ON UPDATE CASCADE
);
DROP TABLE IF EXISTS edi.po_850_allowance CASCADE ;
CREATE TABLE IF NOT EXISTS edi.po_850_allowance
(
	po_no varchar(30) not null,
	charge varchar(100) not null,
	desc_cd varchar(100) null,
	amount int null,
	handling_cd varchar(100) null,
	percent decimal(8,2) null,
	primary key (po_no, charge),
	CONSTRAINT fk_po_no_allowance FOREIGN KEY (po_no) 
	    REFERENCES edi.po_850(po_no) ON DELETE CASCADE ON UPDATE CASCADE
);

--
-- freight invoice 210
-- 1. detail 제거
-- 2. address 통합
--
DROP TABLE IF EXISTS edi.freight_210 CASCADE ;
CREATE TABLE IF NOT EXISTS edi.freight_210
(
	invoice_no  varchar(30) not null,
	invoice_dt  varchar(8) null,
	ship_id_no  varchar(100) null,
	ship_method_of_payment  varchar(100) null,
	amount_to_be_paid  decimal(10,2) null,
	po_number  varchar(100) null,
	vics_bol_no  varchar(100) null,
	dc_no varchar(10) NULL,
	--
	warehouse_name varchar(100) null,
	warehouse_address varchar(200) null,
	consignee_name varchar(100) null,	
	consignee_address varchar(200) null,
	bill_to_name varchar(100) null,
	bill_to_address varchar(200) null,
	--
	total_weight decimal(10,2) null,
	total_weight_unit varchar(10) null,
	weight_qualifier  varchar(10) null,
	amount_charged  decimal(10,2) null,
	qty int,
	memo varchar(500) NULL,
	--
	created_by varchar(30) not null,
	created_on timestamp not null default CURRENT_TIMESTAMP,
	last_update_by varchar(30) null,
	last_update_on timestamp null,
	file_name varchar(300) NULL,
	primary key(invoice_no)
);

-- warehouse shipping order 945
drop table if exists edi.shipping_945 cascade ;
create table if not exists edi.shipping_945
(
	customer_order_id varchar(100) not null,
	actual_pickup_date varchar(100) null,
	vics_bol varchar(100) null,
	hub_groups_order_number varchar(100) null,
	purchase_order_number varchar(100) null,
	mater_vics_bol varchar(100) null,
	link_sequence_number varchar(100) null,
	sf_company_name varchar(100) null,
	sf_seller_buyer varchar(100) null,
	sf_location_id_code varchar(100) null,
	sf_address_info varchar(100) null,
	sf_city varchar(100) null,
	sf_state varchar(100) null,
	sf_zipcode varchar(100) null,
	sf_country_code varchar(100) null,
	st_company_name varchar(100) null,
	st_seller_buyer varchar(100) null,
	st_location_id_code varchar(100) null,
	st_address_info varchar(100) null,
	st_city varchar(100) null,
	st_state varchar(100) null,
	st_zipcode varchar(100) null,
	st_country_code varchar(100) null,
	mf_company_name varchar(100) null,
	mf_seller_buyer varchar(100) null,
	mf_location_id_code varchar(100) null,
	mf_address_info varchar(100) null,
	mf_city varchar(100) null,
	mf_state varchar(100) null,
	mf_zipcode varchar(100) null,
	mf_country_code varchar(100) null,
	bt_company_name varchar(100) null,
	bt_seller_buyer varchar(100) null,
	bt_location_id_code varchar(100) null,
	bt_address_info varchar(100) null,
	bt_city varchar(100) null,
	bt_state varchar(100) null,
	bt_zipcode varchar(100) null,
	bt_country_code varchar(100) null,
	pro_number varchar(100) null,
	master_bol_number varchar(100) null,
	service_level varchar(100) null,
	delivery_appointment_number varchar(100) null,
	purchase_order_date varchar(100) null,
	transportation_mode varchar(100) null,
	carriers_scac_code varchar(100) null,
	carriers_name varchar(100) null,
	payment_method varchar(100) null,
	allowance_or_charge_total_amount varchar(100) null,
	total_units_shipped varchar(100) null,
	total_weight_shipped varchar(100) null,
	lading_quantity varchar(100) null,
	unit_or_basis_for_measurement_code varchar(100) null,
	created_by varchar(30) not null,
	created_on timestamp not null default current_timestamp,
	last_update_by varchar(30) null,
	last_update_on timestamp null,
	memo varchar(500) NULL,
	primary key(customer_order_id)
);  
drop table if exists edi.shipping_945_dtl cascade ;
create table if not exists edi.shipping_945_dtl
(
	customer_order_id varchar(100) not null,
	assigned_number int not null,
	pallet_id varchar(100) null,
	carrier_tracking_number  varchar(100) null,
	shipment_status  varchar(100) null,
	requested_quantity  varchar(100) null,
	actual_quantity_shipped  varchar(100) null,
	difference_between_actual_and_requested  varchar(100) null,
	unit_or_basis_measurement_code  varchar(100) null,
	upc_code  varchar(100) null,
	sku_no  varchar(100) null,
	lot_batch_code  varchar(100) null,
	total_weight_for_item_line  varchar(100) null,
	retailers_item_number  varchar(100) null,
	line_number  varchar(100) null,
	expiration_date  varchar(100) null,
	memo varchar(500) NULL,
	primary key(customer_order_id, assigned_number),
	CONSTRAINT fk_customer_order_id_945 FOREIGN KEY (customer_order_id) 
	    REFERENCES edi.shipping_945(customer_order_id) ON DELETE CASCADE ON UPDATE CASCADE	
);

--944
DROP TABLE IF EXISTS edi.transfer_944 CASCADE ;
CREATE TABLE IF NOT EXISTS edi.transfer_944
(
	hub_groups_order_number varchar(30)  not null,
	receipt_date varchar(10) not null,
	customer_order_id varchar(30) not null,
	customers_bol_number varchar(100) null, 
	hub_groups_warehousename  varchar(100) null, 
	hub_groups_customers_warehouse_id  varchar(100) null, 
	destination_address_information  varchar(100) null, 
	destination_city  varchar(100) null, 
	destination_state  varchar(100) null, 
	destination_zipcode  varchar(100) null, 
	origin_company_name  varchar(100) null, 
	shipper_company_id  varchar(100) null, 
	origin_address_information  varchar(100) null, 
	origin_city  varchar(100) null, 
	origin_state  varchar(100) null, 
	origin_zipcode  varchar(100) null, 
	scheduled_delivery_date  varchar(100) null, 
	transportation_method_type_code  varchar(100) null, 
	standard_carrier_alpha_code  varchar(100) null, 
	quantity_received  varchar(100) null, 
	number_of_units_shipped  varchar(100) null, 
	quantity_damaged_on_hold  varchar(100) null, 
	memo varchar(500) NULL,
--
	created_by varchar(30) not null,
	created_on timestamp not null default current_timestamp,
	last_update_by varchar(30) null,
	last_update_on timestamp null,
--
	primary key(hub_groups_order_number)
);

DROP TABLE IF EXISTS edi.transfer_944_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS edi.transfer_944_dtl
(
	hub_groups_order_number varchar(30)  not null,
	assigned_number int not null,
	receipt_date varchar(10) null,
	stock_receipt_quantity_received  varchar(100) null,
	stock_receipt_unit_of_measure_code   varchar(100) null,
	stock_receipt_sku   varchar(100) null,
	stock_receipt_lot_batch_code   varchar(100) null,
	exception_quantity   varchar(100) null,
	exception_unit_of_measure_code   varchar(100) null,
	exception_receiving_condition_code   varchar(100) null,
	exception_lot_batch_code   varchar(100) null,
	exception_damage_condition   varchar(100) null,
	primary key (hub_groups_order_number, assigned_number),
	CONSTRAINT fk_transfer_944 FOREIGN KEY (hub_groups_order_number) 
	    REFERENCES edi.transfer_944(hub_groups_order_number) ON DELETE CASCADE ON UPDATE CASCADE		
);

--940
DROP TABLE IF EXISTS edi.shipping_order_940 CASCADE ;
CREATE TABLE IF NOT EXISTS edi.shipping_order_940
(
	order_id varchar(100) not null,
	order_no varchar(100) null,
	buyer_po_number  varchar(100) null,
	warehouse_info  varchar(100) null,
	ship_to  varchar(100) null,
	reference_identification  varchar(100) null,
	requested_pickup_date  varchar(100) null,
	requested_delivery_date  varchar(100) null,
	cancel_after_date  varchar(100) null,
	purchase_order_date  varchar(100) null,
	warehouse_carrier_info  varchar(100) null,
	order_group_id  varchar(100) null,
	memo varchar(500) NULL,
--	
	created_by varchar(30) not null,
	created_on timestamp not null default current_timestamp,
	last_update_by varchar(30) null,
	last_update_on timestamp null,

	primary key(order_id)
);

DROP TABLE IF EXISTS edi.shipping_order_940_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS edi.shipping_order_940_dtl
(
	order_id varchar(100) not null,
	seq int not null,
	quantity_ordered varchar(100) null,
	unit_of_measure  varchar(100) null,
	upc_code  varchar(100) null,
	sku  varchar(100) null,
	retailers_item_code  varchar(100) null,
	lot_number  varchar(100) null,
	scc14  varchar(100) null,
	free_form_description  varchar(100) null,
	retail_price  varchar(100) null,
	cost_price  varchar(100) null,
	misc1_number_of_pack  varchar(100) null,
	misc1_size_of_units  varchar(100) null,
	misc1_size_unit  varchar(100) null,
	misc1_color_description  varchar(100) null,
	misc2_number_of_pack  varchar(100) null,
	misc2_size_of_units  varchar(100) null,
	misc2_size_unit  varchar(100) null,
	misc2_color_description  varchar(100) null,
	misc3_number_of_pack  varchar(100) null,
	misc3_size_of_units  varchar(100) null,
	misc3_size_unit  varchar(100) null,
	misc3_color_description  varchar(100) null,
	
	primary key(order_id, seq),
	CONSTRAINT fk_shipping_order_940_dtl FOREIGN KEY (order_id) 
	    REFERENCES edi.shipping_order_940(order_id) ON DELETE CASCADE ON UPDATE CASCADE		
);


-- 846
DROP TABLE IF EXISTS edi.inquiry_846 CASCADE ;
CREATE TABLE IF NOT EXISTS edi.inquiry_846
(
	hub_group_document_number varchar(100) not null,
	date_expresses  varchar(100) null,
	date_time_qualifier  varchar(100) null,
	date  varchar(100) null,
	warehouse_name  varchar(100) null,
	warehouse_id  varchar(100) null,
	address_information  varchar(100) null,
	city  varchar(100) null,
	state  varchar(100) null,
	zipcode  varchar(100) null,
	memo varchar(500) NULL,
--
	created_by varchar(30) not null,
	created_on timestamp not null default current_timestamp,
	last_update_by varchar(30) null,
	last_update_on timestamp null,
	primary key(hub_group_document_number)
);
DROP TABLE IF EXISTS edi.inquiry_846_dtl CASCADE ;
CREATE TABLE IF NOT EXISTS edi.inquiry_846_dtl
(
	hub_group_document_number  varchar(100) not null,
	assgnd_no int not null,
	sku varchar(100) null,
	lot_code  varchar(100) null,
	non_committed_in int null,
	non_committed_out int null,
	on_hand_quantity int null,
	inbound_pending int null,
	outbound_pending int null,
	damaged_quantity int null,
	onhold_quantity int null,
	available_quantity int null,
	total_inventory int null,
	primary key( hub_group_document_number , assgnd_no ),
	CONSTRAINT fk_inquiry_846_dtl FOREIGN KEY (hub_group_document_number) 
	    REFERENCES edi.inquiry_846(hub_group_document_number) ON DELETE CASCADE ON UPDATE CASCADE		
);


