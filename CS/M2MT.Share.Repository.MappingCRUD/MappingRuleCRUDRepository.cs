using Dapper;
using M2MT.Shared.Entity;
using M2MT.Shared.Entity.InformationModel;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRuleCRUDRepository : MappingRuleReadRepository, IMappingRuleCRUDRepository
    {
        public MappingRuleCRUDRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task AddElement(Guid mappingRule, Guid element)
        {
            await dbConnection.ExecuteAsync(
                "INSERT INTO mapping.\"Coupled_elements\" values(@Element, @MappingRule);",
                new CoupledElement { Element = element, MappingRule = mappingRule }
                );
            return;
        }

        public async Task AddElements(IEnumerable<RefTo<Element>> elements, MappingRule mappingrule)
        {
            foreach (var element in mappingrule.Elements)
            {
                await this.AddElement(mappingrule.ID, element.ID);
            }
        }

        public async Task<MappingRule> Create(MappingRule mappingRule)
        {
            var createdMapping = await dbConnection.QueryFirstAsync<MappingRuleEntity>(
                "INSERT INTO mapping.\"Mapping_rules\" (\"ID\", \"Mapping\", \"Name\") values(@ID, @Mapping, @Name); SELECT * FROM mapping.\"Mapping_rules\" WHERE \"Mapping_rules\".\"ID\" = @ID",
                new { ID = mappingRule.ID, Mapping = new Guid(mappingRule.Mapping.ToString()), Name = mappingRule.Name}
                );
            return createdMapping.Convert();
        }

        public async Task<MappingRule> Remove(Guid mappingRule)
        {
            await dbConnection.QueryAsync<MappingRuleEntity>(
                "DELETE FROM mapping.\"Mapping_rules\" WHERE \"Mapping_rules\".\"ID\" = @ID",
                new { ID = mappingRule }
                );
            return null;
        }
    }
}
