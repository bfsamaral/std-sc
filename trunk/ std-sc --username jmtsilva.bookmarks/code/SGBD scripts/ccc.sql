if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_MultiLevel]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Category] DROP CONSTRAINT FK_MultiLevel
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Situation_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Situation] DROP CONSTRAINT FK_Situation_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Info_Hospital]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Info] DROP CONSTRAINT FK_Info_Hospital
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Info_zone_Info]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Info_zone] DROP CONSTRAINT FK_Info_zone_Info
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Info_Pacient]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Info] DROP CONSTRAINT FK_Info_Pacient
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Pacient_Situation]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Pacient] DROP CONSTRAINT FK_Pacient_Situation
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Info_zone_Zone]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Info_zone] DROP CONSTRAINT FK_Info_zone_Zone
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Category]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Hospital]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Hospital]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Info]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Info]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Info_zone]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Info_zone]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pacient]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Pacient]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Situation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Situation]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Zone]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Zone]
GO

CREATE TABLE [dbo].[Category] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[cod_father] [int] NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Hospital] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[address] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Info] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[guid_pacient] [uniqueidentifier] NOT NULL ,
	[cod_hospital] [int] NOT NULL ,
	[pressuremin] [float] NULL ,
	[pressuremax] [float] NULL ,
	[pulse] [int] NULL ,
	[diabets] [bit] NULL ,
	[description] [varchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[gravity] [int] NOT NULL ,
	[source_type] [int] NOT NULL ,
	[source_id] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Info_zone] (
	[cod_info] [int] NOT NULL ,
	[guid_pacient] [uniqueidentifier] NOT NULL ,
	[cod_zone] [int] NOT NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Pacient] (
	[guid] [uniqueidentifier] NOT NULL ,
	[cod_situation] [int] NOT NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[bi] [char] (9) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[blood_type] [int] NULL ,
	[obs] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Situation] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[cod_category] [int] NOT NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[address] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[date_time] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Zone] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

