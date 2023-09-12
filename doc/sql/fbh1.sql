SELECT
    column_name,
    data_type,
    character_maximum_length AS max_length,
    character_octet_length AS octet_length,
    is_nullable,
    column_default
FROM
    information_schema.COLUMNS  
WHERE
    table_schema = 'edi' AND 
    table_name = 'freight_210_dtl';
    
   
   SELECT
    column_name,
    data_type,
    character_maximum_length AS max_length,
    character_octet_length AS octet_length,
    is_nullable,
    column_default
FROM
    information_schema.COLUMNS  
WHERE
    table_schema = 'edi' AND 
    table_name = 'shipping_order_940_dtl';
    
   SELECT
    column_name,
    data_type,
    character_maximum_length AS max_length,
    character_octet_length AS octet_length,
    is_nullable,
    column_default
FROM
    information_schema.COLUMNS  
WHERE
    table_schema = 'edi' AND 
    table_name = 'shipping_945_dtl';
    
   SELECT
    column_name,
    data_type,
    character_maximum_length AS max_length,
    character_octet_length AS octet_length,
    is_nullable,
    column_default
FROM
    information_schema.COLUMNS  
WHERE
    table_schema = 'edi' AND 
    table_name = 'transfer_944_dtl';     
    
   SELECT
    column_name,
    data_type,
    character_maximum_length AS max_length,
    character_octet_length AS octet_length,
    is_nullable,
    column_default
FROM
    information_schema.COLUMNS  
WHERE
    table_schema = 'edi' AND 
    table_name = 'inquiry_846_dtl';     