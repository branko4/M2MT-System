using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Model.Mapping
{
    public class MappingModel : Base
    {
        public string Name { get; set; }
        public Guid modelFrom { get; set; }
        public Guid modelTo { get; set; }
    }
}
