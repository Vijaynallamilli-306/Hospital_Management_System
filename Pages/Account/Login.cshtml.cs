using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _signInManager.PasswordSignInAsync(
                Email,
                Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return Page();
            }

            // ✅ Get logged-in user
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
                return RedirectToPage("/Index");

            // ✅ ROLE-BASED REDIRECTION
            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return RedirectToPage("/Dashboard/Admin");

            if (await _userManager.IsInRoleAsync(user, "Doctor"))
                return RedirectToPage("/Dashboard/Doctor");

            if (await _userManager.IsInRoleAsync(user, "Receptionist"))
                return RedirectToPage("/Dashboard/Receptionist");

            // ✅ Fallback (safety)
            return RedirectToPage("/Index");
        }
    }
}
