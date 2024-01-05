--Function Scalar/Single Row
USE [AdventureWorks2019]
GO

--Date and Time
SELECT DATENAME (Weekday, '07/09/2022') AS [Date Name]

SELECT GETDATE() AS [System Date]

SELECT NationalIDNumber, BirthDate, ModifiedDate FROM HumanResources.Employee

--Date computation
SELECT NationalIDNumber, DATEDIFF(DAY, BirthDate, ModifiedDate) [Processing Time]
FROM HumanResources.Employee;

SELECT NationalIDNumber, DATEDIFF(MONTH, BirthDate, ModifiedDate)
FROM HumanResources.Employee;

SELECT NationalIDNumber, DATEDIFF(MONTH, BirthDate, ModifiedDate)
FROM HumanResources.Employee;

SELECT NationalIDNumber, DATEDIFF(YEAR, BirthDate, ModifiedDate)
FROM HumanResources.Employee;

--
SELECT DATEADD (DAY, 50, '2022-07-09') [Add 20 Days]

--Character Modifications
SELECT *
FROM HumanResources.vEmployee

SELECT BusinessEntityID,FirstName,UPPER(LastName)
FROM HumanResources.vEmployee

SELECT BusinessEntityID,FirstName,lower(LastName)
FROM HumanResources.vEmployee

--Configuration and conversion function
SELECT @@SERVERNAME AS 'Server'

SELECT	CONCAT(FirstName, ' ', LastName, ' was hired on ', CAST(HireDate AS varchar(20))) [Using Cast],
		FirstName + ' ' + LastName + ' was hired on ' + 	CAST (HireDate AS varchar(20) ) AS 'Convert'
		--Using either cast or convert
FROM Person.Person AS p
JOIN HumanResources.Employee AS e
ON p.BusinessEntityID = e.BusinessEntityID
WHERE FirstName = 'Ken'

SELECT CONCAT(FirstName, ' ', LastName)
FROM Person.Person;

--The exact correct date is important
SELECT PARSE('Saturday, 9 July 2022' AS DATETIME2 USING 'en-US') AS [Date in English]

--Logical and Mathmetical Function
SELECT CHOOSE(3, 'Human Resouces', 'Sales', 'Admin', 'Marketing') [Result];

SELECT *
FROM Sales.SalesPerson

--Just like if function
SELECT BusinessEntityID, SalesYTD,
	IIF(SalesYTD > 200000, 'Bonus Applied', 'Not Applicable') [Bonus?]
FROM Sales.SalesPerson

SELECT POWER(5,2) [Result]