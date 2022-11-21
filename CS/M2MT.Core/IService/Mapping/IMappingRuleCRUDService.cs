
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.IService.Mapping
{
    public interface IMappingRuleCRUDService : ICRUDService<MappingRule>, IMappingRuleReadService
    {
        Task<MappingRule> AddElement(RefTo<MappingRule> mappingRule, RefTo<Element> element);
    }
}
