--Function Analytic
USE [AdventureWorks2019]
GO

--LAG and LEAD
SELECT * FROM Sales.SalesPerson;

SELECT BusinessEntityID, SalesYTD,
		LEAD(SalesYTD, 1, 0) OVER(ORDER BY BusinessEntityID) [Lead Values],
		LAG(SalesYTD, 1, 0) OVER(ORDER BY BusinessEntityID) [Lag Values]
FROM Sales.SalesPerson;

--Percentile_Disc and Percentile_Cont
SELECT * FROM HumanResources.Employee

SELECT BusinessEntityID, JobTitle, SickLeaveHours,
		CUME_DIST() OVER(PARTITION BY JobTitle ORDER BY SickLeaveHours ASC) [Cumulative Distribution],
		PERCENTILE_DISC(0.5) WITHIN GROUP(ORDER BY SickLeaveHours)
		OVER(PARTITION BY JobTitle) [Percentile Discreet]
FROM HumanResources.Employee;

SELECT BusinessEntityID, JobTitle, SickLeaveHours,
		CUME_DIST() OVER(PARTITION BY JobTitle ORDER BY SickLeaveHours ASC) [Cumulative Distribution],
		PERCENTILE_DISC(0.5) WITHIN GROUP(ORDER BY SickLeaveHours)
			OVER(PARTITION BY JobTitle) [Percentile Discreet],
		PERCENTILE_CONT(0.5) WITHIN GROUP(ORDER BY SickLeaveHours)
			OVER(PARTITION BY JobTitle) [Percentile Continuous]
FROM HumanResources.Employee;

--First Value
SELECT * FROM Sales.SalesTaxRate;

SELECT StateProvinceID, [Name], TaxRate,
	FIRST_VALUE(StateProvinceID)
	OVER(ORDER BY TaxRate DESC) [First Value]
FROM Sales.SalesTaxRate;

--Last Value
SELECT * FROM Sales.SalesTerritoryHistory;

SELECT TerritoryID, [StartDate], BusinessEntityID,
	LAST_VALUE(BusinessEntityID)
	OVER(ORDER BY TerritoryID) [Last Value]
FROM Sales.SalesTerritoryHistory;

--Percent_Rank and CUME_DIST
SELECT * FROM HumanResources.Employee;

SELECT BusinessEntityID, JobTitle, SickLeaveHours,
	PERCENT_RANK() OVER(PARTITION BY JobTitle ORDER BY SickLeaveHours DESC) [Percent Rank],
	CUME_DIST() OVER(PARTITION BY JobTitle ORDER BY SickLeaveHours DESC) [Cumulative Distribution]
FROM HumanResources.Employee