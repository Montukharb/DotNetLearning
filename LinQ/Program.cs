
public class Program : Fruit
{

    public static void Main(string[] args)
    {
        WriteLine("LinQ test main method run ");
        Fruit fi = new Fruit();
        fi.DisplayFruit();
        LanguageIntegratedQuery query = new LanguageIntegratedQuery();
        LanguageIntegratedQuery2 query2 = new();
        query2.LinqGroupJoin();
        query2.LinqGroupJoinQuery();

        //GroupBy Example :-
        var result = DataProvider.students.GroupBy(x => x.DepartmentId);

        foreach (var group in result)
        {
            WriteLine($"\nGroup Key = {group.Key}, Total record = {group.Count()}");
            foreach (var item in group)
            {
                WriteLine($"StudId = {item.StuId} ,Name = {item.Name}, Email = {item.Email}, DepartmentId = {item.DepartmentId}");
            }
        }

        WriteLine("inner join result on matching id's");

        var innerJoin = DataProvider.students.Join(
             DataProvider.departments,
             s => s.DepartmentId,
             d => d.DeptId,
             (s, d) => new
             {
                 STUDENT = s,
                 DEPARTMENT = d
             }
            );

        foreach (var item in innerJoin)
        {
            WriteLine($"StudId = {item.STUDENT.StuId}, Name = {item.STUDENT.Name}, Email = {item.STUDENT.Email}, StudentDepartmentId = {item.STUDENT.DepartmentId}, DeptName = {item.DEPARTMENT.DeptName}, DeptId = {item.DEPARTMENT.DeptId}");
        }

        var groupJoinResult = DataProvider.departments.GroupJoin(
            DataProvider.students,
            d => d.DeptId,
            s => s.DepartmentId,
            (d, datacollection) => new
            {
                GroupName = d.DeptName,
                datacollection
            }
            );

        WriteLine("\nGroupJoin result :)\n");
        foreach (var group in groupJoinResult)
        {
            WriteLine($"GroupName = {group.GroupName}");
            foreach (var item in group.datacollection)
            {
                WriteLine($"StudId = {item.StuId}, Name = {item.Name}, Email = {item.Email}, StudentDepartmentId = {item.DepartmentId}");
            }
        }
        //leftJoin inbuild in C# new method syntax

        var leftjoinResult = DataProvider.departments.LeftJoin(
            DataProvider.students,
            d => d.DeptId,
            s => s.DepartmentId,
            (d, s) => new
            {
                DEPARTMENT = d,
                STUDENT = $"StudId = {s?.StuId}, Name = {s?.Name}, Email = {s?.Email}, StudentDepartmentId = {s?.DepartmentId}"
            }
            );

        WriteLine("\nLeftJoin result :)\n");
        foreach (var item in leftjoinResult)
        {
            WriteLine(item.DEPARTMENT.DeptName + "-------" + item.STUDENT);
        }


        //RightJoin inbuild in C# new method syntax

        var rightjoinResult = DataProvider.departments.RightJoin(
            DataProvider.students,
            d => d.DeptId,
            s => s.DepartmentId,
            (d, s) => new
            {
                DEPARTMENT = d,
                STUDENT = $"StudId = {s?.StuId}, Name = {s?.Name}, Email = {s?.Email}, StudentDepartmentId = {s?.DepartmentId}"
            }
            );

        WriteLine("\nRightJoin result :)\n");
        foreach (var item in rightjoinResult)
        {
            WriteLine($"{item?.DEPARTMENT?.DeptName ?? "Not provided"} ------- {item?.STUDENT} ");
        }
    }
}

/*
 Har department ke students lao.

var result = await _context.Departments
    .GroupJoin(
        _context.Students,
        department => department.Id,
        student => student.DepartmentId,
        (department, students) => new
        {
            DepartmentName = department.DepartmentName,
            Students = students
        })
    .ToListAsync();
Output
[
  {
    "departmentName":"IT",
    "students":[
      {"id":1,"name":"Aman"},
      {"id":2,"name":"Mohit"}
    ]
  },
  {
    "departmentName":"CSE",
    "students":[
      {"id":3,"name":"Rohit"}
    ]
  },
  {
    "departmentName":"ECE",
    "students":[]
  }
]

Notice:

IT  -> 2 Students
CSE -> 1 Student
ECE -> 0 Student

GroupJoin group bana kar deta hai.

4. LEFT JOIN using GroupJoin

SQL

SELECT *
FROM Departments d
LEFT JOIN Students s
ON d.Id=s.DepartmentId

EF Core

var result = await _context.Departments
    .GroupJoin(
        _context.Students,
        d => d.Id,
        s => s.DepartmentId,
        (d, students) => new
        {
            Department = d,
            Students = students
        })
    .SelectMany(
        x => x.Students.DefaultIfEmpty(),
        (x, student) => new
        {
            DepartmentName = x.Department.DepartmentName,
            StudentName = student != null
                ? student.Name
                : "No Student"
        })
    .ToListAsync();
Output
[
  {
    "departmentName":"IT",
    "studentName":"Aman"
  },
  {
    "departmentName":"IT",
    "studentName":"Mohit"
  },
  {
    "departmentName":"CSE",
    "studentName":"Rohit"
  },
  {
    "departmentName":"ECE",
    "studentName":"No Student"
  }
]
5. GroupBy Example

Department wise student count.

SQL

SELECT DepartmentId,
       COUNT(*)
FROM Students
GROUP BY DepartmentId

EF Core

var result = await _context.Students
    .GroupBy(s => s.DepartmentId)
    .Select(g => new
    {
        DepartmentId = g.Key,
        TotalStudents = g.Count()
    })
    .ToListAsync();
Output
[
  {
    "departmentId":1,
    "totalStudents":2
  },
  {
    "departmentId":2,
    "totalStudents":1
  }
]
Join vs GroupJoin vs GroupBy
Feature	Join	GroupJoin	GroupBy
Purpose	2 tables combine	Parent + child collection	Grouping
Result	Flat rows	Nested collection	Groups
SQL Equivalent	INNER JOIN	LEFT JOIN style	GROUP BY
Collection Return	No	Yes	Yes
Count/Sum	No	Possible	Main purpose
 */