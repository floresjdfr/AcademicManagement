--INSERT
CREATE OR ALTER PROCEDURE udpInsertTeacher(@Fk_User AS INT, @ID AS VARCHAR(50), @Name AS VARCHAR(50), @PhoneNumber AS VARCHAR(50), @Email AS VARCHAR(50))
AS
BEGIN
    INSERT INTO [dbo].Teacher(Fk_User, ID, Name, PhoneNumber, Email) VALUES (@Fk_User, @ID, @Name, @PhoneNumber, @Email);
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
CREATE OR ALTER PROCEDURE udpFindTeacher(@Pk_Teacher AS INT = NULL, @ID AS VARCHAR(50) = NULL, @Name AS VARCHAR(50) = NULL, @PhoneNumber AS VARCHAR(50) = NULL, @Email AS VARCHAR(50) = NULL, @Fk_User AS INT = NULL)
AS
BEGIN
    SELECT  *
    FROM    [dbo].Teacher t 
    WHERE   (@Pk_Teacher IS NULL OR Pk_Teacher = @Pk_Teacher)
            AND (@ID IS NULL OR ID = @ID)
            AND (@Name IS NULL OR Name = @Name)
            AND (@PhoneNumber IS NULL OR PhoneNumber = @PhoneNumber)
            AND (@Email IS NULL OR Email = @Email)
            AND (@Fk_User IS NULL OR Fk_User = @Fk_User)
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


-- Get teacher groups
create or alter procedure udpFindTeacherGroups(@pk_teacher as int, @pk_cycleState as int)
as
begin 
	select cg.*, c.*, g.*
	from CourseGroups cg
	left join Course c on cg.Fk_Course = c.Pk_Course 
	left join [Group] g on cg.Fk_Group = g.Pk_Group 
	left join Teacher t on g.Fk_Teacher = t.Pk_Teacher 
	left join [Cycle] c2 on g.Fk_Cycle = c2.Pk_Cycle 
	where g.Fk_Teacher = @pk_teacher and c2.Fk_CycleState = @pk_cycleState and cg.Fk_Group = g.Pk_Group;
end

























