using FinanceDashboard.Shared.Models;

namespace FinanceDashboard.Shared.DTOs;

public class RegistrationResponseDTO
{
    public bool isSuccessful { get; set; }
    public User? User { get; set; }
    public string Errors { get; set; } = string.Empty;
}