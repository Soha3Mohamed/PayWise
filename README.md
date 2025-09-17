💳 PayWise

PayWise is a Fintech-inspired .NET 8 Web API project that demonstrates user management, wallet operations, and transaction handling in a clean, modular architecture.

It’s built as a learning and showcase project to highlight skills in:

ASP.NET Core Web API

Entity Framework Core (Code-First & Relationships)

Authentication & Authorization (JWT)

DTO Mapping (Mapster)

Service & Repository Pattern

Transaction Management & Wallet Logic

Swagger/OpenAPI Documentation

🚀 Features
👤 User Management

Register, login, and authenticate with JWT

Role-based access (User / Admin)

Each user automatically gets a wallet on registration

💰 Wallets

Wallet is linked 1:1 with a user

Supports deposit, withdraw, and balance checking

Transaction records are automatically generated

🔄 Transactions

Deposit / Withdraw / Transfer between wallets

Each transaction is recorded with:

Amount

Source Wallet

Destination Wallet

Type (Deposit / Withdraw / Transfer)

Status (Pending / Completed / Failed)

Timestamps

🛠️ Tech Stack

.NET 8 (ASP.NET Core Web API)

Entity Framework Core

SQL Server (local / containerized)

Swagger / OpenAPI for documentation

Mapster for DTO mapping

JWT Authentication

Serilog for structured logging

📂 Project Structure
PayWise/
│── PayWise.Api/              # Web API Layer (Controllers, Swagger, Auth)
│── PayWise.Application/      # Business Logic (Services, Interfaces, DTOs)
│── PayWise.Domain/           # Core Entities & Enums
│── PayWise.Infrastructure/   # Data Layer (EF Core, Repositories, DbContext)
│── PayWise.Tests/            # Unit & Integration Tests (xUnit)

📸 Screenshots

Here are some Swagger examples of the available endpoints:

🔑 User Endpoints
![Screenshot_17-9-2025_1465_localhost c](https://github.com/user-attachments/assets/87840565-f472-414e-8263-da2855550a66)

💼 Wallet Endpoints
![Screenshot_17-9-2025_14635_localhost](https://github.com/user-attachments/assets/a3fc97fd-1c9b-455f-9176-5c085c41df2b)

🔄 Transaction Endpoints
![Screenshot_17-9-2025_14635_localhost](https://github.com/user-attachments/assets/293c751d-5328-42e5-9081-f3e5db0e8d94)

⚡ Getting Started
1️⃣ Clone the repository
git clone https://github.com/your-username/paywise.git
cd paywise

2️⃣ Set up the database

Update appsettings.json connection string to point to your SQL Server instance

Apply migrations:

dotnet ef database update

3️⃣ Run the API
dotnet run --project PayWise.Api

4️⃣ Explore the Swagger UI

Navigate to:

https://localhost:5001/swagger

✅ Example Workflows
Register & Login

Register a new user

Automatically creates a wallet with 0 balance

Login to get a JWT token

Deposit & Withdraw

Call wallet deposit/withdraw endpoints

Transaction entry is automatically recorded

Transfer

Transfer funds from one wallet to another

Creates both debit and credit transaction records

🧪 Testing

Unit Tests (services, validation)

Integration Tests (API + DB + Auth)

Run tests with:

dotnet test

🎯 Why PayWise?

This project demonstrates end-to-end backend skills for fintech-like systems:

Domain-driven design

Clean architecture layering

Secure authentication

Realistic financial operations

Logging & testing best practices

📌 Next Steps

Add front-end (React/Vite + Material UI)

Add Redis

Extend transaction types (scheduled payments, refunds)

Add external API integrations (Stripe, PayPal sandbox, etc.)

👨‍💻 Author

Built with ❤️ by Soha Mohamed
🔗 [LinkedIn](https://www.linkedin.com/in/soha-mohamed-2a7bb626b/)
 | [GitHub](https://github.com/Soha3Mohamed/)
