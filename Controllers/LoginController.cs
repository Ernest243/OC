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


    /*public async Task<IActionResult> Login(LoginRequest model)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var logRequest = new
            {
                Email = model.Email,
                Password = model.Password
            };

            var response = await client.PostAsJsonAsync("http://localhost:5164/api/Auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Register token in cookies to be retrieved later

                // Redirect the user to the Dashboard

                return RedirectToPage("");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {errorMessage}");
                ErrorMessage = "Invalid credentials or API error.";
                return View();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            ErrorMessage = "An unexpected error occurred.";
            return View();
        }
    }*/

    public async Task<IActionResult> Login(LoginRequest requestModel)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://localhost:5164/api/Auth/login", requestModel);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenData>(jsonResponse);

                // Set the access token in an HTTP-only cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // For HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddSeconds(tokenResponse!.ExpiresIn)
                };

                Response.Cookies.Append("AccessToken", tokenResponse.AccessToken, cookieOptions);
                Response.Cookies.Append("RefreshToken", tokenResponse.RefreshToken, cookieOptions);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

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