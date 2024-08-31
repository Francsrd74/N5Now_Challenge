/*Add PermissionTypes*/
IF NOT EXISTS (SELECT TOP (1) * FROM [dbo].[PermissionTypes])
BEGIN
	SET IDENTITY_INSERT [dbo].[PermissionTypes] ON
		INSERT INTO [dbo].[PermissionTypes] ([ID],[Description]) VALUES (1,'Admin')
		INSERT INTO [dbo].[PermissionTypes] ([ID],[Description]) VALUES (2,'Guest')
	SET IDENTITY_INSERT [dbo].[PermissionTypes] OFF
END