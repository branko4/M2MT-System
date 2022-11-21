using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.Entity.Util;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRelationReadRepository : IMappingRelationReadRepository
    {
        protected IDbConnection dbConnection;

        public MappingRelationReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<MappingRelation>> GetAll()
        {
            dbConnection.Open();
            var mappingRelations = await dbConnection.QueryAsync<MappingRelationEntity>("SELECT * FROM mapping.\"Mapping_relations\"");
            dbConnection.Close();
            return Converter.ConvertList<MappingRelation, MappingRelationEntity>(mappingRelations);
        }
    }
}
