using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRuleCRUDRepository : MappingRuleReadRepository, IMappingRuleCRUDRepository
    {
        public async Task<MappingRule> Create(MappingRule mappingRule)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MappingRule> Remove(MappingRule mappingRule)
        {
            throw new System.NotImplementedException();
        }
    }
}
