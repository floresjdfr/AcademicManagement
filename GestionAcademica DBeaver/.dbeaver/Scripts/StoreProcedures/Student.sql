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


-- Get cycles list of student academic history
CREATE OR ALTER PROCEDURE udpFindStudentCycles(@Pk_Student AS INT )
AS
BEGIN
    SELECT  c.*, cs.*
    FROM GroupStudents gs  
   	left join [Group] g on gs.Fk_Group = g.Pk_Group 
   	left join [Cycle] c on g.Fk_Cycle = c.Pk_Cycle 
   	left join CycleState cs on c.Fk_CycleState = cs.Pk_CycleState 
   	where gs.Fk_Student = @Pk_Student;
END

-- Get student academic history
-- It can be filtered by cycle, otherwise it return the whole academic history
CREATE OR ALTER PROCEDURE udpFindStudentAcademicHistory(@Pk_Student AS INT, @Pk_Cycle AS INT = NULL)
AS
BEGIN
    SELECT gs.*, g.*, t.*, 
    --Cycle
    c.Pk_Cycle,
    c.Fk_CycleState,
    c.[Year],
    c.[Number] as CycleNumber,
    c.StartDate,
    c.EndDate,
   	
    --Course
    
    c2.Pk_Course,
    c2.Code,
    c2.Name as CourseName,
    c2.Credits,
    c2.WeeklyHours
   
    
    FROM GroupStudents gs, CourseGroups cg  
   	left join [Group] g on cg.Fk_Group = g.Pk_Group 
   	left join [Cycle] c on g.Fk_Cycle = c.Pk_Cycle 
   	left join CycleState cs on c.Fk_CycleState = cs.Pk_CycleState 
   	left join Course c2 on cg.Fk_Course = c2.Pk_Course 
   	left join Teacher t on g.Fk_Teacher = t.Pk_Teacher 
   	where gs.Fk_Student = @Pk_Student
   	and gs.Fk_Group = cg.Fk_Group 
   	and (@Pk_Cycle is null or c.Pk_Cycle = @Pk_Cycle);
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