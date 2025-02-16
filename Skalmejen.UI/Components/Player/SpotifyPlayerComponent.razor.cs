using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Skalmejen.Common.Music.Model;
using Skalmejen.Integration.Configuration;
using Skalmejen.UI.Configuration;
using Skalmejen.UI.Util;
using System.Globalization;

namespace Skalmejen.UI.Components.Player;

public partial class SpotifyPlayerComponent
{

    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public IOptions<SkalmejenIntegrationConfiguration> Conf { get; set; }


    private bool _didInit = false;
    private string? _playerDeviceId;


    protected override async Task OnInitializedAsync()
    {
        if (_currentTrack != null)
        {
            _rangeEndValue = 100;
            _rangeEndTimeSpan = _currentTrack.Duration;
        }
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_didInit)
        {
            var token = Conf.Value.Spotify.DeveloperToken;
            await InitatePlayer(token);
            _didInit = true;
        }
    }
    private bool _isPlaying = false;
    private bool IsPlaying => _isPlaying;

    private SpotifyTrack? _currentTrack = new SpotifyTrack(
        TrackId: "0tGPJ0bkWOUmH7MEOR77qc",
        Name: "Cut To The Feeling",
        Artist: "Carly Rae Jepsen",
        Duration: TimeSpan.FromMilliseconds(207959),
        Images: [
            new SpotifyImage("https://i.scdn.co/image/ab67616d00001e027359994525d219f64872d3b1", 300, 300)
            ]
        );

    private Task OnUpdate() => InvokeAsync(StateHasChanged);

    private SpotifyClientState _clientState;

    private async Task InitatePlayer(string token)
    {
        var module = await JS.InvokeAsync<IJSObjectReference>("import", "./Components/Player/SpotifyPlayerComponent.razor.js");
        _clientState = new SpotifyClientState(
            OnUpdate: OnUpdate
        );
        await module.InvokeVoidAsync("SkalmejenSpotifyClient.setup", token, DotNetObjectReference.Create(_clientState));

    }

    private void OnPlayPauseClicked()
    {
        _isPlaying = !_isPlaying;
        InvokeAsync(StateHasChanged);
    }

    private void OnBackwardClicked()
    {

    }

    private void OnForwardClicked()
    {

    }


    private decimal _rangeStartValue = 0;
    public string RangeStartValue => _rangeStartValue.DecimalValueFormatted(3);
    private static readonly CultureInfo EnUs = CultureInfo.GetCultureInfo("en-US");
    public void OnStartValueChanged(ChangeEventArgs ev)
    {
        if(decimal.TryParse(ev.Value?.ToString(), EnUs, out _rangeStartValue))
        {
            if (_currentTrack != null)
            {
                _rangeStartTimeSpan = _currentTrack.Duration * Convert.ToDouble(_rangeStartValue / 100m);
            }
            InvokeAsync(StateHasChanged);
        }
    }


    private decimal _rangeEndValue = 100;
    public string RangeEndValue => _rangeEndValue.DecimalValueFormatted(3);
    public void OnEndValueChanged(ChangeEventArgs ev)
    {
        if (decimal.TryParse(ev.Value?.ToString(), EnUs, out _rangeEndValue))
        {
            if (_currentTrack != null)
            {
                _rangeEndTimeSpan = _currentTrack.Duration * Convert.ToDouble(_rangeEndValue / 100m);
            }
            InvokeAsync(StateHasChanged);
        }
    }


    private TimeSpan _rangeStartTimeSpan = TimeSpan.Zero;
    public string RangeStartTimeSpan => _rangeStartTimeSpan.TimeValueFormatted(); 

    private void OnStartTimeSpanChanged(ChangeEventArgs ev)
    {
        if (TimeSpan.TryParse(ev.Value?.ToString(), out var start))
        {
            _rangeStartTimeSpan = start;
            if (_currentTrack != null && _currentTrack.Duration > TimeSpan.Zero)
            {
                _rangeStartValue = Convert.ToDecimal(_rangeStartTimeSpan.TotalMilliseconds / _currentTrack.Duration.TotalMilliseconds) * 100;
                InvokeAsync(StateHasChanged);
            }
        }
    }

    private TimeSpan _rangeEndTimeSpan = TimeSpan.Zero;
    public string RangeEndTimeSpan => _rangeEndTimeSpan.TimeValueFormatted();

    private void OnEndTimeSpanChanged(ChangeEventArgs ev)
    {
        if (TimeSpan.TryParse(ev.Value?.ToString(), out var end))
        {
            _rangeEndTimeSpan = end;
            if (_currentTrack != null && _currentTrack.Duration > TimeSpan.Zero)
            {
                _rangeEndValue = Convert.ToDecimal(_rangeEndTimeSpan.TotalMilliseconds / _currentTrack.Duration.TotalMilliseconds) * 100;
                InvokeAsync(StateHasChanged);
            }
        }
    }

}
