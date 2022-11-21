using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;

namespace M2MT.Shared.Service.Model
{
    public class InformationModelCRUDService : InformationModelReadService, IInformationModelCRUDService
    {
        public InformationModelCRUDService(IInformationModelReadRepository modelRepository) : base(modelRepository) { }
    }
}
