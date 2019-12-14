using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Models
{
    public class ProjectViewModel
    {
       // [DisplayName("Enter a Name for The New Project")]
        [Required( ErrorMessage = "You should enter a Project Name")]
        public string Name { get; set; }
        //[DisplayName("Select the Department that ownes the Project")]
        [Required( ErrorMessage = "You should choose a Department Owner for the Project ")]
        public int OwnerDept { get; set; }

        //[DisplayName("Select the Departments that will contribute to the Project")]
        [Required(ErrorMessage ="You should assing at least one Contributing Department")]
        public List<int> Departments { get; set; }
    }
}
