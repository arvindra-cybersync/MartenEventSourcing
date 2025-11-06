# Marten Event Sourcing System (.NET 8 Clean Architecture)

# MartenEventSourcing
This project demonstrates a **production-grade Event Sourcing system** built with **.NET 8**, **PostgreSQL**, and **Marten**.   It follows **Clean Architecture** and **SOLID** principles, using Marten to handle event storage, projections, and document persistence on top of PostgreSQL.

The system models a simple **Order Management Domain** and showcases how to:
- Capture **domain events** (`OrderCreated`, `ItemAdded`, `OrderShipped`)
- Persist events and snapshots using **Marten Event Store**
- Automatically build **read models (async projections)**
- Use **rebuildable projections** for real-time query views
- Apply **CQRS (Command Query Responsibility Segregation)** pattern
- Provide **API endpoints** for managing orders and rebuilding projections

---

## 🧩 Architecture

MartenEventSourcing/
│
├── Domain/
│ ├── Entities/
│ │ └── Order.cs
│ ├── Events/
│ │ ├── OrderCreated.cs
│ │ ├── ItemAdded.cs
│ │ └── OrderShipped.cs
│ └── Aggregates/
│ └── OrderAggregate.cs
│
├── Application/
│ ├── Commands/
│ │ ├── CreateOrderCommand.cs
│ │ ├── AddItemCommand.cs
│ │ └── ShipOrderCommand.cs
│ └── Services/
│ └── OrderService.cs
│
├── Infrastructure/
│ ├── Marten/
│ │ └── MartenConfiguration.cs
│ └── Projections/
│ └── OrderSummaryProjection.cs
│
├── WebApi/
│ ├── Controllers/
│ │ ├── OrdersController.cs
│ │ └── AdminController.cs
│ └── Program.cs
│
└── README.md

---

## ⚙️ Technologies Used

| Component | Description |
|------------|-------------|
| **.NET 8 (C#)** | Core platform for API and domain logic |
| **Marten** | Event Sourcing and Document DB library on PostgreSQL |
| **PostgreSQL** | Event store and data persistence layer |
| **ASP.NET Core Web API** | API surface for commands and projection rebuild |
| **Clean Architecture** | Separation of concerns between Domain, Application, Infrastructure, and Web layers |
| **CQRS Pattern** | Commands and Queries are handled separately for clarity and scalability |

---

## 🧠 What Marten Adds to .NET

Marten transforms PostgreSQL into a **fully capable Event Store** and **Document Database**.

### 🔑 Key Advantages of Using Marten
1. **Event Sourcing Ready**  
   Store domain events directly — no custom serialization or event tables needed.

2. **Async Projections**  
   Automatically build **read models** (e.g., `OrderSummaryProjection`) from events.

3. **Rebuildable Projections**  
   You can **rebuild your read models anytime** (via `/api/admin/projections/rebuild`) to regenerate consistent views.

4. **Aggregations**  
   Marten automatically aggregates event streams into the latest entity state.

5. **Strong Consistency**  
   Since events and snapshots live in PostgreSQL, it ensures atomic persistence and transaction guarantees.

6. **No External Message Bus Needed**  
   Marten handles async projections internally — no need for Kafka/RabbitMQ.

7. **Multi-tenancy & Outbox Support**  
   Built-in support for multi-tenant systems and reliable event publishing.

---

## 🚀 Getting Started

### 1️⃣ Prerequisites
- **.NET 8 SDK**
- **PostgreSQL 15+**
- **pgAdmin 4** (optional, for DB management)

### 2️⃣ PostgreSQL Setup

1. Ensure PostgreSQL is running.
2. Note your PostgreSQL password and connection string (example below):

Host=localhost;Port=5432;Database=MartenES;Username=postgres;Password=your_password


### 3️⃣ Build & Run

In **Visual Studio**:
1. Set **WebApi** as the startup project.
2. Hit **Run (F5)**.

The API will start (default on `https://localhost:5001` or `http://localhost:5000`).

---

## 🧪 API Endpoints

### 🧱 Order Operations
| HTTP | Endpoint | Description |
|------|-----------|-------------|
| `POST` | `/api/orders/create` | Create a new order |
| `POST` | `/api/orders/{orderId}/add-item` | Add an item to an order |
| `POST` | `/api/orders/{orderId}/ship` | Mark the order as shipped |
| `GET`  | `/api/orders/summary` | Get read model of all orders (projection) |

### 🧰 Admin Operations
| HTTP | Endpoint | Description |
|------|-----------|-------------|
| `POST` | `/api/admin/projections/rebuild` | Rebuild projections (e.g., OrderSummaryProjection) |
| `GET`  | `/api/admin/projections/status` | Check projection daemon status |

---

## 🧮 Example Flow

1. **Create an Order**
```bash
POST /api/orders/create
{
  "customerId": "a1b2c3d4-0000-0000-0000-111122223333",
  "customerName": "John Doe"
}


🧩 Features Implemented

✅ Event Sourcing
✅ CQRS pattern (commands + queries separated)
✅ Async projections for read models
✅ Projection rebuild API
✅ PostgreSQL as event store
✅ Clean Architecture (Domain / Application / Infrastructure / Web)
✅ Marten configuration for event store setup
✅ Aggregate-based state reconstruction

🛠 Next Steps (Advanced Marten Features)
Here are the features you can add next — Marten supports them natively:
Live Aggregation: Query real-time aggregates without rebuilding projections.
Stream Archiving / Deletion: Soft delete event streams while preserving history.
Event Versioning: Add version tolerance for evolving event schemas.
Snapshotting: Improve performance for large aggregates.
Outbox Pattern: Integrate with external services safely and transactionally.
Multi-Tenancy: Support multiple tenants in one database.

👨‍💻 Author
Developed by: Arvindra Solanki
Stack: .NET 8 + Marten + PostgreSQL
Architecture: Clean Architecture + SOLID Principles
