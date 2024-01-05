ALTER PROCEDURE [Rehearsals].InsertUpdateDelete_Customer
	@CustomerID BIGINT = 0
	,@Name NVARCHAR(100) = NULL
	,@Mobileno NVARCHAR(15) = NULL
	,@Address NVARCHAR(300) = 0
	,@Birthdate DATETIME = NULL
	,@EmailID NVARCHAR(15) = NULL
	,@Query INT

AS
BEGIN
	IF (@Query = 1)
		BEGIN
			INSERT INTO [Rehearsals].[Customer] ([CustomerID],[NAME], [Address], [Mobileno], [Birthdate], [EmailID])
				VALUES (@CustomerID, @Name,@Address,@Mobileno,@Birthdate,@EmailID)
			IF (@@ROWCOUNT > 0)
			BEGIN
				SELECT 'Insert'
			END
		END

	IF (@Query = 2)
		BEGIN
			UPDATE [Rehearsals].[Customer]
				SET NAME = @Name,Address = @Address,Mobileno = @Mobileno,Birthdate = @Birthdate,EmailID = @EmailID
				WHERE Customer.CustomerID = @CustomerID
			SELECT 'Update'
		END

	IF (@Query = 3)
		BEGIN
			DELETE
				FROM [Rehearsals].[Customer]
				WHERE [Rehearsals].[Customer].CustomerID = @CustomerID
			SELECT 'Deleted'
		END

	IF (@Query = 4)
		BEGIN
			SELECT *
			FROM [Rehearsals].[Customer]
		END

	IF (@Query = 5)
		BEGIN
			SELECT *
			FROM [Rehearsals].[Customer]
			WHERE [Rehearsals].[Customer].CustomerID = @CustomerID
		END
END