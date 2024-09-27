using Microsoft.AspNetCore.Mvc;
using ApiSpotifyReadme.Services; // Ajuste para o namespace correto
using System.Threading.Tasks;

namespace ApiSpotifyReadme.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotifyController : ControllerBase
    {
        private readonly SpotifyService _spotifyService;

        public SpotifyController(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("now-playing")]
        public async Task<IActionResult> GetNowPlaying()
        {
            var nowPlaying = await _spotifyService.GetNowPlayingAsync();
            if (nowPlaying == null)
                return NotFound("Nenhuma música está tocando no momento.");
            return Ok(nowPlaying);
        }

        // Novo endpoint para obter a configuração do Spotify da Vercel
        [HttpGet("config")]
        public async Task<IActionResult> GetSpotifyConfig()
        {
            try
            {
                var config = await _spotifyService.GetSpotifyConfigFromVercelAsync();
                return Ok(config);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter configuração do Spotify: {ex.Message}");
            }
        }
    }
}
