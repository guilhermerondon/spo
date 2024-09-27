using System;
using ApiSpotifyReadme.Models;

namespace ApiSpotifyReadme
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new SpotifyConfig(
                Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID") ?? throw new InvalidOperationException("SPOTIFY_CLIENT_ID não está definido."),
                Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_SECRET") ?? throw new InvalidOperationException("SPOTIFY_CLIENT_SECRET não está definido."),
                Environment.GetEnvironmentVariable("SPOTIFY_REFRESH_TOKEN") ?? throw new InvalidOperationException("SPOTIFY_REFRESH_TOKEN não está definido.")
            );

            Console.WriteLine($"Client ID: {config.ClientId}");
        }
    }
}
