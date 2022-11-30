
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingRelationReadRepository : IReadRepository<MappingRelation>
    {
        Task<IEnumerable<MappingRelation>> GetAllMappingRelations(Guid mappingRuleID);
    }
}
