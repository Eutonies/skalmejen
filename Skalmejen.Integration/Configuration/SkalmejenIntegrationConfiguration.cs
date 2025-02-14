using Skalmejen.Integration.Configuration;

namespace Skalmejen.UI.Configuration;

public class SkalmejenIntegrationConfiguration
{
    public const string ConfigurationName = "Integration";

    public SpotifyConfiguration Spotify { get; set; }

}
