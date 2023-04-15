using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository.InformationModel
{
    public interface IElementReadRepository : IReadRepository<Element>
    {
        Task<IEnumerable<Element>> GetElementsMatchingID(IEnumerable<Guid> elementIDs);
        Task<IEnumerable<Element>> GetAllElementsOfModel(Guid refToElement);
    }
}
