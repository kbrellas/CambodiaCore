using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Owner Department")]
        public Department OwnerDept { get; set; }
        [Display(Name = "Contributing Departments")]

        [Required(ErrorMessage ="You should assign at least one Contributing Department")]
        public ICollection<DepartmentProject> Departments { get; set; }
    }
}
