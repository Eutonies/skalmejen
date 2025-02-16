using Microsoft.Extensions.Options;
using Skalmejen.Common.Music;
using Skalmejen.Common.Music.Model;
using Skalmejen.UI.Configuration;
using SpyOff.Infrastructure.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Integration.Music;
public class SpotifyPlayer : ISpotifyLoader, ISpotifyPlayer
{
    private readonly ISpotifyApiClient _client;

    public SpotifyPlayer(ISpotifyApiClient client, IOptions<SkalmejenIntegrationConfiguration> conf)
    {
        _token = conf.Value.Spotify.DeveloperToken;
        _client = client;
    }

    private string _token;


    public Task<SpotifyTrack> LoadTrack(string trackId)
    {
        throw new NotImplementedException();
    }

    public async Task Pause(string? deviceId = null)
    {
        await _client.PauseAUsersPlaybackAsync(deviceId);
    }

    public async Task Play(TimeSpan? offset = null, string? deviceId = null)
    {
        Body18? body = null;
        if (offset != null) 
        {
            body = new Body18
            {
                Position_ms = (int) offset.Value.TotalMilliseconds
            };
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
