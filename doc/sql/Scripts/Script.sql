-- --------------------------- delete all
delete from edi.freight_210 ;

delete from edi.invoice_810 ;
delete from edi.invoice_810_dtl ;

delete from edi.inquiry_846 ;
delete from edi.inquiry_846_dtl;

delete from edi.po_850 ;
delete FROM edi.po_850_dtl;
delete FROM edi.po_850_allowance;

delete from edi.shipping_order_940;
delete from edi.shipping_order_940_dtl ;

delete from edi.transfer_944 ;
delete from edi.transfer_944_dtl ;

delete from edi.shipping_945 ;
delete from edi.shipping_945_dtl;
--
-- select
-- 
select * from edi.freight_210 f ;

SELECT * FROM edi.invoice_810 i ;
SELECT * FROM edi.invoice_810_dtl id ;

select * from edi.inquiry_846 i ;
SELECT * FROM edi.inquiry_846_dtl id ;

select * from edi.shipping_order_940 so ;
select * from edi.shipping_order_940_dtl sod ;

select * from edi.transfer_944 t ; 
select * from edi.transfer_944_dtl td ;


select * from edi.shipping_945 s ;
select * from edi.shipping_945_dtl sd ;


SELECT company_id FROM edi.stocks s WHERE retail_item_no = '0282079';
select * from edi.stocks s  where hub_group  = 'Kroger';
update edi.stocks set retail_item_no  = concat('0',retail_item_no) where hub_group  = 'Kroger';