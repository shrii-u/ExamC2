

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

                _db.Courses.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
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

        //POST action method for Deleting the Product
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoursePOST(int? id)
        {
            var obj = _db.Courses.FirstOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }



            _db.Courses.Remove(obj);

            _db.SaveChanges();

            TempData["success"] = "course Deleted successfully";


            return RedirectToAction("Index");

        }


    }
}
