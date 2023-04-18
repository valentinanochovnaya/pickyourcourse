using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class StudentController: Controller
{
    public IActionResult StudentHomePage(string email)
    {
        return View();
    }
}
