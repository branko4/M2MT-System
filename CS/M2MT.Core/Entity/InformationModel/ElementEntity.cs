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
        public Guid Parent { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Attributes { get; set; } = new List<string>();

        public ElementEntity() { }

        public ElementEntity(Element attribute)
        {
            ID = attribute.ID;
            Model = attribute.Model;
            Parent = attribute.Parent;
            Name = attribute.Name;
        }

        public Element Convert()
        {
            var AttributeModels =  new List<AttributeModel>();

            foreach (var attr in Attributes)
            {
                var propOfAttr = attr
                    .Split(',');

                var attrModel = new AttributeModel {
                    ID = new Guid(propOfAttr[0].Replace("(", "")), 
                    Name = propOfAttr[1], 
                    Element = new Guid(propOfAttr[2].Replace(")","")) 
                };
                AttributeModels.Add(attrModel);
            }
            return new Element() { Model = Model, ID = ID, Name = Name, Parent = Parent, Attributes = AttributeModels};
        }
    }
}
