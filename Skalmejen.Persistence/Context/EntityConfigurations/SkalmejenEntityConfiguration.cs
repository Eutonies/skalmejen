using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Persistence.Context.EntityConfigurations;
internal class SkalmejenEntityConfiguration<TEnt> : IEntityTypeConfiguration<TEnt> where TEnt : class
{
    public void Configure(EntityTypeBuilder<TEnt> builder)
    {
    }
}
