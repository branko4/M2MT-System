using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingReadService : IMappingReadService
    {
        private IMappingReadRepository _repository;

        public MappingReadService(IMappingReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MappingModel>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
