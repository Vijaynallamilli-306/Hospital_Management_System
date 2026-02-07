using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Patients
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
        public Patient Patient { get; set; } = null!;

        public IActionResult OnGet(int id)
        {
            Patient = _context.Patients.Find(id)!;
            return Patient == null ? NotFound() : Page();
        }

        public IActionResult OnPost()
        {
            _context.Patients.Update(Patient);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
