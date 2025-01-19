namespace OC.Models.Forms;

public class Exst001ViewModel
{
    // Statistiques des Marchandises controlées à l'Exportation 
    public string FormId { get; } = "EX-ST001";
    public DateTime Periode { get; set; }
    public DateTime DateCvee { get; set; }
    public string Exportateur { get; set; } = string.Empty;
    public string Produit { get; set; } = string.Empty;
    public string CodeDouanier { get; set; } = string.Empty;
    public string QualiteProduit { get; set; } = string.Empty;
    public string TypeExportation { get; set; } = string.Empty;
    public string NumeroLicenseEb { get; set; } = string.Empty;
    public string BanqueIntervenante { get; set; } = string.Empty;
    public string NumeroCvee { get; set; } = string.Empty;
    public string TypeEmballage { get; set; } = string.Empty;
    public string UStat { get; set; } = string.Empty;
    public decimal QuantiteDeclaree { get; set; }
    public decimal QuantiteCertifiee { get; set; }
    public decimal QuantiteExpediee { get; set; }
    public decimal ValeurFobDevise { get; set; }
    public decimal ValeurFobUsd { get; set; }
    public string NumeroLotExport { get; set; } = string.Empty;
    public string TypeConditionnement { get; set; } = string.Empty;
    public string TailleContainers { get; set; } = string.Empty;
    public string NumeroConteneur { get; set; } = string.Empty;
    public string LieuEmbarquement { get; set; } = string.Empty;
    public string PosteSortie { get; set; } = string.Empty;
    public string AdresseDestinataire { get; set; } = string.Empty;
    public string PaysDestination { get; set; } = string.Empty;
    public string StatutExportateur { get; set; } = string.Empty;
    public string RegimeExportation { get; set; } = string.Empty;
    public string SigleMonetaire { get; set; } = string.Empty;
    public decimal FraisOcc { get; set; }
}