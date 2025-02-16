namespace OC.Models.Administration;

public class NewEmployeeModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Matricule { get; set; } = string.Empty;
    public string Sexe { get; set; } = string.Empty;
    public DateTime DOB { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Sifa { get; set; } = string.Empty;
    public string LevelStudy { get; set; } = string.Empty;
    public string Domaine { get; set; } = string.Empty;
    public DateTime HireDate { get; set; } 
    public string Grade { get; set; } = string.Empty;
    public string DutyStation { get; set; } = string.Empty;
    public string FonctionEngagement { get; set; } = string.Empty;
    public DateTime LastAffectation { get; set; }
    public string FonctionActuelle { get; set; } = string.Empty;
    public string PosteAffectation { get; set; } = string.Empty;
    public string Anciennete { get; set; } = string.Empty;
    public string GradeActuelle { get; set; } = string.Empty;
    public string ProvinceOrigine { get; set; } = string.Empty;
}