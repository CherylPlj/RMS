using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models;
using System.Security.Claims;

namespace RMS.Controllers
{
    public class ATenantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ATenantController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult ATenantHome()
        //{
        //    return View();
        //}

        public IActionResult ATenantHome()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.UserID.ToString() == userId);

            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
            }

            return View();
        }

        public IActionResult ATenantLease()
        {
            return View();
        }
        public IActionResult ATenantPayment()
        {
            return View();
        }
        public IActionResult ATenantMaintenance()
        {
            return View();
        }
        public IActionResult ATenantEditProfile()
        {
            return View();
        }
        

      
    }
}
