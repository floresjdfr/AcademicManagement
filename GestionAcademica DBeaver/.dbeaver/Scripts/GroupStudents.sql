--------------------------SEARCH
--List GroupStudents 
CREATE OR ALTER PROCEDURE udpFindGroupStudents(@Pk_GroupStudents AS INT = NULL, @Fk_Student AS INT = NULL, @Fk_Group AS INT = NULL, @FK_CycleState AS INT = NULL)
AS
BEGIN
	SELECT
	gs.Pk_GroupStudents, 
	gs.Score, 
	--Group
	g.Pk_Group, 
	g.[Number] AS GroupNumber, 
	g.Schedule,
	
	--Teacher
	t.Pk_Teacher,
	t.ID AS TeacherID,
	t.Name As TeacherName,
	t.PhoneNumber As TeacherPhoneNumber,
	t.Email AS TeacherEmail,
	
	--Cycle
	c.Pk_Cycle,
	c.[Year],
	c.[Number] AS CycleNumber,
	c.StartDate,
	c.EndDate,
	
	--CycleState
	cs.Pk_CycleState,
	cs.StateDescription,
	
	--Student 
	s.Pk_Student,
	s.ID AS StudentID,
	s.Name AS StudentName,
	s.PhoneNumber AS StudentPhoneNumber,
	s.Email AS StudentEmail,
	s.DateOfBirth
	
	FROM [dbo].GroupStudents gs 
		LEFT JOIN [DBO].[Group] g ON gs.Fk_Group = g.Pk_Group 
			LEFT JOIN Teacher t ON g.Fk_Teacher = t.Pk_Teacher 
			LEFT JOIN [Cycle] c ON g.Fk_Cycle = c.Pk_Cycle 
				LEFT JOIN CycleState cs ON c.Fk_CycleState = cs.Pk_CycleState 
		LEFT JOIN Student s ON gs.Fk_Student = s.Pk_Student 
	WHERE (@Pk_GroupStudents IS NULL OR gs.Pk_GroupStudents = @Pk_GroupStudents) 
	AND (@Fk_Student IS NULL OR gs.Fk_Student = @Fk_Student)
	AND (@Fk_Group IS NULL OR gs.Fk_Group = @Fk_Group) 
	AND (@FK_CycleState IS NULL OR c.Fk_CycleState  = @FK_CycleState);
END




-----------------------------INSERT 
--Enroll
CREATE OR ALTER PROCEDURE udpInsertGroupStudent(
	@Fk_Group AS INT, 
	@Fk_Student AS INT
)
AS 
BEGIN
	INSERT INTO [dbo].GroupStudents (Fk_Group, Fk_Student) VALUES (@Fk_Group, @Fk_Student);
END


-------------------------------------UPDATE
--Rate Student
CREATE OR ALTER PROCEDURE udpUpdateGroupStudentScore(
	@Pk_GroupStudents AS INT,
	@Score AS DECIMAL
)
AS
BEGIN
	UPDATE [dbo].GroupStudents SET Score = @Score WHERE Pk_GroupStudents = @Pk_GroupStudents;
END


---------------------------------------DELETE 
--Leave
CREATE OR ALTER PROCEDURE udpDeleteGroupStudent(@Pk_GroupStudents AS INT)
AS 
BEGIN
	DELETE [dbo].GroupStudents WHERE Pk_GroupStudents = @Pk_GroupStudents;
END