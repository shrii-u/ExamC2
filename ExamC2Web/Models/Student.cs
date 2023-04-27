using System.ComponentModel.DataAnnotations;

namespace ExamC2Web.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string? StudentName { get; set; }
        [Required]
        public int RollNo { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        public double CourseTotalPrice { get; set; }
    }
}
