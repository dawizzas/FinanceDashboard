namespace FinanceDashboard.Shared.Models;

public class Company
{
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CurrentPrice { get; set; } = string.Empty;
    public string RevenueGrowth { get; set; } = string.Empty;
    public string ProfitMargins { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
}