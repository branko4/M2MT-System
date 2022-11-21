using M2MT.Shared.IService.Mapping;
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

        public MappingController(IMappingCRUDService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<MappingModel>> GetAll()
        {
            return await service.GetAll();
        }

        [HttpPost]
        public async Task<MappingModel> Create([FromBody] MappingModel mapping)
        {
            return await service.Create(mapping);
        }

        [HttpDelete]
        public async Task<MappingModel> Delete([FromBody] MappingModel mapping)
        {
            return await service.Remove(mapping);
        }
    }
}
