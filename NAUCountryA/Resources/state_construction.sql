CREATE TABLE IF NOT EXISTS public."State"
(
    "STATE_CODE" integer NOT NULL,
    "STATE_NAME" character varying(30) NOT NULL,
    "STATE_ABBREVIATION" character varying(2) NOT NULL,
    "RECORD_TYPE_CODE" character varying(10) NOT NULL,
    CONSTRAINT "Pk_State" PRIMARY KEY ("STATE_CODE"),
    CONSTRAINT "FK_RecordType" PRIMARY KEY ("RECORD_TYPE_CODE")
        REFERENCES public."RecordType" ("RECORD_TYPE_CODE") MATCH SIMPLE
            ON UPDATE NO ACTION
            ON DELETE NO ACTION
            NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."State"
    OWNER to postgres;