USE [MangaOnline.V1.DevPRN221]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Authors]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryManga]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryManga](
	[CategoryId] [uniqueidentifier] NOT NULL,
	[MangaId] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Chapteres]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chapteres](
	[Id] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
	[MangaId] [uniqueidentifier] NOT NULL,
	[ChapterNumber] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[Status] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FilePDF] [nvarchar](max) NULL,
 CONSTRAINT [PK_Chapteres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [uniqueidentifier] NOT NULL,
	[MangaId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[LikedCount] [int] NOT NULL,
	[DislikedCount] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Comments_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FollowList]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowList](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[MangaId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_FollowList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[History]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[Id] [uniqueidentifier] NOT NULL,
	[user] [nvarchar](256) NULL,
	[hash] [nvarchar](256) NULL,
	[from] [nvarchar](256) NULL,
	[to] [nvarchar](256) NULL,
	[value] [nvarchar](256) NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IpUserVote]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IpUserVote](
	[MangaId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Rate] [int] NULL,
 CONSTRAINT [pk_my_table] PRIMARY KEY CLUSTERED 
(
	[MangaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mangas]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mangas](
	[Id] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
	[RateCount] [int] NOT NULL,
	[Star] [int] NOT NULL,
	[FollowCount] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetimeoffset](7) NULL,
	[ModifiedAt] [datetimeoffset](7) NULL,
	[IsActive] [bit] NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mangas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notification]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[ChapterId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pages]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [uniqueidentifier] NOT NULL,
	[ChapterId] [uniqueidentifier] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[PageNumber] [int] NOT NULL,
 CONSTRAINT [PK_Pages_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [bigint] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[SubId] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NULL,
	[ModifiedAt] [datetimeoffset](7) NULL,
	[IsActive] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[Avatar] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 8/22/23 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Expires] [datetime2](7) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'ed45a6ea-d341-4a0c-aa22-02b13bbcf645', 8, N'Aoyama Gosho')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'fcf4d2c5-3006-4d6c-9cfa-10b2cb4673c8', 18, N'Aoyama Gōshō')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'52559b15-3f1c-4d2b-ae37-1e9099e99260', 16, N'Kishimoto Masashi')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'38e73675-74c7-4a67-9e29-23e5103c3200', 9, N'Đang cập nhật')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'dad57cab-1237-453b-8162-25ec3bb6477c', 19, N'Đang Cập Nhật')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'73ec328e-c9e5-4351-8552-62468959c767', 5, N'Đang Cập Nhật')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'27a8541c-29b4-418f-be3e-6e1594ad835e', 6, N'Đang Cập Nhật')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'66fa04dc-b9c8-408c-8887-8342471f6b2c', 4, N'Đang Cập Nhật')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'c46d83f3-fb07-4b14-ad6e-83f7723ba291', 14, N'Akira Toriyama')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'083ca240-3c07-454f-9b43-85ea12799178', 2, N'Eiichiro Oda')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'dcb5593f-1f66-4d90-a8a4-9450a920ce7e', 15, N'Tite Kubo')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'6b088831-4697-4260-9478-a156d941bfbb', 17, N'Đang Cập Nhật')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'b1922b88-1a35-4da4-b112-a57dbb49d87a', 10, N'Kimetsu No Yaiba')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'8c8100eb-9608-430d-bacb-a666aa08acc6', 13, N'Akutami Gege')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'5c579ac2-91a0-4b9f-8324-a86ba98e932e', 3, N'Đang Cập Nhật')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'f79d8ac1-a9dd-430d-acd2-aa5ca9bcc2ea', 11, N'Hajime Isayama')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'ae210230-7aa0-4565-adde-bd0509f36cc4', 12, N'Murata Yuusuke, One')
INSERT [dbo].[Authors] ([Id], [SubId], [Name]) VALUES (N'6b9c91a4-b91c-4294-89d7-bd5b62d7c27b', 7, N'Đang Cập Nhật')
SET IDENTITY_INSERT [dbo].[Authors] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [SubId], [Name]) VALUES (N'd8c7163c-1e11-48c0-98d4-16b22c812607', 7, N'Ngôn tình')
INSERT [dbo].[Categories] ([Id], [SubId], [Name]) VALUES (N'6accc550-d475-4357-bcb9-17302ed409c4', 4, N'Drama')
INSERT [dbo].[Categories] ([Id], [SubId], [Name]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', 6, N'Trinh thám')
INSERT [dbo].[Categories] ([Id], [SubId], [Name]) VALUES (N'385871e9-75b7-4cf9-9eb3-87f31d6e7bae', 3, N'Cổ đại')
INSERT [dbo].[Categories] ([Id], [SubId], [Name]) VALUES (N'2660558e-65c0-4efd-86f8-8b84de4b9ce4', 2, N'Tình cảm')
INSERT [dbo].[Categories] ([Id], [SubId], [Name]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', 1, N'Anime')
INSERT [dbo].[Categories] ([Id], [SubId], [Name]) VALUES (N'83458358-35e2-4845-bddc-c00314040cb0', 8, N'Đam mỹ')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[CategoryManga] ON 

INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'385871e9-75b7-4cf9-9eb3-87f31d6e7bae', N'872a9a88-362c-4e18-97f7-59d36747eb36', 3)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'0dfabc44-bd5d-49ea-baec-2ca4386ad32e', 11)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'0dfabc44-bd5d-49ea-baec-2ca4386ad32e', 12)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'0efebd14-ad6c-4859-88eb-85f01147a8f7', 29)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'2660558e-65c0-4efd-86f8-8b84de4b9ce4', N'0efebd14-ad6c-4859-88eb-85f01147a8f7', 30)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'8edaafba-3ea0-4f38-b9a3-df342827c3fe', 31)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'385871e9-75b7-4cf9-9eb3-87f31d6e7bae', N'8edaafba-3ea0-4f38-b9a3-df342827c3fe', 32)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'8edaafba-3ea0-4f38-b9a3-df342827c3fe', 33)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'd8c7163c-1e11-48c0-98d4-16b22c812607', N'ae502810-03e2-40cc-a7a4-ecb6365c106c', 34)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'6accc550-d475-4357-bcb9-17302ed409c4', N'ae502810-03e2-40cc-a7a4-ecb6365c106c', 35)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'ae502810-03e2-40cc-a7a4-ecb6365c106c', 36)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'8c1e3d6e-90d6-465d-9494-3b04136b5d9e', 37)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'8c1e3d6e-90d6-465d-9494-3b04136b5d9e', 38)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'76fd4451-341b-4bc0-ba5c-70c6923ef45a', 39)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'76fd4451-341b-4bc0-ba5c-70c6923ef45a', 40)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'2a188271-735d-4d56-a7be-293a8dcb5827', 41)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'6accc550-d475-4357-bcb9-17302ed409c4', N'a2c399d9-3465-4594-a56b-6785cb4814fb', 42)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'a2c399d9-3465-4594-a56b-6785cb4814fb', 43)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'a2c399d9-3465-4594-a56b-6785cb4814fb', 44)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'21e8fb34-ca60-456f-9439-811c3ad46c34', 45)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'2660558e-65c0-4efd-86f8-8b84de4b9ce4', N'21e8fb34-ca60-456f-9439-811c3ad46c34', 46)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'21e8fb34-ca60-456f-9439-811c3ad46c34', 47)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'd8c7163c-1e11-48c0-98d4-16b22c812607', N'eb574d4b-c77e-4600-b3f9-1feb27c9210e', 48)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'83458358-35e2-4845-bddc-c00314040cb0', N'eb574d4b-c77e-4600-b3f9-1feb27c9210e', 49)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'6accc550-d475-4357-bcb9-17302ed409c4', N'847b5348-133f-4a7a-ad8c-c370ecf15996', 50)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'847b5348-133f-4a7a-ad8c-c370ecf15996', 51)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'b0a79ab2-2d61-4bf6-ae7b-404885c678b8', 52)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'd02a7aac-8c33-487a-abb2-8885e94a4fd4', 53)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'd02a7aac-8c33-487a-abb2-8885e94a4fd4', 54)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'385871e9-75b7-4cf9-9eb3-87f31d6e7bae', N'153e47f7-a6f1-4912-8b50-f1ac6af42e6a', 55)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'766a75b4-6942-4b9b-aecb-bbeeaa36a4b0', N'153e47f7-a6f1-4912-8b50-f1ac6af42e6a', 56)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'52a213a9-7383-411f-b922-00119bf25b1f', 57)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'fd761c84-897c-4529-aec7-7a8731ef1b90', N'e709378c-b03e-4644-bd70-cba4fa1bc40d', 58)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'2660558e-65c0-4efd-86f8-8b84de4b9ce4', N'e709378c-b03e-4644-bd70-cba4fa1bc40d', 59)
INSERT [dbo].[CategoryManga] ([CategoryId], [MangaId], [SubId]) VALUES (N'83458358-35e2-4845-bddc-c00314040cb0', N'19aef671-c261-4383-aee4-1f306f040393', 60)
SET IDENTITY_INSERT [dbo].[CategoryManga] OFF
SET IDENTITY_INSERT [dbo].[Chapteres] ON 

INSERT [dbo].[Chapteres] ([Id], [SubId], [MangaId], [ChapterNumber], [Name], [CreatedAt], [Status], [IsActive], [FilePDF]) VALUES (N'3e0fa829-38ca-4a01-a013-244dd29c173c', 1, N'52a213a9-7383-411f-b922-00119bf25b1f', 1, N' Chapter 1', CAST(N'2023-03-23T00:59:07.6312038+07:00' AS DateTimeOffset), 1, 1, N'7f27f88a-8461-4f38-a90e-6d9b45e7efc7.pdf')
INSERT [dbo].[Chapteres] ([Id], [SubId], [MangaId], [ChapterNumber], [Name], [CreatedAt], [Status], [IsActive], [FilePDF]) VALUES (N'02633814-2ac1-46e6-88cf-4b6e351ec10d', 2, N'52a213a9-7383-411f-b922-00119bf25b1f', 2, N' Chapter 2', CAST(N'2023-03-23T01:09:25.6360223+07:00' AS DateTimeOffset), 1, 1, N'76646c37-35a3-4fb9-9c9c-6837e55e27d7.pdf')
INSERT [dbo].[Chapteres] ([Id], [SubId], [MangaId], [ChapterNumber], [Name], [CreatedAt], [Status], [IsActive], [FilePDF]) VALUES (N'567f0c1a-ecd0-445b-aff4-4c802fd749c5', 5, N'52a213a9-7383-411f-b922-00119bf25b1f', 4, N' Chapter 4', CAST(N'2023-07-18T14:54:05.9858275+07:00' AS DateTimeOffset), 1, 1, NULL)
INSERT [dbo].[Chapteres] ([Id], [SubId], [MangaId], [ChapterNumber], [Name], [CreatedAt], [Status], [IsActive], [FilePDF]) VALUES (N'd9c69489-6688-48f8-8afc-4f5e07d5cf27', 4, N'52a213a9-7383-411f-b922-00119bf25b1f', 3, N' Chapter 3', CAST(N'2023-07-18T14:51:49.3110513+07:00' AS DateTimeOffset), 1, 1, NULL)
INSERT [dbo].[Chapteres] ([Id], [SubId], [MangaId], [ChapterNumber], [Name], [CreatedAt], [Status], [IsActive], [FilePDF]) VALUES (N'ee0ac461-40b3-43b8-bedb-eb651f933d2d', 3, N'2a188271-735d-4d56-a7be-293a8dcb5827', 1, N' Chapter 1', CAST(N'2023-03-23T01:19:39.4014435+07:00' AS DateTimeOffset), 1, 1, N'782f9e53-36b1-4684-b663-045bd6e6154f.pdf')
SET IDENTITY_INSERT [dbo].[Chapteres] OFF
INSERT [dbo].[Comments] ([Id], [MangaId], [UserId], [Content], [CreatedAt], [LikedCount], [DislikedCount], [IsActive]) VALUES (N'1f431bdd-6f53-46b4-b98c-9bf771d1b1a9', N'a2c399d9-3465-4594-a56b-6785cb4814fb', N'fa413bd3-52a0-4ef3-bfe4-ee903a92ceb6', N'haha', CAST(N'2023-03-21T11:40:09.6780464+07:00' AS DateTimeOffset), 0, 0, 1)
INSERT [dbo].[Comments] ([Id], [MangaId], [UserId], [Content], [CreatedAt], [LikedCount], [DislikedCount], [IsActive]) VALUES (N'53cbda3a-d787-401c-b9a0-d54e94b145cb', N'a2c399d9-3465-4594-a56b-6785cb4814fb', N'fa413bd3-52a0-4ef3-bfe4-ee903a92ceb6', N'ads', CAST(N'2023-03-21T11:32:03.8126016+07:00' AS DateTimeOffset), 10, 10, 1)
INSERT [dbo].[History] ([Id], [user], [hash], [from], [to], [value], [date]) VALUES (N'ef7014c2-3631-4bc1-89d8-36674c65ae77', N'C00037B5-24C0-4255-8620-AC97B53A1C1B', N'2', N'22/08/2023', N'21/09/2023', N'638282991567386388', CAST(N'2023-08-22 13:36:07.943' AS DateTime))
INSERT [dbo].[History] ([Id], [user], [hash], [from], [to], [value], [date]) VALUES (N'8444e38b-0b4e-4e79-895a-7e917d29ba34', N'87CC072D-C572-4C12-83A8-9CC6D0ED623E', N'1', N'22/08/2023', N'29/08/2023', N'638283109242342652', CAST(N'2023-08-22 14:24:15.393' AS DateTime))
INSERT [dbo].[IpUserVote] ([MangaId], [UserId], [Rate]) VALUES (N'52a213a9-7383-411f-b922-00119bf25b1f', N'96ed8a95-595e-4af9-96c6-0ac65f1f4128', 3)
SET IDENTITY_INSERT [dbo].[Mangas] ON 

INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'52a213a9-7383-411f-b922-00119bf25b1f', 17, N'6b088831-4697-4260-9478-a156d941bfbb', 0, 373, 0, 1, 0, N'Tuyện tiếp nối chap 545 của Fairy Tail, khi nhóm Natsu đi làm nhiệm vụ trăm năm.', CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-26T17:10:59.8828507+07:00' AS DateTimeOffset), 1, N'564af0fd-6df5-4d6f-9d3d-85a0429dbf0f.jpg', N'Hội Pháp Sư Nhiệm Vụ Trăm Năm')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'19aef671-c261-4383-aee4-1f306f040393', 19, N'dad57cab-1237-453b-8162-25ec3bb6477c', 1, 678902, 0, 1, 0, N'Truyện tranh Couple Breaker được cập nhật nhanh và đầy đủ nhất tại TruyenQQ. Bạn đọc đừng quên để lại bình luận và chia sẻ, ủng hộ TruyenQQ ra các chương mới nhất của truyện Couple Breaker.', CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-26T17:13:17.9819076+07:00' AS DateTimeOffset), 0, N'2c0f5962-0b53-406d-ad86-8bf3c24d89da.jpg', N'Couple Breaker')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'eb574d4b-c77e-4600-b3f9-1feb27c9210e', 12, N'ae210230-7aa0-4565-adde-bd0509f36cc4', 1, 20, 0, 3, 0, N'Onepunch-Man là một Manga thể loại siêu anh hùng với đặc trưng phồng tôm đấm phát chết luôn… Lol!!! Nhân vật chính trong Onepunch-man là Saitama, một con người mà nhìn đâu cũng thấy “tầm thường”, từ khuôn mặt vô hồn, cái đầu trọc lóc, cho tới thể hình long tong. Tuy nhiên, con người nhìn thì tầm thường này lại chuyên giải quyết những vấn đề hết sức bất thường. Anh thực chất chính là một siêu anh hùng luôn tìm kiếm cho mình một đối thủ mạnh. Vấn đề là, cứ mỗi lần bắt gặp một đối thủ tiềm năng, thì đối thủ nào cũng như đối thủ nào, chỉ ăn một đấm của anh là… chết luôn. Liệu rằng Onepunch-Man Saitaman có thể tìm được cho mình một kẻ ác dữ dằn đủ sức đấu với anh? Hãy theo bước Saitama trên con đường một đấm tìm đối cực kỳ hài hước của anh!!', CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-25T21:58:06.0724373+07:00' AS DateTimeOffset), 1, N'084bc45f-6fda-4d9e-9480-2f9d943cbbe7.jpg', N' Onepunch Man')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'2a188271-735d-4d56-a7be-293a8dcb5827', 9, N'38e73675-74c7-4a67-9e29-23e5103c3200', 0, 23, 0, 3, 0, N'Doraemon bóng chày, tên nguyên bản là Doraemon Bóng chày - Truyền kì về bóng chày siêu cấp là một bộ truyện tranh về bóng chày do Mugiwara Shintaro sáng tác dựa trên hình ảnh chú mèo máy Doraemon của bộ truyện tranh cùng tên của tác giả Fujiko F. Fujio. Truyện được đăng trên tạp chí CoroCoro Comic của Shogakukan.Dù Doraemon có xuất hiện trong một vài chương đầu, câu chuyện lại không xoay quanh cậu và những người bạn ở thế kỷ 20/21, mà lại xoay quanh một đội bóng chày gồm các chú mèo máy ở thế kỷ 22 (vì Doraemon phải trở về quá khứ để giúp Nobita). Trưởng đội bóng là Kuroemon, một mèo máy khá giống Doraemon trừ đôi tai và bộ lông đen (Doraemon không có lông). Trong truyện có nhiều khả năng đánh bóng và ném bóng tưởng tượng, trong một trận bóng, nó cho phép sử dụng 3 bảo bối được qui định từ trước đó.', CAST(N'2022-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-25T21:52:30.2403718+07:00' AS DateTimeOffset), 1, N'72045950-5b5b-4b3f-999e-07fa5543063a.jpg', N'DORABASE (DORAEMON BÓNG CHÀY)')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'0dfabc44-bd5d-49ea-baec-2ca4386ad32e', 4, N'66fa04dc-b9c8-408c-8887-8342471f6b2c', 1, 5, 0, 4, 0, N'Cảnh giới: Luyện nhục cảnh, Luyện cốt cảnh, Luyện tạng cảnh.... La Chính vì gái mà bị đày làm nô bộc. La Bái Nhiên tham vọng đầy mình La Chính lại vì gái mà đâm đầu tu luyện La Gia trong phủ nước sôi lửa bỏng, tranh giành kịch liệt...', CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-20T13:07:58.0957666+07:00' AS DateTimeOffset), 1, N'ce82b1ca-a133-4347-b612-6336abef45fb.jpg', N'BÁCH LUYỆN THÀNH THẦN')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'8c1e3d6e-90d6-465d-9494-3b04136b5d9e', 7, N'6b9c91a4-b91c-4294-89d7-bd5b62d7c27b', 1, 1020000, 0, 3, 0, N'Tỉnh lại sau giấc ngủ, thế giới đại biến. Quen thuộc cao trung truyền thụ chính là phép thuật, nói cho mọi người muốn trở thành một tên xuất sắc Ma Pháp Sư. Ở lại đô thị ở ngoài du đãng tập kích nhân loại ma vật yêu thú, mắt nhìn chằm chằm. Tôn trọng khoa học thế giới đã biến thành tôn trọng phép thuật, một mực có như nhau lấy học tra đối xử giáo viên của chính mình, như nhau ánh mắt dị dạng bạn học, như nhau xã hội tầng dưới chót giãy dụa ba ba, như nhau thuần mỹ nhưng không thể bước đi không phải huyết thống muội muội… Bất quá, Mạc Phàm phát hiện tuyệt đại đa số người đều chỉ có thể chủ tu nhất hệ phép thuật, chính mình nhưng là toàn hệ toàn năng pháp sư!', CAST(N'2022-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-25T21:49:05.2167832+07:00' AS DateTimeOffset), 0, N'5cf34159-50ab-46c2-9b57-8f66f23b2f19.jpg', N'TOÀN CHỨC PHÁP SƯ')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'b0a79ab2-2d61-4bf6-ae7b-404885c678b8', 14, N'c46d83f3-fb07-4b14-ad6e-83f7723ba291', 0, 35566666, 0, 1, 0, N'Câu truyện kể về một cậu bé tên Songoku cùng nhóm bạn của mình tham gia những chuyến phiêu lưu tìm ngọc rồng, chống lại cái ác bảo vệ trái đất. Nhân vật Songoku được mọi người ưa thích bởi tính thánh thiện và ngây ngô của mình. Câu truyện lôi cuốn người qua những tình huống phiêu lưu kì thú, những pha đấu võ đẹp mắt, và những tình huống hài hước.

Dragon ball là bộ truyện tranh thuộc nhóm nổi tiếng nhất thế giới, được rất nhiều bạn trẻ ưa thích.', CAST(N'1999-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-26T16:54:12.6321063+07:00' AS DateTimeOffset), 1, N'329f27ab-021f-42f0-8c95-e88a0f27e022.jpg', N'7 Viên Ngọc Rồng')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'872a9a88-362c-4e18-97f7-59d36747eb36', 3, N'5c579ac2-91a0-4b9f-8324-a86ba98e932e', 1, 230, 0, 2, 0, N'Võ đạo đỉnh phong, là cô độc, là tịch mịch, là dài đằng đẵng cầu tác, là cao xử bất thắng hàn Phát triển trong nghịch cảnh, cầu sinh nơi tuyệt địa, bất khuất không buông tha, mới có thể có thể phá võ chi cực đạo. Lăng Tiêu các thí luyện đệ tử kiêm quét rác gã sai vặt Dương Khai ngẫu lấy được một bản vô tự hắc thư, từ nay về sau đạp vào dài đằng đẵng võ đạo.', CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-17T22:43:55.0654046+07:00' AS DateTimeOffset), 0, N'd6d15444-104b-4cd8-a418-8566fd82ed3e.jpg', N'VÕ LUYỆN ĐỈNH PHONG')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'a2c399d9-3465-4594-a56b-6785cb4814fb', 10, N'b1922b88-1a35-4da4-b112-a57dbb49d87a', 1, 10275, 0, 5, 0, N'Kimetsu no Yaiba – Tanjirou là con cả của gia đình vừa mất cha. Một ngày nọ, Tanjirou đến thăm thị trấn khác để bán than, khi đêm về cậu ở nghỉ tại nhà người khác thay vì về nhà vì lời đồn thổi về ác quỷ luôn rình mò gần núi vào buổi tối. Khi cậu về nhà vào ngày hôm sau, bị kịch đang đợi chờ cậu…', CAST(N'2021-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-25T21:55:30.3567333+07:00' AS DateTimeOffset), 1, N'73a2fdde-f322-496f-8ed7-20de010352fa.jpg', N'Thanh Gươm Diệt Quỷ')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'76fd4451-341b-4bc0-ba5c-70c6923ef45a', 8, N'ed45a6ea-d341-4a0c-aa22-02b13bbcf645', 1, 23, 0, 1, 0, N'Conan và mọi người được mời tới dự buổi biểu diễn khai trương phòng hòa nhạc Domoto do nghệ sĩ piano lừng danh đã giải nghệ xây dựng. Nhưng rồi nhiều sự việc xảy ra đe dọa tính mạng của nữ ca sĩ thiên tài, nhân vật chính của buổi hòa nhạc! Liệu điều này có liên quan gì tới vụ giết người hàng loạt mới xảy ra đã lần lượt cướp đi sinh mạng các học trò của cựu nghệ sĩ piano...?! Và liệu Conan có hiểu được giai điệu, cũng như những nốt nhạc đầy ác ý được diễn tấu? Thám tử Conan - Nốt nhạc kinh hoàng gồm 2 tập. Mời các bạn theo dõi ^_^', CAST(N'2022-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-25T21:51:09.6020628+07:00' AS DateTimeOffset), 1, N'f20bec93-f883-45a3-b13d-5a3cacf3da64.jpg', N'CONAN MÀU - NỐT NHẠC KINH HOÀNG')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'21e8fb34-ca60-456f-9439-811c3ad46c34', 11, N'f79d8ac1-a9dd-430d-acd2-aa5ca9bcc2ea', 0, 13, 0, 2, 0, N'Hơn 100 năm trước, giống người khổng lồ Titan đã tấn công và đẩy loài người tới bờ vực tuyệt chủng. Những con người sống sót tụ tập lại, xây bao quanh mình 1 tòa thành 3 lớp kiên cố và tự nhốt mình bên trong để trốn tránh những cuộc tấn công của người khổng lồ. Họ tìm mọi cách để tiêu diệt người khổng lồ nhưng không thành công. Và sau 1 thế kỉ hòa bình, giống khổng lồ đã xuất hiện trở lại, một lần nữa đe dọa sự tồn vong của con người....  Elen và Mikasa phải chứng kiến một cảnh tượng cực kinh khủng - mẹ của mình bị ăn thịt ngay trước mắt. Elen thề rằng cậu sẽ giết tất cả những tên khổng lồ mà cậu gặp.....', CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-25T21:56:44.0233945+07:00' AS DateTimeOffset), 1, N'de54d04a-4d30-45eb-a9f8-1d22129f0d5e.jpg', N'Đại Chiến Người Khổng Lồ')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'0efebd14-ad6c-4859-88eb-85f01147a8f7', 5, N'73ec328e-c9e5-4351-8552-62468959c767', 1, 1444, 0, 5, 0, N'Ta độ được 999 lần thiên kiếp Chân Hà Đồ, một du khách đến từ thế giới khác. Sau 10 năm tu luyện vẫn chỉ ở Luyện Khí kỳ (8585520/????), không biết bao giờ mới đột phá. Thiên kiếp lần 999, với người tu luyện ai nấy cũng sợ. Cho đến khi sư muội của hắn dẫn đến thì phát hiện ra... hắn thật sự rất mạnh', CAST(N'2022-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-20T14:07:55.5063626+07:00' AS DateTimeOffset), 0, N'7fbf0001-06cf-4592-9432-e41ea33bb030.jpg', N'TA ĐỘ 999 LẦN THIÊN KIẾP')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'd02a7aac-8c33-487a-abb2-8885e94a4fd4', 15, N'dcb5593f-1f66-4d90-a8a4-9450a920ce7e', 0, 443333, 0, 1, 0, N'Nhân vật chính của Bleach là Ichigo Kurosaki có khả năng nhìn thấy những hồn ma. Cuộc sống của cậu thay đổi khi cậu gặp Rukia Kuchiki, một Thần Chết và là thành viên của Âm Giới. Khi chiến đấu với một yêu quái chuyên đi săn những người có năng lực tâm linh, Rukia đã cho Ichigo mượn sức mạnh của mình để cậu có thể cứu gia đình mình. Nhưng trước sự ngạc nhiên của Rukia, Ichigo đã hấp thu toàn bộ sức mạnh của cô. Khi đã trở thành một Thần Chết, Ichigo nhanh chóng biết được rằng thế giới cậu đang sống chứa đầy những linh hồn nguy hiểm, và cùng với Rukia, người đang từ từ khôi phục lại sức mạnh của mình, công việc của Ichigo lúc này là bảo vệ những người vô tội khỏi lũ yêu quái và giúp đỡ những linh hồn tìm được nơi yên nghỉ. Không dừng lại tại đó, trong Bleach, Ichigo sẽ dần phải đụng độ với các tổ chức hùng mạnh, với những âm mưu đan xen để bảo vệ thế giới. Mỗi phần của truyện đều có liên kết chặt chẽ với nhau chứ không tách rời. Đọc Bleach, bạn khó có thể dừng theo dõi các trận chiến hoành tráng và hấp dẫn nối tiếp nhau.', CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-26T16:56:03.0284080+07:00' AS DateTimeOffset), 1, N'9e835e57-6562-456c-b5ca-92300640a1f9.jpg', N'Thần Chết Ichigo')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'847b5348-133f-4a7a-ad8c-c370ecf15996', 13, N'8c8100eb-9608-430d-bacb-a666aa08acc6', 0, 3345, 0, 1, 0, N'Yuuji Itadori là một thiên tài có tốc độ và sức mạnh, nhưng cậu ấy muốn dành thời gian của mình trong Câu lạc bộ Tâm Linh. Một ngày sau cái chết của ông mình, anh gặp Megumi Fushiguro, người đang tìm kiếm vật thể bị nguyền rủa mà các thành viên CLB đã tìm thấy.

 

Đối mặt với những con quái vật khủng khiếp bị "Ám", Yuuji nuốt vật thể bị phong ấn để có được sức mạnh của nó và cứu bạn bè của mình! Nhưng giờ Yuuji là người bị "Ám", và cậu ấy sẽ bị kéo vào thế giới ma quỷ ly kỳ của Megumi và những con quái vật độc ác', CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-26T16:52:23.9266676+07:00' AS DateTimeOffset), 1, N'0ea4aafe-8a25-467c-a851-8e661b96708b.jpg', N'Jujutsu Kaisen - Chú Thuật Hồi Chiến')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'e709378c-b03e-4644-bd70-cba4fa1bc40d', 18, N'fcf4d2c5-3006-4d6c-9cfa-10b2cb4673c8', 0, 2223459, 0, 1, 0, N'Nhân vật chính của truyện là một thám tử học sinh trung học có tên là Kudo Shinichi, người đã bị biến thành một cậu bé cỡ học sinh tiểu học và luôn cố gắng truy tìm tung tích tổ chức Áo Đen nhằm lấy lại hình dáng cũ.', CAST(N'2001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-26T17:11:46.1037244+07:00' AS DateTimeOffset), 1, N'84e1a7bc-aedb-4933-ba45-24d382e1dc76.jpg', N'Thám Tử Conan')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'8edaafba-3ea0-4f38-b9a3-df342827c3fe', 2, N'083ca240-3c07-454f-9b43-85ea12799178', 1, 23455, 0, 4, 0, N'One Piece là câu truyện kể về Luffy và các thuyền viên của mình. Khi còn nhỏ, Luffy ước mơ trở thành Vua Hải Tặc. Cuộc sống của cậu bé thay đổi khi cậu vô tình có được sức mạnh có thể co dãn như cao su, nhưng đổi lại, cậu không bao giờ có thể bơi được nữa. Giờ đây, Luffy cùng những người bạn hải tặc của mình ra khơi tìm kiếm kho báu One Piece, kho báu vĩ đại nhất trên thế giới. Trong One Piece, mỗi nhân vật trong đều mang một nét cá tính đặc sắc kết hợp cùng các tình huống kịch tính, lối dẫn truyện hấp dẫn chứa đầy các bước ngoặt bất ngờ và cũng vô cùng hài hước đã biến One Piece trở thành một trong những bộ truyện nổi tiếng nhất không thể bỏ qua. Hãy đọc One Piece để hòa mình vào một thế giới của những hải tặc rộng lớn, đầy màu sắc, sống động và thú vị, cùng đắm chìm với những nhân vật yêu tự do, trên hành trình đi tìm ước mơ của mình.', CAST(N'2001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-20T15:25:38.6827125+07:00' AS DateTimeOffset), 1, N'97e7da63-16f4-42c2-9400-30dc94c7c5e6.jpg', N'ONE PIECE')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'ae502810-03e2-40cc-a7a4-ecb6365c106c', 6, N'27a8541c-29b4-418f-be3e-6e1594ad835e', 1, 20000, 0, 1, 0, N'Yêu Thần Vừa Xuất, Ai Dám Tranh Phong Yêu Linh Sư Mạnh Nhất Thánh Linh Đại Lục Nhiếp Li, bởi vì một cuốn sách thần bí Yêu Linh Thời Không mà trở về năm 13 tuổi, tu luyện mạnh nhất công pháp, mạnh nhất yêu linh chi lực, đột phá tới võ đạo đỉnh phong... Kẻ thù kiếp trước, toàn bộ thanh toán... Nếu như đã trùng sinh, thì kiếp này ta là chúa tể đại lục, Vạn Thần chi Vương,để cho tất cả run rẩy dưới chân ta đi', CAST(N'2021-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-25T21:47:27.7826959+07:00' AS DateTimeOffset), 1, N'23e3990a-0a37-4220-aa59-e88e14d464c8.jpg', N'YÊU THẦN KÝ')
INSERT [dbo].[Mangas] ([Id], [SubId], [AuthorId], [Status], [ViewCount], [RateCount], [Star], [FollowCount], [Description], [CreatedAt], [ModifiedAt], [IsActive], [Image], [Name]) VALUES (N'153e47f7-a6f1-4912-8b50-f1ac6af42e6a', 16, N'52559b15-3f1c-4d2b-ae37-1e9099e99260', 0, 145, 0, 1, 0, N'Bối cảnh Naruto xảy ra vào mười hai năm trước khi câu chuyện chính thức bắt đầu, một con hồ ly chín đuôi đã tấn công Konohagakure. Nó là một con quái vật có sức mạnh khủng khiếp, chỉ một cái vẫy từ một trong chín cái đuôi của nó có thể tạo ra những cơn sóng thần và san bằng nhiều ngọn núi. Nó gây ra sự hỗn loạn và giết chết rất nhiều người cho đến khi người đứng đầu làng Lá – Hokage đệ tứ – đã đánh bại nó bằng cách đổi lấy mạng sống của mình để phong ấn nó vào trong người một đứa trẻ mới sinh. Đứa trẻ đó tên là Uzumaki Naruto. Bộ truyện kể về cuộc hành trình đầy gian khổ với vô vàn khó khăn, thử thách của Naruto từ khi còn là một cậu bé tới khi trở thành một trong những nhẫn giả vĩ đại nhất. Không chỉ mô tả về một thế giới nhẫn giả huyền bí, Naruto còn mang trong nó nhiều ý nghĩa nhân sinh sâu sắc về tình bạn, tình đồng đội, tình yêu, ước mơ và hi vọng.', CAST(N'2000-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2023-02-26T17:09:32.7709270+07:00' AS DateTimeOffset), 1, N'1101bf63-8568-414a-b570-6791a0da5613.jpg', N'Naruto')
SET IDENTITY_INSERT [dbo].[Mangas] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [SubId], [Description], [Name]) VALUES (N'8ac516e2-fa3b-470d-acb7-6ed7a534280b', 1, N'Admin', N'Admin')
INSERT [dbo].[Roles] ([Id], [SubId], [Description], [Name]) VALUES (N'53428a41-7629-4701-8270-72d8464d9753', 5, N'Normal User Account', N'NormalUser')
INSERT [dbo].[Roles] ([Id], [SubId], [Description], [Name]) VALUES (N'27015dea-6cf7-402a-94de-93e170ab9d7a', 6, N'Vip User', N'VipUser')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([SubId], [UserId], [RoleId]) VALUES (1, N'00000000-0000-0000-0000-000000000000', N'53428a41-7629-4701-8270-72d8464d9753')
INSERT [dbo].[UserRoles] ([SubId], [UserId], [RoleId]) VALUES (2, N'96ed8a95-595e-4af9-96c6-0ac65f1f4128', N'53428a41-7629-4701-8270-72d8464d9753')
INSERT [dbo].[UserRoles] ([SubId], [UserId], [RoleId]) VALUES (3, N'2ac33d9d-7e49-422e-8587-190f6c7bedca', N'8ac516e2-fa3b-470d-acb7-6ed7a534280b')
INSERT [dbo].[UserRoles] ([SubId], [UserId], [RoleId]) VALUES (7, N'87cc072d-c572-4c12-83a8-9cc6d0ed623e', N'27015dea-6cf7-402a-94de-93e170ab9d7a')
INSERT [dbo].[UserRoles] ([SubId], [UserId], [RoleId]) VALUES (6, N'c00037b5-24c0-4255-8620-ac97b53a1c1b', N'27015dea-6cf7-402a-94de-93e170ab9d7a')
INSERT [dbo].[UserRoles] ([SubId], [UserId], [RoleId]) VALUES (5, N'962b2296-5770-441b-a929-bbd15786c906', N'53428a41-7629-4701-8270-72d8464d9753')
INSERT [dbo].[UserRoles] ([SubId], [UserId], [RoleId]) VALUES (4, N'fa413bd3-52a0-4ef3-bfe4-ee903a92ceb6', N'27015dea-6cf7-402a-94de-93e170ab9d7a')
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [SubId], [FullName], [Email], [EmailConfirmed], [Password], [PhoneNumber], [PhoneNumberConfirmed], [AccessFailedCount], [CreatedAt], [ModifiedAt], [IsActive], [Status], [Avatar]) VALUES (N'00000000-0000-0000-0000-000000000000', 1, N'Van Trai Chu', N'trai123trai@gmail.com', 1, N'$2a$10$Jg/YXjudLQYGUT0Ubrgyzu7BPc26typhZ51.JyoURXYZxj6uDTRL6', N'0362351671', 1, 0, CAST(N'2023-02-07T02:06:10.7830969+07:00' AS DateTimeOffset), CAST(N'2023-02-07T02:06:10.7841144+07:00' AS DateTimeOffset), 1, 1, NULL)
INSERT [dbo].[Users] ([Id], [SubId], [FullName], [Email], [EmailConfirmed], [Password], [PhoneNumber], [PhoneNumberConfirmed], [AccessFailedCount], [CreatedAt], [ModifiedAt], [IsActive], [Status], [Avatar]) VALUES (N'96ed8a95-595e-4af9-96c6-0ac65f1f4128', 2, N'Van Trai Chu', N'trai222@gmail.com', 0, N'$2a$10$SFW3byLv1drI9RBmEUmJ1ObY5qkAIpa9w9P3AS78q3g21zMY7sfpS', N'0362351671', 1, 0, CAST(N'2023-02-07T02:10:00.7703388+07:00' AS DateTimeOffset), CAST(N'2023-02-07T02:10:00.7718821+07:00' AS DateTimeOffset), 1, 0, NULL)
INSERT [dbo].[Users] ([Id], [SubId], [FullName], [Email], [EmailConfirmed], [Password], [PhoneNumber], [PhoneNumberConfirmed], [AccessFailedCount], [CreatedAt], [ModifiedAt], [IsActive], [Status], [Avatar]) VALUES (N'2ac33d9d-7e49-422e-8587-190f6c7bedca', 3, N'Admin31111', N'Admin111@gmail.com', 1, N'$2a$10$vT8l1WYePLAZg7PTSmNs/Ov7GhvL2Xal.zIMhnhp3QPUR5cCnmgfm', N'0362351672', 1, 0, CAST(N'2023-02-09T01:15:26.1461329+07:00' AS DateTimeOffset), CAST(N'2023-02-09T01:15:26.1477480+07:00' AS DateTimeOffset), 1, 1, N'97da27e7-0c01-4294-974f-a2460625955a.png')
INSERT [dbo].[Users] ([Id], [SubId], [FullName], [Email], [EmailConfirmed], [Password], [PhoneNumber], [PhoneNumberConfirmed], [AccessFailedCount], [CreatedAt], [ModifiedAt], [IsActive], [Status], [Avatar]) VALUES (N'87cc072d-c572-4c12-83a8-9cc6d0ed623e', 7, N'Van Trai Chu2222', N'bne13160@omeie.com', 1, N'$2a$10$7i3DbsgOF1zTNchTN8uvoOgclEBjmAnxq7foLIt0fkvX.Sufbbq9e', N'0362351671', 1, 0, CAST(N'2023-08-22T13:44:51.1694147+07:00' AS DateTimeOffset), CAST(N'2023-08-22T13:44:51.1694723+07:00' AS DateTimeOffset), 1, 0, NULL)
INSERT [dbo].[Users] ([Id], [SubId], [FullName], [Email], [EmailConfirmed], [Password], [PhoneNumber], [PhoneNumberConfirmed], [AccessFailedCount], [CreatedAt], [ModifiedAt], [IsActive], [Status], [Avatar]) VALUES (N'c00037b5-24c0-4255-8620-ac97b53a1c1b', 6, N'Van Trai Chu', N'cqj00646@zslsz.com', 0, N'$2a$10$TtTqUMA0winRZBd/.8y2tOocTsmEWxKFzumSJgc1C8dRA52f/O2Ce', N'0362351671', 1, 0, CAST(N'2023-08-22T10:52:00.1060926+07:00' AS DateTimeOffset), CAST(N'2023-08-22T10:52:00.1089390+07:00' AS DateTimeOffset), 1, 0, NULL)
INSERT [dbo].[Users] ([Id], [SubId], [FullName], [Email], [EmailConfirmed], [Password], [PhoneNumber], [PhoneNumberConfirmed], [AccessFailedCount], [CreatedAt], [ModifiedAt], [IsActive], [Status], [Avatar]) VALUES (N'962b2296-5770-441b-a929-bbd15786c906', 5, N'Van 112331', N'rmm11391@omeie.com', 0, N'$2a$10$vNKzPz68uk3by0ew2ZzqS.hHAW8iVTlaIfuZpeKBDDAa2oAhl0MJu', N'0362351671', 1, 0, CAST(N'2023-08-18T11:13:16.5069537+07:00' AS DateTimeOffset), CAST(N'2023-08-18T11:13:16.5070098+07:00' AS DateTimeOffset), 1, 0, NULL)
INSERT [dbo].[Users] ([Id], [SubId], [FullName], [Email], [EmailConfirmed], [Password], [PhoneNumber], [PhoneNumberConfirmed], [AccessFailedCount], [CreatedAt], [ModifiedAt], [IsActive], [Status], [Avatar]) VALUES (N'fa413bd3-52a0-4ef3-bfe4-ee903a92ceb6', 4, N'Van Trai Chu', N'admin222@gmail.com', 1, N'$2a$10$X4/Cjcc52ufR1yhcIL/vtOHCS6RgNq3cjEtWudKoybQOkBtO0QtMK', N'0362351671', 1, 0, CAST(N'2023-02-09T01:18:24.5036568+07:00' AS DateTimeOffset), CAST(N'2023-02-09T01:18:24.5052372+07:00' AS DateTimeOffset), 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
INSERT [dbo].[UserTokens] ([Id], [UserId], [Email], [Expires], [Value]) VALUES (N'00000000-0000-0000-0000-000000000000', N'00000000-0000-0000-0000-000000000000', N'trai123trai@gmail.com', CAST(N'2023-03-08 19:06:10.9444935' AS DateTime2), N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiTm9ybWFsVXNlciIsIm5hbWUiOiJWYW4gVHJhaSBDaHUiLCJlbWFpbCI6InRyYWkxMjN0cmFpQGdtYWlsLmNvbSIsImV4cCI6IjIwMjMtMDMtMDhUMTk6MDY6MTAuODcxOTI1M1oiLCJpYXQiOiIyMDIzLTAyLTA2VDE5OjA2OjEwLjg3MTkyODlaIn0.wkUYOgiB3vAi27BwOkJJp0s3XUurBnPNYX6ZeHRMBC4')
INSERT [dbo].[UserTokens] ([Id], [UserId], [Email], [Expires], [Value]) VALUES (N'7c748197-0d68-420a-8989-d69a9aa9fe90', N'96ed8a95-595e-4af9-96c6-0ac65f1f4128', N'trai222@gmail.com', CAST(N'2023-03-08 19:10:00.9268625' AS DateTime2), N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiTm9ybWFsVXNlciIsIm5hbWUiOiJWYW4gVHJhaSBDaHUiLCJlbWFpbCI6InRyYWkyMjJAZ21haWwuY29tIiwiZXhwIjoiMjAyMy0wMy0wOFQxOToxMDowMC44NTI3MjM4WiIsImlhdCI6IjIwMjMtMDItMDZUMTk6MTA6MDAuODUyNzI3NFoifQ.qNAK4vO6tSHa5QlK6Tjj5Dr-FRdmPXy-BZRnx6kcCfA')
INSERT [dbo].[UserTokens] ([Id], [UserId], [Email], [Expires], [Value]) VALUES (N'bc2391eb-856d-405d-adb4-55944494e163', N'2ac33d9d-7e49-422e-8587-190f6c7bedca', N'Admin111@gmail.com', CAST(N'2023-03-10 18:15:26.3067005' AS DateTime2), N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYW1lIjoiQWRtaW4xMTEiLCJlbWFpbCI6IkFkbWluMTExQGdtYWlsLmNvbSIsImV4cCI6IjIwMjMtMDMtMTBUMTg6MTU6MjYuMjMyMDUyMloiLCJpYXQiOiIyMDIzLTAyLTA4VDE4OjE1OjI2LjIzMjA1NTdaIn0.7BMaYRaz1xH7staLyVFZ7AM8sruuK1SrdYGuvDzA26U')
INSERT [dbo].[UserTokens] ([Id], [UserId], [Email], [Expires], [Value]) VALUES (N'b72ff6f5-a94b-4369-8a71-076ecbd87fd4', N'87cc072d-c572-4c12-83a8-9cc6d0ed623e', N'bne13160@omeie.com', CAST(N'2023-08-29 14:24:17.1832466' AS DateTime2), N'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiVmFuIFRyYWkgQ2h1MjIyMiIsImVtYWlsIjoiYm5lMTMxNjBAb21laWUuY29tIiwicm9sZSI6Ik5vcm1hbFVzZXIiLCJleHAiOjE2OTUyNzg2OTEsImlzcyI6Im1hbmdhb25saW5lIiwiYXVkIjoibWFuZ2FvbmxpbmUifQ.rKsuDTShlFITbKe_QtImaimOSHBVStDTAHmFEtEsdzY')
INSERT [dbo].[UserTokens] ([Id], [UserId], [Email], [Expires], [Value]) VALUES (N'8434d7f2-f503-4d03-8be4-7c49b2bd7de1', N'c00037b5-24c0-4255-8620-ac97b53a1c1b', N'cqj00646@zslsz.com', CAST(N'2023-09-21 13:36:09.4667823' AS DateTime2), N'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiVmFuIFRyYWkgQ2h1IiwiZW1haWwiOiJjcWowMDY0NkB6c2xzei5jb20iLCJyb2xlIjoiTm9ybWFsVXNlciIsImV4cCI6MTY5NTI2ODMyMCwiaXNzIjoibWFuZ2FvbmxpbmUiLCJhdWQiOiJtYW5nYW9ubGluZSJ9.338RrTaJGxb2h_DHx60Tkp1N2bbJntYQy_kX46bOLMA')
INSERT [dbo].[UserTokens] ([Id], [UserId], [Email], [Expires], [Value]) VALUES (N'871dba25-883c-4696-a7b5-a075e353f728', N'962b2296-5770-441b-a929-bbd15786c906', N'rmm11391@omeie.com', CAST(N'2023-09-17 04:13:16.6897792' AS DateTime2), N'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiVmFuIDExMjMzMSIsImVtYWlsIjoicm1tMTEzOTFAb21laWUuY29tIiwicm9sZSI6Ik5vcm1hbFVzZXIiLCJleHAiOjE2OTQ5MjM5OTYsImlzcyI6Im1hbmdhb25saW5lIiwiYXVkIjoibWFuZ2FvbmxpbmUifQ.GC43UzunllntED5nVlLnEfjNq-tWqx96iLaVNyE9Uug')
INSERT [dbo].[UserTokens] ([Id], [UserId], [Email], [Expires], [Value]) VALUES (N'e87d8b60-3406-4efa-b3d5-dac47915db26', N'fa413bd3-52a0-4ef3-bfe4-ee903a92ceb6', N'admin222@gmail.com', CAST(N'2022-03-30 18:18:24.6641214' AS DateTime2), N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYW1lIjoiVmFuIFRyYWkgQ2h1IiwiZW1haWwiOiJhZG1pbjIyMkBnbWFpbC5jb20iLCJleHAiOiIyMDIzLTAzLTEwVDE4OjE4OjI0LjU4OTk0NDZaIiwiaWF0IjoiMjAyMy0wMi0wOFQxODoxODoyNC41ODk5NDg0WiJ9.jL4sDEefc5eQBdB_kccQw1hOxMWTWfZvvkXvZT1gso0')
ALTER TABLE [dbo].[CategoryManga]  WITH CHECK ADD  CONSTRAINT [FK_CategoryManga_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[CategoryManga] CHECK CONSTRAINT [FK_CategoryManga_Categories]
GO
ALTER TABLE [dbo].[CategoryManga]  WITH CHECK ADD  CONSTRAINT [FK_CategoryManga_Mangas] FOREIGN KEY([MangaId])
REFERENCES [dbo].[Mangas] ([Id])
GO
ALTER TABLE [dbo].[CategoryManga] CHECK CONSTRAINT [FK_CategoryManga_Mangas]
GO
ALTER TABLE [dbo].[Chapteres]  WITH CHECK ADD  CONSTRAINT [FK_Chapteres_Mangas] FOREIGN KEY([MangaId])
REFERENCES [dbo].[Mangas] ([Id])
GO
ALTER TABLE [dbo].[Chapteres] CHECK CONSTRAINT [FK_Chapteres_Mangas]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Mangas] FOREIGN KEY([MangaId])
REFERENCES [dbo].[Mangas] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Mangas]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[FollowList]  WITH CHECK ADD  CONSTRAINT [FK_FollowList_Mangas] FOREIGN KEY([MangaId])
REFERENCES [dbo].[Mangas] ([Id])
GO
ALTER TABLE [dbo].[FollowList] CHECK CONSTRAINT [FK_FollowList_Mangas]
GO
ALTER TABLE [dbo].[FollowList]  WITH CHECK ADD  CONSTRAINT [FK_FollowList_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[FollowList] CHECK CONSTRAINT [FK_FollowList_Users]
GO
ALTER TABLE [dbo].[Mangas]  WITH CHECK ADD  CONSTRAINT [FK_Mangas_Authors] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO
ALTER TABLE [dbo].[Mangas] CHECK CONSTRAINT [FK_Mangas_Authors]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Chapteres] FOREIGN KEY([ChapterId])
REFERENCES [dbo].[Chapteres] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Chapteres]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Chapteres] FOREIGN KEY([ChapterId])
REFERENCES [dbo].[Chapteres] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Chapteres]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users]
GO
