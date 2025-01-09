using Microsoft.AspNetCore.Mvc;
using RMS.Models;


namespace RMS.Controllers
{
    public class PropertyManagerController : Controller
    {
        // Dashboard
        public IActionResult PMDashboard()
        {
            return View();
        }

        // Units
        public IActionResult PMManageUnits()
        {
            return View();
        }

        private static List<Unit> Units = new List<Unit>
        {
            new Unit { Id = 1, Name = "Cozy Suite 10", Price = 11000, Status = "Occupied" },
            new Unit { Id = 2, Name = "Cozy Suite 11", Price = 12000, Status = "Occupied" },
            new Unit { Id = 3, Name = "Cozy Suite 12", Price = 13000, Status = "Available" },
            new Unit { Id = 4, Name = "Cozy Suite 13", Price = 14000, Status = "Under Maintenance" },
            new Unit { Id = 5, Name = "Cozy Suite 14", Price = 15000, Status = "Available" }
        };

        public IActionResult Index()
        {
            return View(Units);
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            var filteredUnits = Units
                .Where(u => u.Name.Contains(search) || u.Status.Contains(search))
                .ToList();
            return PartialView("_UnitList", filteredUnits);
        }

        [HttpPost]
        public IActionResult Edit(Unit updatedUnit)
        {
            var unit = Units.FirstOrDefault(u => u.Id == updatedUnit.Id);
            if (unit != null)
            {
                unit.Name = updatedUnit.Name;
                unit.Price = updatedUnit.Price;
                unit.Status = updatedUnit.Status;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Units = Units.Where(u => u.Id != id).ToList();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult AddUnit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUnit(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save to database logic here
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Tenants
        public IActionResult PMTenants()
        {
            return View();
        }

        // Lease
        public IActionResult PMManageLease()
        {
            return View();
        }

        // Payments
        public IActionResult PMPayments()
        {
            return View();
        }

        // Request
        public IActionResult PMRequest()
        {
            return View();
        }

        //Staff 
        public IActionResult PMAssignMaintenance()
        {
            return View();
        }


    }
}
