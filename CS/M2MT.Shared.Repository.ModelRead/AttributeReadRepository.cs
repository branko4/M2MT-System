
using Dapper;
using M2MT.Shared.Entity.InformationModel;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.Model.InformationModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Model
{
    public class AttributeReadRepository : IAttributeReadRepository
    {
        private IDbConnection dbConnection;

        public AttributeReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<AttributeModel>> GetAll()
        {
            // TODO add to docs; according to the docs connection should be opend before use and closed after, this is more efficient, then creating a new connection each time
            dbConnection.Open();
            var attributes = await dbConnection.QueryAsync<AttributeEntity>("SELECT * FROM model.\"Attributes\"");
            dbConnection.Close();
            return Converter.ConvertList<AttributeModel, AttributeEntity>(attributes);
        }
    }
}
