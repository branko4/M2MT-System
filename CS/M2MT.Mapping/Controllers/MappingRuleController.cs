using M2MT.Mapping.DTO;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappingRuleController : ControllerBase
    {
        private IMappingRuleCRUDService service;
        private IElementReadService elementService;
        private IMappingReadService mappingService;
        private IInformationModelReadService modelService;
        private IDbConnection dbConnection;
        private IMappingRelationReadService relationReadService;

        public MappingRuleController(
            IMappingRuleCRUDService service, 
            IElementReadService elementService, 
            IMappingReadService mappingService, 
            IMappingRelationReadService relationReadService,
            IInformationModelReadService modelService, 
            IDbConnection dbConnection)
        {
            this.service = service;
            this.elementService = elementService;
            this.mappingService = mappingService;
            this.modelService = modelService;
            this.dbConnection = dbConnection;
            this.relationReadService = relationReadService;
        }

        [HttpGet]
        public Task<IEnumerable<MappingRule>> GetAll()
        {
            return service.GetAll();
        }

        [HttpGet]
        [Route("{mappingRuleID}")]
        public Task<MappingRule> GetAll([FromRoute] Guid mappingRuleID)
        {
            return service.GetOne(mappingRuleID);
        }

        [HttpGet]
        [Route("{mappingRuleID}/modelsides")]
        public async Task<ModelSidesDTO> GetModelSides([FromRoute] Guid mappingRuleID)
        {
            var mappingRule = await service.GetOne(mappingRuleID);
            MappingModel mapping = await mappingService.GetOne(mappingRule.Mapping);
            var elements = await elementService.GetElementsMatchingID(mappingRule.Elements.Select((el) => el.ID));
            var modelFrom = await this.modelService.GetOne(mapping.modelFrom);
            var modelTo = await this.modelService.GetOne(mapping.modelTo);
            return new ModelSidesDTO
            {
                Left = new ModelSideDTO
                {
                    ModelId = modelFrom,
                    elements = elements.Where((element) => element.Model == mapping.modelFrom)
                },
                Right = new ModelSideDTO
                {
                    ModelId = modelTo,
                    elements = elements.Where((element) => element.Model == mapping.modelTo)
                }
            };
        }

        [HttpGet]
        [Route("{mappingRuleID}/relations")]
        public Task<IEnumerable<MappingRelation>> GetRelations([FromRoute] Guid mappingRuleID)
        {
            return this.service.GetAllMappingRelations(mappingRuleID);
        }

        [HttpPost]
        public Task<MappingRule> Create([FromBody] MappingRule mapping)
        {
            return service.Create(mapping);
        }

        [HttpDelete]
        [Route("{mappingRuleID}")]
        public Task<MappingRule> Delete([FromRoute] Guid mappingRuleID)
        {
            return service.Remove(mappingRuleID);
        }

        [HttpPost]
        [Route("element")]
        public Task<MappingRule> AddElement(MappingRuleAddElementDTO dto)
        {
            return service.AddElement(dto.MappingRule.ID, dto.Element.ID);
        }
    }
}
