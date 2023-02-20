CREATE TABLE IF NOT EXISTS public."RecordType"
(
    "RECORD_TYPE_CODE" character varying(10) NOT NULL,
    "RECORD_CATEGORY_CODE" integer NOT NULL,
    "REINSURANCE_YEAR" integer NOT NULL,
    CONSTRAINT "PK_RecordType" PRIMARY KEY ("RECORD_TYPE_CODE")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."RecordType"
    OWNER to postgres;