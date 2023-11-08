using System.ComponentModel.DataAnnotations;

namespace EventPlanning.Models
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public ICollection<Event> RegisteredEvents { get; set; }
    }
}
