# Yooresh — Project Status

A factual snapshot of where the project stands, so work can resume without reconstructing its history.

- **Base branch:** `dev` at `e8dd2a4` (2026-11-07)
- **This document:** authored on `docs/project-status` for issue [#40](https://github.com/AlirezaSabouei/Yooresh/issues/40)
- **Verification performed:** `dotnet build --no-restore` succeeds; `Yooresh.UnitTests` 74/74 pass; `Yooresh.AcceptanceTests` 23/23 fail (see [Test status](#test-status))

> The build and unit test results above were reproduced against this commit. Everything else is read
> from the source and marked accordingly. Anything described as a "domain foundation" is code that
> exists but is not reachable from an API endpoint or UI.

---

## 1. Game concept and intended first playable loop

Yooresh is a browser-based multiplayer strategy game in the classic village-builder genre. A player
registers, confirms their account, accumulates four resources (Food, Lumber, Stone, Gold), upgrades
resource and defensive buildings, trains soldiers, and attacks other players. Content is themed around
four factions (Orc, Human, Elf, Undead) and a world divided into regions and areas containing guilds.

**Intended first playable loop** — the smallest slice that constitutes a game session:

1. Sign up with name, email and password.
2. Confirm the account with the code from the confirmation email.
3. Log in and receive an access token + refresh token.
4. See the village: four resources and four resource buildings, plus a tower and a wall.
5. Resources accrue over time at a rate determined by the resource buildings.
6. Spend resources to upgrade a building; the upgrade takes time and finishes later.
7. Train soldiers in a militia building.
8. Attack another player and read the resulting war log.

Of these eight steps, **steps 1–4 are reachable in the API** but only **1–3 and 5 are actually
functional end-to-end**, and the Blazor client cannot currently complete step 1 (see
[Feature status](#3-feature-status)). Steps 6–8 have domain shapes but no implementation.

---

## 2. Solution structure

.NET 10 solution, Clean Architecture layout (`Yooresh.sln`) with a MediatR CQRS application layer and a
Blazor Server client.

| Project | Layer | What it actually contains today |
|---|---|---|
| [`src/Yooresh.Domain`](../src/Yooresh.Domain) | Domain | Entities, value objects, domain events, domain exceptions, capability interfaces. No dependencies. |
| [`src/Yooresh.Application`](../src/Yooresh.Application) | Application | MediatR commands/queries and handlers, FluentValidation validators, DTOs, AutoMapper profile, pipeline behaviours, JWT token service. |
| [`src/Yooresh.Infrustructure`](../src/Yooresh.Infrustructure) | Infrastructure | EF Core `Context`, entity configurations with seed data, one migration, Gmail SMTP sender, password hashing, Hangfire job wrapper. (Folder is misspelled; namespace/assembly is `Yooresh.Infrastructure`.) |
| [`src/Yooresh.API`](../src/Yooresh.API) | Presentation | ASP.NET Core Web API: accounts, resources, factions controllers; JWT auth; Swagger; Hangfire dashboard; global exception handler. |
| [`src/Yooresh.Village`](../src/Yooresh.Village) | Presentation | Blazor Server client. Calls the API over HTTP. **Currently out of sync with the API** (see below). |
| [`Tests/Yooresh.UnitTests`](../Tests/Yooresh.UnitTests) | Tests | NUnit + Shouldly, in-memory. The only trusted suite. |
| [`Tests/Yooresh.AcceptanceTests`](../Tests/Yooresh.AcceptanceTests) | Tests | `WebApplicationFactory` + in-memory SQLite. Currently all failing. |
| [`Tests/Yooresh.Blazor.Tests`](../Tests/Yooresh.Blazor.Tests) | Tests | Playwright end-to-end. Experimental; launches both processes and assumes fixed ports. |

The `Yooresh.Village` project name refers to the **client application**. The `Village` *aggregate* in the
domain is a different thing and has been removed — see [Known architectural questions](#5-known-architectural-questions).

---

## 3. Feature status

### 3.1 Implemented (reachable and working)

| Feature | Where | Notes |
|---|---|---|
| Player sign-up | [`AccountsController.SignUp`](../src/Yooresh.API/Controllers/AccountsController.cs), [`CreatePlayerCommand`](../src/Yooresh.Application/Accounts/Commands/CreatePlayerCommand.cs) | Validates name, email format, case-insensitive email uniqueness, password strength (≥6 chars, letter + digit). Hashes the password. Raises `PlayerCreatedEvent`. |
| Confirmation email | [`PlayerCreatedEventHandler`](../src/Yooresh.Application/Accounts/EventHandlers/PlayerCreatedEventHandler.cs), [`Email`](../src/Yooresh.Infrustructure/EmailTools/Email.cs) | Sends the confirmation code via Gmail SMTP. Send failures are swallowed by design. |
| Account confirmation | [`ConfirmAccountCommand`](../src/Yooresh.Application/Accounts/Commands/ConfirmAccountCommand.cs), [`Player.ConfirmPlayer`](../src/Yooresh.Domain/Entities/Accounts/Player.cs) | Sets `Confirmed`, raises `PlayerConfirmedEvent`. |
| Player asset initialisation | [`InitPlayerAssetsCommand`](../src/Yooresh.Application/Accounts/Commands/InitPlayerAssetsCommand.cs) | On confirmation, creates 4 `Resource` rows, 4 `ResourceBuilding` rows and 2 `DefensiveBuilding` rows (Tower, Wall) at level 0. |
| Login | [`LoginCommand`](../src/Yooresh.Application/Accounts/Commands/LoginCommand.cs), [`TokenService`](../src/Yooresh.Application/Common/Tools/TokenService.cs) | Requires a confirmed account. Verifies the hash. Returns a 60-minute JWT plus a stored 10-day refresh token. |
| Token refresh | [`RefreshCommand`](../src/Yooresh.Application/Accounts/Commands/RefreshCommand.cs) | Issues a new pair and marks the old refresh token revoked. |
| Resource read + accrual | [`GetResourcesQuery`](../src/Yooresh.Application/Resources/Queries/GetResourcesQuery.cs), [`Resource.CalculateAvailableAmount`](../src/Yooresh.Domain/Entities/Resources/Resource.cs) | `GET /api/resources` computes accrued amounts from `LastUpdateDate` and `HarvestRatePerMinute`. |
| Faction read | [`GetFactionsQuery`](../src/Yooresh.Application/Factions/Queries/GetFactionsQuery.cs), [`FactionConfiguration`](../src/Yooresh.Infrustructure/Persistence/Configurations/FactionConfiguration.cs) | Four factions seeded. `GET /api/factions`, authorized. |
| Upgrade catalogues | [`ResourceBuildingUpgradeConfiguration`](../src/Yooresh.Infrustructure/Persistence/Configurations/ResourceBuildingUpgradeConfiguration.cs), [`DefensiveBuildingUpgradeConfiguration`](../src/Yooresh.Infrustructure/Persistence/Configurations/DefensiveBuildingUpgradeConfiguration.cs) | Levels 0–20 seeded for each building type with cost, duration and bonus. **Data only — nothing consumes it yet.** |
| Domain event dispatch | [`Context.SaveChangesAsync`](../src/Yooresh.Infrustructure/Persistence/Context.cs) | Publishes pending domain events through MediatR after a successful save. |
| Shared plumbing | [`Program.cs`](../src/Yooresh.API/Program.cs), [`ConfigureServices`](../src/Yooresh.Infrustructure/ConfigureServices.cs) | JWT bearer auth, Swagger with bearer input, Hangfire dashboard, CORS, exception handler mapping `ValidationException` to 400. |

### 3.2 Partial — started, not usable

| Area | State | Gap |
|---|---|---|
| **Blazor client** | Pages exist for login, sign-up, confirm and village. | [`AccountServices`](../src/Yooresh.Village/Services/AccountServices.cs) still posts to `api/players/{signup,confirm,login}`, which no longer exists — every account call returns 404. [`ResourceBuildingsServices`](../src/Yooresh.Village/Services/ResourceBuildingsServices.cs) calls `api/resourceBuildings`, for which no controller exists; `IResourceBuildingsServices` is also not registered in [`Program.cs`](../src/Yooresh.Village/Program.cs), so the village page throws when it renders. The village itself is static placeholder markup. |
| **Resource economy** | Accrual maths works. | Nothing ever sets `HarvestRatePerMinute`. `InitResourcesCommand` creates resources with rate 0, and level-0 building seeds also have bonus 0. A new player therefore has 0 resources and 0 income, and `LastUpdateDate` is never persisted after calculation. |
| **Building upgrades** | Upgrade catalogue seeded; `ResourceBuilding` has `Level` and `IsUnderUpgrade`. | [`UpdateResourceBuildingCommand`](../src/Yooresh.Application/ResourceBuildings/Commands/UpdateResourceBuildingCommand.cs) and [`UpgradeResourceBuildingRequestedEventHandler`](../src/Yooresh.Application/ResourceBuildings/EventHandlers/UpgradeResourceBuildingRequestedEventHandler.cs) are entirely commented out. No endpoint, no cost check, no builder check, no timer wiring. |
| **Defensive buildings** | Domain entity, seeded upgrades, created per player on confirmation. | No query/controller/UI. No repair or upgrade. `IsUnderUpgrade` is always false. |
| **Refresh tokens** | Issued, stored, revoked on rotation. | `RefreshCommand` never checks `Expires` or `IsRevoked` on the presented token, so an expired or already-revoked token still mints a new pair. |
| **Authorization** | Role claim present; `SuperAdmin` role exists. | No endpoint distinguishes roles; `SuperAdmin` has no capabilities beyond a normal player. |
| **Tests (acceptance)** | Harness exists (SQLite in-memory, fake Hangfire/auth). | 23/23 fail. Tests call the removed `api/players/*` routes, and the fake auth handler is registered under a non-default scheme so authorized endpoints return 401. The handler also emits an `id` claim while [`BaseApiController`](../src/Yooresh.API/Controllers/BaseApiController.cs) reads `ClaimTypes.NameIdentifier`. |
| **Database schema** | EF Core model + configurations are current. | The only migration ([`20250802192403_init`](../src/Yooresh.Infrustructure/Migrations/20250802192403_init.cs)) and `ContextModelSnapshot` predate both the `Village` removal and the resource/defensive building refactor. Applying it creates `Village`, `Building`, `Tower`, `Wall`, `Troop`, `Farm`, `GoldMine`, `LumberMill`, `MetalMine`, `StoneMine` and **omits `ResourceBuilding`, `ResourceBuildingUpgrade`, `DefensiveBuilding`, `DefensiveBuildingUpgrade`**. It must be regenerated before the API can run against a fresh database. |
| **Caching** | `IMemoryCache` used for factions and building upgrade catalogues. | The upgrade queries ignore the requested level and building types when building the cache key. `GetDefensiveBuildingUpgradesQuery` stores whatever it first fetched under a constant key, so a later request for a different level receives that first result; `GetResourceBuildingUpgradesQuery` checks the same style of key but never populates it, so it always misses. |

### 3.3 Domain foundations only — modelled, not connected

These types exist in [`src/Yooresh.Domain`](../src/Yooresh.Domain) but have no `DbSet`, no EF
configuration, no application handler and no endpoint:

- Armies: [`Army`](../src/Yooresh.Domain/Entities/Armies/Army.cs), [`Squad`](../src/Yooresh.Domain/Entities/Armies/Squad.cs)
- Soldiers: [`Soldier`](../src/Yooresh.Domain/Entities/Soldiers/Soldier.cs), `SoldierReference`, [`PlayerSoldier`](../src/Yooresh.Domain/Entities/PlayerSoldiers/PlayerSoldier.cs)
- Training: [`MilitiaBuilding`](../src/Yooresh.Domain/Entities/MilitiaBuildings/MilitiaBuilding.cs) (Barracks, Stable)
- Combat log: [`WarLog`](../src/Yooresh.Domain/Entities/WarLogs/WarLog.cs) (body contains the comment "Log of soldiers and the result")
- World: [`Region`](../src/Yooresh.Domain/Entities/World/Region.cs), [`Area`](../src/Yooresh.Domain/Entities/World/Area.cs), [`Guild`](../src/Yooresh.Domain/Entities/World/Guild.cs)
- Interfaces: `IGatherable` (empty), `ITrainable`, `IUpgradableTemp`, `IJob`
- Exceptions: `NotAvailableBuildersException`, `NotAvailableTowerCapacityException`, `NotEnoughResourcesException`, `NotValidUpdateException`, `PlayerNotExistsException` are defined but **never thrown**.

### 3.4 Missing — not started

- Combat resolution: no attacker/defender maths, no battle outcome. `Attack`/`Defense` value objects
  and their operators exist, but nothing consumes them.
- Soldier training: no command, no cost deduction, no timed completion.
- Building construction/upgrade execution, builder capacity, repair.
- Army composition and movement; world map ownership.
- Guild membership and management.
- War reports / battle history UI.
- Client UI for buildings, upgrades, training or combat.
- Any integration/permission tests beyond the two API surfaces listed above.

---

## 4. Test status

| Suite | Result at `e8dd2a4` | Command |
|---|---|---|
| Unit | **74 passed, 0 failed** | `dotnet test Tests/Yooresh.UnitTests/Yooresh.UnitTests.csproj` |
| Acceptance | **0 passed, 23 failed** | `dotnet test Tests/Yooresh.AcceptanceTests/Yooresh.AcceptanceTests.csproj` |
| Blazor (Playwright) | Not run; experimental | Requires both apps running, Playwright browsers, and fixed ports |

Unit coverage is concentrated on `Player`, `Resource`, `ResourceValueObject`, the account
validators/handlers and password hashing. There are no tests for resource buildings, defensive
buildings, upgrades, domain event dispatch or infrastructure. `Tests/Yooresh.UnitTests/Application/Village/Commands/CreateVillageCommandHandlerTests.cs`
is an empty file left over from the `Village` removal, and the
`Tests/Yooresh.UnitTests/Application/Players` folder name no longer matches the `Accounts` commands it tests.

CI ([`.github/workflows`](../.github/workflows)) still triggers on `master` and `pull_request` to
`master`, while active development happens on `dev`. It also runs `dotnet test` over the whole solution,
so it would currently fail on the acceptance suite.

---

## 5. Known architectural questions

1. **Removal of the `Village` aggregate** (commit `e58097d`, "Remove village and related codes as it was
   not needed any more"). `Village` and `VillageReference` were deleted along with `CreateVillage`,
   `GetVillage`, `VillageExists` and `VillagesController`. In its place, `Resource`, `ResourceBuilding`
   and `DefensiveBuilding` each hold an owned `PlayerReference` directly. Consequences still unresolved:
   - What is the aggregate root for a player's holdings? `ResourceBuilding`/`DefensiveBuilding` derive
     from `BaseEntity`, not `RootEntity`, yet they are queried and saved individually.
   - Leftovers: `UpgradeResourceBuildingRequestedEvent` still takes an unused `villageId` parameter;
     commented-out `village`/`_context.Villages` code in `UpdateResourceBuildingCommand` and the upgrade
     event handler; an empty `CreateVillageCommandHandlerTests.cs`.
   - `Army.Player` was changed from a village reference to a `PlayerReference` in the same commit; the
     aggregate boundary between a player, their buildings and their army is still undecided.
2. **`PlayerReference` vs a plain `Guid`.** `PlayerReference : BaseEntityReference` is a mutable class
   carrying only an `Id`, owned by many entities. It behaves like a value but has identity semantics; it
   is worth deciding whether it earns its place over a `Guid PlayerId` foreign key.
3. **Where invariants live.** `ResourceValueObject`'s `<` and `>` operators are inconsistent (`>` requires
   every component to be greater, `<` requires any component to be less), no exception is thrown anywhere,
   and upgrade affordability/`NeedBuilderForUpgrade` are never enforced. The upgrade rules currently exist
   only as seeded data.
4. **Entities vs DTOs on the wire.** Queries return domain entities (`GetResourcesQuery` returns
   `List<Resource>`); `IEntityTypeConfiguration`-driven owned types and `AutoInclude` then leak into
   serialisation, which is why `ReferenceHandler.IgnoreCycles` is configured. A DTO pass is pending.
5. **Ownership of time-based state.** Accrual is computed lazily on read and never persisted, so
   `LastUpdateDate` never advances and the same elapsed time can be counted repeatedly. Hangfire is wired
   up with in-memory storage but no recurring job exists. Decide: lazy-with-persist, or background job.
6. **Domain event dispatch during save.** Events are published after `SaveChangesAsync` within the same
   `Context` call, and some handlers save again. This works but makes command handlers re-entrant; worth
   revisiting before more handlers depend on it.
7. **Committed secrets.** `Jwt:Key` has a real value in [`appsettings.json`](../src/Yooresh.API/appsettings.json)
   and `appsettings.Acceptance.json` contains a SQL Server password. Both should move to user secrets /
   environment configuration and be rotated.

---

## 6. Current branch and next recommended task

**Active development branch:** `dev` (tip `e8dd2a4`). Feature branches PR into `dev`; `master` is behind.

**Next recommended small issue — "Connect the Blazor client to the Accounts API":**

> Repoint the Blazor client at the endpoints that actually exist, so the register → confirm → log in →
> village path is reachable in a browser.

Suggested scope, small and self-contained (no domain changes):

1. In [`AccountServices.cs`](../src/Yooresh.Village/Services/AccountServices.cs), change
   `api/players/signup|confirm|login` to `api/accounts/signup|confirm|login`.
2. Add a read-only `GET /api/resourceBuildings` backed by a `GetResourceBuildingsQuery` that returns the
   current player's buildings, and point [`ResourceBuildingsServices.cs`](../src/Yooresh.Village/Services/ResourceBuildingsServices.cs)
   at it — or drop that component from the village page if the endpoint is out of scope.
3. Register `IResourceBuildingsServices` in [`Program.cs`](../src/Yooresh.Village/Program.cs) (`Program.cs` is currently missing it, so the village page cannot render).

Why this one: it is the shortest path to the first playable loop being observable, it needs no aggregate or
schema decisions, and it currently blocks every one of the four existing client pages.

**Immediately after that**, in order of leverage:

1. **Regenerate the EF migration and snapshot** against the current model so a fresh database is usable
   (see the schema row in [Feature status](#3-feature-status)). This is a prerequisite for any further
   end-to-end work, including issue [#40](https://github.com/AlirezaSabouei/Yooresh/issues/40)'s own
   "run the app" instructions in the README.
2. **Repair the acceptance test suite** — repoint it at `api/accounts/*`, make the fake auth scheme the
   default one, and align the claim type with `BaseApiController.PlayerId`.
3. **Apply harvest rates** so resources actually accrue (derive `Resource.HarvestRatePerMinute` from the
   player's resource buildings and persist `LastUpdateDate`).
4. **Implement the resource-building upgrade flow** (restore `UpdateResourceBuildingCommand`, enforce cost
   and builder availability, schedule completion through `IJob`).
5. **Implement soldier training** — the first feature that requires the militia/soldier foundations to be
   connected.

---

## 7. What this document deliberately does not claim

- That the game is playable in a browser — it is not, because the client's account calls 404.
- That the database can be created from the checked-in migration — it cannot, see above.
- That buildings, upgrades, soldiers, armies, combat, guilds or the world map are implemented. They are
  entities and seeded catalogues, not behaviour.
- That the acceptance or Playwright suites are meaningful signals at this commit. Only the unit suite is.
