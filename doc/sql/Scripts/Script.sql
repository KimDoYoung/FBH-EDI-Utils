select count(*) as count from edi.freight_210;
SELECT * FROM edi.freight_210;
-- DELETE FROM edi.freight_210;

SELECT * FROM edi.invoice_810 a 
INNER JOIN edi.invoice_810_dtl b ON a.invoice_no = b.invoice_No;

SELECT * FROM edi.po_850 p ;
SELECT * FROM edi.po_850_dtl pd ;
SELECT * FROM edi.po_850_allowance pa ;

SELECT * FROM edi.