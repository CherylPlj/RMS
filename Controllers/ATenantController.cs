using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers
{
    public class ATenantController : Controller
    {
        public IActionResult ATenantLease()
        {
            return View();
        }

        public IActionResult ATenantHome()
        {
            return View();
        }
    }
}
