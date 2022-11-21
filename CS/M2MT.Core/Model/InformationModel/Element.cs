

using System;

namespace M2MT.Shared.Model.InformationModel
{
    public class Element : Base
    {
        public Guid Model { get; internal set; }
        public string Name { get; internal set; }
    }
}
