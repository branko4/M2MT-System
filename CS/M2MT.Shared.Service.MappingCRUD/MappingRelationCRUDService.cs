using M2MT.Shared.Exceptions;
using M2MT.Shared.IRepository.InformationModel;
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
    public class MappingRelationCRUDService : MappingRelationReadService, IMappingRelationCRUDService
    {
        private IMappingRelationCRUDRepository _repository;
        private IMappingRuleReadRepository _mappingRuleRepository;
        private IAttributeReadRepository _attributeRepository;
        private IElementReadRepository _elementRepository;

        public MappingRelationCRUDService(
            IMappingRelationCRUDRepository repository,
            IMappingRuleReadRepository mappingRuleRepository,
            IAttributeReadRepository attributeRepository,
            IElementReadRepository elementRepository
            ) : base(repository)
        {
            _repository = repository;
            _mappingRuleRepository = mappingRuleRepository;
            _attributeRepository = attributeRepository;
            _elementRepository = elementRepository;
        }

        public async  Task<MappingRelation> Create(MappingRelation mapping)
        {
            mapping.ID = Guid.NewGuid();
            await checkThatMappingRelationIsValid(mapping);
            
            return await _repository.Create(mapping);
        }

        private async Task checkThatMappingRelationIsValid(MappingRelation mapping)
        {
            // none of the properties should be null
            if (Guid.Empty.Equals(mapping.ID)) throw new ArgumentException("id can not be null");
            if (Guid.Empty.Equals(mapping.MappingRule)) throw new ArgumentException("mapping rule reference can not be null");
            if (Guid.Empty.Equals(mapping.AttributeLeft)
                || Guid.Empty.Equals(mapping.AttributeRight)) throw new ArgumentException("Attribute reference can not be null");

            // left and right side of the relation can not be the same
            if (mapping.AttributeLeft.Equals(mapping.AttributeRight)) throw new ArgumentException("Attribute references referenceses the same attribute");

            // checks if references are valid
            var mappingRule = await _mappingRuleRepository.GetOne(mapping.MappingRule);
            if (mappingRule == null) throw new NotFoundException<MappingRule>("mapping rule reference referenceses to unknown mapping rule");

            var left = await _attributeRepository.GetOne(mapping.AttributeLeft);
            var right = await _attributeRepository.GetOne(mapping.AttributeRight);
            if (left == null || right == null) throw new NotFoundException<AttributeModel>("attribute reference referenceses to unknown attribute");

            // check if relations of references are valid
            if (!(mappingRule.Elements.Contains(new RefTo<Element> { ID = left.Element })
                && mappingRule.Elements.Contains(new RefTo<Element> { ID = right.Element}))
                ) throw new NotFoundException<AttributeModel>("attribute reference referenceses to unknown attribute for given mapping rule(s)");

            var leftElement = await _elementRepository.GetOne(left.Element);
            var rightElement = await _elementRepository.GetOne(right.Element);
            if (leftElement.Model == rightElement.Model) throw new ArgumentException("attributes are from the same model");
        }

        public async Task<MappingRelation> Remove(Guid mapping)
        {
            if (Guid.Empty.Equals(mapping)) throw new ArgumentException("Mapping relation reference is null");
            if (!await this._repository.Excists(mapping)) throw new NotFoundException<MappingRelation>("mapping relation is not found");
            var mappingRelation = await _repository.GetOne(mapping);
            await _repository.Remove(mapping);
            return mappingRelation;
        }
    }
}
