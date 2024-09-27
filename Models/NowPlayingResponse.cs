namespace ApiSpotifyReadme.Models
{
    public class NowPlayingResponse
    {
        public string Name { get; set; }
        public string Artist { get; set; }

        public NowPlayingResponse(string name, string artist)
        {
            Name = name;
            Artist = artist;
        }
    }
}
