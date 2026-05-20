using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationBackend.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; } = "Center";

        //[ForeignKey("Department")] 
        public int MemberId { get; set; } //Foreign key
        public Member Member { get; set; } //Navigation Property
    }
}








/*
 Problem tab aa sakti hai jab naam clear nahi hai:

public int DeptId { get; set; }
public Department Department { get; set; }
Yahan EF Core automatically confuse ho sakta hai, kyunki DeptId ka naam DepartmentId nahi hai.

Tab explicitly batana padega: 
 
[ForeignKey("Department")]
public int DeptId { get; set; }

public Department Department { get; set; }
*/