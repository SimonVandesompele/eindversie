using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Cyb.Ui.Mvc.Models
{
    public class RegisterModel
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}
