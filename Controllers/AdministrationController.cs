using Microsoft.AspNetCore.Mvc;

namespace OC.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult Nouveau()
        {
            return View("~/Views/Administration/Nouveau.cshtml");
        }
    }
}
