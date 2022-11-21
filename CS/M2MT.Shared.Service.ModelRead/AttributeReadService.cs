using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Model
{
    public class AttributeReadService : IAttributeReadService
    {
        private IAttributeReadRepository _repository;

        public AttributeReadService(IAttributeReadRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<AttributeModel>> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
