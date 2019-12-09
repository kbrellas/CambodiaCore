using System;
using System.Collections.Generic;
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
            return View(await _context.TimesheetEntries.ToListAsync());
        }

        // GET: TimesheetEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheetEntry = await _context.TimesheetEntries
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Project,HoursWorked")] TimesheetEntryViewModel timesheetEntry)
        {
            
            var actualProject = _context.Projects.Find(timesheetEntry.RelatedProject+1);
            
            if (ModelState.IsValid && actualProject!=null)
            {
                MyUser user = await UserManager.GetUserAsync(HttpContext.User);
                
                Console.WriteLine(user);
                Console.WriteLine(actualProject);
                Console.WriteLine(timesheetEntry.HoursWorked);
                TimesheetEntry actualTimesheetEntry = new TimesheetEntry() {
                    RelatedProject = actualProject,
                    RelatedUser = user,
                    DateCreated = DateTime.Now,
                    HoursWorked = timesheetEntry.HoursWorked
                };
                _context.Add(actualTimesheetEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
            return View(timesheetEntry);
        }

        // POST: TimesheetEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateCreated,HoursWorked")] TimesheetEntry timesheetEntry)
        {
            if (id != timesheetEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timesheetEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimesheetEntryExists(timesheetEntry.Id))
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

            var timesheetEntry = await _context.TimesheetEntries
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
    }
}
