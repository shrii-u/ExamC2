using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ExamC2Web.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Please Enter Username")]
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Please Enter Password")]
        public string AdminPasswords
        {
            get; set;
        }
    }
}
