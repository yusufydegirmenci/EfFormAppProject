using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfFormAppProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EfFormAppProject.Data
{
    public class ObsDbContext : DbContext
    {
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>()
                .HasKey(l => l.LessonId);
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.StudentLessons)
                .WithOne(sl => sl.Lesson)
                .HasForeignKey(sl => sl.LessonId);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Classroom)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId);
            modelBuilder.Entity<Student>()
                .HasMany(s => s.studentLessons)
                .WithOne(sl => sl.Student)
                .HasForeignKey(sl => sl.StudentId);
            
            modelBuilder.Entity<Classroom>()
                .HasKey(c => c.ClassId);
            modelBuilder.Entity<Classroom>()
                .HasMany (c => c.Students)
                .WithOne (s => s.Classroom)
                .HasForeignKey (s => s.ClassId);

            modelBuilder.Entity<StudentLesson>()
                .HasKey(sl => new {sl.StudentId, sl.LessonId});
            modelBuilder.Entity<StudentLesson>()
                .HasOne(sl => sl.Student)
                .WithMany(s => s.studentLessons)
                .HasForeignKey(sl => sl.StudentId);
            modelBuilder.Entity<StudentLesson>()
                .HasOne (sl => sl.Lesson)
                .WithMany(l =>l.StudentLessons)
                .HasForeignKey(sl => sl.LessonId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("OBS_CONNECTION_STRING")
                ?? "Server=localhost;Database=EfFormProject;Integrated Security=true;TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
