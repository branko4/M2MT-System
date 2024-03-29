﻿using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingRuleCRUDService : MappingRuleReadService, IMappingRuleCRUDService
    {
        private IMappingRuleCRUDRepository _repository;
        public MappingRuleCRUDService(IMappingRuleCRUDRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<MappingRule> AddElement(RefTo<MappingRule> mappingRule, RefTo<Element> element)
        {
            await _repository.AddElement(mappingRule, element);
            return await _repository.GetOne(mappingRule);
        }

        public async Task<MappingRule> Create(MappingRule mappingrule)
        {
            return await _repository.Create(mappingrule);
        }

        public async Task<MappingRule> Remove(MappingRule mappingrule)
        {
            return await _repository.Remove(mappingrule);
        }
    }
}
