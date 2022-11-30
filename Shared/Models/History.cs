namespace FinanceDashboard.Shared.Models;

public class History
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string CompanySymbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public DateTime SearchTime { get; set; } = DateTime.Now;
}