
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingRuleCRUDRepository : IMappingRuleReadRepository
    {
        public Task<MappingRule> Create(MappingRule mappingRule);
        public Task<MappingRule> Remove(MappingRule mappingRule);
    }
}
