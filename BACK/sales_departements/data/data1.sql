CREATE SEQUENCE "public".departement_seq START WITH 1;

CREATE SEQUENCE "public".employee_seq START WITH 1;

CREATE SEQUENCE "public".person_seq START WITH 1;

CREATE SEQUENCE "public".product_seq START WITH 1;

CREATE SEQUENCE "public".proforma_details_seq START WITH 1;

CREATE SEQUENCE "public".proforma_seq START WITH 1;

CREATE SEQUENCE "public".purchase_order_details_seq START WITH 1;

CREATE SEQUENCE "public".purchase_order_seq START WITH 1;

CREATE SEQUENCE "public".request_details_seq START WITH 1;

CREATE SEQUENCE "public".request_seq START WITH 1;

CREATE SEQUENCE "public".supplier_product_seq START WITH 1;

CREATE SEQUENCE "public".supplier_seq START WITH 1;

CREATE  TABLE "public".person (
	person_id            varchar DEFAULT custom_seq('PER'::character varying, 'person_seq'::character varying, 5) NOT NULL ,
	first_name           varchar(50)   ,
	last_name            varchar(50)   ,
	date_of_birth        date   ,
	gender               varchar(10)   ,
	phone_number         varchar(20)   ,
	address              varchar(255)   ,
	CONSTRAINT person_pkey PRIMARY KEY ( person_id )
 );

CREATE  TABLE "public".product (
	product_id           varchar DEFAULT custom_seq('PRO'::character varying, 'product_seq'::character varying, 5) NOT NULL ,
	product_name         varchar(100)   ,
	CONSTRAINT product_pkey PRIMARY KEY ( product_id ),
	CONSTRAINT product_product_name_key UNIQUE ( product_name )
 );

CREATE  TABLE "public".supplier (
	supplier_id          varchar DEFAULT custom_seq('SUP'::character varying, 'supplier_seq'::character varying, 5) NOT NULL ,
	name                 varchar(100)   ,
	contact_email        varchar(100)   ,
	contact_phone        varchar(20)   ,
	address              varchar(255)   ,
	CONSTRAINT supplier_pkey PRIMARY KEY ( supplier_id ),
	CONSTRAINT supplier_name_key UNIQUE ( name )
 );

CREATE  TABLE "public".supplier_product (
	supplier_product_id  varchar DEFAULT custom_seq('SPR'::character varying, 'supplier_product_seq'::character varying, 5) NOT NULL ,
	supplier_id          varchar   ,
	product_id           varchar   ,
	CONSTRAINT supplier_product_pkey PRIMARY KEY ( supplier_product_id ),
	CONSTRAINT supplier_product_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT supplier_product_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )
 );

CREATE  TABLE "public".department (
	department_id        varchar DEFAULT custom_seq('DEP'::character varying, 'departement_seq'::character varying, 5) NOT NULL ,
	department_name      varchar(100)   ,
	department_head_id   varchar   ,
	CONSTRAINT department_pkey PRIMARY KEY ( department_id ),
	CONSTRAINT department_department_name_key UNIQUE ( department_name ) ,
	CONSTRAINT department_department_head_id_fkey FOREIGN KEY ( department_head_id ) REFERENCES "public".person( person_id )
 );

CREATE  TABLE "public".employee (
	employee_id          varchar DEFAULT custom_seq('EMP'::character varying, 'employee_seq'::character varying, 5) NOT NULL ,
	person_id            varchar   ,
	department_id        varchar   ,
	hire_date            date   ,
	job_title            varchar(100)   ,
	salary               numeric(10,2)   ,
	email                varchar(100)   ,
	"password"           varchar(20)   ,
	daf                  boolean DEFAULT false  ,
	CONSTRAINT employee_pkey PRIMARY KEY ( employee_id ),
	CONSTRAINT employee_person_id_key UNIQUE ( person_id ) ,
	CONSTRAINT employee_department_id_fkey FOREIGN KEY ( department_id ) REFERENCES "public".department( department_id )   ,
	CONSTRAINT employee_person_id_fkey FOREIGN KEY ( person_id ) REFERENCES "public".person( person_id )
 );

CREATE  TABLE "public".proforma (
	proforma_id          varchar DEFAULT custom_seq('PRO'::character varying, 'proforma_seq'::character varying, 5) NOT NULL ,
	issue_date           date   ,
	due_date             date   ,
	supplier_id          varchar   ,
	CONSTRAINT proforma_pkey PRIMARY KEY ( proforma_id ),
	CONSTRAINT proforma_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )
 );

CREATE  TABLE "public".proforma_details (
	proforma_details_id  varchar DEFAULT custom_seq('PRD'::character varying, 'proforma_details_seq'::character varying, 5) NOT NULL ,
	proforma_id          varchar   ,
	product_id           varchar   ,
	quantity             integer   ,
	price                numeric(10,2)   ,
	CONSTRAINT proforma_details_pkey PRIMARY KEY ( proforma_details_id ),
	CONSTRAINT proforma_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT proforma_details_proforma_id_fkey FOREIGN KEY ( proforma_id ) REFERENCES "public".proforma( proforma_id )
 );

CREATE  TABLE "public".purchase_order (
	purchase_order_id    varchar DEFAULT custom_seq('PUR'::character varying, 'purchase_order_seq'::character varying, 5) NOT NULL ,
	created_at           timestamp(0) DEFAULT CURRENT_TIMESTAMP  ,
	delivery_days        integer   ,
	supplier_id          varchar   ,
	validation           integer DEFAULT 10  ,
	CONSTRAINT purchase_order_pkey PRIMARY KEY ( purchase_order_id ),
	CONSTRAINT purchase_order_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )
 );

CREATE  TABLE "public".purchase_order_details (
	purchase_order_details_id varchar DEFAULT custom_seq('PUD'::character varying, 'purchase_order_details_seq'::character varying, 5) NOT NULL ,
	purchase_order_id    varchar   ,
	product_id           varchar   ,
	quantity             float8   ,
	price                numeric(10,2)   ,
	CONSTRAINT purchase_order_details_pkey PRIMARY KEY ( purchase_order_details_id ),
	CONSTRAINT purchase_order_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT purchase_order_details_purchase_order_id_fkey FOREIGN KEY ( purchase_order_id ) REFERENCES "public".purchase_order( purchase_order_id )
 );

CREATE  TABLE "public".request (
	request_id           varchar DEFAULT custom_seq('REQ'::character varying, 'request_seq'::character varying, 5) NOT NULL ,
	department_id        varchar   ,
	created_at           timestamp(0) DEFAULT CURRENT_TIMESTAMP  ,
	is_validated         boolean DEFAULT false  ,
	CONSTRAINT request_pkey PRIMARY KEY ( request_id ),
	CONSTRAINT request_department_id_fkey FOREIGN KEY ( department_id ) REFERENCES "public".department( department_id )
 );

CREATE  TABLE "public".request_details (
	request_details_id   varchar DEFAULT custom_seq('RED'::character varying, 'request_details_seq'::character varying, 5) NOT NULL ,
	request_id           varchar   ,
	product_id           varchar   ,
	quantity             integer   ,
	reason               varchar(200)   ,
	CONSTRAINT request_details_pkey PRIMARY KEY ( request_details_id ),
	CONSTRAINT request_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT request_details_request_id_fkey FOREIGN KEY ( request_id ) REFERENCES "public".request( request_id )
 );

CREATE OR REPLACE FUNCTION public.custom_seq(in_prefix character varying, in_sequence_name character varying, in_digit_count integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
    seq_value INT;
    result VARCHAR;
BEGIN
    EXECUTE 'SELECT nextval(''' || in_sequence_name || '''::regclass)' INTO seq_value;
    result := in_prefix || LPAD(seq_value::TEXT, in_digit_count, '0');
    RETURN result;
END;
$function$
;

INSERT INTO "public".person( person_id, first_name, last_name, date_of_birth, gender, phone_number, address ) VALUES ( 'PER00001', 'RATIATIANA', 'Jean Mirlin', '2023-10-20', 'M', '0348262182', 'Ambohijanaka');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00001', 'ordinateur');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00002', 'projecteur');
INSERT INTO "public".department( department_id, department_name, department_head_id ) VALUES ( 'DEP00001', 'Informatique', 'PER00001');
INSERT INTO "public".employee( employee_id, person_id, department_id, hire_date, job_title, salary, email, password, daf ) VALUES ( 'EMP00001', 'PER00001', 'DEP00001', '2022-12-12', 'DEV', 2000000, 'jeanmirlin.r@gmail.com', 'mirlin', false);
INSERT INTO "public".request( request_id, department_id, created_at, is_validated ) VALUES ( 'REQ00001', 'DEP00001', '2020-11-11 12.00.00 am', false);
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason ) VALUES ( 'RED00001', 'REQ00001', 'PRO00001', 5, 'informatisation');

insert into supplier values
(default, 'Jumbo Score', 'jumbo@gmail.com', '+261 34 11 111 11', 'Tanjombato'),
(default, 'Shoprite', 'shoprite@gmail.com', '+261 33 25 255 25', 'Analakely'),
(default, 'Leader Price', 'leader@gmail.com', '+261 32 22 222 55', 'Ankadimbahoaka');

insert into proforma values
(default, '20-11-2023', '20-12-2023', 'SUP00001'),
(default, '20-11-2023', '20-12-2023', 'SUP00002'),
(default, '20-11-2023', '20-12-2023', 'SUP00003');

insert into proforma_details values
(default, 'PRO00001', 'PRO00001', 20, 500),
(default, 'PRO00001', 'PRO00002', 35, 300),
(default, 'PRO00002', 'PRO00001', 20, 550),
(default, 'PRO00002', 'PRO00002', 35, 250),
(default, 'PRO00003', 'PRO00001', 20, 600),
(default, 'PRO00003', 'PRO00002', 35, 350);

insert into supplier_product values
(default, 'SUP00001', 'PRO00001'),
(default, 'SUP00001', 'PRO00002'),
(default, 'SUP00002', 'PRO00001'),
(default, 'SUP00002', 'PRO00002'),
(default, 'SUP00003', 'PRO00001'),
(default, 'SUP00003', 'PRO00002');

insert into purchase_order(purchase_order_id, delivery_days, supplier_id) values 
(default, 30, 'SUP00001'),
(default, 30, 'SUP00002');

insert into purchase_order_details values 
(default, 'PUR00001', 'PRO00001', 20, 500),
(default, 'PUR00002', 'PRO00002', 35, 250);


-- Bon de commande
select su.name, su.contact_email, su.contact_phone, su.address, p.product_name, pod.quantity, pod.price, po.created_at, po.delivery_days
from supplier su
join purchase_order po on po.supplier_id = su.supplier_id
join purchase_order_details pod on pod.purchase_order_id = po.purchase_order_id
join product p on p.product_id = pod.product_id


-- moin disant
SELECT proforma_id, product_id, quantity, price
FROM ( SELECT
        proforma_id,
        product_id,
        quantity,
        price,
        ROW_NUMBER() OVER (PARTITION BY product_id ORDER BY price) AS rank
    FROM
        proforma_details
) ranked_products
WHERE
    rank = 1;


-- select detail bon de commande

SELECT pd.product_id, quantity, price
FROM proforma_details pd
JOIN proforma p ON p.proforma_id = pd.proforma_id
JOIN supplier s ON s.supplier_id = p.supplier_id
JOIN purchase_order po ON po.supplier_id = s.supplier_id
JOIN product pr on pr.product_id = pd.product_id
WHERE po.purchase_order_id IN ('PUR00008', 'PUR00009') and pd.proforma_details_id IN ('PRD00001', 'PRD00004');