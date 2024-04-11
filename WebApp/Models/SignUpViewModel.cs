using System.ComponentModel.DataAnnotations;
using WebApp.Helpers;

namespace WebApp.Models;

public class SignUpViewModel
{ 
    [Required(ErrorMessage = "You must enter a first name")]
    [MinLength(2, ErrorMessage = "A valid first name is required")]
    [Display(Name = "First name", Prompt = "Enter your first name")]
    public string FirstName { get; set; } = null!;

    

    [Required(ErrorMessage = "You must enter a last name")]
    [MinLength(2, ErrorMessage = "A valid last name is required")]
    [Display(Name = "Last name", Prompt = "Enter your last name")]
    public string LastName { get; set; } = null!;



    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$", ErrorMessage = "A valid email address is required")]
    [Required(ErrorMessage = "You must enter an email address")]
    [Display(Name = "Email address", Prompt = "Enter your email adress")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;



    [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?!.*\s).{8,}$", ErrorMessage = "A valid password is required")]
    [Required(ErrorMessage = "You must enter a password")]
    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;



    [Compare(nameof(Password), ErrorMessage = "Passwords dont't match")]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Display(Name = "Confirm password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;



    [CheckboxRequired(ErrorMessage = "You must accept the Terms and Conditions")]
    public bool TermsAndConditions { get; set; }

}


/*
 
[MinLength(8, ErrorMessage = "A valid password is required")]
[DataType(DataType.Password)]
 

[Compare(nameof(Password), ErrorMessage = "Passwrod don't match")]
[DataType(DataType.Password)] 
 
[Display(Name = "I agree to the terms & Conditions")]
[Required(ErrorMessage = "You must accept the terms and conditions")]

 */