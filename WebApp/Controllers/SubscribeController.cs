using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SubscribeController(HttpClient http) : Controller
    {
        private readonly HttpClient _http = http;

        [HttpPost]
        public async Task<IActionResult> Subscribe(subscribeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("https://localhost:7289/api/Subscribe?key=5b56ec08-de9d-464d-aafc-b4c97ab5212d", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["StatusMessage"] = "You are now subscribed!";
                    return RedirectToAction("Home", "Default", "subscribe");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["StatusMessage"] = "This email address is already in use for subscription!";
                    return RedirectToAction("Home", "Default", "subscribe");
                }
            }

            TempData["StatusMessage"] = "Something went wrong!";
            return RedirectToAction("Home", "Default", "subscribe");
        }
    }
}