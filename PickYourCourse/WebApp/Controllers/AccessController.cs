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
            var claims = this._accountRepository?.Login(modelLogin.Email, modelLogin.Password, userRole);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            switch (userRole)
            {
                case "Student":
                    return RedirectToAction("StudentHomePage", "Student", new { email = modelLogin.Email }, null);
                case "Professor":
                    return RedirectToAction("ProfessorHomePage", "Professor", new { email = modelLogin.Email }, null);
                case "Manager":
                    return RedirectToAction("ManagerHomePage", "Manager", new {email = modelLogin.Email}, null);
            }
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
