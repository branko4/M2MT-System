using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Entity.Util
{
    public interface Convertable<Base, To>
    {
        public To Convert();
    }
}
