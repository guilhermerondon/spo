using Microsoft.AspNetCore.Mvc;
using ApiSpotifyReadme.Models; 
namespace ApiSpotifyReadme.Controllers 
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new MusicViewModel
            {
                Image = "url_imagem", 
                SongName = "Nome da Música",
                ArtistName = "Nome do Artista",
                SongURI = "link_da_musica",
                ArtistURI = "link_do_artista"
            };

            return View(model);
        }
    }
}
