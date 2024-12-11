IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [Category] (
        [CategoryID] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [Role] (
        [RoleId] int NOT NULL IDENTITY,
        [RoleName] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [Style] (
        [StyleId] int NOT NULL IDENTITY,
        [StyleName] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Style] PRIMARY KEY ([StyleId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [User] (
        [UserName] nvarchar(450) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [RoleId] int NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([UserName]),
        CONSTRAINT [FK_User_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([RoleId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [Product] (
        [ProductId] int NOT NULL IDENTITY,
        [ProductCode] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NULL,
        [ProductName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [StockQuantity] int NOT NULL,
        [CategoryId] int NOT NULL,
        [StyleId] int NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Price] float NOT NULL,
        [Ingredient] nvarchar(max) NOT NULL,
        [Warning] nvarchar(max) NOT NULL,
        [CreateTime] datetime2 NOT NULL,
        [Volume] float NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId]),
        CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Product_Style_StyleId] FOREIGN KEY ([StyleId]) REFERENCES [Style] ([StyleId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [Order] (
        [OrderId] int NOT NULL IDENTITY,
        [Status] nvarchar(max) NOT NULL,
        [TotalPrice] float NOT NULL,
        [UserName] nvarchar(450) NOT NULL,
        [OrderDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([OrderId]),
        CONSTRAINT [FK_Order_User_UserName] FOREIGN KEY ([UserName]) REFERENCES [User] ([UserName]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [Feedback] (
        [FeedbackId] int NOT NULL IDENTITY,
        [Rating] int NOT NULL,
        [UserName] nvarchar(450) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Comment] nvarchar(max) NULL,
        [FeedbackTime] datetime2 NULL,
        [ProductId] int NOT NULL,
        CONSTRAINT [PK_Feedback] PRIMARY KEY ([FeedbackId]),
        CONSTRAINT [FK_Feedback_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Feedback_User_UserName] FOREIGN KEY ([UserName]) REFERENCES [User] ([UserName]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE TABLE [OrderDetail] (
        [OrderDetailId] int NOT NULL IDENTITY,
        [Quantity] int NOT NULL,
        [TotalPrice] float NOT NULL,
        [ProductId] int NOT NULL,
        [OrderId] int NOT NULL,
        CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([OrderDetailId]),
        CONSTRAINT [FK_OrderDetail_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([OrderId]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderDetail_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_Feedback_ProductId] ON [Feedback] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_Feedback_UserName] ON [Feedback] ([UserName]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_Order_UserName] ON [Order] ([UserName]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_OrderDetail_OrderId] ON [OrderDetail] ([OrderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_OrderDetail_ProductId] ON [OrderDetail] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_Product_CategoryId] ON [Product] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_Product_StyleId] ON [Product] ([StyleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    CREATE INDEX [IX_User_RoleId] ON [User] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211102_asdadadadasd'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241122211102_asdadadadasd', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241122211454_lalalalal'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241122211454_lalalalal', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241123084100_kiojjkoko'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Password');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [User] ALTER COLUMN [Password] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241123084100_kiojjkoko'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Email');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [User] ALTER COLUMN [Email] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241123084100_kiojjkoko'
)
BEGIN
    ALTER TABLE [User] ADD [DOB] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241123084100_kiojjkoko'
)
BEGIN
    ALTER TABLE [User] ADD [FullName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241123084100_kiojjkoko'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241123084100_kiojjkoko', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241130100054_kjkjkjkj'
)
BEGIN
    ALTER TABLE [Product] ADD [Discount] float NOT NULL DEFAULT 0.0E0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241130100054_kjkjkjkj'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241130100054_kjkjkjkj', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241206013901_kokokoklll'
)
BEGIN
    ALTER TABLE [User] ADD [Image] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241206013901_kokokoklll'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241206013901_kokokoklll', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241206013934_kisaskkk'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241206013934_kisaskkk', N'8.0.11');
END;
GO

COMMIT;
GO

