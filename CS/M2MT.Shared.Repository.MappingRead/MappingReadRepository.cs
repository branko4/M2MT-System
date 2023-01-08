using Dapper;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.Mapping;
using System;
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
            if (dbConnection.State != ConnectionState.Open) dbConnection.Open();
        }

        public async Task<IEnumerable<MappingModel>> GetAll()
        {
            // TODO add to docs; according to the docs connection should be opend before use and closed after, this is more efficient, then creating a new connection each time
            var mappings = await dbConnection.QueryAsync<MappingEntity>("SELECT * FROM mapping.\"Mappings\"");
            return Converter.ConvertList<MappingModel, MappingEntity>(mappings);
        }

        public async Task<MappingModel> GetOne(Guid id)
        {
            var mapping = await dbConnection.QueryFirstAsync<MappingEntity>("SELECT * FROM mapping.\"Mappings\" WHERE \"ID\" = @ID",
                new { ID = id });
            return mapping.Convert();
        }

        public async Task<bool> Excists(Guid mappingRef)
        {
            return await dbConnection.QueryFirstAsync<bool>("SELECT 1 FROM mapping.\"Mappings\" WHERE \"ID\" = @ID",
                new { ID = mappingRef });
        }
    }
}
