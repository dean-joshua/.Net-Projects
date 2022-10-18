using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StudentAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set;}

        [Required]
        [MaxLength(100)]
        public string? Major { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }

    class StudentDb : DbContext
    {
        public StudentDb(DbContextOptions options) : base(options) { }
        public DbSet<Student> Students { get; set; } = null!;
    }
}