using Nop.Core.Domain.Directory;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Directory
{
    public class AreaMap : EntityTypeConfiguration<Area>
    {
        public AreaMap()
        {
            this.ToTable("Area");
        }
    }
}
