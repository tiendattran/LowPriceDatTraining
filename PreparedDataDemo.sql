USE [LowPriceDB]
GO
SET IDENTITY_INSERT [dbo].[Promotion] ON 

INSERT [dbo].[Promotion] ([Id], [PromotionTypeId], [Name], [StartDate], [EndDate], [RequiredQuantity], [DiscountQuantity], [Saleoff], [IsActive], [OnlyMembership]) VALUES (27, 2, N'Membership Program', CAST(N'2019-10-22T00:00:00.000' AS DateTime), CAST(N'2019-11-01T00:00:00.000' AS DateTime), NULL, NULL, 10, 1, 1)
INSERT [dbo].[Promotion] ([Id], [PromotionTypeId], [Name], [StartDate], [EndDate], [RequiredQuantity], [DiscountQuantity], [Saleoff], [IsActive], [OnlyMembership]) VALUES (28, 2, N'Healthy Program', CAST(N'2019-10-22T00:00:00.000' AS DateTime), CAST(N'2019-11-01T00:00:00.000' AS DateTime), NULL, NULL, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (1, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (2, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (3, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (4, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (4, 28, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (5, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (5, 28, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (6, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (6, 28, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (7, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (8, 27, 1)
INSERT [dbo].[ProductPromotion] ([ProductId], [PromotionId], [IsActive]) VALUES (9, 27, 1)
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
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (14, 3, CAST(N'2019-10-21T15:56:13.450' AS DateTime), 1, 90000)
INSERT [dbo].[Price] ([Id], [ProductId], [ApplyDate], [IsActive], [Value]) VALUES (15, 7, CAST(N'2019-10-22T14:23:39.477' AS DateTime), 1, 30000)
SET IDENTITY_INSERT [dbo].[Price] OFF
