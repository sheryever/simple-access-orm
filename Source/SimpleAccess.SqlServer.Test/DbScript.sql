USE [master]
GO
/****** Object:  Database [SimpleAccessTest]    Script Date: 8/13/2022 8:49:19 PM ******/
CREATE DATABASE [SimpleAccessTest]
 CONTAINMENT = NONE

GO
ALTER DATABASE [SimpleAccessTest] SET COMPATIBILITY_LEVEL = 120
GO

ALTER DATABASE [SimpleAccessTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SimpleAccessTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleAccessTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimpleAccessTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SimpleAccessTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimpleAccessTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SimpleAccessTest] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleAccessTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleAccessTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SimpleAccessTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SimpleAccessTest] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SimpleAccessTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SimpleAccessTest] SET QUERY_STORE = OFF
GO
USE [SimpleAccessTest]
GO
USE [SimpleAccessTest]
GO
/****** Object:  Sequence [dbo].[Seq_Attachments]    Script Date: 8/13/2022 8:49:19 PM ******/
CREATE SEQUENCE [dbo].[Seq_Attachments] 
 AS [int]
 START WITH 6
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 2147483647
 CACHE 
GO
USE [SimpleAccessTest]
GO
/****** Object:  Sequence [dbo].[Seq_Branches]    Script Date: 8/13/2022 8:49:19 PM ******/
CREATE SEQUENCE [dbo].[Seq_Branches] 
 AS [int]
 START WITH 6
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 2147483647
 CACHE 
GO
USE [SimpleAccessTest]
GO
/****** Object:  Sequence [dbo].[Seq_Categories]    Script Date: 8/13/2022 8:49:19 PM ******/
CREATE SEQUENCE [dbo].[Seq_Categories] 
 AS [int]
 START WITH 6
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 2147483647
 CACHE 
GO
USE [SimpleAccessTest]
GO
/****** Object:  Sequence [dbo].[Seq_Employees]    Script Date: 8/13/2022 8:49:19 PM ******/
CREATE SEQUENCE [dbo].[Seq_Employees] 
 AS [bigint]
 START WITH 17
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [SimpleAccessTest]
GO
/****** Object:  Sequence [dbo].[Seq_Jobs]    Script Date: 8/13/2022 8:49:19 PM ******/
CREATE SEQUENCE [dbo].[Seq_Jobs] 
 AS [int]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 2147483647
 NO CACHE 
GO
USE [SimpleAccessTest]
GO
/****** Object:  Sequence [dbo].[Seq_People]    Script Date: 8/13/2022 8:49:19 PM ******/
CREATE SEQUENCE [dbo].[Seq_People] 
 AS [int]
 START WITH 6
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 2147483647
 NO CACHE 
GO
/****** Object:  UserDefinedFunction [dbo].[OnDebugMode]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[OnDebugMode] ()
RETURNS int
AS
BEGIN

	RETURN 1

END
GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwProducts]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwProducts]
AS
SELECT        p.Id, p.Name, p.CategoryId, c.Name AS CategoryName, p.IsActive, p.ProductType, p.PricePerUnit, p.PricePerPackage, p.UnitPerPackage, p.AvailableTill, p.LastPurchase
FROM            dbo.Products AS p INNER JOIN
                         dbo.Categories AS c ON p.CategoryId = c.Id
GO
/****** Object:  Table [dbo].[__SqlLog]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__SqlLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EntryTime] [smalldatetime] NOT NULL,
	[SqlQuery] [nvarchar](4000) NOT NULL,
	[CallerName] [nvarchar](500) NOT NULL,
	[LineLocation] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK___SqlLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachments]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments](
	[Id] [int] NOT NULL,
	[IncidentId] [int] NOT NULL,
	[OtherName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachments2]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments2](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IncidentId] [int] NOT NULL,
	[OtherName] [nvarchar](50) NOT NULL,
	[ShipDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Attachments2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[Id] [int] NOT NULL,
	[CityId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[PhoneNumbers] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches2]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches2](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[PhoneNumbers] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
 CONSTRAINT [PK_Branches2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeJobs]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeJobs](
	[EmployeeId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeJobs] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](40) NOT NULL,
	[Address] [nvarchar](600) NULL,
	[BasicSalary] [decimal](10, 2) NOT NULL,
	[Transport] [int] NULL,
	[Inssurance] [decimal](10, 2) NULL,
	[IsOnDuty] [bit] NOT NULL,
	[Department] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[Id] [int] NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Gender] [smallint] NULL,
	[DOB] [smalldatetime] NOT NULL,
	[Phone] [nvarchar](40) NOT NULL,
	[Address] [nvarchar](600) NULL,
	[BasicSalary] [decimal](10, 2) NOT NULL,
	[Transport] [int] NULL,
	[Alive] [bit] NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Attachments_Delete]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Attachments_Delete
-------------------------
CREATE PROCEDURE [dbo].[Attachments_Delete]
	@id INT
AS
BEGIN

    DELETE FROM dbo.[Attachments]
    WHERE
		[Id] = @Id
END
-------------------------
--END of Attachments_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Attachments_Find]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-------------------------
--- Start of Attachments_Find
-------------------------
 CREATE PROCEDURE  [dbo].[Attachments_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [Id] ' +
         ' , [IncidentId] ' +
         ' , [OtherName] ' +
         'FROM dbo.Attachments ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'Attachments_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of Attachments_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Attachments_GetAll]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 
 
-------------------------
--- Start of Attachments_GetAll
-------------------------
CREATE PROCEDURE [dbo].[Attachments_GetAll]
AS
BEGIN
    SELECT [Id]
         , [IncidentId]
         , [OtherName]
           
		FROM dbo.[Attachments]  
END
-------------------------
--END of Attachments_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Attachments_GetById]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Attachments_GetById
-------------------------
CREATE PROCEDURE [dbo].[Attachments_GetById]
	@id INT
AS
BEGIN
    SELECT  [Id]
         , [IncidentId]
         , [OtherName]
         FROM dbo.Attachments  
		 WHERE [Id] = @Id
END
-------------------------
--END of Attachments_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Attachments_GetPagedList]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Attachments_GetPagedList
-------------------------
CREATE PROCEDURE [dbo].[Attachments_GetPagedList]
	@startIndex INT
    , @pageSize INT
    , @sortExpression VARCHAR(255)
    , @totalRows BIGINT OUTPUT
    WITH EXEC AS CALLER
AS 
BEGIN
    DECLARE @sql NVARCHAR(4000);
	DECLARE @whereClause NVARCHAR(3000) = ' WHERE 1 = 1';   
    BEGIN 

    SET @sql = 'SELECT @totalRows = COUNT(*) From [dbo].[Attachments] '
        + @whereClause;

    EXEC sp_executesql @sql, N'@totalRows INT OUTPUT', @totalRows OUTPUT;
  
SET @sql = 
'SELECT ' + ' [Id] ' +
         ' , [IncidentId] ' +
         ' , [OtherName] ' +
             ' FROM ( ' +
    ' SELECT * ' +
       ' , ROW_NUMBER() OVER ( ORDER BY ' + @sortExpression + ' ) AS [RowNumber] ' +
        ' FROM [dbo].[Attachments] ' +
         @whereClause +
    ' ) AS pagedList ' +
     ' WHERE RowNumber BETWEEN ('+ CONVERT(varchar(10), @StartIndex + 1) + ') AND ( ' + 
  CONVERT(varchar(10), @StartIndex + @PageSize) + ');';
  
        IF ( dbo.OnDebugMode() = 1 ) 
            BEGIN
                INSERT  INTO [dbo].[__SqlLog]
                        ( [EntryTime] ,
                          [SqlQuery] ,
                          [CallerName] ,
                          [LineLocation]
                        )
                VALUES  ( GETDATE() ,
                          @sql ,
                          'Attachments_GetPagedList' ,
                          'before executing query'
                        );
            END

  --SET @sqlToReturn = @sql;
        EXEC sp_executesql @sql;
    END;

END
-------------------------
--END of Attachments_GetPagedList
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Attachments_Insert]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Attachments_Insert
-------------------------
CREATE PROCEDURE [dbo].[Attachments_Insert]
	 @incidentId INT
	 , @otherName NVARCHAR(50)
	,@Id INT OUTPUT    
    
AS
BEGIN
		
	SET XACT_ABORT ON
		
	SET @id = NEXT VALUE FOR [dbo].[Seq_Attachments];
		
    INSERT INTO dbo.[Attachments]
    (
        [Id]
		 , [IncidentId]
		 , [OtherName]

    ) VALUES (
        @id
		 , @incidentId
		 , @otherName
	 
	)


END
-------------------------
--END of Attachments_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Attachments_Update]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Attachments_Update
-------------------------

CREATE PROCEDURE [dbo].[Attachments_Update]
	    @id INT
	 , @incidentId INT
	 , @otherName NVARCHAR(50)
	
        AS 
		BEGIN
		
		SET XACT_ABORT ON
		
        UPDATE dbo.[Attachments] SET
            [IncidentId] = @incidentId
          , [OtherName] = @otherName
      WHERE [Id] = @id 


END
-------------------------
--END of Attachments_Update
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Branches_Delete]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Branches_Delete
-------------------------
CREATE PROCEDURE [dbo].[Branches_Delete]
	@id INT
AS
BEGIN

    DELETE FROM dbo.[Branches]
    WHERE
		[Id] = @Id
END
-------------------------
--END of Branches_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Branches_Find]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-------------------------
--- Start of Branches_Find
-------------------------
 CREATE PROCEDURE  [dbo].[Branches_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [Id] ' +
         ' , [CityId] ' +
         ' , [Name] ' +
         ' , [PhoneNumbers] ' +
         ' , [Address] ' +
         ' , [Address2] ' +
         'FROM dbo.Branches ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'Branches_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of Branches_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Branches_GetAll]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 
 
-------------------------
--- Start of Branches_GetAll
-------------------------
CREATE PROCEDURE [dbo].[Branches_GetAll]
AS
BEGIN
    SELECT [Id]
         , [CityId]
         , [Name]
         , [PhoneNumbers]
         , [Address]
         , [Address2]
           
		FROM dbo.[Branches]  
END
-------------------------
--END of Branches_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Branches_GetById]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Branches_GetById
-------------------------
CREATE PROCEDURE [dbo].[Branches_GetById]
	@id INT
AS
BEGIN
    SELECT  [Id]
         , [CityId]
         , [Name]
         , [PhoneNumbers]
         , [Address]
         , [Address2]
         FROM dbo.Branches  
		 WHERE [Id] = @Id
END
-------------------------
--END of Branches_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Branches_GetPagedList]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Branches_GetPagedList
-------------------------
CREATE PROCEDURE [dbo].[Branches_GetPagedList]
    @name           NVARCHAR(MAX)
    , @startIndex   INT    
    , @pageSize INT
    , @sortExpression VARCHAR(255)
    , @totalRows BIGINT OUTPUT
    WITH EXEC AS CALLER
AS 
BEGIN
    DECLARE @sql NVARCHAR(4000);
	DECLARE @whereClause NVARCHAR(3000) = ' WHERE 1 = 1';   
    BEGIN 

    IF @name IS NOT NULL AND @name != ''
    BEGIN
        SET @whereClause += ' AND Name LIKE ''%' + @name + '%''';
    END;

    SET @sql = 'SELECT @totalRows = COUNT(*) From [dbo].[Branches] '
        + @whereClause;

    EXEC sp_executesql @sql, N'@totalRows INT OUTPUT', @totalRows OUTPUT;
  
SET @sql = 
'SELECT ' + ' [Id] ' +
         ' , [CityId] ' +
         ' , [Name] ' +
         ' , [PhoneNumbers] ' +
         ' , [Address] ' +
         ' , [Address2] ' +
             ' FROM ( ' +
    ' SELECT * ' +
       ' , ROW_NUMBER() OVER ( ORDER BY ' + @sortExpression + ' ) AS [RowNumber] ' +
        ' FROM [dbo].[Branches] ' +
         @whereClause +
    ' ) AS pagedList ' +
     ' WHERE RowNumber BETWEEN ('+ CONVERT(varchar(10), @StartIndex + 1) + ') AND ( ' + 
  CONVERT(varchar(10), @StartIndex + @PageSize) + ');';
  
        IF ( dbo.OnDebugMode() = 1 ) 
            BEGIN
                INSERT  INTO [dbo].[__SqlLog]
                        ( [EntryTime] ,
                          [SqlQuery] ,
                          [CallerName] ,
                          [LineLocation]
                        )
                VALUES  ( GETDATE() ,
                          @sql ,
                          'Branches_GetPagedList' ,
                          'before executing query'
                        );
            END

  --SET @sqlToReturn = @sql;
        EXEC sp_executesql @sql;
    END;

END
-------------------------
--END of Branches_GetPagedList
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Branches_Insert]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Branches_Insert
-------------------------
CREATE PROCEDURE [dbo].[Branches_Insert]
		 @cityId INT
	 , @name NVARCHAR(50)
	 , @phoneNumbers VARCHAR(50)
	 , @address VARCHAR(50)
	 , @address2 VARCHAR(50)
	,@Id INT OUTPUT    
    
AS
BEGIN
		
	SET XACT_ABORT ON
		
	SET @id = NEXT VALUE FOR [dbo].[Seq_Branches];
		
    INSERT INTO dbo.[Branches]
    (
        [Id]
		 , [CityId]
		 , [Name]
		 , [PhoneNumbers]
		 , [Address]
		 , [Address2]

    ) VALUES (
        @id
		 , @cityId
		 , @name
		 , @phoneNumbers
		 , @address
		 , @address2
	 
	)


END
-------------------------
--END of Branches_Insert
-------------------------
GO
/****** Object:  StoredProcedure [dbo].[Branches_LookupItems]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of Branches_LookupItems
-------------------------

CREATE PROCEDURE [dbo].[Branches_LookupItems]
AS
BEGIN
	SELECT [Id], [Name] 
		FROM dbo.[Branches]
		ORDER BY [Name];
END
-------------------------
--END of Branches_LookupItems
-------------------------
GO
/****** Object:  StoredProcedure [dbo].[Branches_Update]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Branches_Update
-------------------------

CREATE PROCEDURE [dbo].[Branches_Update]
	    @id INT
	 , @cityId INT
	 , @name NVARCHAR(50)
	 , @phoneNumbers VARCHAR(50)
	 , @address VARCHAR(50)
	 , @address2 VARCHAR(50)
	
        AS 
		BEGIN
		
		SET XACT_ABORT ON
		
        UPDATE dbo.[Branches] SET
            [CityId] = @cityId
          , [Name] = @name
          , [PhoneNumbers] = @phoneNumbers
          , [Address] = @address
          , [Address2] = @address2
      WHERE [Id] = @id 


END
-------------------------
--END of Branches_Update
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Categories_Delete]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Categories_Delete
-------------------------
CREATE PROCEDURE [dbo].[Categories_Delete]
	@id INT
AS
BEGIN

    DELETE FROM dbo.[Categories]
    WHERE
		[Id] = @Id
END
-------------------------
--END of Categories_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Categories_Find]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-------------------------
--- Start of Categories_Find
-------------------------
 CREATE PROCEDURE  [dbo].[Categories_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [Id] ' +
         ' , [Name] ' +
         ' , [Description] ' +
         'FROM dbo.Categories ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'Categories_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of Categories_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Categories_GetAll]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 
 
-------------------------
--- Start of Categories_GetAll
-------------------------
CREATE PROCEDURE [dbo].[Categories_GetAll]
AS
BEGIN
    SELECT [Id]
         , [Name]
         , [Description]
           
		FROM dbo.[Categories]  
END
-------------------------
--END of Categories_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Categories_GetById]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Categories_GetById
-------------------------
CREATE PROCEDURE [dbo].[Categories_GetById]
	@id INT
AS
BEGIN
    SELECT  [Id]
         , [Name]
         , [Description]
         FROM dbo.Categories  
		 WHERE [Id] = @Id
END
-------------------------
--END of Categories_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Categories_GetPagedList]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Categories_GetPagedList
-------------------------
CREATE PROCEDURE [dbo].[Categories_GetPagedList]
    @name           NVARCHAR(MAX)
    , @startIndex   INT    
    , @pageSize INT
    , @sortExpression VARCHAR(255)
    , @totalRows BIGINT OUTPUT
    WITH EXEC AS CALLER
AS 
BEGIN
    DECLARE @sql NVARCHAR(4000);
	DECLARE @whereClause NVARCHAR(3000) = ' WHERE 1 = 1';   
    BEGIN 

    IF @name IS NOT NULL AND @name != ''
    BEGIN
        SET @whereClause += ' AND Name LIKE ''%' + @name + '%''';
    END;

    SET @sql = 'SELECT @totalRows = COUNT(*) From [dbo].[Categories] '
        + @whereClause;

    EXEC sp_executesql @sql, N'@totalRows INT OUTPUT', @totalRows OUTPUT;
  
SET @sql = 
'SELECT ' + ' [Id] ' +
         ' , [Name] ' +
         ' , [Description] ' +
             ' FROM ( ' +
    ' SELECT * ' +
       ' , ROW_NUMBER() OVER ( ORDER BY ' + @sortExpression + ' ) AS [RowNumber] ' +
        ' FROM [dbo].[Categories] ' +
         @whereClause +
    ' ) AS pagedList ' +
     ' WHERE RowNumber BETWEEN ('+ CONVERT(varchar(10), @StartIndex + 1) + ') AND ( ' + 
  CONVERT(varchar(10), @StartIndex + @PageSize) + ');';
  
        IF ( dbo.OnDebugMode() = 1 ) 
            BEGIN
                INSERT  INTO [dbo].[__SqlLog]
                        ( [EntryTime] ,
                          [SqlQuery] ,
                          [CallerName] ,
                          [LineLocation]
                        )
                VALUES  ( GETDATE() ,
                          @sql ,
                          'Categories_GetPagedList' ,
                          'before executing query'
                        );
            END

  --SET @sqlToReturn = @sql;
        EXEC sp_executesql @sql;
    END;

END
-------------------------
--END of Categories_GetPagedList
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Categories_Insert]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Categories_Insert
-------------------------
CREATE PROCEDURE [dbo].[Categories_Insert]
	 @name NVARCHAR(50)
	 , @description NVARCHAR(500)
	,@Id INT OUTPUT    
    
AS
BEGIN
		
	SET XACT_ABORT ON
		
	SET @id = NEXT VALUE FOR [dbo].[Seq_Categories];
		
    INSERT INTO dbo.[Categories]
    (
        [Id]
		 , [Name]
		 , [Description]

    ) VALUES (
        @id
		 , @name
		 , @description
	 
	)


END
-------------------------
--END of Categories_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[Categories_LookupItems]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------
--- Start of Categories_LookupItems
-------------------------

CREATE PROCEDURE [dbo].[Categories_LookupItems]
AS
BEGIN
	SELECT [Id], [Name] 
		FROM dbo.[Categories]
		ORDER BY [Name];
END
-------------------------
--END of Categories_LookupItems
-------------------------
GO
/****** Object:  StoredProcedure [dbo].[Categories_Update]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of Categories_Update
-------------------------

CREATE PROCEDURE [dbo].[Categories_Update]
	    @id INT
	 , @name NVARCHAR(50)
	 , @description NVARCHAR(500)
	
        AS 
		BEGIN
		
		SET XACT_ABORT ON
		
        UPDATE dbo.[Categories] SET
            [Name] = @name
          , [Description] = @description
      WHERE [Id] = @id 


END
-------------------------
--END of Categories_Update
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[People_Delete]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of People_Delete
-------------------------
CREATE PROCEDURE [dbo].[People_Delete]
	@id INT
AS
BEGIN

    DELETE FROM dbo.[People]
    WHERE
		[Id] = @Id
END
-------------------------
--END of People_Delete
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[People_Find]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-------------------------
--- Start of People_Find
-------------------------
 CREATE PROCEDURE  [dbo].[People_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql = 
		'SELECT ' + ' [Id] ' +
         ' , [FullName] ' +
         ' , [Phone] ' +
         'FROM dbo.People ' +
	ISNULL(@whereClause, '');

	IF ( dbo.OnDebugMode() = 1 ) 
	BEGIN
		INSERT  INTO [dbo].[__SqlLog]
				( [EntryTime] ,
				  [SqlQuery] ,
				  [CallerName] ,
				  [LineLocation]
				)
		VALUES  ( GETDATE() ,
				  @sql ,
				  'People_Find' ,
				  'before executing query'
				);
	END		
		
     EXEC sp_executesql @sql;
END
-------------------------
--END of People_Find
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[People_GetAll]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 
 
-------------------------
--- Start of People_GetAll
-------------------------
CREATE PROCEDURE [dbo].[People_GetAll]
AS
BEGIN
    SELECT [Id]
         , [FullName]
         , [Phone]
           
		FROM dbo.[People]  
END
-------------------------
--END of People_GetAll
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[People_GetById]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of People_GetById
-------------------------
CREATE PROCEDURE [dbo].[People_GetById]
	@id INT
AS
BEGIN
    SELECT  [Id]
         , [FullName]
         , [Phone]
         FROM dbo.People  
		 WHERE [Id] = @Id
END
-------------------------
--END of People_GetById
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[People_GetPagedList]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of People_GetPagedList
-------------------------
CREATE PROCEDURE [dbo].[People_GetPagedList]
	@startIndex INT
    , @pageSize INT
    , @sortExpression VARCHAR(255)
    , @totalRows BIGINT OUTPUT
    WITH EXEC AS CALLER
AS 
BEGIN
    DECLARE @sql NVARCHAR(4000);
	DECLARE @whereClause NVARCHAR(3000) = ' WHERE 1 = 1';   
    BEGIN 

    SET @sql = 'SELECT @totalRows = COUNT(*) From [dbo].[People] '
        + @whereClause;

    EXEC sp_executesql @sql, N'@totalRows INT OUTPUT', @totalRows OUTPUT;
  
SET @sql = 
'SELECT ' + ' [Id] ' +
         ' , [FullName] ' +
         ' , [Phone] ' +
             ' FROM ( ' +
    ' SELECT * ' +
       ' , ROW_NUMBER() OVER ( ORDER BY ' + @sortExpression + ' ) AS [RowNumber] ' +
        ' FROM [dbo].[People] ' +
         @whereClause +
    ' ) AS pagedList ' +
     ' WHERE RowNumber BETWEEN ('+ CONVERT(varchar(10), @StartIndex + 1) + ') AND ( ' + 
  CONVERT(varchar(10), @StartIndex + @PageSize) + ');';
  
        IF ( dbo.OnDebugMode() = 1 ) 
            BEGIN
                INSERT  INTO [dbo].[__SqlLog]
                        ( [EntryTime] ,
                          [SqlQuery] ,
                          [CallerName] ,
                          [LineLocation]
                        )
                VALUES  ( GETDATE() ,
                          @sql ,
                          'People_GetPagedList' ,
                          'before executing query'
                        );
            END

  --SET @sqlToReturn = @sql;
        EXEC sp_executesql @sql;
    END;

END
-------------------------
--END of People_GetPagedList
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[People_Insert]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of People_Insert
-------------------------
CREATE PROCEDURE [dbo].[People_Insert]
	 @fullName NVARCHAR(200)
	 , @gender smallint
	 , @phone NVARCHAR(40)
	 , @address nvarchar(600)
	 , @basicSalary decimal(10, 2)
	 , @transport int
	 , @alive	bit
	,@Id INT OUTPUT    
    
AS
BEGIN
		
	SET XACT_ABORT ON
		
	SET @id = NEXT VALUE FOR [dbo].[Seq_People];
		
    INSERT INTO dbo.[People]
    (
        [Id]
		 , [FullName]
		 , Gender
		 , [Phone]
		 , Address
		 , BasicSalary
		 , Transport
		 , Alive

    ) VALUES (
        @id
		 , @fullName
		 , @gender
		 , @phone
		 , @address
		 , @basicSalary
		 , @transport
		 , @alive
	 
	)


END
-------------------------
--END of People_Insert
-------------------------

GO
/****** Object:  StoredProcedure [dbo].[People_Update]    Script Date: 8/13/2022 8:49:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------------------
--- Start of People_Update
-------------------------

CREATE PROCEDURE [dbo].[People_Update]
	    @id INT
	 , @fullName NVARCHAR(200)
	 , @gender smallint
	 , @phone NVARCHAR(40)
	 , @address nvarchar(600)
	 , @basicSalary decimal(10, 2)
	 , @transport int
	 , @alive	bit
	
        AS 
		BEGIN
		
		SET XACT_ABORT ON
		
        UPDATE dbo.[People] SET
            [FullName] = @fullName
		  , Gender = @gender
          , [Phone] = @phone
		  , Address = @address
		  , BasicSalary = @basicSalary
		  , Transport = @transport
		  , Alive = @alive

      WHERE [Id] = @id 


END
-------------------------
--END of People_Update
-------------------------

ALTER DATABASE [SimpleAccessTest] SET  READ_WRITE 
GO
