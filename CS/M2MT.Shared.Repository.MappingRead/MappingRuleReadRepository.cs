using Dapper;
using M2MT.Shared.Entity.Mapping;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRuleReadRepository : IMappingRuleReadRepository
    {
        protected IDbConnection dbConnection;

        public MappingRuleReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<MappingRule>> GetAll()
        {
            dbConnection.Open();
            var mappingRules = await dbConnection.QueryAsync<MappingRuleEntity>("SELECT * FROM mapping.\"Mapping_rules\"");
            dbConnection.Close();
            return Converter.ConvertList<MappingRule, MappingRuleEntity>(mappingRules);
        }
    }
}
