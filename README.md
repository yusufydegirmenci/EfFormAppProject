# EfFormAppProject

A Windows Forms student registration and course selection app built with **C# (.NET 8)** and **Entity Framework Core (Code First)** on **SQL Server**.

Built as a course project to practice relational modelling, EF Core migrations and desktop UI development.

## Features
- Register, search and update students (name, surname, student number)
- Assign students to classrooms, with a classroom quota
- Select lessons per student from a grid (many-to-many via `StudentLesson`)
- Data stored in SQL Server through EF Core migrations

## Data model
`Classroom` 1 — * `Student` * — * `Lesson` (join entity: `StudentLesson`)

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

## Author
Yusuf Yahya Değirmenci — [GitHub](https://github.com/23380101076) · [LinkedIn](https://www.linkedin.com/in/yusufydegirmenci/)
