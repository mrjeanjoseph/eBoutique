-- Chech if the table exists...
IF 0 < (
    SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_TYPE = 'BASE TABLE'
    AND TABLE_SCHEMA = 'dbo'
    AND TABLE_NAME = 'TABLE1'
)
BEGIN
    -- Check for NULL values in the primary-key column
    IF 0 = (SELECT COUNT(*) FROM TABLE1 WHERE TableID IS NULL)
    BEGIN
        ALTER TABLE TABLE1 ALTER COLUMN TableID uniqueidentifier NOT NULL 

        -- No, don't drop, FK references might already exist...
        -- Drop PK if exists (it is very possible it does not have the name you think it has...)
        -- ALTER TABLE1 DROP CONSTRAINT pk_constraint_name 
        --DECLARE @pkDropCommand nvarchar(1000) 
        --SET @pkDropCommand = N'ALTER TABLE1 DROP CONSTRAINT ' + QUOTENAME((SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
        --WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' 
        --AND TABLE_SCHEMA = 'dbo' 
        --AND TABLE_NAME = 'TABLE1' 
        ----AND CONSTRAINT_NAME = 'PK_TABLE1' 
        --))
        ---- PRINT @pkDropCommand 
        --EXECUTE(@pkDropCommand) 
        -- Instead do
        -- EXEC sp_rename 'dbo.TABLE1.PK_TABLE11234565', 'TABLE1';

        -- Check if the keys are unique (it is very possible they might not be)        
        IF 1 >= (SELECT TOP 1 COUNT(*) AS cnt FROM TABLE1 GROUP BY TableID ORDER BY cnt DESC)
        BEGIN

            -- If no Primary key for this table
            IF 0 =  
            (
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
                WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' 
                AND TABLE_SCHEMA = 'dbo' 
                AND TABLE_NAME = 'TABLE1' 
                -- AND CONSTRAINT_NAME = 'TABLE1' 
            )
                ALTER TABLE TABLE1 ADD CONSTRAINT TableID PRIMARY KEY CLUSTERED (TableID ASC)
            ;

        END -- End uniqueness check
        ELSE
            PRINT 'FSCK, this column has duplicate keys, and can thus not be changed to primary key...' 
    END -- End NULL check
    ELSE
        PRINT 'FSCK, need to figure out how to update NULL value(s)...' 
END 