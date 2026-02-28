#  CrewSchedule ✈️

A crew scheduling management system for airlines. Enables assigning pilots and flight attendants to flights, tracking flight hours, managing crew swap requests, and enforcing rest time requirements in accordance with ICAO standards.

> Built by a former flight attendant — with a real understanding of aviation operations.

---

##  Architecture

The project is based on **Clean Architecture** (by Jason Taylor) with **DDD** and **CQRS** patterns.

```
CrewSchedule.Domain          # Business logic, aggregates, domain events, policies
CrewSchedule.Application     # CQRS commands/queries, validation, interfaces
CrewSchedule.Infrastructure  # EF Core, Dapper, repositories, DB configurations
CrewSchedule.WebApi          # REST API, controllers, middleware
```

Two separate databases are used intentionally: CrewScheduleDb for the core scheduling domain (flights, assignments, swap requests) and FlightHoursDb for flight hour tracking. This reflects a conscious separation of domain boundaries — each context owns its data and can evolve independently.

### Layer dependencies
```
WebApi → Application → Domain
Infrastructure → Application → Domain
```

Infrastructure and WebApi depend on Application, but not on each other. Domain has no dependencies.

---

##  Tech Stack

| Technology | Purpose |
|---|---|
| .NET 10 | Platform |
| ASP.NET Core Web API | REST API |
| Entity Framework Core 10 | Write-side persistence |
| Dapper | Read-side queries |
| MediatR | CQRS and domain events |
| FluentValidation | Command validation |
| AutoMapper | DTO mapping |
| Serilog | Logging |
| SQL Server / LocalDB | Database |

---

##  Key Features

- **Crew assignment to flights** — with composition validation (minimum 2 pilots, 4 flight attendants) per ICAO standards
- **Rest time enforcement** — minimum 12 hours between duties
- **Flight hour tracking** — 900-hour annual limit (rolling 12 months)
- **Swap requests** — crew swaps with approve, reject, and cancel flows
- **Check-in / Check-out** — crew presence tracking per flight
- **Domain events** — automatic data updates on swap approval

---

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server or LocalDB

### Setup

1. Clone the repository:
```bash
git clone https://github.com/AigerimAA/CrewSchedule.git
cd CrewSchedule
```

2. Configure connection strings in `CrewSchedule.WebApi/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "CrewConnection": "Server=(localdb)\\mssqllocaldb;Database=CrewScheduleDb;Trusted_Connection=True;",
    "FlightHoursConnection": "Server=(localdb)\\mssqllocaldb;Database=FlightHoursDb;Trusted_Connection=True;"
  }
}
```

3. Apply migrations:
```bash
dotnet ef database update --project CrewSchedule.Infrastructure --startup-project CrewSchedule.WebApi
```

4. Run the project:
```bash
dotnet run --project CrewSchedule.WebApi
```

5. Open Swagger:
```
https://localhost:{port}/swagger
```

---

## 📁 Project Structure

```
CrewSchedule/
├── CrewSchedule.Domain/
│   ├── Aggregates/          # CrewFlightHours, SwapRequest
│   ├── Entities/            # Flight, Assignment, CrewMember
│   ├── Enums/               # CrewRole, SwapStatus
│   ├── Events/              # Domain events
│   ├── Policies/            # IRestPolicy, ICrewCompositionPolicy
│   └── ValueObjects/        # FlightTime
│
├── CrewSchedule.Application/
│   ├── Behaviors/           # ValidationBehavior (MediatR pipeline)
│   ├── Commands/            # CQRS commands and handlers
│   ├── Queries/             # CQRS queries and handlers
│   ├── DomainEventHandlers/ # Domain event handlers
│   ├── IntegrationEvents/   # Integration events
│   ├── Exceptions/          # NotFoundException, ForbiddenAccessException
│   ├── Interfaces/          # Repository and context interfaces
│   ├── Validators/          # FluentValidation validators
│   ├── DTO/                 # Data Transfer Objects
│   └── Mappings/            # AutoMapper profiles
│
├── CrewSchedule.Infrastructure/
│   ├── Persistence/
│   │   ├── Configurations/  # EF Core entity configurations
│   │   ├── Dapper/          # Read repositories using Dapper
│   │   ├── CrewDbContext.cs
│   │   └── FlightHoursDbContext.cs
│   ├── Repositories/        # Repository implementations
│   └── DependencyInjection.cs
│
└── CrewSchedule.WebApi/
    ├── Controllers/         # REST controllers
    ├── Middleware/          # ExceptionMiddleware
    └── Program.cs
```

---

##  API Endpoints

| Method | URL | Description |
|---|---|---|
| GET | `/api/flights` | Get all flights |
| GET | `/api/flights/{id}` | Get flight by ID |
| GET | `/api/flights/{id}/crew` | Get crew for a flight |
| POST | `/api/flights/{id}/checkin` | Check in a crew member |
| POST | `/flights/assign` | Assign full crew to a flight |

---

##  Business Rules

- Minimum crew composition: **1 Captain**, **1 IFS**, **2+ pilots**, **4+ flight attendants**
- Minimum rest between duties: **12 hours**
- Annual flight hour limit: **900 hours** (rolling 12 months)
- Swap requests can only be processed when in `Pending` status
- Only the target crew member (`ToCrewMember`) can approve or reject a swap
- Only the requester (`FromCrewMember`) can cancel a swap
