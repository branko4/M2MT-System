
using M2MT.Shared.Model.InformationModel;
using System;
using System.Collections.Generic;

namespace M2MT.Shared.Model.Mapping
{
    public class MappingRule : Base
    {
        public string Name { get; set; }
        public Guid Mapping { get; set; }
        public IEnumerable<RefTo<Element>> Elements { get; set; }
    }
}
