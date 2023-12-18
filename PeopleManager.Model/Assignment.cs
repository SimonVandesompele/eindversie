using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Model
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public int? AssignedToId { get; set; }
        
        public Person? AssignedTo { get; set; }
    }
}
