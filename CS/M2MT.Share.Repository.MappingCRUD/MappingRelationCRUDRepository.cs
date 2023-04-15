using Dapper;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System;
using System.Data;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRelationCRUDRepository : MappingRelationReadRepository, IMappingRelationCRUDRepository
    {
        public MappingRelationCRUDRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<MappingRelation> Create(MappingRelation mappingRelation)
        {
            var createdMapping = await dbConnection.QueryFirstAsync<MappingRelationEntity>(
                "INSERT INTO mapping.\"Mapping_relations\" values(@ID, @Mapping_rule, @Attribute_left, @Attribute_right); SELECT * FROM mapping.\"Mapping_relations\" WHERE \"Mapping_relations\".\"ID\" = @ID",
                new MappingRelationEntity(mappingRelation)
                );
            return createdMapping.Convert();
        }

        public async Task<MappingRelation> Remove(Guid mappingRelation)
        {
            await dbConnection.QueryAsync<MappingRelationEntity>(
                "DELETE FROM mapping.\"Mapping_relations\" WHERE \"Mapping_relations\".\"ID\" = @ID;",
                new { ID = mappingRelation }
                );
            return null;
        }
    }
}
