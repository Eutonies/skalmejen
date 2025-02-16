using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Common.Music;
public interface ISpotifyPlayer
{
    Task TransferPlayback(string deviceId);
    Task Play(TimeSpan? offset = null, string? deviceId = null);
    Task Pause(string? deviceId = null);
    Task SeekTo(TimeSpan offset, string? deviceId = null);

}
