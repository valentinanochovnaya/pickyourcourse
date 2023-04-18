using Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class StudentController: Controller
{
    private readonly IAccountRepository _accountRepository;

    public StudentController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public IActionResult StudentHomePage(string email)
    {
        var student = this._accountRepository?.GetStudent(email);
        StudentViewModel? vm = new StudentViewModel
        {
            FirstName = student?.FirstName,
            LastName = student?.LastName,
            Email = student?.Email,
            Year = student?.Year,
            Score = student?.Score,
            CoursesNumber = student?.CoursesNumber,
            StudentCourses = student?.StudentCourses,
            StudentCoursesNumber = student?.StudentCourses?.Count,
            Notifications = student?.Notifications,
        };
        vm.CoursesNumber ??= 0;
        vm.StudentCoursesNumber ??= 0;
        return View(vm);
    }
    
    public async Task<IActionResult> LogOut()
    { 
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Notifications()
    {
        return View();
    }
    
    public IActionResult ChooseCourseGeneral()
    {
        return View();
    }
}
