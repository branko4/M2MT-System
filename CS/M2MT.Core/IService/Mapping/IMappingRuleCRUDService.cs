
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.IService.Mapping
{
    public interface IMappingRuleCRUDService : IMappingRuleReadService
    {
        public Task<MappingRule> Create(MappingRule mappingrule);
        public Task<MappingRule> Remove(MappingRule mappingrule);
    }
}
