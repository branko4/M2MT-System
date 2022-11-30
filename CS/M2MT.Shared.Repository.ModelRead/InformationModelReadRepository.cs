using M2MT.Shared.IRepository.InformationModel;
using System.Collections.Generic;
using System;
using InformationModel = M2MT.Shared.Model.InformationModel.Model;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.Entity.InformationModel;

namespace M2MT.Shared.Repository.Model
{
    public class InformationModelReadRepository : IInformationModelReadRepository
    {
        private IDbConnection dbConnection;

        public InformationModelReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            if (dbConnection.State != ConnectionState.Open) dbConnection.Open();
        }

        public async Task<IEnumerable<InformationModel>> GetAll()
        {
            var models = await dbConnection.QueryAsync<ModelEntity>("SELECT * FROM model.\"Models\"");
            return Converter.ConvertList<InformationModel, ModelEntity>(models);
        }

        public async Task<InformationModel> GetOne(Guid ID)
        {
            var mappingRuleEntity = await dbConnection.QueryFirstAsync<ModelEntity>(
                "SELECT * FROM model.\"Models\" WHERE \"ID\" = @ID;",
                new { ID = ID });
            return mappingRuleEntity.Convert();
        }
    }
}
