
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/30/2019 11:29:09
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParentId] int  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NULL
);
GO

-- Creating table 'Journal'
CREATE TABLE [dbo].[Journal] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Position_Id] int  NOT NULL,
    [EventCategory_Id] int  NOT NULL
);
GO

-- Creating table 'EventCategories'
CREATE TABLE [dbo].[EventCategories] (
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

-- Creating primary key on [Id] in table 'EventCategories'
ALTER TABLE [dbo].[EventCategories]
ADD CONSTRAINT [PK_EventCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Position_Id] in table 'Journal'
ALTER TABLE [dbo].[Journal]
ADD CONSTRAINT [FK_PositionJournalEvent]
    FOREIGN KEY ([Position_Id])
    REFERENCES [dbo].[Positions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PositionJournalEvent'
CREATE INDEX [IX_FK_PositionJournalEvent]
ON [dbo].[Journal]
    ([Position_Id]);
GO

-- Creating foreign key on [EventCategory_Id] in table 'Journal'
ALTER TABLE [dbo].[Journal]
ADD CONSTRAINT [FK_EventCategoryJournalEvent]
    FOREIGN KEY ([EventCategory_Id])
    REFERENCES [dbo].[EventCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventCategoryJournalEvent'
CREATE INDEX [IX_FK_EventCategoryJournalEvent]
ON [dbo].[Journal]
    ([EventCategory_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------