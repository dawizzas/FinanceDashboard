using System.Security.Claims;
using FinanceDashboard.Server.Data;
using FinanceDashboard.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Server.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPut("addcompany")]
    public async Task<ActionResult> UpdateCompanyPortfolio(Company CompanyToAdd)
    {
       try {
           User currentUser = _context.Users.Include(u => u.PortfolioCompanies).SingleOrDefault<User>(u => u.UserName == User.Claims.FirstOrDefault().Value);

           if(currentUser.PortfolioCompanies.Where(c => c.Symbol.ToLower() == CompanyToAdd.Symbol.ToLower()).Count() > 0)
            {
                currentUser.PortfolioCompanies.First(c => c.Symbol.ToLower() == CompanyToAdd.Symbol.ToLower()).Quantity = 
                    currentUser.PortfolioCompanies.First(c => c.Symbol.ToLower() == CompanyToAdd.Symbol.ToLower()).Quantity + CompanyToAdd.Quantity;
                currentUser.PortfolioCompanies.First(c => c.Symbol.ToLower() == CompanyToAdd.Symbol.ToLower()).CollectiveBuyingPrice = 
                    currentUser.PortfolioCompanies.First(c => c.Symbol.ToLower() == CompanyToAdd.Symbol.ToLower()).CollectiveBuyingPrice + 
                    CompanyToAdd.CollectiveBuyingPrice;
            } else {
                currentUser.PortfolioCompanies.Add(CompanyToAdd);
            }
           
            _context.Entry(currentUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

       } catch(Exception e)
       {
           return Ok(e.Message);
       }
    }

    [HttpPut("removecompany")]
    public async Task<ActionResult> RemoveCompanyPortfolio(Company CompanyToRemove)
    {
       try {
           User currentUser = _context.Users.Include(u => u.PortfolioCompanies).SingleOrDefault<User>(u => u.UserName == User.Claims.FirstOrDefault().Value);

           currentUser.PortfolioCompanies.Remove(currentUser.PortfolioCompanies.Find(c => c.Symbol == CompanyToRemove.Symbol));

            _context.Entry(currentUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
       } catch(Exception e)
       {
           return Ok(e.Message);
       }
    }

    [HttpGet("getportfoliocompanies")]
    public async Task<ActionResult<List<Company>>> GetAllPortfolioCompanies()
    {
        try {
            User currentUser = _context.Users.Include(u => u.PortfolioCompanies).SingleOrDefault<User>(u => u.UserName == User.Claims.FirstOrDefault().Value);
            return Ok(currentUser.PortfolioCompanies);
        } catch(Exception e)
        {
            return Ok(e.Message);
        }
    }

    [HttpGet("getallusers"), Authorize(Roles = "admin")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        try {
            List<User> allNonAdminUsers = await _context.Users.Where(u => u.UserName != "admin").ToListAsync();
            return Ok(allNonAdminUsers);
        } catch(Exception e)
        {
            return Ok(e.Message);
        }
    }
}