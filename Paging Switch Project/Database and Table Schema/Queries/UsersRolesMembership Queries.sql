SELECT *
  FROM [aspnetdb].[dbo].[aspnet_Users]


SELECT * FROM dbo.aspnet_Applications

EXEC dbo.aspnet_Membership_GetAllUsers @ApplicationName = 'beeperwebapp', @PageIndex = 0, @PageSize = 100