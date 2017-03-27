USE [PagingSwitch]
GO

/****** Object:  Table [dbo].[PagingQueue]    Script Date: 06/26/2011 01:19:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PagingQueue](
	[PagingQueueID] [int] IDENTITY(1,1) NOT NULL,
	[SubscriberID] [int] NOT NULL,
	[MessageText] [nvarchar](500) NOT NULL,
	[ResponseText] [nvarchar](500) NULL,
	[IsGroupMessage] [bit] NOT NULL,
	[IsSent] [bit] NOT NULL,
	[SenderIP] [nvarchar](12) NULL,
	[PagerOwnerID] [int] NULL,
	[DateTimeSent] [datetime] NULL,
	[DateTimeCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_PagingQueue] PRIMARY KEY CLUSTERED 
(
	[PagingQueueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PagingQueue] ADD  CONSTRAINT [DF_PagingQueue_IsGroupMessage]  DEFAULT ((0)) FOR [IsGroupMessage]
GO

ALTER TABLE [dbo].[PagingQueue] ADD  CONSTRAINT [DF_PagingQueue_IsSent]  DEFAULT ((0)) FOR [IsSent]
GO

ALTER TABLE [dbo].[PagingQueue] ADD  CONSTRAINT [DF_PagingQueue_DateTimeCreated]  DEFAULT (getdate()) FOR [DateTimeCreated]
GO

