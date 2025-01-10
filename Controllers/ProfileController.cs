using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
