using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Common.Model;
public record SkalmejenRound(
    long RoundId,
    long ContestId,
    string RoundName
    );
