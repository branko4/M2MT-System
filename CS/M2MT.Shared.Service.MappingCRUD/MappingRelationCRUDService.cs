using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRelationCRUDService : MappingRelationReadService, IMappingRelationCRUDService
    {
        private IMappingRelationCRUDRepository _repository;
        public MappingRelationCRUDService(IMappingRelationCRUDRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<MappingRelation> Create(MappingRelation mapping)
        {
            return _repository.Create(mapping);
        }

        public Task<MappingRelation> Remove(MappingRelation mapping)
        {
            return _repository.Remove(mapping);
        }
    }
}
