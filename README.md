# ExpenseTracker

ExpenseTracker is a local desktop expense-recording application built with an ASP.NET Core Web API, Windows Forms, Dapper, and SQL Server LocalDB.

The application provides a reusable item catalog, transactional expense recording, historical item snapshots, regional display formatting, and a responsive desktop dashboard. Development is now preparing for the next major feature: Budget Accounts.

## Project Goals

ExpenseTracker is designed as a focused learning project for practicing:

- C# and .NET application development
- ASP.NET Core Web API design
- Windows Forms desktop development
- Dependency injection and service lifetimes
- Feature-first project organization
- Dapper-based SQL persistence
- Database transactions and historical records
- API integration testing
- Responsive desktop layouts
- Clear separation between backend authority and frontend presentation

The project intentionally favors concrete classes, explicit ownership, and small incremental features over speculative abstractions.

## Current Features

### Item Management

- Create reusable catalog items
- Retrieve all items or one item by ID
- Search items locally by name, code, or brand
- Partially update items through `PATCH`
- Delete items while preserving historical expense details
- Prevent duplicate items by the composite combination of name, code, and brand
- Format unit prices according to the selected display region

### Expense Recording

- Record expenses containing one or more catalog items
- Require an expense title and allow optional notes
- Accept quantities with up to three decimal places
- Combine repeated item IDs into one normalized expense line
- Calculate line totals and the final expense total on the backend
- Generate expense timestamps in UTC through SQL Server
- Return the complete backend-confirmed expense after creation
- Display a dedicated successful-recording result view

### Historical Expense Data

Each expense item stores a historical snapshot of:

- Item name
- Item code
- Brand
- Quantity
- Unit price
- Line total

Deleting a source item sets the historical `ItemId` reference to `null` while retaining the snapshot. Existing expense history therefore remains readable even when the original catalog item no longer exists.

### Home Dashboard

- Display the total number of recorded expenses
- Display the total recorded cost
- Calculate and display the average expense from backend-confirmed summary values
- Display the current regional preset
- Display the five most recent expenses
- Refresh dashboard information manually
- Reformat displayed money and date values when regional settings change
- Adapt summary wording and layout to the available width
- Use vertical scrolling for growing activity lists
- Show horizontal scrolling only when the available width becomes too narrow for readable content
- Reserve a scalable right-side area for future Budget Accounts

### Regional Display Settings

The Windows Forms client supports regional display presets for:

- Philippines
- United States
- Germany
- France
- United Kingdom
- China
- Japan
- India
- United Arab Emirates
- Hong Kong
- Vietnam
- South Korea
- Russia

A preset controls presentation only:

- Currency formatting
- Compact date and time formatting
- Long date and time formatting
- Windows time-zone conversion

The backend continues to store authoritative decimal values and UTC timestamps. Regional settings do not modify persisted financial data.

Regional settings are currently session-only. Local persistence is planned but not yet implemented.

### Validation and Error Handling

- Validate Item and Expense requests with FluentValidation
- Reject unknown JSON properties globally
- Return client-safe error responses
- Convert known application failures into appropriate HTTP status codes
- Log unexpected errors without exposing internal details
- Parse validation errors and `ProblemDetails` responses in WinForms
- Display reusable API error dialogs in the desktop client

## Technology Stack

- C# and .NET 10
- ASP.NET Core Web API
- Windows Forms
- Microsoft Generic Host
- Microsoft dependency injection
- Dapper
- FluentValidation
- Microsoft SQL Server LocalDB
- SQL Server Database Project
- xUnit v3
- `WebApplicationFactory` API integration tests

## Architecture Overview

```text
Windows Forms Client
        |
        | HTTPS and JSON
        v
ASP.NET Core Web API
        |
        | Services and repositories
        v
Dapper and SQL Server LocalDB
```

The Windows Forms application never connects directly to the database. All business operations pass through the API.

The API remains authoritative for:

- Validation
- Duplicate detection
- Quantity normalization
- Item snapshot creation
- Line-total calculation
- Expense-total calculation
- UTC timestamps
- Transaction boundaries
- Persistence

The Windows Forms client owns:

- User interaction
- Local filtering and searching
- Loading and error presentation
- Responsive control layout
- Regional currency and date display
- Temporary estimated totals before submission
- Device-specific settings

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
│   │       │   ├── CreateExpenseEntryInput.cs
│   │       │   ├── EntriesController.cs
│   │       │   ├── ExpenseEntry.cs
│   │       │   ├── ExpenseEntryRepository.cs
│   │       │   ├── ExpenseEntryService.cs
│   │       │   └── ExpenseEntrySummary.cs
│   │       ├── Items/
│   │       │   ├── Exceptions/
│   │       │   ├── Requests/
│   │       │   ├── CreateExpenseItemInput.cs
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
├── ExpenseTracker.Database/
│   └── Tables/
│       ├── Items.sql
│       ├── ExpenseEntries.sql
│       └── ExpenseItems.sql
└── ExpenseTracker.WinForms/
    ├── Features/
    │   ├── Home/
    │   │   ├── Views/
    │   │   ├── ExpenseSummary/
    │   │   ├── RecentExpenses/
    │   │   └── BudgetAccounts/
    │   ├── Items/
    │   │   ├── Api/
    │   │   ├── Models/
    │   │   └── Views/
    │   ├── Expenses/
    │   │   ├── Api/
    │   │   ├── Models/
    │   │   └── Views/
    │   └── Settings/
    │       ├── Models/
    │       ├── Services/
    │       └── Views/
    ├── Infrastructure/
    │   ├── Dialogs/
    │   ├── Http/
    │   └── Presentation/
    ├── Shell/
    ├── Program.cs
    └── appsettings.json
```

## Feature Ownership

The solution uses feature-first organization. Business capabilities are grouped by ownership rather than by global technical layers.

### API ownership

`Features/Items` owns the reusable Item catalog.

`Features/Expenses` is the parent Expense feature:

- `Entries` owns persisted expense-entry information and entry-specific aggregate queries.
- `Items` owns historical item lines belonging to an expense.
- `ExpenseService` coordinates operations that cross the Entry, Expense Item, and Item areas.
- `ExpensesController` exposes complete Expense operations.
- `EntriesController` exposes the entry-owned summary operation without an unnecessary parent-service pass-through.

Technical folders such as `Requests`, `Validators`, and `Exceptions` communicate ownership but are not separate business features.

### WinForms ownership

Each main page is a complete `UserControl` hosted by `MainForm`:

- `HomeView`
- `ItemsView`
- `ExpensesView`
- `SettingsView`

The Home dashboard is further divided into focused controls:

- `ExpenseSummaryPanel` owns expense count, total, average, region display, and adaptive summary presentation.
- `RecentExpensesPanel` owns recent expense cards, formatting, and responsive scrolling.
- `BudgetAccountsPanel` owns the right-side budget display area and future budget-card rendering.
- `HomeView` coordinates loading, refresh, errors, and settings-change rerendering.

No interfaces are introduced solely for dependency injection. Concrete classes are used unless multiple implementations or a meaningful boundary creates a real need for an abstraction.

## API Endpoints

All controllers are mapped under:

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
GET     /api/v1/expenses/entries/summary
```

The entry summary endpoint returns one object:

```json
{
  "expenseCount": 23,
  "totalCost": 7748.52
}
```

When no expense entries exist, the endpoint returns zero values rather than `404 Not Found` or `204 No Content`.

## Database Design

The application uses:

```text
(localdb)\MSSQLLocalDB
```

Databases:

```text
ExpenseTracker
ExpenseTracker.Tests
```

- `ExpenseTracker` is used by the development API and Windows Forms client.
- `ExpenseTracker.Tests` is used by automated API integration tests.

The current schema contains:

### `Items`

Reusable catalog definitions with a composite uniqueness rule on:

```text
Name + Code + Brand
```

### `ExpenseEntries`

Parent expense records containing:

- SQL-generated identifier
- Required title
- SQL-generated UTC timestamp
- Backend-calculated total cost
- Optional notes

### `ExpenseItems`

Historical item lines containing:

- Parent expense-entry identifier
- Nullable source item identifier
- Item snapshots
- Normalized quantity
- Unit-price snapshot
- Line total

Deleting an Expense Entry cascades to its Expense Items. Deleting a source Item sets `ExpenseItems.ItemId` to `null` and preserves the historical snapshots.

## Database Infrastructure

Database access is organized around:

- `SqlConnectionFactory`, which creates SQL Server connections
- `UnitOfWork`, which owns the scoped connection and active transaction
- `TransactionManager`, which coordinates transaction startup, commit, rollback, and cleanup
- `DatabaseExecutor`, which reuses the active scoped connection and transaction or creates and disposes a temporary connection

Repository methods use Dapper `CommandDefinition` objects and propagate cancellation tokens.

Expense creation executes inside one transaction. The workflow:

1. Normalizes repeated item IDs.
2. Retrieves all source items.
3. Calculates line totals and the final total.
4. Creates the Expense Entry.
5. Creates historical Expense Item snapshots.
6. Commits only when the complete operation succeeds.

## Windows Forms Client

The desktop application uses the .NET Generic Host and dependency injection.

`Program.cs` registers:

- Named API `HttpClient`
- API clients
- Display settings and formatting services
- Home dashboard panels
- Main feature views
- `MainForm`

The configured API base address is read from:

```text
ExpenseTracker.WinForms/appsettings.json
```

The base address must remain absolute and include a trailing slash, for example:

```text
https://localhost:7120/api/v1/
```

WinForms owns copied transport models and does not reference the API project directly.

## Home Dashboard Design

The dashboard uses a scalable two-column desktop layout:

```text
HomeView
├── Left column
│   ├── ExpenseSummaryPanel
│   └── RecentExpensesPanel
└── Right column
    └── BudgetAccountsPanel
```

The summary adapts displayed wording to card width. Full labels are used when space is available, while compact labels are used when cards become narrow. A very narrow fallback can arrange the four summary cards vertically.

Recent expense cards and future budget cards:

- Expand to the available width at normal sizes
- Scroll vertically as collections grow
- Preserve a minimum readable width
- Show a horizontal scrollbar only when the container becomes narrower than that readable minimum

## Budget Accounts Vision

Budget Accounts are the next planned major feature.

The goal is to let users create named pools of available funds and understand how expenses affect each pool. Examples may include General Expenses, Office Supplies, Transportation, Monthly Operations, or Project Funds.

The Home dashboard already includes a scalable right-side area where Budget Account cards can eventually appear. A future card may show basic information such as:

- Account name
- Available balance
- Allocated amount
- Amount used
- Optional status or description

The planned feature may eventually support creating Budget Accounts, adding funds, recording balance activity, and associating expenses with a selected account. Any deduction must use the final expense total calculated by the backend rather than a client-supplied balance or total.

The exact database design, API routes, request contracts, transaction workflow, deletion behavior, and project structure have not been decided. Those details will be designed gradually when Budget Account implementation begins.

## Validation and Error Handling

FluentValidation covers:

- Item creation
- Partial Item updates
- Expense creation
- Nested expense-item requests

Unknown JSON properties are rejected through `JsonUnmappedMemberHandling.Disallow`.

Known feature failures are converted into safe HTTP responses by the global exception handler. Unexpected failures are logged and returned as generic server errors.

The Windows Forms client handles:

- Structured API errors
- Validation errors
- Connection failures
- Request timeouts
- Unexpected or incompatible responses

## Testing

The automated suite contains:

- Validator unit tests
- API integration tests using `WebApplicationFactory`

Integration tests exercise the request pipeline, controllers, services, repositories, Dapper, and the isolated LocalDB test database.

The current coverage includes:

- Item CRUD operations
- Duplicate-item detection
- Item validation
- Expense request validation
- Expense creation
- Quantity normalization
- Backend calculations
- SQL-generated timestamps
- Historical snapshots
- Missing resources
- Unknown-property rejection
- Source-item deletion behavior
- Complete expense retrieval
- Descending expense ordering
- Expense-entry summary retrieval

Successful Expense API tests currently leave immutable Expense records in `ExpenseTracker.Tests`. Source Items are cleaned up, which sets historical `ItemId` references to `null`. The test database can be manually deleted, recreated, and republished when a clean data set is needed.

## Running the Application

### Prerequisites

- Visual Studio with .NET desktop and ASP.NET workloads
- .NET 10 SDK
- SQL Server LocalDB
- SQL Server Database Project tooling

### Publish the development database

Publish `ExpenseTracker.Database` to:

```text
Server:   (localdb)\MSSQLLocalDB
Database: ExpenseTracker
```

Before running integration tests, publish the same schema to:

```text
Server:   (localdb)\MSSQLLocalDB
Database: ExpenseTracker.Tests
```

### Start the API

1. Open `ExpenseTracker.slnx` in Visual Studio.
2. Start `ExpenseTracker.Api` with the HTTPS launch profile.
3. Confirm that the API is available at:

```text
https://localhost:7120
```

4. Use `ExpenseTracker.Api/ExpenseTracker.Api.http` for manual endpoint verification.

### Start WinForms

1. Keep `ExpenseTracker.Api` running.
2. Start `ExpenseTracker.WinForms`.
3. Confirm that `ExpenseTracker.WinForms/appsettings.json` points to:

```text
https://localhost:7120/api/v1/
```

The desktop client opens through `MainForm` and provides Home, Items, Expenses, and Settings pages.

### Run automated tests

1. Publish the current schema to `ExpenseTracker.Tests`.
2. Open **Test Explorer**.
3. Build the solution if the tests are not listed.
4. Select **Run All Tests**.

## Current Status

Implemented:

- Database infrastructure
- Item backend and WinForms management
- Expense backend and WinForms recording
- Historical Expense Item snapshots
- Transactional expense creation
- Global API error handling
- API and validator tests
- Regional display settings
- Backend-confirmed expense result view
- Expense-entry summary endpoint
- Responsive Home dashboard
- Recent expense activity
- Scalable Budget Accounts dashboard placeholder

Planned next:

1. Finalize Budget Account rules and ownership.
2. Design the Budget database schema.
3. Implement Budget Account and activity backend features.
4. Add transactional budget-funded expense recording.
5. Connect real Budget account cards to the Home dashboard.
6. Add local persistence for regional display settings.

## Scope

ExpenseTracker is intentionally a small local learning project. It is not intended to be a cloud-hosted, multi-tenant, banking, payroll, tax, or full accounting system.

Budget Accounts will provide practical fund allocation and expense-tracking behavior without attempting to implement general-ledger accounting or enterprise financial controls.
