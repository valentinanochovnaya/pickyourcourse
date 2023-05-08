namespace WebApp.Models;

public class ResetPasswordModel
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Token { get; set; }
}
