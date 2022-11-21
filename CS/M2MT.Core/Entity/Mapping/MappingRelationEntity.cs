using M2MT.Shared.Entity.Util;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;

namespace M2MT.Shared.Entity.Mapping
{
    public class MappingRelationEntity :  Convertable<MappingRelationEntity, MappingRelation>
    {
        public Guid ID { get; set; }
        public Guid Mapping_rule { get; set; }
        public Guid Attribute_left { get; set; }
        public Guid Attribute_right { get; set; }


        public MappingRelationEntity() { }

        public MappingRelationEntity(MappingRelation mapping)
        {
            ID = mapping.ID;
            Mapping_rule = mapping.MappingRule;
            Attribute_left = mapping.AttributeLeft;
            Attribute_right = mapping.AttributeRight;
        }

        public MappingRelation Convert()
        {
            return new MappingRelation() { ID = ID, MappingRule = Mapping_rule, AttributeRight = Attribute_right, AttributeLeft = Attribute_left };
        }
    }
}
