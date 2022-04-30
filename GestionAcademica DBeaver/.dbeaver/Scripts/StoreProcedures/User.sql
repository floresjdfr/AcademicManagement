--INSERT
CREATE OR ALTER PROCEDURE udpInsertUser(
	@ID AS VARCHAR(50), 
	@Password as VARCHAR(50),
	@Fk_UserType as INT)
AS
BEGIN
	DECLARE @UsersCount AS INT
	SET @UsersCount = (SELECT COUNT(*) FROM [dbo].[User] u WHERE u.ID = @ID);
	IF @UsersCount = 0
		BEGIN
			INSERT INTO [dbo].[User] VALUES (@Fk_UserType, @ID, @Password);	
		END	
	ELSE
		BEGIN
			THROW 51000, 'An user with the same username already exists', 1; 
		END
END

--UPDATE
CREATE OR ALTER PROCEDURE udpModifyUser(
	@Pk_User AS INT,
	@Fk_UserType AS INT = NULL,
	@Password as VARCHAR(50) = NULL
)
AS
BEGIN
	IF (@Fk_UserType IS NOT NULL)
	BEGIN 
		UPDATE [dbo].[User] SET Fk_UserType = @Fk_UserType WHERE Pk_User  = @Pk_User;
	END
	IF (@Password IS NOT NULL)
	BEGIN 
		UPDATE [dbo].[User] SET Password = @Password WHERE Pk_User  = @Pk_User;
	END
END

--SEARCH
CREATE OR ALTER PROCEDURE udpFindUser(@Pk_User AS INT = NULL, @ID AS VARCHAR(50) = NULL, @Password AS VARCHAR(50) = NULL)
AS
BEGIN
    SELECT  *
    FROM    [dbo].[User] t 
    JOIN [dbo].UserType ut ON t.Fk_UserType = ut.Pk_UserType 
    WHERE   (@Pk_User IS NULL OR Pk_User = @Pk_User)
            AND (@ID IS NULL OR ID = @ID);
END

--DELETE
CREATE OR ALTER PROCEDURE udpDeleteUser(@Pk_User AS INT)
AS
BEGIN
    DELETE 
        [dbo].[User]
    WHERE
        Pk_User = @Pk_User;
END
