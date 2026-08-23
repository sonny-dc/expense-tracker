# ExpenseTracker

ExpenseTracker is a small local expense-recording application built with an ASP.NET Core Web API, SQL Server LocalDB, and a planned Windows Forms desktop client.

The backend is complete for the current Item and Expense scope. Development is now moving to the Windows Forms client, which will communicate with the API instead of connecting directly to the database.

## Features

- Create, retrieve, partially update, and delete reusable items
- Prevent duplicate items by name, code, and brand
- Record expenses containing one or more item lines
- Combine repeated item IDs into one expense line with a normalized quantity
- Calculate line totals and expense totals on the backend
- Preserve item names, codes, brands, and unit prices as historical snapshots
- Retain expense history when a source item is deleted
- Validate requests with FluentValidation
- Reject unknown JSON properties
- Return standardized `ProblemDetails` error responses
- Execute expense creation inside a database transaction
- Retrieve complete expenses without repetitive per-entry database queries

## Technology Stack

- C# and .NET 10
- ASP.NET Core Web API
- Windows Forms, planned client
- FluentValidation
- Dapper
- Microsoft SQL Server LocalDB
- SQL Server Database Project
- xUnit v3

## Solution Structure

```text
ExpenseTracker.slnx
├── ExpenseTracker.Api/
│   ├── Features/
│   │   ├── Items/
│   │   │   ├── Exceptions/
│   │   │   ├── Requests/
│   │   │   ├── Validators/
│   │   │   ├── Item.cs
│   │   │   ├── ItemRepository.cs
│   │   │   ├── ItemService.cs
│   │   │   ├── ItemServiceExtensions.cs
│   │   │   └── ItemsController.cs
│   │   └── Expenses/
│   │       ├── Entries/
│   │       │   ├── Exceptions/
│   │       │   ├── ExpenseEntry.cs
│   │       │   ├── ExpenseEntryRepository.cs
│   │       │   └── ExpenseEntryService.cs
│   │       ├── Items/
│   │       │   ├── Exceptions/
│   │       │   ├── Requests/
│   │       │   ├── ExpenseItem.cs
│   │       │   ├── ExpenseItemRepository.cs
│   │       │   └── ExpenseItemService.cs
│   │       ├── Requests/
│   │       ├── Validators/
│   │       ├── ExpenseResult.cs
│   │       ├── ExpenseService.cs
│   │       ├── ExpenseServiceExtensions.cs
│   │       └── ExpensesController.cs
│   ├── Infrastructure/
│   │   ├── Database/
│   │   ├── Errors/
│   │   └── Routing/
│   ├── ExpenseTracker.Api.http
│   └── Program.cs
├── ExpenseTracker.Api.Tests/
│   ├── Features/
│   │   ├── Items/
│   │   │   ├── Api/
│   │   │   └── Validators/
│   │   └── Expenses/
│   │       ├── Api/
│   │       ├── Helpers/
│   │       └── Validators/
│   └── Infrastructure/
└── ExpenseTracker.Database/
    └── Tables/
        ├── Items.sql
        ├── ExpenseEntries.sql
        └── ExpenseItems.sql
```

### Feature ownership

The API uses a feature-first structure. Business areas are placed under `Features`, while shared technical infrastructure is placed under `Infrastructure`.

`Features/Expenses` is the parent Expense feature. Its nested folders have different responsibilities:

- `Entries` owns persisted expense-entry data and behavior.
- `Items` owns the historical item lines belonging to an expense.
- `Requests` contains request contracts owned by the parent Expense operation.
- `Validators` contains validation rules for those parent request contracts.
- `ExpenseService` coordinates item lookup, total calculation, snapshot preparation, and transactional persistence.

`Requests`, `Validators`, and `Exceptions` are technical-role folders, not separate business features. Their location communicates ownership. For example:

```text
Features/Expenses/Validators/CreateExpenseRequestValidator.cs
```

means that the validator belongs to the parent Expense feature, while:

```text
Features/Expenses/Items/Requests/CreateExpenseItemRequest.cs
```

means that the nested request model belongs to the Expense Items area.

No additional `Features` or `Subfeatures` folder is needed inside `Expenses`. The existing folder hierarchy already expresses the parent and child relationship without adding repetitive namespaces.

## API Endpoints

All controllers are mapped under the versioned base route:

```text
/api/v1
```

### Items

```text
GET     /api/v1/items
GET     /api/v1/items/{itemId}
POST    /api/v1/items
PATCH   /api/v1/items/{itemId}
DELETE  /api/v1/items/{itemId}
```

### Expenses

```text
GET     /api/v1/expenses
GET     /api/v1/expenses/{expenseEntryId}
POST    /api/v1/expenses
```

The client supplies item IDs, quantities, and optional notes when creating an expense. The backend owns the expense timestamp, item snapshots, line totals, and overall total.

## Database Design

The application uses the LocalDB instance:

```text
(localdb)\MSSQLLocalDB
```

The main database is named `ExpenseTracker`. Automated integration tests use the isolated `ExpenseTracker.Tests` database.

The schema is maintained by `ExpenseTracker.Database` and contains:

- `Items`, reusable item definitions with a composite uniqueness rule on name, code, and brand
- `ExpenseEntries`, parent expense records with a SQL-generated UTC timestamp and backend-calculated total
- `ExpenseItems`, historical expense lines containing item snapshots, quantity, unit-price snapshot, and line total

Deleting an expense entry cascades to its expense items. Deleting a source item sets the historical `ExpenseItems.ItemId` reference to `null` while preserving its snapshot values.

## Backend Design

Controllers validate HTTP requests and delegate application behavior to feature services. Repositories own Dapper SQL and use `DatabaseExecutor` for connection and transaction reuse.

Database infrastructure includes:

- `SqlConnectionFactory`, creates SQL Server connections
- `DatabaseExecutor`, executes repository operations using either a temporary connection or the active transaction
- `UnitOfWork`, owns the scoped connection and transaction
- `TransactionManager`, coordinates commit and rollback behavior

Expense creation runs as one transaction. `ExpenseService` normalizes repeated item IDs, retrieves source items in one batch, calculates totals, creates the parent expense entry, and creates its historical item lines.

Retrieving all expenses also uses batch retrieval. The API loads all matching expense items in one query, groups them by `ExpenseEntryId`, and assembles complete results in memory. This avoids the N+1 query pattern.

## Validation and Error Handling

FluentValidation rules cover Item creation, partial Item updates, and Expense creation. Unknown JSON properties are rejected globally through `JsonUnmappedMemberHandling.Disallow`.

Known application failures inherit from `ApiException` and are converted into client-safe `ProblemDetails` responses by `GlobalExceptionHandler`. Unexpected errors are logged and returned as generic `500 Internal Server Error` responses without exposing internal details.

## Running the API in Visual Studio

1. Open `ExpenseTracker.slnx` in Visual Studio.
2. Ensure `ExpenseTracker.Api` is selected as the startup project.
3. Ensure the current database project has been published to the `ExpenseTracker` LocalDB database.
4. Run the API using the **https** launch profile or press **F5**.
5. Use `ExpenseTracker.Api/ExpenseTracker.Api.http` to send manual requests to the running API.

The HTTPS launch profile uses:

```text
https://localhost:7120
```

## Running Tests in Visual Studio

The automated suite contains validator unit tests and API integration tests. API tests run against the isolated `ExpenseTracker.Tests` LocalDB database through `CustomWebApplicationFactory`.

Before running integration tests, publish the current database project to `ExpenseTracker.Tests` so its schema matches the application database.

To run the tests:

1. Open **Test > Test Explorer** in Visual Studio.
2. Build the solution if the tests have not appeared.
3. Select **Run All Tests**.
4. Review failures and output through Test Explorer.

The test suite covers Item CRUD behavior, request validation, Expense creation and retrieval, backend calculations, duplicate item normalization, SQL-generated timestamps, historical snapshots, missing-resource responses, strict JSON contracts, and source-item deletion behavior.

## Current Status

The backend, database schema, manual HTTP requests, validator tests, and API integration tests are complete for the current project scope. All current Item and Expense tests pass against the isolated test database.

Development is now moving to the Windows Forms client. The desktop application will consume the existing API for item management, expense recording, and expense history while leaving validation, calculations, transactions, snapshots, and persistence under backend ownership.

## Scope

ExpenseTracker is intentionally a small local learning project. It is not intended to be a cloud-hosted, multi-tenant, or full accounting system.
