using Skalmejen.UI.Configuration;
using Skalmejen.UI.Components.Graphics;
using Skalmejen.UI.Pages;
using Skalmejen.Common;

namespace Skalmejen.UI;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", optional: false);
        builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);
        builder.Services.Configure<SpotifyConfiguration>(builder.Configuration.GetSection(SpotifyConfiguration.ConfigurationName));
        builder.Services.Configure<UIConfiguration>(builder.Configuration.GetSection(UIConfiguration.ConfigurationName));
        return builder;
    }


    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents()
           .AddInteractiveServerComponents();
        builder.Services
            .AddHttpContextAccessor();
        return builder;
    }

    public static WebApplication ConfigureRequestPipeline(this WebApplication app)
    {
        app.Use((cont, next) =>
        {
            if (cont.Request.Cookies.TryGetValue(SkalmejenConstants.Cookies.ScreenSize, out var screenSizeStr) && 1 == 2)
            {
                if(Enum.TryParse<SkalmejenScreenSize>(screenSizeStr, out var screenSize) && screenSize == SkalmejenScreenSize.Large)
                {
                    cont.Response.StatusCode = 303;
                    cont.Response.Headers.Location = "https://tv2.dk";
                    return Task.CompletedTask;
                }
            }
            return next(cont);
        });
        app.UseHttpsRedirection();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();


        return app;
    }
}
