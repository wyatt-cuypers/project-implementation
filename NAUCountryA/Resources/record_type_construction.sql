CREATE TABLE public."RecordType"
(
    "RecordTypeCode" character varying(10) NOT NULL,
    "RecordCategoryCode" integer NOT NULL,
    "ReinsuranceYear" integer NOT NULL,
    CONSTRAINT "PK_RecordType" PRIMARY KEY ("RecordTypeCode")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."RecordType"
    OWNER to postgres;