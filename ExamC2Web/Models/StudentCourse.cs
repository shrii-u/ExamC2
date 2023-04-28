using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ExamC2Web.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        [ValidateNever]
        public Course Course { get; set; }  
        public string? CourseName { get; set; }
          
    }
}
