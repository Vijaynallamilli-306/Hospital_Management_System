using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Account
{
    [Authorize(Roles = "Admin")] // Only Admin can create users
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        [BindProperty]
        public string Role { get; set; } = "";

        public List<string> Roles { get; set; } = new();

        public void OnGet()
        {
            Roles = _roleManager.Roles.Select(r => r.Name!).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Roles = _roleManager.Roles.Select(r => r.Name!).ToList();

            if (!ModelState.IsValid)
                return Page();

            var user = new ApplicationUser
            {
                UserName = Email,
                Email = Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return Page();
            }

            if (!string.IsNullOrEmpty(Role))
            {
                await _userManager.AddToRoleAsync(user, Role);
            }

            return RedirectToPage("/Index");
        }
    }
}
