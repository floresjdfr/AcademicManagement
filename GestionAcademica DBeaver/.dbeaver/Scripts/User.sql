--INSERT
CREATE OR ALTER PROCEDURE udpInsertUser(
	@ID AS VARCHAR(50), 
	@Password as VARCHAR(50),
	@Fk_UserType as INT)
AS
BEGIN
    INSERT INTO [dbo].[User] VALUES (@Fk_UserType, @ID, @Password);
END

--UPDATE
CREATE OR ALTER PROCEDURE udpModifyUser(
	@Pk_User AS INT,
	@ID AS VARCHAR(50), 
	@Password as VARCHAR(50)
)
AS
BEGIN
    UPDATE 
        [dbo].[User] 
    SET 
    	ID  = @ID,
		Password = @Password
    WHERE
        Pk_User  = @Pk_User;
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
