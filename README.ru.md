# CrewSchedule ✈️ 

Система управления расписанием экипажа для авиакомпании. Позволяет назначать пилотов и бортпроводников на рейсы, отслеживать налёт часов, управлять заменами (swap) между членами экипажа и контролировать время отдыха согласно стандартам ИКАО.

> Проект создан бывшим бортпроводником — с пониманием реальных авиационных процессов.

---

## Архитектура

Проект построен на принципах **Clean Architecture** (по Jason Taylor) с применением **DDD** и **CQRS**.

```
CrewSchedule.Domain          # Бизнес-логика, агрегаты, доменные события, политики
CrewSchedule.Application     # CQRS команды/запросы, валидация, интерфейсы
CrewSchedule.Infrastructure  # EF Core, Dapper, репозитории, конфигурации БД
CrewSchedule.WebApi          # REST API, контроллеры, middleware
```

В проекте намеренно используются две отдельные базы данных: CrewScheduleDb — для основного домена (рейсы, назначения, запросы на замену) и FlightHoursDb — для учёта лётных часов. Это осознанное разделение по доменным границам: каждый контекст владеет своими данными и может развиваться независимо.

### Зависимости между слоями
```
WebApi → Application → Domain
Infrastructure → Application → Domain
```

Infrastructure и WebApi зависят от Application, но не друг от друга. Domain ни от кого не зависит.

---

## Технологии

| Технология | Назначение |
|---|---|
| .NET 10 | Платформа |
| ASP.NET Core Web API | REST API |
| Entity Framework Core 10 | Запись данных (write-side) |
| Dapper | Чтение данных (read-side) |
| MediatR | CQRS, доменные события |
| FluentValidation | Валидация команд |
| AutoMapper | Маппинг DTO |
| Serilog | Логирование |
| SQL Server / LocalDB | База данных |

---

## Ключевые возможности

- **Назначение экипажа на рейс** — с проверкой состава (минимум 2 пилота, 4 бортпроводника) согласно стандартам ИКАО
- **Контроль времени отдыха** — минимум 12 часов между рейсами
- **Учёт налёта** — годовой лимит 900 часов (скользящий год)
- **Swap-запросы** — замена членов экипажа с подтверждением, отклонением и отменой
- **Check-in / Check-out** — фиксация присутствия экипажа на рейсе
- **Доменные события** — автоматическое обновление данных при approve swap

---

## Быстрый старт

### Требования
- .NET 10 SDK
- SQL Server или LocalDB

### Установка

1. Клонируй репозиторий:
```bash
git clone https://github.com/AigerimAA/CrewSchedule.git
cd CrewSchedule
```

2. Настрой строки подключения в `CrewSchedule.WebApi/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "CrewConnection": "Server=(localdb)\\mssqllocaldb;Database=CrewScheduleDb;Trusted_Connection=True;",
    "FlightHoursConnection": "Server=(localdb)\\mssqllocaldb;Database=FlightHoursDb;Trusted_Connection=True;"
  }
}
```

3. Применить миграции:
```bash
dotnet ef database update --project CrewSchedule.Infrastructure --startup-project CrewSchedule.WebApi
```

4. Запустить проект:
```bash
dotnet run --project CrewSchedule.WebApi
```

5. Открыть Swagger:
```
https://localhost:{port}/swagger
```

---

##  Структура проекта

```
CrewSchedule/
├── CrewSchedule.Domain/
│   ├── Aggregates/          # CrewFlightHours, SwapRequest
│   ├── Entities/            # Flight, Assignment, CrewMember
│   ├── Enums/               # CrewRole, SwapStatus
│   ├── Events/              # Доменные события
│   ├── Policies/            # IRestPolicy, ICrewCompositionPolicy
│   └── ValueObjects/        # FlightTime
│
├── CrewSchedule.Application/
│   ├── Behaviors/           # ValidationBehavior (MediatR pipeline)
│   ├── Commands/            # CQRS команды и обработчики
│   ├── Queries/             # CQRS запросы и обработчики
│   ├── DomainEventHandlers/ # Обработчики доменных событий
│   ├── IntegrationEvents/   # Интеграционные события
│   ├── Exceptions/          # NotFoundException, ForbiddenAccessException
│   ├── Interfaces/          # Интерфейсы репозиториев и контекстов
│   ├── Validators/          # FluentValidation валидаторы
│   ├── DTO/                 # Data Transfer Objects
│   └── Mappings/            # AutoMapper профили
│
├── CrewSchedule.Infrastructure/
│   ├── Persistence/
│   │   ├── Configurations/  # EF Core конфигурации сущностей
│   │   ├── Dapper/          # Read-репозитории на Dapper
│   │   ├── CrewDbContext.cs
│   │   └── FlightHoursDbContext.cs
│   ├── Repositories/        # Реализации репозиториев
│   └── DependencyInjection.cs
│
└── CrewSchedule.WebApi/
    ├── Controllers/         # REST контроллеры
    ├── Middleware/          # ExceptionMiddleware
    └── Program.cs
```

---

## API эндпоинты

| Метод | URL | Описание |
|---|---|---|
| GET | `/api/flights` | Получить все рейсы |
| GET | `/api/flights/{id}` | Получить рейс по ID |
| GET | `/api/flights/{id}/crew` | Получить экипаж рейса |
| POST | `/api/flights/{id}/checkin` | Check-in члена экипажа |
| POST | `/flights/assign` | Назначить экипаж на рейс |

---

## Бизнес-правила

- Минимальный состав экипажа: **1 Captain**, **1 IFS**, **2+ пилота**, **4+ бортпроводника**
- Минимальный отдых между дежурствами: **12 часов**
- Годовой лимит налёта: **900 часов** (скользящие 12 месяцев)
- Swap возможен только со статусом `Pending`
- Подтвердить/отклонить swap может только целевой член экипажа (`ToCrewMember`)
- Отменить swap может только инициатор (`FromCrewMember`)
