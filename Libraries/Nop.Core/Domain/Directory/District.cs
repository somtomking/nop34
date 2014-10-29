using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Expand;
namespace Nop.Core.Domain.Directory
{
    [Expand(ExpandType.New)]
    public partial class District : BaseEntity
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public virtual City City { get; set; }
    }
}
