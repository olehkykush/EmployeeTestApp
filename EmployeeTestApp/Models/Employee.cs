using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTestApp.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
    }
}