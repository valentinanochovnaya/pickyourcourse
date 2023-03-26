using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Models;
using Application.Interfaces;
using Persistence.Services;

namespace WebApp.Controllers;

public class AccessController : Controller
{
    private readonly IAccountRepository _accountRepository;
    
    public AccessController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    // GET
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        return View();
    }
    
    [HttpPost]
    public async  Task<IActionResult> Login(VMLogin modelLogin)
    {
        String? userRole = _accountRepository?.GetRole(modelLogin.Email, modelLogin.Password);

        if (userRole != "")
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, modelLogin.Email), new Claim("Role", userRole)
            };
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true, IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        ViewData["ValidateMessage"] = "User not found";
        return View();
    }
    
    
    public async Task<IActionResult> LogOut()
    { 
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Index", "Home");
    }
}
