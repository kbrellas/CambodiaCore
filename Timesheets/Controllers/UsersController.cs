using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data;
using Timesheets.Models;

namespace Timesheets.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<MyUser> UserManager;

        public UsersController(ApplicationDbContext context, UserManager<MyUser> userManager) 
        {
            _context = context;
            UserManager = userManager;
        }
        // GET: Users
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Index()
        {
            var users = _context.Users.Include(d=>d.Department).ToList();
            return  View(users);
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();
            
            var user =  _context.Users.Include(u=>u.Manager).Include(d=>d.Department).FirstOrDefault(u=>u.Id==id);
            var us =await UserManager.GetRolesAsync(user);

            if (user == null)
                return NotFound();
            ViewBag.Roles = us;
            return View(user);
        }

        // GET: Users/Create


        // GET: Users/Edit/5
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Edit(String id)
        { 
        if (id == null)
            {
                return NotFound();
            }
            var user = _context.Users.Include(d => d.Department).FirstOrDefault(i => i.Id == id);
            var roles =  UserManager.GetRolesAsync(user).Result;
            
        
            var roleList = new List<SelectListItem>();
            foreach(var role in roles)
            {
                roleList.Add(new SelectListItem() { Value = role, Text = role });
            }
            var depts = _context.Departments.ToList();
            var departments = new List<SelectListItem>();
            foreach (Department dept in depts)
            {
                departments.Add(new SelectListItem() { Value = dept.Id.ToString(), Text = dept.Name });
            }
            var allRoles = _context.Roles.ToList();
            var allRolesList = new List<SelectListItem>();
            foreach(var role in allRoles)
            {
                allRolesList.Add(new SelectListItem() { Value=role.Name , Text=role.Name});
            }
            ViewBag.AllRoles = allRolesList;
            ViewBag.Depts = departments;
            ViewBag.SelectedRoles=roleList;
            MyUserViewModel userViewModel;
            if (user.Department != null)
            {
                 userViewModel = new MyUserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CostPerHour = user.CostPerHour,
                    Role = roles,
                    DepartmentId = user.Department.Id

                };
            }
            else {
                 userViewModel = new MyUserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CostPerHour = user.CostPerHour,
                    Role = roles
                   

                };
            }
      
            return View(userViewModel);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Edit(string id, MyUserViewModel userViewModel)
        {
            var actualUser= _context.Users.Include(d => d.Department).FirstOrDefault(i => i.Id == id);
            var allRolelist = new List<string>() { "Manager", "Employee", "Admin" };
            await UserManager.RemoveFromRolesAsync(actualUser,allRolelist);
            try
            {
                if (userViewModel.Role.Contains("Manager") || userViewModel.Role.Contains("Admin"))
                {
                    actualUser.FirstName = userViewModel.FirstName;
                    actualUser.LastName = userViewModel.LastName;
                    actualUser.Email = userViewModel.Email;
                    actualUser.CostPerHour = userViewModel.CostPerHour;
                    actualUser.Department = await _context.Departments.FindAsync(userViewModel.DepartmentId);
                    
                    await UserManager.AddToRolesAsync(actualUser, userViewModel.Role);
                    await UserManager.UpdateAsync(actualUser);
                }
                else {
                    actualUser.FirstName = userViewModel.FirstName;
                    actualUser.LastName = userViewModel.LastName;
                    actualUser.Email = userViewModel.Email;
                    actualUser.CostPerHour = userViewModel.CostPerHour;
                    actualUser.Department = await _context.Departments.FindAsync(userViewModel.DepartmentId);
                    actualUser.Manager = await _context.Users.FindAsync(actualUser.Department.DepartmentHeadId);
                    await UserManager.AddToRolesAsync(actualUser, userViewModel.Role);
                    await UserManager.UpdateAsync(actualUser);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(d => d.Department).Include(m=>m.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var roles =await  UserManager.GetRolesAsync(user);
            ViewBag.Roles = roles;


            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin ")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                MyUser user =  _context.Users.Include(d=>d.Department).FirstOrDefault(d=>d.Id==id);
                var roles =await UserManager.GetRolesAsync(user);
                if (roles != null)
                {
                    if (roles.Contains("Admin"))
                    {
                        return View(nameof(Error), "Cannot delete Admin");
                    }
                }
                if (user.Department != null)
                {
                    var dept = _context.Departments.Find(user.Department.Id);
                    dept.RelatedUsers.Remove(user);

                    _context.SaveChanges();
                }
                var allTimesheets = _context.TimesheetEntries.Include(r=>r.RelatedUser).ToList();
                foreach(TimesheetEntry tim in allTimesheets)
                {
                    if (tim.RelatedUser.Id == id) {
                        _context.TimesheetEntries.Remove(tim);
                    }
                }
                var result=await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    return View(nameof(Error), "Could not delete user");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace.ToString());
                return View(nameof(Error),"Could not delete");
            }
        }

        public  ActionResult Error(string msg) {
            return View(msg);
        }
    }
}