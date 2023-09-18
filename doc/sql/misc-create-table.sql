-- edi.stocks
-- edi.company
-- ship_to_addr
-- edi.week_of_year

--
-- warehouse stock no
--
DROP TABLE IF EXISTS edi.stocks CASCADE ;
CREATE TABLE IF NOT EXISTS edi.stocks
(
 warehouse_stock_no varchar(5) not null,
 hub_group varchar(30) null,
 upc varchar(20) null,
 gtin varchar(20) null,
 stk_no varchar(10) null,
 retail_item_no varchar(15) null,
 retail_item_name varchar(100) null,
 company_id int NULL,
 use_yn char(1) NOT NULL DEFAULT 'Y',
 CONSTRAINT uq_stock_retail_item_no UNIQUE (retail_item_no),
 CONSTRAINT pk_stocks primary key(warehouse_stock_no)
);
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Walmart','8809729360523','18809729360520','P60523','60523','658517851','Sunkist Fruit Cups Pineapple 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Walmart','8809729360516','18809729360513','M60516','60516','658517853','Sunkist Fruit Cups Mango 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Walmart','8809729360851','18809729360858','M60851','60851','658517852','Sunkist Fruit cup Mandarin Orange 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Kroger','8809729360523','18809729360520','P60520','60520','0285957','Sunkist Fruit Cups Pineapple 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Kroger','8809729360516','18809729360513','M60513','60513','0282079','Sunkist Fruit Cups Mango 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Kroger','8809729360851','18809729360858','M60858','60858','0285908','Sunkist Fruit cup Mandarin Orange 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Target','8809729360523','18809729360520','P60525','60525',null,'Sunkist Fruit Cups Pineapple 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Target','8809729360516','18809729360513','M60512','60512',null,'Sunkist Fruit Cups Mango 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Target','8809729360851','18809729360858','M60853','60853',null,'Sunkist Fruit cup Mandarin Orange 4oz*4');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Walmart.com','8809729361223','18809729361220 (N/A)','P61223','61223','663454269','Sunkist Fruit Cups Pineapple 4oz?24counts');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Walmart.com','8809729361216','18809729361213 (N/A)','M61216','61216','663454272','Sunkist Fruit Cups Mango 4oz??24counts');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Walmart.com','8809729361230','18809729361237 (N/A)','M61230','61230','663454270','Sunkist Fruit Cups Mandarin Orange 4oz??24counts');
insert into edi.stocks (hub_group, upc, gtin, stk_no, warehouse_stock_no, retail_item_no, retail_item_name) values('Walmart.com','8809729361247','18809729361244 (N/A)','P61247','61247','','Sunkist Fruit Cups Peach 4oz??24counts');
--
UPDATE edi.stocks SET company_id = 1 WHERE hub_group  = 'Walmart';
UPDATE edi.stocks SET company_id = 3 WHERE hub_group  = 'Walmart.com';
UPDATE edi.stocks SET company_id = 3 WHERE hub_group  = 'Kroger';

-- company
DROP TABLE IF EXISTS edi.company CASCADE ;
CREATE TABLE IF NOT EXISTS edi.company
(
	id int not null,
	nm varchar(100) not null,
	abbr varchar(10) null,
	sort_order int null,
	use_yn char(1) NOT NULL DEFAULT 'Y',
	CONSTRAINT pk_company primary key(id)
);
insert into edi.company(id, nm, abbr, sort_order) values(1, 'WalMart','WM',1);
insert into edi.company(id, nm, abbr, sort_order) values(2, 'WalMart.COM','WM.COM',2);
insert into edi.company(id, nm, abbr, sort_order) values(3, 'Kroger','KG',3);

-- ship_to_addr
DROP TABLE IF EXISTS edi.ship_to_addr CASCADE ;
CREATE TABLE IF NOT EXISTS edi.ship_to_addr 
(
	company_id int not null,
	st_id varchar(10) not null,
	dc_nm varchar(100) not null,
	ucc_ean_loc_cd varchar(20) not null,
	cd_qualifer varchar(10) null,
	des_addr_info varchar(100) null,
	des_city varchar(50) null,
	des_state varchar(10) null,
	des_zip varchar(10) null,
	country_cd varchar(20) null,
	CONSTRAINT pk_ship_to_addr primary key(company_id, st_id)
);

insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-1','WAL-MART DC 6006A-ASM DIS','0078742028286','2200B 7TH AVENUE SOUTHWEST','CULLMAN','AL','35055');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-2','WAL-MART DC 6009A-ASM DIS','0078742028552','1501 MAPLE LEAF ROAD','MOUNT PLEASANT','IA','52641');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-3','WAL-MART DC 6010A-ASM DIS','0078742028644','690 HIGHWAY 206','DOUGLAS','GA','31533');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-4','WAL-MART DC 6011A-ASM DIS','0078742028736','2200 MANUFACTURERS BOULEVARD','BOULEVARD','MS','39601');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-5','WAL-MART DC 6012A-ASM DIS','0078742028828','3100 NORTH I-27','PLAINVIEW','TX','79072');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-6','WAL-MART DC 6016A-ASM DIS','0078742029184','3920 IH 35 NORTH','NEW BRAUNFELS','TX','78130');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-7','WAL-MART DC 6017A-ASM DIS','0078742029276','2108 EAST TIPTON STREET','SEYMOUR','IN','47274');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-8','WAL-MART DC 6018A-ASM DIS','0078742029368','2103 SOUTH MAIN','SEARCY','AR','72143');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-9','WAL-MART DC 6019A-ASM DIS','0078742029450','7504 EAST CROSSROADS BOULEVARD','LOVELAND','CO','80538');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-10','WAL-MART DC 6020A-ASM DIS','0078742029542','4224 KETTERING ROAD','BROOKSVILLE','FL','34602');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-11','WAL-MART DC 6021A-ASM DIS','0078742029634','1005 SOUTH H STREET','PORTERVILLE','CA','93257');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-12','WAL-MART DC 6023A-ASM DIS','0078742029818','21504 COX ROAD','SUTHERLAND','VA','23885');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-13','WAL-MART DC 6024A-ASM DIS','0078742029900','3920 SOUTHWEST BLVD.','GROVE CITY','OH','43123');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-14','WAL-MART DC 6025A-ASM DIS','0078742029993','6140 3M DRIVE','MENOMONIE','WI','54751');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-15','WAL-MART DC 6026A-ASM DIS','0078742030081','10817 HWY 99W','RED BLUFF','CA','96080');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-16','WAL-MART DC 6027A-ASM DIS','0078742030173','310 OWENS ROAD, ST RT 970','WOODLAND','PA','16881');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-17','WAL-MART DC 6030A-ASM DIS','0078742030449','42-D FREETOWN ROAD','RAYMOND','NH','3077');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-18','WAL-MART DC 6031A-ASM DIS','0078742030531','23701 WEST SOUTHERN AVE','BUCKEYE','AZ','85326');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-19','WAL-MART DC 6035A-ASM DIS','0078742030807','3220 NEVADA TERRACE','OTTAWA','KS','66067');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-20','WAL-MART DC 6036A-ASM DIS','0078742030890','8660 SOUTH US HWY 79','PALESTINE','TX','75803');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-21','WAL-MART WAREHOUSE 6037','0078742030982','2650 HWY 395 SOUTH','HERMISTON','OR','97838');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-22','WAL-MART DC 6038A-ASM DIS','0078742031071','8827D OLD RIVER RD','MARCY','NY','13403');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-23','WAL-MART DC 6039A-ASM DIS','0078742031163','1659 POTTERTOWN RD','MIDWAY','TN','37809');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-24','WAL-MART DC 6040A-ASM DIS','0078742031255','1010 WAL-MART DRIVE','HOPE MILLS','NC','28348');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-25','WAL-MART DC 6043A-ASM DIS','0078742031521','492 JONESVILLE ROAD','COLDWATER','MI','49036');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-26','WAL-MART DC 6048A-ASM DIS','0078742031972','3022 HWY 743','OPELOUSAS','LA','70570');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-27','WAL-MART DC 6054A-ASM DIS','0078742032511','385 CALLAWAY CHURCH ROAD','LA GRANGE','GA','30241');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-28','WAL-MART DC 6066A-ASM DIS','0078742041445','694 CRENSHAW BLVD','HOPKINSVILLE','KY','42240');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-29','WAL-MART DC 6068A-ASM DIS','0078742033907','221 LOIS RD','SANGER','TX','76266');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-30','WAL-MART DC 6069A-ASM DIS','0078742033938','1200 MATLOCK DRIVE','ST. JAMES','MO','65559');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-31','WAL-MART DC 6070A-ASM DIS','0078742040028','220 WAL-MART DRIVE','SHELBY','NC','28150');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-32','WAL-MART DC 6080A-ASM DIS','0078742038674','300 VETERANS DRIVE','TOBYHANNA','PA','18466');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-33','WAL-MART DC 6092A-ASM DIS','0078742035833','3110 ILLINOIS HWY 89','SPRING VALLEY','IL','61362');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-34','WAL-MART DC 6094A-ASM DIS','0078742035307','5801 SW REGIONAL AIRPORT BLVD.','BENTONVILLE','AR','72712');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-35','WAL-MART DC 7026A-ASM DIS','0078742050775','945 NORTH STATE ROAD 138','GRANTSVILLE','UT','84029');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-36','WAL-MART DC 7033A-ASM DIS','0078742045504','21215 JOHNSON ROAD','APPLE VALLEY','CA','92307');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-37','WAL-MART DC 7034A-ASM DIS','0078742045627','4860 WHEATLEYS POND ROAD','SMYRNA','DE','19977');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-38','WAL-MART DC 7035A-ASM DIS','0078742058436','18245 NW 115 AVENUE','ALACHUA','FL','32615');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-39','WAL-MART DC 7036A-ASM DIS','0078742050867','2226 FM 3013 SUITE 100','SEALY','TX','77474');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-40','WAL-MART DC 7038A-ASM DIS','0078742048079','4009 SOUTH JENKINS RD','FT PIERCE','FL','34981');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-41','WAL-MART DC 7039A-ASM DIS','0078742057903','111 DISTRIBUTION WAY','BEAVER DAM','WI','53916');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(1,'ST-42','WAL-MART DC 7045A-ASM DIS','0078742053790','6004 WALTON WAY','MT. CRAWFORD','VA','22841');

insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(3,'ST-1','PEYTON''S SOUTHEAST','1191030348002','160 GOLDSTAR DRIVE, WHSE 087','CLEVELAND','TN','37311');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(3,'ST-2','PEYTON''S NORTH','1191030348003','1111 SOUTH ADAMS STREET, WHSE 181','BLUFFTON','IN','46714');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd,	des_addr_info,des_city,des_state,des_zip)values(3,'ST-3','PEYTON''S FOUNTAIN','1191030348006','11025 CHARTER OAK RANCH ROAD','FOUNTAIN','CO','80817');

insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd, cd_qualifer,	des_addr_info,des_city,des_state,des_zip)values(2,'ST-1','PHX1 GLENDALE AZ FC-VS3105','0078742109923','UL','6600 N SARIVAL AVE','GLENDALE','AZ','85340');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd, cd_qualifer,	des_addr_info,des_city,des_state,des_zip)values(2,'ST-2','CLT1N TROUTMAN NC FC - VS3856','0078742110196','UL','386 MURDOCK RD','TROUTMAN','NC','28166');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd, cd_qualifer,	des_addr_info,des_city,des_state,des_zip)values(2,'ST-3','DFW6N FORT WORTH TX FC - VS3863','0078742110301','UL','14700 BLUE MOUND RD','FORT WORTH','TX','76052');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd, cd_qualifer,	des_addr_info,des_city,des_state,des_zip)values(2,'ST-4','WALMART DC 7356','0078742077864','UL','3215 COMMERCE CENTER BLVD','BETHLEHEM','PA','18015');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd, cd_qualifer,	des_addr_info,des_city,des_state,des_zip)values(2,'ST-5','REGIONAL DISTRIBUTION CENTER 7441','0078742111681','UL','3501 BRANDON RD','JOLIE','IL','604361249');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd, cd_qualifer,	des_addr_info,des_city,des_state,des_zip)values(2,'ST-6','REGIONAL DISTRIBUTION CENTER 7853','0078742085470','UL','5100 N RIDGE TRL','DAVENPORT','FL','33897');
insert into edi.ship_to_addr(company_id,st_id, dc_nm,ucc_ean_loc_cd, cd_qualifer,	des_addr_info,des_city,des_state,des_zip)values(2,'ST-7','CHINO COMBO WHSE 8103','0078742080604','UL','6750 KIMBALL AVENUE','CHINO','CA','91708');



-- ship_to_addr
DROP TABLE IF EXISTS edi.week_of_year CASCADE ;
CREATE TABLE IF NOT EXISTS edi.week_of_year 
(
	company_id int not null,
	start_day varchar(8) not null,
	woy int not null,
	CONSTRAINT pk_week_of_year primary key(company_id, start_day)
);

insert into edi.week_of_year values(3, '20230101',49);
insert into edi.week_of_year values(3, '20230108',50);
insert into edi.week_of_year values(3, '20230115',51);
insert into edi.week_of_year values(3, '20230122',52);
insert into edi.week_of_year values(3, '20230129',1);
insert into edi.week_of_year values(3, '20230205',2);
insert into edi.week_of_year values(3, '20230212',3);
insert into edi.week_of_year values(3, '20230219',4);
insert into edi.week_of_year values(3, '20230226',5);
insert into edi.week_of_year values(3, '20230305',6);
insert into edi.week_of_year values(3, '20230312',7);
insert into edi.week_of_year values(3, '20230319',8);
insert into edi.week_of_year values(3, '20230326',9);
insert into edi.week_of_year values(3, '20230402',10);
insert into edi.week_of_year values(3, '20230409',11);
insert into edi.week_of_year values(3, '20230416',12);
insert into edi.week_of_year values(3, '20230423',13);
insert into edi.week_of_year values(3, '20230430',14);
insert into edi.week_of_year values(3, '20230507',15);
insert into edi.week_of_year values(3, '20230514',16);
insert into edi.week_of_year values(3, '20230521',17);
insert into edi.week_of_year values(3, '20230528',18);
insert into edi.week_of_year values(3, '20230604',19);
insert into edi.week_of_year values(3, '20230611',20);
insert into edi.week_of_year values(3, '20230618',21);
insert into edi.week_of_year values(3, '20230625',22);
insert into edi.week_of_year values(3, '20230702',23);
insert into edi.week_of_year values(3, '20230709',24);
insert into edi.week_of_year values(3, '20230716',25);
insert into edi.week_of_year values(3, '20230723',26);
insert into edi.week_of_year values(3, '20230730',27);
insert into edi.week_of_year values(3, '20230806',28);
insert into edi.week_of_year values(3, '20230813',29);
insert into edi.week_of_year values(3, '20230820',30);
insert into edi.week_of_year values(3, '20230827',31);
insert into edi.week_of_year values(3, '20230903',32);
insert into edi.week_of_year values(3, '20230910',33);
insert into edi.week_of_year values(3, '20230917',34);
insert into edi.week_of_year values(3, '20230924',35);
insert into edi.week_of_year values(3, '20231001',36);
insert into edi.week_of_year values(3, '20231008',37);
insert into edi.week_of_year values(3, '20231015',38);
insert into edi.week_of_year values(3, '20231022',39);
insert into edi.week_of_year values(3, '20231029',40);
insert into edi.week_of_year values(3, '20231105',41);
insert into edi.week_of_year values(3, '20231112',42);
insert into edi.week_of_year values(3, '20231119',43);
insert into edi.week_of_year values(3, '20231126',44);
insert into edi.week_of_year values(3, '20231203',45);
insert into edi.week_of_year values(3, '20231210',46);
insert into edi.week_of_year values(3, '20231217',47);
insert into edi.week_of_year values(3, '20231224',48);
insert into edi.week_of_year values(3, '20231231',49);
insert into edi.week_of_year values(3, '20240107',50);
insert into edi.week_of_year values(3, '20240114',51);
insert into edi.week_of_year values(3, '20240121',52);
insert into edi.week_of_year values(3, '20240128',53);

