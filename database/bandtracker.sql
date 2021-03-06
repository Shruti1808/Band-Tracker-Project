CREATE DATABASE band_tracker;
go

USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 3/3/2017 4:58:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[venue_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 3/3/2017 4:58:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues_bands]    Script Date: 3/3/2017 4:58:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues_bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[venue_id] [int] NULL,
	[band_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name], [venue_id]) VALUES (1, N'coldplay', NULL)
INSERT [dbo].[bands] ([id], [name], [venue_id]) VALUES (2, N'metallica', NULL)
INSERT [dbo].[bands] ([id], [name], [venue_id]) VALUES (3, N'pasmina', NULL)
INSERT [dbo].[bands] ([id], [name], [venue_id]) VALUES (4, N'sds', NULL)
INSERT [dbo].[bands] ([id], [name], [venue_id]) VALUES (5, N'ksjdksd', NULL)
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name]) VALUES (1, N'KeyArena Theater')
INSERT [dbo].[venues] ([id], [name]) VALUES (2, N'KeyArena Theater')
INSERT [dbo].[venues] ([id], [name]) VALUES (3, N'ksjfksd')
INSERT [dbo].[venues] ([id], [name]) VALUES (4, N'KeyArena')
INSERT [dbo].[venues] ([id], [name]) VALUES (5, N'KeyArena')
INSERT [dbo].[venues] ([id], [name]) VALUES (6, N'Keyarena')
INSERT [dbo].[venues] ([id], [name]) VALUES (7, N'keyarena')
INSERT [dbo].[venues] ([id], [name]) VALUES (8, N'keyarena')
INSERT [dbo].[venues] ([id], [name]) VALUES (9, N'jhkjl')
SET IDENTITY_INSERT [dbo].[venues] OFF
SET IDENTITY_INSERT [dbo].[venues_bands] ON 

INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (1, 1, 1)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (2, 1, 3)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (3, 1, 1)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (4, 3, 1)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (5, 4, 1)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (6, 9, 1)
SET IDENTITY_INSERT [dbo].[venues_bands] OFF
