SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Authors](
	[author_ID] [uniqueidentifier] NOT NULL,
	[author_First_Name] [varchar](50) NOT NULL,
	[author_Initials] [nvarchar](50) NOT NULL,
	[author_Last_Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[author_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Book_Category](
	[book_category_ID] [uniqueidentifier] NOT NULL,
	[CategoryCode] [nvarchar](50) NOT NULL,
	[book_category_Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Book_Category] PRIMARY KEY CLUSTERED 
(
	[book_category_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Books](
	[book_ID] [uniqueidentifier] NOT NULL,
	[ISBN] [nvarchar](50) NOT NULL,
	[book_title] [nvarchar](200) NOT NULL,
	[publicationDate] [date] NOT NULL,
	[book_category_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[book_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AuthorBook](
	[Id] [uniqueidentifier] NOT NULL,
	[author_ID] [uniqueidentifier] NOT NULL,
	[book_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AuthorBook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Customers](
	[customer_ID] [uniqueidentifier] NOT NULL,
	[customer_Code] [nvarchar](50) NOT NULL,
	[customer_name] [nvarchar](50) NOT NULL,
	[customer_Address] [nvarchar](50) NULL,
	[customer_Phone] [nvarchar](50) NULL,
	[customer_Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[customer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Orders](
	[order_ID] [uniqueidentifier] NOT NULL,
	[customer_ID] [uniqueidentifier] NOT NULL,
	[order_date] [datetime] NOT NULL,
	[order_Value] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[order_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Order_Items](
	[order_Item_ID] [uniqueidentifier] NOT NULL,
	[order_ID] [uniqueidentifier] NOT NULL,
	[book_ID] [uniqueidentifier] NOT NULL,
	[Item_Number] [int] NOT NULL,
	[Item_Agreed_Price] [decimal](18, 2) NOT NULL,
	[Item_comment] [varchar](200) NULL,
 CONSTRAINT [PK_Order_Items] PRIMARY KEY CLUSTERED 
(
	[order_Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
INSERT [Authors] ( [author_First_Name], [author_Initials], [author_Last_Name]) VALUES ( N'Test First Name', N'Dr', N'Test Last Name')
GO
INSERT [Book_Category] ( [CategoryCode], [book_category_Description]) VALUES ( N'BCode-001', N'Category Test')
GO
INSERT [Customers] ( [customer_Code], [customer_name], [customer_Address], [customer_Phone], [customer_Email]) VALUES ( N'C-001', N'Test Customer', N'Test Address', N'0450536895', N'test@gmail.com')
GO
DECLARE @CategoryID UniqueIdentifier,
        @AuthorID Uniqueidentifier,
		@BookID  Uniqueidentifier,
		@customerID Uniqueidentifier,
		@orderID Uniqueidentifier 
SELECT @CategoryID = [book_category_ID] FROM Book_Category WHERE CategoryCode = N'BCode-001'
SELECT @AuthorID = [author_ID] FROM Authors WHERE [author_First_Name] =  N'Test First Name'
INSERT [Books] ( [ISBN], [book_title], [publicationDate], [book_category_ID]) VALUES ( N'123456789001', N'Test Book Title', CAST(N'1979-05-30' AS Date), @CategoryID)
SELECT @BookID = [book_ID] FROM Books WHERE book_title = N'Test Book Title'
INSERT [AuthorBook] ( [author_ID], [book_ID]) VALUES ( @AuthorID, @BookID)
SELECT @customerID = [customer_ID] FROM Customers WHERE [customer_Code] =  N'C-001'
INSERT [Orders] ( [customer_ID], [order_date], [order_Value]) VALUES (@customerID,  CAST(N'2019-08-21T00:00:00.000' AS DateTime), CAST(200.00 AS Decimal(18, 2)))
SELECT @orderID = [order_ID] FROM Orders WHERE customer_ID = @customerID 
INSERT [Order_Items] ( [order_ID], [book_ID], [Item_Number], [Item_Agreed_Price], [Item_comment]) VALUES ( @orderID, @BookID, 1, CAST(200.00 AS Decimal(18, 2)), N'Test')
GO

ALTER TABLE [AuthorBook] ADD  CONSTRAINT [DF_AuthorBook_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [Authors] ADD  CONSTRAINT [DF_Authors_author_ID]  DEFAULT (newid()) FOR [author_ID]
GO
ALTER TABLE [Book_Category] ADD  CONSTRAINT [DF_Book_Category_book_category_ID]  DEFAULT (newid()) FOR [book_category_ID]
GO
ALTER TABLE [Books] ADD  CONSTRAINT [DF_Books_book_ID]  DEFAULT (newid()) FOR [book_ID]
GO
ALTER TABLE [Customers] ADD  CONSTRAINT [DF_Customers_customer_ID]  DEFAULT (newid()) FOR [customer_ID]
GO
ALTER TABLE [Order_Items] ADD  CONSTRAINT [DF_Order_Items_order_Item_ID]  DEFAULT (newid()) FOR [order_Item_ID]
GO
ALTER TABLE [Orders] ADD  CONSTRAINT [DF_Orders_order_ID]  DEFAULT (newid()) FOR [order_ID]
GO
ALTER TABLE [Orders] ADD  CONSTRAINT [DF_Orders_order_Value]  DEFAULT ((0)) FOR [order_Value]
GO
ALTER TABLE [AuthorBook]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBook_Authors] FOREIGN KEY([author_ID])
REFERENCES [Authors] ([author_ID])
GO
ALTER TABLE [AuthorBook] CHECK CONSTRAINT [FK_AuthorBook_Authors]
GO
ALTER TABLE [AuthorBook]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBook_Books] FOREIGN KEY([book_ID])
REFERENCES [Books] ([book_ID])
GO
ALTER TABLE [AuthorBook] CHECK CONSTRAINT [FK_AuthorBook_Books]
GO
ALTER TABLE [Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Book_Category] FOREIGN KEY([book_category_ID])
REFERENCES [Book_Category] ([book_category_ID])
GO
ALTER TABLE [Books] CHECK CONSTRAINT [FK_Books_Book_Category]
GO
ALTER TABLE [Order_Items]  WITH CHECK ADD  CONSTRAINT [FK_Order_Items_Books] FOREIGN KEY([book_ID])
REFERENCES [Books] ([book_ID])
GO
ALTER TABLE [Order_Items] CHECK CONSTRAINT [FK_Order_Items_Books]
GO
ALTER TABLE [Order_Items]  WITH CHECK ADD  CONSTRAINT [FK_Order_Items_Orders] FOREIGN KEY([order_ID])
REFERENCES [Orders] ([order_ID])
GO
ALTER TABLE [Order_Items] CHECK CONSTRAINT [FK_Order_Items_Orders]
GO
ALTER TABLE [Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([customer_ID])
REFERENCES [Customers] ([customer_ID])
GO
ALTER TABLE [Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
