using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController(HttpClient http) : Controller
{
    private readonly HttpClient _http = http;

    private string _categoryApiUrl = "https://localhost:7289/api/Categories";

    private string _contactApiUrl = "https://localhost:7289/api/Contact";

    public async Task<IActionResult> Contact(string category = "")
    {
        var viewModel = new ContactViewModel();

        var categoryResponse = await http.GetAsync(_categoryApiUrl);
        if (categoryResponse.IsSuccessStatusCode)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(await categoryResponse.Content.ReadAsStringAsync());
            if (categories != null)
            {
                viewModel.Categories = categories;
            }
        }

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> SubmitContactForm(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await http.PostAsync(_contactApiUrl, content);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Contact", "Contact");
            }

            //LÄGG TILL VIEWDATA

        }
        return View("Contact", model);
    }
}


