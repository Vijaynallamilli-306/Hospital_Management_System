using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Pages.Appointments
{
    [Authorize(Roles = "Admin,Receptionist")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ✅ Load existing entity FIRST
            var existingAppointment = _context.Appointments
                .FirstOrDefault(a => a.Id == Appointment.Id);

            if (existingAppointment == null)
            {
                return NotFound();
            }

            // ✅ Update ONLY allowed fields
            existingAppointment.AppointmentDate = Appointment.AppointmentDate;
            existingAppointment.Status = Appointment.Status;

            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
