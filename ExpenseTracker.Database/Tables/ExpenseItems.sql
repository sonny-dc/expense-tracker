CREATE TABLE [dbo].[ExpenseItems]
(
    [ExpenseItemId] INT IDENTITY(1, 1) NOT NULL,
    [ExpenseEntryId] INT NOT NULL,
    [ItemId] INT NULL,

    [ItemNameSnapshot] NVARCHAR(100) NOT NULL,
    [ItemCodeSnapshot] NVARCHAR(30) NOT NULL,
    [BrandSnapshot] NVARCHAR(100) NOT NULL,

    [Quantity] DECIMAL(18, 3) NOT NULL,
    [UnitPriceSnapshot] DECIMAL(18, 2) NOT NULL,
    [LineTotal] DECIMAL(18, 2) NOT NULL,

    CONSTRAINT [PK_ExpenseItems]
        PRIMARY KEY ([ExpenseItemId]),

    CONSTRAINT [FK_ExpenseItems_ExpenseEntries]
        FOREIGN KEY ([ExpenseEntryId])
        REFERENCES [dbo].[ExpenseEntries] ([ExpenseEntryId])
        ON DELETE CASCADE,

    CONSTRAINT [FK_ExpenseItems_Items]
        FOREIGN KEY ([ItemId])
        REFERENCES [dbo].[Items] ([ItemId])
        ON DELETE SET NULL,

    CONSTRAINT [CK_ExpenseItems_Quantity]
        CHECK ([Quantity] > 0),

    CONSTRAINT [CK_ExpenseItems_UnitPriceSnapshot]
        CHECK ([UnitPriceSnapshot] >= 0),

    CONSTRAINT [CK_ExpenseItems_LineTotal]
        CHECK ([LineTotal] >= 0)
);
