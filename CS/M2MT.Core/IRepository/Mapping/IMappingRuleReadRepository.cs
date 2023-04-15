
using M2MT.Shared.Model;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingRuleReadRepository : IReadRepository<MappingRule>
    {
        Task<IEnumerable<MappingRule>> GetAllRelatedWithMapping(Guid mappingID);
    }
}
