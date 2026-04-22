# Drones Exercise — REST API

A REST API for managing a fleet of drones capable of carrying medication items. Built as a coding exercise demonstrating Clean Architecture, domain validation, background services, and automated testing in ASP.NET Core.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10.0 (Web API) |
| ORM | Entity Framework Core 10.0 (SQL Server) |
| Mapping | AutoMapper 16.1.1 |
| Validation | FluentValidation 11 |
| Documentation | Swashbuckle / Swagger UI |
| Testing | xUnit + Moq + EF Core InMemory |
| Background Services | `IHostedService` / `BackgroundService` |
| Dependency Injection | Microsoft.Extensions.DependencyInjection |

---

## Architecture

Clean Architecture — dependencies flow inward: `WebAPI → Application → Infrastructure → Domain`.

| Project | Role |
|---|---|
| `Drones.Domain` | Entities: `TDrone`, `TMedication`, `RDroneMedication`, `BatteryLog`, `NModel`, `NState` |
| `Drones.Infrastructure` | EF Core `DronesContext`, repositories, Unit of Work, `BatteryMonitorService`, DI wiring |
| `Drones.Application` | Service interfaces + implementations, AutoMapper profiles, FluentValidation validators, ViewModels, `BaseResponse<T>` |
| `Drones.WebAPI` | Controllers, `Program.cs`, Swagger config, `appsettings.json` |
| `Drones.Utilities` | Shared enums (`StateTypes`, `ModelTypes`) and `ReplyMessages` constants |

### Key patterns

- **Repository + Unit of Work** — `IGenericRepository<T>` handles CRUD; `IUnitOfWork` exposes `Drone`, `Medication`, and `DroneMedication` repositories.
- **Standard response envelope** — all endpoints return `BaseResponse<T>` (`IsSuccess`, `Message`, `Data`, `Errors`).
- **Soft business rules** — the fleet is capped at 10 drones; a drone must be in IDLE state with battery ≥ 25 % to be loaded.
- **Background audit** — `BatteryMonitorService` logs battery levels every 60 seconds automatically.

---

## Data Model

```
TDrone (1) ──── (Many) RDroneMedication (Many) ──── (1) TMedication
  Id, SerialNumber, Model (int), WeightLimit, BatteryCapacity, BatteryLevel, State (int)

BatteryLog
  Id, DroneId, SerialNumber, BatteryLevel, CheckedAt
```

**Enums**

| ModelTypes | Value |
|---|---|
| Lightweight | 1 |
| Middleweight | 2 |
| Cruiserweight | 3 |
| Heavyweight | 4 |

| StateTypes | Value |
|---|---|
| IDLE | 0 |
| LOADING | 1 |
| LOADED | 2 |
| DELIVERING | 3 |
| DELIVERED | 4 |
| RETURNING | 5 |

---

## Endpoints

### Dispatch — `/api/Dispatch`

| Verb | Route | Description |
|---|---|---|
| `POST` | `/api/Dispatch/RegisterDrone` | Register a new drone in the fleet |
| `POST` | `/api/Dispatch/LoadMedicationsToDrone` | Load a list of medications onto a drone |
| `GET` | `/api/Dispatch/MedicationsByDrone/{droneId}` | List medications currently loaded on a drone |
| `GET` | `/api/Dispatch/AvailableDrones` | List drones available for loading (IDLE + battery ≥ 25 %) |
| `GET` | `/api/Dispatch/DroneBattery/{droneId}` | Get current battery level of a drone |
| `GET` | `/api/Dispatch/BatteryLogs` | Get the full battery audit history |

### Drone — `/api/Drone`

| Verb | Route | Description |
|---|---|---|
| `GET` | `/api/Drone` | List all drones |
| `GET` | `/api/Drone/{id}` | Get drone by ID |
| `PUT` | `/api/Drone/Edit/{id}` | Update drone data |
| `DELETE` | `/api/Drone/Delete/{id}` | Delete a drone |

### Medication — `/api/Medication`

| Verb | Route | Description |
|---|---|---|
| `GET` | `/api/Medication` | List all medications |
| `GET` | `/api/Medication/{id}` | Get medication by ID |
| `POST` | `/api/Medication/Register` | Register a new medication |
| `PUT` | `/api/Medication/Edit/{id}` | Update medication data |
| `DELETE` | `/api/Medication/Delete/{id}` | Delete a medication |

### Battery Log — `/api/BatteryLog`

| Verb | Route | Description |
|---|---|---|
| `GET` | `/api/BatteryLog` | Get all battery audit records |
| `GET` | `/api/BatteryLog/drone/{droneId}` | Get battery records filtered by drone |

---

## Validations

### DroneValidator (`DroneRequestViewModel`)

| Field | Rule |
|---|---|
| `SerialNumber` | Required, 1–100 characters |
| `Model` | Must be a defined `ModelTypes` enum value (1–4) |
| `State` | Must be a defined `StateTypes` enum value (0–5) |
| `WeightLimit` | 0–500 g |
| `BatteryCapacity` | 0–100 % |
| `BatteryLevel` | 0–100 % |

### MedicationValidator (`MedicationRequestViewModel`)

| Field | Rule |
|---|---|
| `Name` | Required; only letters, digits, `-` and `_` |
| `Weight` | Required; must be `> 0` |
| `Code` | Required; only uppercase letters, digits and `_` |

---

## Battery Monitor Service

`BatteryMonitorService` is a `BackgroundService` registered as `IHostedService`.

- **Interval:** every **60 seconds**
- **What it does:** reads all drones with `AsNoTracking`, creates one `BatteryLog` record per drone (DroneId, SerialNumber, BatteryLevel, CheckedAt = UTC now), then bulk-inserts via `AddRangeAsync` + `SaveChangesAsync`.
- **Resilience:** uses `IServiceScopeFactory` to resolve a fresh `DronesContext` per tick (context is Transient). `OperationCanceledException` is caught silently on shutdown; all other exceptions are logged without crashing the host.

---

## Tests

**Framework:** xUnit + Moq + EF Core InMemory
**Total:** 29 tests — all passing

| File | Class | Tests | What is covered |
|---|---|---|---|
| `DispatchApplicationTests.cs` | `DispatchApplicationTests` | 13 | RegisterDrone (happy path, fleet limit, invalid model, invalid state, weight > 500, empty serial); LoadDrone (happy, battery < 25 %, weight exceeded, drone not available); CheckAvailableDrones (filters correctly, empty); CheckBattery (found, not found) |
| `MedicationValidatorTests.cs` | `MedicationValidatorTests` | 13 | Valid payload; invalid name (3 cases); null name; weight = 0 / negative / null (4 cases); invalid code (3 cases) |
| `BatteryMonitorServiceTests.cs` | `BatteryMonitorServiceTests` | 3 | Inserts one log per drone; log contains correct values; no drones → no logs |

### Running tests

```bash
dotnet test test/Drones.Test/Drones.Test.csproj
```

With coverage:

```bash
dotnet test test/Drones.Test/Drones.Test.csproj --collect:"XPlat Code Coverage"
```

---

## Configuration & Local Setup

### Prerequisites

- .NET 10 SDK
- SQL Server LocalDB (`(localdb)\MSSQLLocalDB`)
- `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`

### 1. Clone and restore

```bash
git clone https://github.com/nmino1984/Drones.Exercise.git
cd Drones.Exercise
dotnet restore
```

### 2. Configure connection string

`src/Drones.WebAPI/appsettings.json`:

```json
"ConnectionStrings": {
  "DBConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Database=DronesDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 3. Apply migrations

```bash
dotnet ef database update \
  --project src/Drones.Infrastructure/Drones.Infrastructure.csproj \
  --startup-project src/Drones.WebAPI/Drones.WebAPI.csproj
```

### 4. Run the API

```bash
dotnet run --project src/Drones.WebAPI/Drones.WebAPI.csproj --launch-profile http
```

Swagger UI: **http://localhost:5053/swagger/index.html**

### 5. Run tests

```bash
dotnet test test/Drones.Test/Drones.Test.csproj
```
