using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRelationCRUDService : MappingRelationReadService, IMappingRelationCRUDService
    {
        public MappingRelationCRUDService(IMappingRelationReadRepository repository) : base(repository)
        {
        }
    }
}
