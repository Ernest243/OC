using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OC.Models.Forms;

namespace OC.Controllers;

public class FormsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FormsController (IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult EXST001()
    {
        return PartialView("~/Views/Forms/exst001.cshtml");
    }

    public async Task<IActionResult> SubmitExst001 (Exst001ViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // If the form data is invalid, redisplay the form with validation errors
            return View("~/Views/Forms/exst001.cshtml", model);
        }

        var client = _httpClientFactory.CreateClient();

        var accessToken = HttpContext.Request.Cookies["accessToken"];

        if (string.IsNullOrEmpty(accessToken))
        {
            ModelState.AddModelError("", "Access token not found.");

            return View("~/Views/Forms/exst001.cshtml", model);
        }

        // Extract email from the access token
        var email = GetEmailFromAccessToken(accessToken);

        // Get the user ID
        var userId = GetUserId(accessToken);

        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError("", "Unable to retrieve user ID.");

            return View("~/Views/Forms/exst001.cshtml", model);
        }

        // Create the request payload in the required format
        var requestPayload = new FormRequest
        {
            FormId = model.FormId,
            UserId = userId,
            FormData = new Dictionary<string, object>
            {
                { "Periode", model.Periode },
                { "DateCVEE", model.DateCvee },
                { "Exportateur", model.Exportateur },
                { "Produit", model.Produit },
                { "CodeDouanier", model.CodeDouanier },
                { "QualitéDuProduit", model.QualiteProduit },
                { "TypeDExportation", model.TypeExportation },
                { "NLicenseEB", model.NumeroLicenseEb },
                { "BanqueIntervenante", model.BanqueIntervenante },
                { "NumeroCVEE", model.NumeroCvee },
                { "TypeDEmballage", model.TypeEmballage },
                { "UStat", model.UStat },
                { "QuantitéDeclarée", model.QuantiteDeclaree },
                { "QuantitéCertifiée", model.QuantiteCertifiee },
                { "QuantitéExpediée", model.QuantiteExpediee },
                { "ValeurFobEnDevise", model.ValeurFobDevise },
                { "ValeurFobUsd", model.ValeurFobUsd },
                { "NumeroLotPretLExport", model.NumeroLotExport },
                { "TypeDeConditionnement", model.TypeConditionnement },
                { "TailleContainers", model.TailleContainers },
                { "NumeroConteneur", model.NumeroConteneur },
                { "LieuDembarquement", model.LieuEmbarquement },
                { "PosteSortie", model.PosteSortie },
                { "AdresseDestinataire", model.AdresseDestinataire },
                { "PaysDestination", model.PaysDestination },
                { "StatutExportateur", model.StatutExportateur },
                { "RegimeExportation", model.RegimeExportation },
                { "SigleMonétaire", model.SigleMonetaire },
                { "FraisOCC", model.FraisOcc }
            }
        };

        // Serialize payload to JSON
        var jsonContent = JsonSerializer.Serialize(requestPayload);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Set the authorization header with the access token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var endpointUrl = "http://localhost:5164/api/Forms/submitForm";

        try
        {
            var response = await client.PostAsync(endpointUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request successful: " + responseBody);
                ModelState.Clear();

                return View("~/Views/Dashboard/Dashboard.cshtml", model);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request failed: "+ errorContent);

                return View("~/Views/Forms/exst001.cshtml");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            throw;
        }
    }

    private string GetUserId(string accessToken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            // Extract the email claim
            var userIdClaim = jwtToken.Claims.FirstOrDefault(
                claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error decoding access token: " + ex.Message);
        }

        return null;
    }

    private string GetEmailFromAccessToken (string accessToken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            // Extract the email claim
            var emailClaim = jwtToken.Claims.FirstOrDefault(
                claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error decoding access token: " + ex.Message);
        }

        return null;
    }
}