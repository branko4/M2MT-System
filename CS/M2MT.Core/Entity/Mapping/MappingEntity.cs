using M2MT.Shared.Entity.Util;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Entity.Mapping
{
    public class MappingEntity : Convertable<MappingEntity, MappingModel>
    {
        public Guid ID { get; set; }
        public Guid Model_From { get; set; }
        public Guid Model_To { get; set; }

        public MappingEntity() { }

        public MappingEntity(MappingModel mapping)
        {
            ID = mapping.ID;
            Model_From = mapping.modelFrom;
            Model_To = mapping.modelTo;
        }

        public MappingModel Convert()
        {
            return new MappingModel() { ID = ID, modelFrom = Model_From, modelTo = Model_To };
        }
    }
}
