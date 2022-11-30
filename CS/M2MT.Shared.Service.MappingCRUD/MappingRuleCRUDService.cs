using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRuleCRUDService : MappingRuleReadService, IMappingRuleCRUDService
    {
        private IMappingRuleCRUDRepository _repository;

        public MappingRuleCRUDService(IMappingRuleCRUDRepository repository, IMappingRelationReadRepository relationRepository) : base(repository, relationRepository)
        {
            _repository = repository;
        }

        public async Task<MappingRule> AddElement(Guid mappingRule, Guid element)
        {
            await _repository.AddElement(mappingRule, element);
            return await _repository.GetOne(mappingRule);
        }

        public async Task<MappingRule> Create(MappingRule mappingrule)
        {
            mappingrule.ID = Guid.NewGuid();
            var createdMappingRule = await _repository.Create(mappingrule);
            await _repository.AddElements(mappingrule.Elements, mappingrule);
            return await _repository.GetOne(createdMappingRule.ID);
        }

        public async Task<MappingRule> Remove(Guid mappingrule)
        {
            return await _repository.Remove(mappingrule);
        }
    }
}
