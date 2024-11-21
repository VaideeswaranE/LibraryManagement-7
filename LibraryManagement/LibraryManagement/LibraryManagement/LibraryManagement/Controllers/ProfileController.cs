using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using System.Threading.Tasks;

public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: Profile/Index
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User); // Correct method
        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            UserId = user.Id,
            FullName = user.FullName,  // Assuming FullName is a custom property
            Email = user.Email,
            Phone = user.PhoneNumber,  // PhoneNumber should be valid
        };

        return View(model);
    }

    // GET: Profile/Update
    public async Task<IActionResult> Update()
    {
        var user = await _userManager.GetUserAsync(User); // Correct method
        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            UserId = user.Id,
            FullName = user.FullName,  // Assuming FullName is a custom property
            Email = user.Email,
            Phone = user.PhoneNumber,  // PhoneNumber should be valid
        };

        return View(model);
    }

    // POST: Profile/Update
    [HttpPost]
    [ValidateAntiForgeryToken] // Ensure CSRF protection
    public async Task<IActionResult> Update(ProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                // Update user properties
                user.FullName = model.FullName; // Assuming FullName is a custom property
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;

                // Update user in the database
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Add errors to the ModelState
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
        }
        return View(model); // Return the model with validation errors if any
    }
}
