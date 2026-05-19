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

## Deploy to Azure App Service

This project can deploy to Azure App Service as a .NET 10 Blazor Server app.

1. Create an Azure Web App using the `.NET 10` runtime and Windows or Linux OS.
2. In GitHub, add these repository secrets for the workflow:
   - `AZURE_WEBAPP_NAME`: the Azure Web App name.
   - `AZURE_WEBAPP_PUBLISH_PROFILE`: the publish profile XML contents from Azure.
3. Push changes to `main` or run the workflow manually from the Actions tab.
4. If you need a production DB path override, set an App Setting in Azure:
   - `ConnectionStrings__DefaultConnection`
   - Value: `Data Source=D:\home\site\wwwroot\calculator.db`

The workflow at `.github/workflows/azure-webapp.yml` restores, builds, publishes, and deploys `CalculatorApp`.

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
