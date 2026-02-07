using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Pages.Doctors
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doctor Doctor { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Doctor = await _context.Doctors.FindAsync(id);

            if (Doctor == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existingDoctor =
                await _context.Doctors
                              .AsNoTracking()
                              .FirstOrDefaultAsync(d => d.Id == Doctor.Id);

            if (existingDoctor == null)
                return NotFound();

            // ✅ keep existing UserId
            Doctor.UserId = existingDoctor.UserId;

            _context.Attach(Doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
