namespace FinanceDashboard.Shared.Models;

public class FinancialDataResult
{
    public QuoteSummary QuoteSummary { get; set; } = new QuoteSummary();
}

public class QuoteSummary
{
    public List<Result>? Result { get; set; }
}

public class Result
{
    public FinancialData FinancialData { get; set; } = new FinancialData();
}

public class FinancialData
{
    public Value CurrentPrice { get; set; } = new Value();
    public Value RevenueGrowth { get; set; } = new Value();
    public Value ProfitMargins { get; set; } = new Value();
    public string RecommendationKey { get; set; } = string.Empty;
}

public class Value
{
    public string fmt { get; set; } = string.Empty;
}