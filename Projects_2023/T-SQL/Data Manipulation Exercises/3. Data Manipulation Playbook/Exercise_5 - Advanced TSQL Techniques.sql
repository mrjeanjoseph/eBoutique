BEGIN
	SELECT * FROM Comments WHERE PostId = 10

	-- Inserting multiple record all at once.
	INSERT INTO [dbo].[Comments]([PostId],[Score],[Text],[CreationDate],[UserDisplayName],[UserId])
	VALUES	(10 ,0 ,N'This is a comment made by Jean-Joseph' ,GETUTCDATE() ,N'fjeanjoseph' ,24),
			(10 ,0 ,N'This is a comment made by Louna Jean-Joseph' ,GETUTCDATE() ,N'fjeanjoseph' ,24),
			(10 ,0 ,N'This is a comment made by Denzel Wasington' ,GETUTCDATE() ,N'fjeanjoseph' ,24),
			(10 ,0 ,N'This is a comment made by Denzel Paniague' ,GETUTCDATE() ,N'fjeanjoseph' ,24),
			(10 ,0 ,N'This is a comment made by Kervens Jean-Joseph' ,GETUTCDATE() ,N'fjeanjoseph' ,24)
END


BEGIN --Using SELECT within INSERT Statement
--Moving users with reputation higher than 5000 as employees
	SELECT u.Id, u.Reputation, u.CreationDate, u.DisplayName
	FROM [dbo].[Users] u
	WHERE u.Reputation > 5000

--check table exists
	SELECT DISTINCT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES --no employee exists initially

	CREATE TABLE Employees (
		Id INT NOT NULL PRIMARY KEY IDENTITY (1,1),
		Reputation INT NOT NULL DEFAULT 0,
		CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
		DisplayName VARCHAR(255) NOT NULL,
		Salary INT,
		JobTitle VARCHAR(255),
		Boss INT,
		Department VARCHAR(255)
	);

	SET IDENTITY_INSERT Employees ON
	INSERT INTO Employees (Id, Reputation, CreationDate, DisplayName, Salary)
		SELECT u.Id, u.Reputation, u.CreationDate, u.DisplayName, 0
		FROM [dbo].[Users] u
		WHERE u.Reputation > 5000
	SET IDENTITY_INSERT Employees OFF

	SELECT * FROM Employees
END


BEGIN -- Using SELECT to Retrieve a specific field in an INSERT statement
	SELECT Id FROM Posts WHERE Title = 'Parallel and distributed computing';

	SELECT * FROM Users WHERE DisplayName = 'user3040444'

	INSERT INTO Comments (CreationDate, PostId, Score, [Text], UserDisplayName, UserId)
	VALUES (
		GETDATE(),
		(SELECT Id FROM Posts WHERE Title = 'Parallel and distributed computing'),
		0,
		'We added a comment using SELECT statement',
		'fjeanjoseph',
		(SELECT Id FROM Users WHERE DisplayName = 'user3040444')
	)

	SELECT * FROM Comments WHERE UserDisplayName = 'fjeanjoseph' ORDER BY CreationDate DESC;
END


BEGIN -- Retrieve Records on INSERT using the OUTPUT clause

	-- For INSERT statments
	INSERT INTO Posts (PostTypeId, Body, OwnerUserId, OwnerDisplayName, Title)
	OUTPUT INSERTED.Id -- to be display as soon as data is inserted.
	VALUES (1, 'This is a post and you know you like it.', 75298, 'fjeanjoseph', 'Cool Post Title')

	-- For UPDATED statments
	UPDATE Posts 
	SET Score += FavoriteCount
	OUTPUT INSERTED.Id, INSERTED.Title, INSERTED.Score, INSERTED.FavoriteCount -- We can output multiple column names
	-- **Notice we use INSERTED instead of UPDATED
	WHERE FavoriteCount > 50

	-- For UPDATED statments
	SELECT * FROM Users WHERE DisplayName LIKE '%darth%';

	DELETE TOP(3) FROM Users
	OUTPUT DELETED.*
	FROM Users u
	LEFT JOIN Posts p ON u.Id = p.OwnerUserId
	LEFT JOIN Badges b ON u.Id = b.UserId
	WHERE DisplayName LIKE '%darth%'
	AND p.OwnerUserId IS NULL
	AND b.UserId IS NULL
END


BEGIN -- Synchronizing Tables Using the MERGE Statment
		--MERGE Statment - Used to sync tables
	--Step1 Create two tables
	CREATE TABLE CommentsSource(
		Id INT NOT NULL PRIMARY KEY,
		PostId INT NOT NULL,
		Score INT NOT NULL DEFAULT 0,
		[Text] VARCHAR(MAX) NOT NULL,
		CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
		UserDisplayName VARCHAR(255),
		UserId INT,
	)	
	CREATE TABLE CommentsTarget(
		Id INT NOT NULL PRIMARY KEY,
		PostId INT NOT NULL,
		Score INT NOT NULL DEFAULT 0,
		[Text] VARCHAR(MAX) NOT NULL,
		CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
		UserDisplayName VARCHAR(255),
		UserId INT,
	)	

	--Step2 Move data into CommentsSource table
	INSERT INTO CommentsSource (Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId)
	SELECT Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId FROM Comments 
	EXCEPT (
		SELECT Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId 
		FROM Comments 
		WHERE [Text] LIKE '%python%'
	)

	--Step3 Move data into CommentsTarget table
	INSERT INTO CommentsTarget (Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId)
	SELECT Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId FROM Comments 
	EXCEPT (
		SELECT Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId 
		FROM Comments 
		WHERE [Text] LIKE '%java%'
	)
	
	--We could have rewritten these scripts using this simpler syntax but we could be wrong
	SELECT Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId FROM Comments 
	WHERE [Text] NOT LIKE '%keyword here%'

	--Step4 Check to make sure the both tables have their intended data
	SELECT * FROM CommentsSource WHERE [Text] LIKE '%python%'; -- Initially empty and will remain empty
	SELECT * FROM CommentsTarget WHERE [Text] LIKE '%java%'; -- Initially empty but will have data

	SELECT * FROM CommentsSource WHERE [Text] LIKE '%java%'; -- has java and will remain
	SELECT * FROM CommentsTarget WHERE [Text] LIKE '%python%'; -- has python but will be deleted

	--Step5 Finally the MERGE
	MERGE CommentsTarget ct USING CommentsSource cs ON (ct.[Text] = cs.[Text])
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, PostId, Score, [Text], CreationDate, UserDisplayName, UserId)
		VALUES (cs.Id, cs.PostId, cs.Score, cs.[Text], cs.CreationDate, cs.UserDisplayName, cs.UserId)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;
END


BEGIN
	SELECT * FROM Comments ORDER BY Id DESC

	BULK INSERT Comments -- Somehow, we don't have the comment.dsv file formatted correctly. This is a manual process for the 
	FROM 'C:\Users\admin\source\_workspace\CRUD\T-SQL\TSQL Data Manipulation Exercises\comments.dsv' WITH
	(FORMATFILE = 'C:\Users\admin\source\_workspace\CRUD\T-SQL\TSQL Data Manipulation Exercises\comments-format.fmt');
END