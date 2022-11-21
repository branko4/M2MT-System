
using M2MT.Shared.Model;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingRuleReadRepository : IReadRepository<MappingRule>
    {
        public Task<MappingRule> GetOne(RefTo<MappingRule> mappingRule);
    }
}
