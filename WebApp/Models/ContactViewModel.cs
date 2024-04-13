using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ContactViewModel
    {

        [Required(ErrorMessage = "You must enter your fullname")]
        [MinLength(2, ErrorMessage = "A valid name is required")]
        [Display(Name = "Full Nam", Prompt = "Enter your full name")]
        public string FullName { get; set; } = null!;

        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$", ErrorMessage = "A valid email address is required")]
        [Required(ErrorMessage = "You must enter an email address")]
        [Display(Name = "Email address", Prompt = "Enter your email adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = null!;

        [Display(Name = "Service (optional)", Prompt = "Choose the service you are interested in")]
        public string? Service { get; set; }

        [Required(ErrorMessage = "You must enter a text")]
        [MinLength(10, ErrorMessage = "You must enter min. 10 letters")]
        [Display(Name = "Message", Prompt = "Enter your message here...")]
        public string Message { get; set; } = null!;


        public IEnumerable<Category>? Categories { get; set; } 
    }
}
