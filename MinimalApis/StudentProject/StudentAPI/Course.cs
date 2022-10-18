using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace StudentAPI.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(7)]
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
    }
}