
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2019 00:42:20
-- Generated from EDMX file: C:\Users\Muterk\Documents\Git-hub\Pacman-services\DataAccess\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PacmanDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsuarioJugador]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioSet] DROP CONSTRAINT [FK_UsuarioJugador];
GO
IF OBJECT_ID(N'[dbo].[FK_JugadorRanking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JugadorSet] DROP CONSTRAINT [FK_JugadorRanking];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UsuarioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioSet];
GO
IF OBJECT_ID(N'[dbo].[JugadorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JugadorSet];
GO
IF OBJECT_ID(N'[dbo].[RankingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RankingSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UsuarioSet'
CREATE TABLE [dbo].[UsuarioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Jugador_Id] int  NOT NULL
);
GO

-- Creating table 'JugadorSet'
CREATE TABLE [dbo].[JugadorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Correo] nvarchar(max)  NOT NULL,
    [PuntuaciónAlta] nvarchar(max)  NOT NULL,
    [Puntuación] nvarchar(max)  NOT NULL,
    [PantallasGanadas] nvarchar(max)  NOT NULL,
    [Elo] nvarchar(max)  NOT NULL,
    [Ranking_Id] int  NOT NULL
);
GO

-- Creating table 'RankingSet'
CREATE TABLE [dbo].[RankingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Posicion] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [PK_UsuarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JugadorSet'
ALTER TABLE [dbo].[JugadorSet]
ADD CONSTRAINT [PK_JugadorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RankingSet'
ALTER TABLE [dbo].[RankingSet]
ADD CONSTRAINT [PK_RankingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Jugador_Id] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [FK_UsuarioJugador]
    FOREIGN KEY ([Jugador_Id])
    REFERENCES [dbo].[JugadorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioJugador'
CREATE INDEX [IX_FK_UsuarioJugador]
ON [dbo].[UsuarioSet]
    ([Jugador_Id]);
GO

-- Creating foreign key on [Ranking_Id] in table 'JugadorSet'
ALTER TABLE [dbo].[JugadorSet]
ADD CONSTRAINT [FK_JugadorRanking]
    FOREIGN KEY ([Ranking_Id])
    REFERENCES [dbo].[RankingSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JugadorRanking'
CREATE INDEX [IX_FK_JugadorRanking]
ON [dbo].[JugadorSet]
    ([Ranking_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------