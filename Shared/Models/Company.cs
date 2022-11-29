namespace FinanceDashboard.Shared.Models;

public class Company
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CurrentPrice { get; set; } = string.Empty;
    public string RevenueGrowth { get; set; } = string.Empty;
    public string ProfitMargins { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
}