USE [PagingSwitch]
GO

/****** Object:  Table [dbo].[Pagers]    Script Date: 06/26/2011 01:19:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pagers](
	[PagerID] [int] IDENTITY(1,1) NOT NULL,
	[SubscriberID] [int] NOT NULL,
	[IndividualID] [int] NULL,
	[GroupID] [int] NULL,
	[MaicdropID] [int] NULL,
	[BagID] [int] NULL,
	[AdditionalNotes] [nvarchar](500) NULL,
	[CreatedDTM] [datetime2](7) NOT NULL,
	[UpdatedDTM] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Pagers] PRIMARY KEY CLUSTERED 
(
	[PagerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Pagers] ADD  CONSTRAINT [DF_Pagers_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO

ALTER TABLE [dbo].[Pagers] ADD  CONSTRAINT [DF_Pagers_UpdatedDTM]  DEFAULT (getdate()) FOR [UpdatedDTM]
GO

