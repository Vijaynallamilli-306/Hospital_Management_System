using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagementSystem.Pages.Appointments
{
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = new Appointment();

        public SelectList Doctors { get; set; } = null!;
        public SelectList Patients { get; set; } = null!;

        public void OnGet()
        {
            Doctors = new SelectList(_context.Doctors, "Id", "Name");
            Patients = new SelectList(_context.Patients, "Id", "Name");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Doctors = new SelectList(_context.Doctors, "Id", "Name");
                Patients = new SelectList(_context.Patients, "Id", "Name");
                return Page();
            }

            _context.Appointments.Add(Appointment);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
