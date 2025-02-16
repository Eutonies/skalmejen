namespace Skalmejen.Common.Music.Model;
public record SpotifyTrack(
    string TrackId,
    string Name,
    string Artist,
    TimeSpan Duration,
    IReadOnlyCollection<SpotifyImage> Images

    );
