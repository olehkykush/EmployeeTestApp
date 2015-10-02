using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmployeeTestApp.Models;

namespace EmployeeTestApp.DAL
{
    public class EmployeeInitializer : System.Data.Entity.DropCreateDatabaseAlways<EmployeeContext>
    {
        protected override void Seed(EmployeeContext context)
        {
            var companies = new List<Company>
            {
            new Company{CompanyName="SymphonySolutions"},
            new Company{CompanyName="EPAM"},
            new Company{CompanyName="SoftServe"},
            new Company{CompanyName="Eleks"},
            new Company{CompanyName="GlobalLogic"}
            };
            companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();
            var employees = new List<Employee>
            {
            new Employee{FirstMidName="Maxim",LastName="Wasylyshyn", CompanyID=1},
            new Employee{FirstMidName="Oleksander",LastName="Zelenko", CompanyID=1},
            new Employee{FirstMidName="Taras",LastName="Wasylyk", CompanyID=2},
            new Employee{FirstMidName="Vasyl",LastName="Pasternak", CompanyID=2},
            new Employee{FirstMidName="Maksim",LastName="Gavrilyuk", CompanyID=5},
            new Employee{FirstMidName="Nazar",LastName="Chownyk", CompanyID=2},
            new Employee{FirstMidName="Marko",LastName="Wolanski", CompanyID=4},
            new Employee{FirstMidName="Oleksandr",LastName="Budny", CompanyID=1},
            new Employee{FirstMidName="Marko",LastName="Stasiuk", CompanyID=4},
            new Employee{FirstMidName="Taras",LastName="Kohut", CompanyID=3}
            };

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();
        }
    }
}