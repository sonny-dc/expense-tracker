CREATE TABLE [dbo].[BudgetAccountLogs]
(
    [BudgetAccountLogId] INT IDENTITY(1, 1) NOT NULL,
    [BudgetAccountId] INT NOT NULL,
    [ExpenseEntryId] INT NULL,
    [Amount] DECIMAL(18, 2) NOT NULL,
    [BalanceBefore] DECIMAL(18, 2) NOT NULL,
    [BalanceAfter] DECIMAL(18, 2) NOT NULL,
    [LogType] VARCHAR(20) NOT NULL CHECK (LogType IN ('StartingBalance', 'Expense', 'Adjustment')),
    [LogDateTime] DATETIME2(0) NOT NULL
        CONSTRAINT [DF_BudgetAccountLogs_LogDateTime]
        DEFAULT SYSUTCDATETIME(),
    
    CONSTRAINT [PK_BudgetAccountLogs]
        PRIMARY KEY ([BudgetAccountLogId]),

    CONSTRAINT [FK_BudgetAccountLogs_BudgetAccounts]
        FOREIGN KEY ([BudgetAccountId])
        REFERENCES [dbo].[BudgetAccounts] ([BudgetAccountId])
        ON DELETE CASCADE,

    CONSTRAINT [FK_BudgetAccountLogs_ExpenseEntries]
        FOREIGN KEY ([ExpenseEntryId])
        REFERENCES [dbo].[ExpenseEntries] ([ExpenseEntryId])
        ON DELETE SET NULL,
);
