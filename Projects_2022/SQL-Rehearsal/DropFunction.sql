--DROP FUNCTION because it is being referenced by object

--To show dependencies you can use sp_depends procedure:
exec sp_depends 'dbo.YourFunc'

--Or, call sys.dm_sql_referencing_entities table valued function:
SELECT * FROM sys.dm_sql_referencing_entities('dbo.YourFunc','OBJECT')

--If the specified functions were used by;
	--a Column for its default value
	--and a Computed Column for its value calculation
--Case 1: The Default Constraints can be viewed from sys.default_constraints
SELECT * FROM sys.default_constraints

--Drop the default constraint by
ALTER TABLE tableName DROP CONSTRAINT DF__tableName_ConstraintName

--After removing all usage of this Function as default constraint, we can drop the function by
DROP FUNCTION 'schemaName.fnName'

--Case 2: We can see the computed columns from sys.computed_columns
SELECT * FROM sys.computed_columns 

--We can drop a column by
ALTER TABLE [schema].ptableName] DROP COLUMN [columnName]