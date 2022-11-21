
using System;

namespace M2MT.Shared.Model.Mapping
{
    public class MappingRelation : Base
    {
        public Guid MappingRule { get; set; }
        public Guid AttributeLeft { get; set; }
        public Guid AttributeRight { get; set; }
    }
}
