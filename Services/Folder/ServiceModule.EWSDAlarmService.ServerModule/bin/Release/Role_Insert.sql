USE [TMN.Classic]
GO

INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Admins', N'مدير سيستم', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Centers_Add', N'درج مرکز', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Centers_Delete', N'حذف مرکز', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Centers_Edit', N'ویرایش مرکز', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Sensors', N'سنسورها', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Settings', N'تنظیمات', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Shifts', N'شیفت', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Users', N'کاربران', NULL)
INSERT [dbo].[Role] ([ID], [Name], [Description]) VALUES (N'Alarms_Delete', N'حذف آلارم', NULL)
GO

