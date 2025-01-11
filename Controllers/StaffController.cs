using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult SMaintenanceAssignment()
        {
            return View();
        }
        public IActionResult SMaintenanceHistory()
        {
            return View();
        }
        public IActionResult SEditProfile()
        {
            return View();
        }

    }
}
