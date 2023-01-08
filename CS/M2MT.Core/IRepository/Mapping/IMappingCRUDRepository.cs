
using M2MT.Shared.Model.Mapping;
using System;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.Mapping
{
    public interface IMappingCRUDRepository : ICRUDRepository<MappingModel>, IMappingReadRepository
    {
    }
}
