using Microsoft.AspNetCore.Mvc;
using WebApplicationBackend.Context;
using WebApplicationBackend.Entities;

namespace WebApplicationBackend.Controllers
{
    public class MemberController : Controller
    {
        private readonly AppDbContext _context;

        public Member MemberData = new Member()
        {
           
            MemberName = "Sonia Gandhi",
            Age = 84,
            Gender = "Female",
            Phone = "6976543210",
            Email = "sonia@inc.com",
            JoinDate = new DateTime(1995, 5, 10)
        };

        public MemberController(AppDbContext context)
        {
            _context = context;
            _context.Members.Add(MemberData);

            _context.SaveChanges();
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
