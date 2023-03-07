CREATE TABLE IF NOT EXISTS public."Practice"
(
    "PRACTICE_CODE" integer NOT NULL,
    "PRACTICE_NAME" character varying(50) NOT NULL,
    "PRACTICE_ABBREVIATION" character varying(25) NOT NULL,
    "COMMODITY_CODE" integer NOT NULL,
    "RELEASED_DATE" character varying(10) NOT NULL,
    "RECORD_TYPE_CODE" character varying(10) NOT NULL,
    CONSTRAINT "Pk_Practice" PRIMARY KEY ("PRACTICE_CODE"),
    CONSTRAINT "Fk_Commodity" FOREIGN KEY ("COMMODITY_CODE")
        REFERENCES public."Commodity" ("COMMODITY_CODE") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "Fk_RecordType" FOREIGN KEY ("RECORD_TYPE_CODE")
        REFERENCES public."RecordType" ("RECORD_TYPE_CODE") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Practice"
    OWNER to postgres;