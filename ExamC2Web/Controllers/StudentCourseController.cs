using ExamC2Web.Data;
using ExamC2Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamC2Web.Controllers
{
	public class StudentCourseController : Controller
	{
		private readonly ApplicationDbContext _db;

		public StudentCourseController(ApplicationDbContext db)
		{
			_db = db;
		}
        
        public IActionResult Index(int? id)
        {
            ViewBag.StudentId = id;
            IEnumerable<StudentCourse> course = _db.StudentsCourse.ToList().Where(x => x.StudentId == id);
            return View(course);
        }
        public IActionResult GetCourse(int id)
        {
            var courselist = _db.StudentsCourse.Where(s => s.StudentId == id).ToList();

            return View(courselist);
        }


        [HttpGet]
        public IActionResult Add(int? id)
        {
            var student = _db.Students.Find(id);
            ViewBag.student = student.StudentId;
            CourseVM courseVM = new CourseVM()
            {
                CourseList = _db.Courses.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                course = new Course()
            };
            return View(courseVM);
        }
        [HttpPost]
        public IActionResult Add(CourseVM obj, int StudentId)
        {
            var coursename = _db.Courses.Where(x => x.Id == obj.course.Id).First();
            var student = _db.Students.Where(x => x.StudentId == StudentId).First();


            StudentCourse course = new StudentCourse()
            {
                StudentId = StudentId,
                CourseId = obj.course.Id,
                CourseName = coursename.Name
            };
            _db.StudentsCourse.Add(course);

            student.CourseTotalPrice = student.CourseTotalPrice + coursename.price;

            _db.SaveChanges();
            TempData["success"] = "Course added successfully";
            return RedirectToAction("Index","Student");
        }
        public IActionResult Delete(int? data)
        {
            var course = _db.StudentsCourse.Find(data);
            if (course == null)
            {
                return NotFound();
            }

            var coursePrice = _db.Courses.Find(course.CourseId)?.price; // retrieve the course price from the Course model
            if (coursePrice == null)
            {
                return BadRequest("Unable to retrieve course price."); // return an error message if the course price is null
            }

            _db.StudentsCourse.Remove(course);
            _db.SaveChanges();

            var student = _db.Students.Find(course.StudentId);
            if (student != null)
            {
                student.CourseTotalPrice = student.CourseTotalPrice - coursePrice.Value; // subtract the course price from the student's total price
                _db.Students.Update(student);
                _db.SaveChanges();
            }
            TempData["success"] = "Data deleted successfully";
            return RedirectToAction("Index","Student");
        }
        //, new { id = course.StudentId }

        //public IActionResult Delete(int? data)
        //{
        //    var course = _db.StudentsCourse.Find(data);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.StudentsCourse.Remove(course);
        //    _db.SaveChanges();

        //    var student = _db.Students.Find(course.StudentId);
        //    var coursePrice =_db.Courses.Find()
        //    if (student != null)
        //    {
        //        student.CourseTotalPrice = student.CourseTotalPrice - course.Price;
        //        _db.Students.Update(student);
        //        _db.SaveChanges();
        //    }
        //    TempData["success"] = "Data deleted successfully";
        //    return RedirectToAction("Index", new { id = course.StudentId });
        //}

    }
}
