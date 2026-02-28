# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Restore and build
dotnet restore
dotnet build

# Run the API (Swagger at /swagger)
dotnet run --project Rockstar

# Run all tests
dotnet test Rockstar.sln

# Run tests for a specific project
dotnet test Application.Test

# Apply EF Core migrations manually (also auto-runs at startup)
dotnet ef database update --project Infrastructure.Persistance
```

## Architecture

This is a **Clean Architecture + DDD + CQRS + Mediator** solution targeting .NET 8.

**Solution projects and their roles:**

| Project | Layer | Purpose |
|---|---|---|
| `Domain` | Domain | Entities, Value Objects, business invariants. No external dependencies. |
| `Application` | Application | MediatR handlers, repository interfaces, FluentValidation validators, Mapster mapping configs, Result pattern types. Depends only on Domain. |
| `Infrastructure.Persistance` | Infrastructure | EF Core `AppDbContext`, repository implementations, migrations. |
| `Infrastructure.Authentication` | Infrastructure | JWT token generation. |
| `Infrastructure.External` | Infrastructure | GitHub API client and other external service integrations. |
| `Rockstar` | Presentation | Thin ASP.NET Core 8 controllers, middleware, DI wiring. |
| `Contracts` | Shared | Request/response DTOs for the API surface. |
| `Application.Test` | Test | xUnit + Moq unit tests for Application handlers. |

**Dependency direction:** Domain ← Application ← Infrastructure ← Presentation. Infrastructure is intentionally split into three projects to prevent cross-contamination (e.g., Authentication must never leak into Persistance).

**Key patterns in practice:**

- **CQRS via MediatR**: Every use case is a `*Command` or `*Query` with a corresponding `*Handler`. Controllers send to `IMediator.Send()` and do no business logic.
- **Result pattern**: Handlers return `Result<T>` / `Result` instead of throwing exceptions. Controllers inspect `IsSuccess` and map to HTTP responses with RFC 7807 `ProblemDetails`.
- **Repository pattern**: Interfaces live in `Application/InfraInterfaces/`; implementations live in `Infrastructure.Persistance`. Never use EF Core directly outside the persistence layer.
- **DDD Aggregates**: `Artist` is the aggregate root for `Song`. Modifications go through `Artist.AddSong()` / domain methods that enforce invariants. Value objects (`Bpm`, `Duration`, `Year`) are C# `record` types.
- **Mapster** is used for all object mapping (not AutoMapper). Mapping configs are in `*MappingConfig.cs` files per layer.
- **API versioning**: All routes are prefixed `/api/v1/`. Use `Asp.Versioning` NuGet package conventions.

## Coding Standards

From `.github/workflows/copilot-instructions.md` and `.github/agents/review.agent.md`:

- **Naming**: Commands → `*Command`, Queries → `*Query`, Handlers → `*Handler`
- **Errors**: Use `Result<T>` for expected failures; reserve exceptions for truly exceptional cases
- **Controllers are thin**: No business logic, no direct EF queries; only HTTP ↔ mediator mapping
- **All functions must have docstrings** explaining parameters and return types
- **Every method needs a unit test**; every public method needs an integration test
- **Idempotency**: Consider it for event handlers, webhooks, and retry-prone operations
- Avoid direct EF Core usage outside `Infrastructure.Persistance`

## Authentication

Every protected endpoint requires two headers:
- `Authorization: Bearer <JWT>` — obtained from `/api/v1/authentication/login` or `/api/v1/authentication/create`
- `X-Api-Key: <key>` — from `AcceptedApiKeys` array in `appsettings.Development.json`

JWT settings and accepted API keys are configured in `Rockstar/appsettings.Development.json`. For the JWT secret, prefer `dotnet user-secrets` in local development.

## Configuration

`Rockstar/appsettings.Development.json` contains:
- `ConnectionStrings:SqlServer` — SQL Server connection (default DB: `RockstarDB`)
- `JwtSettings` — issuer, audience, secret, expiry
- `AcceptedApiKeys` — array of valid API keys

EF Core migrations are auto-applied at startup (`db.Database.Migrate()` in `Program.cs`).

## Tests

Tests live in `Application.Test/` and use xUnit + Moq. Follow the AAA pattern. Mock all repositories and external services — do not hit a real database in unit tests. See `RegisterCommandHandlerTests.cs` for reference on how commands are tested.

## Code Review Priorities (from review agent)

When reviewing PRs, check in this order:
1. Correctness & bugs (race conditions, nulls, async/await issues)
2. Clean Architecture compliance (no layer leakage, Domain stays independent)
3. API design (validation, error contracts, ProblemDetails)
4. Security (auth/authz, input validation, no secrets in logs/code, SQLi/SSRF)
5. Performance (N+1 queries, unnecessary allocations, correct async I/O)
6. Maintainability (SOLID, naming, duplication)
7. Observability (logging, correlation IDs)
8. Test coverage (edge cases, testable boundaries)

Mark production-incident risks as **SEV-1**.

## Reference Files

- `Domain/Entities/Artist.cs` — aggregate root example
- `Domain/ValueObjects/Bpm.cs` — value object record example
- `Application/Mediator/Artist/Commands/` — Command + Handler pattern
- `Rockstar/Controllers/V1/ArtistController.cs` — thin controller pattern
- `Rockstar/Endpoints.http` — sample HTTP requests for all endpoints
- `Rockstar/Documentation/` — detailed guides on DDD, nullable types, infrastructure layering, race conditions
