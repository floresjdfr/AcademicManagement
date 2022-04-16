--SEARCH
CREATE OR ALTER PROCEDURE udpFindUserType(@Pk_UserType AS INT = NULL, @TypeDescription AS VARCHAR(50) = NULL)
AS
BEGIN
    SELECT  *
    FROM    [dbo].[UserType] t 
    WHERE   (@Pk_UserType IS NULL OR Pk_UserType = @Pk_UserType)
            AND (@TypeDescription IS NULL OR TypeDescription = @TypeDescription);
END
