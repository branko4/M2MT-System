using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Entity.Mapping
{
    public class CoupledElement
    {
        public Guid Element { get; set; }
        public Guid MappingRule { get; set; }
    }
}
