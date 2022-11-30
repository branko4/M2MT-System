using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using System;
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
            mapping.ID = Guid.NewGuid();
            return _repository.Create(mapping);
        }

        public async Task<MappingRelation> Remove(Guid mapping)
        {
            var mappingRelation = await _repository.GetOne(mapping);
            await _repository.Remove(mapping);
            return mappingRelation;
        }
    }
}
