using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql.NameTranslation;
using Skalmejen.Persistence.Configuration;
using Skalmejen.Persistence.Contest.Model;
using Skalmejen.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Persistence;
public static class DependencyInjectionPersistence
{
    public static WebApplicationBuilder ConfigurePersistence(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SkalmejenPersistenceConfiguration>(builder.Configuration.GetSection(SkalmejenPersistenceConfiguration.ConfigurationName));
        return builder;
    }
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextFactory<SkalmejenDbContext>(ConfigureDb, lifetime: ServiceLifetime.Singleton);
        return builder;
    }

    private static void ConfigureDb(IServiceProvider services, DbContextOptionsBuilder builder)
    {
        var connectionString = services.GetRequiredService<IOptions<SkalmejenPersistenceConfiguration>>().Value.Db.ConnectionString;
        builder
            .UseNpgsql(connectionString, opts =>
            {
                opts.MapEnum<SkalmejenRoundTypeDbo>("round_type", nameTranslator: new NpgsqlNullNameTranslator());
                opts.MapEnum<SkalmejenMusicProviderDbo>("music_provider", nameTranslator: new NpgsqlNullNameTranslator());

            })
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }


}
