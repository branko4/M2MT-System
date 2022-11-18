
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.IService.Mapping
{
    public interface IMappingCRUDService : IMappingReadService
    {
        public Task<MappingModel> Create(MappingModel mapping);
        public Task<MappingModel> Remove(MappingModel mapping);
    }
}
