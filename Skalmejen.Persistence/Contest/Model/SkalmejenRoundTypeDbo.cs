using Skalmejen.Common.Contest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Persistence.Contest.Model;
internal enum SkalmejenRoundTypeDbo
{
    Buzzer = 1
}


internal static class SkalmejenRoundTypeDboExtensions
{
    public static SkalmejenRoundTypeDbo ToDbo(this SkalmejenRountType typ) => typ switch {
        SkalmejenRountType.Buzzer => SkalmejenRoundTypeDbo.Buzzer,
        _ => throw new Exception("")
        };
}