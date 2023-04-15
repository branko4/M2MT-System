using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRuleReadService : IMappingRuleReadService
    {
        private IMappingRuleReadRepository _repository;
        private IMappingRelationReadRepository _relationRepository;

        public MappingRuleReadService(IMappingRuleReadRepository repository, IMappingRelationReadRepository relationRepository)
        {
            _repository = repository;
            _relationRepository = relationRepository;
        }

        public Task<IEnumerable<MappingRule>> GetAll()
        {
            return this._repository.GetAll();
        }

        public Task<IEnumerable<MappingRelation>> GetAllMappingRelations(Guid mappingRuleID)
        {
            return this._relationRepository.GetAllMappingRelations(mappingRuleID);
        }

        public Task<IEnumerable<MappingRule>> GetAllRelatedWithMapping(Guid mappingRef)
        {
            return this._repository.GetAllRelatedWithMapping(mappingRef);
        }

        public Task<MappingRule> GetOne(Guid mappingRuleRef)
        {
            return this._repository.GetOne(mappingRuleRef);
        }
    }
}
