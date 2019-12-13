using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data;
using Timesheets.Models;
using Timesheets.Security;

namespace Timesheets.Controllers
{
    public class TimesheetEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<MyUser> UserManager;
        private readonly IAuthorizationService _authorizationService;

        public TimesheetEntriesController(ApplicationDbContext context,UserManager<MyUser> userManager
            , IAuthorizationService authorizationService)
        {
            _context = context;
            UserManager = userManager;
            _authorizationService = authorizationService;
        }

        // GET: TimesheetEntries
        [Authorize(Roles = "Admin , Manager , Employee")]
        public async Task<IActionResult> Index()
        {
            //var usrs = UserManager.Users.ToList();
            //var useer = _context.Users.ToList();
            MyUser user = await UserManager.GetUserAsync(HttpContext.User);
            
            MyUser betterUser =  _context.Users.Include(d=>d.Department).FirstOrDefault(u=>u.Id==user.Id);
            _context.Entry(betterUser).Reference("Department").Load();
            // var alluser = await UserManager.GetUsers
            var allTimesheets = await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).Include(d=>d.RelatedUser.Department).ToListAsync();
           
            if (User.IsInRole("Admin"))
            {       

                return View(allTimesheets);
            }
            else if (User.IsInRole("Manager"))
            {
                 var certainTimesheets = from timesheet in allTimesheets
                                        where timesheet.RelatedUser.Department!=null &&
                                        timesheet.RelatedUser.Department.Id == user.Department.Id
                                        select timesheet;
                return View(certainTimesheets);
            }
            else if (User.IsInRole("Employee")) {

                  var certainTimesheets = from timesheet in allTimesheets
                                        where timesheet.RelatedUser.Id == user.Id
                                        select timesheet;
                return View(certainTimesheets);
            }
            else return View(nameof(Error), "Please log in");
        }

        // GET: TimesheetEntries/Details/5
        [Authorize(Roles = "Admin,Manager,Employee")]
      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetEntry = await _context.TimesheetEntries.Include(p=>p.RelatedProject).Include(u=>u.RelatedUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timesheetEntry == null)
            {
                return NotFound();
            }
            var result=await _authorizationService.AuthorizeAsync(User, timesheetEntry, Operations.Read);
            if (result.Succeeded){
                return View(timesheetEntry);
            }
            else{
                return View(nameof(Error), "Access Denied");
            }
            
            

            
        }

        // GET: TimesheetEntries/Create
        [Authorize(Roles = "Admin,Manager,Employee")]
        public IActionResult Create()
        {
            var projects = new List<SelectListItem>();
            foreach (Project project in _context.Projects.ToList()) {
                projects.Add(new SelectListItem() { Value = project.Id.ToString(), Text = project.Name });
                
            }
            ViewBag.Message = projects;
            
            return View();
        }

        // POST: TimesheetEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Create( TimesheetEntryViewModel timesheetEntry)
        {
            
            var actualProject = _context.Projects.Find(timesheetEntry.RelatedProject);
            MyUser user = await UserManager.GetUserAsync(HttpContext.User);
            var allTimesheets=await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).ToListAsync();
            


            if (ModelState.IsValid && actualProject!=null && user!=null)
            {
                var possibleTimesheet = from tim in allTimesheets
                                        where tim.RelatedProject.Id == actualProject.Id &&
                                        tim.RelatedUser.Id == user.Id &&
                                        tim.DateCreated.Date == timesheetEntry.DateCreated.Date
                                        select tim;
                if (possibleTimesheet.Count() == 0)
                {

                    TimesheetEntry actualTimesheetEntry = new TimesheetEntry()
                    {
                        RelatedProject = actualProject,
                        RelatedUser = user,
                        DateCreated = timesheetEntry.DateCreated,
                        HoursWorked = timesheetEntry.HoursWorked
                    };
                    
                        _context.Add(actualTimesheetEntry);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    
                }
                else {
                    var ErrorMsg = "Timesheet for Project: " + actualProject.Name +
                        " with Date: " + timesheetEntry.DateCreated.Date + " already exists";
                    return View(nameof(Error),ErrorMsg); 
                }
            }
            return View(timesheetEntry);
        }

        // GET: TimesheetEntries/Edit/5
       

        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetEntry = await _context.TimesheetEntries.FindAsync(id);
            if (timesheetEntry == null)
            {
                return NotFound();
            }
            var projects = new List<SelectListItem>();
            foreach (Project project in _context.Projects.ToList())
            {
                projects.Add(new SelectListItem() { Value = project.Id.ToString(), Text = project.Name });

            }
            ViewBag.Message = projects;
            TimesheetEntryViewModel timesheetViewModel = new TimesheetEntryViewModel()
            {
                RelatedProject = timesheetEntry.RelatedProject.Id,
                DateCreated = timesheetEntry.DateCreated,
                HoursWorked=timesheetEntry.HoursWorked
            };

            var result = await _authorizationService.AuthorizeAsync(User, timesheetEntry, Operations.Update);
            if (result.Succeeded)
            {
                return View(timesheetViewModel);
            }
            else {
                return View(nameof(Error), "Access Denied");
            }
        }

        // POST: TimesheetEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id,  TimesheetEntryViewModel timesheetEntry)
        {
            var actualProject = _context.Projects.Find(timesheetEntry.RelatedProject);
            var allTimesheets = await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).ToListAsync();


            if (ModelState.IsValid)
            {
                try
                {
                    TimesheetEntry actualTimesheet = _context.TimesheetEntries.Find(id);
                    
                   
                        actualTimesheet.RelatedProject = actualProject;
                        actualTimesheet.HoursWorked = timesheetEntry.HoursWorked;
                        actualTimesheet.DateCreated = timesheetEntry.DateCreated;
                    var result = await _authorizationService.AuthorizeAsync(User, actualTimesheet, Operations.Update);
                    if (result.Succeeded)
                    {
                        _context.Update(actualTimesheet);
                        await _context.SaveChangesAsync();
                    }
                    else{
                        return View(nameof(Error), "Access Denied");
                    }
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimesheetEntryExists(id))
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
            return View(timesheetEntry);
        }

        // GET: TimesheetEntries/Delete/5
        [Authorize(Roles = "Admin,Manager")]
       

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetEntry = await _context.TimesheetEntries.Include(p=>p.RelatedProject).Include(u=>u.RelatedUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timesheetEntry == null)
            {
                return NotFound();
            }
            var result = await _authorizationService.AuthorizeAsync(User, timesheetEntry, Operations.Delete);
            if (result.Succeeded)
            {
                return View(timesheetEntry);
            }
            else {
                return View(nameof(Error), "Access Denied");
            }
        }

        // POST: TimesheetEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
       

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timesheetEntry = await _context.TimesheetEntries.FindAsync(id);
            var result = await _authorizationService.AuthorizeAsync(User, timesheetEntry, Operations.Delete);
            if (result.Succeeded)
            {
                _context.TimesheetEntries.Remove(timesheetEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else {
                return View(nameof(Error), "AccessDenied");
            }
        }

        private bool TimesheetEntryExists(int id)
        {
            return _context.TimesheetEntries.Any(e => e.Id == id);
        }

        public  IActionResult Error(string ErrorMsg) {
            return View(ErrorMsg);
        }
    }
}
