using Microsoft.AspNetCore.Mvc;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.IService.InformationModel;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationModelController : ControllerBase
    {
        private IInformationModelReadService service;

        public InformationModelController(IInformationModelReadService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Model>> GetAll()
        {
            var models = await service.GetAll();
            return models;
        }
    }
}
