CREATE TABLE IF NOT EXISTS public."Type"
(
	"TYPE_CODE" integer NOT NULL,
	"TYPE_NAME" character varying(50) NOT NULL,
	"TYPE_ABBREVIATION" character varying(10) Not Null,
	"COMMODITY_CODE" integer NOT NULL,
	"RELEASED_DATE" character varying(10) NOT NULL,
	"RECORD_TYPE_CODE" character varying(10) NOT NULL,
	CONSTRAINT "pk_type" PRIMARY KEY ("TYPE_CODE"),
	CONSTRAINT "fk_Commodity" FOREIGN KEY ("COMMODITY_CODE")
		REFERENCES public."Commodity" ("COMMODITY_CODE") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
	CONSTRAINT "fk_RecordType" FOREIGN KEY ("RECORD_TYPE_CODE")
	     REFERENCES public."RecordType" ("RECORD_TYPE_CODE") MATCH SIMPLE
            ON UPDATE NO ACTION
            ON DELETE NO ACTION
            NOT VALID

	
)
TABLESPACE pg_default;
ALTER TABLE IF EXISTS public."Type"
	OWNER to postgres;