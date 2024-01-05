-------------------------------------------------------------------------------
SELECT * FROM [dbo].[Users] WHERE DisplayName LIKE '%jeanjoseph%'

SELECT * FROM [dbo].[Posts] WHERE OwnerUserId = 75298

SELECT OwnerUserId, COUNT(OwnerUserId) AS [Total Post] 
FROM [dbo].[Posts] 
GROUP BY OwnerUserId 
ORDER BY [Total Post] DESC


--------------------------------------------------------------------------------
BEGIN TRANSACTION

	UPDATE [dbo].[Posts]
	SET OwnerUserId = 75298,
		OwnerDisplayName = 'fjeanjoseph'
	WHERE OwnerUserId = 836;

	UPDATE Comments 
	SET UserId = 75298,
		UserDisplayName = 'fjeanjoseph'
	WHERE UserId = 836

COMMIT TRANSACTION