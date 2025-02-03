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

    public async Task<IActionResult> SubmitExst001(Exst001ViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // If the form data is invalid, redisplay the form with validation errors
            return View("~/Views/Forms/exst001.cshtml", model);
        }

        var client = _httpClientFactory.CreateClient();

        // Create the request payload in the required format
        var requestPayload = new FormRequest
        {
            FormId = model.FormId,
            UserId = "315214ed-2d8f-4149-8bbf-e69e24d175f1",
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

        var accessToken = HttpContext.Session.GetString("accessToken");
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
                Console.WriteLine("Request failed: " + errorContent);
                return View("~/Views/Forms/exst001.cshtml");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine("An error occured: " + ex.Message);
            throw;
        }
    }
}