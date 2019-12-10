using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Models
{
    public class TimesheetEntry
    {
        
        public int Id { get; set; }

        public MyUser RelatedUser { get; set; }
        public Project RelatedProject { get; set; }

        [Display(Name ="Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name ="Hours Worked")]
        public int HoursWorked { get; set; }
    }
}
