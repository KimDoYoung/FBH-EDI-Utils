-- member
DROP TABLE IF EXISTS public.edi_user CASCADE ;
CREATE TABLE IF NOT EXISTS public.edi_user
(
	id varchar(30) NOT NULL,
	pw varchar(100) NOT NULL,
	nm varchar(10) NOT NULL,
	--
	created_by varchar(30) not null,
	created_on timestamp not null default CURRENT_TIMESTAMP,
	last_update_by varchar(30) null,
	last_update_on timestamp null,
	last_login_on timestamp NULL,
	--
	CONSTRAINT pk_edi_user PRIMARY key( id )
);