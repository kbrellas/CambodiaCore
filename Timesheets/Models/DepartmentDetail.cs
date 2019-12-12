using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Models
{
    public class DepartmentDetail
    {
        public Department department {get; set;}
        public List<Project> projects { get; set; }
    }
}
