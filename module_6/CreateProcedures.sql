USE [SqlModule];
GO
/*FORMATMESSAGE just for fun :)  dynamic execution*/
CREATE PROCEDURE [dbo].[GetDrivers_1] (@fieldName NVARCHAR(50), @fieldValue NVARCHAR(50))
AS BEGIN
	DECLARE @query NVARCHAR(200);
	SELECT @query = FORMATMESSAGE('SELECT Firstname, Lastname, Birthdate FROM [dbo].[Driver] WHERE %s=''%s''', @fieldName, @fieldValue);
	EXEC(@query);
END
GO

CREATE PROCEDURE [dbo].[GetDrivers_2] (@fieldName NVARCHAR(50), @fieldValue NVARCHAR(50))
AS BEGIN
	DECLARE @query NVARCHAR(200);
	SET @query = 'SELECT Firstname, Lastname, Birthdate FROM dbo.Driver WHERE ' + @fieldName + '= @fieldValue;';
	EXECUTE sp_executesql @query, N'@fieldValue NVARCHAR(50)', @fieldValue;
END
GO

/* TEST RUN */
DECLARE @firstNameField NVARCHAR(50) = 'FirstName';
DECLARE @firstNameValue NVARCHAR(50) = 'Adam';
EXEC [dbo].[GetDrivers_1] @firstNameField, @firstNameValue;
EXEC [dbo].[GetDrivers_2] @firstNameField, @firstNameValue;
GO