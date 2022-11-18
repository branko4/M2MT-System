
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingCRUDRepository : IMappingReadRepository
    {
        public Task<MappingModel> Create(MappingModel mapping);
        public Task<MappingModel> Remove(MappingModel mapping);
    }
}
