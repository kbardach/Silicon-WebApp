using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.Controllers
{
    //[Authorize]
    public class DefaultController(DataContext context) : Controller
    {
        private readonly DataContext _context = context;

        [Route("/")]
        public IActionResult Home()
        {
            return View();
        }

        #region Features
        [Route("/features")]
        public IActionResult Features()
        {
            return RedirectToAction("Home", "Default", "features");
        }
        #endregion

    }
}


