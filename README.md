# EfFormAppProject

![C#](https://img.shields.io/badge/C%23-.NET%208-512BD4?logo=dotnet&logoColor=white) ![EF Core](https://img.shields.io/badge/EF%20Core-8-512BD4) ![SQL Server](https://img.shields.io/badge/SQL%20Server-Code%20First-CC2927?logo=microsoftsqlserver&logoColor=white) ![Platform](https://img.shields.io/badge/platform-Windows-0078D6?logo=windows&logoColor=white)

**Contents:** [Features](#features) · [Data model](#data-model) · [Project structure](#project-structure) · [Tech stack](#tech-stack) · [Getting started](#getting-started) · [Troubleshooting](#troubleshooting)

A Windows Forms student registration and course selection app built with **C# (.NET 8)** and **Entity Framework Core (Code First)** on **SQL Server**.

Built as a course project to practice relational modelling, EF Core migrations and desktop UI development.

## Features
- Register, search and update students (name, surname, student number)
- Assign students to classrooms, with a classroom quota
- Select lessons per student from a grid (many-to-many via `StudentLesson`)
- Data stored in SQL Server through EF Core migrations

## Data model
`Classroom` 1 — * `Student` * — * `Lesson` (join entity: `StudentLesson`)

## Project structure

```
EfFormAppProject/
  Data/ObsDbContext.cs   database context and connection string
  Models/                Classroom, Student, Lesson, StudentLesson
  Migrations/            EF Core migrations (create the database)
  Form1.cs               student registration and search
  DersSecimi.cs          lesson selection screen
  RegisterForm.cs        register form
  Program.cs             entry point
```

## Tech stack
- C# / .NET 8 (Windows Forms)
- Entity Framework Core 8 (SQL Server provider, Code First migrations)
- SQL Server

## Getting started
1. Install the .NET 8 SDK and SQL Server (LocalDB or Express works).
2. Set your own connection string in `EfFormAppProject/Data/ObsDbContext.cs` (`OnConfiguring`).
   Do not commit real credentials.
3. Create the database from the migrations:
   ```
   dotnet ef database update --project EfFormAppProject
   ```
4. Run the app:
   ```
   dotnet run --project EfFormAppProject
   ```

## Troubleshooting

- **`dotnet ef` is not recognized:** install the tool once with `dotnet tool install --global dotnet-ef`, then open a new terminal.
- **"Cannot open database" / "A network-related error occurred":** SQL Server is not running or the instance name in the connection string is wrong. Check the server name in SQL Server Management Studio and copy it into the connection string. For LocalDB use `(localdb)\MSSQLLocalDB`.
- **The database does not exist:** run the migration command from "Getting started" again; it creates the database.
- **Visual Studio cannot open the project:** install the ".NET desktop development" workload from the Visual Studio Installer.

## Author
Yusuf Yahya Değirmenci — [GitHub](https://github.com/yusufydegirmenci) · [LinkedIn](https://www.linkedin.com/in/yusufydegirmenci/)
