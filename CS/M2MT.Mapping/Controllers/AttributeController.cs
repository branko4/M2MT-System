using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using Microsoft.AspNetCore.Mvc;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private IAttributeReadService service;

        public AttributeController(IAttributeReadService service)
        {
            this.service = service;
        }

        [HttpGet]
        public Task<IEnumerable<AttributeModel>> GetAll()
        {
            return service.GetAll();
        }
    }
}
