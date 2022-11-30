using Microsoft.AspNetCore.Mvc;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationModelController : ControllerBase
    {
        private IInformationModelReadService service;
        private IElementReadService elementService;

        public InformationModelController(IInformationModelReadService service, IElementReadService elementService)
        {
            this.service = service;
            this.elementService = elementService;
        }

        [HttpGet]
        public Task<IEnumerable<Model>> GetAll()
        {
            return service.GetAll();
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<Model> GetAll([FromRoute] Guid ID)
        {
            return service.GetOne(ID);
        }

        [HttpGet]
        [Route("{ID}/elements")]
        public Task<IEnumerable<Element>> GetAllElementsForModel([FromRoute] Guid ID)
        {
            return elementService.GetAllElementsOfModel(ID);
        }
    }
}
