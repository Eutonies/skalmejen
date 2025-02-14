using Skalmejen.Common.Music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Common.Music;
public interface ISpotifyLoader
{
    Task<SpotifyTrack> LoadTrack(string trackId);

}
