using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleManager.Model
{
    [Table(nameof(Person))]
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public required string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email address")]
        public string? Email { get; set; }

        public IList<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
