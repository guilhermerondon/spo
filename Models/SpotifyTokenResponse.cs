public class SpotifyTokenResponse
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public string RefreshToken { get; set; }

    public SpotifyTokenResponse(string accessToken, string tokenType, string refreshToken)
    {
        AccessToken = accessToken;
        TokenType = tokenType;
        RefreshToken = refreshToken;
    }
}
