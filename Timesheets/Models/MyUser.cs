using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Models
{
    public class MyUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        

        [Display(Name = "Department")]
        public Department Department { get; set; }

        public Department ManagedDepartment { get; set; }

        [Display(Name = "Cost per Hour")]
        public double CostPerHour { get; set; }
        [Display(Name = "Manager")]
        public MyUser Manager { get; set; }
    }
}
