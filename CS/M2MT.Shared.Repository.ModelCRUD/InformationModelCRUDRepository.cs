using M2MT.Shared.IRepository.InformationModel;
using System.Data;

namespace M2MT.Shared.Repository.Model
{
    public class InformationModelCRUDRepository : InformationModelReadRepository, IInformationModelCRUDRepository
    {
        public InformationModelCRUDRepository(IDbConnection dbConnection) : base(dbConnection) { }
    }
}
