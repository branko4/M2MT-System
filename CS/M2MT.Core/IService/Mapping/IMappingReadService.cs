
using M2MT.Shared.Model;
using M2MT.Shared.Model.Mapping;
using System;
using System.Threading.Tasks;

namespace M2MT.Shared.IService.Mapping
{
    public interface IMappingReadService : IReadService<MappingModel>
    {
        Task<MappingModel> GetOne(Guid refTo);
    }
}
