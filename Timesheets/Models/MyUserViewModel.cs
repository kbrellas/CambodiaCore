using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Models
{
    public class MyUserViewModel
    {
        public string  FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public double CostPerHour { get; set; }

        public IList<string> Role { get; set; }

        public int DepartmentId { get; set; }
    }
}
