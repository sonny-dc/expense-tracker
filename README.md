# ExpenseTracker

ExpenseTracker is a small, local expense-recording system built around an ASP.NET Core Web API and a local Microsoft SQL Server database, with a Windows Forms graphical client planned as its desktop interface.

The project is intended as a focused C# application that demonstrates how a desktop client, HTTP API, data-access layer, and relational database can work together in a single local solution.

## Features

- Manage reusable items with a name, code, brand, and unit price
- Retrieve, create, partially update, and delete items through versioned API endpoints
- Prevent duplicate items based on the combination of name, code, and brand
- Validate item requests and reject unknown JSON properties
- Return standardized API error responses for known and unexpected failures
- Record expenses containing one or more item lines
- Store the expense date, total cost, optional notes, and creation time
- Preserve item details as historical snapshots in recorded expenses
- Retain expense history when an item is deleted
- Access application operations through a C# Web API
- Provide a Windows Forms desktop interface for local interaction

## Application Structure

ExpenseTracker is divided into three main parts:

### Windows Forms Client

The planned Windows Forms application will provide the graphical user interface. It will communicate with the Web API instead of connecting directly to the database.

This separation will keep the desktop interface focused on presentation and user interaction while the API owns application behavior and data access.

### ASP.NET Core Web API

The Web API is the application layer between the Windows Forms client and SQL Server. It is responsible for receiving requests, validating input, applying application rules, coordinating database operations, and returning JSON responses.

The API uses a feature-first structure, with application features placed under `Features` and shared database, routing, and error-handling infrastructure placed under `Infrastructure`.

API controllers are grouped under the versioned base route:

```text
/api/v1
```

The completed Item endpoints are available under:

```text
/api/v1/items
```

The API uses explicit route and body binding attributes in controller actions for readability. Request cancellation tokens are propagated from the HTTP request through the controller, service, repository, and Dapper command execution flow.

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
- FluentValidation
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

## Item API

The Item feature is implemented across controller, service, repository, validation, and exception layers.

Supported operations include:

```text
GET     /api/v1/items
GET     /api/v1/items/{itemId}
POST    /api/v1/items
PATCH   /api/v1/items/{itemId}
DELETE  /api/v1/items/{itemId}
```

Item creation returns `201 Created`, includes the created item in the response body, and provides a `Location` header containing the new item URL.

Item updates use `PATCH` and support partial request bodies. Nullable properties in `UpdateItemRequest` represent omitted fields, while the repository uses SQL `COALESCE` expressions to preserve existing values that were not supplied.

The service retrieves the current item before an update so it can calculate the effective name, code, and brand used for composite duplicate checking. The current item ID is excluded from that check so an unchanged item does not conflict with itself.

Successful deletion returns `204 No Content`. Historical expense item records remain valid because their item reference is set to null while stored snapshot values are preserved.

## Request Validation and JSON Contracts

The Item feature uses FluentValidation for strongly typed request validation.

Validation rules cover:

- Required item fields during creation
- Maximum lengths matching the database schema
- Nonnegative unit prices
- Partial item updates
- Rejection of an empty update request
- Validation only of fields supplied in a PATCH request

Validators are registered through assembly scanning from the Item feature service-collection extension. Controllers receive the appropriate validator through `IValidator<TRequest>` dependency injection and validate requests before calling the service layer.

Unknown JSON properties are rejected globally through:

```text
JsonUnmappedMemberHandling.Disallow
```

This prevents misspelled or unsupported request properties from being silently ignored.

## Error Handling

The API uses a centralized global exception handler implemented through ASP.NET Core's `IExceptionHandler` contract.

Known API failures inherit from an abstract `ApiException`, which carries a client-safe status code and title. Feature-specific exceptions currently include:

- `ItemNotFoundException`, returned as `404 Not Found`
- `DuplicateItemException`, returned as `409 Conflict`

The global handler converts known exceptions into structured `ProblemDetails` responses. Each error response includes:

- Error title
- HTTP status code
- Safe error detail
- Request path
- Trace identifier

Unexpected exceptions are logged through `ILogger<GlobalExceptionHandler>` with the request method, path, exception details, and trace identifier. Clients receive a generic `500 Internal Server Error` response without internal stack traces, SQL details, file paths, or other implementation information.

This design allows new feature exceptions to inherit from `ApiException` without requiring the global handler to contain a growing list of feature-specific exception mappings.

## Data Access and Transactions

The API uses Dapper for explicit SQL-based data access.

Database infrastructure includes:

- `SqlConnectionFactory` for creating SQL Server connections
- `DatabaseExecutor` for executing repository operations
- `UnitOfWork` for owning an active connection and transaction
- `TransactionManager` for coordinating commit and rollback behavior

Repository operations can run independently with a temporary connection or reuse the active scoped connection and transaction when participating in a larger transactional workflow.

The current Item operations do not start explicit transactions because each mutation contains a single database write. The transaction infrastructure is intended for multi-write workflows such as creating an expense entry together with all of its expense item lines.

## API Testing

The Item API has been manually tested without a frontend through a committed `.http` request file using the REST Client extension in Visual Studio Code.

Verified scenarios include:

- Retrieving all items
- Creating an item
- Retrieving an item by ID
- Partially updating an item
- Deleting an item
- Rejecting invalid request values
- Rejecting unknown JSON properties
- Returning `409 Conflict` for duplicate items
- Returning `404 Not Found` for missing items
- Returning a valid `Location` header after creation

The `.http` file serves as executable API documentation and a repeatable manual test collection while the Windows Forms client is still under development.

## Current Development Status

The following areas are currently established:

- SQL Server database schema for items and expenses
- LocalDB connection infrastructure
- Unit-of-work and transaction handling
- Shared database executor
- Versioned API routing under `/api/v1`
- Strict JSON request contracts
- FluentValidation request validators
- Scalable global exception handling with `ProblemDetails`
- Item models and request models
- Item repository operations for retrieval, creation, duplicate checking, partial updating, and deletion
- Item service rules for retrieval, duplicate detection, partial updates, creation, and deletion
- Item controller endpoints for complete CRUD interaction
- Successful manual Item API testing through a `.http` file

The next planned development area is the expense feature, including coordinated creation of an expense entry and its associated expense item lines within a transaction. The Windows Forms client will then consume the completed API workflows.

## Project Scope

ExpenseTracker is intentionally a small, local application.

It is not currently designed as:

- A cloud-hosted service
- A multi-tenant system
- A public web application
- A distributed database application
- A replacement for full accounting software

Its purpose is to provide a practical local expense-recording workflow while serving as a focused C# learning project involving desktop development, API design, request validation, error handling, Dapper, transactions, and relational database modeling.
