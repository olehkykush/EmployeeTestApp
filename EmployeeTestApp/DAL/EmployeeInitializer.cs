using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmployeeTestApp.Models;

namespace EmployeeTestApp.DAL
{
    public class EmployeeInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EmployeeContext>
    {
        protected override void Seed(EmployeeContext context)
        {
            var companies = new List<Company>
            {
            new Company{CompanyName="SymphonySolutions"},
            new Company{CompanyName="EPAM"},
            new Company{CompanyName="SoftServe"}
            };
            companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();
            var employees = new List<Employee>
            {
            new Employee{FirstMidName="Carson",LastName="Alexander", CompanyID=1},
            new Employee{FirstMidName="Meredith",LastName="Alonso", CompanyID=1},
            new Employee{FirstMidName="Arturo",LastName="Anand", CompanyID=1},
            new Employee{FirstMidName="Gytis",LastName="Barzdukas", CompanyID=2},
            new Employee{FirstMidName="Gytis",LastName="Li", CompanyID=3}
            };

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();
        }
    }
}