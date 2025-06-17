using ex_1285453.Models;
using ex_1285453.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ex_1285453.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly CandidateDbContext _context;
        private readonly IWebHostEnvironment _he;
        public CandidatesController(CandidateDbContext _context, IWebHostEnvironment _he)
        {
            this._context = _context;
            this._he = _he;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.Include(x => x.EmployeeSkills).ThenInclude(y => y.Skill).ToListAsync());
            
        }

        public IActionResult Aggregation()
        {
            var emp = _context.Employees.ToList();
            var db = _context.Employees;
            ViewBag.Count = db.Count();
            ViewBag.Max = db.Max(e => e.Salary);
            ViewBag.Min = db.Min(e => e.Salary);
            ViewBag.Sum = db.Sum(e => e.Salary);
            ViewBag.Avg = db.Average(e => e.Salary);

            return View(emp);
        }
        public IActionResult AddNewSkills(int? id)
        {
            ViewBag.skills = new SelectList(_context.Skills, "SkillId", "SkillName", id.ToString() ?? "");
            return PartialView("_AddNewSkills");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeeVM, int[] skillId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeName = employeeVM.EmployeeName,
                    Salary = employeeVM.Salary,
                    DateOfBirth = employeeVM.DateOfBirth,
                    Phone = employeeVM.Phone,
                    Fresher = employeeVM.Fresher
                };

                //file
                var file = employeeVM.ImageFile;
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = Path.GetFileName(employeeVM.ImageFile.FileName);
                string fileSave = Path.Combine(webroot, folder, imgFileName);

                if (file != null)
                {
                    using (var stream = new FileStream(fileSave, FileMode.Create))
                    {
                        employeeVM.ImageFile.CopyTo(stream);
                        employee.Image = "/" + folder + "/" + imgFileName;
                    }
                }


                //for skill
                foreach (var item in skillId)
                {
                    EmployeeSkill employeeSkill = new EmployeeSkill()
                    {
                        Employee = employee,
                        EmployeeId = employee.EmployeeId,
                        SkillId = item
                    };
                    _context.EmployeeSkills.Add(employeeSkill);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            EmployeeVM employeeVM = new EmployeeVM()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Phone = employee.Phone,
                Image = employee.Image,
                Fresher = employee.Fresher
            };

            //skill
            var existSkill = _context.EmployeeSkills.Where(x => x.EmployeeId == id).ToList();
            foreach (var item in existSkill)
            {
                employeeVM.SkillList.Add(item.SkillId);
            }
            return View(employeeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeVM employeeVM, int[] skillId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeId = employeeVM.EmployeeId,
                    EmployeeName = employeeVM.EmployeeName,
                    Salary = employeeVM.Salary,
                    DateOfBirth = employeeVM.DateOfBirth,
                    Phone = employeeVM.Phone,
                    Fresher = employeeVM.Fresher
                };

                var file = employeeVM.ImageFile;
                var oldPic = employeeVM.Image;
                if (file != null)
                {
                    string webroot = _he.WebRootPath;
                    string folder = "Images";
                    string imgFileName = Path.GetFileName(employeeVM.ImageFile.FileName);
                    string fileSave = Path.Combine(webroot, folder, imgFileName);

                    using (var stream = new FileStream(fileSave, FileMode.Create))
                    {
                        employeeVM.ImageFile.CopyTo(stream);
                        employee.Image = "/" + folder + "/" + imgFileName;
                    }
                }
                else
                {
                    employee.Image = oldPic;
                }



                //skill
                var existSkill = _context.EmployeeSkills.Where(x => x.EmployeeId == employee.EmployeeId).ToList();
                foreach (var item in existSkill)
                {
                    _context.EmployeeSkills.Remove(item);
                }

                //add
                foreach (var item in skillId)
                {
                    EmployeeSkill employeeSkill = new EmployeeSkill()
                    {
                        EmployeeId = employee.EmployeeId,
                        SkillId = item
                    };
                    _context.EmployeeSkills.Add(employeeSkill);
                }
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var candidate = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            var existSkill = _context.EmployeeSkills.Where(x => x.EmployeeId == id).ToList();
            foreach (var item in existSkill)
            {
                _context.EmployeeSkills.Remove(item);
            }
            _context.Remove(candidate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
