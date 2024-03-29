USE [master]
GO
/****** Object:  Database [LowPriceDB]    Script Date: 10/22/2019 1:09:50 PM ******/
CREATE DATABASE [LowPriceDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LowPriceDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\LowPriceDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LowPriceDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\LowPriceDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LowPriceDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LowPriceDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LowPriceDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LowPriceDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LowPriceDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LowPriceDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LowPriceDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LowPriceDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LowPriceDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LowPriceDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LowPriceDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LowPriceDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LowPriceDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LowPriceDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LowPriceDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LowPriceDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LowPriceDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LowPriceDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LowPriceDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LowPriceDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LowPriceDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LowPriceDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LowPriceDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LowPriceDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LowPriceDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LowPriceDB] SET  MULTI_USER 
GO
ALTER DATABASE [LowPriceDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LowPriceDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LowPriceDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LowPriceDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LowPriceDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LowPriceDB] SET QUERY_STORE = OFF
GO
USE [LowPriceDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Measure]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measure](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Measure] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Price]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ApplyDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Value] [float] NOT NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MeasureId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[CategoryId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPromotion]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPromotion](
	[ProductId] [int] NOT NULL,
	[PromotionId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ProductPromotion] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[PromotionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PromotionTypeId] [int] NULL,
	[Name] [nvarchar](150) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[RequiredQuantity] [int] NULL,
	[DiscountQuantity] [int] NULL,
	[Saleoff] [float] NULL,
	[IsActive] [bit] NOT NULL,
	[OnlyMembership] [bit] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PromotionType]    Script Date: 10/22/2019 1:09:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromotionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PromotionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [IsActive]) VALUES (1, N'Meat', 1)
INSERT [dbo].[Category] ([Id], [Name], [IsActive]) VALUES (2, N'Vegie', 1)
INSERT [dbo].[Category] ([Id], [Name], [IsActive]) VALUES (3, N'Household items', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Measure] ON 

INSERT [dbo].[Measure] ([Id], [Name], [IsActive]) VALUES (1, N'kg', 1)
INSERT [dbo].[Measure] ([Id], [Name], [IsActive]) VALUES (2, N'unit', 1)
SET IDENTITY_INSERT [dbo].[Measure] OFF
SET IDENTITY_INSERT [dbo].[Price] ON 

INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (1, 1, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 200000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (2, 2, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 140000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (5, 3, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 90000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (6, 4, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 20900)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (7, 5, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 29900)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (8, 6, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 10000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (9, 7, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 32000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (10, 8, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 68000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (11, 9, CAST(N'2019-10-21T00:00:00.000' AS DateTime), 1, 37000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (12, 3, CAST(N'2019-10-21T15:51:48.937' AS DateTime), 1, 92000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (13, 7, CAST(N'2019-10-21T15:52:00.067' AS DateTime), 1, 30000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (14, 3, CAST(N'2019-10-21T15:56:13.450' AS DateTime), 1, 92000)
SET IDENTITY_INSERT [dbo].[Price] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (1, N'Beef', N'M1', 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (2, N'Chicken', N'M2', 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (3, N'Pork', N'M3', 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (4, N'Tomato', N'V1', 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (5, N'Potato', N'V2', 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (6, N'Bean sprouts', N'V3', 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (7, N'Bowl', N'H1', 1, 2)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (8, N'Pan', N'H2', 1, 2)
INSERT [dbo].[Product] ([Id], [Name], [Code], [IsActive], [MeasureId]) VALUES (9, N'Chopstick', N'H3', 1, 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (1, 1, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (1, 2, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (1, 3, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (2, 4, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (2, 5, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (2, 6, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (3, 7, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (3, 8, 1)
INSERT [dbo].[ProductCategory] ([CategoryId], [ProductId], [IsActive]) VALUES (3, 9, 1)
SET IDENTITY_INSERT [dbo].[PromotionType] ON 

INSERT [dbo].[PromotionType] ([Id], [Name], [Description], [IsActive]) VALUES (1, N'Buy X , Give Y', N'Cannot truncate table ''Promotion'' because it is being referenced by a FOREIGN KEY constraint.
', 1)
INSERT [dbo].[PromotionType] ([Id], [Name], [Description], [IsActive]) VALUES (2, N'SaleOff X% of Price', N'Sale off X% of the price (applicable to all types of products)', 1)
SET IDENTITY_INSERT [dbo].[PromotionType] OFF
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Measure] FOREIGN KEY([MeasureId])
REFERENCES [dbo].[Measure] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Measure]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Category]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Product]
GO
ALTER TABLE [dbo].[ProductPromotion]  WITH CHECK ADD  CONSTRAINT [FK_ProductPromotion_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductPromotion] CHECK CONSTRAINT [FK_ProductPromotion_Product]
GO
ALTER TABLE [dbo].[ProductPromotion]  WITH CHECK ADD  CONSTRAINT [FK_ProductPromotion_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotion] ([Id])
GO
ALTER TABLE [dbo].[ProductPromotion] CHECK CONSTRAINT [FK_ProductPromotion_Promotion]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_PromotionType] FOREIGN KEY([PromotionTypeId])
REFERENCES [dbo].[PromotionType] ([Id])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_PromotionType]
GO
USE [master]
GO
ALTER DATABASE [LowPriceDB] SET  READ_WRITE 
GO
