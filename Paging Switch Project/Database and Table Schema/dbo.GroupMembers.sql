USE [PagingSwitch]
GO

/****** Object:  Table [dbo].[GroupMembers]    Script Date: 06/26/2011 01:18:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GroupMembers](
	[GroupMemberID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NOT NULL,
	[PagerOwnerID] [int] NOT NULL,
	[CreatedDTM] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED 
(
	[GroupMemberID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Groups]
GO

ALTER TABLE [dbo].[GroupMembers] ADD  CONSTRAINT [DF_GroupMembers_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO

