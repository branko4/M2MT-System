
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
    public class ElementReadRepository : IElementReadRepository
    {
        private IDbConnection dbConnection;

        public ElementReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Element>> GetAll()
        {
            dbConnection.Open();
            var elements = await dbConnection.QueryAsync<ElementEntity>("SELECT * FROM model.\"Elements\"");
            dbConnection.Close();
            return Converter.ConvertList<Element, ElementEntity>(elements);
        }
    }
}
