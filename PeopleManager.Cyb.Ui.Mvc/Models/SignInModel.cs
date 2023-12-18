using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Cyb.Ui.Mvc.Models
{
    public class SignInModel
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        public required bool RememberMe { get; set; }
    }
}
