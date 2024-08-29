CREATE TABLE [dbo].[Permissions]
(
	[Id] INT identity (1,1) PRIMARY KEY NOT NULL, 
    [EmployeeForename] VARCHAR(300) NOT NULL, 
    [EmployeeSurname] VARCHAR(300) NOT NULL, 
    [PermissionTypeId] INT NOT NULL, 
    [PermissionDate] DATE NOT NULL, 
    CONSTRAINT [FK_Permissions_PermissionTypes] FOREIGN KEY ([PermissionTypeId]) REFERENCES [PermissionTypes]([Id])
)
