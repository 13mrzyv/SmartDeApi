# SmartDelivery API

Minimal API foundation targeting .NET 10. Feature endpoints live beside their slice code and are discovered through `MapFeatureEndpoints()`.

## Database migration

After installing the .NET 10 SDK and configuring the PostgreSQL connection string, from this directory run:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.14
dotnet ef migrations add InitialMigration
dotnet ef database update
```

The database schema is managed by EF Core migrations. Hangfire creates and maintains its own storage schema when PostgreSQL is available.

## Configuration

Replace the sample password in `appsettings.json` before running locally. The API exposes `/health`, `/scalar/v1`, and `/hangfire`.
