USE [PagingSwitch]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetMessages]    Script Date: 02/19/2011 17:47:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_GetMessages]
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
