using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ApiSpotifyReadme.Services;
using ApiSpotifyReadme.Models;

namespace ApiSpotifyReadme
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Registra o HttpClient para ser injetado
            services.AddHttpClient();

            // Lê as configurações do appsettings.json e garante que elas não sejam nulas
            string clientId = _configuration["SPOTIFY_CLIENT_ID"]
                ?? throw new ArgumentNullException("SPOTIFY_CLIENT_ID está ausente nas configurações");
            string clientSecret = _configuration["SPOTIFY_CLIENT_SECRET"]
                ?? throw new ArgumentNullException("SPOTIFY_CLIENT_SECRET está ausente nas configurações");
            string refreshToken = _configuration["SPOTIFY_REFRESH_TOKEN"]
                ?? throw new ArgumentNullException("SPOTIFY_REFRESH_TOKEN está ausente nas configurações");

            // Usa o construtor do SpotifyConfig para garantir que todas as propriedades estão inicializadas
            var spotifyConfig = new SpotifyConfig(clientId, clientSecret, refreshToken);

            // Registra o SpotifyConfig como um serviço singleton
            services.AddSingleton(spotifyConfig);
            services.AddScoped<SpotifyService>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                // Mantém o mapeamento de rotas para o controlador Spotify
                endpoints.MapControllers();
            });
        }
    }
}
