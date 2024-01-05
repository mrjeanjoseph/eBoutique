CREATE TRIGGER PreventUpdateRestrictedColumn
ON YourTable
AFTER UPDATE
AS
BEGIN
    IF UPDATE(RestrictedColumn)
    BEGIN
        IF EXISTS (SELECT * FROM inserted WHERE RestrictedColumn <> deleted.RestrictedColumn)
        BEGIN
            -- Raise an error if attempting to update the RestrictedColumn
            RAISEERROR('Updates to RestrictedColumn are not allowed.', 16, 1);
            ROLLBACK TRANSACTION; -- Optionally, rollback the transaction
        END
    END
END;
