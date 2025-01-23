using Microsoft.AspNetCore.Mvc;

namespace OC.Controllers;

public class DasboardController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public string ErrorMessage { get; set; }

    public DasboardController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View("~/Views/Dashboard/Dashboard.cshtml");
    }
}