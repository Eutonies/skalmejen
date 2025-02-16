using Microsoft.JSInterop;

namespace Skalmejen.UI.Components.Player;

public class SpotifyClientState(Func<Task> OnUpdate)
{
    private string? _deviceId;
    public string? DeviceId => _deviceId;

    [JSInvokable]
    public async Task UpdateDeviceId(string deviceId)
    {
        _deviceId = deviceId;
        await OnUpdate();
    }

}
