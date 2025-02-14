using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Skalmejen.Common.Music.Model;
using Skalmejen.Integration.Configuration;

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

}
