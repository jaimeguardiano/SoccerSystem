--------------------------------------------------------
--  File created - Tuesday-August-15-2023   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table TEAMS
--------------------------------------------------------

  CREATE TABLE "JAIME"."TEAMS" 
   (	"TEAMID" NUMBER, 
	"TEAMNAME" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
REM INSERTING into JAIME.TEAMS
SET DEFINE OFF;
Insert into JAIME.TEAMS (TEAMID,TEAMNAME) values (1,'BULACAN SOCCER CLUB');
Insert into JAIME.TEAMS (TEAMID,TEAMNAME) values (21,'PAMPANGA SOCCER CLUB');
--------------------------------------------------------
--  DDL for Index TEAMS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "JAIME"."TEAMS_PK" ON "JAIME"."TEAMS" ("TEAMID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Trigger TEAMS_TRG
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "JAIME"."TEAMS_TRG" 
BEFORE INSERT ON TEAMS 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.TEAMID IS NULL THEN
      SELECT TEAMS_SEQ.NEXTVAL INTO :NEW.TEAMID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "JAIME"."TEAMS_TRG" ENABLE;
--------------------------------------------------------
--  Constraints for Table TEAMS
--------------------------------------------------------

  ALTER TABLE "JAIME"."TEAMS" MODIFY ("TEAMID" NOT NULL ENABLE);
  ALTER TABLE "JAIME"."TEAMS" MODIFY ("TEAMNAME" NOT NULL ENABLE);
  ALTER TABLE "JAIME"."TEAMS" ADD CONSTRAINT "TEAMS_PK" PRIMARY KEY ("TEAMID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
