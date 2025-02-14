using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skalmejen.Common;
using Skalmejen.Common.Session;
using Skalmejen.UI.Components.Graphics;

namespace Skalmejen.UI.Pages.Layout;

public partial class MainLayout
{
    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public IHttpContextAccessor ContextAccessor { get; set; }

    private SkalmejenScreenSize? _screenSize;
    private SkalmejenSession? _session;

    protected override async Task OnInitializedAsync()
    {
        await Task.CompletedTask;
        var context = ContextAccessor.HttpContext!;
        if(_session == null)
        {
            _session = new SkalmejenSession(AuthenticatedUser: null);
            await InvokeAsync(StateHasChanged);
        }

        if(context.Request.Cookies.TryGetValue(SkalmejenConstants.Cookies.ScreenSize, out var cookStr))
        {
            if (Enum.TryParse<SkalmejenScreenSize>(cookStr, out var screenSize))
                _screenSize = screenSize;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(_screenSize == null)
        {
            var screenData = await LoadScreenData();
            _screenSize = screenData.ScreenSize;
            await JS.InvokeVoidAsync("setSkalmejenCookie", _screenSize.Value.ToString(), SkalmejenConstants.Cookies.ScreenSize);
        }
    }


    private async Task<SkalmejenScreenData> LoadScreenData()
    {
        var width = await JS.InvokeAsync<int>("transferScreenWidth");
        var height = await JS.InvokeAsync<int>("transferScreenHeight");
        var userAgent = await JS.InvokeAsync<string>("transferUserAgent");
        return new SkalmejenScreenData(width, height, userAgent);
    }

}
