using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Appointments
{
    [Authorize(Roles = "Doctor")]
    public class CompleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CompleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Get appointment
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            // Update status
            appointment.Status = "Completed";

            await _context.SaveChangesAsync();

            // Redirect back to appointments list
            return RedirectToPage("Index");
        }
    }
}
