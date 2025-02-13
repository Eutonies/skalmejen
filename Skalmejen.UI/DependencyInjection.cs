using Skalmejen.UI.Configuration;
using Skalmejen.UI.Pages;

namespace Skalmejen.UI;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", optional: false);
        builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);
        builder.Services.Configure<SpotifyConfiguration>(SpotifyConfiguration.ConfigurationName, builder.Configuration);
        builder.Services.Configure<UIConfiguration>(UIConfiguration.ConfigurationName, builder.Configuration);
        return builder;
    }


    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        return builder;
    }


    public static WebApplication UseServicePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();


        return app;
    }

}
