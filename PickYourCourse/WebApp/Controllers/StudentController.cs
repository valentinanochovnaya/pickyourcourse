using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class StudentController: Controller
{
    public IActionResult StudentHomePage(string email)
    {
        return View();
    }
    
    public async Task<IActionResult> LogOut()
    { 
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Index", "Home");
    }
}
