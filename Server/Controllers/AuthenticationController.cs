using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using FinanceDashboard.Shared.Models;
using FinanceDashboard.Shared.DTOs;

namespace BlazorCompleteAuth.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponseDTO>> Register(RegistrationRequestDTO request) 
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        User user = new User
        {
            UserName = request.Username
        };

        await _userManager.CreateAsync(user, request.Password);
        if (!await _roleManager.RoleExistsAsync(request.Role))
            await _roleManager.CreateAsync(new IdentityRole(request.Role));
        
        await _userManager.AddToRoleAsync(user, request.Role);

        return Ok(new RegistrationResponseDTO { isSuccessful = true, User = user });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDTO>> Login(AuthenticationRequestDTO request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null) return new AuthenticationResponseDTO { isSuccessful = false, Errors = "The email or password is incorrect" };

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (!result.Succeeded) return new AuthenticationResponseDTO { isSuccessful = false, Errors = "The email or password is incorrect" };

        return new AuthenticationResponseDTO { isSuccessful = true, Token = await CreateToken(user) };
    }

    private async Task<string> CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
        foreach(var role in await _userManager.GetRolesAsync(user))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:SecretKey").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
