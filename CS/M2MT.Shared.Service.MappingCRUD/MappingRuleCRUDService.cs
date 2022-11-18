using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRuleCRUDService : MappingRuleReadService, IMappingRuleCRUDService
    {
        public MappingRuleCRUDService(IMappingRuleReadRepository repository) : base(repository)
        {
        }
    }
}
