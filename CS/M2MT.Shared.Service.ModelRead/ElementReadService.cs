
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Model
{
    public class ElementReadService : IElementReadService
    {
        private IElementReadRepository _repository;

        public ElementReadService(IElementReadRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Element>> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
