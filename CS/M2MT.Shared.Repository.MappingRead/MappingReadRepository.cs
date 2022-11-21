using Dapper;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingReadRepository : IMappingReadRepository
    {
        protected IDbConnection dbConnection;

        public MappingReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<MappingModel>> GetAll()
        {
            // TODO add to docs; according to the docs connection should be opend before use and closed after, this is more efficient, then creating a new connection each time
            dbConnection.Open();
            var mappings = await dbConnection.QueryAsync<MappingEntity>("SELECT * FROM mapping.\"Mappings\"");
            dbConnection.Close();
            return Converter.ConvertList<MappingModel, MappingEntity>(mappings);
        }
    }
}
