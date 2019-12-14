using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Models
{
    public class TimesheetEntryViewModel
    {
        [Display(Name = "Related Project")]
        public int RelatedProject { get; set; }

        [Display(Name = "Date")]
        
        public DateTime DateCreated { get; set; }

        [Display(Name ="Hours Worked")]
        [Range(0, 24)]
        public int HoursWorked { get; set; }
    }
}
