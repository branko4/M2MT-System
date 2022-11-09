using M2MT.Shared.IRepository.InformationModel;
using System.Collections.Generic;
using System;
using InformationModel = M2MT.Shared.Model.InformationModel.Model;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Model
{
    public class InformationModelReadRepository : IInformationModelReadRepository
    {
        private IDbConnection dbConnection;

        public InformationModelReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<InformationModel>> GetModel()
        {
            dbConnection.Open();
            var a = await dbConnection.QueryAsync<InformationModel>("SELECT * FROM model.\"Models\"");
            dbConnection.Close();
            return a != null ? a : new List<InformationModel>();
        }
    }
}
