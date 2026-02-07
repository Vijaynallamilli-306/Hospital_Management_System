using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Dashboard
{
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public int DoctorsCount { get; set; }
        public int PatientsCount { get; set; }
        public int AppointmentsCount { get; set; }

        public AdminModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            DoctorsCount = _context.Doctors.Count();
            PatientsCount = _context.Patients.Count();
            AppointmentsCount = _context.Appointments.Count();
        }
    }
}
