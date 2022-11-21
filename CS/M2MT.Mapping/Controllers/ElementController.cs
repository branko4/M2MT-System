using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using Microsoft.AspNetCore.Mvc;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementController : ControllerBase
    {
        private IElementReadService service;

        public ElementController(IElementReadService service)
        {
            this.service = service;
        }

        [HttpGet]
        public Task<IEnumerable<Element>> GetAll()
        {
            return service.GetAll();
        }
    }
}
