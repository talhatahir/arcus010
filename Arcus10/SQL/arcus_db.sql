USE [master]
GO
/****** Object:  Database [arcus_db]    Script Date: 6/3/2015 1:07:27 AM ******/
CREATE DATABASE [arcus_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'arcus_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\arcus_db.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'arcus_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\arcus_db_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [arcus_db] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [arcus_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [arcus_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [arcus_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [arcus_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [arcus_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [arcus_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [arcus_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [arcus_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [arcus_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [arcus_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [arcus_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [arcus_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [arcus_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [arcus_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [arcus_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [arcus_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [arcus_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [arcus_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [arcus_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [arcus_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [arcus_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [arcus_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [arcus_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [arcus_db] SET RECOVERY FULL 
GO
ALTER DATABASE [arcus_db] SET  MULTI_USER 
GO
ALTER DATABASE [arcus_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [arcus_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [arcus_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [arcus_db] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [arcus_db] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'arcus_db', N'ON'
GO
USE [arcus_db]
GO
/****** Object:  User [tnt]    Script Date: 6/3/2015 1:07:27 AM ******/
CREATE USER [tnt] FOR LOGIN [tnt] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 6/3/2015 1:07:27 AM ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [IIS APPPOOL\DefaultAppPool]    Script Date: 6/3/2015 1:07:27 AM ******/
CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA=[IIS APPPOOL\DefaultAppPool]
GO
ALTER ROLE [db_owner] ADD MEMBER [tnt]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_datareader] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\DefaultAppPool]
GO
/****** Object:  Schema [IIS APPPOOL\DefaultAppPool]    Script Date: 6/3/2015 1:07:28 AM ******/
CREATE SCHEMA [IIS APPPOOL\DefaultAppPool]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 6/3/2015 1:07:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[booking_id] [int] IDENTITY(1,1) NOT NULL,
	[fk_guest_id] [int] NOT NULL,
	[fk_room_id] [int] NOT NULL,
	[booking_checkin] [datetime] NOT NULL,
	[booking_checkout] [datetime] NOT NULL,
	[booking_bActive] [bit] NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[booking_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GuestAccounts]    Script Date: 6/3/2015 1:07:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuestAccounts](
	[guestaccounts_index] [int] IDENTITY(1,1) NOT NULL,
	[guestaccount_credit] [float] NOT NULL,
	[guestaccount_debit] [float] NOT NULL,
	[guestaccount_date] [datetime] NOT NULL,
	[fk_guest_id] [int] NOT NULL,
	[guestaccount_balance] [float] NOT NULL,
	[fk_meal_id] [int] NULL,
	[fk_room_id] [int] NULL,
 CONSTRAINT [PK_GuestAccounts] PRIMARY KEY CLUSTERED 
(
	[guestaccounts_index] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Guests]    Script Date: 6/3/2015 1:07:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Guests](
	[guest_id] [int] IDENTITY(1,1) NOT NULL,
	[guest_name] [varchar](max) NOT NULL,
	[guest_city] [varchar](max) NOT NULL,
	[guest_cnic] [varchar](max) NOT NULL,
	[guest_address] [varchar](max) NOT NULL,
	[guest_phone] [varchar](max) NOT NULL,
	[guest_company] [varchar](max) NOT NULL,
	[guest_email] [varchar](max) NOT NULL,
	[guest_notes] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Guests] PRIMARY KEY CLUSTERED 
(
	[guest_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Meals]    Script Date: 6/3/2015 1:07:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Meals](
	[meal_id] [int] IDENTITY(1,1) NOT NULL,
	[meal_name] [varchar](max) NOT NULL,
	[meal_type] [varchar](max) NOT NULL,
	[meal_description] [varchar](max) NOT NULL,
	[meal_price] [float] NOT NULL,
 CONSTRAINT [PK_Meals] PRIMARY KEY CLUSTERED 
(
	[meal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 6/3/2015 1:07:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rooms](
	[room_id] [int] IDENTITY(1,1) NOT NULL,
	[room_number] [varchar](max) NOT NULL,
	[room_capacity] [varchar](max) NOT NULL,
	[room_type] [varchar](max) NOT NULL,
	[room_cost] [varchar](max) NOT NULL,
	[room_status] [varchar](50) NOT NULL,
	[room_name] [varchar](max) NOT NULL,
	[Room_Availability] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[room_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 6/3/2015 1:07:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Staff](
	[staff_id] [int] IDENTITY(1,1) NOT NULL,
	[staff_fullname] [varchar](max) NOT NULL,
	[staff_address] [varchar](max) NOT NULL,
	[staff_email] [varchar](max) NOT NULL,
	[staff_phone] [varchar](max) NOT NULL,
	[staff_nic] [varchar](max) NOT NULL,
	[staff_position] [varchar](max) NOT NULL,
	[staff_account] [varchar](max) NOT NULL,
	[staff_salary] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[staff_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/3/2015 1:07:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[role] [varchar](max) NULL,
	[fullname] [varchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Guests] FOREIGN KEY([fk_guest_id])
REFERENCES [dbo].[Guests] ([guest_id])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Guests]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Rooms] FOREIGN KEY([fk_room_id])
REFERENCES [dbo].[Rooms] ([room_id])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Rooms]
GO
ALTER TABLE [dbo].[GuestAccounts]  WITH CHECK ADD  CONSTRAINT [FK_GuestAccounts_Guests] FOREIGN KEY([fk_guest_id])
REFERENCES [dbo].[Guests] ([guest_id])
GO
ALTER TABLE [dbo].[GuestAccounts] CHECK CONSTRAINT [FK_GuestAccounts_Guests]
GO
ALTER TABLE [dbo].[GuestAccounts]  WITH CHECK ADD  CONSTRAINT [FK_GuestAccounts_Meals] FOREIGN KEY([fk_meal_id])
REFERENCES [dbo].[Meals] ([meal_id])
GO
ALTER TABLE [dbo].[GuestAccounts] CHECK CONSTRAINT [FK_GuestAccounts_Meals]
GO
ALTER TABLE [dbo].[GuestAccounts]  WITH CHECK ADD  CONSTRAINT [FK_GuestAccounts_Rooms] FOREIGN KEY([fk_room_id])
REFERENCES [dbo].[Rooms] ([room_id])
GO
ALTER TABLE [dbo].[GuestAccounts] CHECK CONSTRAINT [FK_GuestAccounts_Rooms]
GO
USE [master]
GO
ALTER DATABASE [arcus_db] SET  READ_WRITE 
GO
