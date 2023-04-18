using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class RegisterViewModel
{
    [Display(Name = " ")]
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }
    [Display(Name = " ")]
    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }
    [Display(Name = " ")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Display(Name = " ")]
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }
    [Display(Name = " ")]
    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    [Display(Name = " ")]
    [Required(ErrorMessage = "Course Year is required")]
    [Range(1, 4, ErrorMessage = "Course Year must be between 1 and 4")]
    public int Year { get; set; }
    [Display(Name = " ")]
    [Required(ErrorMessage = "Role is required")]
    public Roles Role { get; set; }
}
public enum Roles {
    Student,
    Professor,
}
