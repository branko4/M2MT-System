using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;

namespace M2MT.Mapping.DTO
{
    public class MappingRuleAddElementDTO
    {
        public RefTo<MappingRule> MappingRule { get; set; }
        public RefTo<Element> Element { get; set; }
    }
}
