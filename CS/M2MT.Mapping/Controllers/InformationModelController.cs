using Microsoft.AspNetCore.Mvc;
using M2MT.Shared.Model.InformationModel;

namespace M2MT.Mapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationModelController : ControllerBase
    {
        [HttpGet]
        public List<Model> GetAll()
        {
            return new List<Model>();
        }
    }
}
