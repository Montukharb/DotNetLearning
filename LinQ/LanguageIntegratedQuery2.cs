using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Xml.Schema;

namespace LinQ
{
    internal class LanguageIntegratedQuery2
    {
        public List<Employee> EmpList = new()
        {
            new Employee(){ Id = 1, Name = "Sachin", Email = "SachinKharb526@gmail.com"},
            new Employee(){ Id = 2, Name = "Vishal", Email = "VishalSharma32@gmail.com"},
            new Employee(){ Id = 3, Name = "Tonki", Email = "Tonki33@gmail.com"},
            new Employee(){ Id = 4, Name = "Monki", Email = "Monki56@gmail.com"},
            new Employee(){ Id = 5, Name = "Ponki", Email = "Ponki4@gmail.com"},
            new Employee(){ Id = 6, Name = "Iorim", Email = "Iorim09@gmail.com"},
            new Employee(){ Id = 7, Name = "John", Email = "Johnsmith10@gmail.com"}
        };

        public List<EmpDepartment> DepList = new()
        {
            new EmpDepartment(){DepartmentId = 1, DepartmentName = "Marketing", EmpDeptPosition = "Trainer"},
            new EmpDepartment(){DepartmentId = 2, DepartmentName = "HR", EmpDeptPosition = "HR Manager"},
            new EmpDepartment(){DepartmentId = 3, DepartmentName = "IT", EmpDeptPosition = "Software Developer"},
            new EmpDepartment(){DepartmentId = 4, DepartmentName = "Finance", EmpDeptPosition = "Accountant"},
            new EmpDepartment(){DepartmentId = 5, DepartmentName = "Sales", EmpDeptPosition = "Sales Executive"},
            new EmpDepartment(){DepartmentId = 6, DepartmentName = "Support", EmpDeptPosition = "Customer Support"},
            new EmpDepartment(){DepartmentId = 7, DepartmentName = "Operations", EmpDeptPosition = "Operations Manager"},

            new EmpDepartment(){DepartmentId = 1, DepartmentName = "Marketing", EmpDeptPosition = "SEO Specialist"},
            new EmpDepartment(){DepartmentId = 1, DepartmentName = "Marketing", EmpDeptPosition = "Content Writer"},
            new EmpDepartment(){DepartmentId = 1, DepartmentName = "Marketing", EmpDeptPosition = "Digital Marketer"},

            new EmpDepartment(){DepartmentId = 2, DepartmentName = "HR", EmpDeptPosition = "Recruiter"},
            new EmpDepartment(){DepartmentId = 2, DepartmentName = "HR", EmpDeptPosition = "HR Executive"},

            new EmpDepartment(){DepartmentId = 3, DepartmentName = "IT", EmpDeptPosition = "Backend Developer"},
            new EmpDepartment(){DepartmentId = 4, DepartmentName = "Finance", EmpDeptPosition = "Financial Analyst"},
            new EmpDepartment(){DepartmentId = 4, DepartmentName = "Finance", EmpDeptPosition = "Tax Consultant"},
            new EmpDepartment(){DepartmentId = 3, DepartmentName = "IT", EmpDeptPosition = "Frontend Developer"}
        };

        public void LinqGroupJoin()
        {
            //method syntax 
            var groupjoinResult = EmpList.GroupJoin(
                 DepList,
                 emp => emp.Id,
                 empdep => empdep.DepartmentId,
                 (employee, dataCollection) =>
                 new
                 {
                     Name = employee.Name,
                     Email = employee.Email,
                     Id = employee.Id,

                     dataCollection
                 }

                );

            foreach (var item in groupjoinResult)
            {
                WriteLine($"GroupID = {item.Id} ------ Employee Name = {item.Name}");

                foreach (var item2 in item.dataCollection)
                {
                    Write($"Department Name = {item2.DepartmentName}, Handle Profile = {item2.EmpDeptPosition}\n");
                }
            }

            //left join example left join koi method nahi hai ye groupjoin par hi use hoga 
            var leftjoinresult = EmpList.GroupJoin(
                DepList,
                emp => emp.Id,
                empdep => empdep.DepartmentId,
                (employee, dataCollection) =>
                new
                {
                    employee,
                    dataCollection
                }

               ).SelectMany(x => x.dataCollection.DefaultIfEmpty(), (x, data) => new
               {
                   EmployeName = x.employee.Name,
                   email = x.employee.Email,
                   data,
               });
        }

        //query Method
        public void LinqGroupJoinQuery()
        {
            var groupJoinResult = from emp in EmpList
                                  join empDepartment in DepList on emp.Id equals empDepartment.DepartmentId
                                  into dataCollection
                                  select new
                                  {
                                      Name = emp.Name,
                                      Email = emp.Email,
                                      Id = emp.Id,
                                      dataCollection
                                  };
            //traverse using foreach groupjoin
            foreach (var item in groupJoinResult)
            {
                Write($"GroupId = {item.Id} -- GroupName = {item.Name}{Environment.NewLine}");
                foreach (var item2 in item.dataCollection)
                {
                    Write($"Department Name = {item2.DepartmentName}, Handle Profile = {item2.EmpDeptPosition}\n");
                }
            }
        }

    }

    internal class Employee
    {
        public required int Id { get; set; }
        public required string? Name { get; set; }
        public required string? Email { get; set; }
    }

    internal class EmpDepartment
    {
        public required int DepartmentId { get; set; }
        public required string? DepartmentName { get; set; }
        public string? EmpDeptPosition { get; set; }
    }
}
/*
2.Raw SQL
var students = await _context.Students
    .FromSqlRaw("SELECT * FROM Students")
    .ToListAsync();

Faayde:

Complex queries likhna easy
Existing SQL reuse kar sakte ho
Stored Procedure call kar sakte ho

Nuksan:

String ke andar query likhni padti hai
Column/table rename hone par compiler nahi batata
SQL Injection ka risk (agar parameters sahi use na karo)

    EF Core me 3 tareeke hain:

1.LINQ Method Syntax(sabse ajeeb lagti hai)
var data = _context.Students
    .Join(
        _context.Departments,
        s => s.DepartmentId,
        d => d.Id,
        (s, d) => new
        {
            s.Name,
            d.DepartmentName
        });
2.LINQ Query Syntax(SQL jaisi)

Ye aksar beginners ko zyada samajh aati hai.

var data =
    from s in _context.Students
    join d in _context.Departments
    on s.DepartmentId equals d.Id
    select new
           {
               s.Name,
               d.DepartmentName
           };

Ye lagbhag SQL jaisa hi hai.

3. Raw SQL (Seedha SQL)
var data = await _context.StudentDtos
    .FromSqlRaw(@"
        SELECT s.Name,
               d.DepartmentName
        FROM Students s
        INNER JOIN Departments d
        ON s.DepartmentId = d.Id")
    .ToListAsync();
*/