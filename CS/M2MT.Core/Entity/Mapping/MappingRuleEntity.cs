using M2MT.Shared.Entity.InformationModel;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Entity.Mapping
{
    public class MappingRuleEntity : Convertable<MappingRuleEntity, MappingRule>
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid Mapping { get; set; }

        public IEnumerable<RefTo<Element>> Elements { get; set; }

        public MappingRuleEntity(MappingRule mapping)
        {
            ID = mapping.ID;
            Name = mapping.Name;
            Mapping = mapping.Mapping;
            Elements = mapping.Elements;
        }

        public MappingRuleEntity() { }

        public MappingRule Convert()
        {
            return new MappingRule() { ID = ID, Mapping = Mapping, Name = Name, Elements = Elements };
        }
    }
}
