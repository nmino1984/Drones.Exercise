# Ejercicio Drones — API REST

API REST para gestionar una flota de drones capaces de transportar medicamentos. Ejercicio práctico basado en Clean Architecture con ASP.NET Core 10.

---

## Stack tecnológico

| Capa | Tecnología |
|---|---|
| Framework | ASP.NET Core 10.0 |
| ORM | Entity Framework Core 10.0 (SQL Server) |
| Mapeo | AutoMapper 16.1.1 |
| Validación | FluentValidation 11 |
| Documentación | Swagger / Swashbuckle |
| Tests | xUnit + Moq + EF Core InMemory |

---

## Arquitectura (Clean Architecture)

```
WebAPI → Application → Infrastructure → Domain
```

| Proyecto | Responsabilidad |
|---|---|
| `Drones.Domain` | Entidades puras |
| `Drones.Infrastructure` | EF Core, repositorios, UnitOfWork, BatteryMonitorService |
| `Drones.Application` | Servicios, validadores, mapeos, ViewModels |
| `Drones.WebAPI` | Controladores, Program.cs, Swagger |
| `Drones.Utilities` | Enums (`StateTypes`, `ModelTypes`), mensajes |

---

## Endpoints principales

| Verbo | Ruta | Descripción |
|---|---|---|
| `POST` | `/api/Dispatch/RegisterDrone` | Registrar drone |
| `POST` | `/api/Dispatch/LoadMedicationsToDrone` | Cargar medicamentos en un drone |
| `GET` | `/api/Dispatch/MedicationsByDrone/{id}` | Medicamentos cargados en un drone |
| `GET` | `/api/Dispatch/AvailableDrones` | Drones disponibles (IDLE + batería ≥ 25 %) |
| `GET` | `/api/Dispatch/DroneBattery/{id}` | Nivel de batería de un drone |
| `GET` | `/api/Dispatch/BatteryLogs` | Historial completo de auditoría de batería |
| `GET` | `/api/Drone` | Listar todos los drones |
| `GET` | `/api/Medication` | Listar todos los medicamentos |
| `POST` | `/api/Medication/Register` | Registrar medicamento |
| `GET` | `/api/BatteryLog` | Todos los registros de batería |
| `GET` | `/api/BatteryLog/drone/{id}` | Registros de batería por drone |

---

## Validaciones

**Drone:** SerialNumber requerido (máx 100 car.); Model en rango 1–4; State en rango 0–5; WeightLimit 0–500; BatteryCapacity y BatteryLevel 0–100.

**Medicamento:** Name requerido (solo letras, dígitos, `-` y `_`); Weight > 0; Code requerido (solo mayúsculas, dígitos y `_`).

---

## BatteryMonitorService

Servicio en segundo plano que cada **60 segundos** lee todos los drones y escribe un registro en la tabla `BatteryLog` con el nivel de batería actual. Usa `IServiceScopeFactory` para resolver un contexto fresco por ciclo. Los errores se registran en el log sin detener la aplicación.

---

## Tests

29 pruebas unitarias, todas pasando:

- **DispatchApplicationTests** (13): flujos felices y casos de error para registro de drones, carga de medicamentos, drones disponibles y nivel de batería.
- **MedicationValidatorTests** (13): nombre inválido, peso ≤ 0, código inválido, payload válido.
- **BatteryMonitorServiceTests** (3): verifica inserción de logs, valores correctos y caso sin drones.

```bash
dotnet test test/Drones.Test/Drones.Test.csproj
```

---

## Configuración y ejecución local

**Requisitos:** .NET 10 SDK, SQL Server LocalDB, `dotnet-ef` instalado globalmente.

```bash
# 1. Restaurar dependencias
dotnet restore

# 2. Aplicar migraciones
dotnet ef database update \
  --project src/Drones.Infrastructure/Drones.Infrastructure.csproj \
  --startup-project src/Drones.WebAPI/Drones.WebAPI.csproj

# 3. Ejecutar la API
dotnet run --project src/Drones.WebAPI/Drones.WebAPI.csproj --launch-profile http
```

Swagger UI disponible en: **http://localhost:5053/swagger/index.html**

---

> Repositorio: [github.com/nmino1984/Drones.Exercise](https://github.com/nmino1984/Drones.Exercise)
