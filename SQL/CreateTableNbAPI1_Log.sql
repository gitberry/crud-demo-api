-- USE YOURDATABASEHERE
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
BEGIN TRANSACTION
CREATE TABLE [dbo].[nbAPI1_Log](
	[ID]          [int]      IDENTITY(1,1) NOT NULL,
	[ClientID]    [varchar](500)           NOT NULL,
	[IP]          [varchar](50)            NOT NULL,
	[URL]         [varchar](200)           NOT NULL,
	[DateTimeUTC] [datetime]               NOT NULL,
	[Info]        [varchar](500)           NOT NULL,
	[UserInfo]    [varchar](500)           NOT NULL,
    CONSTRAINT    [PK_Log1] PRIMARY KEY CLUSTERED ([ID]) 
) 
GO

-- COMMIT --
ROLLBACK -- use commit once you have smoke tested...