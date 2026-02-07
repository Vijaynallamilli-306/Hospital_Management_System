using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Pages.Appointments
{
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = null!;

        public IActionResult OnGet(int id)
        {
            Appointment = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == id)!;

            if (Appointment == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            var appt = _context.Appointments.Find(Appointment.Id);

            if (appt != null)
            {
                _context.Appointments.Remove(appt);
                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
