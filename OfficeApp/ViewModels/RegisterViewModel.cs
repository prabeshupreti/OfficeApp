using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OfficeApp.ViewModels;

public class RegisterViewModel
{
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be of 8 length.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).*$",
        ErrorMessage = "The field must contain at least one uppercase letter, one lowercase letter, and one number.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage ="Password and Confirm Password do not match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
