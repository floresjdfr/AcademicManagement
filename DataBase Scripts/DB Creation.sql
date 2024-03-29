USE [master]
GO
/****** Object:  Database [AcademicManagementDB]    Script Date: 22/8/2022 12:27:48 p. m. ******/
CREATE DATABASE [AcademicManagementDB]
 CONTAINMENT = NONE
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AcademicManagementDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
	EXEC [AcademicManagementDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AcademicManagementDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AcademicManagementDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AcademicManagementDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AcademicManagementDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AcademicManagementDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AcademicManagementDB] SET  MULTI_USER 
GO
ALTER DATABASE [AcademicManagementDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AcademicManagementDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AcademicManagementDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AcademicManagementDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AcademicManagementDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AcademicManagementDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AcademicManagementDB] SET QUERY_STORE = OFF
GO
USE [AcademicManagementDB]
GO
/****** Object:  User [Admin]    Script Date: 22/8/2022 12:27:48 p. m. ******/
CREATE USER [Admin] FOR LOGIN [AM_Admin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Admin]
GO
/****** Object:  Table [dbo].[Career]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Career]
(
	[Pk_Career] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[CareerName] [varchar](50) NOT NULL,
	[DegreeName] [varchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_Career] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CareerCourses]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CareerCourses]
(
	[Pk_CareerCourses] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Course] [int] NOT NULL,
	[Fk_Career] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Cycle] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_CareerCourses] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course]
(
	[Pk_Course] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Credits] [int] NOT NULL,
	[WeeklyHours] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_Course] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseGroups]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseGroups]
(
	[Pk_CourseGroups] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Course] [int] NOT NULL,
	[Fk_TGroup] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_CourseGroups] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cycle]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cycle]
(
	[Pk_Cycle] [int] IDENTITY(1,1) NOT NULL,
	[Fk_CycleState] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_Cycle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CycleState]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CycleState]
(
	[Pk_CycleState] [int] IDENTITY(1,1) NOT NULL,
	[StateDescription] [varchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_CycleState] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group]
(
	[Pk_Group] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Teacher] [int] NULL,
	[Fk_Cycle] [int] NULL,
	[Number] [varchar](50) NOT NULL,
	[Schedule] [varchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_Group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupStudents]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupStudents]
(
	[Pk_GroupStudents] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Group] [int] NOT NULL,
	[Fk_Student] [int] NOT NULL,
	[Score] [decimal](10, 0) NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_GroupStudents] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student]
(
	[Pk_Student] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Carreer] [int] NULL,
	[Fk_User] [int] NULL,
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_Student] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher]
(
	[Pk_Teacher] [int] IDENTITY(1,1) NOT NULL,
	[Fk_User] [int] NULL,
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	CONSTRAINT [PK__Teacher__AB4F2B2F71619BD2] PRIMARY KEY CLUSTERED 
(
	[Pk_Teacher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User]
(
	[Pk_User] [int] IDENTITY(1,1) NOT NULL,
	[Fk_UserType] [int] NOT NULL,
	[ID] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType]
(
	[Pk_UserType] [int] IDENTITY(1,1) NOT NULL,
	[TypeDescription] [varchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Pk_UserType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CycleState] ON

INSERT [dbo].[CycleState]
	([Pk_CycleState], [StateDescription])
VALUES
	(1, N'Active')
INSERT [dbo].[CycleState]
	([Pk_CycleState], [StateDescription])
VALUES
	(2, N'Inactive')
SET IDENTITY_INSERT [dbo].[CycleState] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON

INSERT [dbo].[UserType]
	([Pk_UserType], [TypeDescription])
VALUES
	(1, N'ADMINISTRATOR')
INSERT [dbo].[UserType]
	([Pk_UserType], [TypeDescription])
VALUES
	(3, N'TEACHER')
INSERT [dbo].[UserType]
	([Pk_UserType], [TypeDescription])
VALUES
	(4, N'STUDENT')
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
ALTER TABLE [dbo].[CareerCourses]  WITH CHECK ADD FOREIGN KEY([Fk_Career])
REFERENCES [dbo].[Career] ([Pk_Career])
GO
ALTER TABLE [dbo].[CareerCourses]  WITH CHECK ADD FOREIGN KEY([Fk_Course])
REFERENCES [dbo].[Course] ([Pk_Course])
GO
ALTER TABLE [dbo].[CourseGroups]  WITH CHECK ADD FOREIGN KEY([Fk_Course])
REFERENCES [dbo].[Course] ([Pk_Course])
GO
ALTER TABLE [dbo].[CourseGroups]  WITH CHECK ADD FOREIGN KEY([Fk_TGroup])
REFERENCES [dbo].[Group] ([Pk_Group])
GO
ALTER TABLE [dbo].[Cycle]  WITH CHECK ADD FOREIGN KEY([Fk_CycleState])
REFERENCES [dbo].[CycleState] ([Pk_CycleState])
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD FOREIGN KEY([Fk_Cycle])
REFERENCES [dbo].[Cycle] ([Pk_Cycle])
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK__TGroup__Fk_Teach__440B1D61] FOREIGN KEY([Fk_Teacher])
REFERENCES [dbo].[Teacher] ([Pk_Teacher])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK__TGroup__Fk_Teach__440B1D61]
GO
ALTER TABLE [dbo].[GroupStudents]  WITH CHECK ADD FOREIGN KEY([Fk_Group])
REFERENCES [dbo].[Group] ([Pk_Group])
GO
ALTER TABLE [dbo].[GroupStudents]  WITH CHECK ADD FOREIGN KEY([Fk_Student])
REFERENCES [dbo].[Student] ([Pk_Student])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([Fk_Carreer])
REFERENCES [dbo].[Career] ([Pk_Career])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([Fk_User])
REFERENCES [dbo].[User] ([Pk_User])
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK__Teacher__Fk_User__4222D4EF] FOREIGN KEY([Fk_User])
REFERENCES [dbo].[User] ([Pk_User])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK__Teacher__Fk_User__4222D4EF]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([Fk_UserType])
REFERENCES [dbo].[UserType] ([Pk_UserType])
GO
/****** Object:  StoredProcedure [dbo].[udpAddCourseToCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--INSERT COURSE AND THEN ADD IT TO CAREER
CREATE   PROCEDURE [dbo].[udpAddCourseToCareer](
	@Code AS VARCHAR(50),
	@Name AS VARCHAR(50),
	@Credits AS INT,
	@WeeklyHours AS INT,
	@Pk_Career AS INT,
	@Year AS INT,
	@Cycle AS INT
)
AS
BEGIN
	DECLARE @RowsAffected AS INT;
	DECLARE @Pk_CourseAdded AS INT;

	INSERT INTO [dbo].Course
	VALUES(@Code, @Name, @Credits, @WeeklyHours);

	SET @RowsAffected = @@ROWCOUNT;
	IF (@RowsAffected = 0)
		 THROW 51000, 'Course was not added', 1;  
	ELSE
		SET @Pk_CourseAdded = @@IDENTITY

	INSERT INTO [dbo].CareerCourses
	VALUES
		(@Pk_CourseAdded, @Pk_Career, @Year, @Cycle);
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpDeleteCareer](@Pk_Career AS INT)
AS
BEGIN
	DELETE 
        [dbo].Career
    WHERE
        Pk_Career = @Pk_Career;
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteCourse]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpDeleteCourse](@Pk_Course AS INT)
AS
BEGIN
	DELETE 
        [dbo].Course
    WHERE
        Pk_Course = @Pk_Course;
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteCourseFromCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--DELETE
CREATE   PROCEDURE [dbo].[udpDeleteCourseFromCareer](@Pk_CareerCourses AS INT)
AS
BEGIN
	DELETE [dbo].CareerCourses WHERE Pk_CareerCourses = @Pk_CareerCourses;
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteCycle]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--DELETE
CREATE   PROCEDURE [dbo].[udpDeleteCycle](@Pk_Cycle AS INT)
AS
BEGIN
	DELETE 
        [dbo].[Cycle]
    WHERE
        Pk_Cycle = @Pk_Cycle;
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteGroup]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--DELETE
CREATE   PROCEDURE [dbo].[udpDeleteGroup](@Pk_Group AS INT)
AS
BEGIN
	DELETE 
        [dbo].[Group]
    WHERE
        Pk_Group = @Pk_Group;
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteStudent]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--DELETE
CREATE   PROCEDURE [dbo].[udpDeleteStudent](@Pk_Student AS INT)
AS
BEGIN
	DELETE 
        [dbo].[Student]
    WHERE
        Pk_Student = @Pk_Student;
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteTeacher]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--DELETE
CREATE   PROCEDURE [dbo].[udpDeleteTeacher](@Pk_Teacher AS INT)
AS
BEGIN
	DELETE 
        [dbo].Teacher
    WHERE
        Pk_Teacher = @Pk_Teacher;
END
GO
/****** Object:  StoredProcedure [dbo].[udpDeleteUser]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--DELETE
CREATE   PROCEDURE [dbo].[udpDeleteUser](@Pk_User AS INT)
AS
BEGIN
	DELETE 
        [dbo].[User]
    WHERE
        Pk_User = @Pk_User;
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpFindCareer](@Pk_Career AS INT = NULL,
	@Code AS VARCHAR(50) = NULL,
	@CareerName as VARCHAR(50) = NULL,
	@DegreeName as VARCHAR(50) = NULL)
AS
BEGIN
	SELECT *
	FROM [dbo].Career
	WHERE   (@Pk_Career IS NULL OR Pk_Career = @Pk_Career)
		AND (@Code IS NULL OR Code = @Code)
		AND (@CareerName IS NULL OR CareerName = @CareerName)
		AND (@DegreeName IS NULL OR DegreeName = @DegreeName)
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindCareerCourse]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpFindCareerCourse](@Pk_CareerCourse AS INT)
AS
BEGIN
	SELECT *
	FROM [dbo].CareerCourses cc
		JOIN [dbo].Career c ON cc.Fk_Career = c.Pk_Career
		JOIN [dbo].Course c2 ON cc.Fk_Course = c2.Pk_Course
	WHERE Pk_CareerCourses = @Pk_CareerCourse
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindCourse]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpFindCourse](@Pk_Course AS INT = NULL,
	@Code AS VARCHAR(50) = NULL,
	@Name AS VARCHAR(50) = NULL,
	@Credits AS INT = NULL,
	@WeeklyHours AS INT = NULL)
AS
BEGIN
	SELECT *
	FROM [dbo].Course
	WHERE   (@Pk_Course IS NULL OR Pk_Course = @Pk_Course)
		AND (@Code IS NULL OR Code = @Code)
		AND (@Name IS NULL OR Name = @Name)
		AND (@Credits IS NULL OR Credits = @Credits)
		AND (@WeeklyHours IS NULL OR WeeklyHours = @WeeklyHours)
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindCoursesByCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpFindCoursesByCareer](@Pk_Career AS INT)
AS
BEGIN
	SELECT *
	FROM CareerCourses cc
		JOIN Course c ON  cc.Fk_Course = c.Pk_Course
	WHERE Fk_Career = @Pk_Career;
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindCycle]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SEARCH
CREATE   PROCEDURE [dbo].[udpFindCycle](
	@Pk_Cycle AS INT = NULL,
	@Fk_CycleState AS INT = NULL,
	@Year AS INT = NULL,
	@Number AS INT = NULL,
	@StartDate AS DATE = NULL,
	@EndDate AS DATE = NULL
)
AS
BEGIN
	SELECT *
	FROM [dbo].[Cycle] c
		JOIN [dbo].CycleState cs ON c.Fk_CycleState = cs.Pk_CycleState
	WHERE   (@Pk_Cycle IS NULL OR Pk_Cycle = @Pk_Cycle)
		AND (@Fk_CycleState IS NULL OR Fk_CycleState = @Fk_CycleState)
		AND (@Year IS NULL OR Year = @Year)
		AND (@Number IS NULL OR Number = @Number)
		AND (@StartDate IS NULL OR StartDate = @StartDate)
		AND (@EndDate IS NULL OR EndDate = @EndDate)
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindCycleState]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SEARCH
CREATE   PROCEDURE [dbo].[udpFindCycleState](@Pk_CycleState AS INT = NULL,
	@StateDescription AS VARCHAR(50) = NULL)
AS
BEGIN
	SELECT *
	FROM [dbo].CycleState cs
	WHERE   (@Pk_CycleState IS NULL OR Pk_CycleState = @Pk_CycleState)
		AND (@StateDescription IS NULL OR StateDescription = @StateDescription);
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindGroup]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SEARCH
CREATE   PROCEDURE [dbo].[udpFindGroup](
	@Pk_Group AS INT = NULL,
	@Fk_Teacher AS INT = NULL,
	@Fk_Cycle AS INT = NULL,
	@Number AS VARCHAR(50) = NULL,
	@Schedule AS VARCHAR(50) = NULL
)
AS
BEGIN
	SELECT *
	FROM [dbo].[Group] g
		LEFT JOIN [dbo].Teacher t ON g.Fk_Teacher = t.Pk_Teacher
		LEFT JOIN [dbo].[Cycle] c2 ON g.Fk_Cycle = c2.Pk_Cycle
		LEFT JOIN [DBO].CycleState cs ON c2.Fk_CycleState = cs.Pk_CycleState
	WHERE   (@Pk_Group IS NULL OR g.Pk_Group = @Pk_Group)
		AND (@Fk_Teacher IS NULL OR g.Fk_Teacher = @Fk_Teacher)
		AND (@Fk_Cycle IS NULL OR g.Fk_Cycle = @Fk_Cycle)
		AND (@Number IS NULL OR g.[Number] = @Number)
		AND (@Schedule IS NULL OR g.Schedule = @Schedule);
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindStudent]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SEARCH
CREATE   PROCEDURE [dbo].[udpFindStudent](
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
	SELECT *
	FROM [dbo].[Student] s
		LEFT JOIN [dbo].Career c ON s.Fk_Carreer = c.Pk_Career
	WHERE (@Pk_Student IS NULL OR s.Pk_Student = @Pk_Student)
		AND (@Fk_Career IS NULL OR s.Fk_Carreer  = @Fk_Career)
		AND (@Fk_User IS NULL OR s.Fk_User = @Fk_User)
		AND (@ID IS NULL OR s.ID = @ID)
		AND (@Name IS NULL OR s.Name = @Name)
		AND (@PhoneNumber IS NULL OR s.PhoneNumber = @PhoneNumber)
		AND (@Email IS NULL OR s.Email = @Email)
		AND (@DateOfBirth IS NULL OR s.DateOfBirth = @DateOfBirth);
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindTeacher]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SEARCH
CREATE   PROCEDURE [dbo].[udpFindTeacher](@Pk_Teacher AS INT = NULL,
	@ID AS VARCHAR(50) = NULL,
	@Name AS VARCHAR(50) = NULL,
	@PhoneNumber AS VARCHAR(50) = NULL,
	@Email AS VARCHAR(50) = NULL)
AS
BEGIN
	SELECT *
	FROM [dbo].Teacher t
	WHERE   (@Pk_Teacher IS NULL OR Pk_Teacher = @Pk_Teacher)
		AND (@ID IS NULL OR ID = @ID)
		AND (@Name IS NULL OR Name = @Name)
		AND (@PhoneNumber IS NULL OR PhoneNumber = @PhoneNumber)
		AND (@Email IS NULL OR Email = @Email)
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindUser]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SEARCH
CREATE   PROCEDURE [dbo].[udpFindUser](@Pk_User AS INT = NULL,
	@ID AS VARCHAR(50) = NULL,
	@Password AS VARCHAR(50) = NULL)
AS
BEGIN
	SELECT *
	FROM [dbo].[User] t
		JOIN [dbo].UserType ut ON t.Fk_UserType = ut.Pk_UserType
	WHERE   (@Pk_User IS NULL OR Pk_User = @Pk_User)
		AND (@ID IS NULL OR ID = @ID);
END
GO
/****** Object:  StoredProcedure [dbo].[udpFindUserType]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SEARCH
CREATE   PROCEDURE [dbo].[udpFindUserType](@Pk_UserType AS INT = NULL,
	@TypeDescription AS VARCHAR(50) = NULL)
AS
BEGIN
	SELECT *
	FROM [dbo].[UserType] t
	WHERE   (@Pk_UserType IS NULL OR Pk_UserType = @Pk_UserType)
		AND (@TypeDescription IS NULL OR TypeDescription = @TypeDescription);
END
GO
/****** Object:  StoredProcedure [dbo].[udpInsertCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpInsertCareer](@Code AS VARCHAR(50),
	@CareerName as VARCHAR(50),
	@DegreeName as VARCHAR(50))
AS
BEGIN
	INSERT INTO [dbo].Career
	VALUES
		(@Code, @CareerName, @DegreeName);
	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[udpInsertCourse]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpInsertCourse](@Code AS VARCHAR(50),
	@Name AS VARCHAR(50),
	@Credits AS INT,
	@WeeklyHours AS INT)
AS
BEGIN
	INSERT INTO [dbo].Course
	VALUES
		(@Code, @Name, @Credits, @WeeklyHours);
END
GO
/****** Object:  StoredProcedure [dbo].[udpInsertCycle]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--INSERT NEW CYCLE
CREATE   PROCEDURE [dbo].[udpInsertCycle](@Fk_CycleState AS INT,
	@Year AS INT,
	@Number AS INT,
	@StartDate AS DATE,
	@EndDate AS DATE)
AS
BEGIN
	INSERT INTO [dbo].[Cycle]
		(Fk_CycleState, [Year], [Number], StartDate, EndDate)
	VALUES(@Fk_CycleState, @Year, @Number, @StartDate, @EndDate);
END
GO
/****** Object:  StoredProcedure [dbo].[udpInsertGroup]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--INSERT NEW Group
CREATE   PROCEDURE [dbo].[udpInsertGroup](@Number AS VARCHAR(50),
	@Schedule AS VARCHAR(50))
AS
BEGIN
	INSERT INTO [dbo].[Group]
		([Number], Schedule)
	VALUES
		(@Number, @Schedule);
END
GO
/****** Object:  StoredProcedure [dbo].[udpInsertStudent]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--INSERT NEW Student
CREATE   PROCEDURE [dbo].[udpInsertStudent](@ID AS VARCHAR(50),
	@Name AS VARCHAR(50),
	@PhoneNumber AS VARCHAR(50),
	@Email AS VARCHAR(50),
	@DateOfBirth AS VARCHAR(50))
AS
BEGIN
	INSERT INTO [dbo].[Student]
		(ID, Name, PhoneNumber, Email, DateOfBirth)
	VALUES
		(@ID, @Name, @PhoneNumber, @Email, @DateOfBirth);
END
GO
/****** Object:  StoredProcedure [dbo].[udpInsertTeacher]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--INSERT
CREATE   PROCEDURE [dbo].[udpInsertTeacher](@ID AS INT,
	@Name as VARCHAR(50),
	@PhoneNumber as VARCHAR(50),
	@Email AS VARCHAR(50))
AS
BEGIN
	INSERT INTO [dbo].Teacher
		(ID, Name, PhoneNumber, Email)
	VALUES
		(@ID, @Name, @PhoneNumber, @Email);
END
GO
/****** Object:  StoredProcedure [dbo].[udpInsertUser]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--INSERT
CREATE   PROCEDURE [dbo].[udpInsertUser](
	@ID AS VARCHAR(50),
	@Password as VARCHAR(50),
	@Fk_UserType as INT)
AS
BEGIN
	INSERT INTO [dbo].[User]
	VALUES
		(@Fk_UserType, @ID, @Password);
END
GO
/****** Object:  StoredProcedure [dbo].[udpModifyCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[udpModifyCareer](@Pk_Career AS INT,
	@Code AS VARCHAR(50),
	@CareerName as VARCHAR(50),
	@DegreeName as VARCHAR(50))
AS
BEGIN
	UPDATE 
        [dbo].Career 
    SET 
        Code = @Code, 
        CareerName = @CareerName, 
        DegreeName = @DegreeName
    WHERE
        Pk_Career = @Pk_Career;
	SELECT SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[udpModifyCourse]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[udpModifyCourse](@Pk_Course AS INT,
	@Code AS VARCHAR(50),
	@Name AS VARCHAR(50),
	@Credits AS INT,
	@WeeklyHours AS INT)
AS
BEGIN
	UPDATE 
        [dbo].Course 
    SET 
        Code = @Code,
        Name = @Name,
        Credits = @Credits,
        WeeklyHours = @WeeklyHours
    WHERE
        Pk_Course = @Pk_Course;
END
GO
/****** Object:  StoredProcedure [dbo].[udpModifyCycle]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE
CREATE   PROCEDURE [dbo].[udpModifyCycle](
	@Pk_Cycle AS INT,
	@Fk_CycleState AS INT,
	@Year AS INT,
	@Number AS INT,
	@StartDate AS DATE,
	@EndDate AS DATE
)
AS
BEGIN

	UPDATE [dbo].[Cycle] 
	SET 
		Fk_CycleState = @Fk_CycleState,
		[Year]  = @Year,
		[Number] = @Number,
		StartDate = @StartDate,
		EndDate = @EndDate 
	WHERE Pk_Cycle  = @Pk_Cycle;

END
GO
/****** Object:  StoredProcedure [dbo].[udpModifyGroup]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE
--MODIFYTYPE => 1 = Number and Schedule, 2 = Teacher, 3 = Cycle
CREATE   PROCEDURE [dbo].[udpModifyGroup](
	@Pk_Group AS INT,
	@Fk_Teacher AS INT = NULL,
	@Fk_Cycle AS INT = NULL,
	@Number AS VARCHAR(50) = NULL,
	@Schedule AS VARCHAR(50) = NULL,
	@ModifyType AS INT
)
AS
BEGIN
	IF (@ModifyType = 1)
	BEGIN
		UPDATE [DBO].[Group] SET [Number] = @Number, Schedule = @Schedule WHERE Pk_Group = @Pk_Group;
	END
	IF(@ModifyType = 2)
	BEGIN
		UPDATE [dbo].[Group] 
		SET 
			Fk_Teacher = @Fk_Teacher
		WHERE Pk_Group  = @Pk_Group;
	END
	IF(@ModifyType = 3)
	BEGIN
		UPDATE [dbo].[Group] 
		SET 
			Fk_Cycle = @Fk_Cycle
		WHERE Pk_Group  = @Pk_Group;
	END
END
GO
/****** Object:  StoredProcedure [dbo].[udpModifyStudent]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE
--MODIFYTYPE => 1 = Other, 2 = Career, 3 = User
CREATE   PROCEDURE [dbo].[udpModifyStudent](
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
GO
/****** Object:  StoredProcedure [dbo].[udpModifyTeacher]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE
CREATE   PROCEDURE [dbo].[udpModifyTeacher](
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
GO
/****** Object:  StoredProcedure [dbo].[udpModifyUser]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE
CREATE   PROCEDURE [dbo].[udpModifyUser](
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
GO
/****** Object:  StoredProcedure [dbo].[udpUpdateCourseAndCareer]    Script Date: 22/8/2022 12:27:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE COURSE AND CAREER
CREATE   PROCEDURE [dbo].[udpUpdateCourseAndCareer](
	@Pk_CareerCourses AS INT,
	@Code AS VARCHAR(50),
	@Name AS VARCHAR(50),
	@Credits AS INT,
	@WeeklyHours AS INT,
	@Year AS INT,
	@Cycle AS INT
)
AS
BEGIN

	DECLARE @Pk_Course AS INT;

	SET	@Pk_Course = (SELECT Fk_Course
	FROM [dbo].CareerCourses cc
	WHERE cc.Pk_CareerCourses = @Pk_CareerCourses);

	UPDATE [dbo].Course SET Code = @Code, Name = @Name, Credits = @Credits, WeeklyHours = @WeeklyHours WHERE Pk_Course = @Pk_Course;
	IF @@ROWCOUNT = 0
		THROW 51000, 'Course was not updated', 1;  
	ELSE
		UPDATE [dbo].CareerCourses SET Year = @Year, Cycle = @Cycle WHERE Pk_CareerCourses = @Pk_CareerCourses;
END
GO
USE [master]
GO
ALTER DATABASE [AcademicManagementDB] SET  READ_WRITE 
GO
