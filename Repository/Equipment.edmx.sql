
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/27/2019 14:38:15
-- Generated from EDMX file: D:\Projects\C#\EquipmentManager\Repository\Equipment.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EquipmentManager];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PositionJournalEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Journal] DROP CONSTRAINT [FK_PositionJournalEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryCategoryJournalEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Journal] DROP CONSTRAINT [FK_EntryCategoryJournalEntry];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO
IF OBJECT_ID(N'[dbo].[Journal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Journal];
GO
IF OBJECT_ID(N'[dbo].[EntryCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntryCategories];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParentId] int  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ComplexName] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NULL
);
GO

-- Creating table 'Journal'
CREATE TABLE [dbo].[Journal] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Position_Id] int  NOT NULL,
    [EntryCategory_Id] int  NOT NULL
);
GO

-- Creating table 'EntryCategories'
CREATE TABLE [dbo].[EntryCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Journal'
ALTER TABLE [dbo].[Journal]
ADD CONSTRAINT [PK_Journal]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EntryCategories'
ALTER TABLE [dbo].[EntryCategories]
ADD CONSTRAINT [PK_EntryCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Position_Id] in table 'Journal'
ALTER TABLE [dbo].[Journal]
ADD CONSTRAINT [FK_PositionJournalEntry]
    FOREIGN KEY ([Position_Id])
    REFERENCES [dbo].[Positions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PositionJournalEntry'
CREATE INDEX [IX_FK_PositionJournalEntry]
ON [dbo].[Journal]
    ([Position_Id]);
GO

-- Creating foreign key on [EntryCategory_Id] in table 'Journal'
ALTER TABLE [dbo].[Journal]
ADD CONSTRAINT [FK_EntryCategoryJournalEntry]
    FOREIGN KEY ([EntryCategory_Id])
    REFERENCES [dbo].[EntryCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryCategoryJournalEntry'
CREATE INDEX [IX_FK_EntryCategoryJournalEntry]
ON [dbo].[Journal]
    ([EntryCategory_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------