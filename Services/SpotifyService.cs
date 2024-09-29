using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApiSpotifyReadme.Models;
using System.Collections.Generic; // Para KeyValuePair

namespace ApiSpotifyReadme.Services
{
    public class SpotifyService
    {
        private readonly SpotifyConfig _spotifyConfig;
        private readonly HttpClient _httpClient;

        public SpotifyService(SpotifyConfig spotifyConfig, HttpClient httpClient)
        {
            _spotifyConfig = spotifyConfig;
            _httpClient = httpClient;
        }

        public async Task<string> GetNewAccessToken()
        {
            if (string.IsNullOrEmpty(_spotifyConfig.RefreshToken) || 
                string.IsNullOrEmpty(_spotifyConfig.ClientId) || 
                string.IsNullOrEmpty(_spotifyConfig.ClientSecret))
            {
                throw new InvalidOperationException("Configurações do Spotify estão faltando.");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

            var requestBody = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", _spotifyConfig.RefreshToken),
                new KeyValuePair<string, string>("client_id", _spotifyConfig.ClientId),
                new KeyValuePair<string, string>("client_secret", _spotifyConfig.ClientSecret)
            });

            request.Content = requestBody;

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<SpotifyTokenResponse>(responseBody);

                return tokenResponse?.AccessToken ?? throw new InvalidOperationException("Falha ao obter o token de acesso.");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao obter o token de acesso.", ex);
            }
        }

        public async Task<NowPlayingResponse> GetNowPlayingAsync()
        {
            var accessToken = await GetNewAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var response = await _httpClient.GetAsync("https://api.spotify.com/v1/me/player/currently-playing");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var nowPlayingResponse = JsonConvert.DeserializeObject<NowPlayingResponse>(responseBody);

                return nowPlayingResponse ?? throw new InvalidOperationException("Falha ao obter informações sobre a reprodução atual.");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao obter a reprodução atual.", ex);
            }
        }

        // Novo método para chamar a API Vercel
        public async Task<string> GetSpotifyConfigFromVercelAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://seu-username.vercel.app/api/spotify");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody; // Você pode processar a resposta conforme necessário
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao obter a configuração do Spotify da Vercel.", ex);
            }
        }
    }
}
