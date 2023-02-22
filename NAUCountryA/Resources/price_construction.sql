CREATE TABLE IF NOT EXISTS public "Price"
(
	"OFFER_ID" character varying (10) NOT NULL,
	"EXPECTED_INDEX_VALUE" integer NOT NULL,
	CONSTRAINT "pk_Price" PRIMARY KEY ("OFFER_ID"),
	CONSTRAINT "fk_Offer" FOREIGN KEY ("OFFER_ID")
)
TABLESPACE pg_default
ALTER TABLE IF EXISTS public "Price"
	OWNER to postgres;