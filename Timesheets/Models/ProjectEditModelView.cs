using System;
using System.Collections.Generic;


namespace Timesheets.Models
{
    public class ProjectEditModelView
    {
        public string Name { get; set; }
        
        
        public int OwnerDept { get; set; }
        public List<int> Departments { get; set; }
    }
}
