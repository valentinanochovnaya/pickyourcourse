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
    public IActionResult ManagerHomePage(string email, int page = 0, int pageManagers = 0)
    {
        var manager = this._accountRepository?.GetManager(email);
        var pendingProfessors = this._professorInterface?.GetRequestedProfessors().Result;
        var professorsWithManagerRights = this._managerInterface?.GetProfessorsWithManagerRights().Result;
        ManagerViewModel? vm = new ManagerViewModel
        {
            FirstName = manager?.FirstName,
            LastName = manager?.LastName,
            Email = manager?.Email,
            RequestedProfessors = pendingProfessors,
            ProfessorsWithManagerRights = professorsWithManagerRights
        };
        int pageSize = 6;
        int pageManagersSize = 4;
        int countRequested = vm.RequestedProfessors.Count;
        vm.RequestedProfessors = vm.RequestedProfessors.Skip((page) * pageSize).Take(pageSize).ToList();
        this.ViewBag.MaxPage = (countRequested / pageSize) - (countRequested % pageSize == 0 ? 1 : 0);
        this.ViewBag.Page = page;
        int countManagers = vm.ProfessorsWithManagerRights.Count;
        vm.ProfessorsWithManagerRights = vm.ProfessorsWithManagerRights.Skip((pageManagers) * pageManagersSize).Take(pageManagersSize).ToList();
        this.ViewBag.MaxPageManagers = (countManagers / pageManagersSize) - (countManagers % pageManagersSize == 0 ? 1 : 0);
        this.ViewBag.PageManagers = pageManagers;
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

    [HttpPost]
    public async Task<IActionResult> AddManager([FromBody] RequestModel requestModel)
    {
        var result = await _managerInterface.ApproveManager(requestModel.ProfessorEmail);
        if (result == 0)
        {
            return NotFound();
        }
        return Ok();
    }
}
