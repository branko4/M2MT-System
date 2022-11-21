using M2MT.Shared.Entity.Util;
using M2MT.Shared.Model.InformationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Entity.InformationModel
{
    public class AttributeEntity : Convertable<AttributeEntity, AttributeModel>
    {
        public Guid ID { get; set; }
        public Guid Element { get; set; }
        public string Name { get; set; }

        public AttributeEntity() { }

        public AttributeEntity(AttributeModel attribute)
        {
            ID = attribute.ID;
            Element = attribute.Element;
            Name = attribute.Name;
        }

        public AttributeModel Convert()
        {
            return new AttributeModel() { Element = Element, ID = ID, Name = Name };
        }
    }
}
