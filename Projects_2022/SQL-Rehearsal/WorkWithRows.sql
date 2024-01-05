--Working with Row Number
SELECT * FROM dbo.Departments;
WITH cte AS(
	SELECT DeptID,
	ROW_NUMBER() OVER (PARTITION BY DeptID ORDER BY DeptName DESC) AS rn
	FROM dbo.Departments
)
DELETE FROM cte WHERE rn > 1;

--Row numbers without partitions
SELECT ROW_NUMBER() OVER(ORDER BY DeptID ASC) AS RowNumber,
	DeptID, DeptName
FROM dbo.Departments

--Row numbers with partitions
SELECT ROW_NUMBER() OVER(PARTITION BY DeptID ORDER BY DeptID ASC) AS RowNumber
FROM dbo.Departments

--Adding more data
SELECT * FROM dbo.Departments;
INSERT INTO dbo.Departments
VALUES('Corporate');


