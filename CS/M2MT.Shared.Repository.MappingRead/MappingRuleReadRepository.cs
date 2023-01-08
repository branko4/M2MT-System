using Dapper;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRuleReadRepository : IMappingRuleReadRepository
    {
        protected IDbConnection dbConnection;

        public MappingRuleReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            if (dbConnection.State != ConnectionState.Open) dbConnection.Open();
        }

        public async Task<IEnumerable<MappingRule>> GetAll()
        {
            var mappingRules = await dbConnection.QueryAsync<MappingRuleEntity>("SELECT * FROM mapping.\"Mapping_rules\"");
            return Converter.ConvertList<MappingRule, MappingRuleEntity>(mappingRules);
        }

        public async Task<IEnumerable<MappingRule>> GetAllRelatedWithMapping(Guid mappingRef)
        {
            var mappingRules = await dbConnection.QueryAsync<MappingRuleEntity>("SELECT * FROM mapping.\"Mapping_rules\" WHERE \"Mapping\" = @ID",
                new { ID = mappingRef });
            return Converter.ConvertList<MappingRule, MappingRuleEntity>(mappingRules);
        }

        public async Task<MappingRule> GetOne(Guid mappingRule)
        {
            var mappingRuleEntity = await dbConnection.QueryFirstAsync<MappingRuleEntity>(
                "SELECT * FROM mapping.\"Mapping_rules\" WHERE mapping.\"Mapping_rules\".\"ID\" = @ID;",
                new { ID = mappingRule });
            mappingRuleEntity.Elements = await dbConnection.QueryAsync<RefTo<Element>>(
                "SELECT \"Element\" AS \"ID\" FROM mapping.\"Coupled_elements\", mapping.\"Mapping_rules\" WHERE \"Mapping_rules\".\"ID\" = \"Coupled_elements\".\"Mapping_rule\" and \"Coupled_elements\".\"Mapping_rule\" = @ID;",
                new {ID = mappingRule}
                );
            return mappingRuleEntity.Convert();
        }

        public async Task<bool> Excists(Guid mappingRuleRef)
        {
            return await dbConnection.QueryFirstAsync<bool>("SELECT 1 FROM mapping.\"Mapping_rules\" WHERE mapping.\"Mapping_rules\".\"ID\" = @ID;",
                new { ID = mappingRuleRef });
        }
    }
}
