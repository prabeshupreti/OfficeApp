
using System.ComponentModel.DataAnnotations;

namespace OfficeApp.ViewModels;

public class UpdateEmployeeViewModel
{
    public int Id { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = $"First name is required.")]
    [MinLength(2, ErrorMessage = "First name connot be just a single character.")]
    [MaxLength(35, ErrorMessage = "First name cannot be more than 35 characters.")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Last name is required.")]
    [MinLength(2, ErrorMessage = "Last name connot be just a single character.")]
    [MaxLength(35, ErrorMessage = "Last name cannot be more than 35 characters.")]
    public string LastName { get; set; }

    [Display(Name = "Contact Number")]
    [Required(ErrorMessage = "Contact number is required.")]
    [Length(10, 10, ErrorMessage = "Contact number has to be exactly of 10 digits.")]
    public string Contact { get; set; }

    [Display(Name = "Address")]
    public string? Address { get; set; }

    public string? PhotoPath { get; set; }

    public IFormFile Photo { get; set; }
}
