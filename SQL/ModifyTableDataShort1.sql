Use MahCoadz
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* * * * * Creation of DB Table * * * * */
--/*
BEGIN TRANSACTION  -- for dev only
SELECT * INTO dbo.DataShort1bk FROM DataShort1
DROP TABLE IF EXISTS [dbo].[DataShort1]; -- for dev only
CREATE TABLE [dbo].[DataShort1](
	 [ID]             [int] IDENTITY(1,1) NOT NULL
	,[MetaFlag]       [char](5)           NOT NULL DEFAULT ''  -- for apps to differentiate themselves is sharing the same API
	,[MetaStruct]     [varchar](15)       NOT NULL DEFAULT ''  -- for "tables" to differentiate themselves within the same app
	,[StringIdx50A]   [varchar](50)           NULL 
	,[StringIdx50B]   [varchar](50)           NULL 
	,[StringIdx50C]   [varchar](50)           NULL 
	,[StringIdx50D]   [varchar](50)           NULL 
	,[StringIdx50E]   [varchar](50)           NULL 
	,[StringIdx100A]  [varchar](150)          NULL 
	,[String100A]     [varchar](150)          NULL 
	,[String100B]     [varchar](150)          NULL 
	,[String100C]     [varchar](150)          NULL 
	,[String100D]     [varchar](150)          NULL 
	,[String100E]     [varchar](150)          NULL 
	,[String25A]      [varchar](25)           NULL 
	,[String25B]      [varchar](25)           NULL 
	,[String25C]      [varchar](25)           NULL 
	,[String25D]      [varchar](25)           NULL 
	,[String25E]      [varchar](25)           NULL 
	,[DtIdxA]         [datetimE]              NULL
	,[DtIdxB]         [datetimE]              NULL
	,[DtA]            [datetimE]              NULL
	,[DtB]            [datetimE]              NULL
	,[DtC]            [datetimE]              NULL
	,[DtD]            [datetimE]              NULL
	,[DtE]            [datetimE]              NULL
	,[BoolIdxA]       [bit]                   NULL
	,[BoolIdxB]       [bit]                   NULL
	,[BoolA]          [bit]                   NULL
	,[BoolB]          [bit]                   NULL
	,[BoolC]          [bit]                   NULL
	,[BoolD]          [bit]                   NULL
	,[BoolE]          [bit]                   NULL
	,[MoneyIdxA]      [MONEY]                 NULL
	,[MoneyIdxB]      [MONEY]                 NULL
	,[MoneyA]         [MONEY]                 NULL
	,[MoneyB]         [MONEY]                 NULL
	,[MoneyC]         [MONEY]                 NULL
	,[MoneyD]         [MONEY]                 NULL
	,[MoneyE]         [MONEY]                 NULL
	,[MemoA]          [TEXT]
	,[MemoB]          [TEXT]
	,[MemoC]          [TEXT]
	,[MemoD]          [TEXT]
	,[MemoE]          [TEXT]
	,[MemoF]          [TEXT]
	,[IntIdxA]        [INT]                   NULL
	,[IntIdxB]        [INT]                   NULL
	,[IntA]           [INT]                   NULL
	,[IntB]           [INT]                   NULL
	,[IntC]           [INT]                   NULL
	,[IntD]           [INT]                   NULL
	,[IntE]           [INT]                   NULL
	,[DECIMALIdxA]    [DECIMAL](19,3)         NULL
	,[DECIMALIdxB]    [DECIMAL](19,3)         NULL
	,[DECIMALA]       [DECIMAL](19,3)         NULL
	,[DECIMALB]       [DECIMAL](19,3)         NULL
	,[DECIMALC]       [DECIMAL](19,3)         NULL
	,[DECIMALD]       [DECIMAL](19,3)         NULL
	,[DECIMALE]       [DECIMAL](19,3)         NULL
	,[IntSmallIdxA]   [SMALLINT]              NULL
	,[IntSmallIdxB]   [SMALLINT]              NULL
	,[IntSmallA]      [SMALLINT]              NULL
	,[IntSmallB]      [SMALLINT]              NULL
	,[IntSmallC]      [SMALLINT]              NULL
	,[IntSmallD]      [SMALLINT]              NULL
	,[IntSmallE]      [SMALLINT]              NULL
	,[IntBigIdxA]     [BIGINT]                NULL
	,[IntBigIdxB]     [BIGINT]                NULL
	,[IntBigA]        [BIGINT]                NULL
	,[IntBigB]        [BIGINT]                NULL
	,[IntBigC]        [BIGINT]                NULL
	,[IntBigD]        [BIGINT]                NULL
	,[IntBigE]        [BIGINT]                NULL
	,[FloatIdxA]      [FLOAT](8)              NULL
	,[FloatIdxB]      [FLOAT](8)              NULL
	,[FloatA]         [FLOAT](8)              NULL
	,[FloatB]         [FLOAT](8)              NULL
	,[FloatC]         [FLOAT](8)              NULL
	,[FloatD]         [FLOAT](8)              NULL
	,[FloatE]         [FLOAT](8)              NULL
    -- MetaData				                       
	,[IsActive]       [bit]               NOT NULL
    ,[MetaCreated]    [datetime]          NOT NULL
    ,[MetaCreatedBy]  [varchar](50)       NOT NULL --[int]               NOT NULL
    ,[MetaEdited]     [datetime]          NOT NULL
    ,[MetaEditedBy]   [varchar](50)       NOT NULL --[int]               NOT NULL
	--todo add the *ByID fields 
	,CONSTRAINT       PK_Id               PRIMARY KEY CLUSTERED (Id)
    ,INDEX            IX_StringIdx50A     NONCLUSTERED          (StringIdx50A)      
    ,INDEX            IX_StringIdx50B     NONCLUSTERED          (StringIdx50B)      
    ,INDEX            IX_StringIdx50C     NONCLUSTERED          (StringIdx50C)      
    ,INDEX            IX_StringIdx50D     NONCLUSTERED          (StringIdx50D)      
    ,INDEX            IX_StringIdx50E     NONCLUSTERED          (StringIdx50E)      
    ,INDEX            IX_DtIdxA           NONCLUSTERED          (DtIdxA      )      
    ,INDEX            IX_DtIdxB           NONCLUSTERED          (DtIdxB      )      
    ,INDEX            IX_BoolIdxA         NONCLUSTERED          (BoolIdxA    )      
    ,INDEX            IX_BoolIdxB         NONCLUSTERED          (BoolIdxB    )      
    ,INDEX            IX_MoneyIdxA        NONCLUSTERED          (MoneyIdxA   )      
    ,INDEX            IX_MoneyIdxB        NONCLUSTERED          (MoneyIdxB   )      
    ,INDEX            IX_IntIdxA          NONCLUSTERED          (IntIdxA     )      
    ,INDEX            IX_IntIdxB          NONCLUSTERED          (IntIdxB     )      
    ,INDEX            IX_DECIMALIdxA      NONCLUSTERED          (DECIMALIdxA )      
    ,INDEX            IX_DECIMALIdxB      NONCLUSTERED          (DECIMALIdxB )      
    ,INDEX            IX_IntSmallIdxA     NONCLUSTERED          (IntSmallIdxA)      
    ,INDEX            IX_IntSmallIdxB     NONCLUSTERED          (IntSmallIdxB)      
    ,INDEX            IX_IntBigIdxA       NONCLUSTERED          (IntBigIdxA  )      
    ,INDEX            IX_IntBigIdxB       NONCLUSTERED          (IntBigIdxB  )      
    ,INDEX            IX_FloatIdxA        NONCLUSTERED          (FloatIdxA   )      
    ,INDEX            IX_FloatIdxB        NONCLUSTERED          (FloatIdxB   )      
) 
GO
SET IDENTITY_INSERT [DataShort1] ON
INSERT INTO DataShort1([ID],[MetaFlag],[MetaStruct],[StringIdx50A],[StringIdx50B],[StringIdx50C],[StringIdx50D],[StringIdx50E],[StringIdx100A],[String100A],[String100B],[String100C],[String100D],[String100E],[DtIdxA],[DtIdxB],[DtA],[DtB],[DtC],[DtD],[DtE],[BoolIdxA],[BoolIdxB],[BoolA],[BoolB],[BoolC],[BoolD],[BoolE],[MoneyIdxA],[MoneyIdxB],[MoneyA],[MoneyB],[MoneyC],[MoneyD],[MoneyE],[IntIdxA],[IntIdxB],[IntA],[IntB],[IntC],[IntD],[IntE],[DECIMALIdxA],[DECIMALIdxB],[DECIMALA],[DECIMALB],[DECIMALC],[DECIMALD],[DECIMALE],[IntSmallIdxA],[IntSmallIdxB],[IntSmallA],[IntSmallB],[IntSmallC],[IntSmallD],[IntSmallE],[IntBigIdxA],[IntBigIdxB],[IntBigA],[IntBigB],[IntBigC],[IntBigD],[IntBigE],[FloatIdxA],[FloatIdxB],[FloatA],[FloatB],[FloatC],[FloatD],[FloatE],[IsActive],[MetaCreated],[MetaCreatedBy],[MetaEdited],[MetaEditedBy])
SELECT [ID],[MetaFlag],[MetaStruct],[StringIdx50A],[StringIdx50B],[StringIdx50C],[StringIdx50D],[StringIdx50E],[StringIdx100A],[String100A],[String100B],[String100C],[String100D],[String100E],[DtIdxA],[DtIdxB],[DtA],[DtB],[DtC],[DtD],[DtE],[BoolIdxA],[BoolIdxB],[BoolA],[BoolB],[BoolC],[BoolD],[BoolE],[MoneyIdxA],[MoneyIdxB],[MoneyA],[MoneyB],[MoneyC],[MoneyD],[MoneyE],[IntIdxA],[IntIdxB],[IntA],[IntB],[IntC],[IntD],[IntE],[DECIMALIdxA],[DECIMALIdxB],[DECIMALA],[DECIMALB],[DECIMALC],[DECIMALD],[DECIMALE],[IntSmallIdxA],[IntSmallIdxB],[IntSmallA],[IntSmallB],[IntSmallC],[IntSmallD],[IntSmallE],[IntBigIdxA],[IntBigIdxB],[IntBigA],[IntBigB],[IntBigC],[IntBigD],[IntBigE],[FloatIdxA],[FloatIdxB],[FloatA],[FloatB],[FloatC],[FloatD],[FloatE],[IsActive],[MetaCreated],[MetaCreatedBy],[MetaEdited],[MetaEditedBy]
FROM DataShort1bk
SELECT * FROM DataShort1
--ROLLBACK -- 
COMMIT