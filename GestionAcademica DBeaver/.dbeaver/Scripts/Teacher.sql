--INSERT
CREATE OR ALTER PROCEDURE udpInsertTeacher(@ID AS INT, @Name as VARCHAR(50), @PhoneNumber as VARCHAR(50), @Email AS VARCHAR(50))
AS
BEGIN
    INSERT INTO [dbo].Teacher(ID, Name, PhoneNumber, Email) VALUES (@ID, @Name, @PhoneNumber, @Email);
END

--UPDATE
CREATE OR ALTER PROCEDURE udpModifyTeacher(
	@Pk_Teacher AS INT,
	@ID AS INT, 
	@Name as VARCHAR(50), 
	@PhoneNumber as VARCHAR(50), 
	@Email AS VARCHAR(50)
)
AS
BEGIN
    UPDATE 
        [dbo].Teacher 
    SET 
		ID  = @ID,
		Name  = @Name, 
		PhoneNumber = @PhoneNumber, 
		Email = @Email
    WHERE
        Pk_Teacher  = @Pk_Teacher;
END

--SEARCH
CREATE OR ALTER PROCEDURE udpFindTeacher(@Pk_Teacher AS INT = NULL, @ID AS VARCHAR(50) = NULL, @Name AS VARCHAR(50) = NULL, @PhoneNumber AS VARCHAR(50) = NULL, @Email AS VARCHAR(50) = NULL)
AS
BEGIN
    SELECT  *
    FROM    [dbo].Teacher t 
    WHERE   (@Pk_Teacher IS NULL OR Pk_Teacher = @Pk_Teacher)
            AND (@ID IS NULL OR ID = @ID)
            AND (@Name IS NULL OR Name = @Name)
            AND (@PhoneNumber IS NULL OR PhoneNumber = @PhoneNumber)
            AND (@Email IS NULL OR Email = @Email)
END

--DELETE
CREATE OR ALTER PROCEDURE udpDeleteTeacher(@Pk_Teacher AS INT)
AS
BEGIN
    DELETE 
        [dbo].Teacher
    WHERE
        Pk_Teacher = @Pk_Teacher;
END
