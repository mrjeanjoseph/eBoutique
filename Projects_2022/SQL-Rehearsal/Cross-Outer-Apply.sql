--Using CROSS APPLY and INNER JOIN
SELECT * 
FROM Departments d
CROSS APPLY (
	SELECT *
	FROM Employees e
	WHERE e.DepartmentID = d.DeptID
) A
GO
SELECT *
FROM Departments d
INNER JOIN Employees e
ON d.DeptID = e.DepartmentID

--Using OUTER APPLY and OUTER JOIN
SELECT * 
FROM Departments d
OUTER APPLY (
	SELECT *
	FROM Employees e
	WHERE e.DepartmentID = d.DeptID
) A
GO
SELECT *
FROM Departments d
LEFT OUTER JOIN Employees e
ON d.DeptID = e.DepartmentID
GO

--Table-valued Function for APPLY Operator
CREATE FUNCTION dbo.fn_GetAllEmployeeOfADepartment(@DeptID AS int)
RETURNS TABLE AS
RETURN(
	SELECT *
	FROM Employees e
	WHERE e.DepartmentID = @DeptID) GO

SELECT *
FROM Departments d
CROSS APPLY dbo.fn_GetAllEmployeeOfADepartment(d.DeptID) GO

SELECT *
FROM Departments d
OUTER APPLY dbo.fn_GetAllEmployeeOfADepartment(d.DeptID) GO
