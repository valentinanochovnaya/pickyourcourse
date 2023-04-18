using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Controllers;

public class ManagerController: Controller
{
    public IActionResult ManagerHomePage(string email)
    {
        return View();
    }
}
