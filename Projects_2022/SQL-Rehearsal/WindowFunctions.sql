--Window Functions
USE [AdventureWorks2019]
GO

-- Setting up flag
SELECT * FROM Person.BusinessEntityAddress;

SELECT COUNT(AddressTypeID) 
FROM Person.BusinessEntityAddress
GROUP BY AddressTypeID

SELECT BusinessEntityID, AddressID, AddressTypeID,
	COUNT(*) OVER(PARTITION BY AddressTypeID) [Flag]
FROM Person.BusinessEntityAddress;

SELECT BusinessEntityID, AddressID, AddressTypeID,
	(SELECT COUNT(AddressTypeID) -- Error invalid object name
	FROM Person.BusinessEntityAddress b
	WHERE AddressTypeID = a.AddressTypeID)
FROM Person.BusinessEntityAddress a;

--Finding "out-of-sequence" records using the LAG() function
SELECT * FROM HumanResources.[Shift]
SELECT * 
FROM
	(SELECT 
	 hrs.*,
		LAG([Name]) OVER(PARTITION BY ShiftID ORDER BY StartTime) [Start Time]
	FROM HumanResources.[Shift] hrs
	) t1 
WHERE [Name] = 'THREE' AND [Start Time] != 'TWO'

--Alternatively
SELECT * FROM HumanResources.vEmployeeDepartmentHistory
SELECT * FROM HumanResources.EmployeeDepartmentHistory

SELECT a.BusinessEntityID, a.[Shift], b.[Shift] [First Shift]
FROM HumanResources.vEmployeeDepartmentHistory a,
	HumanResources.vEmployeeDepartmentHistory b
WHERE a.BusinessEntityID = b.BusinessEntityID
AND b.StartDate = (
					SELECT MAX(StartDate) 
					FROM HumanResources.vEmployeeDepartmentHistory
					WHERE StartDate < A.StartDate
					AND a.BusinessEntityID = b.BusinessEntityID)
AND a.[Shift] = 'Day'
AND NOT b.[Shift] = 'EVENING'

--Getting a running Total
SELECT * FROM Sales.SalesPersonQuotaHistory
SELECT * FROM Sales.SalesOrderDetail

SELECT ProductID, UnitPrice, SUM(UnitPrice)
	OVER (ORDER BY ProductID ASC) [Running]
FROM Sales.SalesOrderDetail
ORDER BY ProductID ASC


--Adding a total rows selected to every row
SELECT * FROM Sales.SalesPersonQuotaHistory

SELECT *, COUNT(*) OVER() [Total Rows]
FROM Sales.SalesPersonQuotaHistory

--Getting N most recent rows over multiple grouping
SELECT * FROM Sales.SalesPersonQuotaHistory

SELECT BusinessEntityID, QuotaDate
FROM Sales.SalesPersonQuotaHistory

WITH [Sales].[SalesPersonQuotaHistory] AS ( --Does not work
	SELECT *, ROW_NUMBER() OVER (PARTITION BY BusinessEntityID
			ORDER BY QuotaDate DESC) [Row Number]
	FROM Sales.SalesPersonQuotaHistory)
SELECT * FROM Sales.SalesPersonQuotaHistory
WHERE [Row Number] <= 1