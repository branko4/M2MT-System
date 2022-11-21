using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappingRelationController : ControllerBase
    {
        private IMappingRelationCRUDService service;

        public MappingRelationController(IMappingRelationCRUDService service)
        {
            this.service = service;
        }

        [HttpGet]
        public Task<IEnumerable<MappingRelation>> GetAll()
        {
            return service.GetAll();
        }

        [HttpPost]
        public Task<MappingRelation> Create([FromBody] MappingRelation mapping)
        {
            return service.Create(mapping);
        }

        [HttpDelete]
        public Task<MappingRelation> Delete([FromBody] MappingRelation mapping)
        {
            return service.Remove(mapping);
        }
    }
}
