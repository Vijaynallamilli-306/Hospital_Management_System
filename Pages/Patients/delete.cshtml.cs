using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Patients
{
    [Authorize(Roles = "Admin,Receptionist")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; } = null!;

        public IActionResult OnGet(int id)
        {
            Patient = _context.Patients.Find(id)!;

            if (Patient == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(Patient);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
