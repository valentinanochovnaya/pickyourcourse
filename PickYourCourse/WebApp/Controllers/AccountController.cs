using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers;

public class AccountController: Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AccountController(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerVM)
    {
        if (!ModelState.IsValid)
        {
            return View(new RegisterViewModel());
        }
        if (registerVM.Role == 0)
        {
            await _accountRepository.RegisterStudent(_mapper.StudentVMToStudent(registerVM));
        }
        else
        {
            await _accountRepository.RegisterProfessor(_mapper.ProfessorVNToProfessor(registerVM));
        }
        ViewBag.message = "The user is saved successfully"; 
        return View("RegisterCompleted");
    }
}
