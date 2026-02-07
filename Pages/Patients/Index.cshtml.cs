using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Patients
{
    [Authorize(Roles = "Admin,Receptionist")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Patient> Patients { get; set; } = new List<Patient>();

        public void OnGet()
        {
            Patients = _context.Patients.ToList();
        }
    }
}
