CREATE TABLE [dbo].[Permissions]
(
	[Id] INT identity (1,1) PRIMARY KEY NOT NULL, 
    [EmployeeForename] VARCHAR(300) NOT NULL, 
    [EmployeeSurname] VARCHAR(300) NOT NULL, 
    [PermissionType] INT NOT NULL, 
    [PermissionDate] DATE NOT NULL,

)
