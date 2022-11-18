using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Model.Mapping
{
    public class MappingModel
    {
        public Guid ID { get; set; }

        public override bool Equals(object obj)
        {
            MappingModel other = obj as MappingModel;
            if (other == null) return false;
            return this.ID.Equals(other.ID);
        }
    }
}
