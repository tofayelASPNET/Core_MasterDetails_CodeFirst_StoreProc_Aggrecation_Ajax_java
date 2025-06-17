using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ex_1285453.Models.ViewModels
{
    public class EmployeeVM
    {

        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; } = default!;
        public decimal Salary { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required, StringLength(50)]
        public string Phone { get; set; } = default!;
        public IFormFile? ImageFile { get; set; }
        public string? Image { get; set; } = default!;
        public bool Fresher { get; set; }
        public List<int> SkillList { get; set; } = new List<int>();

    }
}
