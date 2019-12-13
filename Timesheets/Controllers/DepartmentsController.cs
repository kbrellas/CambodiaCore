using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Timesheets.Data;
using Timesheets.Models;


namespace Timesheets.Controllers
{
    public class DepartmentsController : Controller
    {


        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<MyUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context, UserManager<MyUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        // GET: Departments
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Departments
                .Include(d => d.DepartmentHead)
                .Include(d => d.Projects)
                .Include(d => d.RelatedUsers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Departments/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var department = await _context.Departments
                .Include(d => d.DepartmentHead).Include(d=>d.Projects).Include(d => d.RelatedUsers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            var projects = await _context.Projects.Where(p =>p.OwnerDept == department).ToListAsync();

            DepartmentDetail departmentDetail = new DepartmentDetail();
            departmentDetail.department = department;
            departmentDetail.projects = projects;

            return View(departmentDetail);
        }

        // GET: Departments/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
           // ViewData["DepartmentHeadId"] = new SelectList(_context.Users, "Id", "Id");
            var managers = await _userManager.GetUsersInRoleAsync("Manager");

            var managersList = new List<SelectListItem>();
            foreach (MyUser u in managers.ToList())
            {
                managersList.Add(new SelectListItem() { Value = u.Id, Text = u.FirstName.ToString() + " " + u.LastName.ToString() });
            }
            ViewBag.managers = managersList;
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,DepartmentHeadId")] Department department)
        {

            if (ModelState.IsValid)
            {
                _context.Add(department);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException e) {
                ViewBag.message = "Department Head already assigned to a Department";
                    ViewBag.title = "Error Creating Department";
                    ViewBag.alertClass = "alert alert-danger";
                    return View("~/Views/Departments/Alerts.cshtml"); }
                return RedirectToAction(nameof(Index));
            }


            ViewData["DepartmentHeadId"] = new SelectList(_context.Users, "Id", "Id", department.DepartmentHeadId);
            return View(department);


        }

        // GET: Departments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var managers = await _userManager.GetUsersInRoleAsync("Manager");
      
            var managersList = new List<SelectListItem>();
            foreach (MyUser u in managers.ToList())
            {
                managersList.Add(new SelectListItem() { Value = u.Id, Text = u.FirstName.ToString() + " " + u.LastName.ToString() });
            }
            ViewBag.managers=managersList;
      
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DepartmentHeadId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                    catch (DbUpdateException)
                    {
                    department.DepartmentHead =await _userManager.FindByIdAsync(department.DepartmentHeadId);
                    ViewBag.error = true;
                    ViewBag.message = department.DepartmentHead.FirstName+" "+department.DepartmentHead.LastName+" already assigned to a Department";
                    ViewBag.title = "Error Editing Department";
                    ViewBag.alertClass = "alert alert-danger";
                    var managers = await _userManager.GetUsersInRoleAsync("Manager");
                    var managersList = new List<SelectListItem>();
                    foreach (MyUser u in managers.ToList())
                    {
                        managersList.Add(new SelectListItem() { Value = u.Id, Text = u.FirstName.ToString() + " " + u.LastName.ToString() });
                    }
                    ViewBag.managers = managersList;
                    return View();
                    }
                return RedirectToAction(nameof(Index));

            }
         
            return View(department);
        }

        // GET: Departments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.DepartmentHead)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }
          
       
            

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          // var department = await _context.Departments.FindAsync(id);
           var department = await _context.Departments.Include(d => d.Projects).Include(d => d.RelatedUsers).FirstOrDefaultAsync(d=> d.Id==id);
            if (department.Projects.Count > 0)
            {
                ViewBag.title = "Error Deleting Department";
                ViewBag.alertClass = "alert alert-danger";
                ViewBag.message = "You cannot delete this Department because it Owns Projects";
                ViewBag.back= "index";
                return View("~/Views/Departments/Alerts.cshtml");
            }
            if (department.RelatedUsers.Count > 0)
            {
                ViewBag.title = "Error Deleting Department";
                ViewBag.alertClass = "alert alert-danger";
                ViewBag.message = "You cannot delete this Department because it has related employees";
                ViewBag.back = "index";
                return View("~/Views/Departments/Alerts.cshtml");
            }



            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            ViewBag.title = "Department Deleted";
            ViewBag.alertClass = "alert alert-success";
            ViewBag.message = "Department was deleted succsfully";
            ViewBag.back = "index";
            return View("~/Views/Departments/Alerts.cshtml");
          //  return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }

    
     
    }


   








}
