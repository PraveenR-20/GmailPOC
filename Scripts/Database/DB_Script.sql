USE [master]
GO
/****** Object:  Database [GmailPOC]    Script Date: 12/15/2020 11:13:16 PM ******/
CREATE DATABASE [GmailPOC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GmailPOC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\GmailPOC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GmailPOC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\GmailPOC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [GmailPOC] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GmailPOC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GmailPOC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GmailPOC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GmailPOC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GmailPOC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GmailPOC] SET ARITHABORT OFF 
GO
ALTER DATABASE [GmailPOC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GmailPOC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GmailPOC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GmailPOC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GmailPOC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GmailPOC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GmailPOC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GmailPOC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GmailPOC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GmailPOC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GmailPOC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GmailPOC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GmailPOC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GmailPOC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GmailPOC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GmailPOC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GmailPOC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GmailPOC] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GmailPOC] SET  MULTI_USER 
GO
ALTER DATABASE [GmailPOC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GmailPOC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GmailPOC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GmailPOC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GmailPOC] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GmailPOC] SET QUERY_STORE = OFF
GO
USE [GmailPOC]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [GmailPOC]
GO
/****** Object:  Table [dbo].[EventAttendees]    Script Date: 12/15/2020 11:13:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventAttendees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserEventId] [int] NULL,
	[comment] [varchar](500) NULL,
	[displayName] [varchar](500) NULL,
	[email] [varchar](500) NULL,
	[user_id] [varchar](500) NULL,
	[optional] [varchar](500) NULL,
	[organizer] [varchar](500) NULL,
	[resource] [varchar](500) NULL,
	[responseStatus] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserEvents]    Script Date: 12/15/2020 11:13:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[anyoneCanAddSelf] [bit] NULL,
	[attendeesOmitted] [bit] NULL,
	[conferenceData] [varchar](500) NULL,
	[createdRaw] [varchar](100) NULL,
	[created] [datetime] NULL,
	[description] [varchar](max) NULL,
	[endTimeUnspecified] [bit] NULL,
	[guestsCanInviteOthers] [bit] NULL,
	[guestsCanModify] [bit] NULL,
	[guestsCanSeeOtherGuests] [bit] NULL,
	[hangoutLink] [varchar](500) NULL,
	[htmlLink] [varchar](500) NULL,
	[iCalUID] [varchar](500) NULL,
	[kind] [varchar](500) NULL,
	[location] [varchar](500) NULL,
	[locked] [bit] NULL,
	[originalStartTime] [datetime] NULL,
	[privateCopy] [bit] NULL,
	[recurrence] [varchar](500) NULL,
	[sequence] [int] NULL,
	[source] [varchar](500) NULL,
	[status] [varchar](500) NULL,
	[summary] [varchar](500) NULL,
	[visibility] [varchar](500) NULL,
	[updatedRaw] [varchar](100) NULL,
	[updated] [datetime] NULL,
	[creator_displayName] [varchar](500) NULL,
	[creator_email] [varchar](500) NULL,
	[creator_id] [varchar](500) NULL,
	[creator_self] [bit] NULL,
	[org_displayName] [varchar](500) NULL,
	[org_email] [varchar](500) NULL,
	[org_id] [varchar](500) NULL,
	[org_self] [bit] NULL,
	[recurringEventId] [varchar](50) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRecurringEvents]    Script Date: 12/15/2020 11:13:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRecurringEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserEventId] [int] NULL,
	[start_date] [varchar](500) NULL,
	[start_datetimeraw] [varchar](500) NULL,
	[start_datatime] [datetime] NULL,
	[start_timezone] [varchar](500) NULL,
	[end_date] [varchar](500) NULL,
	[end_datetimeraw] [varchar](500) NULL,
	[end_datatime] [datetime] NULL,
	[end_timezone] [varchar](500) NULL,
	[recurringEventId] [varchar](500) NULL,
	[reminders_overrides] [varchar](500) NULL,
	[reminders_useDefault] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/15/2020 11:13:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[ClientId] [varchar](550) NULL,
	[SecretKey] [varchar](550) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[UserEvents] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[EventAttendees]  WITH CHECK ADD FOREIGN KEY([UserEventId])
REFERENCES [dbo].[UserEvents] ([Id])
GO
ALTER TABLE [dbo].[UserRecurringEvents]  WITH CHECK ADD FOREIGN KEY([UserEventId])
REFERENCES [dbo].[UserEvents] ([Id])
GO
USE [master]
GO
ALTER DATABASE [GmailPOC] SET  READ_WRITE 
GO
