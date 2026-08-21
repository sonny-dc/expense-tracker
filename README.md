# ExpenseTracker

ExpenseTracker is a small, local desktop application for recording expenses and managing reusable item information. It uses a Windows Forms graphical user interface, an ASP.NET Core Web API, and a local Microsoft SQL Server database.

The project is intended as a focused C# application that demonstrates how a desktop client, HTTP API, data-access layer, and relational database can work together in a single local solution.

## Features

- Manage reusable items with a name, code, brand, and unit price
- Record expenses containing one or more item lines
- Store the expense date, total cost, optional notes, and creation time
- Preserve item details as historical snapshots in recorded expenses
- Retain expense history when an item is deleted
- Access application operations through a C# Web API
- Use a Windows Forms desktop interface for local interaction

## Application Structure

ExpenseTracker is divided into three main parts:

### Windows Forms Client

The Windows Forms application provides the graphical user interface. It communicates with the Web API instead of connecting directly to the database.

This separation keeps the desktop interface focused on presentation and user interaction while the API owns application behavior and data access.

### ASP.NET Core Web API

The Web API is the application layer between the Windows Forms client and SQL Server. It is responsible for receiving requests, applying application rules, coordinating database operations, and returning JSON responses.

The API uses a feature-first structure, with application features placed under `Features` and shared database infrastructure placed under `Infrastructure`.

### SQL Server Database

The application uses Microsoft SQL Server LocalDB through the following local server instance:

```text
(localdb)\MSSQLLocalDB
```

The database is named:

```text
ExpenseTracker
```

The schema is maintained through a SQL Server Database Project.

## Technology Stack

- C#
- .NET 10
- ASP.NET Core Web API
- Windows Forms
- Dapper
- Microsoft SQL Server LocalDB
- SQL Server Database Project

## Database Design

The database currently contains three main tables:

### `Items`

Stores reusable item information:

- Item name
- Item code
- Brand
- Unit price

An item is uniquely identified by the combination of its name, code, and brand. Unit prices cannot be negative.

### `ExpenseEntries`

Stores the main information for each recorded expense:

- Expense date
- Total cost
- Optional notes
- Creation timestamp

Each expense entry can contain multiple expense items.

### `ExpenseItems`

Stores the individual item lines belonging to an expense entry:

- Referenced item, when still available
- Item name snapshot
- Item code snapshot
- Brand snapshot
- Quantity
- Unit cost snapshot
- Line total

Snapshot fields preserve the original item details at the time the expense was recorded. If an item is later changed or deleted, the historical expense information remains intact.

Deleting an expense entry also deletes its associated expense item lines. Deleting an item does not delete historical expense lines. Instead, their item reference becomes null while their snapshots remain available.

## Data Access and Transactions

The API uses Dapper for explicit SQL-based data access.

Database infrastructure includes:

- `SqlConnectionFactory` for creating SQL Server connections
- `DatabaseExecutor` for executing repository operations
- `UnitOfWork` for owning an active connection and transaction
- `TransactionManager` for coordinating commit and rollback behavior

Repository operations can run independently with a temporary connection or reuse the active scoped connection and transaction when participating in a larger transactional workflow.

## Current Development Status

The following areas are currently established:

- SQL Server database schema for items and expenses
- LocalDB connection infrastructure
- Unit-of-work and transaction handling
- Shared database executor
- Item models and request models
- Item repository operations for retrieval, creation, duplicate checking, updating, and deletion

The next planned development area is the item service, followed by item API endpoints and the Windows Forms client integration.

## Project Scope

ExpenseTracker is intentionally a small, local application.

It is not currently designed as:

- A cloud-hosted service
- A multi-tenant system
- A public web application
- A distributed database application
- A replacement for full accounting software

Its purpose is to provide a practical local expense-recording workflow while serving as a focused C# learning project involving desktop development, API design, Dapper, transactions, and relational database modeling.
