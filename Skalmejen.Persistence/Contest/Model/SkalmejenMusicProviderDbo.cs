using Skalmejen.Common.Contest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Persistence.Contest.Model;
internal enum SkalmejenMusicProviderDbo
{
    Spotify
}

internal static class SkalmejenMusicProviderDboExtensions
{
    public static SkalmejenMusicProviderDbo ToDbo(this SkalmejenMusicProvider prov) => prov switch 
    { 
        SkalmejenMusicProvider.Spotify => SkalmejenMusicProviderDbo.Spotify,
        _ => throw new NotSupportedException()
    };
}
