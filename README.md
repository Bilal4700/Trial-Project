# Raptor Trial Calculator

Blazor Server calculator trial project with user registration, login tracking, and calculation history backed by Entity Framework Core.

## Project Structure

- `CalculatorApp/Components` contains the Blazor UI.
- `CalculatorApp/Services` contains the authentication, session, and calculator logic used by the UI.
- `CalculatorApp/Data` contains the Entity Framework database context.
- `CalculatorApp/Models` contains the user, login log, and calculation log entities.
- `CalculatorApp/Migrations` contains the EF Core database migrations.

## Prerequisites

- .NET SDK compatible with the `net10.0` target framework used by this project.
- Optional: Visual Studio Code with the C# Dev Kit extension for code navigation and debugging.

## Run Locally

From the repository root:

```powershell
dotnet restore .\CalculatorApp\CalculatorApp.csproj
dotnet build .\CalculatorApp\CalculatorApp.csproj
dotnet run --project .\CalculatorApp\CalculatorApp.csproj --urls http://127.0.0.1:5280
```

Open `http://127.0.0.1:5280` in a browser.

## App Flow

1. Create a new account from the login page.
2. Sign in with that account.
3. Open the calculator page.
4. Enter two numbers and choose add or multiply.
5. Review the saved calculation history on the calculator page.

The app logs successful logins and calculation attempts to the configured database.

## Database

The project uses EF Core migrations. If the database needs to be recreated, run:

```powershell
dotnet ef database update --project .\CalculatorApp\CalculatorApp.csproj
```

To create a new migration after model changes:

```powershell
dotnet ef migrations add <MigrationName> --project .\CalculatorApp\CalculatorApp.csproj
```

## Repository Hygiene

Generated build outputs are intentionally ignored. Do not commit `bin/`, `obj/`, local IDE settings, or SQLite sidecar files such as `*.db-wal` and `*.db-shm`.
