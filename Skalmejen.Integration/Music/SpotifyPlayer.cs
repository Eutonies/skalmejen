using Microsoft.Extensions.Options;
using Skalmejen.Common.Music;
using Skalmejen.Common.Music.Model;
using Skalmejen.Common.Util;
using Skalmejen.UI.Configuration;
using SpyOff.Infrastructure.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Integration.Music;
public class SpotifyPlayer : ISpotifyPlayer
{
    private readonly ISpotifyApiClient _client;

    public SpotifyPlayer(ISpotifyApiClient client)
    {
        _client = client;
    }



    public async Task<SpotifyTrack> LoadTrack(string trackId)
    {
        var loaded = await _client.GetTrackAsync(trackId);
        var returnee = new SpotifyTrack(
            TrackId: loaded.Id,
            Name: loaded.Name,
            Artist: loaded.Artists.Select(_ => _.Name).MakeString(", "),
            Duration: TimeSpan.FromMilliseconds(loaded.Duration_ms),
            Images: loaded.Album.Images
               .Select(_ => new SpotifyImage(Url: _.Url, _.Width ?? 200, _.Height ?? 200))
               .ToList()
               );
        return returnee;
    }

    public async Task Pause(string? deviceId = null)
    {
        await _client.PauseAUsersPlaybackAsync(deviceId);
    }

    public async Task Play(TimeSpan? offset = null, string? trackId = null, string? deviceId = null)
    {
        Body18? body = null;
        if(trackId != null && offset == null)
            offset = TimeSpan.Zero;
        if (offset != null) 
        {
            body = new Body18
            {
                Position_ms = (int) offset.Value.TotalMilliseconds
            };
            if (trackId != null)
                body.Context_uri = $"spotify:track:{trackId}";
        }
        await _client.StartAUsersPlaybackAsync(deviceId, body);
    }

    public async Task SeekTo(TimeSpan offset, string? deviceId = null)
    {
        await _client.SeekToPositionInCurrentlyPlayingTrackAsync(position_ms: (int) offset.TotalMilliseconds, deviceId);
    }

    public async Task TransferPlayback(string deviceId)
    {
        var body = new Body17 { Device_ids = [deviceId] };
        await _client.TransferAUsersPlaybackAsync(body);
    }
}
