using Microsoft.AspNetCore.Mvc;
using RMS.Models;

namespace RMS.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profile
        public IActionResult EditProfile()
        {
            // Example: Retrieve user data (replace with your logic)
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.UserID.ToString() == userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                //Unit = user.Unit?.UnitName, // Assuming Unit is a navigation property
                //ProfilePicturePath = user.ProfilePicturePath
            };

            return View(model);
        }

        // POST: Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.UserID.ToString() == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Update user details
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            //// Handle unit update (optional)
            //if (!string.IsNullOrEmpty(model.Unit))
            //{
            //    var unit = _context.Units.FirstOrDefault(u => u.UnitName == model.Unit);
            //    if (unit != null)
            //    {
            //        //user.UnitID = unit.UnitID;
            //    }
            //}

            // Handle password update (check current password validity, if applicable)
            //if (model.CurrentPassword == user.Password) // Replace with proper password hashing comparison
            //{
            //    user.Password = model.NewPassword; // Replace with hashing logic
            //}
            //else
            //{
            //    ModelState.AddModelError("CurrentPassword", "The current password is incorrect.");
            //    return View(model);
            //}

            // Handle profile picture upload
            if (model.ProfilePicture != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var uniqueFileName = $"{user.UserID}_{model.ProfilePicture.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }

                //user.ProfilePicturePath = $"/images/{uniqueFileName}";
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction(nameof(EditProfile));
        }
    }
}
