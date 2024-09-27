public class SpotifyConfig
{
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RefreshToken { get; }

    public SpotifyConfig(string clientId, string clientSecret, string refreshToken)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        RefreshToken = refreshToken;
    }
}
