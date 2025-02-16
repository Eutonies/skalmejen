using System.Globalization;

namespace Skalmejen.UI.Util;

public static class FormatExtensions
{

    private static readonly CultureInfo _enUs = new CultureInfo("en-US");

    public static string DecimalValueFormatted(this decimal value, int afterDecimal = 2) => 
        value.ToString($"G{afterDecimal}", _enUs);


    public static string TimeValueFormatted(this TimeSpan timeSpan) => timeSpan.ToString("hh\\:mm\\:ss");


}
