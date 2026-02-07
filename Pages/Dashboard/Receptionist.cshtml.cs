using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Dashboard
{
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public int TodayAppointments { get; set; }

        public ReceptionistModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            TodayAppointments = _context.Appointments
                .Count(a => a.AppointmentDate.Date == DateTime.Today);
        }
    }
}
