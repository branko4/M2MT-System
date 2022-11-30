

using M2MT.Shared.Entity.InformationModel;
using System;
using System.Collections.Generic;

namespace M2MT.Shared.Model.InformationModel
{
    public class Element : Base
    {
        public Guid Model { get; internal set; }
        public Guid Parent { get; internal set; }
        public string Name { get; internal set; }
        public IEnumerable<AttributeModel>? Attributes { get; internal set; }
    }
}
