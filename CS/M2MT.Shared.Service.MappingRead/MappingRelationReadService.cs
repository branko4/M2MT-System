using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRelationReadService : IMappingRelationReadService
    {
        private IMappingRelationReadRepository _repository;

        public MappingRelationReadService(IMappingRelationReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MappingRelation>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
