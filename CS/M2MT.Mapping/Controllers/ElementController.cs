using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

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

        [HttpGet]
        [Route("multiple")]
        public Task<IEnumerable<Element>> GetElementsMatchingID([FromBody] IEnumerable<Guid> elementIDs)
        {
            return service.GetElementsMatchingID(elementIDs);
        }

        [HttpGet]
        [Route("{elementID}/parents")]
        public Task<IEnumerable<Element>> GetElementsWithParents([FromRoute] Guid elementID)
        {
            return service.GetElementsWithParents(elementID);
        }
    }
}
