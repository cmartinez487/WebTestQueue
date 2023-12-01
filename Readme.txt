*Script para la creacion de la tabla en sql server*

USE [Testing]
GO

/****** Object:  Table [dbo].[TestingQueue]    Script Date: 01-dic-23 5:40:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestingQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[dni] [bigint] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[queue] [int] NOT NULL,
	[status] [bit] NOT NULL
) ON [PRIMARY]
GO