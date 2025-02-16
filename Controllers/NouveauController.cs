using Microsoft.AspNetCore.Mvc;

namespace OC.Controllers;

public class NouveauController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public NouveauController (IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Render the form
    public IActionResult Nouveau()
    {
        return PartialView("~/Views/Administration/Nouveau.cshtml");
    }
}