using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Common.Contest.Model;
public abstract record SkalmejenSoundByte(
    Guid SoundByteId,
    SkalmejenMusicProvider MusicProvider
    );

public record SkalmejenSpotifySoundByte(
    Guid SoundByteId,
    string TrackId,
    decimal StartAt,
    decimal EndAt
    ) : SkalmejenSoundByte(SoundByteId, SkalmejenMusicProvider.Spotify);
