
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IService.Mapping
{
    public interface IMappingRuleReadService
    {
        public Task<IEnumerable<MappingRule>> GetAll();
    }
}
