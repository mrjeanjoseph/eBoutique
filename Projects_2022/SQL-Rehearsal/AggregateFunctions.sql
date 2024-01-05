USE [AdventureWorks2019]
GO
SELECT 
	SUM(VacationHours) [Total Vacation Hrs],
	COUNT(VacationHours) [Total Vacation Count]
FROM HumanResources.Employee


SELECT VacationHours
FROM HumanResources.Employee

--Counting rows
SELECT * FROM HumanResources.Department

SELECT COUNT(*) [Total Rows]
FROM HumanResources.Employee

SELECT GroupName, COUNT(*) [Total Rows]
FROM HumanResources.Department
GROUP BY GroupName;

SELECT COUNT(GroupName) [Group Name],
		COUNT(DISTINCT GroupName) [Single Count]
FROM HumanResources.Department