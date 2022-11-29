using FinanceDashboard.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Server.Controller;

[Route("api/[controller]")]
[ApiController]
public class YahooFinanceController : ControllerBase
{
    private readonly HttpClient _http;
    public YahooFinanceController(HttpClient http)
    {
        _http = http;
    }

    [HttpGet("search")]
    public async Task<ActionResult<CompanySearchResult>> SearchCompany([FromQuery] string q)
    {
        try {
            var result = await _http.GetFromJsonAsync<CompanySearchResult>("https://query1.finance.yahoo.com/v1/finance/search?q=" + q);
            return Ok(result);
        } catch(Exception e)
        {
            return Ok(e.Message);
        }
    }

    [HttpGet("financialdata")]
    public async Task<ActionResult<FinancialDataResult>> GetFinancialData([FromQuery] string symbol)
    {
        try {
            var result = await _http.GetFromJsonAsync<FinancialDataResult>("https://query1.finance.yahoo.com/v10/finance/quoteSummary/" + symbol + 
                    "?modules=financialData");
            return Ok(result);
        } catch(Exception e)
        {
            return Ok(new FinancialDataResult());
        }
    }
}
