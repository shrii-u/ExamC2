using ExamC2Web.Data;
using ExamC2Web.Models;
using Microsoft.AspNetCore.Mvc;
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
        //public async Task<IActionResult> Index()
        //{
        //	var studentCourses = await _db.StudentsCourse
        //		.Include(sc => sc.Student)
        //		.Include(sc => sc.Course)
        //		.ToListAsync();

        //	return View(studentCourses);
        //}
        public IActionResult Index(int? id)
        {
            ViewBag.StudentId = id;
            IEnumerable<StudentCourse> course = _db.StudentsCourse.ToList().Where(x => x.StudentId == id);
            return View(course);
        }


        //[HttpGet]
        //public IActionResult Upsert(int id, int? data)
        //{
        //    ViewBag.StudentId = id;
        //    var course = new Course();
        //    course.StudentId = id;
        //    if (data == null || data == 0)
        //    {
        //        ViewBag.oldPrice = 0;
        //        return View(course);
        //    }
        //    else
        //    {
        //        var a = _db.Courses.Find(data);
        //        ViewBag.oldPrice = a.Price;
        //        return View(a);
        //    }

        //}
        //[HttpPost]
        //public IActionResult Upsert(int? id, Course obj)
        //{
        //    //ViewBag.StudentId = obj.StudentId;
        //    //obj.StudentId = ViewBag.StudentId;
        //    var student = _db.Students.Find(obj.StudentId);
        //    if (obj.CourseId.Equals(null) || obj.CourseId == 0)
        //    {
        //        _db.Courses.Add(obj);
        //        _db.SaveChanges();
        //        if (student != null)
        //        {
        //            student.CourseTotalPrice = student.CourseTotalPrice + obj.Price;
        //            _db.Students.Update(student);
        //            _db.SaveChanges();
        //        }
        //        TempData["success"] = "Course added successfully";

        //    }
        //    else
        //    {
        //        var course = _db.Courses.Find(obj.CourseId);
        //        _db.Remove(course);
        //        _db.Courses.Update(obj);
        //        _db.SaveChanges();
        //        if (student != null && course != null)
        //        {
        //            student.CourseTotalPrice = student.CourseTotalPrice + (obj.Price - course.Price);
        //            _db.Students.Update(student);
        //            _db.SaveChanges();
        //        }
        //        TempData["success"] = "Course updated successfully";
        //    }
        //    return RedirectToAction("Index", new { id = obj.StudentId });
        //}

        //public IActionResult Delete(int? data)
        //{
        //    var course = _db.Courses.Find(data);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Courses.Remove(course);
        //    _db.SaveChanges();
        //    var student = _db.Students.Find(course.StudentId);
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
