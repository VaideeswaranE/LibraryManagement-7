using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Admin
        public IActionResult Index()
        {
            return RedirectToAction("UserManagement");
        }

        // GET: Admin/UserManagement
        public async Task<IActionResult> UserManagement()
        {
            var users = await _userManager.Users.ToListAsync();
            return View("Index", users);
        }

        // GET: Admin/UserProfile?userId={userId}
        public async Task<IActionResult> UserProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                UserId = user.Id,
                FullName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
            };

            return View(model);
        }

        // GET: Admin/Update?userId={userId}
        public async Task<IActionResult> Update(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                UserId = user.Id,
                FullName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
            };

            return View(model);
        }

        // POST: Admin/Update
        [HttpPost]
        public async Task<IActionResult> Update(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.UserName = model.FullName; // Assuming UserName is to be set as FullName
                    user.Email = model.Email;
                    user.PhoneNumber = model.Phone;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Profile updated successfully.";
                        return RedirectToAction("UserProfile", new { userId = user.Id });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model); // Return the view with the current model if there's a validation error
        }

        // POST: Admin/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "User deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error deleting user.";
                }
            }
            return RedirectToAction("UserManagement");
        }
    }
}
