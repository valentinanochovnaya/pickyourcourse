using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                ViewData["Role"] = HttpContext.User.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                
            return View();
        }

    }
}
