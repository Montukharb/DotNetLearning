using System;
using System.Collections.Generic;
using System.Text;

namespace LinQ
{
    internal class DataProvider
    {
        public static List<Student> students = new() {

          new Student{StuId = 1, Name = "Sachin", Email = "sachin520@gmail.com",DepartmentId = 101 },
          new Student{StuId = 2, Name = "Vishal", Email = "vishal@gmail.com",DepartmentId = 102 },
          new Student{StuId = 3, Name = "Ashish", Email = "ashish@gmail.com",DepartmentId = 102 },
          new Student{StuId = 4, Name = "Rahul", Email = "rahul@gmail.com",DepartmentId = 103 },
          new Student{StuId = 5, Name = "Ram", Email = "ram@gmail.com",DepartmentId = 103 },
          new Student{StuId = 6, Name = "Shyam", Email = "shyam@gmail.com",DepartmentId = 102 },
          new Student{StuId = 7, Name = "Anny", Email = "anny@gmail.com",DepartmentId = 101 },
          new Student{StuId = 8, Name = "John", Email = "john@gmail.com",DepartmentId = 104 },
          new Student{StuId = 9, Name = "BoB", Email = "bob@gmail.com",DepartmentId = 104 },
          new Student{StuId = 10, Name = "Alice", Email = "alice@gmail.com",DepartmentId = 103 },
          new Student{StuId = 11, Name = "Mayur", Email = "mayur@gmail.com" },
          new Student{StuId = 12, Name = "Deepesh", Email = "deepesh@gmail.com" },
      };

        public static List<Department> departments = new() {

          new Department{DeptId = 101 , DeptName = "IT" },
          new Department{DeptId = 102 , DeptName = "SALES" },
          new Department{DeptId = 103 , DeptName = "HR" },
          new Department{DeptId = 104 , DeptName = "DESIGN" },

        };
    }

    public class Student
    {
        public required int StuId { get; set; }
        public string Name { get; set; } = string.Empty;
        public required string Email { get; set; }

        public int DepartmentId { get; set; }
    }
    public class Department
    {
        public required int DeptId { get; set; }
        public required string DeptName { get; set; }
    }
}
