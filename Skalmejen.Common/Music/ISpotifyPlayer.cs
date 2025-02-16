using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Common.Music;
public interface ISpotifyPlayer : ISpotifyLoader
{
    Task TransferPlayback(string deviceId);
    Task Play(TimeSpan? offset = null, string? trackId = null, string? deviceId = null);
    Task Pause(string? deviceId = null);
    Task SeekTo(TimeSpan offset, string? deviceId = null);

}
