# FitnessCenterDatabase - Layered Architecture

## Architecture

The project is separated into layers:

Presentation
-> Services
-> Repositories
-> Data
-> MySQL

Models are shared between the layers.

## Folders

- `Models` - database entities.
- `Data` - database connection and initialization.
- `Repositories` - SQL/data access.
- `Services` - business rules, seeding, triggers and logging.
- `Presentation` - console output.
- `Database` - SQL schema.
- `Logs` - application log file.

## Before running

Open `Program.cs` and replace:

`YOUR_PASSWORD`

with your MySQL root password.

Then run:

```powershell
dotnet restore
dotnet build
dotnet run
```

The database used is `FitnessCenter`.
