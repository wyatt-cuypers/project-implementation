CREATE TABLE IF NOT EXISTS public."Commodity"
(
    "COMMODITY_CODE" integer NOT NULL,
    "COMMODITY_NAME" character varying(30) NOT NULL,
    "COMMODITY_ABBREVIATION" character varying(15) NOT NULL,
    "ANNUAL_PLANTING_CODE" character(1) NOT NULL,
    "COMMODITY_YEAR" integer NOT NULL,
    "RELEASED_DATE" character varying(10) NOT NULL,
    "RECORD_TYPE_CODE" character varying(10) NOT NULL,
    CONSTRAINT "Pk_Commodity" PRIMARY KEY ("COMMODITY_CODE"),
    CONSTRAINT "Fk_RecordType" FOREIGN KEY ("RECORD_TYPE_CODE")
        REFERENCES public."RecordType" ("RECORD_TYPE_CODE") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Commodity"
    OWNER to postgres;