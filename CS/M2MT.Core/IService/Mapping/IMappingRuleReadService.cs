
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace M2MT.Shared.IService.Mapping
{
    public interface IMappingRuleReadService : IReadService<MappingRule>
    {
        Task<IEnumerable<MappingRule>> GetAllRelatedWithMapping(Guid mappingRef);
        Task<MappingRule> GetOne(Guid mappingRuleRef);
        Task<IEnumerable<MappingRelation>> GetAllMappingRelations(Guid mappingRuleID);
    }
}
