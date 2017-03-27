USE [PagingSwitch]
GO

/****** Object:  Table [dbo].[Groups]    Script Date: 06/26/2011 01:19:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Groups](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupDescription] [nvarchar](100) NOT NULL,
	[CreatedDTM] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO

