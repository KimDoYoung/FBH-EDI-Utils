select count(*) as count from edi.freight_210;
SELECT * FROM edi.freight_210;
-- DELETE FROM edi.freight_210;

SELECT * FROM edi.invoice_810 a 
INNER JOIN edi.invoice_810_dtl b ON a.invoice_no = b.invoice_No;

SELECT * FROM edi.po_850 p ;
SELECT * FROM edi.po_850_dtl pd ;
SELECT * FROM edi.po_850_allowance pa ;

DELETE FROM edi.po_850 ;
DELETE FROM edi.po_850_dtl ;
DELETE FROM edi.po_850_allowance ;

SELECT * FROM edi.company c ;
SELECT * FROM edi.stocks;
SELECT * FROM edi.stocks;


select * from edi.shipping_945 s ;
select * from edi.shipping_945_dtl sd ;
delete from edi.shipping_945 ;
delete from edi.shipping_945_dtl ;
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
select * from edi.freight_210 f ;
select * from edi.shipping_945 s ;
select * from edi.shipping_945_dtl sd ;
select * from edi.transfer_944 t ; 
select * from edi.transfer_944_dtl td ;
select * from edi.shipping_order_940 so ;
select * from edi.shipping_order_940_dtl sod ;
