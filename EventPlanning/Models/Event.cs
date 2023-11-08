using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventPlanning.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        [Range(1, int.MaxValue)]
        public uint MaxRegisteredUsers { get; set; }
        public IList<EventProperty> Properties { get; set; }
        public ICollection<UserDetails> RegisteredUsers { get; set; }
        public class EventProperty
        {
            public Guid Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Value { get; set; }
        }
    }
}
