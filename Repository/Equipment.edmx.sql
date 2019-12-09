
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2019 15:49:23
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
IF OBJECT_ID(N'[dbo].[JournalEntryCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JournalEntryCategories];
GO
IF OBJECT_ID(N'[dbo].[PositionStatusBitsInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PositionStatusBitsInfo];
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
    [Title] nvarchar(max)  NULL,
    [Status] bigint  NOT NULL
);
GO

-- Creating table 'Journal'
CREATE TABLE [dbo].[Journal] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Position_Id] int  NOT NULL,
    [JournalEntryCategory_Id] int  NOT NULL,
    [PositionStatusBitInfo_BitNumber] int  NOT NULL
);
GO

-- Creating table 'JournalEntryCategories'
CREATE TABLE [dbo].[JournalEntryCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'PositionStatusBitsInfo'
CREATE TABLE [dbo].[PositionStatusBitsInfo] (
    [BitNumber] int  NOT NULL,
    [Enable] bit  NOT NULL,
    [Title] nvarchar(max)  NULL
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

-- Creating primary key on [Id] in table 'JournalEntryCategories'
ALTER TABLE [dbo].[JournalEntryCategories]
ADD CONSTRAINT [PK_JournalEntryCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [BitNumber] in table 'PositionStatusBitsInfo'
ALTER TABLE [dbo].[PositionStatusBitsInfo]
ADD CONSTRAINT [PK_PositionStatusBitsInfo]
    PRIMARY KEY CLUSTERED ([BitNumber] ASC);
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

-- Creating foreign key on [JournalEntryCategory_Id] in table 'Journal'
ALTER TABLE [dbo].[Journal]
ADD CONSTRAINT [FK_EntryCategoryJournalEntry]
    FOREIGN KEY ([JournalEntryCategory_Id])
    REFERENCES [dbo].[JournalEntryCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryCategoryJournalEntry'
CREATE INDEX [IX_FK_EntryCategoryJournalEntry]
ON [dbo].[Journal]
    ([JournalEntryCategory_Id]);
GO

-- Creating foreign key on [PositionStatusBitInfo_BitNumber] in table 'Journal'
ALTER TABLE [dbo].[Journal]
ADD CONSTRAINT [FK_PositionStatusBitInfoJournalEntry]
    FOREIGN KEY ([PositionStatusBitInfo_BitNumber])
    REFERENCES [dbo].[PositionStatusBitsInfo]
        ([BitNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PositionStatusBitInfoJournalEntry'
CREATE INDEX [IX_FK_PositionStatusBitInfoJournalEntry]
ON [dbo].[Journal]
    ([PositionStatusBitInfo_BitNumber]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------