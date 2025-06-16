using System.ComponentModel.DataAnnotations;

namespace OfficeApp.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email or Username")]
    public string Username { get; set; }
    public string Password { get; set; }
}
