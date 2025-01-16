using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers
{
    public class PTenantController : Controller
    {
        public IActionResult PTenantHomePage()
        {
            return View();
        }

        public IActionResult PTenantUnits()
        {
            return View();
        }

        public IActionResult PTenantDetails()
        {
            return View();
        }
        public IActionResult PTenantApply()
        {
            return View();
        }

        public IActionResult Category(string category)
        {
            ViewBag.Category = category; // Pass the section to the view
            return View();
        }
    }

}
