﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Common.Session;
public record SkalmejenSession(
    AuthenticatedUser? AuthenticatedUser
    );
