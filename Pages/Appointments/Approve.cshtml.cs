using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Appointments
{
    // Only Admin & Receptionist can approve appointments
    [Authorize(Roles = "Admin,Receptionist")]
    public class ApproveModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ApproveModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Find appointment
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            // Update status
            appointment.Status = "Approved";

            await _context.SaveChangesAsync();

            // Redirect back to list
            return RedirectToPage("Index");
        }
    }
}
