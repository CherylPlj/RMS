using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers
{
    public class ATenantController : Controller
    {
        public IActionResult ATenantLease()
        {
            return View();
        }
    }
}
