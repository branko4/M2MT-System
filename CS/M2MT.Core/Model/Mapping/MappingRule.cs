
using System;

namespace M2MT.Shared.Model.Mapping
{
    public class MappingRule
    {
        public Guid ID { get; set; }

        public override bool Equals(object obj)
        {
            MappingRule other = obj as MappingRule;
            if (other == null) return false;
            return this.ID.Equals(other.ID);
        }
    }
}
