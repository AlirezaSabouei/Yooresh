# Yooresh

[![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-blue)](https://www.linkedin.com/in/alireza-sabouei/)

Yooresh is a browser-based, multiplayer strategy game. Players register, gather resources, raise and upgrade
buildings, train soldiers and fight other players — the classic village-builder loop, built as a learning
project around modern .NET and Domain-Driven Design.

The repository is a .NET 10 solution following Clean Architecture (the Jason Taylor layout) with a
MediatR-based CQRS application layer and a Blazor Server client.

> **Status:** active development on the `dev` branch. The solution builds and the unit test suite is green;
> parts of the API surface are still being moved from the old `Players` controller to `Accounts`, so the
> acceptance/UI test suites are not reliable yet (see [Running the tests](#running-the-tests)).

## Prerequisites

| Requirement | Version | Needed for |
|---|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download) | **10.0.x** | Everything. Every project targets `net10.0`. |
| SQL Server | 2019 or newer (Express / Developer / a container) | Running the API against a real database. |
| Entity Framework Core CLI | 10.x (`dotnet tool install --global dotnet-ef`) | Creating the database from migrations. |
| Gmail account + app password | — | Optional. Sending account-confirmation emails. |
| Playwright browsers | — | Optional. The browser UI tests. |

`dotnet --version` must report `10.0.*` (or newer); .NET 8 SDKs cannot build the solution. Node.js is not required.

## Solution structure

| Project | Layer | Purpose |
|---|---|---|
| `src/Yooresh.Domain` | Domain | Entities, value objects, domain events, domain exceptions and capability interfaces. No dependencies. |
| `src/Yooresh.Application` | Application | Commands, queries, validators, DTOs, MediatR handlers and pipeline behaviours. |
| `src/Yooresh.Infrustructure` | Infrastructure | EF Core `Context`, entity configurations, migrations, email, password hashing, Hangfire jobs and a RabbitMQ event bus (present, not yet wired up). |
| `src/Yooresh.API` | Presentation | ASP.NET Core Web API: controllers, JWT authentication, Swagger, Hangfire dashboard. |
| `src/Yooresh.Village` | Presentation | Blazor Server (Razor Components) client. Talks to the API over HTTP. |
| `Tests/Yooresh.UnitTests` | Tests | NUnit + Shouldly unit tests for domain and application logic. |
| `Tests/Yooresh.AcceptanceTests` | Tests | API tests through `WebApplicationFactory` using an in-memory SQLite database. |
| `Tests/Yooresh.Blazor.Tests` | Tests | Playwright end-to-end tests that launch the API and the Blazor app. |

Project folders are referenced by path. Note that the infrastructure project lives in
`src/Yooresh.Infrustructure` (sic) while its assembly and namespace are `Yooresh.Infrastructure`.

## Getting started

### 1. Restore and build

```bash
git clone https://github.com/AlirezaSabouei/Yooresh.git
cd Yooresh
dotnet restore Yooresh.sln
dotnet build Yooresh.sln
```

### 2. Configure the API

`src/Yooresh.API/appsettings.json` holds development-only defaults. Override them locally with
[user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) — the API project already has a
`UserSecretsId`. Never commit real credentials.

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=Yooresh;User Id=sa;Password=<your-password>;TrustServerCertificate=True" --project src/Yooresh.API
dotnet user-secrets set "Jwt:Key" "<a random string of at least 32 characters>" --project src/Yooresh.API
dotnet user-secrets set "Email:Address" "you@gmail.com" --project src/Yooresh.API
dotnet user-secrets set "Email:Password" "<gmail app password>" --project src/Yooresh.API
```

| Configuration key | Required | Description |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | Yes | SQL Server connection string used by the API. |
| `Jwt:Key` | Yes | Symmetric signing key for access tokens. Must be at least 32 characters for HS256; shorter keys fail at login time. |
| `Jwt:Issuer` | Yes | Issuer written into tokens. Has a development default. |
| `Email:Address` | No | Gmail address used as the sender of confirmation emails. |
| `Email:Password` | No | Gmail app password for that address (plain SMTP on port 587 is hard-coded). |
| `Email:Message` | No | Body template for the confirmation email; `@code` is replaced with the player's confirmation code. |
| `ApiAddress` (Blazor app) | Yes | Base URL of the API as seen by `Yooresh.Village`, e.g. `http://localhost:5000`. |

If email is not configured, registration still succeeds: `Email.SendEmailAsync` swallows send failures, and
the confirmation code is stored on the player row.

### 3. Make sure the database schema is up to date

The API does not create or migrate the database on startup. Apply the migrations once, and again after
pulling schema changes:

```bash
dotnet ef database update \
  --project src/Yooresh.Infrustructure \
  --startup-project src/Yooresh.API
```

Install the EF Core 10 CLI (`dotnet tool install --global dotnet-ef`). An older global tool still runs but
warns that its version is behind the project runtime.

### 4. Run the API

```bash
dotnet run --project src/Yooresh.API --urls http://localhost:5000
```

There are no `launchSettings.json` files, so ports are not fixed — pick them with `--urls` or
`ASPNETCORE_URLS`. With the command above:

- Swagger UI: <http://localhost:5000/swagger>
- Hangfire dashboard: <http://localhost:5000/hangfire> (in-memory storage, so queued jobs are lost on restart)

For HTTPS, run `dotnet dev-certs https --trust` first and pass an `https://` URL.

### 5. Run the Blazor client

`Yooresh.Village` requires `ApiAddress` to point at the running API; without it the app throws as soon as it
builds an HTTP client. The value committed in `appsettings.json` (`https://localhost:59075`) is a leftover
from an old development machine, so override it locally — through the environment or an
`appsettings.Development.json` file:

```bash
ApiAddress=http://localhost:5000 dotnet run --project src/Yooresh.Village --urls http://localhost:5001
```

The client calls the API server-side (Blazor Server), so both processes must be running.

### 6. Try the main flows

| Endpoint | Method | Notes |
|---|---|---|
| `/api/accounts/signup` | POST | `{ "name": "...", "email": "...", "password": "..." }` returns `201` with the created player. |
| `/api/accounts/confirm` | POST | `{ "playerId": "<guid>", "confirmationCode": "<code>" }` |
| `/api/accounts/login` | POST | `{ "email": "...", "password": "..." }` returns access and refresh tokens. Requires a confirmed account. |
| `/api/accounts/refresh` | POST | `{ "refreshToken": "..." }` |
| `/api/resources` | GET | Requires a bearer token. |
| `/api/factions` | GET | Requires a bearer token. |

## Running the tests

```bash
dotnet test Tests/Yooresh.UnitTests/Yooresh.UnitTests.csproj         # fast, no external dependencies
dotnet test Tests/Yooresh.AcceptanceTests/Yooresh.AcceptanceTests.csproj
dotnet test Tests/Yooresh.Blazor.Tests/Yooresh.Village.Tests.csproj  # end-to-end, see below
```

- **Unit tests** run entirely in memory and are the suite to trust while developing.
- **Acceptance tests** boot the API in-process with `WebApplicationFactory` and swap SQL Server for an
  in-memory SQLite database, so no database server is needed. Some of them still call the pre-rename
  `api/players` endpoints and fail; they are being migrated to `api/accounts`.
- **UI tests** start `dotnet run` for both the API and the Blazor app, then drive Chromium through Playwright.
  They need the API configured (database, JWT) and `playwright.ps1 install` / `Microsoft.Playwright.Program.Main(["install"])`
  executed once for the test project. They also assume the Blazor app listens on `http://localhost:5163`.
  Treat them as experimental.

## Troubleshooting

| Symptom | Likely cause |
|---|---|
| `error NU1201 ... is not compatible with net8.0` | A project or the SDK is pinned to an older target. The whole solution is `net10.0`; check `TargetFramework` and `dotnet --list-sdks`. |
| `Failed to determine the https port for redirect` | Both apps call `UseHttpsRedirection()`. Expected when running HTTP-only; requests are not redirected, and the app still works. |
| `UriFormatException` when starting the Blazor app | `ApiAddress` is missing. |
| Login returns `401` after signing up | The account is not confirmed yet; use `/api/accounts/confirm` with the code stored in `Players.ConfirmationCode`. |
| `Login` throws an IDX error | `Jwt:Key` is missing or shorter than 32 characters. |
| SQL Server connection errors | The default connection string uses Windows integrated security (`.\sqlexpress`). Use SQL authentication on Linux/macOS, or a SQL Server container. |
| `dotnet ef` warns that "the Entity Framework tools version is older than that of the runtime" | Update the global tool: `dotnet tool update --global dotnet-ef`. |

## Technologies

- [.NET 10](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10) — ASP.NET Core Web API and Blazor Server
- [Entity Framework Core 10](https://learn.microsoft.com/ef/core/) — SQL Server provider, code-first migrations
- [MediatR](https://github.com/jbogard/MediatR) 14 — CQRS handlers and domain-event dispatching
- [AutoMapper](https://automapper.org/) 16 and [FluentValidation](https://docs.fluentvalidation.net/) 12
- [Hangfire](https://www.hangfire.io/) — background jobs (in-memory storage)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) 10 — OpenAPI/Swagger
- [NUnit 5](https://nunit.org/), [Shouldly](https://github.com/shouldly/shouldly) and [Playwright](https://playwright.dev/dotnet/) for tests
- Clean Architecture and Domain-Driven Design: aggregate roots, value objects, domain events and
  domain exceptions modelled in `Yooresh.Domain`

## Contributing

Issues and pull requests are welcome — see the
[open issues](https://github.com/AlirezaSabouei/Yooresh/issues) and open a new one for bugs or balance
suggestions. Pull requests should branch off `dev`.

## License

[GNU General Public License v3.0](LICENSE)
