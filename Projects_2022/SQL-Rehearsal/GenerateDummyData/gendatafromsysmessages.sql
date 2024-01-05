--This script will generate a list names to be entered into an column
CREATE PROCEDURE [EXE].[GenerateRandomDataFromSys.Msgs]
AS
DECLARE @strings TABLE (
    message_id    INT NOT NULL,
    [text]        NVARCHAR(2048) NOT NULL,
    PRIMARY KEY CLUSTERED (message_id)
)

INSERT INTO @strings (message_id, [text])
SELECT TOP (25) message_id, [text]
FROM sys.messages
WHERE language_id=1033
ORDER BY NEWID();

--Using a recursive common table express
DECLARE @words TABLE (
    word NVARCHAR(100) NOT NULL,
    PRIMARY KEY CLUSTERED (word)
);

WITH CTE AS (
    --- The anchor:
    SELECT CAST(NULL AS NVARCHAR(100)) AS word, [text] AS remain
    FROM @strings
    UNION ALL
    --- The recursion:
    SELECT CAST(LEFT(remain, PATINDEX(N'%[^a-z]%', remain+N' ')-1) AS NVARCHAR(100)),
           CAST(SUBSTRING(remain, PATINDEX(N'%[^a-z]%', remain+N' ')+1, LEN(remain)) AS NVARCHAR(2048))
    FROM CTE WHERE remain!=N''
)

INSERT INTO @words
SELECT DISTINCT LOWER(word)
FROM CTE
WHERE LEN(word)>=3
OPTION (MAXRECURSION 0);

--Putting the words together
SELECT TOP (25)
    UPPER(LEFT(a.word, 1))+SUBSTRING(a.word, 2, LEN(a.word))+
    ISNULL(N' '+b.word, '')+
    ISNULL(N' '+c.word, '') AS CoolStartupName

--- First word:
FROM @words AS a

--- Second word:
OUTER APPLY (
    SELECT TOP (1)
           UPPER(LEFT(x.word, 1))+
           SUBSTRING(x.word, 2, LEN(x.word)) AS word
    FROM @words AS x
    WHERE LEFT(a.word, 3)!=LEFT(x.word, 3) AND RIGHT(a.word, 3)!=RIGHT(x.word, 3)
    ORDER BY NEWID()
    ) AS b

--- Third word (if there's space):
OUTER APPLY (
    SELECT TOP (1)
           UPPER(LEFT(x.word, 1))+
           SUBSTRING(x.word, 2, LEN(x.word)) AS word
    FROM @words AS x
    WHERE LEFT(a.word, 3)!=LEFT(x.word, 3) AND RIGHT(a.word, 3)!=RIGHT(x.word, 3)
      AND LEFT(b.word, 3)!=LEFT(x.word, 3) AND RIGHT(b.word, 3)!=RIGHT(x.word, 3)
      AND LEN(a.word)+LEN(b.word)<=12
    ORDER BY NEWID()
    ) AS c

ORDER BY NEWID();
RETURN 0 



--Run this command with no parameter to execute. exec [EXE].[GenerateRandomDataFromSys.Msgs]