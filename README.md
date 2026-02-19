# Onboarding Guide for CleanArchitectureSQRSMediator

Welcome to the codebase! This document will guide new developers through the necessary steps to get the project running locally, understand its architecture, follow testing and code standards, and contribute effectively.

---

## 1. Project Structure

- **Domain Driven Design (DDD):** Domain logic is in the `Domain` project. Business rules, entities, and value objects live here (see `Domain/Entities` and `Domain/ValueObjects`).
- **Application Layer:** Use cases/commands/queries are in `Application`, following Mediator and CQRS patterns.
- **Infrastructure:** Split into `Persistance` (DB/EfCore), `Authentication`, and `External` (API access).
- **Presentation:** `Rockstar` project is the ASP.NET Core API, with versioned controllers in `Rockstar/Controllers/V1`.

---

## 2. Setting Up Locally

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- SQL Server (Express or Docker is fine)
- Optional: [Postman](https://www.postman.com/) / VSCode REST Client for testing APIs

### Steps

1. **Clone the Repository**

   ```sh
   git clone https://github.com/programmingworld1/CleanArchitectureSQRSMediator.git
   ```
2. **Configuration**

   - See `Rockstar/appsettings.Development.json` for all config. Main points:
     - `"ConnectionStrings:SqlServer"`: Update if needed for your local SQL Server instance.
     - `"JwtSettings"`: Enter a secret string for the JWT key.
     - `"AcceptedApiKeys"`: Add API keys for development/testing.
   - For secrets (e.g. JWT secret), consider setting via [dotnet secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for local dev.
   
3. **Restore and Build**

   From the repo root:

   ```sh
   dotnet restore
   dotnet build
   ```
   
4. **Database Setup**

   - DB migrations are auto-applied at startup (see `Rockstar/Program.cs`: `db.Database.Migrate();`).
   - To check, you can also run:
     ```sh
     dotnet ef database update --project Infrastructure.Persistance
     ```
   - Default DB: `RockstarDB` in SQL Server (see connection string). Change in config if needed.
   
5. **Run the API**

   ```sh
   dotnet run --project Rockstar
   ```
   The API should now be available (Swagger UI at `/swagger`).

6. **Authentication/Authorization**

   - Most endpoints require a JWT and API Key.
   - Use `/api/v1/authentication/create` and `/api/v1/authentication/login` to obtain a bearer token.
   - Pass API key using `X-Api-Key` header (see `appsettings.Development.json`).

7. **Example Requests**

   See `Rockstar/Endpoints.http` for example HTTP calls (register, login, CRUD operations for Artist/Song).
   - Use with VSCode/JetBrains HTTP Client, or copy-paste into Postman.

---

## 3. Clean Architecture, DDD, Mediator & Patterns

### 3.1 DDD and Ubiquitous Language
- Entities represent core concepts like `Artist`, `Song`, and `User`.
- Value Objects (like `Bpm`, `Year`, `Duration`) enforce business rules and immutability.
- Aggregates: Modifications go through aggregate roots (e.g., `Artist.AddSong`).
- See `Rockstar/readme_DDD.md` for deep dives and code samples.

### 3.2 Clean Architecture Layers

- **Domain:** Core business rules, agnostic to infrastructure.
- **Application:** Use cases, CQRS/Mediator handlers, relies only on domain and interfaces.
- **Infrastructure:** Database (EF Core), authentication, APIs—implementation of interfaces from `Application`.
- **Presentation/Controllers:** API entrypoints. Should only contain HTTP logic, Model->Command mapping, and result handling.

### 3.3 Repository Pattern

- Abstracts all data access via repository interfaces (`IArtistRepository`, `ISongRepository`, etc.).
- Implementation in `Infrastructure.Persistance`.
- Avoids direct coupling to EF Core in core business logic.
- See `Infrastructure.Persistance/readme_persistance.md` for additional insights.

### 3.4 Mediator (MediatR)

- Commands/Queries are processed via MediatR Handlers.
- Example: `FindArtistQuery` handled by `FindArtistQueryHandler`.
- Encourages single-responsibility and separation between operation and HTTP logic.

---

## 4. Testing

### 4.1 Existing Tests

- All tests in `Application.Test` (xUnit).
- Example: `RegisterCommandHandlerTests.cs` for testing user registration logic.

### 4.2 Adding New Tests

- Put unit tests for Application services/handlers in `Application.Test`.
- Mock repositories, token generators, etc., as needed (see examples with Moq).
- Focus on:
  - Handler logic (success and failure cases)
  - Domain logic/aggregate invariants

### 4.3 Best Practices

- Do **not** use real DB for application layer unit tests (mock repositories).
- Keep tests fast and isolated.
- For integration/e2e tests (not yet in this solution), use an in-memory or test DB.
- Follow AAA pattern (Arrange, Act, Assert).

---

## 5. RESTful API Standards

- Plural resource routes: `/api/v1/artists`, `/api/v1/songs`
- HTTP Methods:
  - `GET`: Retrieve data. Safe and idempotent.
  - `POST`: Create resources.
  - `PUT`: Replace or update resources fully.
  - `DELETE`: Remove resources.
- Status codes:
  - `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, `404 Not Found`, `409 Conflict`, `500 Internal Server Error`, etc.
- Versioned APIs in route (`v1`)
- All requests must be stateless.
- Error responses use ProblemDetails format (RFC 7807) including a `traceId` for troubleshooting.
- Filtering/Paging via query string.
- See controller summaries (e.g., in `Rockstar/Controllers/V1/ArtistController.cs`) for further REST standards.

---

## 6. Adding Features & Contributing

1. **Create* relevant entities, value objects, and commands/queries.**
2. **Add Mediator handlers.**
3. **Update or add repository interfaces and implementations.**
4. **Map contracts/DTOs to domain models in Presentation layer or Mappings.**
5. **Add/extend tests.**
6. **Document significant business logic in `onboard_readme.md` or the `readme_DDD.md`.**

---

## 7. Troubleshooting & FAQ

- If the API fails to run, check DB connection string and run migrations.
- If JWT authentication fails, verify JWT secret and claims configuration.
- For API/Swagger issues, ensure API Key is provided in the header.
- For further C#/.NET pattern explanations, check the `Documentation/README.md`.

---

## 8. References and Further Reading

- `Rockstar/readme_DDD.md`: Domain Driven Design concepts and sample code.
- `Infrastructure.Persistance/readme_persistance.md`: Repository pattern rationale.
- `Rockstar/readme_infra.md`: Approach to infrastructure layering.
- `Documentation/README.md`: C#/record/required/init/DTOs patterns.

---

Happy coding! If you have questions, reach out to the project maintainer.
