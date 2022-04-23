--INSERT NEW Student
CREATE OR ALTER PROCEDURE udpInsertStudent(@Fk_Career AS INT, @Fk_User AS INT, @ID AS VARCHAR(50), @Name AS VARCHAR(50), @PhoneNumber AS VARCHAR(50), @Email AS VARCHAR(50), @DateOfBirth AS VARCHAR(50))
AS
BEGIN
	INSERT INTO [dbo].[Student] (Fk_Carreer, Fk_User, ID, Name, PhoneNumber, Email, DateOfBirth) VALUES (@Fk_Career, @Fk_User, @ID, @Name, @PhoneNumber, @Email, @DateOfBirth);
END

--UPDATE
--MODIFYTYPE => 1 = Other, 2 = Career, 3 = User
CREATE OR ALTER PROCEDURE udpModifyStudent(
	@Pk_Student AS INT,
	@Fk_Carreer AS INT = NULL,
	@Fk_User AS INT = NULL,
	@ID AS VARCHAR(50) = NULL, 
	@Name AS VARCHAR(50) = NULL, 
	@PhoneNumber AS VARCHAR(50) = NULL, 
	@Email AS VARCHAR(50) = NULL, 
	@DateOfBirth AS VARCHAR(50) = NULL,
	@ModifyType AS INT
)
AS
BEGIN
	IF (@ModifyType = 1)
	BEGIN 
		UPDATE [DBO].[Student] SET ID = @ID, NAME = @Name, PhoneNumber = @PhoneNumber, EMAIL = @Email, DateOfBirth = @DateOfBirth WHERE Pk_Student = @Pk_Student;
	END
	IF(@ModifyType = 2)
	BEGIN
		UPDATE [DBO].[Student] SET Fk_Carreer = @Fk_Carreer WHERE Pk_Student = @Pk_Student;
	END
	IF(@ModifyType = 3)
	BEGIN
		UPDATE [DBO].[Student] SET Fk_User = @Fk_User WHERE Pk_Student = @Pk_Student;
	END
END

--SEARCH
CREATE OR ALTER PROCEDURE udpFindStudent(
	@Pk_Student AS INT = NULL,
	@Fk_Career AS INT = NULL,
	@Fk_User AS INT = NULL,
	@ID AS VARCHAR(50) = NULL, 
	@Name AS VARCHAR(50) = NULL, 
	@PhoneNumber AS VARCHAR(50) = NULL, 
	@Email AS VARCHAR(50) = NULL, 
	@DateOfBirth AS VARCHAR(50) = NULL
)
AS
BEGIN
	SELECT  * 
    FROM    [dbo].[Student] s
    LEFT JOIN [dbo].Career c ON s.Fk_Carreer = c.Pk_Career  
    LEFT JOIN [dbo].[User] u ON s.Fk_User = u.Pk_User 
    LEFT JOIN [dbo].UserType ut ON u.Fk_UserType = ut.Pk_UserType 
    WHERE (@Pk_Student IS NULL OR s.Pk_Student = @Pk_Student) 
    AND (@Fk_Career IS NULL OR s.Fk_Carreer  = @Fk_Career)
    AND (@Fk_User IS NULL OR s.Fk_User = @Fk_User)
    AND (@ID IS NULL OR s.ID = @ID)
    AND (@Name IS NULL OR s.Name = @Name)
    AND (@PhoneNumber IS NULL OR s.PhoneNumber = @PhoneNumber)
    AND (@Email IS NULL OR s.Email = @Email)
    AND (@DateOfBirth IS NULL OR s.DateOfBirth = @DateOfBirth);
END

--DELETE
CREATE OR ALTER PROCEDURE udpDeleteStudent(@Pk_Student AS INT)
AS
BEGIN
    DELETE 
        [dbo].[Student]
    WHERE
        Pk_Student = @Pk_Student;
END