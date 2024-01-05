--DELETE and DROP THEM ALL!!!

SELECT * FROM dbo.clonedCars;
DELETE FROM dbo.clonedCars;
TRUNCATE TABLE dbo.clonedCars;
DROP TABLE dbo.clonedCars;

SELECT * FROM dbo.ConcatCars;
TRUNCATE TABLE dbo.ConcatCars;
DROP TABLE dbo.ConcatCars;

SELECT * FROM dbo.DummyTable;

IF EXISTS(
	SELECT *
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_SCHEMA = 'dbo'
	AND TABLE_NAME = 'DummyTable')
DROP TABLE dbo.DummyTable;

--Same as above / will drop table and all data
DROP TABLE dbo.DummyTable2;