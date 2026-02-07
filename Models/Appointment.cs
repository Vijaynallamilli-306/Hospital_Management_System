using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HospitalManagementSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ValidateNever]
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }

        [ValidateNever]
        public Patient Patient { get; set; } = null!;

        // ✅ NEW
        [Required]
        public string Status { get; set; } = "Booked";
    }
}
