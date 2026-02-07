using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
