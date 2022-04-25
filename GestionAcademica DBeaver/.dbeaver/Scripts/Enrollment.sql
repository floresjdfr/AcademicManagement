----------------------------SEARCH
-- Lists student enrolled courses
-- In case it's needed to get the groups of current cycle, specify @Fk_CycleState = 1 which means the active cycle
CREATE OR ALTER PROCEDURE udpFindStudentCourseGroups(@Fk_Student AS INT, @Fk_CycleState AS INT = NULL)
AS 
BEGIN
	select cg.*, c.*, g.*, t.*
	from GroupStudents gs, CourseGroups cg 
	left join Course c on cg.Fk_Course = c.Pk_Course 
	left join [Group] g on cg.Fk_Group = g.Pk_Group 
	left join [Cycle] c2 on g.Fk_Cycle = c2.Pk_Cycle 
	left join CycleState cs on c2.Fk_CycleState = cs.Pk_CycleState 
	left join Teacher t on g.Fk_Teacher = t.Pk_Teacher 
	where gs.Fk_Student = @Fk_Student  and gs.Fk_Group = cg.Fk_Group and (@Fk_CycleState is null or c2.Fk_CycleState = @Fk_CycleState);
end

-- Lists available courses to enroll using Pk_Student
-- Returns Tables: CourseGroups, Course and Group
create or alter procedure udpFindAvaibleGroupsToEnroll(@studentID as int)
as
BEGIN 
	select cg.*, c2.*, g.*, t.*, t.Name as TeacherName
	from Student s , CareerCourses cc , CourseGroups cg
	left join [Group] g on cg.Fk_Group = g.Pk_Group 
	left join [Cycle] c on g.Fk_Cycle = c.Pk_Cycle 
	left join Course c2 on cg.Fk_Course = c2.Pk_Course 
	left join CycleState cs on c.Fk_CycleState = cs.Pk_CycleState 
	left join Teacher t on g.Fk_Teacher = t.Pk_Teacher 
	where s.Pk_Student = @studentID and s.Fk_Carreer = cc.Fk_Career and cc.Fk_Course = cg.Fk_Course and c.Fk_CycleState = 1;
END





