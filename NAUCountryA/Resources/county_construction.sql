CREATE TABLE IF NOT EXISTS public."County"
(
    "COUNTY_CODE" integer NOT NULL,
    "STATE_CODE" integer NOT NULL,
    "COUNTY_NAME" character varying(50) NOT NULL,
    "RECORD_TYPE_CODE" character varying(10) NOT NULL,
    CONSTRAINT "Pk_County" PRIMARY KEY ("COUNTY_CODE"),
    CONSTRAINT "Fk_StateType" FOREIGN KEY ("STATE_CODE")
        REFERENCES public."State" ("STATE_CODE") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT "Fk_RecordType" FOREIGN KEY ("RECORD_TYPE_CODE")
        REFERENCES public."RecordType" ("RECORD_TYPE_CODE") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."County"
    OWNER to postgres;