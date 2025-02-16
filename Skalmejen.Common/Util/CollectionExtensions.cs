using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Common.Util;
public static class CollectionExtensions
{
    public static string MakeString(this IEnumerable<object> input, string separator = ",") =>
        string.Join(separator, input);


}
