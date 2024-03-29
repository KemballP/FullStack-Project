﻿using RegistrationSystem.Data;
using RegistrationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace RegistrationSystem.Endpoints
{
    public static class StudentsEndpoints
    {
        public static void RegisterStudentsEndpoints(this WebApplication app)
        {
            _ = app.MapGet("/students", async (ApplicationDbContext db) =>
            {
                return await db.Students.ToListAsync();
            });

            _ = app.MapGet("/students/{studentId}", async (int studentId, ApplicationDbContext db) =>
            {
                Student? student = await db.Students.FindAsync(studentId);

                return student == null ? Results.NotFound() : Results.Ok(student);
            });

            _ = app.MapPut("/students/{studentId}", async (int studentId, [FromBody] StudentRequest student, ApplicationDbContext db) =>
            {
                Student? studentToUpdate = await db.Students.FindAsync(studentId);

                if (studentToUpdate == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    studentToUpdate.StudentNumber = student.StudentNumber;
                    studentToUpdate.FirstName = student.FirstName;
                    studentToUpdate.LastName = student.LastName;
                    studentToUpdate.BirthDate = student.BirthDate;

                    _ = await db.SaveChangesAsync();

                    return Results.NoContent();
                }
            });

            _ = app.MapPost("/students", async ([FromBody] StudentRequest student, ApplicationDbContext db) =>
            {
                Student studentToAdd = new()
                {
                    StudentNumber = student.StudentNumber,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    IsDeleted = false
                };

                _ = db.Students.Add(studentToAdd);

                _ = await db.SaveChangesAsync();

                return Results.Created($"/students/{studentToAdd.StudentId}", studentToAdd);
            });
            _ = app.MapDelete("/students/{studentId}", async (int studentId, ApplicationDbContext db) =>
            {
                Student? studentToRemove = await db.Students.FindAsync(studentId);

                if (studentToRemove == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    studentToRemove.IsDeleted = true;

                    _ = await db.SaveChangesAsync();

                    return Results.Ok(studentToRemove);
                }
            });
        }
    }
}
