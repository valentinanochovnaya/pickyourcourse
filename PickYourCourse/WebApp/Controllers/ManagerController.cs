using Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Models;

namespace WebApp.Controllers;

public class ManagerController: Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IProfessorInterface _professorInterface;
    private readonly IManagerInterface _managerInterface;
    public ManagerController(IAccountRepository accountRepository, IProfessorInterface professorInterface, IManagerInterface managerInterface)
    {
        _accountRepository = accountRepository;
        _professorInterface = professorInterface;
        _managerInterface = managerInterface;
    }
    public IActionResult ManagerHomePage(string email, int page = 0)
    {
        var manager = this._accountRepository?.GetManager(email);
        var pendingProfessors = this._professorInterface?.GetRequestedProfessors().Result;
        ManagerViewModel? vm = new ManagerViewModel
        {
            FirstName = manager?.FirstName,
            LastName = manager?.LastName,
            Email = manager?.Email,
            RequestedProfessors = pendingProfessors
        };
        int pageSize = 6;
        int count = vm.RequestedProfessors.Count;
        vm.RequestedProfessors = vm.RequestedProfessors.Skip((page) * pageSize).Take(pageSize).ToList();
        this.ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
        this.ViewBag.Page = page;
        return View(vm);
    }
    
    public async Task<IActionResult> LogOut()
    { 
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Index", "Home");
    }
    
    public class RequestModel
    {
        public bool isApproval { get; set; }
        public string ProfessorEmail { get; set; }
        public string ManagerEmail { get; set; }
    }
    
    
    [HttpPost]
    public async Task<IActionResult> ProfessorAction([FromBody] RequestModel requestModel)
    {
        if (requestModel.isApproval)
        {
            await this._managerInterface?.ApproveProfessor(requestModel.ProfessorEmail);

        }
        else
        {
            await this._managerInterface?.RejectProfessor(requestModel.ProfessorEmail);
        }
        
        return RedirectToAction("ManagerHomePage", "Manager", new { email = requestModel.ManagerEmail });
    }
}
