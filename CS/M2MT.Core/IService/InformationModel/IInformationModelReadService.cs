
using System.Collections.Generic;
using System.Threading.Tasks;
using IModel = M2MT.Shared.Model.InformationModel.Model;

namespace M2MT.Shared.IService.InformationModel
{
    public interface IInformationModelReadService
    {
        public Task<IEnumerable<IModel>> GetAll();
    }
}
