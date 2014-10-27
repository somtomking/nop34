using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Expand
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class ExpandAttribute : Attribute
    {
        public ExpandType Type { get; private set; }
        public string Desc { get; private set; }
        public ExpandAttribute(ExpandType expandType, string desc = "")
        {
            this.Desc = desc;
            this.Type = expandType;
        }
    }

    public enum ExpandType
    {
        New,
        Update
    }

}
