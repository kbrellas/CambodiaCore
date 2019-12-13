using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }



        // GET: Projects/Details/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.
                Include(c => c.Departments).FirstOrDefaultAsync(d => d.Id == id);
            List<string> depnames = new List<string>();

            if (project.Departments == null || project.Departments.Count==0)
            {
                depnames.Add("No Contributing Departments");
            }
            else
            {
                var actualDepartments = new List<Department>();
                foreach (DepartmentProject dep in project.Departments)
                {
                    actualDepartments.Add(await _context.Departments.FindAsync(dep.DepartmentId));
                }
                foreach(Department dep in actualDepartments)
                {
                    if (dep.Name == null)
                        depnames.Add("Department with no Name and ID: " + dep.Id);
                    else
                        depnames.Add(dep.Name);
                }
            }
            if(project.OwnerDept == null || project.OwnerDept.Name == null)
            {
                ViewBag.HeadDepartment = "Not yet set";
            }
            else
            {
                ViewBag.HeadDepartment = project.OwnerDept.Name;
            }
            
            ViewBag.ContributingDepartments = depnames;
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
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
                    Project = actualProject, 
                };
                actualProject.Departments.Add(departmentProject);

            }

            if (ModelState.IsValid)
            {
             
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
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project =  _context.Projects.Include(d=>d.OwnerDept).Include(d=>d.Departments).FirstOrDefault(i=>i.Id==id);
            if (project == null)
            {
                return NotFound();
            }
            var departments = new List<SelectListItem>();
            var departmentsId = new List<int>();
            foreach (Department department in (await _context.Departments.ToListAsync()))
            {
                departments.Add(new SelectListItem() { Value = department.Id.ToString(), Text = department.Name });
                
            }
            foreach(DepartmentProject dept in project.Departments) {
                departmentsId.Add(dept.DepartmentId);
            }
            if(project.OwnerDept == null)
            {
                ViewBag.InitialDepartmentValue = 0;
                ViewBag.InitialDepartmentName = departments.ElementAt(0).Text;
            }
            else
            {
                foreach (SelectListItem selectListItem in departments)
                {
                    if (selectListItem.Text.Equals(project.OwnerDept.Name))
                    {
                        ViewBag.InitialDepartmentValue = selectListItem.Value;
                        ViewBag.InitialDepartmentName = selectListItem.Text;
                    }
                        
                }
            }
            ProjectViewModel projectViewModel;
            if (project.OwnerDept != null && project.Departments != null)
            {
                projectViewModel = new ProjectViewModel()
                {
                    Name = project.Name,
                    OwnerDept = project.OwnerDept.Id,
                    Departments = departmentsId
                };
            }
            else if (project.Departments != null) {
                projectViewModel = new ProjectViewModel()
                {
                    Name = project.Name,
                    
                    Departments = departmentsId
                };

            }
            else 
            {
                projectViewModel = new ProjectViewModel()
                {
                    Name = project.Name,
                    OwnerDept = project.OwnerDept.Id

                };
            }
            
                       
            ViewBag.departments = departments;
            return View(projectViewModel);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OwnerDept,Departments")] ProjectViewModel project)
        {


            var actualProject = await _context.Projects.Include(c => c.Departments).FirstOrDefaultAsync(d => d.Id == id);

            if (id != actualProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                


                if (project.Name == null)
                {
                    actualProject.Name = project.Name;
                    _context.Update(actualProject);
                }

                var actualOwnerDept = await _context.Departments.FindAsync(project.OwnerDept);
                if (actualOwnerDept != null)
                {
                    actualProject.OwnerDept = actualOwnerDept;
                    _context.Update(actualProject);
                }

                if (project.Departments != null)
                {
                    ICollection<DepartmentProject> fetchedDepartmentProjects = actualProject.Departments;
                    foreach (DepartmentProject dp in fetchedDepartmentProjects)
                    {
                        _context.DepartmentProjects.Remove(dp);
                    }

                    ICollection<Department> actualDepartments = new List<Department>();
                    foreach (int i in project.Departments)
                    {
                        actualDepartments.Add(_context.Departments.Find(i));
                    }

                    ICollection<DepartmentProject> departmentProjects = new List<DepartmentProject>();
                    foreach (Department department in actualDepartments)
                    {
                        var departmentProject = new DepartmentProject()
                        {
                            Department = department,
                            Project = actualProject,
                        };
                        actualProject.Departments.Add(departmentProject);
                    }
                }
                try
                {
                    //return View(project);
                    _context.Update(actualProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
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
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
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
