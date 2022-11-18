
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingReadRepository
    {
        public Task<IEnumerable<MappingModel>> GetMappings();
    }
}
