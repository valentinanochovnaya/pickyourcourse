using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Controllers;

public class ProfessorController: Controller
{
    public IActionResult ProfessorHomePage(string email)
    {
        return View();
    }
}
