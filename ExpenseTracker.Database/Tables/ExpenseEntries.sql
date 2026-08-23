CREATE TABLE [dbo].[ExpenseEntries]
(
    [ExpenseEntryId] INT IDENTITY(1, 1) NOT NULL,
    [ExpenseDateTime] DATETIME2(0) NOT NULL
        CONSTRAINT [DF_ExpenseEntries_ExpenseDateTime]
        DEFAULT SYSUTCDATETIME(),
    [TotalCost] DECIMAL(18, 2) NOT NULL,
    [Notes] NVARCHAR(500) NULL,

    CONSTRAINT [PK_ExpenseEntries]
        PRIMARY KEY ([ExpenseEntryId]),

    CONSTRAINT [CK_ExpenseEntries_TotalCost]
        CHECK ([TotalCost] >= 0)
);
