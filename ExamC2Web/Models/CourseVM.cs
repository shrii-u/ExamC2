using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamC2Web.Models
{
    public class CourseVM
    {
        
        
        public Course course { get; set; }
       
     
        [ValidateNever]
        public IEnumerable<SelectListItem> CourseList { get; set; }
    }
}
