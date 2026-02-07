using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Appointments
{
    // ✅ All roles can cancel (as per your design)
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public class CancelModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CancelModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // 🔍 Find appointment
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            // ❌ Cancel appointment (soft cancel via status)
            appointment.Status = "Cancelled";

            await _context.SaveChangesAsync();

            // 🔁 Go back to appointment list
            return RedirectToPage("Index");
        }
    }
}
