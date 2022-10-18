using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Create DB connection
var connectionString = builder.Configuration.GetConnectionString("Students") ?? "Data Source = Students.db";

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlite<StudentDb>(connectionString);
// This was for in memory cache test
//builder.Services.AddDbContext<StudentDb>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "Student API",
         Description = "A collection of students",
         Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API V1");
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/student", async (StudentDb db) => await db.Students.ToListAsync());
app.MapGet("/student/{id}", async (StudentDb db, int id) => await db.Students.FindAsync(id));
app.MapPost("/student", async (StudentDb db, Student student) => {
    await db.Students.AddAsync(student);
    await db.SaveChangesAsync();
    return Results.Created($"/student/{student.Id}", student);
});
app.MapPut("/student/{id}", async (StudentDb db, Student updateStudent, int id) => {
    var student = await db.Students.FindAsync(id);
    if (student is null) return Results.NotFound();
    student.LastName = updateStudent.LastName;
    student.FirstName = updateStudent.FirstName;
    student.Email = updateStudent.Email;
    student.Phone = updateStudent.Phone;
    student.Major = updateStudent.Major;
    student.Courses = updateStudent.Courses;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/student/{id}", async (StudentDb db, int id) => {
    var student = await db.Students.FindAsync(id);
    if (student is null) return Results.NotFound();
    db.Students.Remove(student);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();