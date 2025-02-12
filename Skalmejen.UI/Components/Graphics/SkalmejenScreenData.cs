namespace Skalmejen.UI.Components.Graphics;

public record SkalmejenScreenData(
    int Width,
    int Height,
    string UserAgent
    )
{
    private const int SmallWidthBreakpoint = 600;
    private const int SmallHeightBreakPoint = 1200;

    public SkalmejenScreenSize ScreenSize => Math.Min(Width, Height) < SmallWidthBreakpoint && Math.Max(Width, Height) < SmallHeightBreakPoint ? 
        SkalmejenScreenSize.Small :
        SkalmejenScreenSize.Large;
} 
