CREATE PROCEDURE [dbo].[GetDrivers_3] (@filterXmlData XML)
AS BEGIN

DECLARE @handler INT;
DECLARE @filterTable TABLE (FieldName NVARCHAR(50), FieldValue NVARCHAR(50));
DECLARE @query NVARCHAR(500) = 'SELECT Firstname, Lastname, Birthdate FROM dbo.Driver WHERE ';
DECLARE @whereString NVARCHAR(400);

EXEC sp_xml_preparedocument @handler OUTPUT, @filterXmlData;

INSERT INTO @filterTable
SELECT *
FROM OPENXML (@handler, '/ROOT/Filter', 1) WITH (
		FieldName VARCHAR(50),
		FieldValue VARCHAR(50)
	);

EXEC sp_xml_removedocument @handler;

SELECT @whereString = COALESCE(@whereString + ' AND ', '') + FieldName + ' = ' + '''' + FieldValue + ''''
FROM @filterTable;

SET @query = CONCAT(@query, '', @whereString);
EXEC(@query);

END

GO
/*Test*/

DECLARE @filterXml XML;
SELECT @filterXml = BulkColumn
FROM OPENROWSET(
		/*need set absolute path*/
		BULK '..\module_6\Filter.xml',
		SINGLE_BLOB
	) x 

EXEC [dbo].[GetDrivers_3] @filterXml;