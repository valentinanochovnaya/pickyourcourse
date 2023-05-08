using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class StudentController: Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IStudentInterface _studentInterface;

    public StudentController(IAccountRepository accountRepository, IStudentInterface studentInterface)
    {
        _accountRepository = accountRepository;
        _studentInterface = studentInterface;
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
    
    public IActionResult ChooseCourseGeneral(string email, int index = 0)
    {
        var student = this._accountRepository?.GetStudent(email);
        var courses = this._studentInterface?.GetStudentCourses(student).Result;
        this.ViewBag.index = index;
        int pagination_size = 9;
        bool NeedToShowNextPage = false;
        if (courses.Count < pagination_size * index || courses.Count == 0)
        {
            courses = new List<Course>();
        } else if (courses.Count <= pagination_size * index + pagination_size)
        {
            courses = courses.GetRange(index * pagination_size, courses.Count - (index * pagination_size));
        }
        else
        {
            courses = courses.GetRange(index * pagination_size, pagination_size);
            if (courses.Count - (index * pagination_size) > 0)
            {
                NeedToShowNextPage = true;
            }
        }
        this.ViewBag.NeedToShowNextPage = NeedToShowNextPage;
        var vm = new ChooseCourseGeneralVM {StudentCourses = courses, Email = email};
        Console.WriteLine(email);
        return View(vm);
    }
}
