--Run this sp with no parameter to generate random data into table
--EXEC [EXE].[GenerateRandomDummyData]

ALTER PROCEDURE [EXE].[GenerateRandomDummyData]
AS
DECLARE @counter INT
SET @counter = 1
--Limits the exec to 10 rows each time
TRUNCATE TABLE [EXE].[DummyDataTbl]
WHILE @counter <= 10
BEGIN
	INSERT INTO  [EXE].[DummyDataTbl] VALUES (
		ABS(CAST(NEWID() AS BINARY( 6)) %1000) + 1,
		CAST( (ABS(CAST(NEWID() AS BINARY(6)) %1000) + 1)/7.0123 AS NUMERIC( 15,4)),
		CONVERT(varchar, (DATEADD(DAY, RAND(CHECKSUM(NEWID()))*(1+DATEDIFF(DAY, '01/01/1980', '12/31/2022')),'01/01/1980')), 107),
		--This line from http://stackoverflow.com/questions/15038311/sql-password-generator-8-characters-upper-and-lower-and-include-a-number
		CAST((ABS(CHECKSUM(NEWID()))%10) AS VARCHAR(1)) + CHAR(ASCII('a')+(ABS(CHECKSUM(NEWID()))%25)) + CHAR(ASCII('A')+(ABS(CHECKSUM(NEWID()))%25)) + LEFT(NEWID(),5),
		ABS(CHECKSUM(NEWID()))%50000+1,
		CONVERT(VARCHAR, GETDATE(), 101))
	PRINT @Counter
	SET @Counter = @counter + 1
END

SELECT * FROM [EXE].[DummyDataTbl]

RETURN 0

ALTER TABLE [EXE].[DummyDataTbl]
ALTER COLUMN RandomDate VARCHAR(15)

--KEEP WORKING HERE!!!