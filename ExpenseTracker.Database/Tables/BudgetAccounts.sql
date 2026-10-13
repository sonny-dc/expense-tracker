CREATE TABLE [dbo].[BudgetAccounts]
(
    [BudgetAccountId] INT IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(500) NULL,
    [Balance] DECIMAL(18, 2) NOT NULL,
    [Status] VARCHAR(20) NOT NULL CHECK (Status IN ('Open', 'Archived', 'Settled')),
    [CreatedDateTime] DATETIME2(0) NOT NULL
        CONSTRAINT [DF_BudgetAccounts_CreatedDateTime]
        DEFAULT SYSUTCDATETIME(),
    [LastUpdatedDateTime] DATETIME2(0) NOT NULL
        CONSTRAINT [DF_BudgetAccounts_LastUpdatedDateTime]
        DEFAULT SYSUTCDATETIME(),
    
    CONSTRAINT [PK_BudgetAccounts]
        PRIMARY KEY ([BudgetAccountId]),
);
