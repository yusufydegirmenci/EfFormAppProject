# EfFormAppProject

![C#](https://img.shields.io/badge/C%23-.NET%208-512bd4) ![EF Core](https://img.shields.io/badge/EF%20Core-8-informational) ![SQL Server](https://img.shields.io/badge/SQL%20Server-Code%20First-cc2927) ![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-0078d4)

A small Windows Forms app for registering students, putting them in classrooms and choosing their lessons. Built as a course project to practice relational modelling, EF Core migrations and desktop UI development.

## Features
- Register, search and update students (name, surname, student number)
- Assign students to classrooms, with a classroom quota
- Select lessons per student from a grid (many-to-many via `StudentLesson`)
- Data stored in SQL Server through EF Core migrations

## Data model
`Classroom` 1 — * `Student` * — * `Lesson` (join entity: `StudentLesson`)

## Technology and infrastructure

| Part | What it uses | Notes |
|---|---|---|
| Language | C# on .NET 8 | The project targets `net8.0-windows`. |
| Interface | Windows Forms | Windows only. |
| Data access | Entity Framework Core 8 | `Microsoft.EntityFrameworkCore.SqlServer` and `.Tools` 8.0.0, Code First with migrations. |
| Database | SQL Server | LocalDB or Express is enough. The database is created from the migrations. |
| Connection | `OBS_CONNECTION_STRING` | Read from an environment variable in `Data/ObsDbContext.cs`, so no password ends up in the code. |

## Getting started
1. Install the .NET 8 SDK and SQL Server (LocalDB or Express works).
2. Choose the database connection. By default the app uses
   `Server=localhost;Database=EfFormProject;Integrated Security=true;TrustServerCertificate=true;`.
   To use another server, set the `OBS_CONNECTION_STRING` environment variable (see `ObsDbContext.OnConfiguring`).
   Never commit real credentials.
3. Create the database from the migrations:
   ```
   dotnet ef database update --project EfFormAppProject
   ```
4. Run the app:
   ```
   dotnet run --project EfFormAppProject
   ```

## Project structure
```
EfFormAppProject/
  Data/ObsDbContext.cs     DbContext, relationships, connection string lookup
  Models/                  Student, Classroom, Lesson, StudentLesson
  Migrations/              EF Core Code First migrations
  Form1.cs                 add / find / update students, classroom choice
  DersSecimi.cs            lesson selection screen
  RegisterForm.cs         student form for an existing record (edit and lesson selection)
```

## Notes
This is a learning project, not production code. Authentication and validation are intentionally simple.

## Author
Yusuf Yahya Değirmenci — [GitHub](https://github.com/yusufydegirmenci) · [LinkedIn](https://www.linkedin.com/in/yusufydegirmenci/)
