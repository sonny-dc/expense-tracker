CREATE TABLE [dbo].[Items]
(
    [ItemId] INT IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Code] NVARCHAR(30) NOT NULL,
    [Brand] NVARCHAR(100) NOT NULL,
    [UnitPrice] DECIMAL(18, 2) NOT NULL,

    CONSTRAINT [PK_Items]
        PRIMARY KEY ([ItemId]),

    CONSTRAINT [UQ_Items_Name_Code_Brand]
        UNIQUE ([Name], [Code], [Brand]),

    CONSTRAINT [CK_Items_UnitPrice]
        CHECK ([UnitPrice] >= 0)
);
