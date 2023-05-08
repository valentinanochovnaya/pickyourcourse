using System.Security.Claims;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProfessorController: Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IProfessorInterface _professorInterface;
    private readonly IMapper _mapper;
    public ProfessorController(IAccountRepository accountRepository, IProfessorInterface professorInterface, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _professorInterface = professorInterface;
        _mapper = mapper;
    }
    public IActionResult ProfessorHomePage(string email, int index = 0)
    {
        this.ViewBag.index = index;
        var professor = this._accountRepository?.GetProfessor(email);
        var courses = _professorInterface.GetProfessorCourses(professor).Result;
        int pagination_size = 7;
        bool NeedToShowNextPage = false;
        /* LINQ.Skip for pagination; Rename func method to Index;  */
        if (courses.Count < pagination_size * index || courses.Count == 0)
        {
            courses = new List<Course>();
        } else if (courses.Count < pagination_size * index + pagination_size)
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
        ProfessorViewModel? vm = new ProfessorViewModel
        {
            FirstName = professor?.FirstName,
            LastName = professor?.LastName,
            Email = professor?.Email,
            ProfessorCourses = courses,
            IsActivated = professor?.IsActivated
        };
        return View(vm);
    }
    [HttpGet]
    public IActionResult CreateNewCourse()
    {
        var email = HttpContext.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        var professor = _accountRepository.GetProfessor(email);
        
        if (!professor.IsActivated)
        {
            return Content("<script language='javascript' type='text/javascript'>alert('Access denied. Account not confirmed');</script>");
        }
        ProfessorViewModel? vm = new ProfessorViewModel
        {
            FirstName = professor?.FirstName,
            LastName = professor?.LastName,
            Email = professor?.Email
        };
        return View(vm);
    }
    
    [HttpPost]
    public IActionResult CreateNewCourseAction(ProfessorViewModel professorModel)
    {
        var email = HttpContext.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        var professor = _accountRepository.GetProfessor(email);
        
        if (!professor.IsActivated)
        {
            return Content("<script language='javascript' type='text/javascript'>alert('Access denied. Account not confirmed');</script>");
        }

        var course = professorModel.NewCourse;
        course.ProfessorId = professor.Id;
        Console.WriteLine("CreateNewCourseAction");
        _accountRepository.AddCourse(_mapper.CourseVMToCourse(course));
        
        return RedirectToAction("ProfessorHomePage", "Professor", new { email = email, index = 0 });
    }
}
