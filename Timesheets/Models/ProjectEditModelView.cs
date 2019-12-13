using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Timesheets.Models
{
    public class ProjectEditModelView
    {
        public string Name { get; set; }
        
        
        public int OwnerDept { get; set; }
        public List<int> Departments { get; set; }
    }
}
