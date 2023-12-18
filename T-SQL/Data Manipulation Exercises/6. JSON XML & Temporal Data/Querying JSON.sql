USE[JSON-XML-TDB]

BEGIN
	-- Querying JSON Documents with TSQL
	SELECT
		JSON_VALUE(Post_json, '$.Post.Id') AS JsonID,
		JSON_VALUE(Post_json, '$.Post.Title'), /*AS JsonTitle*/
		JSON_QUERY(Post_json, '$.Post.Author') AS JsonAuthor,
		JSON_VALUE(Post_json, '$.Post.Comments[0].Comment.Text') AS jsonFirstComment
	FROM Posts
	WHERE JSON_VALUE(Post_json, '$.Post.Title') = 'Python vs R for machine learning';
END

BEGIN
	-- Converting Data into JSON with FOR JSON PATH and AUTO
	SELECT
		c.Text [Content],
		u.DisplayName [AuthorName],
		u.LastAccessDate [Last Access Date],
		JSON_VALUE(Post_json, '$.Post.Title') [Post]
	FROM Comments c
	JOIN Users u ON u.Id = c.UserId
	JOIN Posts p ON JSON_VALUE(Post_json, '$.Post.Id') = c.PostId
	WHERE c.Score > 15 AND JSON_VALUE(Post_json, '$.Post.Title') IS NOT NULL
	FOR JSON PATH, ROOT('Comments');

	---
	SELECT 
		u.DisplayName,
		u.Reputation,
		JSON_VALUE(Post_json, '$.Post.Title') AS PostTitle
	FROM Users u
	JOIN Posts p ON p.Id = JSON_VALUE(Post_json, '$.Post.OwnerUserId')
	WHERE u.Reputation > 8000 
	AND JSON_VALUE(Post_json, '$.Post.Title') IS NOT NULL
	FOR JSON AUTO
END

BEGIN
	DECLARE @JsonFile NVARCHAR(MAX);
	SELECT @JsonFile = BulkColumn
	FROM OPENROWSET(BULK 'C:\globaldir\top-posts.json', SINGLE_CLOB) AS j;

	If(ISJSON(@JsonFile)=1) PRINT 'It is a valid JSON';
	PRINT @JsonFile;
	
	-- Parsing and Importing JSON using OPENJSON
	DECLARE @JsonFile2 NVARCHAR(MAX);
	SET @JsonFile2 = N'
	[
	   {
		  "Id":6107,
		  "Score":176,
		  "ViewCount":155988,
		  "Title":"What are deconvolutional layers?",
		  "OwnerUserId":8820
	   },
	   {
		  "Id":155,
		  "Score":164,
		  "ViewCount":25822,
		  "Title":"Publicly Available Datasets",
		  "OwnerUserId":227
	   }
	]';

	SELECT * FROM OPENJSON(@JsonFile2) WITH (
		Id INT,
		Score INT,
		ViewCount INT,
		Title NVARCHAR(255),
		OwnerUserId INT
	) AS TopPosts
END

BEGIN
	-- Handling Missing Properties with Lax and Strict mode
	SELECT TOP 1 Post_Json FROM Posts

	SELECT TOP(10)
		JSON_VALUE(Post_Json, '$.Post.Id') [Post Id],
		JSON_VALUE(Post_Json, '$.Post.Title') [Post Title],
		JSON_VALUE(Post_json, '$.Post.Author.Reputation') [Reputation],
		JSON_VALUE(Post_Json, 'lax$.Post.Badges') [Post Badges] --Lax is default format
		-- JSON_VALUE(Post_Json, 'strict$.Post.Badges') [Post Badges]
		-- JSON_VALUE(Post_Json, '$.Post.Badges') [Post Badges]
	FROM Posts
	WHERE JSON_VALUE(Post_json, '$.Post.Title') LIKE '%Python%';

END