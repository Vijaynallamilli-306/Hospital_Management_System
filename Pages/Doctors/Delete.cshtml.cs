using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Doctors
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doctor Doctor { get; set; } = null!;

        public IActionResult OnGet(int id)
        {
            Doctor = _context.Doctors.Find(id)!;
            if (Doctor == null) return NotFound();
            return Page();
        }

        public IActionResult OnPost()
        {
            _context.Doctors.Remove(Doctor);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
