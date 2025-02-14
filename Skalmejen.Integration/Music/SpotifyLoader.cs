using Skalmejen.Common.Music;
using Skalmejen.Common.Music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Integration.Music;
public class SpotifyLoader : ISpotifyLoader
{



    public Task<SpotifyTrack> LoadTrack(string trackId)
    {
        throw new NotImplementedException();
    }
}
