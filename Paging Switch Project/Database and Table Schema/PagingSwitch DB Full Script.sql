USE [PagingSwitch]
GO
/****** Object:  User [PagingSwitchUser]    Script Date: 06/26/2011 01:43:33 ******/
CREATE USER [PagingSwitchUser] FOR LOGIN [PagingSwitchUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [mojouser]    Script Date: 06/26/2011 01:43:33 ******/
CREATE USER [mojouser] FOR LOGIN [mojouser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetMessages]    Script Date: 06/26/2011 01:43:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetMessages]
	@messageType int = -1 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @strQuery nvarchar(500);
	
	set @strQuery = 	
	'SELECT 
		PagingQueueID, 
		SubscriberID, 
		MessageText, 
		IsSent, 
		DateTimeSent, 
		ResponseText,
		PagerOwnerID 
	FROM dbo.PagingQueue ';
	
	IF @messageType <> -1 BEGIN 
		set @strQuery = @strQuery + 'WHERE IsSent = ' + CONVERT(nvarchar(10), @messageType);
	END
	
	--PRINT @strQuery;
	
	EXEC(@strQuery);
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetDistinctSubscriberIDs]    Script Date: 06/26/2011 01:43:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetDistinctSubscriberIDs] 
	@lstGroupIDs nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @query nvarchar(max);
    
    set @query = '
		SELECT DISTINCT pgr.SubscriberID
		FROM dbo.Groups grp
			INNER JOIN dbo.GroupMembers mem
				ON grp.GroupID = mem.GroupID
			INNER JOIN dbo.PagerOwners own
				ON own.PagerOwnerID = mem.PagerOwnerID
			INNER JOIN dbo.Pagers pgr
				ON pgr.PagerID = own.PagerID
		WHERE grp.GroupID in (@groupIDs);';
	
	set @query = Replace(@query, '@groupIDs', @lstGroupIDs);
	
	--PRINT @query;
	
	EXEC(@query);
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePagerOwner]    Script Date: 06/26/2011 01:43:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePagerOwner] 
	@PagerOwnerID int,
	@PagerID int, 
    @FirstName nvarchar(20), 
    @LastName nvarchar(20), 
    @EmailAddress nvarchar(200), 
    @PhoneNumber nvarchar(20), 
    @AdditionalInfo nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @strQuery nvarchar(500);
	
	SET @strQuery = 
	'UPDATE dbo.PagerOwners SET '
    
    IF @PagerID <> -1 BEGIN
		SET @strQuery = @strQuery + 'PagerID = ' + CONVERT(nvarchar(10), @PagerID) + ','; 
    END
    
    SET @strQuery = @strQuery + 
    'FirstName = ''' + @FirstName + 
    ''',LastName = ''' + @LastName +  
    ''',EmailAddress = ''' + @EmailAddress +  
    ''',PhoneNumber = ''' + @PhoneNumber +
    ''',AdditionalInfo = ''' + @AdditionalInfo +
    ''',UpdatedDTM = GETDATE()
    WHERE PagerOwnerID = ' + CONVERT(nvarchar(10), @PagerOwnerID);
    
    --PRINT @strQuery;
    
    EXEC(@strQuery); 
END
GO
/****** Object:  Table [dbo].[PagingQueue]    Script Date: 06/26/2011 01:43:45 ******/
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
/****** Object:  Table [dbo].[Pagers]    Script Date: 06/26/2011 01:43:45 ******/
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
/****** Object:  Table [dbo].[PagerOwners]    Script Date: 06/26/2011 01:43:45 ******/
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
/****** Object:  Table [dbo].[Groups]    Script Date: 06/26/2011 01:43:45 ******/
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
/****** Object:  Table [dbo].[GroupMembers]    Script Date: 06/26/2011 01:43:45 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdatePager]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePager] 
	@pagerID int, 
    @subscriberID int, 
    @bagID int, 
    @notes nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.Pagers SET
	SubscriberID = @subscriberID,
	BagID = @bagID,
	AdditionalNotes = @notes,
	UpdatedDTM = GETDATE()
	WHERE PagerID = @pagerID;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateGroup]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdateGroup]
	@groupID int,
	@groupDescription nvarchar(100) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE dbo.Groups
    SET GroupDescription = @groupDescription
    WHERE GroupID = @groupID;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertGroup]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_InsertGroup] 
	@GroupDescription nvarchar(100)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @intGroups int;
	declare @intGroupID int;
	
	SELECT @intGroups = COUNT(1) 
	FROM dbo.Groups 
	WHERE GroupDescription = @GroupDescription
	
	IF @intGroups = 0 BEGIN
		INSERT INTO dbo.Groups (GroupDescription)
		VALUES(@GroupDescription);
		
		SELECT @intGroupID = SCOPE_IDENTITY();	
	END
	ELSE BEGIN
		SELECT @intGroupID = -1;
	END
	
    SELECT @intGroupID;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUnassignedPagers]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUnassignedPagers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		pgr.PagerID,
		pgr.SubscriberID
	FROM dbo.Pagers pgr
	WHERE pgr.PagerID not in 
		(
			SELECT own.PagerID
			FROM dbo.PagerOwners own
		);
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPagers]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetPagers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		pgr.PagerID,
		pgr.SubscriberID,
		pgr.IndividualID,
		pgr.GroupID,
		pgr.MaicdropID,
		pgr.BagID,
		pgr.AdditionalNotes,
		pgr.CreatedDTM,
		pgr.UpdatedDTM
	FROM dbo.Pagers pgr;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPagerOwners]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetPagerOwners]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		own.PagerOwnerID,
		own.PagerID,
		own.FirstName,
		own.LastName,
		own.EmailAddress,
		own.PhoneNumber,
		own.AdditionalInfo,
		pgr.SubscriberID,
		own.CreatedDTM,
		own.UpdatedDTM
	FROM dbo.PagerOwners own
		LEFT JOIN dbo.Pagers pgr
			ON own.PagerID = pgr.PagerID;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeletePager]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeletePager]
	@pagerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	DELETE FROM dbo.Pagers WHERE pagerID = @pagerID;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteOwner]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteOwner]
	@ownerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	DELETE FROM dbo.PagerOwners WHERE PagerOwnerID = @ownerID;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteGroup]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteGroup]
	@groupID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	DELETE FROM dbo.Groups WHERE GroupID = @groupID;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetExistingGroups]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetExistingGroups]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	SELECT
		grp.GroupID,
		grp.GroupDescription,
		grp.CreatedDTM
			FROM dbo.Groups grp;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetExistingGroupAndMembers]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetExistingGroupAndMembers]
	@groupID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	SELECT
		own.PagerOwnerID
	INTO #tempOwners
	FROM dbo.GroupMembers mem
		INNER JOIN dbo.PagerOwners own
			ON own.PagerOwnerID = mem.PagerOwnerID
	WHERE mem.GroupID = @groupID;

	-- SELECT * FROM #tempOwners

	SELECT
		pgr.SubscriberID,
		own.PagerOwnerID,
		own.FirstName, 
		own.LastName,
		own.EmailAddress,
		own.PhoneNumber,
		Added = 1
	FROM dbo.GroupMembers mem
		INNER JOIN dbo.PagerOwners own
			ON own.PagerOwnerID = mem.PagerOwnerID
		INNER JOIN dbo.Pagers pgr
			ON pgr.PagerID = own.PagerID
	WHERE mem.GroupID = @groupID
	UNION
	SELECT
		pgr.SubscriberID,
		own.PagerOwnerID,
		own.FirstName, 
		own.LastName,
		own.EmailAddress,
		own.PhoneNumber,
		Added = 0
	FROM dbo.PagerOwners own
		INNER JOIN dbo.Pagers pgr
			ON pgr.PagerID = own.PagerID
	WHERE own.PagerOwnerID not in (SELECT tmp.PagerOwnerID FROM #tempOwners tmp);
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteGroupMembers]    Script Date: 06/26/2011 01:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteGroupMembers]
	@groupID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	DELETE FROM dbo.GroupMembers WHERE GroupID = @groupID;
END
GO
/****** Object:  Default [DF_PagingQueue_IsGroupMessage]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[PagingQueue] ADD  CONSTRAINT [DF_PagingQueue_IsGroupMessage]  DEFAULT ((0)) FOR [IsGroupMessage]
GO
/****** Object:  Default [DF_PagingQueue_IsSent]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[PagingQueue] ADD  CONSTRAINT [DF_PagingQueue_IsSent]  DEFAULT ((0)) FOR [IsSent]
GO
/****** Object:  Default [DF_PagingQueue_DateTimeCreated]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[PagingQueue] ADD  CONSTRAINT [DF_PagingQueue_DateTimeCreated]  DEFAULT (getdate()) FOR [DateTimeCreated]
GO
/****** Object:  Default [DF_Pagers_CreatedDTM]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[Pagers] ADD  CONSTRAINT [DF_Pagers_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO
/****** Object:  Default [DF_Pagers_UpdatedDTM]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[Pagers] ADD  CONSTRAINT [DF_Pagers_UpdatedDTM]  DEFAULT (getdate()) FOR [UpdatedDTM]
GO
/****** Object:  Default [DF_PagerOwners_CreatedDTM]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[PagerOwners] ADD  CONSTRAINT [DF_PagerOwners_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO
/****** Object:  Default [DF_PagerOwners_UpdatedDTM]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[PagerOwners] ADD  CONSTRAINT [DF_PagerOwners_UpdatedDTM]  DEFAULT (getdate()) FOR [UpdatedDTM]
GO
/****** Object:  Default [DF_Groups_CreatedDTM]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO
/****** Object:  Default [DF_GroupMembers_CreatedDTM]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[GroupMembers] ADD  CONSTRAINT [DF_GroupMembers_CreatedDTM]  DEFAULT (getdate()) FOR [CreatedDTM]
GO
/****** Object:  ForeignKey [FK_PagerOwners_PagerOwners]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[PagerOwners]  WITH CHECK ADD  CONSTRAINT [FK_PagerOwners_PagerOwners] FOREIGN KEY([PagerOwnerID])
REFERENCES [dbo].[PagerOwners] ([PagerOwnerID])
GO
ALTER TABLE [dbo].[PagerOwners] CHECK CONSTRAINT [FK_PagerOwners_PagerOwners]
GO
/****** Object:  ForeignKey [FK_GroupMembers_Groups]    Script Date: 06/26/2011 01:43:45 ******/
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Groups]
GO
