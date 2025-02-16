using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Skalmejen.Common.Music.Model;
using Skalmejen.Integration.Configuration;
using Skalmejen.UI.Util;

namespace Skalmejen.UI.Components.Player;

public partial class SpotifyPlayerComponent
{

    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public IOptions<SpotifyConfiguration> Conf { get; set; }


    private bool _didInit = false;

    protected override async Task OnInitializedAsync()
    {
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_didInit)
        {
            var token = Conf.Value.DeveloperToken;
            if (_currentTrack != null)
            {
                _rangeEndValue = 100;
                _rangeEndTimeSpan = _currentTrack.Duration;
            }
            //await InitatePlayer(token);
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

    private async Task InitatePlayer(string token)
    {
        var module = await JS.InvokeAsync<IJSObjectReference>("import", "./Components/Player/SpotifyPlayerComponent.razor.js");
        await module.InvokeVoidAsync("SkalmejenSpotifyClient.setup", token);
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
    public decimal RangeStartValue
    {
        get => _rangeStartValue;
        set 
        {
            _rangeStartValue = value;       
            if(_currentTrack != null)
            {
                _rangeStartTimeSpan = _currentTrack.Duration * Convert.ToDouble(value / 100m);
                InvokeAsync(StateHasChanged);
            }
        }
    }

    private decimal _rangeEndValue = 0;
    public decimal RangeEndValue
    {
        get => _rangeEndValue;
        set
        {
            _rangeEndValue = value;
            if (_currentTrack != null)
            {
                _rangeEndTimeSpan = _currentTrack.Duration * Convert.ToDouble(value / 100m);
                InvokeAsync(StateHasChanged);
            }

        }
    }

    private TimeSpan _rangeStartTimeSpan = TimeSpan.Zero;
    public string RangeStartTimeSpan { 
        get => _rangeStartTimeSpan.TimeValueFormatted(); 
        set
        {
            if(TimeSpan.TryParse(value, out var start))
            {
                _rangeStartTimeSpan = start;
                if(_currentTrack != null && _currentTrack.Duration > TimeSpan.Zero)
                {
                    _rangeStartValue = Convert.ToDecimal(_rangeStartTimeSpan.TotalMilliseconds / _currentTrack.Duration.TotalMilliseconds) * 100;
                    InvokeAsync(StateHasChanged);
                }
            }
        }
    }

    private TimeSpan _rangeEndTimeSpan = TimeSpan.Zero;
    public string RangeEndTimeSpan
    {
        get => _rangeEndTimeSpan.TimeValueFormatted();
        set
        {
            if (TimeSpan.TryParse(value, out var start))
            {
                _rangeEndTimeSpan = start;
                if (_currentTrack != null && _currentTrack.Duration > TimeSpan.Zero)
                {
                    _rangeEndValue = Convert.ToDecimal(_rangeEndTimeSpan.TotalMilliseconds / _currentTrack.Duration.TotalMilliseconds) * 100;
                    InvokeAsync(StateHasChanged);
                }
            }
        }
    }


}
