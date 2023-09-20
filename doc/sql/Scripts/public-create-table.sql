-- member
DROP TABLE IF EXISTS public.edi_user CASCADE ;
CREATE TABLE IF NOT EXISTS public.edi_user
(
	id varchar(30) NOT NULL,
	pw varchar(100) NOT NULL,
	nm varchar(50) NOT NULL,
	email varchar(100) NOT NULL,
	--
	created_by varchar(30) not null,
	created_on timestamp not null default CURRENT_TIMESTAMP,
	last_update_by varchar(30) null,
	last_update_on timestamp null,
	last_login_on timestamp NULL,
	--
	CONSTRAINT pk_edi_user PRIMARY key( id )
);
INSERT INTO public.edi_user(id, pw, nm, email, created_by) values('kdy987','1111','Kim Do Young','kdy987@gmail.com','system');