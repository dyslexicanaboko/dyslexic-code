USE [PagingSwitch]
GO

/****** Object:  Table [dbo].[PagerOwners]    Script Date: 06/26/2011 01:19:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PagerOwners](
	[PagerOwnerID] [int] IDENTITY(1,1) NOT NULL,
	[PagerID] [int] NULL,
	[FirstName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NULL,
	[EmailAddress] [nvarchar](200) NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[AdditionalInfo] [nvarchar](500) NULL,
	[CreatedDTM] [datetime2](7) NOT NULL,
	[UpdatedDTM] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PagerOwners] PRIMARY KEY CLUSTERED 
(
	[PagerOwnerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PagerOwners]  WITH CHECK ADD  CONSTRAINT [FK_PagerOwners_PagerOwners] FOREIGN KEY([PagerOwnerID])
REFERENCES [dbo].[PagerOwners] ([PagerOwnerID])
GO

ALTER TABLE [dbo].[PagerOwners] CHECK CONSTRAINT [FK_PagerOwners_PagerOwners]
GO

ALTER TABLE [dbo].[PagerOwners] ADD  CONSTRAINT [DF_PagerOwners_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO

ALTER TABLE [dbo].[PagerOwners] ADD  CONSTRAINT [DF_PagerOwners_UpdatedDTM]  DEFAULT (getdate()) FOR [UpdatedDTM]
GO

