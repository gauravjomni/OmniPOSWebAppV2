USE [master]
GO
/****** Object:  Database [OmniAdmin]    Script Date: 03/09/2014 04:05:38 ******/
CREATE DATABASE [OmniAdmin] ON  PRIMARY 
( NAME = N'OmniAdmin', FILENAME = N'D:\Omni\db\OmniAdmin.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OmniAdmin_log', FILENAME = N'D:\Omni\db\OmniAdmin_1.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OmniAdmin] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OmniAdmin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OmniAdmin] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [OmniAdmin] SET ANSI_NULLS OFF
GO
ALTER DATABASE [OmniAdmin] SET ANSI_PADDING OFF
GO
ALTER DATABASE [OmniAdmin] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [OmniAdmin] SET ARITHABORT OFF
GO
ALTER DATABASE [OmniAdmin] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [OmniAdmin] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [OmniAdmin] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [OmniAdmin] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [OmniAdmin] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [OmniAdmin] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [OmniAdmin] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [OmniAdmin] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [OmniAdmin] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [OmniAdmin] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [OmniAdmin] SET  DISABLE_BROKER
GO
ALTER DATABASE [OmniAdmin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [OmniAdmin] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [OmniAdmin] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [OmniAdmin] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [OmniAdmin] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [OmniAdmin] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [OmniAdmin] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [OmniAdmin] SET  READ_WRITE
GO
ALTER DATABASE [OmniAdmin] SET RECOVERY FULL
GO
ALTER DATABASE [OmniAdmin] SET  MULTI_USER
GO
ALTER DATABASE [OmniAdmin] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [OmniAdmin] SET DB_CHAINING OFF
GO
USE [OmniAdmin]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 03/09/2014 04:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Login] ON
INSERT [dbo].[Login] ([Id], [UserId], [Password], [CreatedDate], [UpdatedDate], [Active]) VALUES (2, N'Admin', N'admin123', CAST(0x0000A2E10139F347 AS DateTime), CAST(0x0000A2E10139F347 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Login] OFF
/****** Object:  Table [dbo].[Company]    Script Date: 03/09/2014 04:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[DBName] [nvarchar](50) NOT NULL,
	[DBUserName] [nvarchar](50) NOT NULL,
	[DBPassword] [nvarchar](50) NOT NULL,
	[DBConnectionString] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[IsDataBaseCreated] [bit] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON
SET IDENTITY_INSERT [dbo].[Company] OFF
/****** Object:  StoredProcedure [dbo].[SP_Company_Update_ConString]    Script Date: 03/09/2014 04:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Company_Update_ConString]
@CompanyID			int = 0,
@DBConnectionString	NVARCHAR(500),
@CommandName	NVARCHAR(50)

AS
BEGIN
	Declare @dbname nvarchar(50)
	select @dbname = dbname from Company where ID = @CompanyID
	IF (RTRIM(LTrim(@CommandName)) = 'Activate')
	begin
		if Exists (SELECT name FROM master.dbo.sysdatabases WHERE name = @dbname)
		begin
		
		UPDATE Company SET	DBConnectionString =@DBConnectionString,IsDataBaseCreated=1   where Id = @CompanyID	
		end
	end
		else
		begin
		UPDATE Company SET	DBConnectionString ='',IsDataBaseCreated=0   where Id = @CompanyID	
		end
		
	
END


--Select IsDataBaseCreated from  Company
GO
/****** Object:  StoredProcedure [dbo].[SP_Company_Update]    Script Date: 03/09/2014 04:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Company_Update]
@CompanyID			int = 0,
@Name			varchar(50) = null,
@Code			varchar(50) = null,
@DBName			VarChar(20) = null,
@DBUserName		varchar(100) = null,
@DBPassword		varchar(100) = null,
--@EmptyConnectionString		BIT,
--@Active BIT,
@ConnectionString		varchar(250) = null
AS
BEGIN
	IF (@CompanyID = 0 or @CompanyID IS NULL)
	BEGIN
		INSERT INTO Company(Name , Code ,DBName,DBUserName, DBPassword )
		 VALUES(@Name, @Code,@DBName, @DBUserName, @DBPassword )
	END
	ELSE
	BEGIN
		UPDATE Company SET Name = @Name, Code = @Code, DBName = @DBName, DBUserName=@DBUserName, DBPassword = @DBPassword,
		DBConnectionString = '', IsDataBaseCreated =0
		where Id = @CompanyID	
	END
	select SCOPE_IDENTITY(), @CompanyID
	
END
GO
/****** Object:  Default [DF_Login_CreatedDate]    Script Date: 03/09/2014 04:05:42 ******/
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Login_UpdatedDate]    Script Date: 03/09/2014 04:05:42 ******/
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
/****** Object:  Default [DF_Company_CreatedDate]    Script Date: 03/09/2014 04:05:42 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  Default [DF_Company_ModifiedDate]    Script Date: 03/09/2014 04:05:42 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  Default [DF_Company_Active]    Script Date: 03/09/2014 04:05:42 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_Active]  DEFAULT ((0)) FOR [Active]
GO
/****** Object:  Default [DF__Company__IsDataB__03317E3D]    Script Date: 03/09/2014 04:05:42 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF__Company__IsDataB__03317E3D]  DEFAULT ((0)) FOR [IsDataBaseCreated]
GO
