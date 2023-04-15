
using Dapper;
using M2MT.Shared.Entity.InformationModel;
using M2MT.Shared.Entity.Util;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.Model.InformationModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using M2MT.Shared.Model;

namespace M2MT.Shared.Repository.Model
{
    public class ElementReadRepository : IElementReadRepository
    {
        private IDbConnection dbConnection;

        public ElementReadRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            if (dbConnection.State != ConnectionState.Open) dbConnection.Open();
        }

        public async Task<IEnumerable<Element>> GetAll()
        {
            var elements = await dbConnection.QueryAsync<ElementEntity>("SELECT * FROM model.\"Elements\"");
            return Converter.ConvertList<Element, ElementEntity>(elements);
        }

        public async Task<IEnumerable<Element>> GetAllElementsOfModel(Guid refTo)
        {
            var elements = await dbConnection.QueryAsync<ElementEntity>(
                "SELECT * FROM model.\"Elements\" WHERE \"Model\" = @ID",
                new { ID = refTo });
            return Converter.ConvertList<Element, ElementEntity>(elements);
        }

        public async Task<IEnumerable<Element>> GetElementsMatchingID(IEnumerable<Guid> elementIDs)
        {
            var elements = await dbConnection.QueryAsync<ElementEntity>("SELECT * FROM model.\"Elements\" WHERE \"ID\" = ANY(@IDs)",
                new { IDs = elementIDs.ToArray() });
            return Converter.ConvertList<Element, ElementEntity>(elements);
        }

        public async Task<Element> GetOne(Guid id)
        {
            // TODO FIXME find out rather connection should be closed
            var element = await dbConnection.QueryFirstAsync<ElementEntity>(
                "SELECT q.*, (SELECT array_agg((i.*)::text) FROM model.\"Attributes\" AS i  WHERE i.\"Element\" = @ID GROUP BY i.\"Element\" ) AS \"Attributes\" FROM model.\"Elements\" AS q WHERE q.\"ID\" = @ID;",
                new { ID = id });
            return element.Convert();
        }
        public async Task<bool> Excists(Guid elementRef)
        {
            return await dbConnection.QueryFirstAsync<bool>("SELECT 1 FROM model.\"Elements\" WHERE \"Model\" = @ID",
                new { ID = elementRef });
        }
    }
}
