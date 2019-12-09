using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data;
using Timesheets.Models;

namespace Timesheets.Controllers
{
    public class TimesheetEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<MyUser> UserManager;

        public TimesheetEntriesController(ApplicationDbContext context,UserManager<MyUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: TimesheetEntries
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.TimesheetEntries.Include( p => p.RelatedProject).Include(u=>u.RelatedUser).ToListAsync());
        }

        // GET: TimesheetEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetEntry = await _context.TimesheetEntries.Include(p=>p.RelatedProject).Include(u=>u.RelatedUser)
                .FirstOrDefaultAsync(m => m.Id == id);
           // var tims = await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).ToListAsync();
          //  var actual = from tim in tims where tim.RelatedProject.Id == 1 select tim;
            if (timesheetEntry == null)
            {
                return NotFound();
            }
            

            return View(timesheetEntry);
        }

        // GET: TimesheetEntries/Create
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


            return View(timesheetViewModel);
        }

        // POST: TimesheetEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  TimesheetEntryViewModel timesheetEntry)
        {
            var actualProject = _context.Projects.Find(timesheetEntry.RelatedProject);
            var allTimesheets = await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).ToListAsync();


            if (ModelState.IsValid)
            {
                try
                {
                    TimesheetEntry actualTimesheet = _context.TimesheetEntries.Find(id);
                    var possibleTimesheet = from tim in allTimesheets
                                            where tim.RelatedProject.Id == actualProject.Id &&
                                            tim.RelatedUser.Id == actualTimesheet.RelatedUser.Id &&
                                            tim.DateCreated.Date == timesheetEntry.DateCreated.Date
                                            select tim;
                    if (possibleTimesheet.Count() == 0)
                    {
                        actualTimesheet.RelatedProject = actualProject;
                        actualTimesheet.HoursWorked = timesheetEntry.HoursWorked;
                        actualTimesheet.DateCreated = timesheetEntry.DateCreated;
                        _context.Update(actualTimesheet);
                        await _context.SaveChangesAsync();
                    }
                    else {
                        var ErrorMsg = "Timesheet for Project: " + actualProject.Name +
                        " with Date: " + timesheetEntry.DateCreated.Date + " already exists";
                        return View(nameof(Error), ErrorMsg);
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

            return View(timesheetEntry);
        }

        // POST: TimesheetEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timesheetEntry = await _context.TimesheetEntries.FindAsync(id);
            _context.TimesheetEntries.Remove(timesheetEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
