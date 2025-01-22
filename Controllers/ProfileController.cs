using Microsoft.AspNetCore.Mvc;
using RMS.Models;

namespace RMS.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(ApplicationDbContext context, ILogger<ProfileController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult GoBack()
        {
            var refererUrl = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(refererUrl))
            {
                return Redirect(refererUrl); // Redirect to the previous page
            }
            return RedirectToAction("ProfilePage", "Profile"); // Fallback if no referer is found
        }


        // Display the profile page
        public IActionResult ProfilePage()
        {
            // Retrieve the logged-in user's ID from session (as string) and convert to int
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // If the session doesn't contain a valid UserId, redirect to login page
                //TempData["ErrorMessage"] = "User profile not found.";
                return RedirectToAction("Login", "Login");
            }

            // Log the UserId to the console (this will output to the console/logs)
            _logger.LogInformation($"Logged-in UserId: {userId}");


            // Fetch user profile and user details
            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == userId);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (profile == null)
            {
               // var newProfile = new Profile();  // Create a new profile (you can add default values if needed)
               // newProfile.Id = user.Id;
                // Create a default profile for new users
                profile = new Profile
                {
                    Id = user.Id, // Associate the profile with the user
                    FirstName = user.FirstName, 
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = profile.PhoneNumber,
                    //ProfilePicture = "",
                };
                _context.UserProfiles.Add(profile);
                _context.SaveChanges();
            }
            return View(profile);
        }


        public IActionResult EditProfile()
        {
            // Retrieve the logged-in user's ID from session (as string) and convert to int
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // If the session doesn't contain a valid UserId, redirect to login page
                return RedirectToAction("Login", "Login");
            }
            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == userId);
            return View(profile);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile model)
        {
            // Step 1: Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // Return the view with the validation error messages
                return View("EditProfile", model);
            }

            // Step 2: Retrieve the logged-in user's ID from session (as string) and convert to int
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // If the session doesn't contain a valid UserId, redirect to login page
                TempData["ErrorMessage"] = "User session expired. Please log in again.";
                return RedirectToAction("Login", "Login");
            }

            // Step 3: Retrieve the profile and user objects
            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == userId); // Use UserId instead of Id
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            // Step 4: Check for null objects
            if (profile == null)
            {
                TempData["ErrorMessage"] = "Profile not found for the user.";
                return RedirectToAction("CreateProfile", "Profile"); // Redirect to create profile page if not found
            }

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Login", "Login");
            }

            // Step 5: Debugging - Add checks to ensure profile and user objects are not null
            if (profile == null || user == null)
            {
                TempData["ErrorMessage"] = "User or Profile data is missing.";
                return RedirectToAction("Error", "Home"); // Or any other error page
            }

            // Step 6: Update profile and user information
            try
            {
                // Debugging - Ensure properties are not null
                if (model.FirstName == null || model.LastName == null)
                {
                    TempData["ErrorMessage"] = "First Name or Last Name is missing.";
                    return View("EditProfile", model);
                }

                // Update profile information
                profile.FirstName = model.FirstName ?? user.FirstName;
                profile.LastName = model.LastName ?? user.LastName;
                profile.PhoneNumber = model.PhoneNumber;
                profile.ProfilePicture = model.ProfilePicture; // Ensure ProfilePicture is handled properly
                profile.Email = model.Email;

                // Update user information (Email and Name)
                user.FirstName = model.FirstName ?? user.FirstName;
                user.LastName = model.LastName ?? user.LastName;
                user.Email = model.Email; // Make sure email is unique, if necessary

                // Update session with the new email
                HttpContext.Session.SetString("UserEmail", user.Email);

                // Step 7: Save changes to the database
                _context.SaveChanges();

                // Success message
                TempData["SuccessMessage"] = "Profile updated successfully!";
            }
            catch (Exception ex)
            {
                // Log error (optional) and display user-friendly message
                TempData["ErrorMessage"] = $"An error occurred while updating the profile: {ex.Message}";
            }

            // Step 8: Redirect to ProfilePage
            return RedirectToAction("ProfilePage");
        }



        // Display the EditPasswordPage
        public IActionResult ChangePasswordPage()
        {
            return View();
        }

        // Change or Update the password
        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            // Retrieve the logged-in user's ID from session (as string) and convert to int
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("ChangePasswordPage");
            }

            //// Check if the current password matches the user's password (this is without hashing)
            //if (user.Password != currentPassword)
            //{
            //    TempData["ErrorMessage"] = "Current password is incorrect.";
            //    return RedirectToAction("ChangePasswordPage");
            //}
            // Check if the current password matches the hashed password (using BCrypt)
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))  // Compare hashed password
            {
                TempData["ErrorMessage"] = "Current password is incorrect.";
                return RedirectToAction("ChangePasswordPage");
            }

            // Check if the new password and confirm password match
            if (newPassword != confirmPassword)
            {
                TempData["ErrorMessage"] = "New password and confirm password do not match.";
                return RedirectToAction("ChangePasswordPage");
            }

            // Hash the new password before saving it to the database
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);  // Hash the new password


            _context.SaveChanges();

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("ProfilePage");
        }




        public IActionResult ATenantEditProfile()
        {
            // Retrieve the logged-in user's ID from session (as string) and convert to int
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // If the session doesn't contain a valid UserId, redirect to login page
                return RedirectToAction("Login", "Login");
            }

            // Log the UserId to the console (this will output to the console/logs)
            _logger.LogInformation($"Logged-in UserId: {userId}");


            // Fetch user profile and user details
            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == userId);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (profile == null)
            {
                // Create a default profile for new users
                profile = new Profile
                {
                    FirstName = "",
                    LastName = "",
                    Email = "",
                    PhoneNumber = "",
                    CurrentPassword = "",
                    NewPassword = "",
                    ConfirmPassword = "",
                    ProfilePicture = null,
                    //Address = ""
                };
                _context.UserProfiles.Add(profile);
                _context.SaveChanges();
            }
            return View(profile);
        }
        public IActionResult PMEditProfile()
        {
            // Retrieve the logged-in user's ID from session (as string) and convert to int
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // If the session doesn't contain a valid UserId, redirect to login page
                return RedirectToAction("Login", "Login");
            }

            // Log the UserId to the console (this will output to the console/logs)
            _logger.LogInformation($"Logged-in UserId: {userId}");


            // Fetch user profile and user details
            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == userId);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (profile == null)
            {
                // Create a default profile for new users
                profile = new Profile
                {
                    FirstName = "",
                    LastName = "",
                    Email = "",
                    PhoneNumber = "",
                    CurrentPassword = "",
                    NewPassword = "",
                    ConfirmPassword = "",
                    ProfilePicture = null,
                    //Address = ""
                };
                _context.UserProfiles.Add(profile);
                _context.SaveChanges();
            }
            return View(profile);
        }

       

        // GET: Profile
        //public IActionResult EditProfile()
        //{
        //    // Example: Retrieve user data (replace with your logic)
        //    var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        //    var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new UserProfileModel
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //       //Unit = Unit.UnitName?.UnitName, // Assuming Unit is a navigation property
        //        //ProfilePicturePath = user.ProfilePicturePath
        //    };

        //    return View(model);
        //}

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
            var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Update user details
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            // Handle unit update (optional)
            if (!string.IsNullOrEmpty(model.Unit))
            {
                var unit = _context.Units.FirstOrDefault(u => u.UnitName == model.Unit);
                if (unit != null)
                {
                    //user.UnitID = unit.UnitID;
                }
            }

            //Handle password update(check current password validity, if applicable)
                if (model.CurrentPassword == user.Password) // Replace with proper password hashing comparison
                {
                    user.Password = model.NewPassword; // Replace with hashing logic
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "The current password is incorrect.");
                    return View(model);
                }

            // Handle profile picture upload
            if (model.ProfilePicture != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var uniqueFileName = $"{user.Id}_{model.ProfilePicture.FileName}";
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
