using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Shared.DTOs;

public class RegistrationRequestDTO
{
    public string Username { get; set; } = string.Empty;
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least one special character," + 
            "one upper case character, one lower case character, and one number, and must be at least 8 characters long.")]
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Role { get; set; } = "user";
}