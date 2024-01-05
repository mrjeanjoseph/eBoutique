--VIEWS
USE [AdventureWorks2019]
GO

--Simple Views
SELECT * FROM HumanResources.Employee;

CREATE VIEW [New_Emp] AS
SELECT e.BusinessEntityID, e.NationalIDNumber, e.HireDate
FROM HumanResources.Employee e
WHERE e.HireDate > '2009-01-01';

SELECT * FROM New_Emp;

--Complex Views
SELECT * FROM HumanResources.Employee
SELECT * FROM HumanResources.EmployeePayHistory

CREATE VIEW [Emp_Pay] AS
SELECT e.JobTitle, SUM(e.VacationHours) [Vacation Hours]
FROM HumanResources.Employee e
JOIN HumanResources.EmployeePayHistory h
ON e.BusinessEntityID = h.BusinessEntityID
GROUP BY e.JobTitle;

SELECT * FROM Emp_Pay

--Materialzed Views are not supported in MSSQL. Use Index views instead.