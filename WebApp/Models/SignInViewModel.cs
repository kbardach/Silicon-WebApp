using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignInViewModel
{

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



    [Display(Name = "I agree to the terms & Conditions")]
    public bool RememberMe { get; set; }

}
