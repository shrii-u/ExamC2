namespace ExamC2Web.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }
}
