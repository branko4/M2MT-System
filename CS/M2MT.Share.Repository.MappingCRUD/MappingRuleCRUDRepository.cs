using Dapper;
using M2MT.Shared.Entity;
using M2MT.Shared.Entity.InformationModel;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System.Data;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRuleCRUDRepository : MappingRuleReadRepository, IMappingRuleCRUDRepository
    {
        public MappingRuleCRUDRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task AddElement(RefTo<MappingRule> mappingRule, RefTo<Element> element)
        {
            dbConnection.Open();
            await dbConnection.ExecuteAsync(
                "INSERT INTO mapping.\"Coupled_elements\" values(@Element, @MappingRule);",
                new CoupledElement { Element = element.ID, MappingRule = mappingRule.ID }
                );
            dbConnection.Close();
            return;
        }

        public async Task<MappingRule> Create(MappingRule mappingRule)
        {
            dbConnection.Open();
            var createdMapping = await dbConnection.QueryFirstAsync<MappingRuleEntity>(
                "INSERT INTO mapping.\"Mapping_rules\" values(@ID, @Mapping, @Name); SELECT * FROM mapping.\"Mapping_rules\" WHERE \"Mapping_rules\".\"ID\" = @ID",
                new MappingRuleEntity(mappingRule)
                );
            dbConnection.Close();
            return createdMapping.Convert();
        }

        public async Task<MappingRule> Remove(MappingRule mappingRule)
        {
            dbConnection.Open();
            await dbConnection.QueryAsync<MappingRuleEntity>(
                "DELETE FROM mapping.\"Mapping_rules\" WHERE \"Mapping_rules\".\"ID\" = @ID",
                new MappingRuleEntity(mappingRule)
                );
            dbConnection.Close();
            return null;
        }
    }
}
