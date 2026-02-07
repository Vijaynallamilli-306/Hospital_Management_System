using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Pages.Dashboard
{
    [Authorize(Roles = "Doctor")]
    public class DoctorModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public int MyAppointments { get; set; }

        public DoctorModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            MyAppointments = await _context.Appointments
                .CountAsync(a => a.Doctor.UserId == userId);
        }
    }
}
