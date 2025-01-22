using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models;

namespace RMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(string email, string password)
        {
            // Validate if email or password is empty
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email and Password are required.");
                return View("Login"); // Return to the Login page with error messages
            }

            // Find the user by email
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password."); // Generic error for security reasons
                return View("Login"); // Return to the Login page
            }

            // Verify the entered password against the stored hashed password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid email or password."); // Generic error for security reasons
                return View("Login"); // Return to the Login page
            }

            // Store user information in the session
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("FirstName", user.FirstName);
            HttpContext.Session.SetString("LastName", user.LastName);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Redirect the user to their appropriate dashboard based on role
            if (user.Role == "Property Manager")
            {
                return RedirectToAction("PMDashboard", "PropertyManager");
            }
            else if (user.Role == "Staff")
            {
                return RedirectToAction("SHomePage", "Staff");
            }
            else if (user.Role == "Tenant")
            {
                return RedirectToAction("ATenantHome", "ATenant");
            }

            // Fallback redirect if no role matches
            return RedirectToAction("Login");
        }



        public IActionResult ForgotPass()
        {
            return View();
        }

        public IActionResult Register()
        {
            var user = new User();
            return View(user);
        }

        // RegisterUser action to handle form submission and save user data
        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            // Check if email already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "An account with this email already exists.");
                return View("Register", user); // Return to the Register page
            }

            // Validate password length
            if (user.Password.Length < 15 || user.Password.Length > 64)
            {
                ModelState.AddModelError("", "Password must be between 15 and 64 characters long.");
                return View("Register", user); // Return to the Register page
            }

            // Check if passwords match
            if (user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("", "Password and Confirm Password do not match.");
                return View("Register", user); // Return to the Register page
            }

            // Check if Terms and Conditions are accepted
            if (user.TermsAndConditions.HasValue && !user.TermsAndConditions.Value)
            {
                ModelState.AddModelError("", "You must agree to the terms and conditions.");
                return View("Register", user); // Return to the Register page
            }

            // Set the Role based on the email domain
            if (user.Email.Contains("@manager"))
            {
                user.Role = "Property Manager";
            }
            else if (user.Email.Contains("@staff"))
            {
                user.Role = "Staff";
            }
            else
            {
                user.Role = "Tenant"; // Default role if the email doesn't match above criteria
            }

            // Hash the password using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Create a new User object and populate it with data
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = hashedPassword, // Store hashed password
                Role = user.Role,
                TermsAndConditions = user.TermsAndConditions,
                DateCreated = DateTime.Now
            };

            // Save the new user to the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //// Create a new profile and assign the UserID from the saved user
            //var newProfile = new Profile
            //{
            //    //Id = newUser.Id, // Assign the generated UserID to the profile
            //    FirstName = newUser.FirstName, // Use the user's first name
            //    LastName = newUser.LastName,   // Use the user's last name
            //                                   // Add any other default values or required fields for the profile here
            //};

            //// Save the new profile to the database
            //_context.UserProfiles.Add(newProfile);
            //_context.SaveChanges();

            // Store user information in the session
            HttpContext.Session.SetInt32("UserId", newUser.Id);
            HttpContext.Session.SetString("FirstName", newUser.FirstName);
            HttpContext.Session.SetString("LastName", newUser.LastName);
            HttpContext.Session.SetString("UserEmail", newUser.Email);
            HttpContext.Session.SetString("UserRole", newUser.Role);

            // Redirect based on the user's role
            if (newUser.Role == "Property Manager")
            {
                return RedirectToAction("PMDashboard", "PropertyManager");
            }
            else if (newUser.Role == "Staff")
            {
                return RedirectToAction("SHomePage", "Staff");
            }
            else if (newUser.Role == "Tenant")
            {
                return RedirectToAction("ATenantHome", "ATenant");
            }

            // If none of the conditions match, return to the Register action (or another appropriate action)
            return RedirectToAction("Register"); // This is the fallback action, should never be reached
        }



        //// RegisterUser action to handle form submission and save user data
        //[HttpPost]
        //public IActionResult RegisterUser(User user)
        //{
        //    // Check if email already exists
        //    var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
        //    if (existingUser != null)
        //    {
        //        ModelState.AddModelError("", "An account with this email already exists.");
        //        return View("Register", user); // Return to the Register page
        //    }

        //    // Check if passwords match
        //    if (user.Password != user.ConfirmPassword)
        //    {
        //        ModelState.AddModelError("", "Password and Confirm Password do not match.");
        //        return RedirectToAction("Register"); // Redirect to the Register action
        //    }

        //    // Check if Terms and Conditions are accepted
        //    if (user.TermsAndConditions.HasValue && !user.TermsAndConditions.Value)
        //    {
        //        ModelState.AddModelError("", "You must agree to the terms and conditions.");
        //        return RedirectToAction("Register"); // Redirect to the Register action
        //    }

        //    // Set the Role based on the email domain
        //    if (user.Email.Contains("@manager"))
        //    {
        //        user.Role = "Property Manager";
        //    }
        //    else if (user.Email.Contains("@staff"))
        //    {
        //        user.Role = "Staff";
        //    }
        //    else
        //    {
        //        user.Role = "Tenant"; // Default role if the email doesn't match above criteria
        //    }


        //    // Store user information in the session
        //    HttpContext.Session.SetInt32("UserId", user.UserID);
        //    HttpContext.Session.SetString("FirstName", user.FirstName);
        //    HttpContext.Session.SetString("LastName", user.LastName);
        //    HttpContext.Session.SetString("UserEmail", user.Email);
        //    HttpContext.Session.SetString("UserRole", user.Role);


        //    // Create a new User object and populate it with data
        //    var newUser = new User
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        Password = user.Password,
        //        Role = user.Role, // Default or set role based on your requirement
        //        TermsAndConditions = user.TermsAndConditions,
        //    };

        //    // Save the new user to the database
        //    _context.Users.Add(newUser);
        //    _context.SaveChanges();

        //    // Redirect based on the user's role
        //    if (user.Role == "Property Manager")
        //    {
        //        return RedirectToAction("PMDashboard", "PropertyManager");
        //    }
        //    else if (user.Role == "Staff")
        //    {
        //        return RedirectToAction("SHomePage", "Staff");
        //    }
        //    else if (user.Role == "Tenant")
        //    {
        //        return RedirectToAction("ATenantHome", "ATenant");
        //    }

        //    // If none of the conditions match, return to the Register action (or another appropriate action)
        //    return RedirectToAction("Register"); // This is the fallback action, should never be reached

        //}


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
           // return RedirectToAction("Login");
            return RedirectToAction("PTenantHomePage", "PTenant");

        }
    }
}