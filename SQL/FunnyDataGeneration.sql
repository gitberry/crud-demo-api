-- I would have loved to spend more time making this elegant - just a quick and dirty way to randomly select shtuff..
-- USE YOURDATABASEHERE
GO
BEGIN TRANSACTION;
WITH 
 Base AS (SELECT *                       FROM FunnySongTitleComposer )
,B1   AS (SELECT Usr=j1.UserName, N.*    FROM (SELECT  R1 = 1 + ABS(CHECKSUM(NewId())) % 19 , * FROM Base ) N LEFT JOIN FunnyUserName j1 on j1.id = n.r1)
,B2   AS (SELECT e.*, CreatedBy = b.Usr  FROM Base e  LEFT JOIN b1 b on b.id = e.id)
,B3   AS (SELECT Usr=j1.UserName, N.*    FROM (SELECT  R1 = 1 + ABS(CHECKSUM(NewId())) % 19 , * FROM Base ) N LEFT JOIN FunnyUserName j1 on j1.id = n.r1)
,B4   AS (SELECT e.*, ModifiedBy = b.Usr FROM b2 e  LEFT JOIN b3 b on b.id = e.id)
,B5   AS (SELECT Pub=j1.CorpName, N.*    FROM (SELECT  R1 = 1 + ABS(CHECKSUM(NewId())) % 74 , * FROM b4   ) N LEFT JOIN FunnyCompany  j1 on j1.id = n.r1)
,B6   AS (SELECT e.*, Publisher = b.Pub  FROM b4 e  LEFT JOIN b5 b on b.id = e.id)						  							  
,B7   AS (SELECT Sty=j1.StyleName, N.*   FROM (SELECT  R1 = 1 + ABS(CHECKSUM(NewId())) % 7  , * FROM b6   ) N LEFT JOIN FunnyStyle    j1 on j1.id = n.r1)
,B8   AS (SELECT e.*, Style = b.Sty      FROM b6 e  LEFT JOIN b7 b on b.id = e.id)
,B9   AS (SELECT *, CreatedOn  = Dateadd(second, -1 * ABS(CHECKSUM(NewId())) % 1343423333, getdate()) FROM B8) -- any time in the past 30 or so years
,B10  AS (SELECT *, ModifiedOn = Dateadd(second,      ABS(CHECKSUM(NewId())) % 1344353,    CreatedOn) FROM B9) -- had to be sometime AFTER the CreatedOn date..

INSERT INTO DataShort1(MetaFlag,MetaStruct,StringIdx100a,StringIdx50b, StringIdx50c,StringIdx50d
                       ,IsActive,MetaCreatedBy, MetaEditedBy,MetaCreateD, MetaEditeD )
SELECT 'SILS1', 'SillySongs', Title ,Composer,Publisher,Style
       ,1,CreatedBy,ModifiedBy,CreatedOn,ModifiedOn 
  FROM B10

-- JUST TO LOOK AT IT.. 
SELECT Max(MetaCreateD), Min(MetaCreateD) FROM DataShort1 
SELECT * FROM DataShort1 ORDER BY ID DESC


-- COMMIT --
ROLLBACK -- use commit once you have smoke tested...