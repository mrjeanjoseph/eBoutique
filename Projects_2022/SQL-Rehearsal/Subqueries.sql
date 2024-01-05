--Subqueries
USE [AdventureWorks2019]
GO

--Subquery in FROM clause
SELECT * FROM Sales.Store;
SELECT * FROM Sales.Currency ORDER BY [Name];

SELECT GETDATE();
INSERT INTO Sales.Currency(CurrencyCode, [Name], ModifiedDate)
VALUES('HTG', 'Haiti', GETDATE());

SELECT * FROM Sales.SalesPerson;
SELECT * FROM HumanResources.Employee;

--Subquery in FROM clause
SELECT e.BusinessEntityID, s.BusinessEntityID
FROM (
	SELECT BusinessEntityID
	FROM HumanResources.Employee e
	WHERE e.BusinessEntityID > 270) AS [Employees]
JOIN Sales.SalesPerson s
ON s.BusinessEntityID = HumanResources.Employee.BusinessEntityID;

--Subquery in SELECT clause
SELECT * FROM Sales.SalesPerson;
SELECT * FROM HumanResources.Employee;
SELECT * FROM HumanResources.Department;
