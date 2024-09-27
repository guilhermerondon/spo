using System;
using ApiSpotifyReadme.Models; 

namespace ApiSpotifyReadme
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new SpotifyConfig
            {
                ClientId = "seu_client_id", 
                ClientSecret = "seu_client_secret",
                RefreshToken = "seu_refresh_token" 
            };

            if (config.ClientId != null)
            {
                // Use config.ClientId com segurança
                Console.WriteLine($"Client ID: {config.ClientId}");
            }
            else
            {
                Console.WriteLine("Client ID não foi configurado.");
            }

        }
    }
}
