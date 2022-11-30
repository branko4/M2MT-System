using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappingController : ControllerBase
    {
        private IMappingCRUDService service;
        private IMappingRuleReadService readService;

        public MappingController(IMappingCRUDService service, IMappingRuleReadService readService)
        {
            this.service = service;
            this.readService = readService;
        }

        [HttpGet]
        public async Task<IEnumerable<MappingModel>> GetAll()
        {
            return await service.GetAll();
        }

        [HttpGet]
        [Route("{mappingID}/mappingrules")]
        public Task<IEnumerable<MappingRule>> GetAllMappingRulesByMapping([FromRoute] Guid mappingID)
        {
            return readService.GetAllRelatedWithMapping(mappingID);
        }

        [HttpGet]
        [Route("{mappingID}")]
        public Task<MappingModel> GetAll([FromRoute] Guid mappingID)
        {
            return service.GetOne(mappingID);
        }

        [HttpPost]
        public async Task<MappingModel> Create([FromBody] MappingModel mapping)
        {
            return await service.Create(mapping);
        }

        [HttpDelete]
        [Route("{mappingID}")]
        public async Task<MappingModel> Delete([FromRoute] Guid mappingID)
        {
            return await service.Remove(mappingID);
        }
    }
}
