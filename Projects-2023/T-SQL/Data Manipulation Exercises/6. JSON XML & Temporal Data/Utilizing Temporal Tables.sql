BEGIN
	--Creating Temporal and History Tables
	CREATE TABLE PostsTemporal (
		Id INT NOT NULL PRIMARY KEY CLUSTERED,
		CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
		Score INT NOT NULL DEFAULT 0,
		ViewCount INT,
		Body NVARCHAR(MAX) NOT NULL,
		OwnerUserId INT,
		LastActivityDate DATETIME NOT NULL DEFAULT GETDATE(),
		Title NVARCHAR(MAX),
		Tags NVARCHAR(MAX),
		AnswerCount INT,
		CommentCount INT,
		FavoriteCount INT,
		SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
		SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
		PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
	)	WITH (
		SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.PostsHistory)
	);
END

BEGIN
	--Inserting, Adding and modifying data in Temporal tables
	INSERT PostsTemporal (
		id,
		CreationDate,
		Score,
		ViewCount,
		Body,
		OwnerUserId,
		LastActivityDate,
		Title,
		Tags,
		AnswerCount,
		CommentCount,
		FavoriteCount)
	SELECT
		Id,
		JSON_VALUE(Post_json, '$.Post.CreationDate') AS CreationDate,
		JSON_VALUE(Post_json, '$.Post.Score') AS Score,
		JSON_VALUE(Post_json, '$.Post.ViewCount') AS ViewCount,
		JSON_VALUE(Post_json, '$.Post.Body') AS Body,
		JSON_VALUE(Post_json, '$.Post.OwnerUserId') AS OwnerUserId,
		JSON_VALUE(Post_json, '$.Post.LastActivityDate') AS LastActivityDate,
		JSON_VALUE(Post_json, '$.Post.Title') AS Title,
		JSON_VALUE(Post_json, '$.Post.Tags') AS Tags,
		JSON_VALUE(Post_json, '$.Post.AnswerCount') AS AnswerCount,
		JSON_VALUE(Post_json, '$.Post.CommentCount') AS CommentCount,
		JSON_VALUE(Post_json, '$.Post.FavoriteCount') AS FavoriteCount
	FROM Posts
	WHERE	JSON_VALUE(Post_json, '$.Post.Tags') LIKE '%python%'
	AND		JSON_VALUE(Post_json, '$.Post.Score') > 20
	AND		JSON_VALUE(Post_json, '$.Post.Body') IS NOT NULL;

	SELECT * FROM PostsTemporal;
	SELECT * FROM PostsHistory;

	UPDATE PostsTemporal
	SET Title = 'Estimating users age based on Facebook sites they like'
	WHERE Id = 116;

	UPDATE PostsTemporal
	SET Title = 'Which cost function is the best option for neural networks'
	WHERE Id = 9850

	SELECT * FROM PostsTemporal;
	SELECT * FROM PostsHistory;

	--Larger number of update
	UPDATE PostsTemporal
	SET Score += 50
	WHERE Tags LIKE '%neural-network%' OR Tags LIKE '%deep-learning%';

	SELECT * FROM PostsTemporal;
	SELECT * FROM PostsHistory;

	--Delete statement
	DELETE PostsTemporal
	WHERE Score < 50;
	SELECT * FROM PostsTemporal;
	SELECT * FROM PostsHistory;
END

BEGIN
	--Selecting data from temporal tables at specific points in time
	SELECT Id, Score, Title, CreationDate, OwnerUserId
	FROM PostsTemporal
	WHERE Id = 9850

	SELECT Id, Score, Title, CreationDate, OwnerUserId
	FROM PostsTemporal
	FOR SYSTEM_TIME AS OF '2023-12-17 16:40:40.593'
	WHERE Id = 9850

	--Multiple ways to retrieve (Particular time range)
	SELECT * FROM PostsTemporal WHERE Id = 116;

	SELECT * FROM PostsTemporal
	FOR SYSTEM_TIME BETWEEN '2019-09-16 20:05:45' AND '2023-12-17 16:59:42.223'
	WHERE Id = 116;

	SELECT * FROM PostsHistory WHERE Id = 116;

	SELECT * FROM PostsTemporal
	FOR SYSTEM_TIME ALL
	WHERE Id = 9850
	ORDER BY SysEndTime
END

BEGIN
	--Recovering data in a temporal table
	SELECT * FROM PostsTemporal WHERE Tags LIKE '%scikit-learn%';
	DELETE PostsTemporal WHERE Tags LIKE '%scikit-learn%';
	SELECT * FROM PostsTemporal WHERE Tags LIKE '%scikit-learn%';

	--Recovering the data
	INSERT INTO PostsTemporal
		(id,
		CreationDate,
		Score,
		ViewCount,
		Body,
		OwnerUserId,
		LastActivityDate,
		Title,
		Tags,
		AnswerCount,
		CommentCount,
		FavoriteCount)
	SELECT id,
		CreationDate,
		Score,
		ViewCount,
		Body,
		OwnerUserId,
		LastActivityDate,
		Title,
		Tags,
		AnswerCount,
		CommentCount,
		FavoriteCount
	FROM PostsTemporal 
	FOR SYSTEM_TIME AS OF '2023-12-17 17:00:58.933'
	WHERE Tags LIKE '%scikit-learn%';
	
	SELECT * FROM PostsTemporal WHERE Tags LIKE '%scikit-learn%';
END
 