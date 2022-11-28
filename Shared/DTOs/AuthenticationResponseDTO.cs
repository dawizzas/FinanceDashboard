namespace FinanceDashboard.Shared.DTOs;

public class AuthenticationResponseDTO
{
    public bool isSuccessful { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Errors { get; set; } = string.Empty;
}