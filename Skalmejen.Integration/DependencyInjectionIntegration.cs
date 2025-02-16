using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skalmejen.UI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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


    private static void ConfigureHttpClients(this WebApplicationBuilder builder)
    {

    }
}
