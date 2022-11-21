using Dapper;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
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
            dbConnection.Open();
            var createdMapping = await dbConnection.QueryFirstAsync<MappingEntity>(
                "INSERT INTO mapping.\"Mappings\" values(@ID, @Model_From, @Model_To); SELECT * FROM mapping.\"Mappings\" WHERE \"Mappings\".\"ID\" = @ID",
                new MappingEntity(mapping)
                );
            dbConnection.Close();
            return createdMapping.Convert();
        }

        public async Task<MappingModel> Remove(MappingModel mapping)
        {
            dbConnection.Open();
            await dbConnection.QueryAsync<MappingEntity>(
                "DELETE FROM mapping.\"Mappings\" WHERE \"Mappings\".\"ID\" = @ID",
                new MappingEntity(mapping)
                );
            dbConnection.Close();
            return null;
        }
    }
}
