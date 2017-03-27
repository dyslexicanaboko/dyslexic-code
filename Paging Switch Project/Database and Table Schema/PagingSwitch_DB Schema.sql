USE [master]
GO

/****** Object:  Database [PagingSwitch]    Script Date: 06/26/2011 01:17:26 ******/
CREATE DATABASE [PagingSwitch] ON  PRIMARY 
( NAME = N'PagingSwitch', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\PagingSwitch.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PagingSwitch_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\PagingSwitch_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [PagingSwitch] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PagingSwitch].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PagingSwitch] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PagingSwitch] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PagingSwitch] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PagingSwitch] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PagingSwitch] SET ARITHABORT OFF 
GO

ALTER DATABASE [PagingSwitch] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PagingSwitch] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [PagingSwitch] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PagingSwitch] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PagingSwitch] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PagingSwitch] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PagingSwitch] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PagingSwitch] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PagingSwitch] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PagingSwitch] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PagingSwitch] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PagingSwitch] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PagingSwitch] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PagingSwitch] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PagingSwitch] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PagingSwitch] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PagingSwitch] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PagingSwitch] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PagingSwitch] SET  READ_WRITE 
GO

ALTER DATABASE [PagingSwitch] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PagingSwitch] SET  MULTI_USER 
GO

ALTER DATABASE [PagingSwitch] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PagingSwitch] SET DB_CHAINING OFF 
GO

