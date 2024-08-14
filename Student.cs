using System.ComponentModel.DataAnnotations;

namespace WebAPI_Student_Callingusingcore.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? StudentName { get; set; }
        [Required]
        public string? StudentGender { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int? Class { get; set; }
        [Required]
        public string? FatherName { get; set; }
    }
}
