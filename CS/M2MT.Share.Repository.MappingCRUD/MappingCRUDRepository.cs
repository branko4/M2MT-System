using Dapper;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System;
using System.Data;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingCRUDRepository : MappingReadRepository, IMappingCRUDRepository
    {
        public MappingCRUDRepository(IDbConnection dbConnection) : base(dbConnection)
        {}

        public async Task<MappingModel> Create(MappingModel mapping)
        {
            var createdMapping = await dbConnection.QueryFirstAsync<MappingEntity>(
                "INSERT INTO mapping.\"Mappings\" values(@ID, @Model_From, @Model_To); SELECT * FROM mapping.\"Mappings\" WHERE \"Mappings\".\"ID\" = @ID",
                new MappingEntity(mapping)
                );
            return createdMapping.Convert();
        }

        public async Task<bool> Excists(Guid mapping)
        {
            return await dbConnection.QueryFirstAsync<bool>("SELECT 1 FROM mapping.\"Mappings\" WHERE \"Mappings\".\"ID\" = @ID; ",
                new { ID = mapping });
        }

        public async Task<MappingModel> Remove(Guid mapping)
        {
            await dbConnection.QueryAsync<MappingEntity>(
                "DELETE FROM mapping.\"Mappings\" WHERE \"Mappings\".\"ID\" = @ID",
                new { ID = mapping }
                );
            return null;
        }
    }
}
