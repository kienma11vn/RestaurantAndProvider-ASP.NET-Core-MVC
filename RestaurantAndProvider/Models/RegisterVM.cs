using System.ComponentModel.DataAnnotations;

namespace RestaurantAndProvider.Models
{
    public class RegisterVM
    {
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorrect!")]
        public string? ConfirmPassword { get; set; }
    }
}
