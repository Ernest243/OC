namespace OC.Models.Forms;

public class FormRequest
{
    public string FormId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public Dictionary<string, object> FormData { get; set; } = new Dictionary<string, object>();
}