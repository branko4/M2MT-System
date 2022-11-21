
using System;

namespace M2MT.Shared.Model.Mapping
{
    public class MappingRule : Base
    {
        public string Name { get; internal set; }
        public Guid Mapping { get; internal set; }
    }
}
