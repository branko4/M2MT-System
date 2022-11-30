using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingCRUDService : MappingReadService, IMappingCRUDService
    {
        private IMappingCRUDRepository _repository;

        public MappingCRUDService(IMappingCRUDRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<MappingModel> Create(MappingModel mapping)
        {
            mapping.ID = Guid.NewGuid();
            return await _repository.Create(mapping);
        }

        public async Task<MappingModel> Remove(Guid mapping)
        {
            return await _repository.Remove(mapping);
        }
    }
}
