USE [master]
GO
/****** Object:  Database [event_watcher_db]    Script Date: 18.12.2018 20:43:30 ******/
CREATE DATABASE [event_watcher_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'event_watcher_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\event_watcher_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'event_watcher_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\event_watcher_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [event_watcher_db] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [event_watcher_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [event_watcher_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [event_watcher_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [event_watcher_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [event_watcher_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [event_watcher_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [event_watcher_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [event_watcher_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [event_watcher_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [event_watcher_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [event_watcher_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [event_watcher_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [event_watcher_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [event_watcher_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [event_watcher_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [event_watcher_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [event_watcher_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [event_watcher_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [event_watcher_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [event_watcher_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [event_watcher_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [event_watcher_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [event_watcher_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [event_watcher_db] SET RECOVERY FULL 
GO
ALTER DATABASE [event_watcher_db] SET  MULTI_USER 
GO
ALTER DATABASE [event_watcher_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [event_watcher_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [event_watcher_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [event_watcher_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [event_watcher_db] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'event_watcher_db', N'ON'
GO
ALTER DATABASE [event_watcher_db] SET QUERY_STORE = OFF
GO
USE [event_watcher_db]
GO
/****** Object:  Table [dbo].[descryptions]    Script Date: 18.12.2018 20:43:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[descryptions](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[descryprion] [nvarchar](max) NULL,
 CONSTRAINT [PK_descryptions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[events]    Script Date: 18.12.2018 20:43:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[events](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[location_id] [bigint] NULL,
	[event_type_id] [bigint] NULL,
	[details_id] [bigint] NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[events_types]    Script Date: 18.12.2018 20:43:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[events_types](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[event_type] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_events_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[locations]    Script Date: 18.12.2018 20:43:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locations](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[location] [nvarchar](150) NULL,
	[date] [datetime2](0) NULL,
 CONSTRAINT [PK_locations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeatsStatus]    Script Date: 18.12.2018 20:43:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatsStatus](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[seats_count] [int] NOT NULL,
	[event_id] [bigint] NOT NULL,
 CONSTRAINT [PK_SeatsStatus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[events]  WITH CHECK ADD  CONSTRAINT [FK_events_details] FOREIGN KEY([details_id])
REFERENCES [dbo].[descryptions] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[events] CHECK CONSTRAINT [FK_events_details]
GO
ALTER TABLE [dbo].[events]  WITH CHECK ADD  CONSTRAINT [FK_events_eventsTypes] FOREIGN KEY([event_type_id])
REFERENCES [dbo].[events_types] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[events] CHECK CONSTRAINT [FK_events_eventsTypes]
GO
ALTER TABLE [dbo].[events]  WITH CHECK ADD  CONSTRAINT [FK_events_locations] FOREIGN KEY([location_id])
REFERENCES [dbo].[locations] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[events] CHECK CONSTRAINT [FK_events_locations]
GO
ALTER TABLE [dbo].[SeatsStatus]  WITH CHECK ADD  CONSTRAINT [FK_SeatsStatus_events] FOREIGN KEY([event_id])
REFERENCES [dbo].[events] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SeatsStatus] CHECK CONSTRAINT [FK_SeatsStatus_events]
GO
USE [master]
GO
ALTER DATABASE [event_watcher_db] SET  READ_WRITE 
GO
