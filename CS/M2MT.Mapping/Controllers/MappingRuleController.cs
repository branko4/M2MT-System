using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappingRuleController : ControllerBase
    {
        private IMappingRuleCRUDService service;

        public MappingRuleController(IMappingRuleCRUDService service)
        {
            this.service = service;
        }

        [HttpGet]
        public Task<IEnumerable<MappingRule>> GetAll()
        {
            return service.GetAll();
        }

        [HttpPost]
        public Task<MappingRule> Create([FromBody] MappingRule mapping)
        {
            return service.Create(mapping);
        }

        [HttpDelete]
        public Task<MappingRule> Delete([FromBody] MappingRule mapping)
        {
            return service.Remove(mapping);
        }
    }
}
