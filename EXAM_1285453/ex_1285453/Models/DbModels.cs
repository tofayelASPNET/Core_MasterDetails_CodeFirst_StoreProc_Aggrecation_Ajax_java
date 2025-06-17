using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ex_1285453.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        [Required, StringLength(50), Display(Name = "Skill Name")]
        public string SkillName { get; set; } = default!;
        //nev
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();
    }
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name = "Candidate Name")]
        public string EmployeeName { get; set; } = default!;
        public decimal Salary { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required, StringLength(50)]
        public string Phone { get; set; } = default!;
        public string Image { get; set; } = default!;
        public bool Fresher { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();
    }
    public class EmployeeSkill
    {
        public int EmployeeSkillId { get; set; }
        public int SkillId { get; set; }
        public int EmployeeId { get; set; }
        //nev
        public virtual Skill Skill { get; set; } = default!;
        public virtual Employee Employee { get; set; } = default!;
    }
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
        {

        }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        public void InsertSkill(Skill sk)
        {
            SqlParameter skName = new SqlParameter("@skName", sk.SkillName);
            this.Database.ExecuteSqlRaw("EXEC spInsertSkill @skName", skName);
        }

    }
}
