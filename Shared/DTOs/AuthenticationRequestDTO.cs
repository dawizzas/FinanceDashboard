namespace FinanceDashboard.Shared.DTOs;

public class AuthenticationRequestDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}