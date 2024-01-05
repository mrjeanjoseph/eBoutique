--Common Table Expressions
USE [AdventureWorks2019]
GO

--Generating values
WITH Numbers(i) AS(
	SELECT 1
	UNION ALL
	SELECT i+1
	FROM Numbers
	WHERE i< 5
) SELECT i [Numbers] FROM Numbers;

--Recursively enumerating a subtree
SELECT * FROM HumanResources.vEmployee;

WITH RECURSIVE ManagedByJeanJoseph(Level, BusinessEntityID, FirstName, LastName) AS (
	SELECT 1, BusinessEntityID, FirstName, LastName
	FROM HumanResources.vEmployee
	WHERE BusinessEntityID = 1
	UNION ALL

	SELECT ManagedByJeanJoseph.Level + 1,
			BusinessEntityID,
			FirstName,
			LastName
	FROM HumanResources.vEmployee e
	JOIN ManagedByJeanJoseph m
	ON e.BusinessEntityID = m.BusinessEntityID
	ORDER BY 1 DESC
) SELECT * FROM ManagedByJeanJoseph;

--Temporary Query
SELECT * FROM HumanResources.EmployeeDepartmentHistory;

WITH ReadyCars AS(
	SELECT *
	FROM HumanResources.EmployeeDepartmentHistory
	WHERE EndDate IS NULL
) SELECT BusinessEntityID, DepartmentID, ShiftID
FROM ReadyCars
ORDER BY ShiftID

SELECT BusinessEntityID, DepartmentID, ShiftID
FROM (
	SELECT *
	FROM HumanResources.EmployeeDepartmentHistory
	WHERE EndDate IS NULL
) AS ReadyEmp
ORDER BY ShiftID

--Recursively Generate Dates
DECLARE @DateFrom DATETIME = '2019-07-10';
DECLARE @DatetO DATETIME = '2019-09-10';
DECLARE @IntervalDays INT = 7;

WITH Rooster AS(
	SELECT @DateFrom AS RosterStart, 1 AS TeamA, 2 AS TeamB, 3 AS TeamC
	UNION ALL
	SELECT DATEADD(d, @IntervalDays, RosterStart),
		CASE TeamA WHEN 1 THEN 2 WHEN 2 THEN 3 WHEN 3 THEN 1 END AS TeamA,
		CASE TeamB WHEN 1 THEN 2 WHEN 2 THEN 3 WHEN 3 THEN 1 END AS TeamB,
		CASE TeamC WHEN 1 THEN 2 WHEN 2 THEN 3 WHEN 3 THEN 1 END AS TeamC
	FROM Roster WHERE RosterStart < DATEADD(d, -@IntervalDays, @DateTo)
)
SELECT RosterStart,
	ISNULL(LEAD(RoasterStart) OVER(ORDER BY RosterStart), RosterStart + @IntervalDays) [RosterEnd],	
		CASE TeamA WHEN 1 THEN 'RR' WHEN 2 THEN 'DS' WHEN 3 THEN 'NS' END AS TeamA,
		CASE TeamB WHEN 1 THEN 'RR' WHEN 2 THEN 'DS' WHEN 3 THEN 'NS' END AS TeamB,
		CASE TeamC WHEN 1 THEN 'RR' WHEN 2 THEN 'DS' WHEN 3 THEN 'NS' END AS TeamC
FROM Roster
