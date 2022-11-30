using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.Entity.Util;
using System;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRelationReadRepository : IMappingRelationReadRepository
    {
        protected IDbConnection dbConnection;

        public MappingRelationReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            if (dbConnection.State != ConnectionState.Open) dbConnection.Open();
        }

        public async Task<IEnumerable<MappingRelation>> GetAll()
        {
            var mappingRelations = await dbConnection.QueryAsync<MappingRelationEntity>("SELECT * FROM mapping.\"Mapping_relations\"");
            return Converter.ConvertList<MappingRelation, MappingRelationEntity>(mappingRelations);
        }

        public async Task<IEnumerable<MappingRelation>> GetAllMappingRelations(Guid mappingRuleID)
        {
            var mappingRelations = await dbConnection.QueryAsync<MappingRelationEntity>("SELECT * FROM mapping.\"Mapping_relations\" WHERE \"Mapping_rule\" = @ID;",
                new { ID = mappingRuleID });
            return Converter.ConvertList<MappingRelation, MappingRelationEntity>(mappingRelations);
        }

        public async Task<MappingRelation> GetOne(Guid mappingRelation)
        {
            var mappingRelationEntity = await dbConnection.QueryFirstAsync<MappingRelationEntity>(
                "SELECT * FROM mapping.\"Mapping_relations\" WHERE mapping.\"Mapping_relations\".\"ID\" = @ID;",
                new { ID = mappingRelation });
            return mappingRelationEntity.Convert();
        }
    }
}
