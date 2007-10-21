/****** Object:  Database ccc    Script Date: 19-10-2007 0:52:55 ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'ccc')
	DROP DATABASE [ccc]
GO

CREATE DATABASE [ccc]  ON (NAME = N'ccc_Data', FILENAME = N'C:\Programas\Microsoft SQL Server\MSSQL\data\ccc_Data.MDF' , SIZE = 1, FILEGROWTH = 10%) LOG ON (NAME = N'ccc_Log', FILENAME = N'C:\Programas\Microsoft SQL Server\MSSQL\data\ccc_Log.LDF' , SIZE = 1, FILEGROWTH = 10%)
 COLLATE Latin1_General_CI_AS
GO

exec sp_dboption N'ccc', N'autoclose', N'false'
GO

exec sp_dboption N'ccc', N'bulkcopy', N'false'
GO

exec sp_dboption N'ccc', N'trunc. log', N'false'
GO

exec sp_dboption N'ccc', N'torn page detection', N'true'
GO

exec sp_dboption N'ccc', N'read only', N'false'
GO

exec sp_dboption N'ccc', N'dbo use', N'false'
GO

exec sp_dboption N'ccc', N'single', N'false'
GO

exec sp_dboption N'ccc', N'autoshrink', N'false'
GO

exec sp_dboption N'ccc', N'ANSI null default', N'false'
GO

exec sp_dboption N'ccc', N'recursive triggers', N'false'
GO

exec sp_dboption N'ccc', N'ANSI nulls', N'false'
GO

exec sp_dboption N'ccc', N'concat null yields null', N'false'
GO

exec sp_dboption N'ccc', N'cursor close on commit', N'false'
GO

exec sp_dboption N'ccc', N'default to local cursor', N'false'
GO

exec sp_dboption N'ccc', N'quoted identifier', N'false'
GO

exec sp_dboption N'ccc', N'ANSI warnings', N'false'
GO

exec sp_dboption N'ccc', N'auto create statistics', N'true'
GO

exec sp_dboption N'ccc', N'auto update statistics', N'true'
GO

if( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) )
	exec sp_dboption N'ccc', N'db chaining', N'false'
GO

use [ccc]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_MultiLevel]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Category] DROP CONSTRAINT FK_MultiLevel
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Situation_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Situation] DROP CONSTRAINT FK_Situation_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Info_Hospital]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Info] DROP CONSTRAINT FK_Info_Hospital
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Zone_Info]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Zone] DROP CONSTRAINT FK_Zone_Info
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Info_Pacient]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Info] DROP CONSTRAINT FK_Info_Pacient
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Pacient_Situation]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Patient] DROP CONSTRAINT FK_Pacient_Situation
GO

/****** Object:  Table [dbo].[Category]    Script Date: 19-10-2007 0:52:55 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Category]
GO

/****** Object:  Table [dbo].[Hospital]    Script Date: 19-10-2007 0:52:55 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Hospital]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Hospital]
GO

/****** Object:  Table [dbo].[Info]    Script Date: 19-10-2007 0:52:55 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Info]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Info]
GO

/****** Object:  Table [dbo].[Patient]    Script Date: 19-10-2007 0:52:55 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Patient]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Patient]
GO

/****** Object:  Table [dbo].[Situation]    Script Date: 19-10-2007 0:52:55 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Situation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Situation]
GO

/****** Object:  Table [dbo].[Zone]    Script Date: 19-10-2007 0:52:55 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Zone]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Zone]
GO

/****** Object:  Login ca    Script Date: 19-10-2007 0:52:55 ******/
if not exists (select * from master.dbo.syslogins where loginname = N'ca')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'ca', null, @logindb, @loginlang
END
GO

/****** Object:  Login ca1    Script Date: 19-10-2007 0:52:55 ******/
if not exists (select * from master.dbo.syslogins where loginname = N'ca1')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'ca1', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'ca1', null, @logindb, @loginlang
END
GO

/****** Object:  Login ccc    Script Date: 19-10-2007 0:52:55 ******/
if not exists (select * from master.dbo.syslogins where loginname = N'ccc')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'ccc', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'ccc', null, @logindb, @loginlang
END
GO

/****** Object:  Login einsight51    Script Date: 19-10-2007 0:52:55 ******/
if not exists (select * from master.dbo.syslogins where loginname = N'einsight51')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'einsight51', null, @logindb, @loginlang
END
GO

/****** Object:  Login fp    Script Date: 19-10-2007 0:52:55 ******/
if not exists (select * from master.dbo.syslogins where loginname = N'fp')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'fp', null, @logindb, @loginlang
END
GO

/****** Object:  Login jcaps    Script Date: 19-10-2007 0:52:55 ******/
if not exists (select * from master.dbo.syslogins where loginname = N'jcaps')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'jcaps', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'jcaps', null, @logindb, @loginlang
END
GO

/****** Object:  Login BUILTIN\Administradores    Script Date: 19-10-2007 0:52:55 ******/
exec sp_addsrvrolemember N'BUILTIN\Administradores', sysadmin
GO

/****** Object:  Login einsight51    Script Date: 19-10-2007 0:52:55 ******/
exec sp_addsrvrolemember N'einsight51', sysadmin
GO

/****** Object:  Login jcaps    Script Date: 19-10-2007 0:52:55 ******/
exec sp_addsrvrolemember N'jcaps', sysadmin
GO

/****** Object:  Login einsight51    Script Date: 19-10-2007 0:52:55 ******/
exec sp_addsrvrolemember N'einsight51', securityadmin
GO

/****** Object:  User ccc    Script Date: 19-10-2007 0:52:55 ******/
if not exists (select * from dbo.sysusers where name = N'ccc' and uid < 16382)
	EXEC sp_grantdbaccess N'ccc', N'ccc'
GO

/****** Object:  User dbo    Script Date: 19-10-2007 0:52:55 ******/
/****** Object:  User ccc    Script Date: 19-10-2007 0:52:55 ******/
exec sp_addrolemember N'db_owner', N'ccc'
GO

/****** Object:  Table [dbo].[Category]    Script Date: 19-10-2007 0:52:57 ******/
CREATE TABLE [dbo].[Category] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[cod_father] [int] NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Hospital]    Script Date: 19-10-2007 0:52:58 ******/
CREATE TABLE [dbo].[Hospital] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[address] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Info]    Script Date: 19-10-2007 0:52:58 ******/
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

/****** Object:  Table [dbo].[Patient]    Script Date: 19-10-2007 0:52:58 ******/
CREATE TABLE [dbo].[Patient] (
	[guid] [uniqueidentifier] NOT NULL ,
	[cod_situation] [int] NOT NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[bi] [char] (9) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[blood_type] [int] NULL ,
	[isDead] [bit] NULL ,
	[obs] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Situation]    Script Date: 19-10-2007 0:52:58 ******/
CREATE TABLE [dbo].[Situation] (
	[cod] [int] IDENTITY (1, 1) NOT NULL ,
	[cod_category] [int] NOT NULL ,
	[name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[description] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[address] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[date_time] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Zone]    Script Date: 19-10-2007 0:52:58 ******/
CREATE TABLE [dbo].[Zone] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[info] [int] NOT NULL ,
	[guid] [uniqueidentifier] NOT NULL ,
	[content] [varchar] (500) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Category] WITH NOCHECK ADD 
	CONSTRAINT [PK_Category] PRIMARY KEY  CLUSTERED 
	(
		[cod]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Hospital] WITH NOCHECK ADD 
	CONSTRAINT [PK_Hospital] PRIMARY KEY  CLUSTERED 
	(
		[cod]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Info] WITH NOCHECK ADD 
	CONSTRAINT [PK_Info] PRIMARY KEY  CLUSTERED 
	(
		[cod],
		[guid_pacient]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Patient] WITH NOCHECK ADD 
	CONSTRAINT [PK_Pacient] PRIMARY KEY  CLUSTERED 
	(
		[guid]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Situation] WITH NOCHECK ADD 
	CONSTRAINT [PK_Situation] PRIMARY KEY  CLUSTERED 
	(
		[cod]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Zone] WITH NOCHECK ADD 
	CONSTRAINT [PK_Zone] PRIMARY KEY  CLUSTERED 
	(
		[id],
		[info],
		[guid]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Info] ADD 
	CONSTRAINT [IX_Info] UNIQUE  NONCLUSTERED 
	(
		[gravity]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Patient] ADD 
	CONSTRAINT [DF_Patient_isDead] DEFAULT (0) FOR [isDead]
GO

ALTER TABLE [dbo].[Category] ADD 
	CONSTRAINT [FK_MultiLevel] FOREIGN KEY 
	(
		[cod_father]
	) REFERENCES [dbo].[Category] (
		[cod]
	)
GO

ALTER TABLE [dbo].[Info] ADD 
	CONSTRAINT [FK_Info_Hospital] FOREIGN KEY 
	(
		[cod_hospital]
	) REFERENCES [dbo].[Hospital] (
		[cod]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_Info_Pacient] FOREIGN KEY 
	(
		[guid_pacient]
	) REFERENCES [dbo].[Patient] (
		[guid]
	)
GO

ALTER TABLE [dbo].[Patient] ADD 
	CONSTRAINT [FK_Pacient_Situation] FOREIGN KEY 
	(
		[cod_situation]
	) REFERENCES [dbo].[Situation] (
		[cod]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[Situation] ADD 
	CONSTRAINT [FK_Situation_Category] FOREIGN KEY 
	(
		[cod_category]
	) REFERENCES [dbo].[Category] (
		[cod]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[Zone] ADD 
	CONSTRAINT [FK_Zone_Info] FOREIGN KEY 
	(
		[info],
		[guid]
	) REFERENCES [dbo].[Info] (
		[cod],
		[guid_pacient]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

