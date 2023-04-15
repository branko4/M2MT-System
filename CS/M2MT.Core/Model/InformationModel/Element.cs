

using M2MT.Shared.Entity.InformationModel;
using System;
using System.Collections.Generic;

namespace M2MT.Shared.Model.InformationModel
{
    public class Element : Base
    {
        public Guid Model { get; set; }
        public Guid Parent { get; set; }
        public string Name { get; set; }
        public IEnumerable<AttributeModel>? Attributes { get; set; }
    }
}
