using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountBasicInfo
{
    [Required(ErrorMessage = "You must enter a first name")]
    //[MinLength(2, ErrorMessage = "A valid first name is required")]
    [Display(Name = "First name", Prompt = "Enter your first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a last name")]
    //[MinLength(2, ErrorMessage = "A valid last name is required")]
    [Display(Name = "Last name", Prompt = "Enter your last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter an email address")]
    [Display(Name = "Email address", Prompt = "Enter your email adress")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone", Prompt = "Enter your phone number")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Bio (Optional)", Prompt = "Add a short bio...")]
    public string? Biography { get; set; }
}
