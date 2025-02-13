using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Skalmejen.UI.Configuration;

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
            await InitatePlayer(token);
            _didInit = true;
        }
    }

    private async Task InitatePlayer(string token)
    {
        var module = await JS.InvokeAsync<IJSObjectReference>("import", "./Components/Player/SpotifyPlayerComponent.razor.js");
        await module.InvokeVoidAsync("SkalmejenSpotifyClient.setup", token);
    }

}
