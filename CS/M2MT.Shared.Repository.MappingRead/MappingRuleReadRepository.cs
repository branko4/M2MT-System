using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRuleReadRepository : IMappingRuleReadRepository
    {
        public Task<IEnumerable<MappingRule>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
