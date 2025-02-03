using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OC.Models;

namespace OC.Controllers;

public class LoginController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public string ErrorMessage { get; set; }
    public LoginController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View("~/Views/Shared/Login.cshtml");
    }

    public async Task<IActionResult> Login(LoginRequest requestModel)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://localhost:5164/api/Auth/login", requestModel);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonResponse, options);

                // Set the access token in an HTTP-only cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // For HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddSeconds(tokenResponse!.Token.ExpiresIn)
                };

                Response.Cookies.Append("AccessToken", tokenResponse.Token.AccessToken, cookieOptions);
                Response.Cookies.Append("RefreshToken", tokenResponse.Token.RefreshToken, cookieOptions);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenResponse.Token.AccessToken);

                return View("~/Views/Dashboard/Dashboard.cshtml");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ErrorMessage = "Invalid credentials or API error.";
                return View();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occured during login.";
            return View();
        }
    }
}