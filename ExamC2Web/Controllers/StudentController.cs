

using ExamC2Web.Data;
using ExamC2Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Diagnostics.Metrics;
using System.Security.Claims;


namespace ExamC2Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        #region APICALL
        public IActionResult Allstudents()
        {
            var students = _db.Students.ToList();
            return Json(new { data = students });

        }
        #endregion
        [HttpGet]
        public IActionResult Index(string searchstring)
        {
           
            IEnumerable<Student> student = _db.Students.ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                student = student.Where(s => s.StudentName.Contains(searchstring));
               

            }
            return View(student);
        }
        public IActionResult Index(IFormFile file)
        {
            var data = new List<Student>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        if (worksheet.Cells[i, 1].Value == null)
                        {
                            break;
                        }
                        data.Add(new Student
                        {
                            StudentName = worksheet.Cells[i, 1].Value.ToString().Trim(),
                            RollNo = Convert.ToInt32(worksheet.Cells[i, 2].Value),
                            Email = worksheet.Cells[i, 3].Value.ToString().Trim(),
                            Address = worksheet.Cells[i, 4].Value.ToString().Trim(),
                            State = worksheet.Cells[i, 5].Value.ToString().Trim(),
                            City = worksheet.Cells[i, 6].Value.ToString().Trim(),
                            Phone = worksheet.Cells[i, 7].Value.ToString().Trim(),
                            PostalCode = worksheet.Cells[i, 8].Value.ToString().Trim(),

                        });


                    }
                    foreach (var obj in data)
                    {
                        _db.Students.Add(obj);
                        _db.SaveChanges();
                        TempData["success"] = "Student Added successfully";
                    }
                }

            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Create()
        {


            return View(); // Make sure to pass the StudentVm object to the view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student obj)
        {
            if (ModelState.IsValid)
            {

                _db.Students.Add(obj);
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
            var StudentFromDb = _db.Students.FirstOrDefault(x => x.StudentId == id);


            if (StudentFromDb == null)
            {
                return NotFound();
            }

            return View(StudentFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student obj)
        {
            if (ModelState.IsValid)
            {

                _db.Students.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "student created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


		//POST action method for Deleting the Product

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDb = _db.Students.Find(id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		//POST action method for Deleting the Product
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Students.Find(id);
			if (obj == null)
			{
				return NotFound();
			}


			_db.Students.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Student Deleted successfully";

			return RedirectToAction("Index","Student");

		}


	}
}
