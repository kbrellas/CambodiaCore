using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data;
using Timesheets.Models;

namespace Timesheets.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        
       
        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            var departments = new List<SelectListItem>();
            foreach(Department department in _context.Departments.ToList())
            {
                departments.Add(new SelectListItem() { Value=department.Id.ToString(), Text = department.Name});
            }
            ViewBag.Message = departments;
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OwnerDept,Departments")] ProjectViewModel project)
        {
            var actualOwnerDept = _context.Departments.Find(project.OwnerDept);
            ICollection < Department > actualDepartments = new List<Department>();
            ICollection<DepartmentProject> departmentProjects = new List<DepartmentProject>();
            foreach (int i in project.Departments)
            {
                actualDepartments.Add(_context.Departments.Find(i));
            }
            Project actualProject = new Project()
            {
                Name = project.Name,
                OwnerDept = actualOwnerDept,
                Departments = departmentProjects
            };
            foreach (Department department in actualDepartments)
            {
                var departmentProject = new DepartmentProject() 
                { 
                    Department = department, 
                    //DepartmentId = department.Id, 
                    Project = actualProject, 
                    //ProjectId = actualProject.Id 
                };
                actualProject.Departments.Add(departmentProject);

            }

            if (ModelState.IsValid)
            {
                //MyUser user = await UserManager.GetUserAsync(HttpContext.User);

                Console.WriteLine(project.Name);
                Console.WriteLine(actualOwnerDept);
                Console.WriteLine(actualDepartments);

                //_context.AddRange(departmentProjects);
                _context.Add(actualProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            var departments = new List<SelectListItem>();
            foreach (Department department in _context.Departments.ToList())
            {
                departments.Add(new SelectListItem() { Value = department.Id.ToString(), Text = department.Name });
            }
            ViewBag.Message = departments;
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OwnerDept,Departments")] ProjectEditModelView project)
        {
            var actualOwnerDept = _context.Departments.Find(project.OwnerDept);
            ICollection<Department> actualDepartments = new List<Department>();
            ICollection<DepartmentProject> departmentProjects = new List<DepartmentProject>();
            foreach (int i in project.Departments)
            {
                actualDepartments.Add(_context.Departments.Find(i));
            }
            Project actualProject = new Project()
            {
                Id = id,
                Name = project.Name,
                OwnerDept = actualOwnerDept,
                Departments = departmentProjects
            };
            foreach (Department department in actualDepartments)
            {
                var departmentProject = new DepartmentProject()
                {
                    Department = department,
                    Project = actualProject, 
                };
                actualProject.Departments.Add(departmentProject);

            }
            if (id != actualProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //return View(project);

                    _context.Update(actualProject);
                    await _context.SaveChangesAsync();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(actualProject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(actualProject);

        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            var relatedTimesheets = await _context.TimesheetEntries.AnyAsync(d => d.RelatedProject.Id == id);
            if (relatedTimesheets)
            {
                return RedirectToAction(nameof(TimesheetFullProject));
            }
            return View(project);
        }
        public async Task<IActionResult> TimesheetFullProject(int id)
        {
            return View(await _context.Projects.FirstOrDefaultAsync(m => m.Id == id));
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
