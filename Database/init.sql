CREATE SCHEMA IF NOT EXISTS model;
CREATE SCHEMA IF NOT EXISTS "mapping";

CREATE ROLE model_read;
GRANT USAGE ON SCHEMA model TO model_read;
CREATE ROLE model_crud WITH IN ROLE model_read;

CREATE ROLE mapping_read;
GRANT USAGE ON SCHEMA mapping TO mapping_read;
CREATE ROLE mapping_crud WITH IN ROLE mapping_read;

CREATE USER mapping_system WITH IN ROLE mapping_crud, model_read;

CREATE TABLE IF NOT EXISTS model."Models"
(
    "ID" uuid NOT NULL,
    "Name" character varying(30) NOT NULL,
    "Source" character varying(30),
    CONSTRAINT "MODEL_PK" PRIMARY KEY ("ID"),
    CONSTRAINT "MODEL_NAME_UNIQUE" UNIQUE ("Name")
);

CREATE TABLE IF NOT EXISTS model."Elements"
(
  "ID" uuid NOT NULL,
  "Name" character varying(100) NOT NULL,
  "Model" uuid NOT NULL,
  CONSTRAINT "ELEMENT_PK" PRIMARY KEY ("ID"),
  CONSTRAINT "ELEMENT_MODEL_FK" FOREIGN KEY ("Model") REFERENCES model."Models" ("ID")
);

CREATE TABLE IF NOT EXISTS model."Attributes"
(
  "ID" uuid NOT NULL,
  "Name" character varying(100) NOT NULL,
  "Element" uuid NOT NULL,
  CONSTRAINT "ATTRIBUTE_PK" PRIMARY KEY ("ID"),
  CONSTRAINT "ATTRIBUTE_ELEMENT_FK" FOREIGN KEY ("Element") REFERENCES model."Elements" ("ID")
);

CREATE TABLE IF NOT EXISTS "mapping"."Mappings"
(
    "ID" uuid NOT NULL,
    "Model_From" uuid NOT NULL,
    "Model_To" uuid NOT NULL,
    CONSTRAINT "MAPPING_PK" PRIMARY KEY ("ID"),
    CONSTRAINT "MAPPING_MODEL_FROM_FK" FOREIGN KEY ("Model_From") REFERENCES model."Models" ("ID"),
    CONSTRAINT "MAPPING_MODEL_TO_FK" FOREIGN KEY ("Model_To") REFERENCES model."Models" ("ID")
);

CREATE TABLE IF NOT EXISTS "mapping"."Mapping_rules"
(
  "ID" uuid NOT NULL,
  "Name" character varying(100) NOT NULL,
  "Mapping" uuid NOT NULL,
  CONSTRAINT "MAPPING_RULE_PK" PRIMARY KEY ("ID"),
  CONSTRAINT "MAPPING_RULE_MAPPING_FK" FOREIGN KEY ("Mapping") REFERENCES mapping."Mappings" ("ID")
);

CREATE TABLE IF NOT EXISTS "mapping"."Coupled_elements"
(
  "Element" uuid NOT NULL,
  "Mapping_rule" uuid NOT NULL,
  CONSTRAINT "COUPLED_ELEMENT_PK" PRIMARY KEY ("Element", "Mapping_rule"),
  CONSTRAINT "COUPLED_ELEMENT_ELEMENT_FK" FOREIGN KEY ("Element") REFERENCES model."Elements" ("ID"),
  CONSTRAINT "COUPLED_ELEMENT_MAPPING_RULE_FK" FOREIGN KEY ("Mapping_rule") REFERENCES mapping."Mapping_rules" ("ID")
);

CREATE TABLE IF NOT EXISTS "mapping"."Mapping_relations"
(
  "ID" uuid NOT NULL,
  "Mapping_rule" uuid NOT NULL,
  "Attribute_left" uuid NOT NULL,
  "Attribute_right" uuid NOT NULL,
  CONSTRAINT "MAPPING_RELATION_PK" PRIMARY KEY ("ID"),
  CONSTRAINT "MAPPING_RELATION_MAPPING_RULE_FK" FOREIGN KEY ("Mapping_rule") REFERENCES mapping."Mapping_rules" ("ID"),
  CONSTRAINT "MAPPING_RELATION_ATTRIBUTE_LEFT_FK" FOREIGN KEY ("Attribute_left") REFERENCES model."Attributes" ("ID"),
  CONSTRAINT "MAPPING_RELATION_ATTRIBUTE_RIGHT_FK" FOREIGN KEY ("Attribute_right") REFERENCES model."Attributes" ("ID"),
  CONSTRAINT "MAPPING_RELATION_ONLY_RELATION" UNIQUE ("Attribute_left", "Attribute_right")
  -- bit of a tricky question, since it might be possible to have to relations from the same attribute, going to two (or more) different attributes.
  -- however it is easier to (later) on lift a rule, then to add a rule
);

GRANT SELECT ON ALL TABLES IN SCHEMA model TO model_read;
GRANT INSERT, UPDATE, DELETE on ALL TABLES IN SCHEMA model TO model_crud;
GRANT SELECT ON ALL TABLES IN SCHEMA "mapping" TO mapping_read;
GRANT INSERT, UPDATE, DELETE on ALL TABLES IN SCHEMA "mapping" TO mapping_crud;
