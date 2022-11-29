namespace FinanceDashboard.Shared.Models;

public class CompanySearchResult 
{
    public List<Quote> Quotes { get; set; } = new List<Quote>();
}

public class Quote
{
    public string Symbol { get; set; } = string.Empty;
    public string LongName { get; set; } = string.Empty;
}