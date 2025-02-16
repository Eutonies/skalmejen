using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Skalmejen.Common.Music;
using Skalmejen.Integration.Music;
using Skalmejen.UI.Configuration;
using SpyOff.Infrastructure.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Integration;
public static class DependencyInjectionIntegration
{
    public static WebApplicationBuilder AddIntegrationConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SkalmejenIntegrationConfiguration>(builder.Configuration.GetSection(SkalmejenIntegrationConfiguration.ConfigurationName));
        return builder;
    }

    public static WebApplicationBuilder AddIntegration(this WebApplicationBuilder builder)
    {
        builder.ConfigureHttpClients();
        builder.Services.AddScoped<ISpotifyPlayer, SpotifyPlayer>();
        return builder;
    }


    private static void ConfigureHttpClients(this WebApplicationBuilder builder)
    {

        builder.Services.AddHttpClient<ISpotifyApiClient, SpotifyApiClient>()
            .ConfigurePrimaryHttpMessageHandler(prov => CreateMessageHandler(prov))
            .ConfigureHttpClient((prov, client) => ConfigureHttpClientAction(prov, client));
    }


    private static HttpMessageHandler CreateMessageHandler(IServiceProvider provider)
    {
        var handler = new HttpClientHandler();
        var conf = provider.GetRequiredService<IOptions<SkalmejenIntegrationConfiguration>>().Value;
        var token = conf.Spotify.DeveloperToken;
        return handler;

    }


    private static void ConfigureHttpClientAction(IServiceProvider provider, HttpClient httpClient)
    {
        var conf = provider.GetRequiredService<IOptions<SkalmejenIntegrationConfiguration>>().Value;
        var baseAddress = conf.Spotify.BaseUrl;
        httpClient.BaseAddress = new Uri(baseAddress);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", conf.Spotify.DeveloperToken);

 
    }

}
