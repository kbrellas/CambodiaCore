using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Timesheets.Data;
using Timesheets.Models;

namespace Timesheets.Controllers
{
    public struct HistoryItem 
    {
       public int time { get; set; }
       public double cost { get; set; }
    }



    [Route("Stats")]
    [ApiController]
    public class StatsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<MyUser> _usermanager;

        public StatsController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            this._context = context;
            this._usermanager = userManager;
        }

        [HttpGet("time")]
        public async Task<ActionResult> GetProjectsPerTime()
        {
            Dictionary<string, int> data = await ProjectTimeData();


            return Json(data);

        }



        [HttpGet("cost")]
        public async Task<ActionResult> GetProjectsPerCost()
        {
            Dictionary<string, double> data = await ProjectCostData();


            return Json(data);

        }

        [HttpGet("history")]
        public async Task<ActionResult> GetProjectsd()
        {

            Dictionary<string, HistoryItem> data = await ProjectHistoryData();

            return Json(data);

        }









        private async Task<Dictionary<string, int>> ProjectTimeData()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            var timesheets = await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).ToListAsync();
            foreach (TimesheetEntry t in timesheets)
            {
                if (!data.ContainsKey(t.RelatedProject.Name))
                {
                    data.Add(t.RelatedProject.Name, t.HoursWorked);
                }
                else
                {
                    data[t.RelatedProject.Name] += t.HoursWorked;
                }

            }

            return data;
        }

        private async Task<Dictionary<string, double>> ProjectCostData()
        {
            Dictionary<string, double> data = new Dictionary<string, double>();
            var timesheets = await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).ToListAsync();
            foreach (TimesheetEntry t in timesheets)
            {
                if (!data.ContainsKey(t.RelatedProject.Name))
                {
                    data.Add(t.RelatedProject.Name, t.HoursWorked * t.RelatedUser.CostPerHour);
                }
                else
                {
                    data[t.RelatedProject.Name] += t.HoursWorked * t.RelatedUser.CostPerHour;
                }

            }

            return data;
        }



        private async Task<Dictionary<string, HistoryItem>> ProjectHistoryData()

        {
            Dictionary<string, HistoryItem> data = new Dictionary<string, HistoryItem>();
            var timesheets = await _context.TimesheetEntries.Include(p => p.RelatedProject).Include(u => u.RelatedUser).ToListAsync();
            DateTime now = DateTime.Now;
            foreach (TimesheetEntry t in timesheets)
               
            {   if (t.DateCreated.Year.Equals(now.Year))
                {
                    if (!data.ContainsKey(t.DateCreated.Month.ToString()))
                    {
                        data.Add(t.DateCreated.Month.ToString(), new HistoryItem { time = t.HoursWorked, cost = t.HoursWorked * t.RelatedUser.CostPerHour });
                    }
                    else
                    {
                        HistoryItem temp = data[t.DateCreated.Month.ToString()];
                        data[t.DateCreated.Month.ToString()] = new HistoryItem { time = temp.time + t.HoursWorked, cost = temp.cost + (t.HoursWorked * t.RelatedUser.CostPerHour) };
                    }
                }
            }

        
            

            return data;
        }




    } 
}
