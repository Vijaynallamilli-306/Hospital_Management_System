using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Pages.Appointments
{
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            // 👑 Admin & Receptionist → see ALL appointments
            if (User.IsInRole("Admin") || User.IsInRole("Receptionist"))
            {
                Appointments = await _context.Appointments
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToListAsync();
            }
            // 👨‍⚕️ Doctor → see ONLY own appointments
            else if (User.IsInRole("Doctor"))
            {
                var userId = _userManager.GetUserId(User);

                Appointments = await _context.Appointments
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient)
                    .Where(a => a.Doctor.UserId == userId)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToListAsync();
            }
        }
    }
}
