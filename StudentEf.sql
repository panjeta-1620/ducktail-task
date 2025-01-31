USE [StudentEf]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 9/24/2020 12:00:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[ClassID] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSubject]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubject](
	[StudentSubjectID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
 CONSTRAINT [PK_StudentSubject] PRIMARY KEY CLUSTERED 
(
	[StudentSubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSubjectMarks]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubjectMarks](
	[SubjectStudentMarksID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
	[SubjectMarks] [float] NOT NULL,
 CONSTRAINT [PK_StudentSubjectMarks] PRIMARY KEY CLUSTERED 
(
	[SubjectStudentMarksID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [varchar](50) NOT NULL,
	[ClassID] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([ClassID], [ClassName]) VALUES (1, N'10')
INSERT [dbo].[Class] ([ClassID], [ClassName]) VALUES (2, N'12')
SET IDENTITY_INSERT [dbo].[Class] OFF
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentID], [FirstName], [LastName], [ClassID]) VALUES (1, N'Amrit', N'Kaur', 2)
INSERT [dbo].[Student] ([StudentID], [FirstName], [LastName], [ClassID]) VALUES (2, N'Amit', N'Kumar', 1)
SET IDENTITY_INSERT [dbo].[Student] OFF
SET IDENTITY_INSERT [dbo].[StudentSubject] ON 

INSERT [dbo].[StudentSubject] ([StudentSubjectID], [StudentID], [SubjectID]) VALUES (1, 1, 3)
INSERT [dbo].[StudentSubject] ([StudentSubjectID], [StudentID], [SubjectID]) VALUES (2, 1, 4)
INSERT [dbo].[StudentSubject] ([StudentSubjectID], [StudentID], [SubjectID]) VALUES (3, 2, 1)
SET IDENTITY_INSERT [dbo].[StudentSubject] OFF
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [ClassID]) VALUES (1, N'Maths', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [ClassID]) VALUES (2, N'English', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [ClassID]) VALUES (3, N'Chemistry', 2)
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [ClassID]) VALUES (4, N'Physics', 2)
SET IDENTITY_INSERT [dbo].[Subject] OFF
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Class]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Student]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubject_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [FK_StudentSubject_Student]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubject_StudentSubject] FOREIGN KEY([StudentSubjectID])
REFERENCES [dbo].[StudentSubject] ([StudentSubjectID])
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [FK_StudentSubject_StudentSubject]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubject_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [FK_StudentSubject_Subject]
GO
ALTER TABLE [dbo].[StudentSubjectMarks]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubjectMarks_StudentSubjectMarks] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[StudentSubjectMarks] CHECK CONSTRAINT [FK_StudentSubjectMarks_StudentSubjectMarks]
GO
ALTER TABLE [dbo].[StudentSubjectMarks]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubjectMarks_StudentSubjectMarks1] FOREIGN KEY([SubjectStudentMarksID])
REFERENCES [dbo].[StudentSubjectMarks] ([SubjectStudentMarksID])
GO
ALTER TABLE [dbo].[StudentSubjectMarks] CHECK CONSTRAINT [FK_StudentSubjectMarks_StudentSubjectMarks1]
GO
ALTER TABLE [dbo].[StudentSubjectMarks]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubjectMarks_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[StudentSubjectMarks] CHECK CONSTRAINT [FK_StudentSubjectMarks_Subject]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Class]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Subject]
GO
/****** Object:  StoredProcedure [dbo].[spInsertStudentData]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertStudentData]
@FirstName varchar(50),
@LastName varchar(50),
@ClassID varchar(50),
@StudentID INT OUT
AS
BEGIN         
      	INSERT INTO Student(FirstName,LastName,ClassID)
			    VALUES (@FirstName,@LastName,@ClassID)
                SELECT @StudentID=SCOPE_IDENTITY() 

End
GO
/****** Object:  StoredProcedure [dbo].[spSearchStudent]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSearchStudent]

@Keyword varchar(50)
--@pageIndex INT = 1,
--@PageSize INT = 10,
--@totalCount INT OUTPUT
AS
BEGIN
SELECT t.StudentID, t.FirstName,t.LastName,t.ClassName,t.SubjectName FROM
(

SELECT s.StudentID, s.FirstName,s.LastName,c.ClassName,
		
          SubjectName= STUFF 
          (   (
            SELECT ',' + SubjectName
            FROM Subject subj
			join StudentSubject stu on  subj.SubjectID=stu.SubjectID   			
			     where stu.StudentID=s.StudentID      
            FOR XML PATH(''), type
        ).value('.', 'varchar(max)'), 1, 1, ''), ROW_NUMBER() OVER(ORDER BY s.StudentID) AS RowIndex
		FROM Student s
		join Class c on s.ClassID=c.ClassID
		) t
	 WHERE( @keyword IS NULL OR t.FirstName  LIKE '%' + @keyword + '%'
							  OR t.LastName LIKE '%' + @keyword + '%'
                            OR t.ClassName LIKE '%' + @keyword + '%'
                            OR t.StudentID LIKE '%' + @keyword + '%'
							OR t.StudentID LIKE '%' + @keyword + '%'
                            OR t.SubjectName LIKE '%' + @keyword + '%'
                        )
	 --WHERE t.RowIndex BETWEEN (((@pageIndex - 1) * @PageSize) + 1)  AND (@pageIndex * @PageSize)
	 --SET @totalCount = (
		-- SELECT COUNT(*)
		-- FROM Student s	 
--)

END
GO
/****** Object:  StoredProcedure [dbo].[spShowStudentData]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spShowStudentData]
--@pageIndex INT = 1,
--@PageSize INT = 10,
--@totalCount INT OUTPUT
AS
BEGIN
SELECT t.StudentID, t.FirstName,t.LastName,t.ClassName,t.SubjectName FROM
(

SELECT s.StudentID, s.FirstName,s.LastName,c.ClassName,
		
          SubjectName= STUFF 
          (   (
            SELECT ',' + SubjectName
            FROM Subject subj
			join StudentSubject stu on  subj.SubjectID=stu.SubjectID   			
			     where stu.StudentID=s.StudentID      
            FOR XML PATH(''), type
        ).value('.', 'varchar(max)'), 1, 1, ''), ROW_NUMBER() OVER(ORDER BY s.StudentID) AS RowIndex
		FROM Student s
		join Class c on s.ClassID=c.ClassID
		) t
	
	 --WHERE t.RowIndex BETWEEN (((@pageIndex - 1) * @PageSize) + 1)  AND (@pageIndex * @PageSize)
	 --SET @totalCount = (
		-- SELECT COUNT(*)
		-- FROM Student s	 
--)

END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStudent]    Script Date: 9/24/2020 12:00:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdateStudent]

@FirstName varchar(50),
@LastName varchar(50),
@ClassID varchar(50),
@StudentID INT

AS
BEGIN
update Student set FirstName=@FirstName,LastName=@LastName,ClassID=@ClassID
WHERE StudentID=@StudentID
                     
END
GO
