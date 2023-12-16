
USE TSFP
BEGIN --Setting up the connection
	SP_CONFIGURE 'show advanced options', 1
	RECONFIGURE
	SP_CONFIGURE 'Ad Hoc Distributed Queries', 1
	RECONFIGURE	
END
GO;


BEGIN -- Querying the remote server
	SELECT * FROM OPENQUERY 
	(NameOfLinkedServer, 'SELECT * FROM [DatabaseName].[SchemaName].[TableName]');

	--Can use an orderby clause	
	SELECT * FROM OPENQUERY 
	(NameOfLinkedServer, 'SELECT * FROM [DatabaseName].[SchemaName].[TableName] ORDER BY [ColumnName]');
	
	--Can move data from this linked Server into our local tables
	SELECT * 
	INTO [LocalTable]
	FROM OPENQUERY 
	(NameOfLinkedServer, 'SELECT * FROM [DatabaseName].[SchemaName].[TableName] ORDER BY [ColumnName]');

	--Can filter the data to only get dataset we want
	SELECT * 
	INTO [LocalTable]
	FROM OPENQUERY 
	(NameOfLinkedServer, 'SELECT * FROM [DatabaseName].[SchemaName].[TableName] WHERE [ColumnName] = ''Some Value'' ');

	--Can filter the data even more for brevity
	SELECT s.Country_Code, c.CountryCode,
	SUM(Sales_Qty)
	FROM F_Sales s
	INNER JOIN OPENQUERY 
	(NameOfLinkedServer, 'SELECT * FROM [DatabaseName].[SchemaName].[TableName] ORDER BY [ColumnName]') c
	ON s.Country_Code = c.CountryCode
	GROUP BY s.Country_Code, c.CountryCode
	ORDER BY s.Country_Code, c.CoutryCode
END


BEGIN --Using OpenROWSET

	--Using linked server using OPENROWSET
	SELECT *
	FROM OPENROWSET ('[SQLOLEDB]', 'ConnectionStrings',
			'SELECT Country_Code, Region
			 FROM [TrainingDB Remote].dbo.M_ISO_Region
			 ORDER BY Country_Code, Region');

	--Joining other tables with the linked server
	SELECT s.Country_Code, c.Country_Name,
	SUM(s.Sales_Qty) [Sales Qty]
	FROM F_Sales s
	JOIN OPENROWSET ('[SQLOLEDB]', 'ConnectionStrings',
			'SELECT Country_Code, Region
			 FROM [TrainingDB Remote].dbo.M_ISO_Region
			 ORDER BY Country_Code, Region') c
	ON s.Country_Code = c.Country_Code
	GROUP BY s.Country_Code, c.CountryCode
	ORDER BY s.Country_Code, c.CoutryCode
	
	--Fixing a collation
	SELECT s.Country_Code, c.Country_Name,
	SUM(s.Sales_Qty) [Sales Qty]
	FROM F_Sales s
	JOIN OPENROWSET ('[SQLOLEDB]', 'ConnectionStrings',
			'SELECT Country_Code, Region
			 FROM [TrainingDB Remote].dbo.M_ISO_Region
			 ORDER BY Country_Code, Region') c
	ON s.Country_Code collate SQL_Latin1_General_CP1_CI_AS = c.Country_Code
	GROUP BY s.Country_Code, c.CountryCode
	ORDER BY s.Country_Code, c.CoutryCode
END