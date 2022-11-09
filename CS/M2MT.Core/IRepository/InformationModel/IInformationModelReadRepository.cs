
using System.Collections.Generic;
using System.Threading.Tasks;
using IModel = M2MT.Shared.Model.InformationModel.Model;

namespace M2MT.Shared.IRepository.InformationModel
{
    public interface IInformationModelReadRepository
    {
        public Task<IEnumerable<IModel>> GetModel();
    }
}
