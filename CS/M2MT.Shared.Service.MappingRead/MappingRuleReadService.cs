using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRuleReadService : IMappingRuleReadService
    {
        private IMappingRuleReadRepository _repository;

        public MappingRuleReadService(IMappingRuleReadRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<MappingRule>> GetAll()
        {
            return this._repository.GetAll();
        }
    }
}
