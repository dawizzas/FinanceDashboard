using Microsoft.AspNetCore.Identity;

namespace FinanceDashboard.Shared.Models;

public class User : IdentityUser
{
    public List<Company> PortfolioCompanies { get; set; } = new List<Company>();
}