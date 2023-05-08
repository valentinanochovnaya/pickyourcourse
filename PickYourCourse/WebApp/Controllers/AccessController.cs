using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Models;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.Services;

namespace WebApp.Controllers;

public class AccessController : Controller
{
    private readonly IAccountRepository _accountRepository;
    protected Dictionary<String, String> tokens = new Dictionary<string, string>();
    
    public AccessController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    // GET
    [HttpGet]
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        return View();
    }
    
    [HttpPost]
    public async  Task<IActionResult> Login(VMLogin modelLogin)
    {
        String? userRole = _accountRepository?.GetRole(modelLogin.Email, modelLogin.Password);

        if (userRole != "")
        {
            var claims = this._accountRepository?.Login(modelLogin.Email, modelLogin.Password, userRole);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            switch (userRole)
            {
                case "Student":
                    return RedirectToAction("StudentHomePage", "Student", new { email = modelLogin.Email }, null);
                case "Professor":
                    return RedirectToAction("ProfessorHomePage", "Professor", new { email = modelLogin.Email }, null);
                case "Manager":
                    return RedirectToAction("ManagerHomePage", "Manager", new {email = modelLogin.Email}, null);
            }
        }

        ViewData["ValidateMessage"] = "User not found";
        return View();
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
    {
        var user = _accountRepository.GetUser(forgotPasswordModel.Email);
        if (user != null)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            
            /* REPLACE TOKEN; STORE TOKEN IN DB; STORE TIMESTAMP IN DB */
            _accountRepository.AddTokenToUser(user.Email, token);
            var callbackUrl = UrlHelperExtensions.Action(Url, "ResetPassword", "Access", new { code = token });
            Console.WriteLine("CallbackUrl");
            Console.WriteLine(callbackUrl);

            string fromMail = "ggwp22022000@gmail.com";
            string fromPassword = "tdgdvwqaxpydqwbz";
            
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Subject";
            message.Body = "Please login " + "https://localhost:7034" + callbackUrl;
            message.To.Add(user.Email);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port=587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };
            
            smtp.Send(message);
        }
        return RedirectToAction("ForgotPasswordConfirmation");
    }
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }
    
    public ActionResult ResetPassword(string code)
    {
        var model = new ResetPasswordModel { Token = code };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        if (resetPasswordModel.Password == resetPasswordModel.ConfirmPassword)
        {
            _accountRepository.UpdateUserPassword(resetPasswordModel.Token, resetPasswordModel.Password);
        }

        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> LogOut()
    { 
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Index", "Home");
    }
}
