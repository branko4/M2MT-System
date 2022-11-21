using M2MT.Shared.Entity.Util;
using M2MT.Shared.Model.InformationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace M2MT.Shared.Entity.InformationModel
{
    public class ElementEntity : Convertable<ElementEntity, Element>
    {
        public Guid ID { get; set; }
        public Guid Model { get; set; }
        public string Name { get; set; }

        public ElementEntity() { }

        public ElementEntity(Element attribute)
        {
            ID = attribute.ID;
            Model = attribute.Model;
            Name = attribute.Name;
        }

        public Element Convert()
        {
            return new Element() { Model = Model, ID = ID, Name = Name };
        }
    }
}
