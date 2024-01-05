SELECT CONCAT('Hello ', 'World');

SELECT CONCAT('Foo',CAST(42 AS VARCHAR(5)), 'Bar');

SELECT LEN('Hello SQL');
SELECT DATALENGTH('Hello SQL  ');

DECLARE @str VARCHAR(100) = 'Hello '
SELECT DATALENGTH(@str) Result

DECLARE @nstr NVARCHAR(100) = 'Hello SQL'
SELECT DATALENGTH(@Nstr)

SELECT RTRIM('                          HELLO  .')

SELECT UPPER('Hello mssql') [UPPER CASE], LOWER('Hello MSSQL') [lower case];

--String Split
SELECT VALUE FROM STRING_SPLIT('Lorem Ipsum Dolor Sit Amet.',' ');

SELECT REPLACE('Lorem Ipsum Dolor Sit Amet', 'Dolor','Billy');

--Implementation
DECLARE @newWrds VARCHAR(10) = 'Maximus',
		@myStr NVARCHAR(100) = 'Lorem Ipsum Dolor Sit Amet';

DECLARE @replaceStr VARCHAR(100) =  REPLACE (@myStr, 'Dolor',@newWrds);
SELECT VALUE FROM STRING_SPLIT(@replaceStr,' ');

SELECT SUBSTRING('Hello SQL', 1,5)

--Get the last n char of a string
DECLARE @strCh1 VARCHAR(10) = 'Hello', @strCh2 VARCHAR(10) = 'SQL DATABASE';
SELECT SUBSTRING(@strCh1, LEN(@strCh1) - 2,3)
SELECT SUBSTRING(@strCh2, LEN(@strCh2) - 2,3)

SELECT STUFF('FooBarBaz',4,3,'Hello')
SELECT LEFT('Bonjour',3);
SELECT RIGHT('Bonjour',4);

--Reverse a string
SELECT REVERSE('look at me, i am in reverse');

SELECT REPLICATE ('Hello ',3);
--
USE [CrudAllDay]
GO
--Replace function similar to Set
SELECT * FROM dbo.Customers

--This is for display purposes only
SELECT CustFirstName,
	REPLACE(PrefContact, 'Email','FaxMachine') PrefContact
FROM dbo.Customers
ORDER BY CustFirstName

--This is permanent
UPDATE dbo.Customers -- Don't work
SET PrefContact = (PrefContact, 'Email', 'Email Address');

--Another option
UPDATE dbo.Customers -- Don't work
SET PrefContact = (PrefContact, 'Email', 'Email Address')
WHERE PrefContact LIKE 'Email%';