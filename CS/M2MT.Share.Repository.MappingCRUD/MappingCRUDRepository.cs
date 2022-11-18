using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    internal class MappingCRUDRepository : MappingReadRepository, IMappingCRUDRepository
    {
        public Task<MappingModel> Create(MappingModel mapping)
        {
            throw new System.NotImplementedException();
        }

        public Task<MappingModel> Remove(MappingModel mapping)
        {
            throw new System.NotImplementedException();
        }
    }
}
