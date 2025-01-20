using System.Text.Json.Serialization;

namespace OC.Models;

public class TokenResponse
{
    public TokenData Token { get; set; }
}

public class TokenData
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonPropertyName("expiresIn")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}