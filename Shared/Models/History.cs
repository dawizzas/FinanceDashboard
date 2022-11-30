namespace FinanceDashboard.Shared.Models;

public class History
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public Company Company { get; set; } = new Company();
    public DateTime SearchTime { get; set; } = DateTime.Now;
}