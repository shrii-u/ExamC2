

using ExamC2Web.Data;
using ExamC2Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Diagnostics.Metrics;
using System.Security.Claims;


namespace ExamC2Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CourseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            IEnumerable<Course> course = _db.Courses.ToList();
            return View(course);
        }



        [HttpGet]
        public IActionResult AddCourse()
        {


            return View(); // Make sure to pass the StudentVm object to the view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(Course obj)
        {
            if (ModelState.IsValid)
            {
                // Check if the course name already exists
                if (_db.Courses.Any(c => c.Name == obj.Name))
                {
                    TempData["error"] = "Course already exist ";
                    return RedirectToAction("Index","Course");
                }

                _db.Courses.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

            //{
            //    if (ModelState.IsValid)
            //    {

            //        _db.Courses.Add(obj);
            //        _db.SaveChanges();
            //        TempData["success"] = "Product created successfully";
            //        return RedirectToAction("Index");
            //    }
            //    return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var courseFromDb = _db.Courses.FirstOrDefault(x => x.Id == id);


            if (courseFromDb == null)
            {
                return NotFound();
            }

            return View(courseFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course obj)
        {
            if (ModelState.IsValid)
            {

                _db.Courses.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Course created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult DeleteCourse(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var courseFromDb = _db.Courses.FirstOrDefault(x => x.Id == id);


            if (courseFromDb == null)
            {
                return NotFound();
            }

            return View(courseFromDb);
        }
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoursePOST(int? id)
        {
            var course = _db.Courses.FirstOrDefault(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            // Remove all records in the StudentsCourse table that refer to the deleted course
            var studentsCourses = _db.StudentsCourse.Where(sc => sc.CourseId == id);
            _db.StudentsCourse.RemoveRange(studentsCourses);

            // Remove the course from the Courses table
            _db.Courses.Remove(course);
            _db.SaveChanges();
            var students = _db.Students.ToList();
            foreach (var student in students)
            {
                var studentCourses = _db.StudentsCourse.Where(sc => sc.StudentId == student.StudentId);
                var totalCoursePrice = studentCourses.Select(sc => sc.Course.price).Sum();
                student.CourseTotalPrice = totalCoursePrice;
                _db.Students.Update(student);
            }
            _db.SaveChanges();

            TempData["success"] = "Course deleted successfully.";
            return RedirectToAction("Index");
        }


        //POST action method for Deleting the Product
        //[HttpPost, ActionName("DeleteCourse")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteCoursePOST(int? id)
        //{
        //    var obj = _db.Courses.FirstOrDefault(x => x.Id == id);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }



        //    _db.Courses.Remove(obj);

        //    _db.SaveChanges();

        //    TempData["success"] = "course Deleted successfully";


        //    return RedirectToAction("Index");

        //}


    }
}
