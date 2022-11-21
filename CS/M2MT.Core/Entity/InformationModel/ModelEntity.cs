using System;
using ModelModel = M2MT.Shared.Model.InformationModel.Model;
using System.Collections.Generic;
using M2MT.Shared.Entity.Util;

namespace M2MT.Shared.Entity.InformationModel
{
    public class ModelEntity : Convertable<ModelEntity, ModelModel>
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public ModelEntity() { }

        public ModelEntity(ModelModel attribute)
        {
            ID = attribute.ID;
            Name = attribute.Name;
        }

        public ModelModel Convert()
        {
            return new ModelModel() { ID = ID, Name = Name };
        }
    }
}
