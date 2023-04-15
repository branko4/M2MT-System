using M2MT.Shared.Exceptions;
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
        private IMappingReadRepository _mappingRepository;

        public MappingRuleCRUDService(
            IMappingRuleCRUDRepository repository, 
            IMappingRelationReadRepository relationRepository,
            IMappingReadRepository mappingRepository
            ) : base(repository, relationRepository)
        {
            _repository = repository;
            _mappingRepository = mappingRepository;
        }

        public async Task<MappingRule> AddElement(Guid mappingRule, Guid element)
        {
            await _repository.AddElement(mappingRule, element);
            return await _repository.GetOne(mappingRule);
        }

        public async Task<MappingRule> Create(MappingRule mappingrule)
        {
            if (Guid.Empty.Equals(mappingrule.Mapping)) throw new ArgumentException("Mapping reference can not be null");
            if (! await _mappingRepository.Excists(mappingrule.Mapping)) throw new NotFoundException<MappingModel>("mapping is not found");
            if (mappingrule.Name.Length > 100) throw new ArgumentException("Name can not be longer then 100 characters");
            mappingrule.ID = Guid.NewGuid();
            var createdMappingRule = await _repository.Create(mappingrule);
            await _repository.AddElements(mappingrule.Elements, mappingrule);
            return await _repository.GetOne(createdMappingRule.ID);
        }

        public async Task<MappingRule> Remove(Guid mappingrule)
        {
            if (Guid.Empty.Equals(mappingrule)) throw new ArgumentException("Mapping rule reference can not be null");
            var returnVal = await _repository.GetOne(mappingrule);
            if (returnVal == null) throw new NotFoundException<MappingRule>("mapping rule is not found");
            await _repository.Remove(mappingrule);
            return returnVal;
        }
    }
}
