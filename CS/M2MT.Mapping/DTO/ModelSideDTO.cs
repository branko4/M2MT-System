using M2MT.Shared.Model.InformationModel;

namespace M2MT.Mapping.DTO
{
    public class ModelSideDTO
    {
        public Model Model { get; set; }
        public IEnumerable<Element> elements { get; set; }
    }
}