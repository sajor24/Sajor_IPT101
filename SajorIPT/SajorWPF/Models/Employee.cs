using System.ComponentModel.DataAnnotations;

namespace SajorWPF.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Position { get; set; } = string.Empty;
    }
}
