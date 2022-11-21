
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingRuleCRUDRepository : ICRUDRepository<MappingRule>, IMappingRuleReadRepository
    {
        public Task AddElement(RefTo<MappingRule> mappingRule, RefTo<Element> element);
    }
}
