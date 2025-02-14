using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Persistence.Configuration;
public class SkalmejenPersistenceConfiguration
{
    public const string ConfigurationName = "Persistence";
    public SkalmejenDbConfiguration Db { get; set; }

}
