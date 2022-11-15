using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using InformationModel = M2MT.Shared.Model.InformationModel.Model;

namespace M2MT.Shared.Service.Model
{
    public class InformationModelReadService : IInformationModelReadService
    {
        private IInformationModelReadRepository modelRepository;

        public InformationModelReadService(IInformationModelReadRepository modelRepository)
        {
            this.modelRepository = modelRepository;
        }

        public async Task<IEnumerable<InformationModel>> GetAll()
        {
            return await this.modelRepository.GetModels();
        }
    }
}
