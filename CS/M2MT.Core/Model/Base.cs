using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Model
{
    public class Base
    {
        public Guid ID { get; set; }

        public override bool Equals(object obj)
        {
            Base other = obj as Base;
            if (other == null) return false;
            return this.ID.Equals(other.ID);
        }
    }
}
